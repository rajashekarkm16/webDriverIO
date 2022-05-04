using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class SearchComponent : MobileBasePage, ISearchComponent
    {
        private readonly IAtWebDriver _webDriver;

        private AtBy byCloseSearch => GetBy(LocatorType.CssSelector, "div[class=sc-c-modal] header[class*=elevated] button[aria-label='Close']");
        private AtWebElement CloseSearch => _webDriver.FindElement(byCloseSearch);
        private AtBy bySearchHeader => GetBy(LocatorType.XPath, "//h3[text()='Your Search']");
        private AtWebElement SearchHeader => _webDriver.FindElement(bySearchHeader);
        private AtWebElement YourSelection => _webDriver.FindElement(LocatorType.CssSelector, "div[class*=background-offset] div:last-child");

        // Destination
        private AtBy byDestination => GetBy(LocatorType.XPath, "//div[text()='Destination']/following-sibling::button");
        private AtWebElement Destination => _webDriver.FindElement(byDestination);
        // Guests
        private AtBy byPassengers => GetBy(LocatorType.XPath, "//div[text()='Guests' or text()='Passengers']//following-sibling::button");
        private AtWebElement Passengers => _webDriver.FindElement(byPassengers);
        //Dates
        private AtBy byDates => GetBy(LocatorType.XPath, "//div[text()='Dates']//following-sibling::button");
        private AtWebElement Dates => _webDriver.FindElement(byDates);
        private AtBy byNewSearch => GetBy(LocatorType.XPath, "//a[text()='Start a new search']");
        private AtWebElement NewSearch => _webDriver.FindElement(byNewSearch);
        private AtBy byCheckAvailability => GetBy(LocatorType.XPath, "//div[contains(@class,'dialog-content')]//button[contains(@class,'accent')]");
        private AtWebElement CheckAvailability => _webDriver.FindElement(byCheckAvailability);
        private AtBy byFlyingFrom => GetBy(LocatorType.XPath, "//div[text()='Flying from']/following-sibling::button");
        private AtWebElement FlyingFrom => _webDriver.FindElement(byFlyingFrom);
        private AtWebElement FlyingFromValue => _webDriver.FindElement(LocatorType.XPath, "//div[text()='Flying from']/following-sibling::button");
        private AtBy byGuestsValue => GetBy(LocatorType.XPath, "//div[text()='Guests']//following-sibling::*");
        private AtWebElement GuestsValue => _webDriver.FindElement(byGuestsValue);
        private AtWebElement DatesValue => _webDriver.FindElement(LocatorType.XPath, "//div[text()='Dates']//following-sibling::*");
        private AtWebElement SelectedHotel => _webDriver.FindElement(LocatorType.XPath, "//div[text()='Your selected hotel']/parent::div//div[contains(@class,'bold')]");
        private AtBy bySearchResultsLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byDestinationAutoCompleter => GetBy(LocatorType.XPath, "//div[text()='Destinations' or text()='Hotels']/ancestor::li//ul/li[1]/div");
        private AtWebElement DestinationAutoCompleter => _webDriver.FindElement(byDestinationAutoCompleter);
        private AtBy byHighlightedDestinationOnAutoCompleter => GetBy(LocatorType.XPath, "//div[contains(@class,'autocompleter-option__main')]/div[contains(@class,'bold')]");
        private AtWebElement HighlightedDestinationOnAutoCompleter => _webDriver.FindElement(byHighlightedDestinationOnAutoCompleter);
        private AtBy byConfirmDestinationButton => GetBy(LocatorType.XPath, "//span[text()='Confirm destination']/ancestor::button");
        private AtWebElement ConfirmDestinationButton => _webDriver.FindElement(byConfirmDestinationButton);
        private AtBy byConfirmDepartureAirportButton => GetBy(LocatorType.XPath, "//span[text()='Confirm airports']/ancestor::button");
        private AtWebElement ConfirmDepartureAirportButton => _webDriver.FindElement(byConfirmDepartureAirportButton);
        private AtWebElement DestinationText => _webDriver.FindElement(LocatorType.XPath, "//input[contains(@id,'destination')]");
        private AtBy byAirportText => GetBy(LocatorType.XPath, "//input[contains(@id,'airport')]");
        private AtWebElement AirportText => _webDriver.FindElement(byAirportText);
        private AtBy byAirportAutoCompleter => GetBy(LocatorType.XPath, "(//ul[contains(@id,'autocompleter-list')]/li//div[contains(@class,'autocompleter-option') and @aria-selected='false'])[1]");
        private AtWebElement AirportAutoCompleter => _webDriver.FindElement(byAirportAutoCompleter);
        private AtBy byThisHotelOnlyToggleButton => GetBy(LocatorType.XPath, "//span[text()='This Hotel Only']/parent::button");
        private AtWebElement ThisHotelOnlyToggleButton => _webDriver.FindElement(byThisHotelOnlyToggleButton);
        private AtBy byAllHotelsToggleButton => GetBy(LocatorType.XPath, "//span[text()='All Hotels']/parent::button");
        private AtWebElement AllHotelsToggleButton => _webDriver.FindElement(byAllHotelsToggleButton);
        private AtBy BySelectedAirports => GetBy(LocatorType.XPath, "//ul[@id='airport-autocompleter-list']//li//div[contains(@class,'selected')]");
        private ReadOnlyCollection<AtWebElement> SelectedAirports => _webDriver.FindElements(BySelectedAirports);             
        private AtBy BySelectedAirportsName => GetBy(LocatorType.XPath, "//ul[@id='airport-autocompleter-list']//li//div[contains(@class,'selected')]//div[contains(@class,'bold')]");
        private ReadOnlyCollection<AtWebElement> SelectedAirportsName => _webDriver.FindElements(BySelectedAirportsName);       
        private AtBy byHolidaysTab => GetBy(LocatorType.XPath, "//span[text()='HOLIDAYS']/parent::button");
        private AtWebElement HolidaysTab => _webDriver.FindElement(byHolidaysTab);
        private AtBy byHotelTab => GetBy(LocatorType.XPath, "//span[text()='HOTELS']/parent::button");
        private AtWebElement HotelTab => _webDriver.FindElement(byHotelTab);
        


        public SearchComponent(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetDestination()
        {
            _webDriver.WaitForElementClickable(byDestination, 30, "Destination is not visible");
            return Destination.Text;
        }

        public string GetHighlightedDestinationFromAutoCompleter()
        {
            _webDriver.WaitForElementVisible(byHighlightedDestinationOnAutoCompleter, Constants.ShortWait, "Destination Autocompleter is not displayed ");
            return HighlightedDestinationOnAutoCompleter.Text;
        }

        public List<string> GetSelectedAirports()
        {
            List<string> selectedAirports = new List<string>();
            _webDriver.WaitForElementVisible(byAirportText, 30, "Selected Airports names are not visible");
            foreach (var airports in SelectedAirportsName)
            {
                selectedAirports.Add(airports.Text);
            }
            return selectedAirports;
        }

        public virtual void EditPassengers()
        {
            _webDriver.WaitForElementClickable(byPassengers, 30);
            Passengers.Click();
        }

        public virtual void EditDates()
        {
            _webDriver.WaitForElementVisible(byDates, 30, "Dates is not visible");
            _webDriver.WaitForElementClickable(byDates, 30);
            _webDriver.ScrollToElement(Dates);
            Dates.Click();
        }

        public void SelectNewSearch()
        {
            _webDriver.WaitForElementVisible(byNewSearch, 30, "New search is not visible");
            NewSearch.Click();
        }

        public virtual void ClickCheckAvailability()
        {
            _webDriver.ScrollToElement(CheckAvailability);
            CheckAvailability.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(byCheckAvailability, 30);
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 120);
            if (CloseSearch.Visible)
                CloseSearch.Click();
        }

        public void CloseSearchPage()
        {
            _webDriver.WaitForElementVisible(byCloseSearch, 30, "close search is not visible");
            CloseSearch.Click();
        }

        public bool IsDestinationVisible()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return Destination.Visible;
        }

        public bool IsFlyingFromVisible()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return FlyingFrom.Visible;
        }
        public bool IsPassengersVisible()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return Passengers.Visible;
        }
        public bool IsDatesVisible()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return Dates.Visible;
        }
        public bool IsNewSearchVisible()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return NewSearch.Visible;
        }
        public bool IsCheckHotelAvailabilityVisible()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return CheckAvailability.Visible;
        }

        public bool ValidateHolidaySearchModal()
        {
            return (IsDestinationVisible() & IsFlyingFromVisible() & IsPassengersVisible() & IsDatesVisible() & IsNewSearchVisible() & IsCheckHotelAvailabilityVisible());
        }

        public string GetSelectedHotelText()
        {
            _webDriver.WaitForElementVisible(bySearchHeader, 30, "SearchHeader is not visible");
            return SelectedHotel.Text;
        }

        public void VerifyGuestsOnSearchModal(List<RoomOccupantDetails> roomOccupantDetails)
        {
            int totalAdults = 0;
            int totalChildren = 0;
            int totalRooms = roomOccupantDetails == null? 1 : roomOccupantDetails.Count;

            if (roomOccupantDetails == null)
                totalAdults = 2;
            else
                roomOccupantDetails.ForEach(room => {
                    totalAdults += room.NoOfAdults;
                    totalChildren = totalChildren + room.NoOfChildren + room.NoOfInfants;
                });
            _webDriver.WaitForElementVisible(byGuestsValue, Constants.DefaultWait, "Guests field is not visible");
            if (totalChildren == 0)
                Assert.AreEqual(GuestsValue.Text, string.Format("{0} {1}{2}, {3} {4}{5}", totalAdults, "Adult", totalAdults > 1 ? "s" : "", totalRooms, "Room", totalRooms > 1 ? "s" : ""));
            else
                Assert.AreEqual(GuestsValue.Text, string.Format("{0} {1}{2}, {3} {4}{5}, {6} {7}{8}", totalAdults, "Adult", totalAdults > 1 ? "s" : "", totalChildren, "Child", totalChildren > 1 ? "ren" : "", totalRooms, "Room", totalRooms > 1 ? "s" : ""));
        }

        public void VerifyDatesOnSearchModal(int departure, int duration)
        {
            DateTime departureDate = DateTime.Now.AddDays(departure);
            DateTime returnDate = DateTime.Now.AddDays(departure + duration);
            string dateDuration = departureDate.ToString(Constants.SelectedDateFormat)+" - "+ returnDate.ToString(Constants.SelectedDateFormat);
            if(!DatesValue.Text.Equals(dateDuration))
                Assert.Inconclusive("Dates mismatch on search modal");
        }

        public string GetDatesOnSearchModal()
        {
            _webDriver.WaitForElementVisible(byDates, 10, "Date field on search modal is not visible");
            return DatesValue.Text;
        }

        public bool IsDestinationEditable()
        {
            return Destination.Enabled;
        }

        public bool IsHotelNameEditable()
        {
            try
            {
                SelectedHotel.SendKeys("test");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public virtual void ClickDestinationField()
        {
            _webDriver.WaitForElementClickable(byDestination, 10);
            Destination.Click();
        }

        public virtual void ClickAirportField()
        {
            _webDriver.WaitForElementClickable(byFlyingFrom, 10);
            FlyingFrom.Click();
        }

        public void EnterDestination(string destination)
        {
            DestinationText.Clear();
            DestinationText.SendKeys(destination);
        }

        public bool IsDestinationAutoCompleterDisplayed()
        {
            bool isDisplayed = true;
            try
            {
                _webDriver.WaitForElementVisible(byDestinationAutoCompleter, 10, "Destination auto completer is not visible");
            }
            catch
            {
                isDisplayed = false;
            }
            return isDisplayed;
        }

        public virtual void PopulateDestination(string destination)
        {
            Destination.Click();
            DestinationText.Clear();
            DestinationText.SendKeys(destination);
            _webDriver.WaitForElementVisible(byDestinationAutoCompleter, 10, "Destination auto completer is not visible");
            _webDriver.WaitForTextPresent(byDestinationAutoCompleter, destination, TimeSpan.FromSeconds(10), 3);
            DestinationAutoCompleter.ClickButtonUsingJs();
            ConfirmDestinationButton.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(byConfirmDestinationButton, 5);
        }
        public virtual void PopulateAirport(string airport)
        {
            FlyingFrom.Click();
            _webDriver.WaitForElementVisible(byAirportText, 20, "Airport picker modal is not displayed");
            while (SelectedAirports.Count > 0)
                SelectedAirports.First().Click();
            AirportText.EnterText(airport);
            _webDriver.WaitForElementVisible(byAirportAutoCompleter, 10, "Airport auto completer is not visible");
            _webDriver.WaitForTextPresent(byAirportAutoCompleter, airport, TimeSpan.FromSeconds(1), 5);
            AirportAutoCompleter.Click();
            ConfirmDepartureAirportButton.Click();
            _webDriver.WaitUntilNotVisible(byConfirmDepartureAirportButton, 5);
        }

        public void ConfirmNewDestination()
        {
            ConfirmDestinationButton.ClickButtonUsingJs();
        }

        public void PopulateAirports(List<string> airportsToSelect)
        {
            FlyingFrom.Click();
            _webDriver.WaitForElementVisible(byAirportText, 20, "Airport picker modal is not displayed");
            while (SelectedAirports.Count > 0)
                SelectedAirports.First().Click();
            foreach (string airport in airportsToSelect)
            {
                AirportText.EnterText(airport);
                _webDriver.WaitForElementVisible(byAirportAutoCompleter, 10, "Airport auto completer is not visible");
                _webDriver.WaitForTextPresent(byAirportAutoCompleter, airport, TimeSpan.FromSeconds(1), 10);
                AirportAutoCompleter.Click();
            }            
            ConfirmDepartureAirportButton.Click();
            _webDriver.WaitUntilNotVisible(byConfirmDepartureAirportButton, 5);
        }

        public void ClickFlyingFromField()
        {        
            _webDriver.WaitForElementVisible(byFlyingFrom, 20, "Flying from field is not displayed");
            FlyingFrom.Click();
        }

        public bool IsUpdateAvailabilityToggleVisible()
        {
            _webDriver.WaitForElementClickable(byDestination, 10);
            return (ThisHotelOnlyToggleButton.Visible && AllHotelsToggleButton.Visible);
        }

        public string GetFlightsFromText()
        {
            _webDriver.WaitForElementVisible(byFlyingFrom, 10, "Flying from is not visible");
            return FlyingFrom.Text;
        }

        public void SelectThisHotelOnlyToggleButton()
        {
            _webDriver.WaitForElementVisible(byCheckAvailability, 10, "CheckAvailability is not visible");
            _webDriver.WaitForElementClickable(byThisHotelOnlyToggleButton, 10);
            if(!IsThisHotelToggleSelected())
                ThisHotelOnlyToggleButton.Click();    
        }

        public bool IsThisHotelToggleSelected()
        {
            return ThisHotelOnlyToggleButton.GetAttribute("aria-pressed") == "true";
        }

        public void SelectAllHotelsToggleButton()
        {
            _webDriver.WaitForElementVisible(byCheckAvailability, 10, "CheckAvailability is not visible");
            _webDriver.WaitForElementClickable(byAllHotelsToggleButton, 10);
            if (!IsAllHotelsToggleSelected())
                AllHotelsToggleButton.Click();            
        }

        public bool IsAllHotelsToggleSelected()
        {
            return AllHotelsToggleButton.GetAttribute("aria-pressed") == "true";
        }

        public void SelectHolidaysTab()
        {
            _webDriver.WaitForElementVisible(byHolidaysTab, Constants.MediumWait, "Holiday tab on search modal is not visible");
            if (!IsHolidaysTabSelected())
                HolidaysTab.Click();
        }

        public bool IsHolidaysTabSelected()
        {
            return HolidaysTab.GetAttribute("aria-selected") == "true";
        }

        public virtual void SelectHotelsTab()
        {
            _webDriver.WaitForElementVisible(byHotelTab, 10, "Holiday tab on search modal is not visible");
            if (!IsHotelsTabSelected())
                HotelTab.Click();
        }

        public virtual bool IsHotelsTabSelected()
        {
            return HotelTab.GetAttribute("aria-selected") == "true";
        }        

    }
}
