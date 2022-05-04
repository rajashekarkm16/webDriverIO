using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.Reporting.CustomReporter;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.TravelRepublic.MobileWeb.UI.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class EstabPageSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly ICalendarComponent _calendarComponent;
        private readonly ISearchComponent _searchComponent;
        private readonly IFlightDetailsPage _flightDetailsPage;
        private readonly IGuestComponent _guestComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private readonly ITransfers _transfers;
        private readonly IRoomSelectionSummary _roomSelectionSummary;

        public EstabPageSteps(IHomePage homePage, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, IBookingSummary bookingSummary, ISearchSummaryComponent searchSummaryComponent, ICalendarComponent calendarComponent, ISearchComponent searchComponent, IFlightDetailsPage flightDetailsPage, IGuestComponent guestComponent, ITransfers transfers, ILandingPageGuestComponent landingPageGuestComponent, IRoomSelectionSummary roomSelectionSummary, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _bookingSummary = bookingSummary;
            _searchSummaryComponent = searchSummaryComponent;
            _calendarComponent = calendarComponent;
            _searchComponent = searchComponent;
            _flightDetailsPage = flightDetailsPage;
            _guestComponent = guestComponent;
            _transfers = transfers;
            _roomSelectionSummary = roomSelectionSummary;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        [Given(@"I am on hotel search results page")]
        public void GivenIAmOnHotelSearchResultsPage()
        {
            _homePage.SearchHotels();
            _hotelSearchResults.GetHotelToSelect();
        }

        [Given(@"I perform a hotel search for (.*)")]
        public void GivenIPerformAHotelSearchFor(string destination)
        {
            _homePage.SearchHotels(destination);
            context.Add("Destination", destination);
        }

        [Given(@"I am on hotel estab page")]
        public void GivenIAmOnHotelEstabPage()
        {
            _homePage.SearchHotels();
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
        }

        [Then(@"Non refundable option is displayed")]
        public void ThenNonRefundableOptionIsDisplayed()
        {
            Assert.IsTrue(_hotelEstabPage.IsNonRefundable(), "Non refundable is not available for the hotel!");
        }

        [Then(@"Clicking the message opens non refundable message dialog modal")]
        public void ThenClickingTheMessageOpensNonRefundableMessageDialogModal()
        {
            _hotelEstabPage.ClickOnNonRefundableMessageLink();
            Assert.AreEqual(Constants.NonRefundableHeader, _hotelSearchResults.GetDialogHeader(), "Pop up header verification has failed.");
            Assert.AreEqual(Constants.NonRefundableMessage, _hotelSearchResults.GetDialogContent(), "Pop up message verification has failed.");
            _hotelSearchResults.CloseDialogContentPopUp();
        }

        [When(@"I select a random hotel from the results")]
        [When(@"I store the information of the hotel to be selected from the results")]
        [When(@"Select a random hotel from the results")]
        public void WhenISelectAHotelFromTheResults()
        {
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
        }

        [When(@"Select the first hotel from the results")]
        public void WhenSelectTheFirstHotelFromTheResults()
        {
            _hotelSearchResults.CaptureAndReturnHotelInformation(1);
            _hotelSearchResults.SelectHotel(1);
        }

        [When(@"Select (.*) from the search results")]
        public void WhenSelectHotelNameFromTheSearchResults(string hotelNameToSelect)
        {
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect(hotelNameToSelect));
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
        }

        [When(@"I select the distinct room types")]
        public void WhenISelectTheDistinctRoomTypes()
        {
            _hotelEstabPage.SelectDistinctRoomsTypesOnMobile(2);
        }

        [Then(@"I should see the selected hotel estab page with rooms")]
        [Then(@"Hotel information on estab page should match with search results page")]
        [Then(@"Hotel information should match as per the selected recommender card")]
        public void ThenIShouldSeeTheSelectedHotelEstabPageWithVariousRoomTypes()
        {
            var info = _hotelSearchResults.GetHotelInformation();
            Assert.AreEqual(info.HotelName, _hotelEstabPage.GetHotelName());
            if (!info.Location.Equals(_hotelEstabPage.GetHotelLocation()))
                Assert.IsTrue(_hotelEstabPage.GetHotelLocation().Contains(_homePage.GetDestination(), StringComparison.OrdinalIgnoreCase), "Location validation!");
            Assert.AreEqual(info.Rating, _hotelEstabPage.GetHotelRating());
            Assert.AreEqual(info.StarRating, _hotelEstabPage.GetHotelStarRating());
            Assert.LessOrEqual(_hotelEstabPage.GetHotelPrice(), info.Price);           
        }

        [Then(@"Flight details pop-up information on estab page should match with search results page")]
        public void ThenFlightDetailsPop_UpInformationOnEstabPageShouldMatchWithSearchResultsPage()
        {
            _hotelEstabPage.ViewFlightDetails();
            FlightInformation SrPageFlightInfo = new FlightInformation();
            SrPageFlightInfo = _flightDetailsPage.GetFlightDetails();

            FlightInformation EstabPageFlightInfo = new FlightInformation();
            EstabPageFlightInfo = _flightDetailsPage.CaptureAndReturnFlightDetails();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(SrPageFlightInfo.flightOutBoundDetails.Date, EstabPageFlightInfo.flightOutBoundDetails.Date);
                Assert.AreEqual(SrPageFlightInfo.flightOutBoundDetails.Duration, EstabPageFlightInfo.flightOutBoundDetails.Duration);
                Assert.AreEqual(SrPageFlightInfo.flightOutBoundDetails.NoOfStops, EstabPageFlightInfo.flightOutBoundDetails.NoOfStops);
                List<FlightLegModel> outboundFlightLegsInSrp = SrPageFlightInfo.flightOutBoundDetails.flightLeg;
                List<FlightLegModel> outboundFlightLegsInEstab = EstabPageFlightInfo.flightOutBoundDetails.flightLeg;
                _flightDetailsPage.CompareFlightLeg(outboundFlightLegsInSrp, outboundFlightLegsInEstab);
                Assert.AreEqual(SrPageFlightInfo.flightInBoundDetails.Date, EstabPageFlightInfo.flightInBoundDetails.Date);
                Assert.AreEqual(SrPageFlightInfo.flightInBoundDetails.Duration, EstabPageFlightInfo.flightInBoundDetails.Duration);
                Assert.AreEqual(SrPageFlightInfo.flightInBoundDetails.NoOfStops, EstabPageFlightInfo.flightInBoundDetails.NoOfStops);
                List<FlightLegModel> returnFlightLegsInSrp = SrPageFlightInfo.flightInBoundDetails.flightLeg;
                List<FlightLegModel> returnFlightLegsInEstab = EstabPageFlightInfo.flightInBoundDetails.flightLeg;
                _flightDetailsPage.CompareFlightLeg(returnFlightLegsInSrp, returnFlightLegsInEstab);
            });

            _flightDetailsPage.CloseModal();
        }

        [Then(@"I should see the selected hotel estab page")]
        public void ThenIShouldSeeTheSelectedHotelEstabPage()
        {
            var info = _hotelSearchResults.GetHotelInformation();
            Assert.AreEqual(info.HotelName, _hotelEstabPage.GetHotelName());
            Assert.AreEqual(info.Location, _hotelEstabPage.GetHotelLocation());
        }

        [When(@"Select random rooms and board type")]
        [When(@"I select random rooms and board type")]
        public void WhenSelectRandomRoomsAndBoardType()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
            _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
            _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
        }

        [When(@"Select room and board (.*)")]
        public void WhenSelectRoomAndBoard(string board)
        {
            if (HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count, board);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count, board);
        }

        [When(@"Select new search")]
        public void WhenSelectNewSearch()
        {
            _searchComponent.SelectNewSearch();
        }

        [Then(@"I am redirected to home page")]
        public void ThenIAmRedirectedToHomePage()
        {
            _homePage.VerifyHomePage();
        }

        [When(@"Toggle to per person price")]
        [When(@"I select per person price Toggle")]
        public void WhenToggleToPerPersonPrice()
        {
            _hotelEstabPage.SelectPerPersonPriceToggle();
            int roomCount = _hotelEstabPage.GetRoomTypeCount();
            for (int i = 1; i <= roomCount; i++)
            {
                if (HelperFunctions.IsDesktop())
                    context.Add(string.Format("perPersonPrice{0}", i.ToString()), _hotelEstabPage.GetDesktopBoardTypeInformation(1, i).BoardTypePrice);
                else
                    context.Add(string.Format("perPersonPrice{0}",i.ToString()), _hotelEstabPage.GetRoomInformation(i).RoomPrice);              
            }
        }

        [Then(@"Total price should match accordingly")]
        public void ThenTotalPriceShouldMatchAccordingly()
        {
            decimal totalRoomPrice = 0;
            decimal perPersonRoomPrice = 0;
            _hotelEstabPage.SelectTotalPriceToggle();
            int roomCount = _hotelEstabPage.GetRoomTypeCount();
            for (int i = 1; i <= roomCount; i++)
            {
                if (HelperFunctions.IsDesktop())
                    totalRoomPrice = _hotelEstabPage.GetDesktopBoardTypeInformation(1, i).BoardTypePrice;
                else
                    totalRoomPrice = _hotelEstabPage.GetRoomInformation(i).RoomPrice;
                perPersonRoomPrice = Convert.ToDecimal(context[string.Format("perPersonPrice{0}", i.ToString())]);
                Assert.GreaterOrEqual(Constants.MaximumPriceDifference, (totalRoomPrice - (perPersonRoomPrice * 2)), "Price toggle swicth validation");
                Assert.GreaterOrEqual(Constants.MaximumPriceDifference, (perPersonRoomPrice * 2) - totalRoomPrice, "Price toggle swicth validation");
                Console.WriteLine(string.Format("PerPerson: {0} Total: {1}", perPersonRoomPrice.ToString(), totalRoomPrice.ToString()));
            }
        }

        [Then(@"I can see the local charges information link displayed in hotel estab page")]
        public void ThenICanSeeTheLocalChargesInformationLinkDisplayedInHotelEstabPage()
        {
            context.Add("LocalTaxInEstabPage", Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(_hotelEstabPage.GetLocalTaxes())));
            Console.WriteLine("LocalTaxInEstabPage: " + context["LocalTaxInEstabPage"] as string);
            _hotelEstabPage.CloseLocalTaxPopup();
        }

        [Then(@"Local tax is displayed in board type selection page")]
        public void ThenLocalTaxIsDisplayedInBoardTypeSelectionPage()
        {
            List<BoardTypeInformation> lBoardTypeInfo = _hotelEstabPage.GetSelectedBoardTypes();
            if (!HelperFunctions.IsDesktop())
            {
                decimal localTax = 0;
                foreach (var list in lBoardTypeInfo)
                {
                    Assert.IsTrue(list.LocalTax > 0);
                    localTax = localTax + list.LocalTax;
                }
                context.Add("LocalTaxInBoardType", localTax);
                Console.WriteLine("LocalTaxInBoardType: " + localTax.ToString());
            }            
        }

        [Then(@"Total price is the default price selected")]
        public void ThenTotalPriceIsTheDefaultPriceSelected()
        {
            Assert.IsTrue(_hotelEstabPage.VerifyDefaultSelectedPrice());
        }

        [Then(@"Background color of selected price is blue")]
        public void ThenBackgroundColorOfSelectedPriceIsBlue()
        {
            Assert.Ignore(_hotelEstabPage.GetTotalPriceToggleSwitchBackgorundColor(), "#009DD9");
            Assert.Ignore(_hotelEstabPage.GetPerPersonPriceToggleSwitchBackgorundColor(), "#FFF");
        }

        [Then(@"Toggle to per person price updates the color")]
        public void ThenToggleToPerPersonPriceUpdatesTheColor()
        {
            Assert.Ignore(_hotelEstabPage.GetPerPersonPriceToggleSwitchBackgorundColor(), "#009DD9");
            Assert.Ignore(_hotelEstabPage.GetTotalPriceToggleSwitchBackgorundColor(), "#FFF");
        }

        [Then(@"Holiday price on map matches with the price on Estab page")]
         public void ThenHolidayPriceOnMapMatchesWithThePriceOnEstabPage()
        {
            HotelInformation info = context["HotelInformationInMaps"] as HotelInformation;
            Assert.AreEqual(info.HotelName, _hotelEstabPage.GetHotelName());
            Assert.IsTrue(_hotelEstabPage.GetHotelLocation().Contains(info.Location));
            Assert.LessOrEqual(_hotelEstabPage.GetHotelPrice(), info.Price);
        }

        [Then(@"Overview tab is selected by default")]
        public void ThenOverviewTabIsSelectedByDefault()
        {
            _hotelEstabPage.VerifyOverviewTab();
        }

        [Then(@"Hotel information section is displayed")]
        public void ThenHotelInformationSectionIsDisplayed()
        {
            _hotelEstabPage.VerifyHotelInformationSection();
        }

        [Then(@"Hotel information toggle switch loads content")]
        public void ThenHotelInformationToggleSwitchLoadsContent()
        {
            _hotelEstabPage.VerifyHotelInfoToggleSwitch();
        }


        [Then(@"Facilities section is displayed")]
        public void ThenFacilitiesIconsAreDisplayed()
        {
            _hotelEstabPage.VerifyFacilitiesSection();
        }

        [Then(@"Faciltities options are displayed")]
        public void ThenFaciltitiesOptionsAreDisplayed()
        {
            List<string> facilities = _hotelEstabPage.GetFacilities();
            Console.WriteLine("Available facilities:");
            foreach (string option in facilities)
            {
                Console.WriteLine(option + "\n");
            }
        }

        [Then(@"Main price displays total price and also per person price below")]
        public void ThenMainPriceDisplaysTotalPriceAndAlsoPerPersonPriceBelow()
        {
            context.Add("TotalRoomPrice", _hotelEstabPage.GetHotelPrice());
            context.Add("PerPersonPrice", _hotelEstabPage.GetHotelPerPersonPrice());
        }


        [Then(@"Main room price should display only per person price")]
        public void ThenMainRoomPriceShouldBeUpdatedToPerPersonPrice()
        {
            Assert.IsTrue(_hotelEstabPage.GetHotelPrice().Equals(Convert.ToDecimal(context["PerPersonPrice"])));
        }

        [Then(@"Overview tab contains reviews section")]
        public void ThenOverviewTabContainsReviewsSection()
        {
            _hotelEstabPage.VerifyReviewsSectionInOverView();
        }

        [When(@"I click on reviews tab")]
        public void WhenIClickOnReviewsTab()
        {
            _hotelEstabPage.ClickReviewsTab();
        }


        [Then(@"Clicking on see all customer reviews takes user to reviews tab")]
        public void ThenClickingOnSeeAllCustomerReviewsTakesUserToReviewsTab()
        {
            _hotelEstabPage.ClickSeeAllCustomerReviews();
        }

        [Then(@"Reviews are sorted from newer to older dates")]
        public void ThenReviewsAreSortedFromNewerToOlderDates()
        {
            Assert.IsTrue(_hotelEstabPage.VerifyReviewsDisplayedInAscendingOrderByDate());
        }

        [Then(@"See more reviews button is displayed")]
        public void ThenSeeMoreReviewsButtonIsDisplayed()
        {
            context.Add("ReviewsCount", _hotelEstabPage.GetReviewsCount());
            _hotelEstabPage.ClickSeeMoreReviews();
        }

        [Then(@"More reviews are loaded on clicking see more reviews")]
        public void ThenMoreReviewsAreLoadedOnClickingSeeMoreReviews()
        {
            context.Add("UpdatedReviewsCount", _hotelEstabPage.GetReviewsCount());
            Assert.IsTrue(Convert.ToInt16(context["UpdatedReviewsCount"]) > Convert.ToInt16(context["ReviewsCount"]));
        }

        [Then(@"Moving to overview tab and back to reviews tab contains the last loaded reviews")]
        public void ThenMovingToOverviewTabAndBackToReviewsTabContainsTheLastLoadedReviews()
        {
            _hotelEstabPage.ClickOverviewTab();
            _hotelEstabPage.ClickSeeAllCustomerReviews();
            Assert.AreEqual(_hotelEstabPage.GetReviewsCount(), Convert.ToInt16(context["UpdatedReviewsCount"]));
        }

        [Then(@"See whole review link is displayed if review has more characters")]
        public void ThenSeeWholeReviewLinkIsDisplayedIfReviewHasMoreCharacters()
        {
            _hotelEstabPage.VerifySeeWholeReview();
        }

        [Then(@"No reviews should be displayed")]
        public void ThenNoReviewsShouldBeDisplayed()
        {
            _hotelEstabPage.VerifyNoReviewsMessage();
        }

        [Given(@"Select a hotel with reviews")]
        public void GivenSelectAHotelWithReviews()
        {
            int hotelToSelect = 1;
            while(hotelToSelect <= _hotelSearchResults.GetResultsCount())
            {
                if (_hotelSearchResults.GetHotelReviewsCount(hotelToSelect) > 10)
                {
                    _hotelSearchResults.SelectHotel(hotelToSelect);
                    break;
                }
                else
                    hotelToSelect++;
            }
        }

        [When(@"I select random room")]
        public void WhenISelectRandomRoom()
        {
            _hotelEstabPage.SelectRoom(HelperFunctions.RandomNumber(_hotelEstabPage.GetRoomTypeCount()));
        }

        [Then(@"I should see the board selection page")]
        public void ThenIShouldSeeTheBoardSelectionPage()
        {
            Assert.IsTrue(_hotelEstabPage.GetBoardTypeCount() > 0);
        }

        [Then(@"Holiday price should be displayed")]
        public void ThenHolidayPriceShouldBeDisplayed()
        {
            Assert.AreEqual("Holiday price from", _hotelEstabPage.GetPriceCaption());
        }

        [Then(@"Continue to Flights button should be displayed")]
        public void ThenContinueToFlightsButtonShouldBeDisplayed()
        {
            Assert.AreEqual("Confirm and Continue to Flights", _hotelEstabPage.GetContinueButtonText());
        }

        [Then(@"Hotel estab should be displayed")]
        public void ThenEstabShouldBeDisplayed()
        {
            Assert.IsTrue(_hotelEstabPage.IsEstabPageDisplayed(), "Hotel estab page is not displayed");
        }

        [When(@"I click on hotel recommender")]
        public void WhenIClickOnHotelRecommender()
        {
            _hotelSearchResults.CaptureAndReturnRecommenderHotelInformation(1);
            _hotelSearchResults.SelectHotelFromRecommender(1);
        }

        [Then(@"I should see hotel recommender displayed")]
        public void ThenIShouldSeeHotelRecommenderDisplayed()
        {
            if (!_hotelSearchResults.IsNoRoomsMessageDisplayed())
            {
                Assert.IsTrue(_hotelSearchResults.IsRecommenderDisplayed(), "Recommender is not displayed");
                if (_homePage.GetIsHolidayFlow())
                    Assert.AreEqual(_hotelSearchResults.GetRecommenderPriceCaption(), "Holiday price from");
                else
                    Assert.AreEqual(_hotelSearchResults.GetRecommenderPriceCaption(), "Total price from");
            }
            else
                Assert.Fail("No search results");            
        }

        [Then(@"Hotel recommender should not be displayed")]
        public void ThenHotelRecommenderShouldNotBeDisplayed()
        {
            Assert.IsFalse(_hotelSearchResults.IsRecommenderDisplayed(), "Recommender is displayed");           
        }

        [Then(@"Choose room button is displayed")]
        public void ThenChooseRoomButtonIsDisplayed()
        {
            Assert.IsTrue(_hotelEstabPage.IsChooseRoomsButtonDisplayed(), "Choose rooms button validation");
        }

        [When(@"I click on the choose room button")]
        public void WhenIClickOnTheChooseRoomButton()
        {
            _hotelEstabPage.ClickChooseRoomButton();
        }

        [Then(@"Rooms tab is selected by default")]
        public void ThenRoomsTabIsSelectedByDefault()
        {
            Assert.IsTrue(_hotelEstabPage.IsRoomsTabSelected(), "Rooms tab selection validation");
        }

        [Then(@"Rooms tab is scrolled to the top of the page")]
        public void ThenRoomsTabIsScrolledToTheTopOfThePage()
        {
            Assert.IsTrue(_hotelEstabPage.IsRoomsTabScrolledToTop(), "Rooms tab position validation");
        }
        [Then(@"I want to see faded content")]
        public void ThenIWantToSeeFadedContent()
        {
            Assert.IsTrue(_hotelEstabPage.IsReviewContentFaded(), "Review content is faded");
        }

        [When(@"I click on Show whole review")]
        public void WhenIClickOnShowWholeReview()
        {
            _hotelEstabPage.ClickShowWholeReviewLink();
        }

       [Then(@"Show less link should be displayed")]
        public void ThenShowLessLinkShouldBeDisplayed()
        {
           Assert.IsTrue(_hotelEstabPage.IsShowLessLinkDisplayed(), "Show less link validation");
        }

        [Then(@"See all customer reviews button is not displayed")]
        public void ThenSeeAllCustomerReviewsButtonIsNotDisplayed()
        {
            Assert.IsFalse(_hotelEstabPage.IsSeeAllReviewsDisplayed(), "See all customer reviews button is not displayed");
        }

        [Then(@"Your room options button is displayed")]
        public void ThenYourRoomOptionsButtonIsDisplayed()
        {
            Assert.IsTrue(_hotelEstabPage.IsYourRoomOptionsButtonDisplayed(), "Choose rooms button validation");
        }

        [When(@"I click on the your room options button")]
        public void WhenIClickOnTheYourRoomOptionsButton()
        {
            _hotelEstabPage.ClickChooseRoomButton();
        }

        [Then(@"Price includes section is displayed in estab page")]
        public void ThenPriceIncludesSectionIsDisplayedInEstabPage()
        {
            Assert.IsTrue(_hotelEstabPage.IsBoardTypeDisplayedInPriceIncludesSection(), "Estab page price includes section board type validation!");
            Assert.LessOrEqual(3, _hotelEstabPage.GetPriceIncludesOptionsCount(), "Options in price includes section");
        }

        [Then(@"Secure today pill is displayed in estab page")]
        public void ThenSecureTodayPillIsDisplayedInEstabPage()
        {
            Assert.IsTrue(_hotelEstabPage.IsSecureTodayPillDisplayed(), "Secure today pill validation");
        }

        [Then(@"Deposit price matches in the secure today pop up modal on estab page")]
        public void ThenDepositPriceMatchesInTheSecureTodayPopUpModalOnEstabPage()
        {
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _landingPageGuestComponent.GetRoomOccupantDetails().Count, _hotelEstabPage.GetDepositPriceFromSecurePill());
                _hotelEstabPage.ClickSecureTodayPill();
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _landingPageGuestComponent.GetRoomOccupantDetails().Count, _hotelSearchResults.GetAmountFromDepositDialogModal(), "Price amount doesnt match in pay deposit dialog box!");
            }
            else
            {
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _guestComponent.GetRoomOccupantDetails().Count, _hotelEstabPage.GetDepositPriceFromSecurePill());
                _hotelEstabPage.ClickSecureTodayPill();
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _guestComponent.GetRoomOccupantDetails().Count, _hotelSearchResults.GetAmountFromDepositDialogModal(), "Price amount doesnt match in pay deposit dialog box!");
            }
        }

        [Then(@"Free cancellation message is displayed in estab page")]
        public void ThenFreeCancellationMessageIsDisplayedInEstabPage()
        {
            if (_hotelEstabPage.IsNonRefundable())
                Assert.Warn("Hotel is Non refundable ");
            else
            Assert.IsTrue(_hotelEstabPage.IsFreeCancellationLinkDisplayed(), "Free cancellation link validation");
        }

        [Then(@"Clicking the message opens free cancellation dialog modal on estab page")]
        public void ThenClickingTheMessageOpensFreeCancellationDialogModalOnEstabPage()
        {
            _hotelEstabPage.ClickFreeCancellationMessage();
            Assert.IsTrue(_hotelSearchResults.GetDialogContent().Contains(Constants.FreeCancellationMessage), "Free cancellation message doesn't match!");
            _hotelSearchResults.CloseDialogContentPopUp();
        }

        [Then(@"Pay monthly pill is displayed on estab page")]
        public void ThenPayMonthlyPillIsDisplayedOnEstabPage()
        {
            if(HelperFunctions.IsPayMonthlyEnabledForHotels() || _hotelEstabPage.IsHolidayJourney())
                Assert.IsTrue(_hotelEstabPage.IsPayMonthlyPillDisplayed(), "Pay monthly pill is not displayed for hotel!");
            else
                Assert.Inconclusive("Paymonthly is disabled for hotels");
        }

        [Then(@"Monthly deposit price matches in the secure today pop up modal on estab page")]
        public void ThenMonthlyDepositPriceMatchesInTheSecureTodayPopUpModalOnEstabPage()
        {
            if (HelperFunctions.IsDesktop())
            {
                context.Add("PriceInPill", _hotelEstabPage.GetAmountFromPayMonthlyPill());
                _hotelEstabPage.ClickPayMonthlyPill();
                Assert.AreEqual(context["PriceInPill"], _hotelSearchResults.GetAmountFromPayMonthlyDialogModal(), "Price amount doesn't match in pay deposit dialog box!");
            }
            else
            {
                _hotelEstabPage.ClickPayMonthlyPill();
                Assert.IsTrue(_hotelSearchResults.GetAmountFromPayMonthlyDialogModal() > 0, "Monthly deposit price is not displayed in the pop up modal!");
            }
        }

        [Then(@"Return transfers message is displayed")]
        public void ThenReturnTransfersMessageIsDisplayed()
        {
            Assert.IsTrue(_hotelEstabPage.IsReturnTransfersMessageDisplayed(), "Return transfer in hotel message is not displayed");
        }

        [When(@"Select rooms tab")]
        public void WhenSelectRoomsTab()
        {
            _hotelEstabPage.MoveToRoomsTab();
        }

        [Then(@"First room tab is marked as blue during selection")]
        public void ThenFirstRoomTabIsMarkedAsBlueDuringSelection()
        {
            Assert.IsTrue(_hotelEstabPage.GetRoomNumberColor(1).Equals(Constants.Blue), "Room selection badge validation");
        }

        [Then(@"Occupancy for room (.*) is displayed")]
        public void ThenOccupancyForRoomIsDisplayed(int roomNo)
        {
            string occupants;
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                if (_landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfChildren > 0 || _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfInfants > 0)
                    occupants = string.Format("Room {0} (for {1} Adult{2}, {3} Child{4})", roomNo.ToString(),
                    _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults,
                    _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults > 1 ? "s" : "",
                    _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfChildren + _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfInfants,
                    _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfChildren + _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfInfants > 1 ? "ren" : "");
                else
                    occupants = string.Format("Room {0} (for {1} Adult{2})", roomNo.ToString(),
                    _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults,
                    _landingPageGuestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults > 1 ? "s" : "");
            }
            else
            {
                if (_guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfChildren > 0 || _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfInfants > 0)
                    occupants = string.Format("Room {0} (for {1} Adult{2}, {3} Child{4})", roomNo.ToString(),
                    _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults,
                    _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults > 1 ? "s" : "",
                    _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfChildren + _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfInfants,
                    _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfChildren + _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfInfants > 1 ? "ren" : "");
                else
                    occupants = string.Format("Room {0} (for {1} Adult{2})", roomNo.ToString(),
                    _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults,
                    _guestComponent.GetRoomOccupantDetails()[roomNo - 1].NoOfAdults > 1 ? "s" : "");
            }

            Assert.AreEqual(_hotelEstabPage.GetRoomHeaderText(), occupants, "Room occupants validation");
        }

        [Then(@"Second room is marked as grey")]
        public void ThenSecondRoomIsMarkedAsGrey()
        {
            Assert.IsTrue(_hotelEstabPage.GetRoomNumberColor(2).Equals(Constants.Grey), "Room selection badge validation");
        }

        [When(@"I select the first room")]
        public void WhenISelectTheFirstRoom()
        {
            _hotelEstabPage.SelectRoomsAndBoardTypes(1);
        }

        [Then(@"Toast message is displayed on mobile")]
        public void ThenToastMessageIsDisplayedOnMobile()
        {
            if (HelperFunctions.IsDesktop())
                Assert.Pass("Toast message not applicable in desktop!");
            else
                Assert.AreEqual(_hotelEstabPage.GetToastMessage(), "Room 1 Selected!", "Toast message validation");
        }

        [Then(@"First room tab is marked as green")]
        public void ThenFirstRoomTabIsMarkedAsGreen()
        {
            Assert.IsTrue(_hotelEstabPage.GetRoomNumberColor(1).Equals(Constants.Green), "Room selection badge validation");
        }

        [Then(@"Second room is marked as blue")]
        public void ThenSecondRoomIsMarkedAsBlue()
        {
            Assert.IsTrue(_hotelEstabPage.GetRoomNumberColor(2).Equals(Constants.Blue), "Room selection badge validation");
        }

        [When(@"I select the first room stepper")]
        public void WhenISelectTheFirstRoomStepper()
        {
            _hotelEstabPage.ClickRoomSelectionBadge(1);
        }

        [Then(@"Your room button is displayed on the sticky footer")]
        public void ThenYourRoomButtonIsDisplayedOnTheStickyFooter()
        {
            Assert.IsTrue(_hotelEstabPage.IsYourRoomStickyButtonDisplayed(), "Your room button validation");
        }

        [Then(@"Clicking on your room sticky button opens the room summary page")]
        public void ThenClickingOnYourRoomStickyButtonOpensTheRoomSummaryPage()
        {
            WhenClickOnYourRoomButtonOnStickyFooter();
            Assert.AreEqual("Room Selection", _roomSelectionSummary.GetDialogHeader(), "Header validation");
            Assert.AreEqual(_hotelEstabPage.GetSelectedRoomTypes()[0].RoomType, _roomSelectionSummary.GetRoomType(1), "Selected room type validation!");
            Assert.AreEqual(_hotelEstabPage.GetSelectedRoomTypes()[0].RoomPrice + _hotelEstabPage.GetSelectedBoardTypes()[0].BoardTypePrice, _roomSelectionSummary.GetRoomPrice(1), "Room price validation");
        }

        [When(@"Click on your room button on sticky footer")]
        public void WhenClickOnYourRoomButtonOnStickyFooter()
        {
            _hotelEstabPage.ClickYourRoomStickyButton();
        }

        [When(@"Click on your room button and select change room link for room (.*)")]
        public void WhenClickOnYourRoomButtonAndSelectChangeRoomLinkForRoom(int roomNumber)
        {
            _hotelEstabPage.ClickYourRoomStickyButton();
            _roomSelectionSummary.ClickChangeRoom(roomNumber);

        }

        [Then(@"pre selected room (.*) is highlighted and displayed with info")]
        public void ThenPreSelectedRoomIsHighlightedAndDisplayedWithInfo(int roomNo)
        {
            Assert.IsTrue(_hotelEstabPage.GetRoomNumberColor(roomNo).Equals(Constants.Blue), "Room selection badge validation");
            Assert.IsTrue(_hotelEstabPage.IsPreSelectedRoomDisplayed(), "Preselected flight card validation");
            Console.WriteLine(_hotelEstabPage.GetSelectedRoomTypes()[roomNo - 1].RoomPrice + " " + _hotelEstabPage.GetSelectedBoardTypes()[0].BoardTypePrice + " " + _hotelEstabPage.GetPreSelectedRoomPrice());
            Assert.AreEqual(_hotelEstabPage.GetSelectedRoomTypes()[roomNo-1].RoomType, _hotelEstabPage.GetPreSelectedRoomType(), "Preselected room type validation");
            Assert.AreEqual(_hotelEstabPage.GetSelectedRoomTypes()[roomNo-1].RoomPrice + _hotelEstabPage.GetSelectedBoardTypes()[0].BoardTypePrice, _hotelEstabPage.GetPreSelectedRoomPrice(), "PreSelected room price validation");
        }


        [Then(@"Local charges Payable at hotel message is displayed in estab page")]
        public void ThenLocalChargesPayableAtHotelMessageIsDisplayedInEstabPage()
        {
            Assert.IsTrue(_hotelEstabPage.IsLocalChargesMessageDisplayed(), "Local charges is displayed on hotel estab");
        }

        [Then(@"Capture local charges amount from modal pop up")]
        public void ThenCaptureLocalChargesAmountFromModalPopUp()
        {
            context.Add("LocalTaxInEstabPage", Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(_hotelEstabPage.GetLocalTaxes())));
            _hotelEstabPage.CloseLocalTaxPopup();
        }

        [Then(@"Taxes should match accordingly")]
        public void ThenTaxesShouldMatchAccordingly()
        {
            context.Add("PerPersonLocalTaxInEstabPage", Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(_hotelEstabPage.GetLocalTaxes())));
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.IsTrue((Convert.ToInt16(context["PerPersonLocalTaxInEstabPage"]) * _landingPageGuestComponent.GetNonInfantOccupants()) - Convert.ToInt16(context["LocalTaxInEstabPage"]) <= 1, "Per person and Total local taxes in the estab page both match");
            else
               Assert.IsTrue((Convert.ToInt16(context["PerPersonLocalTaxInEstabPage"]) * _guestComponent.GetNonInfantOccupants()) - Convert.ToInt16(context["LocalTaxInEstabPage"]) <= 1, "Per person and Total local taxes in the estab page both match");
        }

        [When(@"Click on change room link for room (.*)")]
        public void WhenClickOnChangeRoomLinkForRoom(int roomNo)
        {
            _roomSelectionSummary.ClickChangeRoom(roomNo);
        }

        [Then(@"Pre selected room is displayed")]
        public void ThenPreSelectedRoomIsDisplayed()
        {
            Assert.IsTrue(_hotelEstabPage.IsPreSelectedRoomDisplayed(), "Preselected flight card validation");
            Assert.AreEqual(_hotelEstabPage.GetSelectedRoomTypes()[0].RoomType, _hotelEstabPage.GetPreSelectedRoomType(), "Preselected room type validation");
            Assert.AreEqual(_hotelEstabPage.GetSelectedBoardTypes()[0].BoardType, _hotelEstabPage.GetPreSelectedBoardType(), "Preselected board type validation");
            Assert.AreEqual(_hotelEstabPage.GetSelectedRoomTypes()[0].RoomPrice + _hotelEstabPage.GetSelectedBoardTypes()[0].BoardTypePrice, _hotelEstabPage.GetPreSelectedRoomPrice(), "PreSelected room price validation");
        }

        [When(@"Close the room selection summary")]
        public void WhenCloseTheRoomSelectionSummary()
        {
            _roomSelectionSummary.CloseRoomSelectionModal();
        }

        [Then(@"Free cancellation message is displayed in board type page")]
        public void ThenFreeCancellationMessageIsDisplayedInBoardTypePage()
        {
            Assert.IsTrue(_hotelEstabPage.IsFreeCancellationMessageDisplayedInBoardType(), "Free cancellation message validation in board selection page");
        }

        [Then(@"Non refundable message is displayed in board selection page")]
        public void ThenNonRefundableMessageIsDisplayedInBoardSelectionPage()
        {
            bool isNonRefundable = false;
            for(int boardType = 1; boardType <= _hotelEstabPage.GetBoardTypeCount(); boardType++)
            {
                if (_hotelEstabPage.GetBoardTypeInformation(boardType).isNonRefundable)
                {
                    isNonRefundable = true;
                    break;
                }
            }
            Assert.IsTrue(isNonRefundable, "Non refundable message validation");
        }

        [Then(@"Secure today pill is displayed on in board type page")]
        public void ThenSecureTodayPillIsDisplayedOnInBoardTypePage()
        {
            Assert.IsTrue(_hotelEstabPage.IsSecureTodayPillDisplayedInBoardType(), "Secure today pill validation in board type page");
        }

        [Then(@"Pay monthly pill is displayed on in board type page")]
        public void ThenPayMonthlyPillIsDisplayedOnInBoardTypePage()
        {
            Assert.IsTrue(_hotelEstabPage.IsPayMonthlyPillDisplayedInBoardType(), "Secure today pill validation in board type page");
        }

        [When(@"Select a random board type filter from board type dropdown")]
        public void WhenSelectARandomBoardTypeFilterFromBoardTypeDropdown()
        {
            context.Add("Applied filter", _hotelEstabPage.SelectRandomBoardFilter());
        }

        [Then(@"Selected filter board type is only displayed")]
        public void ThenSelectedFilterBoardTypeIsOnlyDisplayed()
        {
            bool isSelectedBoardTypeSame = true;
            for (int boardType = 1; boardType <= _hotelEstabPage.GetBoardTypeCount(); boardType++)
            {
                if (!_hotelEstabPage.GetBoardTypeInformation(boardType).BoardType.Equals(context["Applied filter"]))
                    isSelectedBoardTypeSame = false;
            }
            Assert.IsTrue(isSelectedBoardTypeSame, "Board type filter validation");
        }

        [Then(@"Was now price should be displayed on estab page")]
        public void ThenWasNowPriceShouldBeDisplayedOnEstabPage()
        {
            Assert.IsTrue(_hotelEstabPage.GetHotelWasPrice().ToString()!= "", "Was Price is Displayed");
            Assert.IsTrue(_hotelEstabPage.GetHotelPrice().ToString()!= "", "Now Price is Displayed");
            Assert.IsTrue(_hotelEstabPage.GetHotelWasPrice() > _hotelEstabPage.GetHotelPrice(), "Was Price is Greater than Now Price");
            context.Add("WasPrice", _hotelEstabPage.GetHotelWasPrice());
            context.Add("NowPrice", _hotelEstabPage.GetHotelPrice());
            context.Add("Discount", _hotelEstabPage.GetHotelWasPrice() - _hotelEstabPage.GetHotelPrice());
        }

        [Then(@"Sash ribbon should be displayed on estab page")]
        public void ThenSashRibbonShouldBeDisplayedOnEstabPage()
        {
           Assert.AreEqual(_hotelEstabPage.GetSahLabelText().Replace("\n", "").Replace("\r", "").Replace(" ",""), PromoDetails.PromoSashText,"Sash Label Text is not Matched");
        }

        [Then(@"Promo Message should be displayed on estab page")]
        public void ThenPromoMessageShouldBeDisplayedOnEstabpage()
        {
            Assert.AreEqual(PromoDetails.PromoMessage, _hotelSearchResults.GetPromoMessageonHotelcardorEstab(), "Promo Mesaage is not displayed on EstabPage");
        }

        [Then(@"UrlSash ribbon should be displayed on estab page")]
        public void ThenUrlSashRibbonShouldBeDisplayedOnEstabPage()
        {
            Assert.AreEqual(_hotelEstabPage.GetSahLabelText().Replace("\n", "").Replace("\r", "").Replace(" ", ""), PromoDetails.URLPromoSashText, "Sash Label Text is not Matched");
        }

        [Then(@"UrlPromo Message should be displayed on estabpage")]
        public void ThenUrlPromoMessageShouldBeDisplayedOnEstabpage()
        {
            Assert.AreEqual(PromoDetails.UrlPromoMessage, _hotelSearchResults.GetPromoMessageonHotelcardorEstab(), "URLPromo Mesaage is not displayed on EstabPage");
        }

        [When(@"I Select total price Toggle")]
        public void WhenISelectTotalPriceToggle()
        {            
            _hotelEstabPage.SelectTotalPriceToggle();
        }
      

        [Then(@"Promo message should be displayed on boardtype")]
        public void ThenPromomessageshouldbedisplayedOnboardtype()
        {
            if (HelperFunctions.IsDesktop())
            {
               Assert.AreEqual(_hotelEstabPage.GetBoardPromoMessage(), PromoDetails.PromoMessage, "Promo Mesaage is not displayed on BoardType");
            }
            else
            {                                                  
                Assert.AreEqual(_hotelEstabPage.GetRoomPromoMessage(), PromoDetails.PromoMessage, "Promo Mesaage is not displayed on Room");
                _hotelEstabPage.SelectRoom(1);                              
                Assert.AreEqual(_hotelEstabPage.GetBoardPromoMessage(), PromoDetails.PromoMessage, "Promo Mesaage is displayed not on BoardType");
                _hotelEstabPage.ClickBackToRooms();
            }               
        }

        [Then(@"UrlPromo message should be displayed on boardtype")]
        public void ThenUrlPromoMessageShouldBeDisplayedOnBoardtype()
        {
            if (HelperFunctions.IsDesktop())
            {                
               Assert.AreEqual(_hotelEstabPage.GetBoardPromoMessage(), PromoDetails.UrlPromoMessage, "Promo Mesaage is not displayed on BoardType");                
            }
            else
            {                
               Assert.AreEqual(_hotelEstabPage.GetRoomPromoMessage(), PromoDetails.UrlPromoMessage, "Promo Mesaage is not displayed on Room");    
               _hotelEstabPage.SelectRoom(1);               
               Assert.AreEqual(_hotelEstabPage.GetBoardPromoMessage(), PromoDetails.UrlPromoMessage, "Promo Mesaage is displayed not on BoardType");                
               _hotelEstabPage.ClickBackToRooms();
            }
        }


        [Then(@"Was now price should be displayed on boardtype")]
        public void ThenWasnowPriceShouldBeDisplayedOnBoardtype()
        {
            if (HelperFunctions.IsDesktop())
            {
                Assert.IsTrue(_hotelEstabPage.GetBoardWasPrice().ToString() != "", "Was Price is not Displayed on BoardType");
                Assert.IsTrue(_hotelEstabPage.GetBoardNowPrice().ToString() != "", "Now Price is not Displayed on BoardType");                
            }
            else
            {
                _hotelEstabPage.SelectRoom(1);
                Assert.IsTrue(_hotelEstabPage.GetBoardWasPrice().ToString() != "", "Was Price is Displayed on BoardType");
                Assert.IsTrue(_hotelEstabPage.GetBoardNowPrice().ToString() != "", "Now Price is Displayed on BoardType");
                _hotelEstabPage.ClickBackToRooms();
            }
        }

        [When(@"I select room type (.*) and board type (.*)")]
        public void WhenISelectRoomTypeAndBoardType(int roomTypeToSelect, int boardTypeToSelect)
        {
            _hotelEstabPage.GetRoomInformation(roomTypeToSelect);
            if (HelperFunctions.IsDesktop())
            {
                _hotelEstabPage.GetDesktopBoardTypeInformation(boardTypeToSelect, roomTypeToSelect);
                _hotelEstabPage.SelectDesktopBoardType(boardTypeToSelect, roomTypeToSelect);
            }
            else
            {
                _hotelEstabPage.SelectRoom(roomTypeToSelect);
                _hotelEstabPage.GetBoardTypeInformation(boardTypeToSelect);
                _hotelEstabPage.SelectBoardType(boardTypeToSelect);
            }  
        }

        [Then(@"Pill should be displayed on estab")]
        public void ThenPillShouldBeDisplayedOnEstab()
        {
            Assert.AreEqual(PromoDetails.CustomerFavoritePillText,_hotelEstabPage.GetHotelPillText(),"Pill Text is not Matched");
        }

        [Then(@"Check Free Cancellation Date with (.*) and (.*) In Estab Price Includes Section")]
        public void ThenCheckFreeCancellationDateInEstabPriceIncludesSection(int departure, int ruleDate)
        {
            if (_hotelEstabPage.IsNonRefundable())
            {
                Assert.Inconclusive("Hotel is Non Refundable");
            }
            string actualDatevalue = "";
            string expectedDateValue = _hotelEstabPage.GetFreeHotelCancellationTextInPriceIncludesSection();
            DateTime defaultDynamicaDate = DateTime.Now.AddDays(departure - Constants.DynamicDefaultCancellationDays);
            DateTime actualDate = DateTime.Now.AddDays(departure - Constants.BufferDate - ruleDate);
            DateTime departureDate = DateTime.Now.AddDays(departure);
            //Caluclate the Actual free cancellation date 
            if (HelperFunctions.IsDynamicCancellationEnabled())
            {
                //option 1 : actual date is earlier that defaultDynamic date
                if(DateTime.Compare(actualDate,defaultDynamicaDate)<0)
                    actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - ruleDate));
                else
                    //option 2
                    actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.DynamicDefaultCancellationDays));
                // compare departue date and date in iternary
                if((_hotelEstabPage.GetCheckinDayfromSearchItinerary()-int.Parse(departureDate.Day.ToString()))==1)
                    //option 3 : Accodomation date Adjustment
                    actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - ruleDate + 1));
            }
            else
                actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - (Constants.DefaultCancellationDays)));

            context.Add("FreeCancellationDateValue", actualDatevalue);
            Console.WriteLine("Free Cancellation Date In Estab :- " + "DateExpected-" + expectedDateValue.Substring(30) + " : Date Displayed-" + actualDatevalue);
            Assert.IsTrue(expectedDateValue.Contains(actualDatevalue), "Hotel Free Cancellation date is not matched on estab");
        }

        [Then(@"Check Free Cancellation Date on roomType (.*) and boardType (.*)")]
        public void ThenCheckFreeCancellationDateOnBoardType(int roomType,int boardType)
        {           
                string expectedDateValue = "";
                if (HelperFunctions.IsDesktop())
                {
                    _hotelEstabPage.ClickYourRoomOptionsButton();
                    expectedDateValue = _hotelEstabPage.GetFreeHotelCancellationTextInBoardType(roomType, boardType);
                }
                else
                {
                    _hotelEstabPage.ClickChooseRoomButton();
                    _hotelEstabPage.SelectRoom(1);
                    expectedDateValue = _hotelEstabPage.GetFreeHotelCancellationTextInBoardType(roomType, boardType);
                    _hotelEstabPage.ClickBackToRooms();
                }
                Assert.IsTrue(expectedDateValue.Contains(context["FreeCancellationDateValue"].ToString()), "Hotel Free Cancellation date is not matched on boardType");
        }

        [Then(@"Check Free Cancellation Date on MultiRoom with (.*) and (.*) and (.*)")]
        public void ThenCheckFreeCancellationDateOnMultiRoomWithAndAnd(int departure, int contractRuleDate, int extranetRuleDate)
        {
            string expectedDateValue = "";
            string actualDateValue = "";
            DateTime departureDate = DateTime.Now.AddDays(departure);
            _hotelEstabPage.ClickYourRoomOptionsButton();

            //Get Room and Board To Select for Room1
            int roomToSelect = _hotelEstabPage.GetRooomToSelectByName("Classic Room");           
            int boardToSelect= _hotelEstabPage.GetBoardToSelectByNameInGivenRoom(roomToSelect, "Bed And Breakfast");
            expectedDateValue = _hotelEstabPage.GetFreeHotelCancellationTextInBoardType(roomToSelect, boardToSelect);
            // compare departue date and date in iternary
            if ((_hotelEstabPage.GetCheckinDayfromSearchItinerary() - int.Parse(departureDate.Day.ToString())) == 1)
                //option 3 : Accodomation date Adjustment
                actualDateValue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - contractRuleDate + 1));
            else
                actualDateValue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - contractRuleDate));

            context.Add("ContractFreeCancellationDate", actualDateValue);
            Assert.IsTrue(expectedDateValue.Contains(actualDateValue), "Hotel Free Cancellation date is not matched on Room1");
            _hotelEstabPage.SelectDesktopBoardType(boardToSelect, roomToSelect);

            //Get Room and Board To Select for Room2
            roomToSelect = _hotelEstabPage.GetRooomToSelectByName("Deluxe Room");
            boardToSelect = _hotelEstabPage.GetBoardToSelectByNameInGivenRoom(roomToSelect, "Room Only");
            expectedDateValue = _hotelEstabPage.GetFreeHotelCancellationTextInBoardType(roomToSelect, boardToSelect);
            // compare departue date and date in iternary
            if ((_hotelEstabPage.GetCheckinDayfromSearchItinerary() - int.Parse(departureDate.Day.ToString())) == 1)
                //option 3 : Accodomation date Adjustment
                actualDateValue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - extranetRuleDate + 1));
            else
                actualDateValue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - extranetRuleDate));
            context.Add("ExtranetFreeCancellationDate", actualDateValue);
            Assert.IsTrue(expectedDateValue.Contains(actualDateValue), "Hotel Free Cancellation date is not matched on Room1");
            _hotelEstabPage.SelectDesktopBoardType(boardToSelect, roomToSelect);
        }

    }
}
