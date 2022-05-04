
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;


namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class HomePageSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly IHeaderComponent _headerComponent;
        private readonly IFooterComponent _footerComponent;

        public HomePageSteps(IHomePage homePage, IHeaderComponent headerComponent, IFooterComponent footerComponent, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _headerComponent = headerComponent;
            _footerComponent = footerComponent;
        }

        [Given(@"Navigated to hotels landing page (.*)")]
        [Given(@"I navigate to hotels landing page (.*)")]
        public void GivenNavigatedToHotelsLandingPage(string url)
        {
            if (url.Contains("http"))
                _homePage.NavigateToHotelLandingPage(url);
            else
            {
                if (HelperFunctions.IsTRUK())
                    _homePage.NavigateToHotelLandingPage();
                else
                    _homePage.NavigateToHotelLandingPage(Constants.HotelLandingPageIEURL);
            }
            context.Add("LandingPageDestination", _homePage.GetCurrentPageURL().Split("in-")[1]);
        }

        [Given(@"Navigated to holidays landing page (.*)")]
        [Given(@"I Navigate to holidays landing page (.*)")]
        public void GivenNavigatedToHolidaysLandingPage(string url)
        {
            if (url.Contains("http"))
                _homePage.NavigateToHolidaysLandingPage(url);
            else
            {
                if (HelperFunctions.IsTRUK())
                    _homePage.NavigateToHolidaysLandingPage();
                else
                    _homePage.NavigateToHolidaysLandingPage(Constants.HolidaysLandingPageIEURL);
            }
            context.Add("LandingPageDestination", _homePage.GetCurrentPageURL().Split("in-")[1]);           
        }

        [Given(@"Click on Accept and close cookies button")]
        [Given(@"When I access Travel Republic site")]
        public void GivenClickOnAcceptAndCloseCookiesButton()
        {
            _homePage.AcceptAndCloseCookies();
        }

        [Given(@"I do a holiday search to (.*) from (.*) for (.*) during (.*) and (.*) dates")]
        public void GivenIDoAHolidaySearchToFromForDuringAndDates(string destination, string departure, string guests, string departureDate, string returnDate)
        {
            _homePage.SearchHolidaysForSpecificDates(destination, departure, departureDate, returnDate, guests);
        }

        [Given(@"I am on TR site")]
        public void GivenIAmOnTRSite()
        {
            Assert.IsTrue(_homePage.IsTRLogoVisible(), "Home page is not Loaded");
        }

        [When(@"I click on promotion terms and conditions")]
        public void WhenIClickOnPromotionTermsAndConditions()
        {
           _homePage.ClickPromoTermsandConditions();
        }

        [Then(@"Terms and conditions for promo is displayed")]
        public void ThenTermsAndConditionsForPromoIsDisplayed()
        {
           Assert.IsTrue(_homePage.CheckPromoText(PromoDetails.TermsAndCondition), "Promo Terms and Conditions is Visible");
        }

        [Given(@"I have added DiscountCode to URL")]
        public void GivenIHaveAddedDiscountCodeToURL()
        {
            _homePage.AddDiscountCodeToUrl();            
        }

        [Then(@"Verify DiscountCode cookie is added")]
        public void ThenVerifyDiscountCodeCookieIsAdded()
        {
            Assert.IsTrue(_homePage.GetDiscountCookieValue().Equals(PromoDetails.DiscountCode), "Discount Code is Not Added");
        }


    }
}
