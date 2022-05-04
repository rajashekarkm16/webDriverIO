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
    public class LandingPageSearchComponent : SearchComponent, ILandingPageSearchComponent
    {

        private AtBy bySearchHeader => GetBy(LocatorType.XPath, "//h3[text()='Your Search']");

        // Holidays Tab
        private AtBy byHolidaysTab => GetBy(LocatorType.XPath, "//button[span[@class='sc-c-tab__label'][normalize-space()='HOLIDAYS']]");
        private AtWebElement HolidaysTab => _webDriver.FindElement(byHolidaysTab);

        // Hotels Tab
        private AtBy byHotelOnlyTab => GetBy(LocatorType.XPath, "//button[span[normalize-space()='HOTEL ONLY']]");
        private AtWebElement HotelOnlyTab => _webDriver.FindElement(byHotelOnlyTab);

        // Destination
        private AtBy byDestination => GetBy(LocatorType.XPath, "//div[div[text()='Destination']]/button");
        private AtWebElement Destination => _webDriver.FindElement(byDestination);
        private AtBy byDestinationText => GetBy(LocatorType.XPath, "//input[@placeholder='Add a destination']");
        private AtWebElement DestinationText => _webDriver.FindElement(byDestinationText);
        private AtBy byDestinationAutoCompleterItem => GetBy(LocatorType.XPath, "//li//div[@class='sc-c-autocompleter-option__main']/div[1]");
        private AtWebElement DestinationAutoCompleterItem(int index) => _webDriver.FindElement(LocatorType.XPath, "//li[#]//div[@class='sc-c-autocompleter-option__main']/div[1]", index.ToString());

        // Flying From
        private AtWebElement FlyingFrom => _webDriver.FindElement(LocatorType.XPath, "//div[div[text()='Flying from']]/button");
        private AtWebElement FlyingFromText => _webDriver.FindElement(LocatorType.Id, "airport-autocompleter-field");
        private AtBy byFlyingFromAutoCompleterCategories => GetBy(LocatorType.XPath, "//li/div[@class='sc-c-autocompleter-option']");
        private ReadOnlyCollection<AtWebElement> FlyingFromAutoCompleterCategories => _webDriver.FindElements(byFlyingFromAutoCompleterCategories);
        private ReadOnlyCollection<AtWebElement> FlyingFromAutoCompleterCategoryItems(int categoryIndex = 1) => _webDriver.FindElements(LocatorType.XPath, "(//li[div[@class='sc-c-autocompleter-option']])[#]/ul/li", categoryIndex.ToString());

        // Dates
        private AtBy byDates => GetBy(LocatorType.XPath, "//div[div[text()='Dates']]/button");
        private AtWebElement Dates => _webDriver.FindElement(byDates);

        // Occupancy
        private AtBy byGuests => GetBy(LocatorType.XPath, "//div[div[text()='Guests']]/button");
        private AtWebElement Guests => _webDriver.FindElement(byGuests);

        // Search
        private AtWebElement Search => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Search']");


        private readonly IAtWebDriver _webDriver;

        public LandingPageSearchComponent(IAtWebDriver webDriver) 
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public override void ClickDestinationField()
        {
            _webDriver.WaitForElementVisible(byDestination, 30, "Destination is no visible");
            Destination.Click();
        }

        public override void PopulateDestination(string destination)
        {
            _webDriver.WaitInSec(2);
            ClickDestinationField();
            _webDriver.WaitInSec(1);
            _webDriver.WaitForElementVisible(byDestinationText, 30, "Destination input control is not visible");
            DestinationText.Clear();
            _webDriver.WaitInSec(1);
            DestinationText.SendKeys(destination);
            _webDriver.WaitInSec(2);
            _webDriver.WaitForTextPresent(byDestinationAutoCompleterItem, destination, TimeSpan.FromMilliseconds(250), 10);
            _webDriver.WaitForElementVisible(byDestinationAutoCompleterItem, 30, "Destination Autocompleter items are not visible");
            DestinationAutoCompleterItem(1).ClickButtonUsingJs();
        }

        public override void ClickAirportField()
        {
            FlyingFrom.ClickButtonUsingJs();
        }

        public override void PopulateAirport(string destination)
        {
            if (!FlyingFromText.Visible)
            {
                ClickAirportField();
            }
            FlyingFromText.SendKeys(destination);
            _webDriver.WaitForTextPresent(byFlyingFromAutoCompleterCategories, destination, TimeSpan.FromSeconds(1), 5);
            FlyingFromAutoCompleterCategories[0].ClickButtonUsingJs();
        }

        public override void EditDates()
        {
            Dates.ClickButtonUsingJs();
        }

        public override void EditPassengers()
        {
            Guests.ClickButtonUsingJs();
        }

        public override void ClickCheckAvailability()
        {
            Search.ClickButtonUsingJs();
        }

        public override void SelectHotelsTab()
        {
            _webDriver.WaitForElementVisible(byHotelOnlyTab, 10, "Holiday tab on search modal is not visible");
            _webDriver.WaitForElementClickable(byHotelOnlyTab, Constants.DefaultWait, "Hotel tab is not clickable");
            if (!IsHotelsTabSelected())
                HotelOnlyTab.Click();
        }

        public override bool IsHotelsTabSelected()
        {
            return HotelOnlyTab.GetAttribute("aria-selected") == "true";
        }

        public bool IsHomepageDisplayed()
        {
            return HotelOnlyTab.Visible;
        }
    }
}
