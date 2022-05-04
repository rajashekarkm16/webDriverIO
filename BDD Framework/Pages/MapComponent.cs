using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class MapComponent : MobileBasePage, IMapComponent
    {
        private IAtWebDriver _webDriver;
        public MapComponent(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        private AtWebElement MapHeader => _webDriver.FindElement(LocatorType.Id, "map-dialog-dialog-title");
        private AtBy byMapPin => GetBy(LocatorType.XPath, "//div[not (@title)]/img[contains(@src,'map-pin')]");
        private ReadOnlyCollection<AtWebElement> MapPin => _webDriver.FindElements(byMapPin);
        private AtBy byHotelName => GetBy(LocatorType.XPath, "//div[contains(@class,'dialog')]//a//h5");
        private AtWebElement HotelName => _webDriver.FindElement(byHotelName);
        private AtWebElement HotelLocation => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'sc-o-flex')]/h5/following-sibling::*");
        private AtWebElement HotelPrice => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'sc-o-flex')]/h5/parent::div/following-sibling::div//*[contains(@class,'color-accent')]");
        private AtWebElement CloseMapPinImage => _webDriver.FindElement(LocatorType.XPath, "(//button[@title='Close' and @class='gm-ui-hover-effect'])[1]");
        private AtWebElement CloseMapView => _webDriver.FindElement(LocatorType.XPath, "//div/header[contains(@class,'primary')]//button[@aria-label='Close']");

        public string GetMapPageHeader()
        {
            _webDriver.WaitForElementVisible(byMapPin, 20, "Map pin is not visible");
            return MapHeader.Text;
        }

        public HotelInformation GetHotelInformation()
        {
            _webDriver.WaitForElementVisible(byMapPin, 20, "Map pin is not visible");
            HotelInformation hotelInformation = new HotelInformation();
            MapPin[0].ClickUsingActions();
            hotelInformation.HotelName = GetHotelName();
            hotelInformation.Location = GetHotelLocation();
            hotelInformation.Price = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(GetHotelPrice()));
            return hotelInformation;
        }

        public string GetHotelName()
        {
            _webDriver.WaitForElementVisible(byHotelName, 10, "Hotel name is not displayed");
            return HotelName.Text;
        }

        public string GetHotelLocation()
        {
            return HotelLocation.Text;
        }

        public string GetHotelPrice()
        {
            return HotelPrice.Text;
        }

        public void SelectHotelFromMaps()
        {
            _webDriver.WaitForElementVisible(byHotelName, 20, "Hotel name is not visible");
            HotelName.Click();
        }

        public void SelectHotelPin()
        {
            _webDriver.WaitForElementVisible(byMapPin, 20, "Map pin is not visible");
            MapPin[0].ClickUsingActions();
        }

        public int GetLocationPins()
        {
            _webDriver.WaitForElementVisible(byMapPin, 20, "Map pin is not visible");
            return MapPin.Count;
        }
    }
}
