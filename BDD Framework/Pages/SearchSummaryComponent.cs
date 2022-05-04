using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class SearchSummaryComponent: MobileBasePage, ISearchSummaryComponent
    {
        private readonly IAtWebDriver _webDriver;

        public SearchSummaryComponent(IAtWebDriver driver)
            : base(driver)
        {
            _webDriver = driver;
        }

        private AtBy byEditSearchButton => GetBy(LocatorType.CssSelector, "div.sc-c-container button[class*=square]");
        private AtWebElement EditSearchButton => _webDriver.FindElement(byEditSearchButton);
        private AtBy bySearchResultsLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy bySearchItinerary => GetBy(LocatorType.CssSelector, "div[class*=nowrap] div p.sc-o-body--s");
        private AtWebElement SearchItinerary => _webDriver.FindElement(bySearchItinerary);
        private AtBy bySearchLocation => GetBy(LocatorType.XPath, "//div[contains(@class,'nowrap')]//p/parent::div/div[contains(@class,'4xs')]");
        private AtWebElement SearchLocation => _webDriver.FindElement(bySearchLocation);

        public void EditSearch()
        {
            WaitUntilPageLoads();
            _webDriver.ScrollToElement(EditSearchButton);
            EditSearchButton.Click();
        }

        public bool IsEditSearchButtonVisible()
        {
            _webDriver.WaitForElementVisible(bySearchLocation, Constants.DefaultWait, "Search Location is not visible");
            return EditSearchButton.Visible;
        }

        public void WaitUntilPageLoads()
        {
            //stability issue work around fix
            try
            {
                _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.DefaultWait);
                _webDriver.WaitForAnyVisible(bySearchLocation, bySearchItinerary, Constants.DefaultWait);
            }
            catch { }
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.DefaultWait);
            _webDriver.WaitForAnyVisible(bySearchLocation, bySearchItinerary, Constants.DefaultWait);
        }

        public string GetSearchItinerary()
        {
            WaitUntilPageLoads();
            return SearchItinerary.Text;
        }

        public string GetSearchLocation()
        {
            WaitUntilPageLoads();
            return SearchLocation.Text;
        }
    }
}
