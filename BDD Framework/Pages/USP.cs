using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class USP : MobileBasePage, IUSP
    {
        private readonly IAtWebDriver _webDriver;

        #region [Constructor]
        public USP(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region [ Web Elements]

        private AtBy byUSPItems => GetBy(LocatorType.CssSelector, "div.sc-c-usp");
        private ReadOnlyCollection<AtWebElement> USPItems => _webDriver.FindElements(byUSPItems);
        private AtWebElement USPImage => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-usp__image");
        private AtWebElement USPTitle => _webDriver.FindElement(LocatorType.CssSelector, "h5.sc-c-usp__title");
        private AtWebElement USPBody => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-usp p.sc-o-body");

        #endregion

        #region [ Methods]

        public void ClickUSP(int index)
        {
            _webDriver.ScrollElementToCenter(USPItems[index]);
            _webDriver.WaitForElementVisible(USPItems[index]);
            USPItems[index].Click();
        }

        public string GetBody(int index)
        {
            _webDriver.WaitForElementVisible(USPItems[index]);
            return USPItems[index].FindElement(USPBody).Text;
        }

        public string GetHeading(int index)
        {
            _webDriver.WaitForElementVisible(USPItems[index]);
            return USPItems[index].FindElement(USPTitle).Text;
        }
        #endregion
    }
}
