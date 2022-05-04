using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.Reporting.CustomReporter;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.TravelRepublic.MobileWeb.UI.Pages;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class HotelSearchResultsPageSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly ISearchComponent _searchComponent;
        private readonly IFlightDetailsPage _flightDetailsPage;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IGuestComponent _guestComponent;
        private readonly IMapComponent _mapComponent;
        private readonly IFiltersModal _filtersModal;
        private readonly IBreadCrumb _breadCrumb;
        private readonly IUSP _usp;
        private readonly IModalPopup _modalPopup;
        private readonly IHeaderComponent _headerComponent;
        private readonly IFooterComponent _footerComponent;
        private readonly ILandingPage _landingPage;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private readonly ILandingPageCalendarComponent _landingPageCalendarComponent;


        public HotelSearchResultsPageSteps(IHomePage homePage, ISearchSummaryComponent searchSummaryComponent, ILandingPageGuestComponent landingPageGuestComponent, ILandingPageCalendarComponent landingPageCalendarComponent, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, ISearchComponent searchComponent, IGuestComponent guestComponent, IFlightDetailsPage flightDetailsPage, ICalendarComponent calendarComponent, IHeaderComponent headerComponent, IFooterComponent footerComponent, ScenarioContext injectedContext, IMapComponent mapComponent, IFiltersModal filtersModal, IBreadCrumb breadCrumb, IUSP usp, IModalPopup modalPopup, ILandingPage landingPage)
        {
            context = injectedContext;
            _homePage = homePage;
            _searchSummaryComponent = searchSummaryComponent;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _searchComponent = searchComponent;
            _flightDetailsPage = flightDetailsPage;
            _calendarComponent = calendarComponent;
            _landingPageCalendarComponent = landingPageCalendarComponent;
            _guestComponent = guestComponent;
            _mapComponent = mapComponent;
            _filtersModal = filtersModal;
            _breadCrumb = breadCrumb;
            _usp = usp;
            _modalPopup = modalPopup;
            _headerComponent = headerComponent;
            _footerComponent = footerComponent;
            _landingPage = landingPage;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        [Given(@"Given I perform a mobile holiday search")]
        public void GivenGivenIPerformAMobileHolidaySearch()
        {
            _homePage.SearchHolidays();
        }

        [When(@"I store hotel and pre-selected flight information and select the hotel from the results")]
        public void WhenIStoreTheInformationOfTheHotelToBeSelectedAndPre_SelectedFlightFromTheResults()
        {
            _hotelSearchResults.CaptureHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.ViewFlightDetails(_hotelSearchResults.GetHotelToSelect());
            _flightDetailsPage.CaptureFlightDetails();
            _flightDetailsPage.CloseModal();
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
        }

        [Given(@"I perform hotel search for (.*) dates (.*) and (.*) with (.*)")]
        public void PerformAHotelSearchWith(string destination, int departure, int duration, string guests)
        {
            _homePage.SearchHotels(destination, departure, duration, guests);
        }

        [Then(@"No rooms available (.*) should be displayed")]
        public void ThenNoRoomsAvailableShouldBeDisplayed(string NoRoomMessage)
        {
            //_hotelEstabPage.ClickRoomsTab();
            Assert.IsTrue(_hotelSearchResults.CaptureNoRoomsMessage().Contains(NoRoomMessage), "No rooms message mismatch");
        }

        [Then(@"Start a new search button should be displayed")]
        public void ThenStartANewSearchButtonShouldBeDisplayed()
        {
            Assert.IsTrue(_hotelSearchResults.IsStartNewSearchButtonDisplayed());
        }

        [When(@"I click on Start a new search button")]
        [When(@"Click on Start a new search button")]
        public void WhenIClickOnStartANewSearchButton()
        {
            _hotelSearchResults.ClickStartNewSearchButton();
        }

        [Then(@"Home page should be displayed")]
        public void ThenHomePageShouldBeDisplayed()
        {
            _homePage.VerifyHomePage();
        }

        [When(@"I click back button")]
        public void WhenIClickBackButton()
        {
            _homePage.NavigateBack();
        }

        [When(@"No rooms are available")]
        public void WhenNoRoomsAreAvailableAndIClickBackButton()
        {
            _hotelEstabPage.ClickRoomsTab();
            Assert.IsTrue(_hotelSearchResults.IsNoRoomsMessageDisplayed());
        }

        [When(@"No search results are available")]
        public void WhenNoSearchResultsAreAvailable()
        {
            Assert.IsTrue(_hotelSearchResults.IsNoRoomsMessageDisplayed());
        }

        [When(@"I click show more hotels button")]
        public void WhenIClickShowMoreHotelsButton()
        {
            context.Add("Coordinates", _hotelSearchResults.GetLoadMoreLocation());
            Console.WriteLine("Location of the button is " + context["Coordinates"] as string);
            _hotelSearchResults.LoadMoreResults();
        }

        [Then(@"Additional 15 results should be loaded")]
        public void ThenAdditionalResultsShouldBeLoaded()
        {
            Assert.AreEqual(_hotelSearchResults.GetResultsCount(), 30);
        }

        [Then(@"The “show more hotels” button should move to the bottom of the list")]
        public void ThenTheShowMoreHotelsButtonShouldMoveToTheBottomOfTheList()
        {
            Console.WriteLine("Location of the button after loading more hotels is " + _hotelSearchResults.GetLoadMoreLocation() as string);
            Assert.Less(Convert.ToInt16(context["Coordinates"]), _hotelSearchResults.GetLoadMoreLocation());
        }

        [Given(@"I am on holiday search results page")]
        public void GivenIAmOnHolidaySearchResultsPage()
        {
            _homePage.SearchHolidays();
            string occupancy = "2,0,0";
            List<RoomOccupantDetails> defaultRoomDetails = new List<RoomOccupantDetails>();
            if(HelperFunctions.IsV3HomepageEnabled())
                defaultRoomDetails = _landingPageGuestComponent.SetRoomsData(occupancy);
            else
                defaultRoomDetails = _guestComponent.SetRoomsData(occupancy);
            context.Add("DefaultRoomDetails", defaultRoomDetails);
        }

        [When(@"I click Search alternative date button")]
        public void WhenIClickSearchAlternativeDateButton()
        {
            _hotelSearchResults.ClickSearchAlternativeDatesButton();
        }

        [When(@"I click on one of the hotel icon")]
        public void WhenIClickOnOneOfTheHotelIcon()
        {
            _mapComponent.SelectHotelPin();
        }

        [When(@"I click on the hotel name")]
        public void WhenIClickOnTheHotelName()
        {
            context.Add("HotelInformationInMaps", _mapComponent.GetHotelInformation());
            _mapComponent.SelectHotelFromMaps();
        }

        [Then(@"View matches button should display the search result count")]
        public void ThenViewMatchesButtonShouldMatchWithSearchResultCount()
        {
            Assert.GreaterOrEqual(_filtersModal.GetViewMatchesResultCount(), 0, "View matches is not showing results count");
        }

        [When(@"Click on Return flights link")]
        public void WhenClickOnReturnFlightsLink()
        {
            _hotelSearchResults.ViewFlightDetails(_hotelSearchResults.GetHotelToSelect());
        }

        [Then(@"Return flights link and board type details should be displayed under price includes section")]
        public void ThenReturnFlightsLinkAndBoardTypeDetailsShouldBeDisplayedUnderPriceIncludesSection()
        {
            WhenClickOnReturnFlightsLink();
            _flightDetailsPage.CloseModal();
            Assert.IsTrue(_hotelSearchResults.GetHotelBoardType(_hotelSearchResults.GetHotelToSelect()).Length > 0);
        }

        [Then(@"Return flights link and board type details should be displayed under price includes section in estab page")]
        public void ThenReturnFlightsLinkAndBoardTypeDetailsShouldBeDisplayedUnderPriceIncludesSectionInEstabPage()
        {
            _hotelEstabPage.ViewFlightDetails();
            _flightDetailsPage.CloseModal();
            Assert.IsTrue(_hotelEstabPage.IsBoardTypeDisplayedInPriceIncludesSection());
        }


        [Then(@"Holiday price from and Per person price should be displayed")]
        public void ThenHolidayPriceFromAndPerPersonPriceShouldBeDisplayed()
        {
            Assert.LessOrEqual((_hotelSearchResults.GetHolidayPerPersonPrice(_hotelSearchResults.GetHotelToSelect()) * 2) - (_hotelSearchResults.GetHolidayTotalPrice(_hotelSearchResults.GetHotelToSelect())), Constants.MaximumPriceDifference);
        }

        [Then(@"Hotel search results page should be displayed")]
        public void ThenHotelSearchResultsPageShouldBeDisplayed()
        {
            Assert.IsTrue(_hotelSearchResults.GetResultsCount() > 0 || _hotelSearchResults.IsNoRoomsMessageDisplayed(), "Hotel search result page is not displayed");
        }

        [When(@"Update dates if adjusted")]
        public void WhenUpdateDatesIfAdjusted()
        {
            if(_hotelSearchResults.AreDatesAdjusted())
            _landingPageCalendarComponent.SetDepartureDate(_landingPageCalendarComponent.GetDepartureDate().AddDays(1));
        }


        [Then(@"Validate the breadcrumb on hotel results page")]
        public void ThenValidateTheBreadcrumbOnHotelResultsPage()
        {
            Dictionary<string, bool> breadCrumbItems = _breadCrumb.GetBreadCrumbListItemsWithStatus();

            if (HelperFunctions.IsDesktop())
            {
                Assert.IsTrue(breadCrumbItems["CHOOSE HOTEL"], "Choose hotel bread crumb is not active ");
                Assert.IsFalse(breadCrumbItems["CHOOSE ROOM"], "Choose room list is active ");
                Assert.IsFalse(breadCrumbItems["BOOK"], "Book list is active ");
            }
            else
            {
                Assert.IsEmpty(breadCrumbItems, "Mobile has breadcrumbs");
            }
        }

        [Then(@"Validate the usps on hotel results page")]
        public void ThenValidateTheUspsOnHotelResultsPage()
        {
            if (HelperFunctions.IsTRUK())
            {
                Assert.AreEqual("ATOL Protected", _usp.GetHeading(0), "ATOL Protected heading mismatch");
                Assert.AreEqual("Your holiday is safe with us", _usp.GetBody(0), "ATOL Protected body mismatch");
                _usp.ClickUSP(0);
                Assert.AreEqual("ATOL Protected", _modalPopup.GetModalHeading(), "ATOL Protected modal heading mismatch");
                if (HelperFunctions.IsTRUK())
                    Assert.AreEqual(Constants.LandingPageATOLUSPModalTextUK, _modalPopup.GetModalContent().Trim(), "ATOL Protected modal content mismatch");
                else
                    Assert.AreEqual(Constants.ATOLUSPModalTextIE, _modalPopup.GetModalContent().Trim(), "ATOL Protected modal content mismatch");
            }

            List<string> allLinks = _modalPopup.GetAllLinks();
            if (HelperFunctions.IsTRUK())
            {
                Assert.IsTrue(allLinks.Contains(Constants.TotalFinancialProtetionUKUrl), "Total Financial Protection URL is incorrect");
                //else
                //    Assert.IsTrue(allLinks.Contains(Constants.TotalFinancialProtetionIEUrl), "Total Financial Protection URL is incorrect");
                Assert.IsTrue(allLinks.Contains(Constants.PeaceOfMindURl), "Peace of mind url is incorrect");
                _modalPopup.ClosePopUp();
            }
            int index = HelperFunctions.IsTRUK() ? 1 : 0;
                Thread.Sleep(2000);
            Assert.AreEqual("Lowest Price Guarantee", _usp.GetHeading(index), "Lowest Price Guarantee heading mismatch");
            Assert.AreEqual("Terms & conditions apply", _usp.GetBody(index), "Lowest Price Guarantee body mismatch");
            _usp.ClickUSP(index);
            Assert.AreEqual("Lowest Price Guarantee", _modalPopup.GetModalHeading(), "Low Price modal heading mismatch");
            allLinks = _modalPopup.GetAllLinks();
            Assert.IsTrue(allLinks.Contains(Constants.PriceMatchEmail), "Price matches email is incorrect");
            _modalPopup.ClosePopUp();

            Thread.Sleep(2000);
            if (HelperFunctions.IsTRUK())
            {
                Assert.AreEqual("Travel With Confidence", _usp.GetHeading(index+1));
                Assert.AreEqual("ABTA Member", _usp.GetBody(index+1));
            }
            else
            {
                Assert.AreEqual("ITAA Member", _usp.GetHeading(index+1));
                Assert.AreEqual("Travel with confidence", _usp.GetBody(index+1));
            }

            _usp.ClickUSP(index+1);
            Thread.Sleep(2000);
            if (HelperFunctions.IsTRUK())
            {
                Assert.AreEqual("Book with confidence", _modalPopup.GetModalHeading());
                Assert.AreEqual(Constants.ABTAUSPModalText, _modalPopup.GetModalContent().Trim(), "ABTA Member modal content mismatch");
            }
            else
                Assert.AreEqual("Travel Republic is a member of the ITAA", _modalPopup.GetModalHeading());
            _modalPopup.ClosePopUp();
        }

        [Then(@"Hotels in (.*) shown")]
        public void ThenHotelsInDestinationShown(string destination)
        {
            Assert.AreEqual("Hotels in " + destination, _hotelSearchResults.GetHotelsInDestinationText().Split("-")[0].Trim(), "Hotels in destination text didnt match");
        }

        [Then(@"(.*) is shown search itinerary summary")]
        public void ThenDestinationInSearchItinerarySummary(string destination)
        {
            Assert.AreEqual(destination, _searchSummaryComponent.GetSearchLocation(), "Hotel destination text in search summary GetHotelDestinationFromSummarydidnt match");
        }

        [When(@"I click on Travel republic logo")]
        public void WhenIClickOnTravelRepublicLogo()
        {
            _headerComponent.ClickTRLogo();
        }

        [When(@"I click on the help centre logo")]
        public void WhenIClickOnTheHelpCentreLogo()
        {
            _headerComponent.ClickHelpCentreLogo();
        }

        [Then(@"User should be navigated to help centre page")]
        public void ThenUserShouldBeNavigatedToHelpCentrePage()
        {
            string HelpCentreURL = _homePage.GetURlOfNewWindow();
            if (HelperFunctions.IsTRUK())
                Assert.IsTrue(HelpCentreURL.Contains(Constants.HelpCentreLinkUKURL), "Help Centre link is incorrect");
            else
                Assert.IsTrue(HelpCentreURL.Contains(Constants.HelpCentreLinkIEURL), "Help Centre link is incorrect");

        }

        [When(@"I click on ATOL protected logo")]
        public void WhenIClickOnATOLProtectedLogo()
        {
            _headerComponent.ClickATOLLogo();
        }

        [Then(@"ATOL protected modal should be displayed")]
        public void ThenATOLProtectedModalShouldBeDisplayed()
        {
            Assert.IsTrue(_modalPopup.IsModalDisplayed(), "ATOL protected modal is not displayed");
            Assert.IsTrue(_modalPopup.GetModalHeading().Equals("ATOL Protected"));
        }

        [Then(@"Links on ATOL modal should be clickable")]
        public void ThenLinksOnATOLModalShouldBeClickable()
        {
            Assert.IsTrue(_headerComponent.IsLinksOnATOLModalEnbaled(), "Links on ATOL modal are not clickable");
        }

        [When(@"I click on main menu icon")]
        public void WhenIClickOnMainMenuIcon()
        {
            _headerComponent.ClickMainMenu();
        }

        [Then(@"Main menu options should be displayed")]
        public void ThenMainMenuOptionsShouldBeDisplayed()
        {
            Assert.IsTrue(_headerComponent.IsMainMenuDisplayed(), "Main Menu is not displayed");
            List<string> menuOptions = new List<string>();
            menuOptions = _headerComponent.GetMainMenuOptions();
            Assert.IsTrue(menuOptions.Contains("Destinations"));
            Assert.IsTrue(menuOptions.Contains("Holidays"));
            Assert.IsTrue(menuOptions.Contains("Hotels"));
            //Assert.IsTrue(menuOptions.Contains("Flights"));            
            Assert.IsTrue(menuOptions.Contains("Holiday Essentials"));
            Assert.IsTrue(menuOptions.Contains("Help Centre"));
        }

        [When(@"I click on (.*) from menu option")]
        public void WhenIClickOnDestinationsFromMenuOption(string menuOption)
        {
            _headerComponent.SelectFromMainMenuOptions(menuOption);
        }

        [Then(@"Second level menu options should be displayed")]
        public void ThenSecondLevelMenuOptionsShouldBeDisplayed()
        {
            Assert.IsTrue(_headerComponent.GetMenuTitle().Equals("Destinations"));
        }

        [When(@"I select available destination option")]
        public void WhenISelectAvailableDestinationOption()
        {
            _headerComponent.SelectDestinationFromMenu();
        }

        [Then(@"Third level menu option relavant to selected destination should be displayed")]
        public void ThenThirdLevelMenuOptionRelavantToSelectedDestinationShouldBeDisplayed()
        {
            _headerComponent.IsThirdLevelMenuDisplayed();
            Assert.IsTrue(_headerComponent.GetMenuTitle().Contains(_headerComponent.GetSelectedDestination().Split(" ")[0]));
        }

        [When(@"I click on account menu")]
        public void WhenIClickOnAccountMenu()
        {
            _headerComponent.ClickAccountMenu();
        }

        [Then(@"Account not signed in menu options should be displayed")]
        public void ThenAccountNotSignedInMenuOptionsShouldBeDisplayed()
        {
            List<string> menuOptions = new List<string>();
            menuOptions = _headerComponent.GetAccountMenuOptions();
            Assert.IsTrue(menuOptions.Contains("Sign In"));
            Assert.IsTrue(menuOptions.Contains("Manage booking"));
            Assert.IsTrue(menuOptions.Contains("Sign up"));
            Assert.AreEqual(menuOptions.Count, 3, "Account menu options count mismatch");
        }

        [Then(@"Account signed in menu options should be displayed")]
        public void ThenAccountSignedInMenuOptionsShouldBeDisplayed()
        {
            List<string> menuOptions = new List<string>();
            menuOptions = _headerComponent.GetAccountMenuOptions();
            //Assert.IsTrue(menuOptions.Exists(option => option.Contains("Dashboard")));
            Assert.IsTrue(menuOptions.Exists(option => option.Contains("Bookings")));
            //Assert.IsTrue(menuOptions.Exists(option => option.Contains("Reviews")));
            Assert.IsTrue(menuOptions.Exists(option => option.Contains("Settings")));
            Assert.IsTrue(menuOptions.Exists(option => option.Contains("Sign out", StringComparison.OrdinalIgnoreCase)));
            Assert.LessOrEqual(menuOptions.Count, 4, "Account signed in menu options count mismatch!");
            Assert.GreaterOrEqual(menuOptions.Count, 3, "Account signed in menu options count mismatch!");
        }

        [Then(@"Sign option should be displayed")]
        public void ThenSignOptionShouldBeDisplayed()
        {
            Assert.IsTrue(_headerComponent.IsSigninOptionDisplayed(), "All signin option not displayed");
        }

        [Given(@"Sign in as a user from hotel search results page")]
        public void GivenSignInAsAUserFromHotelSearchResultsPage()
        {
            _headerComponent.ClickAccountMenu();
            if (!HelperFunctions.IsV3SignInEnabled())            
                _headerComponent.SelectFromAccountMenuOptions("Sign In");
            _headerComponent.PopulateAndSignInToAccount();
            //Work around to complete account sign in'            
            //if (HelperFunctions.IsDesktop())
            //    _homePage.SearchHotels();
            //else
             //   _headerComponent.ClickTRLogo();
        }

        [Then(@"Header should be as per design")]
        public void ThenHeaderShouldBeAsPerDesign()
        {
            Assert.IsTrue(_headerComponent.IsTravelRepublicLogoDisplayed() && _headerComponent.IsHelpCentreLogoDisplayed() && _headerComponent.IsMenuOptionDisplayed() && _headerComponent.IsAccountMenuDisplayed());
            if (HelperFunctions.IsDesktop())
            {  
                if (HelperFunctions.IsTRUK())
                {
                    Assert.IsTrue(_headerComponent.IsATOLLogoVisible(), "ATOL logo is not displayed on Desktop");
                    Assert.AreEqual(_headerComponent.GetCallToBookNumber(), Constants.CallToBookUK);
                }
                else
                    Assert.AreEqual(_headerComponent.GetCallToBookNumber(), Constants.CallToBookIE);
                Assert.IsFalse(_headerComponent.IsCallToBookNumberAHyperLink(), "Call to book should not be clickable");
            }
            else if (!HelperFunctions.IsDesktop() & HelperFunctions.IsTRUK())
                Assert.IsTrue(_headerComponent.IsATOLLogoVisible(), "ATOL logo should be displayed on Mobile UK");
            else
                Assert.IsFalse(_headerComponent.IsATOLLogoVisible(), "ATOL logo should not be displayed on Mobile IE");
        }

        [Then(@"Footer should be as per design")]
        public void ThenFooterShouldBeAsPerDesign()
        {
            Assert.IsTrue(_footerComponent.IsCallUsNumberDisplayed() && _footerComponent.IsFooterLinksDisplayed() && _footerComponent.IsFooterTextDisplayed(), "Footer contents are missing");
        }

        [Then(@"ABTA/ITAA logo should be displayed")]
        public void ThenABTAITAALogoShouldBeDisplayed()
        {
            if (HelperFunctions.IsTRUK())
                Assert.IsTrue(_footerComponent.IsABTALogoDisplayed(), "ABTA logo not displayed on footer");
            else
                Assert.IsTrue(_footerComponent.IsITAALogoDisplayed(), "ITAA logo not displayed on footer");
        }

        [When(@"I click Terms of Business Link on footer")]
        public void WhenIClickTermsOfBusinessLinkOnFooter()
        {
            _footerComponent.ClickTermsOfBusinessLink();
        }

        [Then(@"Terms of Business modal should be displayed")]
        public void ThenTermsOfBusinessModalShouldBeDisplayed()
        {
            Assert.IsTrue(_modalPopup.IsModalDisplayed(), "Terms of Business modal is not displayed");
            Assert.AreEqual(_modalPopup.GetModalHeading(), "Agency Terms of Business");
        }

        [When(@"I click Standard Cancellations Terms link on footer")]
        public void WhenIClickStandardCancellationsTermsLinkOnFooter()
        {
            _footerComponent.ClickStandardCancellationsTermsLink();
        }

        [Then(@"Standard Cancellations Terms site should be displayed")]
        public void ThenStandardCancellationsTermsSiteShouldBeDisplayed()
        {
            if (HelperFunctions.IsTRUK())
                Assert.AreEqual(_homePage.GetCurrentPageURL(), Constants.StandardCancellationTermsUK);
            else
                Assert.AreEqual(_homePage.GetCurrentPageURL(), Constants.StandardCancellationTermsIE);
        }

        [Then(@"Footer Links should be displayed and clickable")]
        public void ThenFooterLinksShouldBeDisplayedAndClickable()
        {
            Assert.IsTrue(_footerComponent.IsFooterLinksClickable(), "Footer links are not clickable");
        }

        [Then(@"Call us number should be displayed")]
        public void ThenCallUsNumberShouldBeDisplayed()
        {
            if (HelperFunctions.IsTRUK())
                Assert.AreEqual(_footerComponent.GetCallUsNumber(), Constants.CallToBookUK);
            else
                Assert.AreEqual(_footerComponent.GetCallUsNumber(), Constants.CallToBookIE);
        }

        [Given(@"Capture all hotel search results")]
        public void WhenCaptureAllHotelSearchResults()
        {
            context.Add("HotelNames", _hotelSearchResults.GetAllHotelNames() as List<string>);
        }

        [When(@"Close the sort model")]
        public void ThenCloseTheSortModel()
        {
            _hotelSearchResults.CloseSortModel();
        }

        [Then(@"Hotel search results remains unchanged")]
        public void ThenHotelSearchResultsRemainsUnchanged()
        {
            List<string> hotelNames = context["HotelNames"] as List<string>;
            Thread.Sleep(2000);
            List<string> newHotelNames = _hotelSearchResults.GetAllHotelNames();            
            Assert.IsTrue(hotelNames.SequenceEqual(newHotelNames), "Hotel results are changed after closing the sort model!");
        }

        [Then(@"Secure today pill is displayed")]
        public void ThenSecureTodayPillIsDisplayed()
        {
            Assert.IsTrue(_hotelSearchResults.IsSecureTodayPillDisplayed(1), "Secure today from pill is not displayed for hotel!");
        }

        [Then(@"Deposit price matches in the secure today pop up modal")]
        public void ThenDepositPriceMatchesInTheSecureTodayPopUpModal()
        {
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _landingPageGuestComponent.GetRoomOccupantDetails().Count, _hotelSearchResults.GetAmountFromSecureTodayPill(1));
                _hotelSearchResults.ClickSecureTodayPill(1);
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _landingPageGuestComponent.GetRoomOccupantDetails().Count, _hotelSearchResults.GetAmountFromDepositDialogModal(), "Price amount doesnt match in pay deposit dialog box!");
            }

            else 
            {
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _guestComponent.GetRoomOccupantDetails().Count, _hotelSearchResults.GetAmountFromSecureTodayPill(1));
                _hotelSearchResults.ClickSecureTodayPill(1);
                Assert.AreEqual(Constants.DefaultHotelDepositPrice * _guestComponent.GetRoomOccupantDetails().Count, _hotelSearchResults.GetAmountFromDepositDialogModal(), "Price amount doesnt match in pay deposit dialog box!");
            }
        }

        [Then(@"Pay monthly pill is displayed")]
        public void ThenPayMonthlyPillIsDisplayed()
        {
            Assert.IsTrue(_hotelSearchResults.IsPayMonthlyPillDisplayed(1), "Pay monthly pill is not displayed for hotel!");
        }

        [Then(@"Pay monthly pill is displayed for hotels")]
        public void ThenPayMonthlyPillIsDisplayedForHotels()
        {
            if (HelperFunctions.IsPayMonthlyEnabledForHotels())
                Assert.IsTrue(_hotelSearchResults.IsPayMonthlyPillDisplayed(1), "Pay monthly pill is not displayed for hotel!");
            else
                Assert.Inconclusive("Paymonthly is disabled for hotels");
        }


        [Then(@"Monthly deposit price matches in the secure today pop up modal")]
        public void ThenMonthlyDepositPriceMatchesInTheSecureTodayPopUpModal()
        {
            if (HelperFunctions.IsDesktop())
            {
                context.Add("PriceInPill", _hotelSearchResults.GetAmountFromPayMonthlyPill(1));
                _hotelSearchResults.ClickPayMonthlyPill(1);
                Assert.AreEqual(context["PriceInPill"], _hotelSearchResults.GetAmountFromPayMonthlyDialogModal(), "Price amount doesn't match in pay deposit dialog box!");
            }
            else
            {
                _hotelSearchResults.ClickPayMonthlyPill(1);
                Assert.IsTrue(_hotelSearchResults.GetAmountFromPayMonthlyDialogModal() > 0, "Monthly deposit price is not displayed in the pop up modal!");
            }
        }

        [Then(@"Free cancellation message is displayed in hotel card")]
        public void ThenFreeCancellationMessageIsDisplayedInHotelCard()
        {
            Assert.IsTrue(_hotelSearchResults.IsFreeCancellationMessageDisplayed(1), "Free cancellation is not available for the hotel!");
        }

        [Then(@"Clicking the message opens free cancellation dialog modal")]
        public void ThenClickingTheMessageOpensFreeCancellationDialogModal()
        {
            _hotelSearchResults.ClickFreeCancellationMessage(1);
            Assert.IsTrue(_hotelSearchResults.GetDialogContent().Contains(Constants.FreeCancellationMessage), "Free cancellation message doesn't match!");
        }

        [Then(@"Non refundable message is displayed in hotel card")]
        public void ThenNonRefundableMessageIsDisplayedInHotelCard()
        {
            Assert.IsTrue(_hotelSearchResults.IsNonRefunableMessageDisplayed(1), "Non refundable is not available for the hotel!");
        }

        [Then(@"Clicking the message opens non refundable dialog modal")]
        public void ThenClickingTheMessageOpensNonRefundableDialogModal()
        {
            _hotelSearchResults.ClickNonRefundableMessage(1);
            Assert.AreEqual(Constants.NonRefundableHeader, _hotelSearchResults.GetDialogHeader(), "Pop up header verification has failed.");
            Assert.AreEqual(Constants.NonRefundableMessage, _hotelSearchResults.GetDialogContent(), "Pop up message verification has failed.");
            _hotelSearchResults.CloseDialogContentPopUp();
        }

        [Then(@"Sash ribbon should be displayed on hotel card")]
        public void ThenSashRibbonShouldBeDisplayedOnHotelcard()
        {
            context.Add("HotelToSelect", _hotelSearchResults.GetHotelToSelect(context["HotelName"].ToString()));
            int hotelSelect = Convert.ToInt32(context["HotelToSelect"].ToString());
            if (hotelSelect == 0)
                Assert.Fail("Hotel "+context["HotelName"].ToString()+" is not listed in the results");
            Assert.AreEqual(_hotelSearchResults.GetHotelSahLabelText(hotelSelect).Replace("\n", "").Replace("\r", "").Replace(" ", ""), PromoDetails.PromoSashText, "Sash Label Text is not Matched");
        }

        [Then(@"Promo Message should be displayed on hotel card")]
        public void ThenPromoMessageshouldbedisplayedonhotelcard()
        {
            Assert.AreEqual(PromoDetails.PromoMessage,_hotelSearchResults.GetPromoMessageonHotelcardorEstab(), "Promo Mesaage is not displayed on HotelCard");
        }

        [Then(@"Was now price should be displayed on hotel card")]
        public void ThenWasNowPriceShouldBeDisplayedOnHotelcard()
        {
            int hotelSelect = Convert.ToInt32(context["HotelToSelect"].ToString());
            Assert.IsTrue(_hotelSearchResults.GetHotelWasPrice(hotelSelect).ToString() != "", "Was Price is Displayed");
            Assert.IsTrue(_hotelSearchResults.GetHotelPrice(hotelSelect).ToString() != "", "Now Price is Displayed");
            Assert.IsTrue(_hotelSearchResults.GetHotelWasPrice(hotelSelect) > _hotelSearchResults.GetHotelPrice(hotelSelect), "Was Price is Greater than Now Price");
        }

        [Then(@"I should see platform 195 ads")]
        public void ThenIShouldSeePlatformAds()
        {
            Assert.AreEqual(Constants.DefaultPlatform195AdsCount, _hotelSearchResults.GetPlatformAdsCount());
        }

        [Then(@"I should see sponsered hotels")]
        public void ThenIShouldSeeSponseredHotels(int count)
        {
            Assert.AreEqual(Constants.DefaultSponserdHotelsCount, _hotelSearchResults.GetSponseredHotelsCount());
        }

        [Then(@"Validate the GA Tracking")]
        public void ThenValidateTheGATracking()
        {
            Dictionary<string, dynamic> gaData = _hotelSearchResults.GetGAData("event", "track_search_results_page_holiday");

            Assert.IsTrue(gaData.Keys.Contains("event") && gaData.Values.Contains("track_search_results_page_holiday"));
            // Assert.IsTrue(_hotelSearchResults.ValidateGAData("event", "track_search_results_page_holiday"));
        }

        [Then(@"Validate the GA Tracking for filter button click")]
        public void ThenValidateTheGATrackingForFilterButtonClick()
        {
            _hotelSearchResults.SelectFilters();
            Dictionary<string, dynamic> gaData = _hotelSearchResults.GetGAData("event", "gtm.click");
            Assert.AreEqual("sc-c-button sc-c-button--primary sc-c-button--s sc-c-button--has-icon", gaData.GetValueOrDefault("gtm.elementClasses"));



            //Assert.IsTrue(_hotelSearchResults.ValidateFilterClickGAEvent("event", "gtm.click"));
        }

        [Then(@"Validate the GA Tracking on landing page")]
        public void ThenValidateTheGATrackingOnLandingPage()
        {
            Dictionary<string, dynamic> gaData = _hotelSearchResults.GetGAData("widgetName", "heroWidget");

            Assert.AreEqual(gaData.GetValueOrDefault("widgetTitle"), "Hotels in Spain");
            Assert.AreEqual(gaData.GetValueOrDefault("widgetPosition"), 0);

            gaData = _hotelSearchResults.GetGAData("widgetName", "breadCrumbWidget");
            Assert.AreEqual(gaData.GetValueOrDefault("widgetPosition"), 1);

            gaData = _hotelSearchResults.GetGAData("widgetName", "headlineWidget");
            Assert.AreEqual(gaData.GetValueOrDefault("widgetPosition"), 2);
            Assert.AreEqual(gaData.GetValueOrDefault("widgetTitle"), "Hotels from as little as £5 pppn");

            //Fail

            ////_landingPage.OpenSearch();

            ////gaData = _hotelSearchResults.GetGAData("event", "v3destSearchClick");

            ////_landingPage.CloseSearch();


            //_landingPage.SelectOffersTab();

            //gaData = _hotelSearchResults.GetGAData("event", "virtualPageView");
            //Assert.AreEqual("/1-17-1-0/hotels-in-spain/offers", gaData.GetValueOrDefault("virtualPageURL"));

            //_landingPage.SelectOverviewTab();

            //gaData = _hotelSearchResults.GetGAData("event", "virtualPageView");
            //Assert.AreEqual("/1-17-1-0/hotels-in-spain/overview", gaData.GetValueOrDefault("virtualPageURL"));


            //Pass





            IReadOnlyCollection<object> ignoreData = _hotelSearchResults.GetAllDataLayerEntries();
            _landingPage.SelectOffersTab();
            gaData = _hotelSearchResults.GetGADataByKeyAndValueIgnoringEarlierData(ignoreData, "event", "virtualPageView");
            Assert.AreEqual("/v3/1-17-1-0/hotels-in-spain/offers", gaData.GetValueOrDefault("virtualPageURL"));


            ignoreData = _hotelSearchResults.GetAllDataLayerEntries();
            _landingPage.SelectOverviewTab();
            gaData = _hotelSearchResults.GetGADataByKeyAndValueIgnoringEarlierData(ignoreData, "event", "virtualPageView");
            Assert.AreEqual("/v3/1-17-1-0/hotels-in-spain/overview", gaData.GetValueOrDefault("virtualPageURL"));
        }

        [Then(@"Pill should be displayed on hotelcard")]
        public void ThenPillShouldBeDisplayed()
        {
            context.Add("HotelToSelect", _hotelSearchResults.GetHotelToSelect(context["HotelName"].ToString()));
            int hotelSelect = Convert.ToInt32(context["HotelToSelect"].ToString());
            Assert.AreEqual(PromoDetails.CustomerFavoritePillText,_hotelSearchResults.GetHotelPillText(hotelSelect),"Pill Text is not Matched");
        }

        [Given(@"Sign in as a user from home page")]
        public void GivenSignInAsAUserFromHomePage()
        {
            _headerComponent.ClickAccountMenu();
            if (!HelperFunctions.IsDesktop() && !HelperFunctions.IsV3SignInEnabled())
                _headerComponent.SelectFromAccountMenuOptions("Sign in");
            _headerComponent.PopulateAndSignInToAccount();

        }        

        [Then(@"Covid cover promotion box should be displayed")]
        public void ThenCovidcoverPromotionBoxShouldBeDisplayed()
        {
           Assert.AreEqual(CovidCover.Promotionboxtext, _hotelSearchResults.GetCovidCoverPlusText(),"CovidCover Promotion box Text is not matched");
        }

        [Then(@"Covid cover should be displayed in Price includes section")]
        public void ThenCovidcoverShouldBeDisplayedInPriceincludesSection()
        {
            Assert.AreEqual(CovidCover.Promotionboxtext, _hotelSearchResults.GetCovidPlusTextinPriceincludesSection(), "CovidCover Text in PriceIncludes section not matched");
        }

        [Then(@"Clicking on Covid cover should open dailog modal with content")]
        public void ThenClickingOnCovidcoverShouldOpenDailogModalWithContent()
        {
            Assert.AreEqual(CovidCover.CovidCoverdailogContent, _hotelSearchResults.GetCovidCoverDailogContentInPromotionbox(), "CovidCover Dailog Content is not matched");
        }

        [Then(@"Clicking on Covid cover in Price includes should open dailog modal with content")]
        public void ThenClickingOnCovidcoverinPriceincludesShouldOpenDailogModalWithContent()
        {
            Assert.AreEqual(CovidCover.CovidCoverdailogContent, _hotelSearchResults.GetCovidCoverDailogContentinPriceincludes(), "CovidCover Dailog Content is not matched");
        }

        [Then(@"It should display Stop Sell message")]
        public void ThenItShouldDisplayNoAvailablityMessage()
        {
            Assert.AreEqual(PromoDetails.StopSellMessage, _hotelSearchResults.GetNoAvailablityMessage(), "No availablityMessage is not disaplayed");
        }

        [Then(@"Check Free Cancellation Date with (.*) and (.*) In (.*) Hotel Card")]
        public void ThenCheckFreeCancellationDateInHotelCard(int departure,int ruleDate,string destination)
        {
            if (_hotelSearchResults.IsNonRefunableMessageDisplayed(_hotelSearchResults.GetHotelToSelect(destination)))
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
                if (DateTime.Compare(actualDate, defaultDynamicaDate) < 0)
                    actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - ruleDate));
                else
                    //option 2
                    actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.DynamicDefaultCancellationDays));

                // compare departue date and date initernary
                if ((_hotelEstabPage.GetCheckinDayfromSearchItinerary() - int.Parse(departureDate.Day.ToString())) == 1)
                    //option 3 : Accodomation date Adjustment
                    actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - Constants.BufferDate - ruleDate + 1));
            }
            else
                actualDatevalue = String.Format("{0:ddd dd MMM yyyy}", DateTime.Now.AddDays(departure - (Constants.DefaultCancellationDays)));

            Console.WriteLine("Free Cancellation Date In Hotel Card :- " + "DateExpected - " + expectedDateValue.Substring(30) + " :  Date Displayed : " + actualDatevalue);
            Assert.IsTrue(expectedDateValue.Contains(actualDatevalue), "Hotel Free Cancellation date is not matched on Hotel Card");
        }
    }




}
