using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class PriceFilter : MobileBasePage, IPriceFilter
    {
        private readonly IAtWebDriver _webDriver;

        #region [Constructor]
        public PriceFilter(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region [ Web Elements]

        AtWebElement PriceFrom => _webDriver.FindElement(LocatorType.CssSelector, "span.sc-c-slider__point-label--start");
        AtWebElement PriceTo => _webDriver.FindElement(LocatorType.CssSelector, "span.sc-c-slider__point-label--end");
        private AtBy byFliterPrice => GetBy(LocatorType.XPath, "(//span/span[contains(@class, 'sc-u-bold')])[2]");
        AtWebElement FliterPrice => _webDriver.FindElement(byFliterPrice);
        AtWebElement FilterPriceHeading => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-dialog-content h3.sc-o-heading");
        //left slider =1 and right slider = 2.
        AtWebElement TotalPriceSliderHandle(int index) => _webDriver.FindElement(LocatorType.XPath, "(//span[text()= 'Total Price']//ancestor::div[contains(@class, 'sc-c-accordion sc-c-accordion--condensed')]//span[@class= 'sc-c-slider-handle'])[#]", index.ToString());
        AtWebElement SliderTrack => _webDriver.FindElement(LocatorType.CssSelector, "span.sc-c-slider__track");
        private AtBy byFiltersPageLoader => GetBy(LocatorType.CssSelector, "span.sc-c-throbber");

        #endregion

        #region [ Methods]

        public string GetPriceFilterHeader()
        {
            return FilterPriceHeading.Text;
        }

        public decimal GetPriceFrom()
        {
            _webDriver.WaitForElementVisible(PriceFrom);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo( PriceFrom.Text));
        }

        public decimal GetPriceTill()
        {
            _webDriver.WaitForElementVisible(PriceTo);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PriceTo.Text));
        }

        public decimal GetUpToPrice()
        {
            _webDriver.WaitForElementVisible(byFliterPrice, Constants.MediumWait, "Filter price is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(FliterPrice.Text));
        }

        public void SetPriceFilter(int xCoordinate, int yCoordinate=0)
        { 
            TotalPriceSliderHandle(2).DragSliderByOffset(xCoordinate, yCoordinate);
            _webDriver.WaitUntilNotVisible(_webDriver.FindElement(byFiltersPageLoader), 40, "filter page loader is visible");
        }

        #endregion
    }
}
