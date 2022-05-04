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
    public sealed class FiltersModalSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly IFlightDetailsPage _flightDetailsPage;
        private readonly IFlightFilters _flightFilters;
        private readonly IFiltersModal _filtersModal;
        private readonly IModalPopup _modalPopup;
        private readonly IPriceFilter _priceFilter;
        private readonly IFilterSlider _filterSlider;
        private readonly IGuestComponent _guestComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;

        public FiltersModalSteps(IHomePage homePage, IHotelSearchResults hotelSearchResults, ILandingPageGuestComponent landingPageGuestComponent, IHotelEstabPage hotelEstabPage, IBookingSummary bookingSummary, IFlightSearchResults flightSearchResults, IFlightDetailsPage flightDetailsPage, IFlightFilters flightFilters,
                                    ScenarioContext injectedContext, IModalPopup modalPopup, IFiltersModal filtersModal,
                                    IPriceFilter priceFilter, IFilterSlider filterSlider, IGuestComponent guestComponent)
        {
            context = injectedContext;
            _homePage = homePage;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _bookingSummary = bookingSummary;
            _flightSearchResults = flightSearchResults;
            _flightDetailsPage = flightDetailsPage;
            _flightFilters = flightFilters;
            _modalPopup = modalPopup;
            _filtersModal = filtersModal;
            _priceFilter = priceFilter;
            _filterSlider = filterSlider;
            _guestComponent = guestComponent;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        private int ViewMatchesCount = 0;
        private decimal filterPrice = 0;

        [Given(@"I am on holiday flight filters modal")]
        public void GivenIAmOnHolidayFlightFiltersModal()
        {
            _homePage.SearchHolidays();
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if(HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.OpenFlightFilters();
        }

        [Given(@"I am on holiday flight filters modal for (.*) and (.*)")]
        public void GivenIAmOnHolidayFlightFiltersModalWithDestinationAndDepartureAiport(string destination, string departureAiport)
        {
            _homePage.SearchHolidays(destination, departureAiport);
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if (HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.OpenFlightFilters();
        }

        [Given(@"I am on holiday flight filters modal for (.*) from (.*)")]
        public void GivenIAmOnHolidayFlightFiltersModalForTenerifeFromLondon(string destination, string departure_airport)
        {
            _homePage.SearchHolidays(destination, departure_airport);
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if (HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.OpenFlightFilters();
        }

        [When(@"I click on close button")]
        [When(@"Click on close filter button")]
        public void WhenIClickOnCloseButton()
        {
            _flightFilters.CloseFlightsFliter();
        }

        [Then(@"Filter model should have reset filter button")]
        [Then(@"Reset all button should be displayed")]
        public void ThenResetAllButtonShouldBeDisplayed()
        {
            Assert.IsTrue(_flightFilters.IsResetAllButtonDisplayed(), "Reset all filter button is not dispalyed");
        }

        [Then(@"Departure and return to same airport toggle should be displayed")]
        [When(@"Departure and return to same airport toggle is displayed")]
        public void ThenDepartureAndReturnToSameAirportToggleShouldBeDisplayed()
        {
            Assert.IsTrue(_flightFilters.IsSameDepartureArrivalAirportFilterDisplayed(), "Same departure and arrival airport toggle is not dispalyed");
        }

        [Then(@"Departure airport filter should be displayed")]
        [When(@"Departure airport filter should be displayed")]
        public void ThenDepartureAirportFilterShouldBeDisplayed()
        {
            Assert.IsTrue(_flightFilters.IsDepartureAirportFilterDisplayed(), "Departure airport filter is not dispalyed");
        }

        [Then(@"Outbound Departure time slider should be displayed")]
        [When(@"Outbound Departure time slider should be displayed")]
        public void ThenOutboundDepartureTimeSliderShouldBeDisplayed()
        {
            Assert.IsTrue(_flightFilters.IsOutboundDepartureTimeFilterDisplayed(), "Outbound Departure times filter is not dispalyed");
        }

        [Then(@"Return Departure time slider should be displayed")]
        [When(@"Return Departure time slider should be displayed")]
        public void ThenReturnDepartureTimeSliderShouldBeDisplayed()
        {
            Assert.IsTrue(_flightFilters.IsReturnDepartureTimeFilterDisplayed(), "Return Departure times filter is not dispalyed");
        }

        [Then(@"Airline filter should be displayed")]
        [When(@"Airline filter is displayed")]
        public void ThenAirlineFilterShouldBeDisplayed()
        {
            Assert.IsTrue(_flightFilters.IsAirlineFilterDisplayed(), "Airlines filter is not dispalyed");
        }

        [Then(@"I should see available Airlines")]
        public void ThenIShouldSeeAvailableAirlines()
        {
            Assert.IsTrue(_flightFilters.IsAirlinesDisplayed(), "Airline is not dispalyed");
        }

        [When(@"I select departure airport (.*)")]
        public void WhenISelectDepartureAirportLondonGatwick(string filter)
        {
            _flightFilters.ClickResetFilters();
            _flightFilters.SelectDepartureAirportFilter(filter);
        }

        [Then(@"I can select multiple Airlines and checkbox should be selected")]
        public void ThenICanSelectMultipleAirlines()
        {
            ViewMatchesCount = _flightFilters.GetViewMatchesCount();
            _flightFilters.SelectMultipleAirlines();
        }

        [Then(@"View Matches button count should be updated")]
        public void ThenViewMatchesButtonCountShouldBeUpdated()
        {
            Assert.IsTrue(ViewMatchesCount != _flightFilters.GetViewMatchesCount(), "View matches count not updated");
        }

        [Then(@"I should be able to toggle (.*) the option")]
        [When(@"I toggle (.*) the option")]
        public void ThenIShouldBeAbleToToggleTheOption(string toggle)
        {
            ViewMatchesCount = _flightFilters.GetViewMatchesCount();
            if (toggle.Equals("on", StringComparison.InvariantCultureIgnoreCase))
            {
                _flightFilters.ToggleDepartureReturnSameAirport(true);
            }

            else if(toggle.Equals("off", StringComparison.InvariantCultureIgnoreCase))
            {
                _flightFilters.ToggleDepartureReturnSameAirport(false);
            }
            
        }
        [Then(@"I want to see flight results as per selected airport toggle (.*) filter")]
        public void ThenIWantToSeeFlightResultsAsPerSelectedAirportToggleONFilter(string toggle)
        {
            //_flightSearchResults.LoadAllFlights();
            List<FlightInboundOutboudInformationModel> flightInfo = _flightSearchResults.CaptureAllFlightResults();
            if (toggle.Equals("on", StringComparison.InvariantCultureIgnoreCase))
            {
                foreach (var flight in flightInfo)
                {
                    Assert.IsTrue(flight.flightLeg[0].DepartureLocation.Equals(flight.flightLeg[1].ArrivalLocation), "Departure and return airports are not same");
                }
            }
            else if (toggle.Equals("off", StringComparison.InvariantCultureIgnoreCase))
            {
                bool isNotSameAirport = false;
                foreach (var flight in flightInfo)
                {
                    if (!flight.flightLeg[0].DepartureLocation.Equals(flight.flightLeg[1].ArrivalLocation))
                    {
                        isNotSameAirport = true;
                        break;
                    }
                }
                if(!isNotSameAirport)
                    Assert.Inconclusive("Departure and return airports are same!");
            }
        }

        [Then(@"selection should turn to (.*)")]
        public void ThenSelectionShouldTurnToBlue(string colour)
        {
            Assert.IsTrue(_flightFilters.IsSelectedToggle(colour));
        }

        [Then(@"I want filter modal to be closed")]
        public void ThenIWantFilterModalToBeClosed()
        {
            Assert.IsFalse(_flightFilters.IsFlightFliterModalOpen());
        }

        [When(@"I click View Matches button")]
        public void WhenIClickViewMatches()
        {
            ViewMatchesCount = _flightFilters.GetViewMatchesCount();
            _flightFilters.ClickViewMatchesButton();
        }

        [Then(@"I should see available Departure Airports")]
        public void ThenIShouldSeeAvailableDepartureAirports()
        {
            if(_flightFilters.IsDepartureAirportsDisplayed())
                Assert.IsTrue(_flightFilters.IsDepartureAirportsDisplayed(), "Departure Airports is not dispalyed");
            else
                Assert.Inconclusive("Departure Airports is not dispalyed");
        }

        [Then(@"I can select multiple departure airports and selection should change to blue")]
        public void ThenICanSelectMultipleDepartureAirports()
        {
            ViewMatchesCount = _flightFilters.GetViewMatchesCount();
            _flightFilters.SelectMultipleDepartureAirports();
        }

        [Then(@"Number of stops filter should be displayed if available")]
        [When(@"Number of stops filter should be displayed if available")]
        public void ThenNumberOfStopsFilterShouldBeDisplayedIfAvailable()
        {
            Assert.IsTrue(_flightFilters.IsNoOfStopsFilterDisplayed(), "No of stops filter is not dispalyed");
        }

        [When(@"I click on Reset filter button")]
        public void WhenIClickOnResetFilterButton()
        {
            _flightFilters.ClickResetFilters();
        }

        [Then(@"I want to see Flight Search Results page")]
        public void ThenIWantToSeeFlightSearchResultsPage()
        {
            Assert.IsTrue(_flightSearchResults.IsAdditionalFlightSearchResultsDisplayed(), "flight serach results not displayed");
        }

        [Given(@"There are no alternate flight search results")]
        public void WhenThereAreNoAlternateFlightSearchResults()
        {
            Assert.IsFalse(_flightSearchResults.IsAdditionalFlightSearchResultsDisplayed(), "flight serach results displayed");
        }

        [Then(@"I can select more than one option and each selection should change to blue")]
        public void ThenICanSelectMoreThanOneOptionAndEachSelectionShouldChangeToBlue()
        {
            ViewMatchesCount = _flightFilters.GetViewMatchesCount();
            _flightFilters.SelectMultipleStops();
        }

        [When(@"I apply a (.*) stop filter")]
        public void WhenIApplyAStopFilter(string numberOfStops)
        {
            if (_flightSearchResults.IsFlightFiltersVisible())
                _flightSearchResults.OpenFlightFilters();
            _flightFilters.ClickResetFilters();
            _flightFilters.SelectNoOfStopsFilter(numberOfStops);
            if(_flightFilters.GetViewMatchesCount() > 0)
                _flightFilters.ClickViewMatchesButton();
        }

        [Then(@"I want to see flight results as per selected (.*) stop")]
        public void ThenIWantToSeeFlightResultsAsPerSelectedNumberOfStops(string numberOfStops)
        {
            int noOfStops = _flightFilters.GetNoOfStops(numberOfStops);
            if (_flightFilters.IsFlightFliterModalOpen())
            {
                if(_flightFilters.GetViewMatchesCount() == 0)
                    Assert.Inconclusive("No results for " + numberOfStops + " stop filter");
            }
            else if(!_flightFilters.IsFlightFliterModalOpen())
            {
                List<FlightInboundOutboudInformationModel> flightInfo = _flightSearchResults.CaptureAllFlightResults();
                foreach (var flight in flightInfo)
                {
                    if (noOfStops == 0)
                        Assert.IsTrue(flight.flightLeg[0].Stops.Equals(noOfStops) && flight.flightLeg[1].Stops.Equals(noOfStops), "Direct filter is not working");
                    else if (noOfStops == 1)
                        Assert.IsTrue(flight.flightLeg[0].Stops.Equals(noOfStops) | flight.flightLeg[1].Stops.Equals(noOfStops), "One Stop Filter is not working");
                    else if (noOfStops == 2)
                        Assert.IsTrue(flight.flightLeg[0].Stops.Equals(noOfStops) | flight.flightLeg[1].Stops.Equals(noOfStops),"Two stop filter is not working");
                }
            }
        }

        [Then(@"View Matches button count should be decreased")]
        public void ThenViewMatchesButtonCountShouldBeDecreased()
        {
            Assert.IsTrue(_flightFilters.GetViewMatchesCount() <= ViewMatchesCount, "View matches count not updated");
        }

        [Then(@"View Matches button count should be increased")]
        public void ThenViewMatchesButtonCountShouldBeIncreased()
        {
            Assert.IsTrue(_flightFilters.GetViewMatchesCount() >= ViewMatchesCount, "View matches count not updated");
        }

        [Then(@"I want to see flight results as per selected airlines filter")]
        public void ThenIWantToSeeFlightResultsAsPerSelectedAirlinesFilter()
        {
            Assert.AreEqual(ViewMatchesCount, _flightSearchResults.GetFilteredResultsCount(), "Flitered count not macthing in flight SRP");
            _flightSearchResults.LoadAllFlights();
            Assert.AreEqual(ViewMatchesCount, _flightSearchResults.GetTotalFlightCount(), "Flitered count not macthing in flight SRP");
            Assert.AreEqual(ViewMatchesCount-1, _flightSearchResults.GetAdditionalFlightResultsCount(), "Flitered count not macthing in flight SRP");
        }
        [Then(@"View Matches button count should same as it was before applying fliters")]
        public void ThenViewMatchesButtonCountShouldSameAsItWasBeforeApplyingFliters()
        {
            Assert.AreEqual(_flightFilters.GetViewMatchesCount(), ViewMatchesCount, "View matches count is not same");
        }

        [Then(@"I want to see flight results as per selected departure airport (.*)")]
        public void ThenIWantToSeeFlightResultsAsPerSelectedDepartureAirportFilter(string filter)
        {
            Assert.AreEqual(ViewMatchesCount, _flightSearchResults.GetFilteredResultsCount(), "Flitered count not macthing in flight SRP");
            List<FlightInboundOutboudInformationModel> flightInfo = _flightSearchResults.CaptureAllFlightResults();
            foreach (var flight in flightInfo)
            {
                Assert.IsTrue(flight.flightLeg[0].DepartureLocation.Equals(filter),"Flight results not matching the departure airport");
            }          
        }

        [Then(@"I should see available number of stops")]
        public void ThenIShouldSeeAvailableNumberOfStops()
        {
            Assert.IsTrue(_flightFilters.IsNumberOfStopsDisplayed(), "No of filters not diplayed");
        }

        [When(@"I apply the available flight filters")]
        [When(@"all available filters are selected")]
        public void WhenIApplyTheAvailableFlightFilters()
        {
            ViewMatchesCount = _flightFilters.GetViewMatchesCount();
            _flightFilters.AddAvailableFilters();
        }

        [Then(@"all selected filters are cleared")]
        public void ThenAllSelectedFiltersAreCleared()
        {
            Assert.IsTrue(_flightFilters.IsAllAvailableFiltersReset(), "Available filters are not reset");
        }

        [Then(@"Departure airport filter should not be displayed")]
        public void ThenDepartureAirportFilterShouldNotBeDisplayed()
        {
            Assert.IsFalse(_flightFilters.IsDepartureAirportFilterDisplayed(), "Departure airport is displayed");
        }

        [Given(@"Departure airport on flights srp is having only (.*)")]
        public void WhenDepartureAirportOnFlightsSrpIsHavingOnlyLondonGatwick(string depAirport)
        {
            Assert.IsTrue(_flightSearchResults.ValidateDepartureAirport(depAirport), "Flights SRP does not match departure airport " + depAirport);
        }

        [Then(@"Title should be Filter Results")]
        public void ThenTitleShouldBeFilterResults()
        {
            Assert.AreEqual("Filter Results", _filtersModal.GetFilterModelHeading(), "Filter Modal title mismatch");
        }

        [When(@"Filter by Total Price")]
        public void WhenFilterByTotalPrice()
        {
            decimal price = _priceFilter.GetUpToPrice();
            int initiaSearchResultCount = GetHotelSearchResultsCount();                       
            Console.WriteLine("Initial Price : " + price);
            Console.WriteLine("Initial Search Result Count : " + initiaSearchResultCount);
            int filteredViewMatchesCount = 0;
            do
            {
                _priceFilter.SetPriceFilter(-200, 0);
                filterPrice = _priceFilter.GetUpToPrice();
                filteredViewMatchesCount = GetFilteredMatchesCount();
            } while (price == filterPrice && initiaSearchResultCount == filteredViewMatchesCount);
            Console.WriteLine("Filtered Price" + filterPrice);
            Assert.Greater(price, filterPrice, "Actual price is less than filter price");
            Assert.Greater(initiaSearchResultCount, filteredViewMatchesCount, "Actual price is less than filter price");
            Console.WriteLine("Filter Search Result Count : " + filteredViewMatchesCount);
            SelectViewFilterMatches();
        }
        
        [Then(@"Validate by total price filter")]
        public void WhenValidateByTotalPriceFilter()
        {
           _hotelSearchResults.LoadMoreResults(2);
           Assert.IsTrue(_hotelSearchResults.ValidatePriceFilter(filterPrice), "Hotel price filter validation");
        }

        [Then(@"Check the filtered count in hotel search results")]
        public void WhenCheckTheFilteredCountInHotelSearchResults()
        {
            if(!HelperFunctions.IsDesktop())
            Assert.AreEqual(_filtersModal.GetStoredViewMatchesCount(), _hotelSearchResults.GetHotelSearchResultsCount(), "Filtered count in model and search results in not matching");
        }

        [Then (@"Filter by available Star Ratings and validate it")]
        public void WhenFilterByStarRating()
        {
            string setFilter;
            // Reset the filters
            _filtersModal.ResetFilter();
            for (int counter = 1; counter <= _filtersModal.GetAvailableStarRatingOptions(); counter++)
            {
                // Get the inital filters results count
                int initialSearchCount = GetHotelSearchResultsCount();               
                Console.WriteLine("Initial Search Result Count : " + initialSearchCount);
                // Apply the filter
                setFilter = _filtersModal.SetStarRatingFilter(counter);
                Console.WriteLine("Number of Hotels with star rating {0}", setFilter);               
                // Check that View Matches is shown , means that it has search items
                if (IsHotelResultsAvaialble())
                {
                    int filteredCout = 0;
                    // Get the filtered result count                    
                    filteredCout = GetFilteredMatchesCount();                   
                    Console.WriteLine("Search Result Count : " + filteredCout);
                    // Click View filters
                    SelectViewFilterMatches();
                    // Compare the filtered count on hotel search results
                    Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");
                    // Validate the filters
                    _hotelSearchResults.ValidateStarRating(setFilter);
                    //Validate Filterd Count and Count Match with Initial by reset filter
                    ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
                }
                else
                {
                    Console.WriteLine("No Hotels with star rating {0}", setFilter);
                    Assert.Fail("No Hotels with star rating {0}", setFilter);
                }
            }
        }

        [Then(@"Filter by available Customer Ratings and validate it")]
        public void WhenFilterByAvailbleCustomerRatingsAndValidateIt()
        {
            //string setFilter;
            // Reset the filters
            _filtersModal.ResetFilter();

                // Get the inital filters results count
                int initialSearchCount = GetHotelSearchResultsCount();                
                //Console.WriteLine("Customer rating Filter {0} - {1}", (2 * counter) - 1, (2 * counter));
                Console.WriteLine("Initial Search Result Count : " + initialSearchCount);

                // Apply the filter
                //setFilter = _filtersModal.SetCustomerRatingFilter(counter);
                _filtersModal.IncreaseCutomerRating(100,0);

                // Check that View Matches is shown , means that it has search items               
                if (IsHotelResultsAvaialble())
                {
                    // Get the filtered result count
                    int filteredCout = GetFilteredMatchesCount();                                 
                    Console.WriteLine("Search Result Count : " + filteredCout);

                    // Click View filters
                    SelectViewFilterMatches();
                   
                    // Initial and filtered searh results are not same , then compare with the filtered count on search results
                    if (initialSearchCount != filteredCout)
                        // Compare the filtered count on hotel search results
                        Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");
                    else
                        Console.WriteLine("Filtered results matched with initial count");

                      //Validate Filterd Count and Count Match with Initial by reset filter
                        ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
                }
                else
                {
                    // Reset the filters
                    _filtersModal.ResetFilter();
                    //Console.WriteLine("No Hotels with customer rating {0} - {1}", (2*counter)-1, (2 * counter));
                }           
        }

        [Then(@"Filter by available Board Type and validate it")]
        public void WhenFilterByAvailbleBoardTypeAndValidateIt()
        {
            // Get All board types
            List<string> filterBoardTypes = _filtersModal.GetFilterBoardTypes();            
            // Reset the filters
            _filtersModal.ResetFilter();
            foreach (var boardOption in filterBoardTypes)
            {
                // Get the inital filters results count
                // Get Initial search count
                int initialSearchCount = GetHotelSearchResultsCount();                
                Console.WriteLine("Filter Board Type by : " + boardOption);
                Console.WriteLine("Initial Search Result Count : " + initialSearchCount);

                // Apply the filter
                _filtersModal.SetBoardTypeFilter(boardOption);

                // Check that View Matches is shown , means that it has search items                
                if (IsHotelResultsAvaialble())
                {
                    // Get the filtered result count
                    int filteredCout = GetFilteredMatchesCount();                   
                    Console.WriteLine("Search Result Count : " + filteredCout);

                    // Click View filters
                    SelectViewFilterMatches();

                    // Validate the filters
                    _hotelSearchResults.ValidateBoardType(boardOption);

                    // Initial and filtered searh results are not same , then compare with the filtered count on search results
                    if (initialSearchCount != filteredCout)
                        // Compare the filtered count on hotel search results
                        Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");

                    //Validate Filterd Count and Count Match with Initial by reset filter
                    ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
                }
                else
                {
                    Assert.Fail("No search results for the board type {0} ", boardOption);
                }
            }
        }

        [Then(@"Filter by multiple available Board Type and validate it")]
        public void WhenFilterByMultipleAvailableBoardTypeAndValidateIt()
        {
            // Get All board types
            List<string> filterBoardTypes = _filtersModal.GetFilterBoardTypes();          

            // Reset filters 
            _filtersModal.ResetFilter();
            foreach (var filterdBoardType in filterBoardTypes)
            {
                // Get Initial search count
                int initialSearchCount = GetHotelSearchResultsCount();                
                Console.WriteLine("Initial Search Result Count : " + initialSearchCount);

                //Apply The boardType
                _filtersModal.SetBoardTypeFilter(filterdBoardType);

                // Check that View Matches is shown , means that it has search items                
                if (IsHotelResultsAvaialble())
                {
                    // Get the filtered count
                    int filteredCout = GetFilteredMatchesCount();                    
                    Console.WriteLine("Search Result Count : " + filteredCout);

                    // Click View Matches button
                    if (!HelperFunctions.IsDesktop())
                        _filtersModal.ViewFilterMatches();

                    // Initial and filtered searh results are not same , then compare with the filtered count on search results
                    if (initialSearchCount != filteredCout)
                        // Compare the filtered count on hotel search results
                        Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");
                    else
                        Console.WriteLine("Filtered results matched with initial count");

                    // Validate the filter
                    //Assert.IsTrue(_hotelSearchResults.ValidateBoardType(filteredBoardTypes), "BoarTypes are not Matched ");

                    // Select the first hotel
                    _hotelSearchResults.SelectHotel(1);

                    // Validate property amenities in estab page 
                    Assert.IsTrue(_hotelEstabPage.ValidateBoardType(filterdBoardType),"Filtered BoardType not avaialble in Estab Room ");

                    // Navigate back
                    _hotelEstabPage.NavigateBack();

                    //Validate Filterd Count and Count Match with Initial by reset filter
                    ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
                }
                else
                {
                    Assert.Fail("No Search Results found for multiple Board Type filter");
                }
            }                       
        }

        [Then(@"Filter by property amenities and validate it")]
        public void WhenFilterByPropertyAmenitiesAndValidateIt()
        {
            List<string> availalePropertyAminities = _filtersModal.GetAvailablePropertyAmenities();

            // Reset filters
            _filtersModal.ResetFilter();
            foreach (var propertyAmenity in availalePropertyAminities)
            {               
                // Get the initial filter ount
                int initialSearchCount = GetHotelSearchResultsCount();                
                Console.WriteLine("Initial Search Result Count : " + initialSearchCount);
                // Apply filter
                _filtersModal.SetPropertyAmenities(propertyAmenity);
                // Check that View Matches is shown , means that it has search items                
                if (IsHotelResultsAvaialble())
                {
                    // Get the filtered count
                    int filteredCout = GetFilteredMatchesCount();
                    Console.WriteLine("Filtered Search Result Count : " + filteredCout);

                    // Click view matches 
                    if (!HelperFunctions.IsDesktop())
                        _filtersModal.ViewFilterMatches();

                    // Initial and filtered searh results are not same , then compare with the filtered count on search results
                    if (initialSearchCount != filteredCout)
                        // Compare the filtered count on hotel search results
                        Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");
                    else
                        Console.WriteLine("Filtered results matched with initial count");

                    // Select the first hotel
                    _hotelSearchResults.SelectHotel(1);

                    // Validate property amenities in estab page 
                    _hotelEstabPage.ValidatePropertyAmenities(propertyAmenity);

                    // Navigate back
                    _hotelEstabPage.NavigateBack();

                    //Validate Filterd Count and Count Match with Initial by reset filter
                    ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
                }
                else
                {
                    Assert.Fail("No Search Results found for multiple Property Amenity filter");
                }
            }
        }

        [Then(@"Filter by multiple property amenities and validate it")]
        public void WhenFilterByMultiplePropertyAmenitiesAndValidateIt()
        {
            // Get all property Amenities
            List<string> availalePropertyAmenities = _filtersModal.GetAvailablePropertyAmenities();
            List<string> selectedPropertyAmenities = new List<string>();
            int propertyAmenitySelectCounter = 0;

            // Reset filters 
            _filtersModal.ResetFilter();

            // Get initial count
            int initialSearchCount = GetHotelSearchResultsCount();            
            Console.WriteLine("Initial Search Result Count : " + initialSearchCount);

            // Select half of the Property Amenities types
            while (propertyAmenitySelectCounter <= availalePropertyAmenities.Count / 2)
            {
                selectedPropertyAmenities.Add(availalePropertyAmenities[propertyAmenitySelectCounter]);
                _filtersModal.SetPropertyAmenities(selectedPropertyAmenities[propertyAmenitySelectCounter]);
                propertyAmenitySelectCounter = propertyAmenitySelectCounter + 1;
            }

            // Check that View Matches is shown , means that it has search items            
            if (IsHotelResultsAvaialble())
            {
                // Get the filtered count
                int filteredCout = GetFilteredMatchesCount();                
                Console.WriteLine("Filtered Search Result Count : " + filteredCout);

                // Click view matches
                SelectViewFilterMatches();

                // Initial and filtered searh results are not same , then compare with the filtered count on search results
                if (initialSearchCount != filteredCout)
                    // Compare the filtered count on hotel search results
                    Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");
                else
                    Console.WriteLine("Filtered results matched with initial count");

                // Select the first hotel
                _hotelSearchResults.SelectHotel(1);

                // Validate property amenities in estab page 
                _hotelEstabPage.ValidatePropertyAmenities(selectedPropertyAmenities);

                // Navigate back
                _hotelEstabPage.NavigateBack();

                //Validate Filterd Count and Count Match with Initial by reset filter
                ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
            }
            else
            {
                Assert.Fail("No Search Results found for multiple Property Amenity filter");
            }
        }

        [Then(@"Filter by holiday type and validate it")]
        [Then(@"Validate Filter by holiday type")]
        public void WhenValidateFilterbyholidaytype()
        {
            // Get all holiday types
            List<string> availableHolidayTypes = _filtersModal.GetAvailableHolidayTypeNames();
            List<string> selectedHolidayTypes = new List<string>();
            int holidayTypeSeletCounter = 0;

            // Reset filters 
            _filtersModal.ResetFilter();

            // Get initial count
            int initialSearchCount = GetHotelSearchResultsCount();            
            Console.WriteLine("Initial Search Result Count : " + initialSearchCount);

            // Select few of the Holiday Amenities types
            while (holidayTypeSeletCounter <= availableHolidayTypes.Count / 3)
            {
                selectedHolidayTypes.Add(availableHolidayTypes[holidayTypeSeletCounter]);
                _filtersModal.SetHolidayTypes(selectedHolidayTypes[holidayTypeSeletCounter]);
                holidayTypeSeletCounter = holidayTypeSeletCounter + 1;
            }

            // Check for the Results Avialbility after filter applied             
            if (IsHotelResultsAvaialble())
            {
                // Get the filtered count
                int filteredCout = GetFilteredMatchesCount();                
                Console.WriteLine("Filtered Search Result Count : " + filteredCout);

                // Click view matches 
                SelectViewFilterMatches();

                // Initial and filtered searh results are not same , then compare with the filtered count on search results
                if (initialSearchCount != filteredCout)
                    // Compare the filtered count on hotel search results
                    Assert.AreEqual(filteredCout, _hotelSearchResults.GetHotelSearchResultsCount(), "View matches count didnt match to filtered count on hotel search results");
                else
                    Console.WriteLine("Filtered results matched with initial count");

                //Validate Filterd Count and Count Match with Initial by reset filter
                ValidateFilteredCountAndResetFilter(filteredCout, initialSearchCount);
            }
            else
            {
                Assert.Fail("No Search Results found for multiple Property Amenity filter");
            }
        }

        [When(@"Apply outbound departure time filter")]
        public void WhenApplyOutboundDepartureTimeFilter()
        {
            DateTime preSelectedOutboundFlightDepartureTime = DateTime.ParseExact(_flightSearchResults.GetFlightInformation().flightLeg[(int)FlightLegType.Outbound].DepartureTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime outBoundMaxDepartureTime = DateTime.ParseExact(_filterSlider.GetOutBoundDepartureTimeValues().Split("-\r\n")[1], "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime outBoundMinDepartureTime = DateTime.ParseExact(_filterSlider.GetOutBoundDepartureTimeValues().Split("\r\n-")[0], "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime filteredTime;
            if (preSelectedOutboundFlightDepartureTime == outBoundMinDepartureTime || preSelectedOutboundFlightDepartureTime.CompareTo(DateTime.ParseExact("12:00", "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) > 0)
            {
                do
                {
                    _filterSlider.SetOutBoundDepartureTimeMinFilter(20, 0);
                    filteredTime = DateTime.ParseExact(_filterSlider.GetOutBoundDepartureTimeValues().Split("\r\n-")[0], "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                } while (preSelectedOutboundFlightDepartureTime.CompareTo(filteredTime) > 0);
            }

            else if(preSelectedOutboundFlightDepartureTime == outBoundMaxDepartureTime || preSelectedOutboundFlightDepartureTime.CompareTo(DateTime.ParseExact("12:00", "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) < 0)
            {
                do
                {
                    _filterSlider.SetOutBoundDepartureTimeMaxFilter(-20, 0);
                    filteredTime = DateTime.ParseExact(_filterSlider.GetOutBoundDepartureTimeValues().Split("-\r\n")[1], "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                } while (preSelectedOutboundFlightDepartureTime.CompareTo(filteredTime) < 0);
            }
            _filtersModal.ViewFilterMatches();
        }

        [Then(@"Verify Outbound departure times filter")]
        public void ThenVerifyOutboundDepartureTimesFilter()
        {
            DateTime filteredMinTime,departureTime, filteredMaxTime;
            _filtersModal.ResetFilter();
            int intialSearchCount = _filtersModal.GetViewMatchesResultCount();

            //Set Outbound ans return departure time filter
            _filterSlider.SetOutBoundDepartureTimeMinFilter(20, 0);
            _filterSlider.SetOutBoundDepartureTimeMaxFilter(-20, 0);            

            //Capture filterd Departure Min and Max Time and count
            filteredMinTime = Convert.ToDateTime(_filterSlider.GetOutBoundDepartureTimeValues().Split("-")[0]);
            filteredMaxTime = Convert.ToDateTime(_filterSlider.GetOutBoundDepartureTimeValues().Split("-")[1]);

            int filteredCount = _filtersModal.GetViewMatchesResultCount();
            Console.WriteLine("Intial Search Count :" + intialSearchCount + " - " + "Filtered Count : " + filteredCount);
            _flightFilters.CloseFlightsFliter();

            int additionalFlightsResultscount = _flightSearchResults.GetAdditionalFlightResultsCount();
            for (int i = 1; i <= additionalFlightsResultscount; i++)
            {
                departureTime= Convert.ToDateTime(_flightSearchResults.GetDeaprtureAndArrivalTimefromFlightLegInfo(i, FlightLegType.Outbound).DepartureTime);
                if (departureTime.TimeOfDay <= filteredMinTime.TimeOfDay &&departureTime.TimeOfDay>=filteredMaxTime.TimeOfDay)
                    Assert.Fail("Filtered Outbound Departure time is not matched in Additional Flight results");
            }
        }


        [Then(@"Verify Return departure times filter")]
        public void ThenVerifyReturnDepartureTimesFilter()
        {
            DateTime filteredMinTime, departureTime, filteredMaxTime;
            _filtersModal.ResetFilter();
            int intialSearchCount = _filtersModal.GetViewMatchesResultCount();

            //Set Outbound departure time filter
            _filterSlider.SetReturnDepartureTimeMinFilter(1, 0);
            _filterSlider.SetReturnDepartureTimeMaxFilter(-1,0);

            //Capture filterdDepartureTime and count
            filteredMinTime = Convert.ToDateTime(_filterSlider.GetReturnDepartureTimeValues().Split("-")[0]);
            filteredMaxTime = Convert.ToDateTime(_filterSlider.GetReturnDepartureTimeValues().Split("-")[1]);

            int filteredCount = _filtersModal.GetViewMatchesResultCount();
            Console.WriteLine("Intial Search Count :" + intialSearchCount + " - " + "Filtered Count : " + filteredCount);
            _flightFilters.CloseFlightsFliter();

            int additionalFlightsResultscount = _flightSearchResults.GetAdditionalFlightResultsCount();
            for (int i = 1; i <= additionalFlightsResultscount; i++)
            {                
                departureTime = Convert.ToDateTime(_flightSearchResults.GetDeaprtureAndArrivalTimefromFlightLegInfo(i, FlightLegType.Inbound).DepartureTime);
                if (departureTime.TimeOfDay <= filteredMinTime.TimeOfDay && departureTime.TimeOfDay >= filteredMaxTime.TimeOfDay)
                    Assert.Fail("Filtered Outbound Departure time is not matched in Additional Flight results");
            }
        }

        [When(@"I click on customer rating filter icon")]
        public void WhenIClickOnCustomerRatingFilterIcon()
        {
            //_filtersModal.SetCustomerRatingFilter(1);
            _filtersModal.DecreaseCutomerRating(-200, 0);
            _filtersModal.IncreaseCutomerRating(50, 0);
            Assert.Zero(_filtersModal.GetViewMatchesResultCount(), "view match count is not zero");
        }

        [Then(@"View matches button should be disabled")]
        public void ThenViewMatchesButtonShouldBeDisabled()
        {
            Assert.IsFalse(_filtersModal.IsViewMatchesButtonEnabled(), "View matches button is not disabled");
        }

        [Then(@"Filter options are updated based on applied filter")]
        public void ThenFilterOptionsAreUpdatedBasedOnAppliedFilter()
        {
            Assert.IsTrue(_filtersModal.IsStarRatingOptionsDisabled(),"star rating filter is disabled");
        }


        public void SelectViewFilterMatches()
        {
            if (!HelperFunctions.IsDesktop())
                _filtersModal.ViewFilterMatches();
        }
        public int GetFilteredMatchesCount()
        {
            return GetHotelSearchResultsCount();
        }


        public int GetHotelSearchResultsCount()
        {
            int initiaSearchResultCount = 0;
            if (HelperFunctions.IsDesktop())
                initiaSearchResultCount = _hotelSearchResults.GetHotelSearchResultsCount();
            else
                initiaSearchResultCount = _filtersModal.GetViewMatchesResultCount();
            return initiaSearchResultCount;
        }

        public bool IsHotelResultsAvaialble()
        {
            bool IsResultsAvailable = false;
            if (HelperFunctions.IsDesktop())
                IsResultsAvailable = _hotelSearchResults.GetHotelSearchResultsCount() > 0;
            else
                IsResultsAvailable = _filtersModal.IsViewMatchesButtonVisible();
            return IsResultsAvailable;
        }

        public void ValidateFilteredCountAndResetFilter(int filteredCout, int initialSearchCount) 
        {
            // Open the Filter model
            if (!HelperFunctions.IsDesktop())
            {
                _hotelSearchResults.SelectFilters();
                // Check the filtered count is retained
                Assert.AreEqual(filteredCout, _filtersModal.GetViewMatchesResultCount(), "View matches count is retained after opening the filter back");
            }            
            // Reset the filters
            _filtersModal.ResetFilter();
            // Check the filter count mathes the initial search count
            Assert.AreEqual(initialSearchCount, GetHotelSearchResultsCount(), "Initial Search Count is retained after opening the filter back");
        }
    }
}


