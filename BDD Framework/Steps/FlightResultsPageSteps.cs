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
    public sealed class FlightResultsPageSteps
    {
        
        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly IBaggage _baggage;
        private readonly ITransfers _transfers;
        private readonly ITravelInsurance _travelInsurance;
        private readonly IHolidayExtrasPage _holidayExtrasPage;
        private readonly IGuestComponent _guestComponent; 
        private readonly IFlightDetailsPage _flightDetailsPage;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IFlightFilters _flightFilters;
        private readonly IFilterSlider _filterSlider;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private readonly ILandingPageCalendarComponent _landingPageCalendarComponent;

        public FlightResultsPageSteps(IHomePage homePage, IHotelSearchResults hotelSearchResults, ILandingPageCalendarComponent landingPageCalendarComponent, ILandingPageGuestComponent landingPageGuestComponent, IHotelEstabPage hotelEstabPage, IBookingSummary bookingSummary, ISearchComponent searchComponent, IFlightSearchResults flightSearchResults, IFilterSlider filterSlider, IBaggage baggage, ITransfers transfers, ITravelInsurance travelInsurance, IHolidayExtrasPage holidayExtrasPage, IGuestComponent guestComponent, IFlightDetailsPage flightDetailsPage,ICalendarComponent calendarComponent, IFlightFilters flightFilters, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _bookingSummary = bookingSummary;
            _flightSearchResults = flightSearchResults;
            _baggage = baggage;
            _transfers = transfers;
            _travelInsurance = travelInsurance;
            _holidayExtrasPage = holidayExtrasPage;
            _guestComponent = guestComponent;
            _flightDetailsPage = flightDetailsPage;
            _calendarComponent = calendarComponent;
            _flightFilters = flightFilters;
            _filterSlider = filterSlider;
            _landingPageGuestComponent = landingPageGuestComponent;
            _landingPageCalendarComponent = landingPageCalendarComponent;
        }
        private List<FlightInboundOutboudInformationModel> flightInfo;
        private int initialFlightSearchResultCount;

        [When(@"Confirm the pre selected flight")]
        [When(@"I click on the Keep button")]
        public void WhenConfirmThePreSelectedFlight()
        {
            _flightSearchResults.CapturePreSelectedFlightInformation();
            _flightSearchResults.KeepSelectedFlight();
        }

        [When(@"Select random flight")]
        public void WhenSelectRandomFlight()
        {
            _flightSearchResults.CaptureFlightInformation(_flightSearchResults.GetFlightToSelect());
            _flightSearchResults.SelectAlternateFlight(_flightSearchResults.GetFlightToSelect());
            if(_flightSearchResults.IsAccomadationAdjustmentPopupDisplayed())
            {
                if (_flightSearchResults.AreNumberOfDaysDecreased())
                {
                    _landingPageCalendarComponent.SetDepartureDate(_landingPageCalendarComponent.GetDepartureDate().AddDays(1));
                }
                else
                {
                    _landingPageCalendarComponent.SetDepartureDate(_landingPageCalendarComponent.GetDepartureDate().AddDays(-1));
                }
                _flightSearchResults.AcceptAccomadationDateAdjustment();
            }
        }


        [Given(@"I am on holiday Flight search results page")]
        public void GivenIAmOnHolidayFlightSearchResultsPage()
        {
            _homePage.SearchHolidays();
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if(HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
        }

        [Then(@"Flight search results count is (.*)")]
        public void ThenFlightSearchResultsCountIs(int flightCount)
        {
            Assert.AreEqual(flightCount, _flightSearchResults.GetTotalFlightCount(), "Flight results count is not matching");
        }

        [Then(@"Flight search results are displayed")]
        public void ThenFlightSearchResultsAreDisplayed()
        {
            Assert.IsTrue(_flightSearchResults.GetTotalFlightCount() >= 1, "Flight search results are displayed");
        }


        [Then(@"Booking Summary header should be displayed")]
        [Then(@"Booking Summary screen should be displayed")]
        public void ThenBookingSummaryHeaderShouldBeDisplayed()
        {
            if(!HelperFunctions.IsDesktop())
                Assert.IsTrue(_bookingSummary.IsBookingSummaryHeaderDisplayed());
        }

        [Then(@"Baggage option (.*) should be displayed")]
        public void ThenBaggageOptionNeedCheckedBagsShouldBeDisplayed(string message)
        {
            Assert.AreEqual(_flightSearchResults.GetBaggageOptionText(), message);
        }

        [Then(@"Sort by link should be displayed")]
        public void ThenSortByLinkShouldBeDisplayed()
        {
            Assert.IsTrue(_flightSearchResults.IsSortOptionVisible(), "Sort option not available");
        }

        [Then(@"Show more flights button should be displayed if more flights available")]
        public void ThenShowMoreFlightsButtonShouldBeDisplayedIfMoreFlightsAvailable()
        {
           if(_flightSearchResults.GetTotalFlightCount() == 15)
            {
                Assert.IsTrue(_flightSearchResults.IsShowMoreFlightsButtonVisible(), "Show more flights button is not visible");
            }
        }

        [Then(@"Pre-selected flight card holiday price should be zero")]
        public void ThenPre_SelectedFlightCardHolidayPriceShouldBeZero()
        {
            Assert.IsTrue(_flightSearchResults.GetPreSelectedFlightHolidayPrice() == 0, "Delta price on pre-selected flight card is not zero");
        }

        [Then(@"Extras selection screen should be displayed")]
        public void ThenExtrasSelectionScreenShouldBeDisplayed()
        {
            Assert.IsTrue(_transfers.IsTransferSectionVisible(), "Extras page not displayed");
        }

        [When(@"I select a random flight from the results")]
        public void WhenISelectARandomFlightFromTheResults()
        {
            _flightSearchResults.SelectAlternateFlight(_flightSearchResults.GetFlightToSelect());
        }

        [When(@"I click on View flight details link")]
        [When(@"I click on Return flights link")]
        public void WhenIClickOnReturnFlightsLink()
        {
            _flightSearchResults.ViewPreSelectedFlightDetails();
        }

        [Then(@"Flight details modal should be displayed")]
        public void ThenFlightDetailsModalShouldBeDisplayed()
        {
            Assert.IsTrue(_flightDetailsPage.IsFlightDetailsModalDisplayed(), "Fligght details modal is not displayed");
        }

        [Given(@"I am on holiday flight search results for (.*) from (.*)")]
        public void GivenIAmOnHolidayFlightSearchResultsForParisFromLondon(string destination, string departure_airport)
        {
            _homePage.SearchHolidays(destination, departure_airport);
            _hotelSearchResults.CaptureHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if(HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.ClickShowMoreFlights();
        }

        [Given(@"I am on holiday flight search results page for (.*) from (.*) for (.*) during (.*) and (.*) dates")]
        public void GivenIAmOnHolidayFlightSearchResultsPageForFromForDuringAndDates(string destination, string departureAirport, string guests, int departure, int duration)
        {
            _homePage.SearchHolidays(destination, departureAirport, departure, duration, guests);
            _hotelSearchResults.CaptureHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if (HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
        }

        [When(@"I click show more Flights button")]
        public void WhenIClickShowMoreFlightsButton()
        {
            context.Add("Coordinates", _flightSearchResults.GetShowMoreFlightsLocation());
            Console.WriteLine("Location of the button is " + context["Coordinates"] as string);
            context.Add("InitialFlightResultsCount", _flightSearchResults.GetTotalFlightCount());
            _flightSearchResults.ClickShowMoreFlights();
        }

        [Then(@"Additional flight results should be loaded")]
        public void ThenAdditionalFlightResultsShouldBeLoaded()
        {
            Assert.Greater(_flightSearchResults.GetTotalFlightCount(), Convert.ToInt32(context["InitialFlightResultsCount"]));
        }

        [Then(@"The show more flights button should move to the bottom of the list")]
        public void ThenTheShowMoreFlightsButtonShouldMoveToTheBottomOfTheList()
        {
            Console.WriteLine("Location of the button after loading more hotels is " + _flightSearchResults.GetShowMoreFlightsLocation() as string);
            Assert.LessOrEqual(Convert.ToInt16(context["Coordinates"]), _flightSearchResults.GetShowMoreFlightsLocation(), "Coordinates of show more flights button does not match!");
        }

        [Then(@"Total price should be displayed on booking summary bar")]
        public void ThenTotalPriceShouldBeDisplayedOnBookingSummaryBar()
        {
            Assert.IsTrue(_bookingSummary.IsTotalPriceDisplayedOnSummaryHeadder());
        }

        [When(@"I click on Booking Summary button")]
        public void WhenIClickOnBookingSummaryButton()
        {
            _bookingSummary.ClickBookingSummaryButton();
        }

        [When(@"I open flight sort options")]
        public void WhenIOpenFlightSortOptions()
        {
            _flightSearchResults.OpenSortOptions();
        }

        [Then(@"Flight search results are sorted based on the selected (.*)")]
        public void ThenFlightSearchResultsAreSortedBasedOnTheSelectedRecommended(string option)
        {
           Assert.IsTrue(_flightSearchResults.VerifyAppliedFlightSortOption(option));
        }

        [Then(@"I want to see the Additional Flight cards displayed")]
        public void ThenIWantToSeeTheAdditionalFlightCardsDisplayed()
        {
            Assert.IsTrue(_flightSearchResults.IsAdditionalFlightSearchResultsDisplayed(), "Additional flight results is not available!");
        }

        [Then(@"I Want to see the Preselected Flight card displayed")]
        public void ThenIWantToSeeThePreselectedFlightCardDisplayed()
        {
            Assert.IsTrue(_flightSearchResults.IsPreselectedFlightCardDisplayed(), "Pre-Selected flight results is not available!");
        }

        [Then(@"Flight card for open jaw flight information displayed")]
        public void ThenFlightCardForOpenJawFlightInformationDisplayed()
        {
            FlightInboundOutboudInformationModel info = _flightSearchResults.CaptureAndReturnFlightInformation(_flightSearchResults.GetOpenJawFlight());
        }

        [Then(@"the flight card for pre-selected flights information displayed")]
        public void ThenTheFlightCardForPre_SelectedFlightsInformationDisplayed()
        {
            _flightSearchResults.CapturePreSelectedFlightInformation();
        }

        [Then(@"the flight card for connecting flights information displayed")]
        public void ThenTheFlightCardForConnectingFlightsInformationDisplayed()
        {
            _flightSearchResults.CaptureAndReturnFlightInformation(_flightSearchResults.GetConnectingFlight());
        }

        [Then(@"the flight card for direct flight different carrier information displayed")]
        public void ThenTheFlightCardForDirectFlightDifferentCarrierInformationDisplayed()
        {
            _flightSearchResults.CaptureAndReturnFlightInformation(_flightSearchResults.GetDirectFlightDifferentCarrier());
        }

        [Then(@"the flight card for direct flight same carrier information displayed")]
        public void ThenTheFlightCardForDirectFlightSameCarrierInformationDisplayed()
        {
            _flightSearchResults.CaptureAndReturnFlightInformation(_flightSearchResults.GetDirectFlightSameCarrier());
        }


        [Then(@"the flight card for different carrier each flight information displayed")]
        public void ThenTheFlightCardForDifferentCarrierEachFlightInformationDisplayed()
        {
            _flightSearchResults.CaptureAndReturnFlightInformation(_flightSearchResults.GetDifferentCarrierForEachFlight());
        }

        [Then(@"Flight filter button should not be displayed")]
        public void ThenFlightFilterButtonShouldNotBeDisplayed()
        {
            Assert.IsFalse(_flightSearchResults.IsFlightFiltersVisible(), "flight filter button is displayed");
        }
        [Then(@"Number of stops filter should not be displayed")]
        public void ThenNumberOfStopsFilterShouldNotBeDisplayed()
        {
            Assert.IsFalse(_flightFilters.IsNoOfStopsFilterDisplayed(), "No of stops filter is dispalyed");
        }

        [Then(@"Depart/Return to same airport should not be displayed")]
        public void ThenDepartReturnToSameAirportShouldNotBeDisplayed()
        {
            Assert.IsFalse(_flightFilters.IsSameDepartureArrivalAirportFilterDisplayed(), "Depart/return to same airport filter is dispalyed");
        }

        [When(@"I open flight filters")]
        public void WhenIOpenFlightFilters()
        {
            _flightSearchResults.OpenFlightFilters();
        }

        [When(@"I applied filters to see view matches count is zero")]
        public void WhenIAppliedFiltersToSeeViewMatchesCountIsZero()
        {
            _flightSearchResults.OpenFlightFilters();
            int count = 0;
            while (_flightFilters.GetViewMatchesCount() != 0 && count < 5)
            {
                _filterSlider.SetOutBoundDepartureTimeMaxFilter(-100, 0);
                _filterSlider.SetOutBoundDepartureTimeMinFilter(50, 0);
                _filterSlider.SetReturnDepartureTimeMaxFilter(-100, 0);
                _filterSlider.SetReturnDepartureTimeMinFilter(50, 0);
                count++;
            }
            Assert.Zero(_flightFilters.GetViewMatchesCount(), "View match count is not zero");
        }


        [Given(@"Store the flight search results")]
        public void GivenStoreTheFlightSearchResults()
        {
            flightInfo = _flightSearchResults.CaptureAllFlightResults();
            initialFlightSearchResultCount = _flightSearchResults.GetTotalFlightCount();
        }

        [Then(@"Flight Search Results should change as per filters applied")]
        public void ThenFlightSearchResultsShouldChangeAsPerFiltersApplied()
        {
            Assert.IsTrue(_flightSearchResults.IsFilteredMessageVisisble(), "Filters applied on flight SRP");
            Assert.AreNotEqual(initialFlightSearchResultCount, _flightSearchResults.GetFilteredResultsCount(), "Filters are not applied");
        }

        [Then(@"Departure airport should match in pre selected flight card")]
        public void ThenDepartureAirportShouldMatchInPreSelectedFlightCard()
        {
            _flightSearchResults.CapturePreSelectedFlightInformation();
            Assert.AreEqual(_homePage.GetDepartureAirport(), _flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Outbound].DepartureLocation, "Outbound departure airport validation in flight card");
            Assert.AreEqual(_homePage.GetDepartureAirport(), _flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Inbound].ArrivalLocation, "Inbound arrival airport validation in flight card");
        }

        [Then(@"Preselected flight is a easyjet flight")]
        public void ThenPreselectedFlightIsAEasyjetFlight()
        {
            _flightSearchResults.CapturePreSelectedFlightInformation();
            Assert.AreEqual("Small/ezy.png", _flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Outbound].FlightLogo);
            Assert.AreEqual("Small/ezy.png", _flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Inbound].FlightLogo);
        }

        [Then(@"Baggage information text should be displayed")]
        public void ThenBaggageInformationTextIsDisplayed()
        {
            Assert.AreEqual(_flightSearchResults.GetFlightAllocationBaggageText(), Constants.FlightAllocationBaggageText, "Baggage text in flight card validation");
        }

        [Then(@"Baggage price sash is displayed")]
        public void ThenBaggagePriceSashIsDisplayed()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.AreEqual(string.Format("{0}{1}{2}", "SAVE£", Constants.FlightAllocationBaggagePrice * _landingPageGuestComponent.GetNonInfantOccupants(), "TODAY!")
                , _flightSearchResults.GetFlightAllocationSashText().Replace("\r\n", ""));
            else
                Assert.AreEqual(string.Format("{0}{1}{2}", "SAVE£", Constants.FlightAllocationBaggagePrice * _guestComponent.GetNonInfantOccupants(), "TODAY!")
                , _flightSearchResults.GetFlightAllocationSashText().Replace("\r\n", ""));
        }

        [Then(@"Preselected flight should not change")]
        public void ThenPreselectedFlightShouldNotChange()
        {
            FlightInboundOutboudInformationModel preselectedFlightInfo = _flightSearchResults.GetFlightInformation();
            _flightSearchResults.CapturePreSelectedFlightInformation();
            FlightInboundOutboudInformationModel newPreselectedFlightInfo = _flightSearchResults.GetFlightInformation();
            Assert.IsTrue(_flightSearchResults.CompareFlightLegs(preselectedFlightInfo.flightLeg[(int)FlightLegType.Outbound], newPreselectedFlightInfo.flightLeg[(int)FlightLegType.Outbound]), "Preselected outbound flight validation");
            Assert.IsTrue(_flightSearchResults.CompareFlightLegs(preselectedFlightInfo.flightLeg[(int)FlightLegType.Inbound], newPreselectedFlightInfo.flightLeg[(int)FlightLegType.Inbound]), "Preselected inbound flight validation");
        }

        [Then(@"flight card should display arriving on future days as per design")]
        public void ThenFlightCardShouldDisplayArrivingOnFutureDaysAsPerDesign()
        {
            Assert.IsTrue(_flightSearchResults.IsArrivingOnFutureDays(), "Arriving on future days validation failed. Try changing data");
        }

        [When(@"I Click on Flight filter results Button")]
        public void WhenIClickOnFlightFilterResultsButton()
        {
            _flightSearchResults.OpenFlightFilters();
        }

        [Then(@"Outbound arrival time should be after 4AM")]
        public void ThenOutboundArrivalTimeShouldBeAfter4AM()
        {
            _flightSearchResults.CapturePreSelectedFlightInformation();
            FlightInboundOutboudInformationModel preselectedFlightInfo = _flightSearchResults.GetFlightInformation();
            DateTime SearchDate;
            if (HelperFunctions.IsV3HomepageEnabled())
                SearchDate = _landingPageCalendarComponent.GetDepartureDate();
            else
                SearchDate = _calendarComponent.GetDepartureDate();
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickBookingSummaryButton();
            DateTime DateOnBookingSummary = Convert.ToDateTime(_bookingSummary.GetSearchItinerary().Split("-")[0].Remove(0, 4));
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.CloseBookingSummaryModal();
            if (preselectedFlightInfo.flightLeg[(int)FlightLegType.Outbound].AdditionalDays > 0)
            {
                DateTime ArrivalTime = Convert.ToDateTime(preselectedFlightInfo.flightLeg[(int)FlightLegType.Outbound].ArrivalTime);
                if(ArrivalTime.TimeOfDay >= Convert.ToDateTime(Constants.OutBoundArrivalCutOffTime).TimeOfDay)
                {
                    flightInfo = _flightSearchResults.CaptureAllFlightResults();
                    foreach (var flight in flightInfo)
                    {
                        Assert.AreEqual(preselectedFlightInfo.flightLeg[(int)FlightLegType.Outbound].AdditionalDays, flight.flightLeg[(int)FlightLegType.Outbound].AdditionalDays, "Additional days miss match");
                        ArrivalTime = Convert.ToDateTime(flight.flightLeg[(int)FlightLegType.Outbound].ArrivalTime);
                        Assert.IsTrue(ArrivalTime.TimeOfDay >= Convert.ToDateTime(Constants.OutBoundArrivalCutOffTime).TimeOfDay, "Expected outbound arrival cut off is 4am, but arival time was " + ArrivalTime.TimeOfDay);
                    }
                    Assert.IsTrue(SearchDate.AddDays(preselectedFlightInfo.flightLeg[(int)FlightLegType.Outbound].AdditionalDays).Date.CompareTo(DateOnBookingSummary.Date).Equals(0), "Hotel check in date is not adjusted as per flight cut off timings");                   
                }                    
                else
                {
                    // Known issue for some destinations (Eg : Costa Blanca)
                    Assert.Warn("Expected outbound arrival cut off is 4am, but arival time was " +ArrivalTime.TimeOfDay);                    
                    Assert.IsTrue(SearchDate.Date.CompareTo(DateOnBookingSummary.Date).Equals(0), "Hotel check in date on booking summary does not match with initial search data");
                    flightInfo = _flightSearchResults.CaptureAllFlightResults();
                    foreach (var flight in flightInfo)
                    {
                        if (flight.flightLeg[(int)FlightLegType.Outbound].AdditionalDays > 0)
                            Assert.IsTrue(ArrivalTime.TimeOfDay < Convert.ToDateTime(Constants.OutBoundArrivalCutOffTime).TimeOfDay);
                    }
                }
            }
            else
            {
                Assert.IsTrue(SearchDate.Date.CompareTo(DateOnBookingSummary.Date).Equals(0), "Hotel check in date on booking summary does not match with initial search data");                
            }           
        }

        [Then(@"Inbound departure time should be after 3AM")]
        public void ThenInboundDepartureTimeShouldBeAfter3AM()
        {
            _flightSearchResults.CapturePreSelectedFlightInformation();
            FlightInboundOutboudInformationModel preselectedFlightInfo = _flightSearchResults.GetFlightInformation();
            DateTime DepartureTime = Convert.ToDateTime(preselectedFlightInfo.flightLeg[(int)FlightLegType.Inbound].DepartureTime);
            Assert.IsTrue(DepartureTime.TimeOfDay >= Convert.ToDateTime(Constants.InBoundDepartureCutOffTime).TimeOfDay);

            if (flightInfo == null)
                flightInfo = _flightSearchResults.CaptureAllFlightResults();

            foreach (var flight in flightInfo)
            {
                DepartureTime = Convert.ToDateTime(flight.flightLeg[(int)FlightLegType.Inbound].DepartureTime);
                Assert.IsTrue(DepartureTime.TimeOfDay >= Convert.ToDateTime(Constants.InBoundDepartureCutOffTime).TimeOfDay);                
            }
        }
        [Given(@"flights search results contains less flights")]
        public void GivenFlightsSearchResultsContainsLessFlights()
        {
            Assert.IsTrue((_flightSearchResults.GetAdditionalFlightResultsCount() <= 14), "Less than 15 Flights are displayed");
        }

        [Then(@"Show more flights button should not displayed")]
        public void ThenShowMoreFlightsButtonShouldNotDisplayed()
        {
            Assert.IsFalse(_flightSearchResults.IsShowMoreFlightsButtonVisible(), "Show more flights button is not visible");
        }

        [When(@"I continue to Guest details")]
        [When(@"I continue to book on Extras page")]
        public void WhenIContinueToGuestDetails()
        {
            if (!HelperFunctions.IsDesktop())
                _holidayExtrasPage.ContinueToBook();
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
        }

        [When(@"I continue to book")]
        public void WhenIContinueToBook()
        {
            if (!HelperFunctions.IsDesktop())
                _holidayExtrasPage.ContinueToBook();
        }

        [Then(@"Flight cards should display stopover information")]
        public void ThenFlightCardsShouldDisplayStopoverInformation()
        {
            //Pre Seleted Flight
            Assert.IsTrue(_flightSearchResults.VerifyFlightStopOverInfo(_flightSearchResults.GetStopOverInfoInPreSelctedFlight(FlightType.Outbound)), "Outbound Stopover info is not matched");
            Assert.IsTrue(_flightSearchResults.VerifyFlightStopOverInfo(_flightSearchResults.GetStopOverInfoInPreSelctedFlight(FlightType.Return)), "Return Stopover info is not matched");

            //Additional Flight cards           
            Assert.IsTrue(_flightSearchResults.VerifyStopOverInfoInAdditionalFlightCards(FlightType.Outbound), "Outbound Stopover info is not matched");
            Assert.IsTrue(_flightSearchResults.VerifyStopOverInfoInAdditionalFlightCards(FlightType.Return), "Return Stopover info is not matched");

        }

        [Then(@"Flight card should display hand luggage for passenger")]
        public void ThenFlightcardShouldDisplayHandLuggageForPassenger()
        {
            Assert.IsTrue(_flightSearchResults.IsHoldLuggageMessageDisplayedOnAnyFlightCard(), "Hold Luggage Message is not displayed in Flight Card");
        }

        [Then(@"I want to see the flight details modal with flight NoOfStops information")]
        public void ThenIWantToSeeTheFlightDetailsModalWithFlightNoOfStopsInformation()
        {            
            string outboundStopInCard= _flightSearchResults.GetStopOverInfoInPreSelctedFlight(FlightType.Outbound);
            string inBoundStopInCard = _flightSearchResults.GetStopOverInfoInPreSelctedFlight(FlightType.Return);
            _flightSearchResults.ViewPreSelectedFlightDetails();
            string OutboundStop =_flightDetailsPage.GetFlightInboundOutboudDetailsModel(FlightType.Outbound).NoOfStops;
            string ReturnStop = _flightDetailsPage.GetFlightInboundOutboudDetailsModel(FlightType.Return).NoOfStops;
            if(outboundStopInCard==OutboundStop && inBoundStopInCard==ReturnStop)
                Console.WriteLine("OutBoundStop and Return Stop Text is Matched");
            else
                Assert.Fail("OutBoundStop and Return stop Text is not matched ");            
        }

        [Then(@"I Want to see the flight details modal with Flight Inclusion information")]
        public void ThenIWantToSeeTheflightdetailsmodalFlightInclusioninformation()
        {
            //check flight card has baggage information before
            if (_flightDetailsPage.IsFlightDetailsModalDisplayed())
                _flightDetailsPage.CloseModal();                         
            if (!_flightSearchResults.IsHoldLuggageMessageDisplayedOnPreSelectedFlightCard())
                Assert.Inconclusive("Flight Inclusion Info is not Displayed");
            string holdLuggageMessage = _flightSearchResults.GetHoldLuggageMessageonPreSelectedFligthCard();
            _flightSearchResults.ViewPreSelectedFlightDetails();
            string OutboundFlightInclusionInfo = _flightDetailsPage.GetFligthInclusionInfo(FlightType.Outbound);
            string ReturnFlightInclusionInfo = _flightDetailsPage.GetFligthInclusionInfo(FlightType.Return);
            _flightDetailsPage.CloseModal();
            
            if (OutboundFlightInclusionInfo.Equals(holdLuggageMessage) && ReturnFlightInclusionInfo.Equals(holdLuggageMessage))
                Console.WriteLine("OutBound ReturnFlight InclusionInfo Text is macthed ");
            else
                Assert.Fail("OutBound ReturnFlight InclusionInfo Text is not matched ");            
        }

    }
}
