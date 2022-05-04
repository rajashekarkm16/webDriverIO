using System;
using System.Globalization;

namespace Dnata.Automation.BDDFramework.Helpers
{
    class DateTimeHelper
    {

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetCurrentDateTimeInUTCFormat()
        {
            return DateTime.Now.ToUniversalTime();
        }

        public static string GetCurrentDateTimeInLoggingFormat()
        {
            return GetCurrentDateTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Generates unique value
        /// </summary>
        /// <returns>Unique value</returns>
        public static string GenerateTimeStamp()
        {
            return DateTime.Now.ToString("ssmmhhddMM");
        }

        /// <summary>
        /// Generates unique value
        /// </summary>
        /// <returns>Unique value</returns>
        public static string GenerateDateStamp()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        /// <summary>
        /// Convert date format
        /// </summary>
        /// <param name="date">date to be converted</param>
        /// <param name="currentDateFormat">format of the date passed</param>
        /// <param name="expectedDateFormat">expected date format</param>
        /// <returns>date as per expected date format</returns>
        public static string FormatDate(string date, string currentDateFormat, string expectedDateFormat, CultureInfo culture)
        {
            return DateTime.ParseExact(date, currentDateFormat, culture).ToString(expectedDateFormat, culture);
        }
    }
}
