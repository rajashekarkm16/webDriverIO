using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class LandingPageSteps
    {
        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly ILandingPage _landingPage;
        private readonly IBreadCrumb _breadCrumb;
        private readonly IModalPopup _modalPopup;

        public LandingPageSteps(IHomePage homePage, ISearchSummaryComponent searchSummaryComponent, IBreadCrumb breadCrumb, ILandingPage landingPage, IModalPopup modalPopup, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _searchSummaryComponent = searchSummaryComponent;
            _landingPage = landingPage;
            _breadCrumb = breadCrumb;
            _modalPopup = modalPopup;
        }

        [When(@"I click search icon on landing page")]
        public void WhenIClickSearchIconOnLandingPage()
        {
            _landingPage.OpenSearch();
        }

        [Then(@"Headline widget should display heading and description")]
        public void ThenLandingPageShouldBeDisplayedWithValidHeading()
        {
            Assert.IsTrue(_landingPage.GetHeadlinePriceWidgetText().Split("\r")[0].Length > 1, "Headline widget- Header text is empty");
            Assert.IsTrue(_landingPage.GetHeadlinePriceWidgetText().Split("\r")[1].Length > 1, "Headline widget- description text is empty");
        }

        [Then(@"Intro widget should be displayed")]
        public void ThenIntroWidgetShouldBeDisplayed()
        {
            Assert.IsTrue(_landingPage.GetIntroWidgetHeading().Contains(context["LandingPageDestination"].ToString(), StringComparison.OrdinalIgnoreCase), "Intro widget text does not contain searched location");
            Assert.IsTrue(_landingPage.GetIntroWidgetDescription().Length > 1, "Intro widget text is empty");
        }

        [Then(@"Links on Intro widget should be clickable")]
        public void ThenLinksOnIntroWidgetShouldBeClickable()
        {
             Assert.IsTrue(_landingPage.IsIntroWidgetLinksClickable(), "Links on intro widget is not clickable");
        }

        [When(@"I click on more button")]
        public void WhenIClickOnMoreButton()
        {
            if (_landingPage.IsIntroWidgetShowMoreButtonVisible())
                _landingPage.ClickIntroWidgetShowMoreButton();
            else
                Assert.Fail("Show More button is not visible");

        }

        [Then(@"Intro text should be expanded")]
        public void ThenIntroTextShouldBeExpanded()
        {
            Assert.IsFalse(_landingPage.IsIntroTextFaderVisible(), "Intro text is not expanded");
        }

        [Then(@"Less button is displayed")]
        public void ThenLessButtonIsDisplayed()
        {
            Assert.IsTrue(_landingPage.IsIntroWidgetShowLessButtonVisible(), "Less button not visible on intro text");
        }

        [When(@"I click on Less button")]
        public void WhenIClickOnLessButton()
        {
            _landingPage.ClickIntroWidgetShowLessButton();
        }

        [Then(@"Intro text should be collapsed")]
        public void ThenIntroTextShouldBeFaded()
        {
            Assert.IsTrue(_landingPage.IsIntroTextFaderVisible(), "Intro text is  not faded");
        }

        [Then(@"More button is displayed")]
        public void ThenMoreButtonIsDisplayed()
        {
            Assert.IsTrue(_landingPage.IsIntroWidgetShowMoreButtonVisible(), "More button not visible on intro text");
        }

        [Then(@"Check hero wideget is displayed")]
        public void ThenCheckHeroWidegetIsDisplayed()
        {
            Assert.IsTrue(_landingPage.IsHeroWidgetDisplayed(), "Hero widget is not displayed");
        }

        [Then(@"Validate breadcrumbs are navigating to respective pages (.*)")]
        public void ThenValidateBreadcrumbsAreNavigatingToRespectivePages(string productType)
        {
            bool isHoliday;
            isHoliday = productType == "2" ? true : false;
            Assert.IsTrue(_breadCrumb.ValidateLandingPageBreadCrumbNavigation(isHoliday), "Landing Page bread crumbs not validated");
        }

        [Then(@"Map is not interactive")]
        public void ThenMapIsNotInteractive()
        {
            Assert.IsFalse(_landingPage.IsMapWidgetInteractable(), "Map should not be clickable");
        }

        [Then(@"Validate the usps on landing page")]
        public void ThenValidateTheUspsOnLandingPage()
        {
            List<string> uspHeadingOnUI = _landingPage.GetUSPHeadings();

            for (int count = 0; count < uspHeadingOnUI.Count(); count++)
            {
                switch (uspHeadingOnUI[count])
                {
                    case "ATOL Protected":
                        _landingPage.ClickUSP(count);
                        Assert.AreEqual("ATOL Protected", _modalPopup.GetModalHeading(), "ATOL Protected modal heading mismatch");
                        if (HelperFunctions.IsTRUK())
                            Assert.AreEqual(Constants.ATOLUSPModalTextUK, _modalPopup.GetModalContent().Trim(), "ATOL Protected modal content mismatch");
                        else
                            Assert.AreEqual(Constants.LandingPageATOLUSPModalTextIE, _modalPopup.GetModalContent().Trim(), "ATOL Protected modal content mismatch");
                        List<string> linksOnModal = _modalPopup.GetAllLinks();
                        if (HelperFunctions.IsTRUK())
                            Assert.IsTrue(linksOnModal.Contains(Constants.TotalFinancialProtetionUKUrl), "Total Financial Protection URL is incorrect");
                        else
                            Assert.IsTrue(linksOnModal.Contains(Constants.TotalFinancialProtetionIEUrl), "Total Financial Protection URL is incorrect");
                        Assert.IsTrue(linksOnModal.Contains(Constants.PeaceOfMindURl), "Peace of mind url is incorrect");
                        _modalPopup.ClosePopUp();
                        break;

                    case "Book With Confidence":
                        _landingPage.ClickUSP(count);
                        Assert.IsTrue("Book with confidence".Equals(_modalPopup.GetModalHeading(), StringComparison.OrdinalIgnoreCase));
                        Assert.AreEqual(Constants.LandingPageABTAUSPModalText, _modalPopup.GetModalContent().Trim(), "ABTA Member modal content mismatch");
                        _modalPopup.ClosePopUp();
                        break;

                    case "Lowest Price Guarantee":
                        _landingPage.ClickUSP(count);
                        Assert.AreEqual("Lowest Price Guarantee", _modalPopup.GetModalHeading(), "Low Price modal heading mismatch");
                        linksOnModal = _modalPopup.GetAllLinks();
                      //  Assert.IsTrue(linksOnModal.Contains(Constants.PriceMatchEmail), "Price matches email is incorrect");
                        _modalPopup.ClosePopUp();
                        break;
                    case "Rated 4.7/5":
                        _landingPage.ClickUSP(count);                        
                        string TrustPilotHeading = Regex.Replace("Rated 4.7/5", @"[^0-9a-zA-Z/._ ]", "");
                        Assert.IsTrue(TrustPilotHeading.Equals(_modalPopup.GetModalHeading(), StringComparison.OrdinalIgnoreCase));
                        Assert.AreEqual(Constants.TrustpilotModalText, _modalPopup.GetModalContent().Trim(), "ABTA Member modal content mismatch");
                        _modalPopup.ClosePopUp();
                        break;
                    case "24/7 In-Resort Support":
                        _landingPage.ClickUSP(count);
                        string ResortSupportHeading = Regex.Replace("24/7 In-Resort Support", @"[^0-9a-zA-Z-/._ ]", "");
                        Assert.IsTrue(ResortSupportHeading.Equals(_modalPopup.GetModalHeading(), StringComparison.OrdinalIgnoreCase));
                        Assert.AreEqual(Constants.ResortSupportText, _modalPopup.GetModalContent().Trim(), "ABTA Member modal content mismatch");
                        _modalPopup.ClosePopUp();
                        break;
                    default:
                        Assert.Fail("Invalid USP: " + uspHeadingOnUI[count]);
                        break;
                }
            }
        }

        [Then(@"Current month bar is selected by default")]
        public void ThenCurrentMonthBarIsSelectedByDefault()
        {
           Assert.AreEqual(DateTime.Today.Month, _landingPage.GetSelectedWeatherWidget());
        }

        [When(@"I click on the other month")]
        public void WhenIClickOnTheOtherMonth()
        {
            _landingPage.SelectRandomMonthBar();
        }

        [Then(@"Only the selected month bar should be highlighted")]
        public void ThenOnlyTheSelectedMonthBarShouldBeHighlighted()
        {
           Assert.IsTrue(_landingPage.ValidateWeatherWidgetSelection(_landingPage.GetSelectedWeatherWidget()), "Monthly bar selection validation failed");
        }

        [Then(@"Temperature on top of the bar must match with average")]
        public void ThenTemperatureOnTopOfTheBarMustMatchWithAverage()
        {
            Assert.IsTrue(_landingPage.GetAverageTemperatureOnTemperatureChart().Contains(_landingPage.GetAverageTemperatureOnMonthlyBar(_landingPage.GetSelectedWeatherWidget())));
        }

        [Then(@"I should be able to scroll through the available option\tand validate content count per page")]
        public void ThenIShouldBeAbleToScrollThroughTheAvailableOptionAndValidateContentCountPerPage()
        {
            Assert.IsTrue(_landingPage.ValidateFlightRoutesContentCountPerPage(), "Flight routes content count validation failed");
        }

        [Then(@"I should be able to navigate to blog content and it should be linked to corresponding blog page")]
        public void ThenIShouldBeAbleToNavigateToBlogContentAndItShouldBeLinkedToCorrespondingBlogPage()
        {
            Assert.IsTrue(_landingPage.ValidateBlogWidget(), "Blog widget not validated");
        }

        [When(@"I click on the open full screen icon")]
        public void WhenIClickOnTheOpenFullScreenIcon()
        {
            _landingPage.ClickFullScreenButton();
        }

        [Then(@"Image should be displayed in full screen")]
        public void ThenImageShouldBeDisplayedInFullScreen()
        {
            Assert.IsTrue(_landingPage.IsImageDisplayedInFullScreen(), "Image is not displayed in full screen");            
        }

        [When(@"I click on the close full screen icon")]
        public void WhenIClickOnTheCloseFullScreenIcon()
        {
            _landingPage.ClickFullScreenButton();
        }

        [Then(@"Image should be collapsed")]
        public void ThenImageShouldBeCollapsed()
        {
            Assert.IsFalse(_landingPage.IsImageDisplayedInFullScreen(), "Image is not collapsed");
        }

        [When(@"I click on destination image")]
        public void WhenIClickOnDestinationImage()
        {
            context.Add("SelectedLocation", _landingPage.GetHotelNameOnInterlinkingWidget(1));
            _landingPage.ClickOnInterlinkingWidgetImage(1);
        }

        [Then(@"Hotel landing page for selected destination should be displayed")]
        public void ThenHotelLandingPageForSelectedDestinationShouldBeDisplayed()
        {
            string url = _homePage.GetCurrentPageURL();
            Assert.IsTrue(url.Contains("hotels"));
            Assert.IsTrue(url.Contains(context["SelectedLocation"].ToString().Replace(" ", "-").ToLower()), "URL is does not contain location: " + context["SelectedLocation"].ToString());
        }

        [When(@"I navigate back")]
        public void WhenINavigateBack()
        {
            _homePage.NavigateBack();
        }

        [When(@"I click on the View more button")]
        public void WhenIClickOnTheViewMoreButton()
        {
            context.Clear();
            context.Add("SelectedLocation", _landingPage.GetHotelNameOnInterlinkingWidget(1));
            _landingPage.ClickSearchButtonOnInterlinkingWidget(1);
        }

        [Then(@"Holiday landing page for selected destination should be displayed")]
        public void ThenHolidayLandingPageForSelectedDestinationShouldBeDisplayed()
        {
            string url = _homePage.GetCurrentPageURL();
            Assert.IsTrue(url.Contains("holidays"));
            Assert.IsTrue(url.Contains(context["SelectedLocation"].ToString().Replace(" ", "-").ToLower()), "URL is does not contain location: " + context["SelectedLocation"].ToString());
        }

        [Then(@"Over view and offers view tab should be displayed")]
        public void ThenOverViewAndOffersViewTabShouldBeDisplayed()
        {
            Assert.IsTrue(_landingPage.IsOverviewAndOffersTabDisplayed(), "Over view and offers view tab is not displayed on UI");
        }

        [When(@"I click on offers tab")]
        public void WhenIClickOnOffersTab()
        {
            _landingPage.SelectOffersTab();
        }

        [Then(@"Offers view tab should be displayed")]
        public void ThenOffersViewTabShouldBeDisplayed()
        {
            _homePage.GetCurrentPageURL().Contains("Offers");
        }

        [When(@"I click on overview tab")]
        public void WhenIClickOnOverviewTab()
        {
            _landingPage.SelectOverviewTab();
        }

        [Then(@"Overview tab should be displayed")]
        public void ThenOverviewTabShouldBeDisplayed()
        {
            _homePage.GetCurrentPageURL().Contains("Overview");
        }

        [Then(@"Key facts items should be aligned to center")]
        public void ThenKeyFactsItemsShouldBeAlignedToCenter()
        {
            Assert.IsTrue(_landingPage.IsKeyFactsItemsAlignedToCenter(), "Key facts items are not aligned to center");
        }

        [When(@"I select random card on theme widget")]
        public void WhenISelectRandomCardOnThemeWidget()
        {
            int cardToSelect = _landingPage.GetCardToSelectOnThemeWidget();
            _landingPage.ScrollToCardOnThemeWidget(cardToSelect);
            _landingPage.ClickViewMoreButtonOnThemeWidget(cardToSelect + 1);
        }

        [Then(@"Holiday landing page should be displayed")]
        public void ThenHolidayLandingPageShouldBeDisplayed()
        {
            Assert.IsTrue(_homePage.GetCurrentPageURL().Contains("travelrepublic"), "Landing page is not displayed");
        }

        [When(@"I click on destination image on similar destination holiday card")]
        public void WhenIClickOnDestinationImageOnSimilarDestinationCard()
        {
            context.Add("SelectedLocation", _landingPage.GetHeadingOnSimilarDestinationsWidgetHolidayCard(1));
            _landingPage.ClickOnSimilarDestinationsWidgetHolidayImage(1);
        }        

        [When(@"I click on the select button on similar destination holiday card")]
        public void WhenIClickOnTheSelectButtonOnSimilarDestinationCard()
        {
            context.Clear();
            context.Add("SelectedLocation", _landingPage.GetHeadingOnSimilarDestinationsWidgetHolidayCard(1));
            _landingPage.ClickSelectHolidayCardOnSimilarDestinationsWidget(1);
        }

        [When(@"I click on destination image on similar destination hotel card")]
        public void WhenIClickOnDestinationImageOnSimilarDestinationHotelCard()
        {
            context.Add("SelectedLocation", _landingPage.GetHeadingOnSimilarDestinationsWidgetHotelCard(1));
            _landingPage.ClickOnSimilarDestinationsWidgetHotelImage(1);
        }

        [When(@"I click on the select button on similar destination hotel card")]
        public void WhenIClickOnTheSelectButtonOnSimilarDestinationHotelCard()
        {
            context.Clear();
            context.Add("SelectedLocation", _landingPage.GetHeadingOnSimilarDestinationsWidgetHotelCard(1));
            _landingPage.ClickSelectHotelCardOnSimilarDestinationsWidget(1);
        }
    }
}
