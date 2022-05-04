using Dnata.Automation.BDDFramework.Configuration;
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
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class HomePage :MobileBasePage, IHomePage
    {
        
        private AtWebElement HotelSearchResults => _webDriver.FindElement(LocatorType.CssSelector, "li:nth-child(13) > a");
        private AtWebElement UserName => _webDriver.FindElement(LocatorType.XPath, "//input[contains(@id, 'sername')]");
        private AtWebElement Password => _webDriver.FindElement(LocatorType.XPath, "//input[@name='password' or @id='txtPassword']");
        private AtWebElement LoginButton => _webDriver.FindElement(LocatorType.XPath, "//button[@type='submit']");
        private AtBy ByUSPList => GetBy(LocatorType.CssSelector, ".sc-c-usp-list");
        private AtWebElement USPList => _webDriver.FindElement(ByUSPList);
        private AtBy ByCloseNewsletter => GetBy(LocatorType.CssSelector, "div.newsletter-modal a.close");
        private AtWebElement CloseNewsLetter => _webDriver.FindElement(ByCloseNewsletter);
        private AtBy byTRLogo => GetBy(LocatorType.CssSelector, "a[class*=logo]");
        private AtWebElement V3TRLogo => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-app-header div.sc-o-flex-grid-item--fill-space a");
        private AtWebElement TRLogo => _webDriver.FindElement(byTRLogo);
        private AtBy byHotelSearchResultsLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'il-spinner')]");
        #region[Mobile Home page search elements]
        private AtWebElement AcceptOldCookies => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Accept and Continue']");
        private AtBy byHotelsTab => GetBy(LocatorType.XPath, "//a[@ng-click=\"cm.productToggle('hotels')\"]");
        private AtWebElement HotelsTab => _webDriver.FindElement(byHotelsTab);
        private AtBy byDestination => GetBy(LocatorType.XPath, "//li[@path='hotels/destination']/span");
        private AtWebElement Destination => _webDriver.FindElement(byDestination);
        private AtBy bySearchDestination => GetBy(LocatorType.XPath, "//input[@placeholder='Search destination']");
        private AtWebElement SearchDestination => _webDriver.FindElement(bySearchDestination);
        private AtBy byHotelAutoComplete => GetBy(LocatorType.XPath, "//ul[@class='field-options']/li[2]");
        private AtWebElement HotelAutoComplete => _webDriver.FindElement(byHotelAutoComplete);
        private AtBy byCheckIn => GetBy(LocatorType.XPath, "//div[@class='search-unit']/ul/li[2]/span");
        private AtWebElement CheckIn => _webDriver.FindElement(byCheckIn);
        private AtWebElement CheckOut => _webDriver.FindElement(LocatorType.XPath, "//div[@class='search-unit']/ul/li[3]/span");
        private AtBy ByDatePickerMonthAndYear => GetBy(LocatorType.XPath, "//div[@class='ui-datepicker-title']");
        private AtWebElement DatePickerMonthAndYear => _webDriver.FindElement(ByDatePickerMonthAndYear);
        private AtWebElement DatePickerPrev => _webDriver.FindElement(LocatorType.XPath, "//a[contains(@class,'ui-datepicker-prev ui-corner-all')]");//span[@class='ui-icon ui-icon-circle-triangle-w']");
        private AtWebElement DatePickerNext => _webDriver.FindElement(LocatorType.XPath, "//a[contains(@class,'ui-datepicker-next')]");//span[@class='ui-icon ui-icon-circle-triangle-e']");
        private AtWebElement DatePickerDay(string day) => _webDriver.FindElement(LocatorType.XPath, "//a[.='#']", day);
        private AtBy ByCheckOutDatePickerMonthAndYear => GetBy(LocatorType.XPath, "//div[@class='ui-datepicker-title']");
        private AtWebElement CheckOutDatePickerMonthAndYear => _webDriver.FindElement(ByCheckOutDatePickerMonthAndYear);
        private AtWebElement CheckOutDatePickerNext => _webDriver.FindElement(LocatorType.XPath, "//a[@data-handler='next']");
        private AtWebElement CheckOutDatePickerDay(string day) => _webDriver.FindElement(LocatorType.XPath, "//table[@class='ui-datepicker-calendar']//td[@data-handler='selectDay']//a[.='#']", day);
        private AtWebElement Occupancy => _webDriver.FindElement(LocatorType.XPath, "//li[contains(@path,'/occupancy')]/span");
        private AtWebElement AdultsCount => _webDriver.FindElement(LocatorType.XPath, "//form[@ng-model='cm.currentRoom.Adults']/input");
        private AtBy ByAdultsCountDecrement => GetBy(LocatorType.XPath, "//form[@ng-model='cm.currentRoom.Adults']/button[1]");
        private AtWebElement AdultsCountDecrement => _webDriver.FindElement(ByAdultsCountDecrement);
        private AtWebElement AdultsCountIncrement => _webDriver.FindElement(LocatorType.XPath, "//form[@ng-model='cm.currentRoom.Adults']/button[2]");
        private AtWebElement ChildCount => _webDriver.FindElement(LocatorType.XPath, "//form[@ng-model='cm.currentRoom.numChildren']/input");
        private AtWebElement ChildCountDecrement => _webDriver.FindElement(LocatorType.XPath, "//form[@ng-model='cm.currentRoom.numChildren']/button[1]");
        private AtWebElement ChildCountIncrement => _webDriver.FindElement(LocatorType.XPath, "//form[@ng-model='cm.currentRoom.numChildren']/button[2]");
        private AtWebElement AgeSelector(int number) => _webDriver.FindElement(LocatorType.XPath, "//div[@config='cm.occupancyAgeConfig'][#]/span", number.ToString());
        private AtWebElement AddRoom => _webDriver.FindElement(LocatorType.XPath, "//div/a[@ng-click='cm.addRoom()']");
        private AtWebElement Apply => _webDriver.FindElement(LocatorType.XPath, "//button[@ng-click='cm.applyOccupancy()']");
        private AtBy bySearch => GetBy(LocatorType.XPath, "//input[@ng-click='cm.search()']");
        private AtWebElement Search => _webDriver.FindElement(bySearch);
        //Holiday Search unit:
        private AtBy byHolidaysTab => GetBy(LocatorType.XPath, "//a[@ng-click=\"cm.productToggle('holidays')\"]");
        private AtWebElement HolidaysTab => _webDriver.FindElement(byHolidaysTab);
        private AtBy byHoldiayDestination => GetBy(LocatorType.XPath, "//li[@path='holidays/destination']/span");
        private AtWebElement HoldiayDestination => _webDriver.FindElement(byHoldiayDestination);
        private AtBy byDepartureAirport => GetBy(LocatorType.XPath, "//li[@path='holidays/departureairport']/span");
        private AtBy bySearchAirport => GetBy(LocatorType.XPath, "//input[@placeholder='Search airports']");
        private AtWebElement SearchAirport => _webDriver.FindElement(bySearchAirport);
        private AtWebElement DepartureAirport => _webDriver.FindElement(byDepartureAirport);
        private AtBy byAirportAutoComplete => GetBy(LocatorType.XPath, "//ul[@class='field-options']/li[2]");
        private AtWebElement AirportAutoComplete => _webDriver.FindElement(byAirportAutoComplete);
        private AtBy byDepartureDate => GetBy(LocatorType.XPath, "//div[@class='search-unit']/ul/li[3]/span");
        private AtWebElement DepartureDate => _webDriver.FindElement(byDepartureDate);
        private AtBy byReturnDate => GetBy(LocatorType.XPath, "//div[@class='search-unit']/ul/li[4]/span");
        private AtWebElement ReturnDate => _webDriver.FindElement(byReturnDate);
        #endregion

        #region[Desktop home page elements]
        private AtBy ByProductTab(string productText) => GetBy(LocatorType.XPath, "//a[@ng-click='is.selectTab(tab)' and contains(.,'#')]", productText);
        private AtWebElement ProductTab(string productText) => _webDriver.FindElement(ByProductTab(productText));
        private AtBy ByDestinationInput => GetBy(LocatorType.CssSelector, "input[placeholder *='Enter a destination']");
        private AtWebElement DestinationInput => _webDriver.FindElement(ByDestinationInput);
        private AtBy ByDestinationDropdown => GetBy(LocatorType.XPath, "//div[@select='us.selectDestination()' or  @class='autocompleter']//ul/li[@class='field-options-item'][1]");
        private AtWebElement DestinationDropdown => _webDriver.FindElement(ByDestinationDropdown);
        private AtWebElement WhenInput => _webDriver.FindElement(LocatorType.CssSelector, "div[icon-name='calendar'] input");
        private AtWebElement NextButton => _webDriver.FindElement(LocatorType.CssSelector, "a[class*='ui-datepicker-next ui-corner-all']");
        private AtWebElement PreviousButton => _webDriver.FindElement(LocatorType.CssSelector, "a[class*='ui-datepicker-prev']");
        private AtWebElement DateHeader => _webDriver.FindElement(LocatorType.CssSelector, "div[class *='corner-left'] div[class *='ui-datepicker-title']");
        private ReadOnlyCollection<AtWebElement> AvailableDates => _webDriver.FindElements(LocatorType.CssSelector, "div[class *= 'ui-datepicker-multi-2'] td[data-handler = 'selectDay'] a");
        private AtWebElement SearchButton => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@class,'sc-c-button sc-c-button--accent search-button')] | //button[@ng-click='search()']");
        private AtBy byPromoTerms => GetBy(LocatorType.CssSelector, "a[href*='promotionterms']");
        private AtWebElement PromoTerms => _webDriver.FindElement(byPromoTerms);
        private ReadOnlyCollection<AtWebElement> PromoTermsText => _webDriver.FindElements(LocatorType.ClassName, "u-text--preline");
        private AtBy ByCalltobookContainer => GetBy(LocatorType.ClassName, "call-to-book-close-btn");
        private AtWebElement CalltobookContainer => _webDriver.FindElement(ByCalltobookContainer);
        private AtWebElement NewDateToSelect(string date)
        {
            _webDriver.WaitForElementVisible(AvailableDates[0]);
            return AvailableDates.Where(d => d.Text.Contains(date)).First();
        }
        private AtWebElement WhoIsGoing => _webDriver.FindElement(LocatorType.CssSelector, "div[icon-name='occupancy'] input");
        private ReadOnlyCollection<AtWebElement> RemoveRoom => _webDriver.FindElements(LocatorType.CssSelector, "div.room-row a.remove-row");
        private AtWebElement AddRoomButton => _webDriver.FindElement(LocatorType.CssSelector, "a[class = 'round-o-icon-label button-add-room']");
        private ReadOnlyCollection<AtWebElement> Adults => _webDriver.FindElements(LocatorType.CssSelector, "select[ng-model = 'room.Adults']");
        private ReadOnlyCollection<AtWebElement> Children => _webDriver.FindElements(LocatorType.CssSelector, "select[ng-model *= 'room.ChildAges.length']");
        private AtBy ByChildAges => GetBy(LocatorType.XPath, "//descendant::select[@ng-model='room.ChildAges[$index]']");
        private ReadOnlyCollection<AtWebElement> ChildAges => _webDriver.FindElements(ByChildAges);
        private AtWebElement OkButton => _webDriver.FindElement(LocatorType.CssSelector, "button[class *= 'submit']");
        private AtBy ByLazyLoadCompleteBar => GetBy(LocatorType.CssSelector, "div.sc-c-horizontal-loader.sc-c-horizontal-loader--fade-out");
        private AtBy ByEstabName => GetBy(LocatorType.CssSelector, "div.hotel-summary .hotel-name span");
        private AtWebElement EstabName => _webDriver.FindElement(ByEstabName);
        private AtBy ByHotelResultItems => GetBy(LocatorType.CssSelector, "div.main-panel div.sc-o-row > div:not(.ng-hide)");
        private AtWebElement FlyingFromMoreField => _webDriver.FindElement(LocatorType.XPath, "//span[@class='more-text']");
        private AtWebElement FlyingFromItem(int index) => _webDriver.FindElement(LocatorType.XPath, "//li[#]/a[@ng-click='ac.removeItem(item)']", index.ToString());
        private AtBy ByFlyingFromAirport => GetBy(LocatorType.CssSelector, "input[placeholder='Enter an airport']");
        private AtWebElement FlyingFromAirport => _webDriver.FindElement(ByFlyingFromAirport);
        private AtBy ByFlyingFromAutoComplete => GetBy(LocatorType.XPath, "//div[@class='autocompleter-multi-select-inner']//ul[@class='field-options']/li[contains(@class,'field-options-item')][1]");
        private AtWebElement FlyingFromAutoComplete => _webDriver.FindElement(ByFlyingFromAutoComplete);
        private AtBy byCloseHolidayEssentials => GetBy(LocatorType.CssSelector, "button[aria-label='Close Holiday Essentials menu']");
        private AtWebElement CloseHolidayEssentials => _webDriver.FindElement(byCloseHolidayEssentials);
        #endregion

        private readonly IAtWebDriver _webDriver;
        private string DestinationLocation { get; set; }
        private string DepartureAirportLocation { get; set; }
        private readonly ICalendarComponent _calendarComponent;
        private readonly IGuestComponent _guestComponent;
        private bool isNewsletterHandled = false;
        private bool isHolidayFlow = false;
        private bool IsHolidayEssentialsHandled = false;
        private readonly ILandingPageSearchComponent _landingPageSearchComponent;
        private readonly ILandingPageCalendarComponent _landingPageCalendarComponent;
        private readonly ScenarioContext context;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;

        public HomePage(ScenarioContext injectedContext, ICalendarComponent calendarComponent, IGuestComponent guestComponent, IAtWebDriver webDriver, ILandingPageSearchComponent landingPageSearchComponent, ILandingPageCalendarComponent landingPageCalendarComponent, ILandingPageGuestComponent landingPageGuestComponent)
            :base(webDriver)
        {
            context = injectedContext;
            _webDriver = webDriver;
            _calendarComponent = calendarComponent;
            _guestComponent = guestComponent;
            _landingPageSearchComponent = landingPageSearchComponent;
            _landingPageCalendarComponent = landingPageCalendarComponent;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        public bool IsTRLogoVisible()
        {
            if (V3TRLogo.Visible || TRLogo.Visible)
                return true;
            else
                return false;
        }

        public string GetDestination()
        {
            return DestinationLocation;
        }

        public void SetDestination(string destination)
        {
            DestinationLocation = destination;
        }

        public string GetDepartureAirport()
        {
            return DepartureAirportLocation;
        }

        public void SetDepartureAirport(string departureAirport)
        {
            DepartureAirportLocation = departureAirport;
        }

        public void SelectHotelResultsPage()
        {
            _webDriver.WaitForElementVisible(HotelSearchResults, 30);
            _webDriver.ScrollToElement(HotelSearchResults);
            HotelSearchResults.Click();
            System.Threading.Thread.Sleep(5000);
        }

        public void AcceptAndCloseCookies()
        {
            _webDriver.WaitForDomReady(TimeSpan.FromSeconds(2));
            if (AcceptOldCookies.Displayed)
                AcceptOldCookies.ClickButtonUsingJs();
        }


        public void ClickHotelsTab()
        {
            _webDriver.WaitForAnyVisible(ByProductTab(Constants.MenuHotels), ByDestinationInput, 60, "Element is visible");
            if (ProductTab(Constants.MenuHotels).Visible)
            {
                _webDriver.WaitForElementClickable(ByProductTab(Constants.MenuHotels), 30);
                _webDriver.ScrollToElement(ProductTab(Constants.MenuHotels));
                Thread.Sleep(1000);
                ProductTab(Constants.MenuHotels).ClickButtonUsingJs();
            }
        }
        public void PopulateDestination(string destination)
        {
            _webDriver.WaitForElementClickable(ByDestinationInput, 30);
            _webDriver.ScrollToElement(DestinationInput);
            Thread.Sleep(1000);
            DestinationInput.EnterText(destination);
            _webDriver.WaitForElementClickable(ByDestinationDropdown, 30);
            _webDriver.WaitForTextPresent(DestinationDropdown, destination, TimeSpan.FromMilliseconds(1000), 5);
            DestinationDropdown.Click();
        }
        public void SelectDateFromCalendar(int checkInDate, int duration = 0)
        {
            SelectDate(checkInDate);
            if (duration > 0)
                SelectDate(checkInDate + duration);
            _webDriver.WaitUntilNotVisible(NextButton, 30);
        }
        private void SelectDate(int daysFromCurrentdate)
        {
            _webDriver.WaitForElementVisible(NextButton);

            bool prevIndex = false;

            var dateToSelect = DateTime.Now.AddDays(daysFromCurrentdate);
            string year = Convert.ToString(dateToSelect.Year);
            string day = Convert.ToString(dateToSelect.Day);
            string dateAsString = dateToSelect.ToString("dd-MMMM-yyyy");
            string month = dateAsString.Split('-')[1];

            _webDriver.WaitForElementVisible(NextButton);

            // Click Previous until its visible so we are set to the current month
            while (!PreviousButton.GetAttribute("class").Contains("disabled"))
                PreviousButton.Click();

            var dateHeaderText = DateHeader.GetText();
            var calendarYear = dateHeaderText.Split(' ')[1];

            while (!DateHeader.GetText().Contains(year))
            {
                if (Convert.ToInt32(calendarYear) < Convert.ToInt32(year))
                    NextButton.Click();
                else
                {
                    PreviousButton.Click();
                    prevIndex = true;
                }
            }

            while (!(DateHeader.Text.Contains(month)))
            {
                if (!prevIndex)
                    NextButton.Click();
                else
                    PreviousButton.Click();
            }
            NewDateToSelect(day).Click();
        }
        public void SetRoomsOccupancy(List<RoomOccupantDetails> lRoomsOccupancy)
        {
            while (RemoveRoom.Count > 0)
                RemoveRoom[1].Click();
            for (int room = 0; room < lRoomsOccupancy.Count - 1; room++)
            {
                AddRoomButton.Click();
            }
            SetAdults(lRoomsOccupancy);
            SetChildren(lRoomsOccupancy);
            SetChildAges(lRoomsOccupancy);
        }
        public void SetAdults(List<RoomOccupantDetails> rooms)
        {
            for (int counter = 0; counter < Adults.Count; counter++)
            {
                Adults[counter].SelectDropdownOptionByValue(rooms[counter].NoOfAdults.ToString());
            }
        }

        public void SetChildren(List<RoomOccupantDetails> rooms)
        {
            for (int counter = 0; counter < Children.Count; counter++)
            {
                Children[counter].SelectDropdownOptionByValue((rooms[counter].NoOfChildren + rooms[counter].NoOfInfants).ToString());
            }
        }
        public void SetChildAges(List<RoomOccupantDetails> rooms)
        {
            int counter = 0;
            for (int room = 0; room < rooms.Count; room++)
            {
                int childCount = 0;
                while (childCount < rooms[room].NoOfChildren)
                {
                    _webDriver.WaitForElementClickable(ByChildAges, 30);
                    ChildAges[counter].SendKeys(Constants.ChildAge.ToString());
                    counter++;
                    childCount++;
                }
                int infantCount = 0;
                while (infantCount < rooms[room].NoOfInfants)
                {
                    _webDriver.WaitForElementClickable(ByChildAges, 30);
                    ChildAges[counter].SendKeys(Constants.InfantAge.ToString());
                    counter++;
                    infantCount++;
                }
            }
        }

        public void SearchHotels(string destination, int departure, int noOfDays, string guests)
        {
            if (HelperFunctions.IsV3HomepageEnabled())
                SearchHotelsV3(destination, departure, noOfDays, guests);
            else
                SearchHotelsV2(destination, departure, noOfDays, guests);

        }
        public void SearchHotelsV2(string destination, int departure, int noOfDays, string guests)
        {
            try
            {
                if (HelperFunctions.IsDesktop())
                { 
                        if (!isNewsletterHandled)
                            CloseNewsletter();
                        ClickHotelsTab();
                        DestinationLocation = destination;
                        PopulateDestination(destination);
                        _calendarComponent.SetDepartureDate(DateTime.Now.AddDays(departure));
                        _calendarComponent.SetReturnDate(DateTime.Now.AddDays(departure + noOfDays));
                        WhenInput.Click();
                        SelectDateFromCalendar(departure, noOfDays);
                        _guestComponent.SetRoomsData(guests);
                        WhoIsGoing.Click();
                        SetRoomsOccupancy(_guestComponent.GetRoomOccupantDetails());
                        OkButton.Click();
                        _webDriver.WaitUntilNotVisible(OkButton, 30, "OK button is still visible");
                        SearchButton.Click();
                }
                else
                {
                    DestinationLocation = destination;
                    _webDriver.WaitForElementClickable(byHotelsTab, 60);
                    HotelsTab.Click();
                    _webDriver.WaitForElementClickable(byDestination, 10);
                    Destination.ClickButtonUsingJs();
                    _webDriver.WaitForElementClickable(bySearchDestination, 10);
                    SearchDestination.EnterText(destination);
                    _webDriver.WaitForElementClickable(byHotelAutoComplete, 10);
                    _webDriver.WaitForTextPresent(HotelAutoComplete, destination, TimeSpan.FromMilliseconds(1000), 3);
                    HotelAutoComplete.ClickButtonUsingJs();
                    _webDriver.WaitForElementClickable(byCheckIn, 10);
                    CheckIn.Click();
                    SelectDate(DateTime.Now.AddDays(Convert.ToInt32(departure)));
                    _calendarComponent.SetDepartureDate(DateTime.Now.AddDays(Convert.ToInt32(departure)));
                    CheckOut.Click();
                    SelectDate((DateTime.Now.AddDays(Convert.ToInt32(departure + noOfDays))), true);
                    _calendarComponent.SetReturnDate(DateTime.Now.AddDays(Convert.ToInt32(departure + noOfDays)));
                    _webDriver.WaitForElementClickable(bySearch, 10);
                    Occupancy.Click();
                    _guestComponent.SetRoomsData(guests);
                    PopulateRoomsData(_guestComponent.GetRoomOccupantDetails());
                    _webDriver.WaitForElementClickable(bySearch, 10);
                    Search.ClickButtonUsingJs();
                    _webDriver.WaitUntilNotVisible(bySearch, 20);
                }
            }
            catch(Exception e)
            {
                if (!isNewsletterHandled && CloseNewsLetter.Displayed)
                {
                    _webDriver.WaitForElementClickable(ByCloseNewsletter, 10);
                    CloseNewsLetter.ClickButtonUsingJs();
                    _webDriver.WaitUntilNotVisible(CloseNewsLetter, 10);
                    isNewsletterHandled = true;
                    SearchHotels(destination, departure, noOfDays, guests);
                }
                else
                    throw new Exception("Is Newsletter handled already: " + isNewsletterHandled + "!\n" + e.Message);
            }
        }
        public void SearchHotelsV3(string destination, int departure, int noOfDays, string guests)
        {
            //try
            //{
                if (HelperFunctions.IsDesktop())
                {
                    SetDestination(destination);
                    _landingPageSearchComponent.SelectHotelsTab();
                    _landingPageSearchComponent.PopulateDestination(destination);
                    _landingPageSearchComponent.EditDates();
                    _landingPageCalendarComponent.SelectDates(departure, noOfDays);
                    _landingPageCalendarComponent.VerifySelectedDates(departure, noOfDays);
                    _landingPageCalendarComponent.ConfirmTheDates();
                    _landingPageSearchComponent.EditPassengers();
                     List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                    roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                    _landingPageGuestComponent.PopulateGuests(roomDetails);
                    _landingPageGuestComponent.ConfirmNumberOfGuests();
                    context.Add("Search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, noOfDays, _landingPageGuestComponent.GetRoomOccupantDetails()));
                    _landingPageSearchComponent.ClickCheckAvailability();
                }
                else
                {
                    SetDestination(destination);
                    _landingPageSearchComponent.SelectHotelsTab();
                    _landingPageSearchComponent.PopulateDestination(destination);
                   // _landingPageSearchComponent.EditDates();
                    _landingPageCalendarComponent.SelectDates(departure, noOfDays);
                    _landingPageCalendarComponent.VerifySelectedDates(departure, noOfDays);
                    _landingPageCalendarComponent.ConfirmTheDates();
                    _landingPageSearchComponent.EditPassengers();
                    List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                    roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                    _landingPageGuestComponent.PopulateGuests(roomDetails);
                    _landingPageGuestComponent.ConfirmNumberOfGuests();
                    context.Add("Search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, noOfDays, _landingPageGuestComponent.GetRoomOccupantDetails()));
                    _landingPageSearchComponent.ClickCheckAvailability();
                }
            }
            //catch (Exception e)
            //{
            //    if (!isNewsletterHandled && CloseNewsLetter.Displayed)
            //    {
            //        _webDriver.WaitForElementClickable(ByCloseNewsletter, 10);
            //        CloseNewsLetter.ClickButtonUsingJs();
            //        _webDriver.WaitUntilNotVisible(CloseNewsLetter, 10);
            //        isNewsletterHandled = true;
            //        SearchHotels(destination, departure, noOfDays, guests);
            //    }
            //    else
            //        throw new Exception("Is Newsletter handled already: " + isNewsletterHandled + "!\n" + e.Message);
            //}
        //}
        private void WaitForSearchResults()
        {            
            _webDriver.WaitUntilNotVisible(byHotelSearchResultsLoader, Constants.MediumWait, "Hotel Search results is till loading after search");
                                         
        }

        public void ClickHotelPlusFlightTab()
        {
            _webDriver.WaitForElementClickable(ByProductTab(Constants.MenuHotelPlusFlight), 60);
            _webDriver.ScrollToElement(ProductTab(Constants.MenuHotelPlusFlight));
            ProductTab(Constants.MenuHotelPlusFlight).ClickButtonUsingJs();
        }

        public void PopulateFromAirport(string fromAirport)
        {
            try
            {
                DepartureAirportLocation = fromAirport;
                if (FlyingFromMoreField.Visible)
                    FlyingFromMoreField.Click();
                while (FlyingFromItem(1).Visible)
                {
                    FlyingFromItem(1).Click();
                    System.Threading.Thread.Sleep(100);
                }
                _webDriver.WaitForElementClickable(ByFlyingFromAirport, 20);
                FlyingFromAirport.EnterText(fromAirport);
                _webDriver.WaitForElementVisible(ByFlyingFromAutoComplete, 30, "Auto Completer is not visible");
                _webDriver.WaitForElementClickable(ByFlyingFromAutoComplete, 30);
                FlyingFromAutoComplete.Click();
            }
            catch
            {
                if (!IsHolidayEssentialsHandled)
                {
                    if (CloseHolidayEssentials.Visible)
                        CloseHolidayEssentials.ClickButtonUsingJs();
                    System.Threading.Thread.Sleep(100);
                    IsHolidayEssentialsHandled = true;
                    PopulateFromAirport(fromAirport);
                }
            }
        }

        public void SearchHolidays(string destination, string airport, int departure, int noOfDays, string guests)
        {
            if (HelperFunctions.IsV3HomepageEnabled())
                SearchHolidaysV3(destination, airport, departure, noOfDays, guests);
            else
                SearchHolidaysV2(destination, airport, departure, noOfDays, guests);
        }

        public void SearchHolidaysV3(string destination, string airport, int departure, int noOfDays, string guests)
        {
            //try
            //{
                isHolidayFlow = true;
                if (HelperFunctions.IsDesktop())
                {
                    //_webDriver.SetCookie("x-v3", "true");
                    SetDestination(destination);
                    SetDepartureAirport(airport);
                    _landingPageSearchComponent.SelectHolidaysTab();
                    _landingPageSearchComponent.PopulateDestination(destination);
                    _landingPageSearchComponent.PopulateAirport(airport);
                    _landingPageSearchComponent.EditDates();
                    _landingPageCalendarComponent.SelectDates(departure, noOfDays);
                    _landingPageCalendarComponent.VerifySelectedDates(departure, noOfDays);
                    _landingPageCalendarComponent.ConfirmTheDates();
                    _landingPageSearchComponent.EditPassengers();
                    List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                    roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                    _landingPageGuestComponent.PopulateGuests(roomDetails);
                    _landingPageGuestComponent.ConfirmNumberOfGuests();
                    context.Add("Search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, noOfDays, _landingPageGuestComponent.GetRoomOccupantDetails()));
                    _landingPageSearchComponent.ClickCheckAvailability();
                }
                else
                {
                    SetDestination(destination);
                    SetDepartureAirport(airport);
                    _landingPageSearchComponent.SelectHolidaysTab();
                    _landingPageSearchComponent.PopulateDestination(destination);
                    _landingPageSearchComponent.PopulateAirport(airport);
                    _landingPageCalendarComponent.ConfirmTheDates();
                   // _landingPageSearchComponent.EditDates();
                    _landingPageCalendarComponent.SelectDates(departure, noOfDays);
                    _landingPageCalendarComponent.VerifySelectedDates(departure, noOfDays);
                    _landingPageCalendarComponent.ConfirmTheDates();
                    _landingPageSearchComponent.EditPassengers();
                    List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                    roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                    _landingPageGuestComponent.PopulateGuests(roomDetails);
                    _landingPageGuestComponent.ConfirmNumberOfGuests();
                    context.Add("Search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, noOfDays, _landingPageGuestComponent.GetRoomOccupantDetails()));
                    _landingPageSearchComponent.ClickCheckAvailability();
                }
            //}
            //catch (Exception e)
            //{
            //    if (!isNewsletterHandled && CloseNewsLetter.Displayed)
            //    {
            //        _webDriver.WaitForElementClickable(ByCloseNewsletter, 10);
            //        CloseNewsLetter.ClickButtonUsingJs();
            //        _webDriver.WaitUntilNotVisible(CloseNewsLetter, 10);
            //        isNewsletterHandled = true;
            //        SearchHolidays(destination, airport, departure, noOfDays, guests);
            //    }
            //    else
            //        throw new Exception("Is Newsletter handled already: " + isNewsletterHandled + "!\n" + e.Message);
            //}
        }

        public void SearchHolidaysV2(string destination, string airport, int departure, int noOfDays, string guests)
        {
            try
            {
                isHolidayFlow = true;
                if (HelperFunctions.IsDesktop())
                {
                    //_webDriver.SetCookie("x-v3", "true");
                    if (!isNewsletterHandled)
                        CloseNewsletter();
                    _webDriver.WaitForElementVisible(byTRLogo, 30, "TR  Logo is not visible");
                    _webDriver.MoveToElement(TRLogo);
                    TRLogo.ClickUsingActions();                    
                    ClickHotelPlusFlightTab();
                    DestinationLocation = destination;
                    PopulateDestination(destination);
                    DepartureAirportLocation = airport;
                    PopulateFromAirport(airport);
                    _calendarComponent.SetDepartureDate(DateTime.Now.AddDays(departure));
                    _calendarComponent.SetReturnDate(DateTime.Now.AddDays(departure + noOfDays));
                    WhenInput.Click();
                    SelectDateFromCalendar(departure, noOfDays);
                    _guestComponent.SetRoomsData(guests);
                    WhoIsGoing.Click();
                    SetRoomsOccupancy(_guestComponent.GetRoomOccupantDetails());
                    OkButton.Click();
                    _webDriver.WaitUntilNotVisible(OkButton, 30, "OK button is still visible");
                    SearchButton.Click();
                }
                else
                {
                    DestinationLocation = destination;
                    DepartureAirportLocation = airport;
                    _webDriver.WaitForElementClickable(byHolidaysTab, 60);
                    _webDriver.WaitForElementClickable(byHoldiayDestination, 30);
                    HoldiayDestination.ClickButtonUsingJs();
                    _webDriver.WaitForElementClickable(bySearchDestination, 20);
                    SearchDestination.EnterText(destination);
                    _webDriver.WaitForElementClickable(byHotelAutoComplete, 10);
                    _webDriver.WaitForTextPresent(byHotelAutoComplete, destination, TimeSpan.FromSeconds(5), 3);
                    HotelAutoComplete.ClickButtonUsingJs();
                    _webDriver.WaitForElementClickable(byDepartureAirport, 20);
                    DepartureAirport.ClickButtonUsingJs();
                    _webDriver.WaitForElementClickable(bySearchAirport, 20);
                    SearchAirport.EnterText(airport);
                    _webDriver.WaitForElementClickable(byAirportAutoComplete, 10);
                    _webDriver.WaitForTextPresent(byAirportAutoComplete, airport, TimeSpan.FromSeconds(5), 3);
                    AirportAutoComplete.ClickButtonUsingJs();
                    _webDriver.WaitForElementClickable(byDepartureDate, 10);
                    _webDriver.ScrollToElement(DepartureDate);
                    DepartureDate.Click();
                    SelectDate(DateTime.Now.AddDays(Convert.ToInt32(departure)));
                    _calendarComponent.SetDepartureDate(DateTime.Now.AddDays(Convert.ToInt32(departure)));
                    _webDriver.ScrollToElement(ReturnDate);
                    ReturnDate.Click();
                    SelectDate((DateTime.Now.AddDays(Convert.ToInt32(departure + noOfDays))), true);
                    _calendarComponent.SetReturnDate(DateTime.Now.AddDays(Convert.ToInt32(departure + noOfDays)));
                    _webDriver.WaitForElementClickable(bySearch, 10);
                    Occupancy.Click();
                    _guestComponent.SetRoomsData(guests);
                    PopulateRoomsData(_guestComponent.GetRoomOccupantDetails());
                    _webDriver.WaitForElementClickable(bySearch, 10);
                    Search.ClickButtonUsingJs();
                }
            }
            catch(Exception e)
            {
                if (!isNewsletterHandled && CloseNewsLetter.Displayed)
                {
                    _webDriver.WaitForElementClickable(ByCloseNewsletter, 10);
                    CloseNewsLetter.ClickButtonUsingJs();
                    _webDriver.WaitUntilNotVisible(CloseNewsLetter, 10);
                    isNewsletterHandled = true;
                    SearchHolidays(destination, airport, departure, noOfDays, guests);
                }
                else
                    throw new Exception("Is Newsletter handled already: "+isNewsletterHandled+"!\n"+ e.Message);
            }
        }

        private void SelectDate(DateTime dateTobeSelected, bool checkout = false)
        {
            string year = Convert.ToString(dateTobeSelected.Year);
            string day = Convert.ToString(dateTobeSelected.Day);
            string DateAsString = dateTobeSelected.ToString("dd-MMMM-yyyy");
            string[] month = DateAsString.Split('-');

            if (!checkout)
            {
                // Go to next if year does not match
                _webDriver.WaitForElementVisible(ByDatePickerMonthAndYear, 20, "Date Picker Month And Year is not visible");
                while (!DatePickerPrev.GetAttribute("class").Contains("disabled"))
                {
                    DatePickerPrev.Click();
                }                
                while(!DatePickerMonthAndYear.Text.Equals(dateTobeSelected.ToString("MMMM yyyy")))
                {
                    _webDriver.WaitForElementClickable(DatePickerNext, 10);
                    DatePickerNext.Click();
                }
                DatePickerDay(day).Click();
            }
            else
            {
                _webDriver.WaitForElementVisible(ByCheckOutDatePickerMonthAndYear, 20, "Date Picker Month And Year is not visible");
                while (!DatePickerPrev.GetAttribute("class").Contains("disabled"))
                {
                    DatePickerPrev.Click();
                }
                while (!CheckOutDatePickerMonthAndYear.Text.Equals(dateTobeSelected.ToString("MMMM yyyy")))
                {
                    _webDriver.WaitForElementClickable(DatePickerNext, 10);
                    DatePickerNext.Click();
                }
                CheckOutDatePickerDay(day).Click();
            }
        }

        private void PopulateRoomsData(List<RoomOccupantDetails> roomOccupantDetails)
        {
            int child = 0;
            int infant = 0;
            int roomcount = 1;
            foreach (RoomOccupantDetails dRoomDetail in roomOccupantDetails)
            {
                if (roomcount > 1)
                    AddRoom.ClickButtonUsingJs();

                _webDriver.WaitForElementClickable(ByAdultsCountDecrement, 10);
                AdultsCountDecrement.Click();

                while (Convert.ToInt32(AdultsCount.Value) != Convert.ToInt32(dRoomDetail.NoOfAdults))
                {
                    AdultsCountIncrement.Click();
                }

                child = Convert.ToInt32(dRoomDetail.NoOfChildren);
                infant = Convert.ToInt32(dRoomDetail.NoOfInfants);
                if (child + infant != 0)
                {
                    int childCount = 1;
                    for (; childCount <= child; childCount++)
                    {
                        ChildCountIncrement.Click();
                        //AgeSelector(childCount).MoveSlider(40);
                    }
                    for (; childCount <= child + infant; childCount++)
                    {
                        ChildCountIncrement.Click();
                        int counter = 0;
                        while(!AgeSelector(childCount).GetAttribute("style").Contains("left: 0%;") && counter < 10)
                        {
                            AgeSelector(childCount).MoveSlider(-40);
                            counter++;
                        }  
                    }
                }
                roomcount++;
            }
            Apply.ClickButtonUsingJs();
        }

        public void VerifyHomePage()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _landingPageSearchComponent.IsHomepageDisplayed();
            else
               _webDriver.WaitForAnyVisible(byHotelsTab, ByProductTab(Constants.MenuHotels), 10);
            
        }

        public void Login()
        {
            // In case of live and not on VPN or office network , user will take to public mode
            if (UserName.Visible)
            {
                UserName.SendKeys(AtConfiguration.GetConfiguration<string>("AgentUsername"));
                Password.SendKeys(AtConfiguration.GetConfiguration<string>("AgentPassword"));
                LoginButton.Click(); 
            }
        }

        public void CloseNewsletter()
        {
            _webDriver.WaitForElementVisible(ByUSPList, 30, "USP list is not visible");
            _webDriver.ScrollToElement(USPList);
            try
            {
                _webDriver.WaitForElementClickable(ByCloseNewsletter, 5);
                CloseNewsLetter.ClickButtonUsingJs();
            }
            catch { }
            _webDriver.WaitUntilNotVisible(ByCloseNewsletter, 5);
        }

        public void SearchHolidaysForSpecificDates(string destination, string departure, string departureDate, string returnDate, string guests)
        {
            int checkInDate = Convert.ToInt16((DateTime.ParseExact(departureDate, "dd MM yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Today).TotalDays);
            int duration = Convert.ToInt16((DateTime.ParseExact(returnDate, "dd MM yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Today).TotalDays) - checkInDate;
            SearchHolidays(destination, departure, checkInDate, duration, guests);
        }   
        
        public string GetURlOfNewWindow()
        {
            return GetUrlOfNewWindow();
        }

        public string GetCurrentPageURL()
        {
            return GetCurrentURL();
        }
        
        public bool GetIsHolidayFlow()
        {
            return isHolidayFlow;
        }

        public void ClickPromoTermsandConditions()
        {
            _webDriver.WaitForElementVisible(PromoTerms,Constants.LongWait);            
            _webDriver.ScrollToElement(PromoTerms);
            _webDriver.WaitForElementClickable(PromoTerms,30);
             PromoTerms.Click();
        }

        public bool CheckPromoText(string text)
        {
            System.Threading.Thread.Sleep(4000);
            if (CalltobookContainer.Displayed)
            {
                CalltobookContainer.ClickButtonUsingJs();
                _webDriver.WaitUntilNotVisible(CalltobookContainer, 20);
            }            
            foreach (AtWebElement promoText in PromoTermsText)
            {
                if (promoText.Text.Equals(text))
                {
                    _webDriver.ScrollToElement(promoText);
                    return true;
                }                    
            }
            return false;
        }

        public void AddDiscountCodeToUrl()
        {
            _webDriver.GotoUrl(_webDriver.Url+ "?dlc="+ PromoDetails.DiscountCode);
        }

        public string GetDiscountCookieValue()
        {
            return _webDriver.GetCookieValue("DiscountCode");
        }

        public void NavigateToHotelLandingPage(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public void NavigateToHolidaysLandingPage(string url)
        {
            _webDriver.Navigate().GoToUrl(url);

        }

        public void NavigateToLandingPage(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }
    }
}
