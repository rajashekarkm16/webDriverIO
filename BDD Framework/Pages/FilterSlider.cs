using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class FilterSlider: MobileBasePage, IFilterSlider
    {
        private readonly IAtWebDriver _webDriver;

        public FilterSlider(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        #region [ Web Elements]

        private AtWebElement SliderFrom => _webDriver.FindElement(LocatorType.CssSelector, "span.sc-c-slider__point-label--start");
        private AtWebElement SliderTo => _webDriver.FindElement(LocatorType.CssSelector, "span.sc-c-slider__point-label--end");
        private AtBy ByFilterValue(string filterHeading) => GetBy(LocatorType.XPath, "//span[text()='#']//ancestor::div[2]//span[@class='sc-c-pill__caption']", filterHeading);
        private AtWebElement FilterValue(string filterHeading) => _webDriver.FindElement(ByFilterValue(filterHeading));
        private AtWebElement DepartureTimeHeading(string filterHeading) => _webDriver.FindElement(LocatorType.XPath, "//span[text()='#']",filterHeading);
        private AtBy BySliderMinHandle(string filterHeading) => GetBy(LocatorType.XPath, "//span[text()='#']//ancestor::div[2]//span[@class='sc-c-slider-handle'][1]", filterHeading);
        private AtWebElement SliderMinHandle(string filterHeading) => _webDriver.FindElement(BySliderMinHandle(filterHeading));
        private AtBy BySliderMaxHandle(string filterHeading) => GetBy(LocatorType.XPath, "//span[text()='#']//ancestor::div[2]//span[@class='sc-c-slider-handle'][2]", filterHeading);
        private AtWebElement SliderMaxHandle(string filterHeading) => _webDriver.FindElement(BySliderMaxHandle(filterHeading));
        private AtWebElement SliderTrack => _webDriver.FindElement(LocatorType.CssSelector, "span.sc-c-slider__track");        
        #endregion

        #region[Methods]

        public string GetOutBoundDepartureTimeValues()
        {
            _webDriver.WaitForElementVisible(ByFilterValue("Outbound Departure Times"), 20, "Filter value is not visible");
            return FilterValue("Outbound Departure Times").Text;
        }

        public string GetReturnDepartureTimeValues()
        {
            _webDriver.WaitForElementVisible(ByFilterValue("Return Departure Times"), 20, "Filter value is not visible");
            return FilterValue("Return Departure Times").Text;
        }

        public void SetOutBoundDepartureTimeMaxFilter(int xCoordinate, int yCoordinate = 0)
        {
            _webDriver.WaitForElementVisible(BySliderMaxHandle("Outbound Departure Times"), 20, "Filter value is not visible");
            SliderMaxHandle("Outbound Departure Times").DragSliderByOffset(xCoordinate, yCoordinate);
        }

        public void SetOutBoundDepartureTimeMinFilter(int xCoordinate, int yCoordinate = 0)
        {
            _webDriver.WaitForElementVisible(BySliderMinHandle("Outbound Departure Times"), 20, "Filter Slider is not visible");
            SliderMinHandle("Outbound Departure Times").DragSliderByOffset(xCoordinate, yCoordinate);
        }       
        public void SetReturnDepartureTimeMinFilter(int xCoordinate, int yCoordinate = 0)
        {
            _webDriver.WaitForElementVisible(BySliderMinHandle("Return Departure Times"), 20, "Filter Slider is not visible");
            SliderMinHandle("Return Departure Times").DragSliderByOffset(xCoordinate, yCoordinate);
        }
        public void SetReturnDepartureTimeMaxFilter(int xCoordinate, int yCoordinate = 0)
        {
            _webDriver.WaitForElementVisible(BySliderMaxHandle("Return Departure Times"), 20, "Filter Slider is not visible");
            SliderMaxHandle("Return Departure Times").DragSliderByOffset(xCoordinate, yCoordinate);
        }
        #endregion
    }
}
