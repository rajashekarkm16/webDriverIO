using Dnata.Automation.BDDFramework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System.Collections.ObjectModel;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public interface IAtWebElement : IWebElement, ILocatable, IWrapsElement
    {
        By Selector { get; set; }

        LocatorType Locator { get; set; }

        string LocatorValue { get; set; }

        /// <summary>
        /// Extended method for setting check state to checkbox
        /// </summary>
        void Check();

        /// <summary>
        /// Extended method for setting uncheck state to checkbox
        /// </summary>
        void UnCheck();

        AtWebElement FindElement(LocatorType locatorType, string locatorValue, params string[] param);
        AtWebElement FindElementByIndex(LocatorType locatorType, string locatorValue, int index, params string[] param);
        ReadOnlyCollection<AtWebElement> FindElements(LocatorType locatorType, string locatorValue, params string[] param);


        AtWebElement FindElement(AtBy atBy);
        AtWebElement FindElementByIndex(AtBy atBy,int index);
        ReadOnlyCollection<AtWebElement> FindElements(AtBy atBy);
    }
}