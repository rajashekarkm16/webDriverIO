using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.Extensions
{
    public static class IwebElementExtensions
    {
        public static bool IsElementPresent(this IWebElement element, By by)
        {
            const bool present = false;
            try
            {
                element.FindElement(by);
            }
            catch
            {
                return present;
            }

            return true;
        }

        public static bool IsClickable(this IWebElement element)
        {
            return element.Enabled;
        }
    }
}
