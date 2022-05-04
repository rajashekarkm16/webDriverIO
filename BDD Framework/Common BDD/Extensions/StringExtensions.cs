using System;

namespace Dnata.Automation.BDDFramework.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Allows case insensitive comparison
        /// </summary>
        public static bool Contains(this string source, string toCheck, StringComparison compare)
        {
            return source?.IndexOf(toCheck, compare) >= 0;
        }
    }
}
