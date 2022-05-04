using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Dnata.Automation.BDDFramework.Utils;
using Dnata.Automation.BDDFramework.Extensions;
using NUnit.Framework;
using OfficeOpenXml;
using static System.String;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dnata.Automation.BDDFramework.Helpers
{
    public static class CommonFunctions
    {
        #region -- Data Members --
        static readonly char[] hexDigits = {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        private static Random random = new Random();

        #endregion

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static double GetRandomDouble(int max)
        {
            return random.NextDouble() * max;
        }

        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();

            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) ||
                (strInput.StartsWith("[") && strInput.EndsWith("]")))
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates Json template Key Value where json item contains needed value
        /// </summary>
        /// <param name="template">JObject Json template which will be updated</param>
        /// <param name="key">Key to which value be updated</param>
        /// <param name="value">Updated value</param>
        /// <param name="itemValue">Value by which will be found item in Json</param>
        /// <returns>JObject with updated template</returns>
        public static JObject UpdateJsonTemplate(JObject template, string key, string value, string itemValue)
        {
            var items = template.Descendants().OfType<JObject>().Where(x => x.ToString().Contains(itemValue));

            foreach (var item in items)
            {
                item[key] = value;
            }

            return template;
        }

        public static JObject UpdateJsonTemplate(JObject template, string key, string value)
        {
            template[key] = value;
            return template;
        }

        public static JObject DeleteKeyInJson(JObject template, string key)
        {
            if (!(template.Property(key) is null))
            {
                template.Property(key).Remove();
            }
            return template;
        }

        public static string FormatJson(string payload)
        {
            var instance = JsonConvert.DeserializeObject(payload);
            return JsonConvert.SerializeObject(instance, Formatting.None);
        }

        public static bool IsFileCreated(int fileCreationTime, TimeSpan waitingTime)
        {
            var waitedTime = TimeSpan.Zero;

            while (waitedTime <= waitingTime)
            {
                var downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                var files = Directory.GetFiles(downloadsPath);
                if (files.Any(file => File.GetCreationTime(file).AddMinutes(fileCreationTime) > DateTime.Now))
                    return true;
                Thread.Sleep(1000);
                waitedTime = waitedTime.Add(TimeSpan.FromSeconds(1));
            }

            return false;
        }

        public static bool IsFileCreated(string fileName, int fileCreationTime, TimeSpan waitingTime)
        {
            var waitedTime = TimeSpan.Zero;

            while (waitedTime <= waitingTime)
            {
                var downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
                var files = Directory.GetFiles(downloadsPath);
                if (files.Any(file =>
                    File.GetCreationTime(file).AddMinutes(fileCreationTime) > DateTime.Now &&
                    file.Contains(fileName)))
                    return true;
                Thread.Sleep(1000);
                waitedTime = waitedTime.Add(TimeSpan.FromSeconds(1));
            }

            return false;
        }

        public static string GetFilePathDownloadedFile(string fileType)
        {
            var downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            var files = Directory.GetFiles(downloadsPath);
            var filePath = files.OrderByDescending(File.GetCreationTime)
                .First(fileName => fileName.Contains(fileType.GetDescription()));

            return filePath;
        }

        public static IEnumerable<T> ParseDownloadedFile<T>(string filePath) where T : new()
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var excel = new ExcelPackage(fileStream);
                var workSheet = excel.Workbook.Worksheets[2];
                var newcollection = workSheet.ConvertSheetToObjects<T>().Where(x => !IsAllNullOrEmpty(x)).ToList();

                return newcollection;
            }
        }

        public static bool IsAllNullOrEmpty(object myObject)
        {
            return myObject.GetType().GetProperties()
                .Where(pi => pi.GetValue(myObject) is string)
                .Select(pi => (string) pi.GetValue(myObject))
                .All(IsNullOrEmpty);
        }

        public static void PropertyValuesAreEquals(object actual, object expected, string message = null)
        {
            PropertyInfo[] properties = expected.GetType().GetProperties();

            if (actual is IList list)
            {
                var actualList = list;
                var expectedList = (expected as IList);

                if (actualList.Count != expectedList?.Count)
                    Assert.Fail($"Expected IList containing {expectedList?.Count} elements but was IList containing {actualList.Count} elements");

                for (int i = 0; i < actualList.Count; i++)
                {
                    PropertyValuesAreEquals(actualList[i], expectedList[i]);
                }
                return;
            }

            foreach (var property in properties)
            {
                var expectedValue = property.GetValue(expected, null);
                var actualValue = property.GetValue(actual, null);

                if (expectedValue == null)
                {
                    continue;
                }

                if (((expectedValue.GetType().GetProperties()).Length == 0) ||
                    (expectedValue is string) ||
                    (expectedValue is DateTime))
                {
                    if (Equals(expectedValue, actualValue)) continue;
                    if (message != null)
                        Console.WriteLine(message);
                    Assert.Fail($"Property {property.DeclaringType.Name}.{property.Name} does not match. Expected: {expectedValue} but was: {actualValue}");

                }
                else
                {
                    PropertyValuesAreEquals(actualValue, expectedValue);
                }
            }
        }

        public static void AssertListsAreEquals(IList actualList, IList expectedList)
        {
            if (actualList.Count != expectedList.Count)
                Assert.Fail($"Expected IList containing {expectedList.Count} elements but was IList containing {actualList.Count} elements");

            for (int i = 0; i < actualList.Count; i++)
                PropertyValuesAreEquals(actualList[i], expectedList[i]);
        }

        public static void Succeed(IEnumerable<Action> assertions)
        {
            var errors = new List<Exception>();

            foreach (var assertion in assertions)
            {
                try
                {
                    assertion();
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (errors.Any())
            {
                var ex = new AssertionException(
                    Join(Environment.NewLine, errors.Select(e => e.Message)),
                    errors.First());

                // Use stack trace from the first exception to ensure first
                // failed Assert is one click away
                ReplaceStackTrace(ex, errors.First().StackTrace);

                throw ex;
            }
        }

        private static void ReplaceStackTrace(Exception exception, string stackTrace)
        {
            var remoteStackTraceString = typeof(Exception)
                .GetField("_remoteStackTraceString",
                    BindingFlags.Instance | BindingFlags.NonPublic);

            remoteStackTraceString.SetValue(exception, stackTrace);
        }
        /// <summary>
        /// Method checks if passed string is datetime
        /// </summary>
        /// <param name="text">string text for checking</param>
        /// <returns>bool - if text is datetime return true, else return false</returns>
        public static bool IsDateTime(string text)
        {
            // Check for empty string.
            if (IsNullOrEmpty(text))
            {
                return false;
            }

            var isDateTime = DateTime.TryParse(text, out var dateTime);

            return isDateTime;
        }

        //Method to convert RGB color to HEX 
        public static string ConvertRgbaToHex(string cssColor)
        {
            var color = new Color();
            cssColor = cssColor.Trim();

            if (!cssColor.StartsWith("rgb")) throw new FormatException("Not rgb, rgba or hexa color string");
            var left = cssColor.IndexOf('(');
            var right = cssColor.IndexOf(')');

            if (left < 0 || right < 0)
                throw new FormatException("rgba format error");
            var noBrackets = cssColor.Substring(left + 1, right - left - 1);

            var parts = noBrackets.Split(',');

            var r = int.Parse(parts[0], CultureInfo.InvariantCulture);
            var g = int.Parse(parts[1], CultureInfo.InvariantCulture);
            var b = int.Parse(parts[2], CultureInfo.InvariantCulture);

            if (parts.Length == 3)
            {
                color = Color.FromArgb(r, g, b);
            }
            else
            {
                var a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                if (parts.Length == 4)
                {
                    color =  Color.FromArgb((int)(a * 255), r, g, b);
                }
            }

            return ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb())).ToLower();
        }

        /// <summary>
        /// Convert a .NET Color to a hex string.
        /// </summary>
        /// <returns>ex: "FFFFFF", "AB12E9"</returns>
        public static string ColorToHexString(Color color)
        {
            var bytes = new byte[3];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            var chars = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }

            return "#" + new string(chars).ToLower();
        }

        /// <summary>
        /// Removes the currency information from the cost
        /// </summary>
        public static string RemoveCurrencyInfo(string cost, string updatedCurrency = "")
        {
            //VerifyCurrency(cost,updatedCurrency);
            //return cost.Replace("£", "").Replace("€", "").Replace("AED", "").Replace(" ", "").Replace("$", "").Replace("INR","");
            if (cost.Split('\n').Length > 1)
                cost = cost.Split('\n')[1];
            if (cost.Contains("-"))
                return "-" + Regex.Match(cost, @"[^£$€ A-Za-z()-]+").Value;
            else
                return Regex.Match(cost, @"[^£$€ A-Za-z()-+]+").Value;
        }

        public static void LogToConsole(string value)
        {
            // allow indenting
            if (!string.IsNullOrEmpty(value) && value.Length > 0 && value.Substring(0, 1) != "*")
            {
                value = "      " + value;
            }

            // write the log
            Console.WriteLine(value);
        }
    }
}
