using Dnata.Automation.BDDFramework.BrowserStack;
using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Dnata.TravelRepublic.MobileWeb.UI
{
    public class HelperFunctions
    {
        public static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int count, int minValue = 1)
        {
            return random.Next(minValue, count);
        }

        public static bool IsLive()
        {
            if ((Environment.GetEnvironmentVariable("Environment") == null && AtConfiguration.GetConfiguration("Environment") == "Prod") || Environment.GetEnvironmentVariable("Environment") == "Prod")
                return true;
            else
                return false;
        }

        public static string GetBrowser()
        {
            if (Environment.GetEnvironmentVariable("Browser") != null)
                return Environment.GetEnvironmentVariable("Browser");
            else
                return AtConfiguration.GetConfiguration<string>("Browser");
        }

        public static bool IsV3HomepageEnabled()
        {
            return true;
        }

        public static bool IsDesktop()
        {
            if (GetIsRemote())
                return !IsBrowserStackMobileDevice();
                
            else if (GetBrowser().Equals("Chrome_Mobile", StringComparison.OrdinalIgnoreCase) && !GetIsRemote())
                return false;
            else
                return true;
        }

        public static DateTime GetDateOfBirth(int age)
        {
           return DateTime.Now.AddYears(-age); 
        }

        public static bool IsCovidNoticeEnabled()
        {
            return IsTRUK();
        }

        public static bool IsTRUK()
        {
            if (Environment.GetEnvironmentVariable("Domain") == null)
                return AtConfiguration.GetConfiguration("Domain").Equals("UK");
            else
                return Environment.GetEnvironmentVariable("Domain").Equals("UK");
        }

        public static bool GetIsRemote()
        {
            if (Environment.GetEnvironmentVariable("IsRemote") != null)
                return Convert.ToBoolean(Environment.GetEnvironmentVariable("IsRemote"));
            else
                return AtConfiguration.GetConfiguration<bool>("IsRemote");
        }

        public static bool IsBrowserStackMobileDevice()
        {
            if (Environment.GetEnvironmentVariable("IsMobileDevice") != null)
                return Convert.ToBoolean(Environment.GetEnvironmentVariable("IsMobileDevice"));
            else
                return AtConfiguration.GetConfiguration<bool>("IsMobileDevice");
        }

        public static IReadOnlyCollection<object> GetAllDataLayerEntries(IAtWebDriver _webDriver)
        {
            return _webDriver.ExecuteScript("return window.dataLayer.filter(obj => obj.hasOwnProperty('event'))") as ReadOnlyCollection<Object>;
        }

        public static Dictionary<string, dynamic> GetGADataByKeyAndValue(IAtWebDriver _webDriver, string gKey, string gValue)
        {
            int counter = 1;
            IReadOnlyCollection<object> GAData = GetAllDataLayerEntries(_webDriver);
            Dictionary<string, dynamic> newD = new Dictionary<string, dynamic>();
            foreach (Object obj in GAData)
            {
                newD.Clear();
                Console.WriteLine("Counter " + counter);
                if (typeof(IDictionary).IsAssignableFrom(obj.GetType()))
                {
                    IDictionary idict = (IDictionary)obj;

                    
                    foreach (object key in idict.Keys)
                    {
                        if (idict[key] != null)
                            newD.Add(key.ToString(), idict[key]);
                    }

                    if (newD.Values.Contains(gValue) && newD.Keys.Contains(gKey))
                    {
                        return newD;
                    }
                }
                else
                {
                    // My object is not a dictionary
                }

                counter++;
            }
            return newD;
        }

        public static Dictionary<string, dynamic> GetGADataByKeyAndValueIgnoringEarlierData(IAtWebDriver _webDriver, IReadOnlyCollection<Object> ignoreData, string gKey, string gValue)
        {

            IReadOnlyCollection<object> GAData = GetAllDataLayerEntries(_webDriver);
            Dictionary<string, dynamic> newData = new Dictionary<string, dynamic>();

            int counter = 1;
            int initialLength = ignoreData.Count;
            int currentLength = GAData.Count;
            
            foreach (Object obj in GAData)
            {
                if (counter > initialLength)
                {
                    newData.Clear();
                    if (typeof(IDictionary).IsAssignableFrom(obj.GetType()))
                    {
                        IDictionary idict = (IDictionary)obj;

                        foreach (object key in idict.Keys)
                        {
                            if (idict[key] != null)
                                newData.Add(key.ToString(), idict[key]);
                        }

                        if (newData.Values.Contains(gValue) && newData.Keys.Contains(gKey))
                        {
                            return newData;
                        }
                    }
                    else
                    {
                        // My object is not a dictionary
                    }
                }
                counter++;
            }
            return newData;
        }

        public static int GetDomainID()
        {
            if (IsTRUK())
                return 1;
            else
                return 2;
        }


        public static string GetCulture()
        {
            if (IsTRUK())
                return "en-GB";
            else
                return "en-IE";
        }

        public static string GetFacadeURL()
        {
            string env;

            if (Environment.GetEnvironmentVariable("Environment") != null)
                env = Environment.GetEnvironmentVariable("Environment");
            else
                env = AtConfiguration.GetConfiguration("Environment");

            if (env == "Prod")
                return AtConfiguration.GetConfiguration("LiveFacadeUrl");
            else if ((env == "QA"))
                return AtConfiguration.GetConfiguration("QAFacadeUrl");
            else
                return AtConfiguration.GetConfiguration("PPFacadeUrl");
        }

        public static string RemoveContentBetweenCharacters(string inputString, char startChar, char endChar)
        {
            for (int count = 0; count < inputString.Length; count++)
            {
                if (inputString[count] == startChar)
                {
                    int startIndex = count;
                    while (inputString[count] != endChar)
                    {
                        count++;
                    }
                    int endIndex = count + 1;
                    inputString = inputString.Remove(startIndex, endIndex - startIndex);
                    count = startIndex - 1;
                }
            }
            return inputString;          
        }

        public static bool IsAllHolidayPackage()=> true;
        
        public static bool IsPayMonthlyEnabledForHotels()
        {
            return false;
        }

        public static bool IsDynamicCancellationEnabled()
        {
            return true;
        }

        public static bool IsV3SignInEnabled() => true;

        public static bool IsPDS2EnabledOnV3() => true;

    }
}
