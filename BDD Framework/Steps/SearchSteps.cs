using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.Reporting.CustomReporter;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class SearchSteps
    {
        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly ISearchComponent _searchComponent;
        private readonly IGuestComponent _guestComponent;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IMapComponent _mapComponent;
        private readonly IFiltersModal _filtersmodal;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly IAirportComponent _airportComponent;
        private readonly ILandingPageSearchComponent _landingPageSearchComponent;
        private readonly ILandingPageCalendarComponent _landingPageCalendarComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        public IEnumerable<object> childAgefield { get; private set; }

        public SearchSteps(IHomePage homePage, ISearchSummaryComponent searchSummaryComponent, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, IBookingSummary bookingSummary, ISearchComponent searchComponent, ILandingPageSearchComponent searchComponentNew, IGuestComponent guestComponent, ILandingPageGuestComponent landingPageGuestComponent, ICalendarComponent calendarComponent, ILandingPageCalendarComponent landingPageCalendarComponent, IMapComponent mapComponent, IFiltersModal filtersmodal, IFlightSearchResults flightSearchResults, IAirportComponent airportComponent, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _searchSummaryComponent = searchSummaryComponent;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _bookingSummary = bookingSummary;
            _searchComponent = searchComponent;
            _guestComponent = guestComponent;
            _calendarComponent = calendarComponent;
            _mapComponent = mapComponent;
            _filtersmodal = filtersmodal;
            _flightSearchResults = flightSearchResults;
            _airportComponent = airportComponent;
            _landingPageSearchComponent = searchComponentNew;
            _landingPageCalendarComponent = landingPageCalendarComponent;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        [When(@"I click on search icon")]
        [When(@"Click on search icon")]
        public void WhenIClickOnSearchIcon()
        {
            _searchSummaryComponent.EditSearch();
        }

        [When(@"I store search itinerary and click on edit search icon")]
        public void WhenIStoreSearchItineraryAndClickOnEditSearchIcon()
        {
            context.Add("Initial search itinerary", _searchSummaryComponent.GetSearchItinerary().ToLower());
            _searchSummaryComponent.EditSearch();
        }

        [When(@"Click guests field")]
        public void WhenISelectPassengerField()
        {
            _searchComponent.EditPassengers();
        }

        [Then(@"I can add (.*)")]
        [When(@"Populate guests with child age (.*)")]
        public void ThenICanAddTo(string guests)
        {
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                _landingPageGuestComponent.PopulateGuests(roomDetails);
                _guestComponent.VerifyGuests(roomDetails);
            }
            else
            {
                List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                roomDetails = _guestComponent.SetRoomsData(guests);
                _guestComponent.PopulateGuests(roomDetails);
                _guestComponent.VerifyGuests(roomDetails);
            }
        }

        [When(@"Edit guests (.*)")]
        public void WhenEditGuests(string guests)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
            {
                _searchComponent.EditPassengers();
                List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                _landingPageGuestComponent.PopulateGuests(roomDetails);
                _guestComponent.ConfirmNumberOfGuests();
                _guestComponent.VerifyGuests(roomDetails);
            }
            else
            {
                _searchComponent.EditPassengers();
                List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
                roomDetails = _guestComponent.SetRoomsData(guests);
                _guestComponent.PopulateGuests(roomDetails);
                _guestComponent.ConfirmNumberOfGuests();
                _guestComponent.VerifyGuests(roomDetails);
            }
        }

        [When(@"Edit guests (.*) on landing page")]
        public void WhenEditGuestsOnLandingPage(string guests)
        {
            _landingPageSearchComponent.EditPassengers();
            List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
            roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
            _landingPageGuestComponent.PopulateGuests(roomDetails);
            _landingPageGuestComponent.ConfirmNumberOfGuests();
        }


        [Then(@"Guests details should be pre-populated as per the initital search")]
        [Then(@"Guests details should be populated as per the initital search")]
        public void ThenGuestsDetailsShouldBePre_PopulatedAsPerTheInititalSearch()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
            {
                _searchComponent.VerifyGuestsOnSearchModal(_landingPageGuestComponent.GetRoomOccupantDetails());
            }
            else
            {
                _searchComponent.VerifyGuestsOnSearchModal(_guestComponent.GetRoomOccupantDetails());
            }
        }

        [Then(@"Confirm the number of guests")]
        [When(@"Confirm the guests")]
        public void ThenConfirmTheNumberOfGuests()
        {
            _guestComponent.ConfirmNumberOfGuests();
        }

        [When(@"Click dates field")]
        public void WhenISelectDatesField()
        {
            _searchComponent.EditDates();
        }

        [Then(@"I can select (.*) and (.*) date")]
        [When(@"Change (.*) and (.*) dates")]
        public void ThenICanSelectAndDate(int departure, int duration)
        {
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                _landingPageCalendarComponent.SelectDates(departure, duration);
                _calendarComponent.VerifySelectedDates(departure, duration);
                context.Add("Updated search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, duration, _landingPageGuestComponent.GetRoomOccupantDetails()));
            }
            else
            {
                _calendarComponent.SelectDates(departure, duration);
                _calendarComponent.VerifySelectedDates(departure, duration);
                context.Add("Search Itinerary", _calendarComponent.GetItinerary(departure, duration, _guestComponent.GetRoomOccupantDetails()));
            }
        }

        [Then(@"I can select (.*) and (.*) date on landing page")]
        [When(@"Change (.*) and (.*) dates on landing page")]
        public void ThenICanSelectAndDateOnLandingPage(int departure, int duration)
        {
            _landingPageSearchComponent.EditDates();
            _landingPageCalendarComponent.SelectDates(departure, duration);
            _landingPageCalendarComponent.VerifySelectedDates(departure, duration);
            context.Add("Updated search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, duration, _landingPageGuestComponent.GetRoomOccupantDetails()));
            _landingPageCalendarComponent.ConfirmTheDates();
        }

        [When(@"Update (.*) and (.*) dates")]
        public void WhenUpdateAndDates(int departure, int duration)
        {
            _searchComponent.EditDates();
            ThenICanSelectAndDate(departure, duration);
            _calendarComponent.ConfirmTheDates();
        }

        [When(@"Update (.*) and (.*) dates on Landing Page Search")]
        public void WhenUpdateAndDatesOnLandingPage(int departure, int duration)
        {
            ThenICanSelectAndDate(departure, duration);
            _calendarComponent.ConfirmTheDates();
            
        }

        [Then(@"Confirm the dates")]
        [When(@"Confirm dates selection")]
        public void ThenConfirmTheDates()
        {
            _calendarComponent.ConfirmTheDates();
        }

        [Then(@"Search itinerary is populated accordingly for landing Page search")]
        public void ThenCheckSearchItineraryForLandingPageSearch()
        {
            Assert.AreEqual(_searchSummaryComponent.GetSearchItinerary().ToLower(), (_landingPageCalendarComponent.GetItinerary(_landingPageCalendarComponent.GetDepartureDate(), _landingPageCalendarComponent.GetReturnDate(), _landingPageGuestComponent.GetRoomOccupantDetails())).ToLower());
        }

        [Then(@"Search itinerary is populated accordingly")]
        public void ThenCheckSearchItinerary()
        {
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                Assert.AreEqual(_searchSummaryComponent.GetSearchItinerary().ToLower(), (_landingPageCalendarComponent.GetItinerary(_landingPageCalendarComponent.GetDepartureDate(), _landingPageCalendarComponent.GetReturnDate(), _landingPageGuestComponent.GetRoomOccupantDetails())).ToLower());
            }
            else
            {
                Assert.AreEqual(_searchSummaryComponent.GetSearchItinerary().ToLower(), (_calendarComponent.GetItinerary(_calendarComponent.GetDepartureDate(), _calendarComponent.GetReturnDate(), _guestComponent.GetRoomOccupantDetails())).ToLower());
            }
        }

        [Then(@"Search itinerary is populated as per initial data")]
        public void ThenSearchItineraryIsPopulatedAsPerInitialData()
        {
            Assert.AreEqual(_searchSummaryComponent.GetSearchItinerary().ToLower(), context["Initial search itinerary"]);
        }

        [Then(@"Search itinerary is populated accordingly in estab page")]
        public void ThenCheckSearchItineraryInEstabPage()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.AreEqual(_hotelEstabPage.GetSearchItinerary().ToLower(), (_landingPageCalendarComponent.GetItinerary(_landingPageCalendarComponent.GetDepartureDate(), _landingPageCalendarComponent.GetReturnDate(), _landingPageGuestComponent.GetRoomOccupantDetails())).ToLower());
            else
                Assert.AreEqual(_hotelEstabPage.GetSearchItinerary().ToLower(), (_calendarComponent.GetItinerary(_calendarComponent.GetDepartureDate(), _calendarComponent.GetReturnDate(), _guestComponent.GetRoomOccupantDetails())).ToLower());
        }

        [When(@"Search for hotel availability")]
        [When(@"Search for room availability")]
        [When(@"Search for holidays")]
        public void WhenISearchForHotelAvailability()
        {
            _searchComponent.ClickCheckAvailability();
        }


        [When(@"Search for hotel availability on landing page")]
        [When(@"Search for room availability on landing page")]
        [When(@"Search for holidays on landing page")]
        public void WhenISearchForHotelAvailabilityOnLandingPage()
        {
            _landingPageSearchComponent.ClickCheckAvailability();
        }

        [Then(@"CMA compliance (.*) is displayed")]
        public void ThenCMAComplianceIsDisplayed(string CMA)
        {
            Assert.AreEqual(CMA, _hotelSearchResults.GetCMAComplianceText(), "CMA text is not macthing " + CMA);
        }

        [Then(@"Hotel search results count is (.*)")]
        public void ThenSearchResultsCountIs(int resultCount)
        {
            Assert.AreEqual(resultCount, _hotelSearchResults.GetResultsCount(), "Default search results count is not " + resultCount);
        }

        [When(@"I Click on Map view link")]
        public void WhenIClickOnMapViewLink()
        {
            _hotelSearchResults.SelectMapView();
        }

        [Then(@"Map view is displayed")]
        public void ThenMapViewIsDisplayed()
        {
            Assert.AreEqual(_homePage.GetDestination().ToUpper(), _mapComponent.GetMapPageHeader().ToUpper());
        }

        [Then(@"Multiple hotel locations are displayed")]
        public void ThenMultipleHotelLocationsAreDisplayed()
        {
            Assert.IsTrue(_mapComponent.GetLocationPins() > 1, "Locations are not displayed");
            Console.WriteLine(_mapComponent.GetLocationPins() + " locations are displayed");
        }


        [When(@"Store hotel information for the pin point to be selected from the map")]
        public void WhenClickOnAPinPointFromTheMap()
        {
            context.Add("HotelInformationInMaps", _mapComponent.GetHotelInformation());
            _mapComponent.SelectHotelFromMaps();
        }

        [Then(@"Selected hotel information matches in the estab page")]
        public void ThenHotelEstabPageIsDisplayed()
        {
            HotelInformation info = context["HotelInformationInMaps"] as HotelInformation;
            Assert.AreEqual(info.HotelName, _hotelEstabPage.GetHotelName());
            Assert.IsTrue(_hotelEstabPage.GetHotelLocation().Contains(info.Location));
            Assert.LessOrEqual(_hotelEstabPage.GetHotelPrice(), info.Price);
        }

        [Then(@"I can see the local charges information link displayed in hotel tile")]
        [Then(@"local charges information link should be displayed on hotel tile")]
        public void ThenICanSeeTheLocalChargesInformationLinkDisplayedInHotelTile()
        {
            context.Add("LocalTaxInSearchResults", Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(_hotelSearchResults.GetLocalCharges(_hotelSearchResults.GetHotelToSelect()))));
            Console.WriteLine("LocalTaxInSearchResults: " + context["LocalTaxInSearchResults"] as string);
            _hotelSearchResults.CloseLocalTaxPopup();
        }

        [Then(@"Holiday search modal with valid fields should be displayed")]
        public void ThenHolidaySearchModalWithValidFieldsShouldBeDisplayed()
        {
            Assert.IsTrue(_searchComponent.ValidateHolidaySearchModal());
        }

        [When(@"I click on filter icon")]
        public void WhenIClickOnFilterIcon()
        {
            if(!HelperFunctions.IsDesktop())
                _hotelSearchResults.SelectFilters();
        }

        [Then(@"Filter modal should be displayed")]
        public void ThenFilterModalShouldBeDisplayed()
        {
            if (!_filtersmodal.ValidateFiltersModal())
                Assert.Inconclusive("Not all filters are visible");
        }

        [Then(@"Destination should be pre-populated as per initital search details (.*)")]
        [Then(@"Destination on search modal should be updated to (.*)")]
        public void ThenDestinationShouldBePre_PopulatedAsPerInititalSearchDetails(string destination)
        {
            Assert.IsTrue((_searchComponent.GetDestination()).Contains(destination), "Destination not matched");
        }


        [Then(@"Hotel name on search modal should match with the selected hotel")]
        public void ThenHotelNameOnSearchModalShouldMatchWithTheSelectedHotel()
        {
            var info = _hotelSearchResults.GetHotelInformation();
            Assert.AreEqual(info.HotelName, _searchComponent.GetSelectedHotelText(), "Hotel name not macthing on search modal");
        }

        [Then(@"Passenger details should be pre-populated as per initital search details")]
        public void ThenPassengerDetailsShouldBePre_PopulatedAsPerInititalSearchDetails()
        {
            _searchComponent.VerifyGuestsOnSearchModal(_guestComponent.GetRoomOccupantDetails());
        }

        [Then(@"Passenger details should be updated as per selection on search modal")]
        public void ThenPassengerDetailsShouldBeUpdatedAsPerSelectionOnSearchModal()
        {
            _searchComponent.VerifyGuestsOnSearchModal(_guestComponent.GetRoomOccupantDetails());
        }


        [Then(@"Dates should be pre-populated as per initital search details")]
        public void ThenDatesShouldBePre_PopulatedAsPerInititalSearchDetails()
        {
            _searchComponent.VerifyDatesOnSearchModal();
        }

        [Then(@"Destination field should not be editable")]
        public void ThenDestinationFieldShouldNotBeEditable()
        {
            Assert.IsFalse(_searchComponent.IsDestinationEditable(), "Destination fied should not be Editable");
        }

        [Then(@"Destination field should be editable")]
        public void ThenDestinationFieldShouldBeEditable()
        {
            Assert.IsTrue(_searchComponent.IsDestinationEditable(), "Destination fied should be Editable");
        }

        [Then(@"Hotel name should not be editable")]
        public void ThenHotelNameShouldNotBeEditable()
        {
            Assert.IsFalse(_searchComponent.IsHotelNameEditable(), "Hotel name fied should not be Editable");
        }


        [When(@"I update (.*) and change (.*) and (.*) dates")]
        public void WhenIUpdateAndChangeAndDates(string guests, int departure, int duration)
        {
            _searchSummaryComponent.EditSearch();
            _searchComponent.EditPassengers();
            List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                roomDetails = _landingPageGuestComponent.SetRoomsData(guests);
                _landingPageGuestComponent.PopulateGuests(roomDetails);
                _landingPageGuestComponent.VerifyGuests(roomDetails);
                _guestComponent.ConfirmNumberOfGuests();
                _searchComponent.EditDates();
                _landingPageCalendarComponent.SelectDates(departure, duration);
                _calendarComponent.VerifySelectedDates(departure, duration);
                context.Add("Updated search Itinerary", _landingPageCalendarComponent.GetItinerary(departure, duration, _landingPageGuestComponent.GetRoomOccupantDetails()));
            }
            else
            {
                roomDetails = _guestComponent.SetRoomsData(guests);
                _guestComponent.PopulateGuests(roomDetails);
                _guestComponent.VerifyGuests(roomDetails);
                _guestComponent.ConfirmNumberOfGuests();
                _searchComponent.EditDates();
                _calendarComponent.SelectDates(departure, duration);
                _calendarComponent.VerifySelectedDates(departure, duration);
                context.Add("Search Itinerary", _calendarComponent.GetItinerary(departure, duration, _guestComponent.GetRoomOccupantDetails()));
            }
            _calendarComponent.ConfirmTheDates();
            _searchComponent.ClickCheckAvailability();
        }

        [Then(@"Datepicker modal should be displayed with previous selected (.*) and (.*) dates")]
        public void ThenDatepickerModalShouldBeDisplayedWithPreviousAndSelected(int departure, int duration)
        {
            _searchComponent.EditDates();
            _calendarComponent.VerifySelectedDates(departure, duration);
        }

        [Then(@"Date details should be pre-populated as per the initial (.*) and (.*) date")]
        public void ThenDateDetailsShouldBePre_PopulatedAsPerTheInitialAndDate(int departure, int duration)
        {
            _calendarComponent.VerifySelectedDates(departure, duration);
        }

        [When(@"Populate guests without child age (.*)")]
        public void WhenPopulateGuestsWithoutChildAge(string guests)
        {
            List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
            roomDetails = _guestComponent.SetRoomsData(guests);
            _guestComponent.PopulateGuestsWithoutChildAge(roomDetails);
            //context.Add("RoomDetails", roomDetails);
            //context.Add("NoOfRooms", roomDetails.Count);
        }

        [Then(@"Verify error message prompting to enter child age is displayed for each room")]
        public void ThenVerifyErrorMessagePromptingToEnterChildIsDisplayed()
        {
            Assert.IsTrue(_guestComponent.ValidateChildErrorMessage(_guestComponent.GetRoomOccupantDetails().Count), "Error message prompting to enter child is not displayed");
            _guestComponent.ClearAddedGuests();
        }

        [When(@"Click on number of rooms as '(.*)'")]
        public void WhenClickOnNumberOfRoomsAs(int roomCount)
        {
            _guestComponent.SelectRooms(roomCount);
        }

        [Then(@"Guest modal should display (.*) rooms details")]
        public void ThenGuestModalShouldDisplayRoomsDetails(int roomCount)
        {
            Assert.IsTrue(_guestComponent.CheckRoomDetails(roomCount), "Occupancy modal is not displayed for" + roomCount + " rooms");
        }

        [Then(@"Verify maximum of (.*) adults can be selected")]
        public void ThenVerifyMaximumOfAdultsCanBeSelected(int maxAdults)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.IsTrue(_guestComponent.CheckAdultsIncrementButton(_landingPageGuestComponent.GetRoomOccupantDetails(), maxAdults));
            else
                Assert.IsTrue(_guestComponent.CheckAdultsIncrementButton(_guestComponent.GetRoomOccupantDetails(), maxAdults));
        }

        [Then(@"Veriy minimum of (.*) adults should be selected")]
        public void ThenVeriyMinimumOfAdultsShouldBeSelected(int minAdults)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.IsTrue(_guestComponent.CheckAdultsDecrementButton(_landingPageGuestComponent.GetRoomOccupantDetails(), minAdults));
            else
                Assert.IsTrue(_guestComponent.CheckAdultsDecrementButton(_guestComponent.GetRoomOccupantDetails(), minAdults));
        }

        [Then(@"Verify maximum of (.*) children can be selected")]
        public void ThenVerifyMaximumOfChildrenCanBeSelected(int maxChildren)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.IsTrue(_landingPageGuestComponent.CheckChildrenIncrementButton(_landingPageGuestComponent.GetRoomOccupantDetails(), maxChildren));
            else
                Assert.IsTrue(_guestComponent.CheckChildrenIncrementButton(_guestComponent.GetRoomOccupantDetails(), maxChildren));
        }

        [Then(@"Veriy minimum of (.*) children can be selected")]
        public void ThenVeriyMinimumOfChildrenCanBeSelected(int minChildren)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.IsTrue(_landingPageGuestComponent.CheckChildrenIncrementButton(_landingPageGuestComponent.GetRoomOccupantDetails(), minChildren));
            else
                Assert.IsTrue(_guestComponent.CheckChildrenIncrementButton(_guestComponent.GetRoomOccupantDetails(), minChildren));
        }

        [When(@"Click on reset dates")]
        public void WhenClcikOnResetDates()
        {
            _calendarComponent.ClickResetLink();
        }

        [Then(@"Selected dates should be reset")]
        public void ThenSelectedDatesShouldBeReset()
        {
            Assert.IsTrue(_calendarComponent.IsSelectedDatesNull(), "Selected dates is not reset");
        }

        [When(@"Click on close button")]
        public void WhenClickOnCloseButton()
        {
            _calendarComponent.CloseDatePickerModal();
        }

        [Then(@"Date picker modal should be closed")]
        public void ThenDatePickerModalShouldBeClosed()
        {
            Assert.IsTrue(_searchComponent.IsDatesVisible(), "Date picker is not closed");
        }

        [Then(@"Search modal with updated (.*) and (.*) should be displayed")]
        [Then(@"Date on search modal should be populated as per the initial (.*) and (.*) date")]
        public void ThenSearchModalShouldBePopulatedAsPerTheInitialSearch(int departure, int duration)
        {
            _searchComponent.VerifyDatesOnSearchModal(departure, duration);
        }

        [Then(@"(.*) message should be displayed")]
        public void ThenPleaseSelectADepartureDateMessageShouldBeDisplayed(string errorMessage)
        {
            Assert.AreEqual(_calendarComponent.GetDateErrorMessage(), errorMessage);
        }

        [When(@"Select (.*) date")]
        public void WhenSelectDate(int departure)
        {
            _calendarComponent.SelectDepartureDate(departure);
        }

        [When(@"Edit destination to (.*)")]
        public void WhenEditDestinationOnSearchModalTo(string newDestination)
        {
            _homePage.SetDestination(newDestination);
            _searchComponent.PopulateDestination(newDestination);
        }

        [When(@"Edit destination to (.*) on Landing Page")]
        public void WhenEditDestinationOnSearchModalInLandingPage(string newDestination)
        {
            _homePage.SetDestination(newDestination);
            _landingPageSearchComponent.PopulateDestination(newDestination);
        }

        [Then(@"Search location should match with (.*)")]
        public void ThenSearchLocationShouldMatchWithDubai(string newDestination)
        {
            Assert.IsTrue(_searchSummaryComponent.GetSearchLocation().Equals(newDestination, StringComparison.OrdinalIgnoreCase), "Search location on itinerary is not matching with the searched location");
        }

        [When(@"Update departure airport on search modal to (.*)")]
        public void WhenUpdateDepartureAirportOnSearchModalTo(string departureAirport)
        {
            _homePage.SetDepartureAirport(departureAirport);
            _searchComponent.PopulateAirport(departureAirport);
        }

        [When(@"Update departure airport on landing page search modal to (.*)")]
        public void WhenUpdateDepartureAirportOnLandingPageSearchModalTo(string departureAirport)
        {
            _homePage.SetDepartureAirport(departureAirport);
            _landingPageSearchComponent.PopulateAirport(departureAirport);
        }

        [When(@"I add departure airport on search modal to (.*)")]
        public void WhenIAddDepartureAirportOnSearchModalToLondonGatwick(string departureAirport)
        {
            string[] airportsToAdd = departureAirport.Split("-");
            context.Add("SelectedDepartureAirports", airportsToAdd.ToList() as List<string>);
            _searchComponent.PopulateAirports(airportsToAdd.ToList());
        }

        [Then(@"Flying from field should be updated with selected airports")]
        public void ThenFlyingFromFieldShouldBeUpdatedWithSelectedAirports()
        {
            List<string> airports = context["SelectedDepartureAirports"] as List<string>;
            Assert.AreEqual(string.Format("{0} (+ {1} other{2})", airports.First(), airports.Count - 1, airports.Count - 1 > 1 ? "s" : ""),
                _searchComponent.GetFlightsFromText(), "Selected airports field validation");
        }

        [Then(@"Update availability toggle should not be visible")]
        public void ThenUpdateAvailabilityToggleShouldNotBeVisible()
        {
            Assert.IsFalse(_searchComponent.IsUpdateAvailabilityToggleVisible(), "Toggle is displayed on search modal");
        }

        [Then(@"Update availability toggle should be visible")]
        public void ThenUpdateAvailabilityToggleShouldBeVisible()
        {
            Assert.IsTrue(_searchComponent.IsUpdateAvailabilityToggleVisible(), "Toggle is not displayed on search modal");
        }

        [When(@"Update departure (.*) and destination (.*) in search modal")]
        public void WhenUpdateDepartureAndDestinationInSearchModal(string depAirport, string desAirport)
        {
            _searchSummaryComponent.EditSearch();
            _homePage.SetDestination(desAirport);
            WhenEditDestinationOnSearchModalTo(desAirport);
            _homePage.SetDepartureAirport(depAirport);
            WhenUpdateDepartureAirportOnSearchModalTo(depAirport);
        }

        [Then(@"Hotel search results page is displayed for searched (.*)")]
        public void ThenHotelSearchResultsPageIsDisplayedForSearched(string destination)
        {
            Assert.AreEqual(destination, _searchSummaryComponent.GetSearchLocation(), "Destination location validation");
            Assert.IsTrue(_hotelSearchResults.IsHotelSearchResultsPageLoaded(), "Confirming hotel search results page");
        }

        [Then(@"Departure (.*) should match in flight search results")]
        public void ThenDepartureShouldMatchInFlightSearchResults(string airport)
        {
            _hotelSearchResults.SelectHotel(_hotelSearchResults.RandomizeHotelToSelect());
            ThenDepartureShouldMatchInFlightSearchResultsFromEstabPage(airport);
        }

        [Then(@"Departure (.*) should match in flight search results from estab page")]
        public void ThenDepartureShouldMatchInFlightSearchResultsFromEstabPage(string airport)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.CapturePreSelectedFlightInformation();
            Assert.AreEqual(airport, _flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Outbound].DepartureLocation, "Departure airport validation");
            Assert.AreEqual(airport, _flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Inbound].ArrivalLocation, "Departure airport validation");
        }

        [Then(@"Hotel estab page of selected hotel is displayed")]
        public void ThenHotelEstabPageOfSelectedHotelIsDisplayed()
        {
            Assert.AreEqual(_hotelSearchResults.GetHotelInformation().HotelName, _hotelEstabPage.GetHotelName(), "Hotel name validation");
        }

        [When(@"I select all hotels from availability toggle")]
        [When(@"I select All Hotels toggle")]
        public void WhenISelectAllHotelsFromAvailabilityToggle()
        {
            _searchComponent.SelectAllHotelsToggleButton();
            Assert.IsTrue(_searchComponent.IsAllHotelsToggleSelected(), "All hotels toggle is not selected");
        }

        [When(@"I select update this hotel only from availability toggle")]
        [When(@"I select This Hotel Only toggle")]
        public void WhenISelectUpdateThisHotelOnlyFromAvailabilityToggle()
        {
            _searchComponent.SelectThisHotelOnlyToggleButton();
            Assert.IsTrue(_searchComponent.IsThisHotelToggleSelected(), "This hotel toggle is not selected");
        }

        [When(@"Update departure (.*) destination (.*) guests (.*) during (.*) and (.*) in search modal")]
        public void WhenUpdateDepartureDestinationGuestsDuringAndInSearchModal(string newDepartureAirport, string newDestination, string newGuestsData, int newDepartureDate, int newReturnDate)
        {
            WhenUpdateDepartureAndDestinationInSearchModal(newDepartureAirport, newDestination);
            _searchComponent.EditDates();
            ThenICanSelectAndDate(newDepartureDate, newReturnDate);
            ThenConfirmTheDates();
            _searchComponent.EditPassengers();
            ThenICanAddTo(newGuestsData);
            _guestComponent.ConfirmNumberOfGuests();
            _searchComponent.ClickCheckAvailability();
        }

        [When(@"Update departure (.*) guests (.*) during (.*) and (.*) in estab page search modal")]
        public void WhenUpdateDepartureGuestsDuringAndInSearchModal(string newDepartureAirport, string newGuestsData, int newDepartureDate, int newReturnDate)
        {
            _searchSummaryComponent.EditSearch();
            WhenUpdateDepartureAirportOnSearchModalTo(newDepartureAirport);
            _searchComponent.EditDates();
            ThenICanSelectAndDate(newDepartureDate, newReturnDate);
            ThenConfirmTheDates();
            _searchComponent.EditPassengers();
            ThenICanAddTo(newGuestsData);
            _guestComponent.ConfirmNumberOfGuests();
        }

       
        [When(@"I populate destination (.*) guests (.*) during (.*) and (.*) on search modal")]
        public void WhenPopulateDestinationGuestsDuringAndSearchModal(string destination, string guestsData, int departure, int duration)
        {
            //if (HelperFunctions.IsV3HomepageEnabled())
            //{
            //    WhenEditDestinationOnSearchModalInLandingPage(destination);
            //    _searchComponent.ConfirmNewDestination();
            //    WhenEditGuestsOnLandingPage(guestsData);
            //    ThenICanSelectAndDateOnLandingPage(departure, duration);
            //}
            //else
            {
                WhenEditDestinationOnSearchModalTo(destination);
                WhenEditGuests(guestsData);
                WhenUpdateAndDates(departure, duration);
            }
            
        }

        [When(@"Populate destination (.*) guests (.*) during (.*) and (.*) on landing page search modal")]
        [When(@"I populate destination (.*) guests (.*) during (.*) and (.*) on landing page search modal")]
        public void WhenPopulateDestinationGuestsDuringAndOnLandingPageSearchModal(string destination, string guestsData, int departure, int duration)
        {
            WhenEditDestinationOnSearchModalInLandingPage(destination);
            WhenEditGuestsOnLandingPage(guestsData);
            ThenICanSelectAndDateOnLandingPage(departure, duration);
        }

     
        [When(@"I populate destination (.*) airport (.*) guests (.*) during (.*) and (.*) on search modal")]
        public void WhenPopulateDestinationAirportGuestsDuringSearchModal(string destination, string airport, string guestsData, int departure, int duration)
        {
            WhenEditDestinationOnSearchModalTo(destination);
            WhenUpdateDepartureAirportOnSearchModalTo(airport);
            WhenEditGuests(guestsData);
            WhenUpdateAndDates(departure, duration);
        }

        [When(@"Populate destination (.*) airport (.*) guests (.*) during (.*) and (.*) in landing page search modal")]
        [When(@"I populate destination (.*) airport (.*) guests (.*) during (.*) and (.*) in landing page search modal")]
        public void WhenPopulateDestinationAirportGuestsDuringAndInLandingPageSearchModal(string destination, string airport, string guestsData, int departure, int duration)
        {
            WhenEditDestinationOnSearchModalInLandingPage(destination);
            WhenUpdateDepartureAirportOnLandingPageSearchModalTo(airport);
            WhenEditGuestsOnLandingPage(guestsData);
            ThenICanSelectAndDateOnLandingPage(departure, duration);
        }


        [When(@"I populate destination (.*) airport (.*) guests (.*) during (.*) and (.*) on search modal")]
        public void WhenPopulateDestinationAirportGuestsDuringAnd(string destination, string airport, string guestsData, int departure, int duration)
        {
            WhenEditDestinationOnSearchModalTo(destination);
            WhenUpdateDepartureAirportOnSearchModalTo(airport);
            WhenEditGuests(guestsData);
            WhenUpdateAndDates(departure, duration);
        }

        [Then(@"Search modal is displayed")]
        public void ThenSearchModalIsDisplayed()
        {
            Assert.IsTrue(_searchComponent.IsDestinationVisible(), "Destination field validation");
            Assert.IsTrue(_searchComponent.IsPassengersVisible(), "Passengers field validation");
            Assert.IsTrue(_searchComponent.IsDatesVisible(), "Dates field validation");
        }

        [Then(@"Need more rooms text information is displayed")]
        public void ThenNeedMoreThanRoomsTextInformationIsDisplayed()
        {
            Assert.IsTrue(_guestComponent.GetNeedMoreRoomsText().Contains("Need more help? Call our Sales Experts on 0208 974 7200"), "Need more help message validation");
        }

        [When(@"Click on hotels tab on search modal")]
        public void WhenClickOnHotelsTabOnSearchModal()
        {
            _searchComponent.SelectHotelsTab();
        }

        [When(@"Click on hotels tab on search modal on landing page")]
        [Given (@"Click on hotels tab on search modal on landing page")]
        public void WhenClickOnHotelsTabOnSearchModalOnLandingPage()
        {
            _landingPageSearchComponent.SelectHotelsTab();
        }

        [When(@"Click on holidays tab on search modal")]
        public void WhenClickOnHolidaysTabOnSearchModal()
        {
            _searchComponent.SelectHolidaysTab();
        }

        [When(@"Click on holidays tab on search modal on landing page")]
        public void WhenClickOnHolidaysTabOnSearchModalOnLandingPage()
        {
            _landingPageSearchComponent.SelectHolidaysTab();
        }

        [Then(@"Landing page search modal should be displayed with pre-populated details")]
        public void ThenLandingPageSearchModalShouldBeDisplayedWithPre_PopulatedDetails()
        {
            Assert.IsTrue(_searchComponent.GetDestination().Contains(context["LandingPageDestination"].ToString(), StringComparison.OrdinalIgnoreCase), "landing destination not matched on search modal");
            _searchComponent.VerifyGuestsOnSearchModal(_guestComponent.SetRoomsData());
            Assert.AreEqual("Add dates", _searchComponent.GetDatesOnSearchModal(), "Dates field should be blank on landing page search modal");
            if(_searchComponent.IsHolidaysTabSelected())
                Assert.AreEqual("Add airports",_searchComponent.GetFlightsFromText(), "Airport field should be blank on landing page search modal");
        }

        [When(@"Edit child age")]
        public void WhenEditChildAge()
        {
            if (HelperFunctions.IsV3HomepageEnabled())
                _landingPageGuestComponent.PopulateNewChildAge(6);
            else
                _guestComponent.PopulateNewChildAge(6);
            _guestComponent.ConfirmNumberOfGuests();
        }

        [When(@"I update guests")]
        public void WhenIUpdateGuests()
        {
            List<RoomOccupantDetails> roomDetails = new List<RoomOccupantDetails>();
            roomDetails = _guestComponent.SetRoomsData();
        }

        [When(@"click on close icon on occupancy")]
        public void WhenClickOnCloseIconOnOccupancy()
        {
            _guestComponent.CloseIconButton();
        }

        [Then(@"I should be able to select the current date")]
        public void ThenIShouldBeAbleToSelectTheCurrentDate()
        {
            Assert.IsTrue(_calendarComponent.IsDateEnabled(0), "current date is not enabled ");
        }

        [Then(@"Privious date should be disabled")]
        public void ThenPriviousDateShouldBeDisabled()
        {
            Assert.IsFalse(_calendarComponent.IsDateEnabled(-1),"previous date is not  disabled");
        }

        [Then(@"Future date should be enabled")]
        public void ThenFutureDateShouldBeEnabled()
        {
            Assert.IsTrue(_calendarComponent.IsDateEnabled(1),"future date is not enabled");
        }

        [Then(@"New selected child age is displayed")]
        public void ThenNewSelectedChildAgeIsDisplayed()
        {
            _searchComponent.EditPassengers();
            Assert.AreEqual(6, _guestComponent.GetSelectedChildAge(1), "Child age validation");
            Assert.AreEqual(6, _guestComponent.GetSelectedChildAge(2), "Child age validation");
        }

        [When(@"Click on destination")]
        public void WhenClickOnDestination()
        {
            _searchComponent.ClickDestinationField();
        }

        [Then(@"(.*) should be pre populated on destination modal")]
        public void ThenShouldBePrePopulared(string destination)
        {
            Assert.AreEqual(destination, _searchComponent.GetDestination(), "Destination name miss match on search modal");
        }

        [Then(@"(.*) should be highlighted on destination auto completer")]
        public void ThenShouldBeHighlightedOnDestinationAutoCompleter(string destination)
        {
            Assert.AreEqual(destination, _searchComponent.GetHighlightedDestinationFromAutoCompleter(), "Destination name miss match on destination auto completer");
        }

        [When(@"I enter three letter (.*) in the destination text box")]
        public void WhenIEnterThreeLetterDUBInTheDestinationTextBox(string destination)
        {
            _searchComponent.EnterDestination(destination);
        }

        [Then(@"Destination auto completer should be displayed")]
        public void ThenDestinationAutoCompleterShouldBeDisplayed()
        {
            Assert.IsTrue(_searchComponent.IsDestinationAutoCompleterDisplayed(), "Destination auto completer is not displayed");
        }

        [When(@"Close search modal")]
        public void WhenCloseSearchModal()
        {
            _searchComponent.CloseSearchPage();
        }

        [When(@"Click on flying from field")]
        public void WhenClickOnFlyingFromField()
        {
            _searchComponent.ClickFlyingFromField();
        }

        [Then(@"Flying from field should be pre-populated for london all airports")]
        public void ThenFlyingFromFieldShouldBePre_PopulatedForLondonAllAirports()
        {
            Assert.IsTrue(_airportComponent.GetFlyingFromSummaryText().Contains("London"), "Airpot name is missing on summary");
            Assert.IsTrue(_airportComponent.GetFlyingFromSummaryText().Contains("(+ 5 others)") ,"additional airports text is missing on summary");
        }

        [Then(@"Flying from field should be pre-populated as per the initial (.*)")]
        public void ThenFlyingFromFieldShouldBePre_PopulatedAsPerTheInitial(string flyingFromDestination)
        {
            Assert.AreEqual(flyingFromDestination, _airportComponent.GetFlyingFromSummaryText());
        }

        [Then(@"All london airports should be pre selected")]
        public void ThenAllLondonAirportsShouldBePreSelected()
        {
            List<string> selectedAirports = _searchComponent.GetSelectedAirports();
            Assert.IsTrue(selectedAirports.Count == 6);
            Assert.IsTrue(selectedAirports.Contains("London Heathrow"));
            Assert.IsTrue(selectedAirports.Contains("Luton"));
            Assert.IsTrue(selectedAirports.Contains("London Southend"));
            Assert.IsTrue(selectedAirports.Contains("Stansted"));
            Assert.IsTrue(selectedAirports.Contains("London City"));
            Assert.IsTrue(selectedAirports.Contains("London Gatwick"));
        }

        [Then(@"(.*) alone should be pre selected")]
        public void ThenAloneShouldBePreSelected(string flyingFromDestination)
        {
            List<string> selectedAirports = _searchComponent.GetSelectedAirports();
            Assert.IsTrue(selectedAirports.Count == 1, "More than one airport is selected");
            Assert.AreEqual(selectedAirports[0], flyingFromDestination, "Expected airport is not pre selected: " + flyingFromDestination);
        }

        [Then(@"Validate airport picker header text")]
        public void ThenValidateAirportPickerHeaderText()
        {
            Assert.AreEqual(_airportComponent.GetFlyingFromHeaderText(), "Where are you flying from?", "Header text missmatch");
        }

        [Then(@"Close modal button should be displayed on airport picker modal")]
        public void ThenCloseModalButtonShouldBeDisplayedOnAirportPickerModal()
        {
            Assert.IsTrue(_airportComponent.IsCloseButtonDisplayedOnAirpotSelectionModal(), "Close button not dispalyed on airport picker modal");
        }

        [Then(@"Confirm departure airports button should be displayed")]
        public void ThenConfirmDepartureAirportsButtonShouldBeDisplayed()
        {
            Assert.IsTrue(_airportComponent.IsConfirmDepartureAirportButtonDisplayed(), "Confirm departure aiports button not dispalyed on airport picker modal");
        }

        [Then(@"Other airports near the searched location should be displayed")]
        public void ThenOtherAirportsNearTheSearchedLocationShouldBeDisplayed()
        {
            Assert.IsTrue(_airportComponent.IsNearBySuggestedAirpotsDisplayed(), "Near by aiports not dispalyed on airport picker modal");
        }

        [When(@"Remove all added airports")]
        public void WhenRemoveAllAddedAirports()
        {
            _airportComponent.RemoveAllSelectedAirports();
        }

        [When(@"Click confirm departure airport button")]
        public void WhenClickConfirmDepartureAirportButton()
        {
            _airportComponent.ClickConfirmDepartureAirportButton();
        }

        [Then(@"(.*) error message should be displayed on airport picker modal")]
        public void ThenErrorMessageShouldBeDisplayedOnAirportPickerModal(string errorMessage)
        {
            Assert.AreEqual(errorMessage, _airportComponent.GetErrorMessageOnAirportPickerModal(), "Error message miss match");
        }

        [When(@"Click close button on airport picker modal")]
        public void WhenClickCloseButtonOnAirportPickerModal()
        {
            _airportComponent.CloseAirportPickerModal();
        }

        [Then(@"Flying from text should be unchanged from previous selection for london all airports")]
        public void ThenFlyingFromTextShouldNotBeChangedOnSearchModal()
        {
            Assert.IsTrue(_searchComponent.GetFlightsFromText().Contains("London"), "departure airport miss match in flying from summary on search modal");
            Assert.IsTrue(_searchComponent.GetFlightsFromText().Contains("(+ 5 others)"), "Additional aiports text missing on summary");
        }

        [Then(@"Flying from text on search modal should match with (.*)")]
        public void ThenFlyingFromTextOnSearchModalShouldMatchWithLondonGatwick(string departureAirport)
        {
            string[] airportsToAdd = departureAirport.Split("-");
             List<string> airports = airportsToAdd.ToList();
            if (airports.Count > 1)
            {
                Assert.IsTrue(_searchComponent.GetFlightsFromText().Contains(departureAirport), "departure airport miss match in flying from summary on search modal");
                Assert.IsTrue(_searchComponent.GetFlightsFromText().Contains("(+ "+(airports.Count-1).ToString()+" others)"), "Additional aiports text missing on summary");
            }
            else
                Assert.AreEqual(_searchComponent.GetFlightsFromText(), departureAirport, "departure airport miss match in flying from summary on search modal");
        }
    }

}

