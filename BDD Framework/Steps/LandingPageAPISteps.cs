using Dnata.Automation.BDDFramework;
using Dnata.Automation.BDDFramework.API;
using Dnata.Automation.BDDFramework.API.BaseSteps;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Methods;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class LandingPageAPISteps : BaseSteps
    {
        private readonly ScenarioContext _scenarioContext;  
        private readonly LandingPage _landingPageMethods;
        private readonly IHomePage _homePage;
        private readonly ILandingPage _landingPage;
        private readonly IBreadCrumb _breadCrumb;
        private readonly IModalPopup _modalPopup;
        private readonly IUSP _usp;

        private int widgetPosistion = 0;

        public LandingPageAPISteps(ScenarioContext scenarioContext, LandingPage landingPageMethods, IHomePage homePage, ILandingPage landingPage, IBreadCrumb breadCrumb, IModalPopup modalPopup, IUSP usp)
            :base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _landingPageMethods = landingPageMethods;
            _landingPage = landingPage;
            _homePage = homePage;
            _breadCrumb = breadCrumb;
            _modalPopup = modalPopup;
            _usp = usp;
        }

        [When(@"request to the resource - (.*)")]
        public void WhenRequestToTheResource_FacadeContent(string resource)
        {
            SaveRequestToContext(new HttpRequestWrapper(HelperFunctions.GetFacadeURL()).SetResourse(ResourceEndPoints.EndPoints[resource]));
        }

        [When(@"add (.*), (.*),(.*),(.*),(.*),(.*) to FacadeContent request")]
        public void WhenAddFacadeContentRequest(int destinationID, int productType, int themeID=0, bool isPreview = false , int tabID=1, bool tabOnly=true)
        {
            var request = GetRequestFromContext();
            IDictionary<string, Object> paramters = _landingPageMethods.GetParamtersToRequest( destinationID, productType, themeID, isPreview, tabID, tabOnly);
            request.AddParameters(paramters, ParameterType.GetOrPost);
            SaveRequestToContext(request);
        }

        [When(@"Add (.*), (.*) to FacadeContent and execute GET request")]
        [When(@"Add (.*), (.*) to FacadeContent")]
        public void WhenAddToFacadeContentAndExecuteGETRequest(int destinationID, int productType)
        {
            var response = _landingPageMethods.GetFacadeResponse(destinationID, productType);
            SaveResponseToContext(response);
        }

        [When(@"execute GET request")]
        public void WhenExecutePOSTRequest()
        {
            var request = GetRequestFromContext();
            var response = _landingPageMethods.Execute(request, Method.GET);
            SaveResponseToContext(response);
        }

        [Then(@"Validate the facade response")]
        public void ThenValidateTheFacadeResponse()
        {
            var response = GetResponseFromContext();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Response status is not OK");
            Assert.AreEqual(ResponseStatus.Completed, response.ResponseStatus, "Response status is not completed");
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            Assert.IsTrue(deserializedResponse.widgets.Count() >= 1, "Widgets are not shown on landing page");
            Assert.AreEqual("heroWidget", deserializedResponse.widgets[0].alias, "First widget is not hero widget");
            Assert.AreEqual("breadCrumbWidget", deserializedResponse.widgets[1].alias, "Second widget is not breadcrumb widget");
            Assert.AreEqual("headlineWidget", deserializedResponse.widgets[2].alias, "Third widget is not Headline widget");
            Assert.AreEqual("introTextWidget", deserializedResponse.widgets[3].alias, "Fourth widget is not Headline widget");
            Assert.AreEqual("hotelPageCountry", deserializedResponse.alias, "Hotel Page Country alias is missing");
            Assert.AreEqual("footerLinksWidget", deserializedResponse.widgets[deserializedResponse.widgets.Count() - 1].alias, "Footer link Widget is not the last one");
            Assert.AreEqual(2, deserializedResponse.widgets[deserializedResponse.widgets.Count() - 1].items.Count(), "Footer link Widget is not the last one");

        }

      
        [Then(@"CreateBasketResponse valid with JSON Schema")]
        public void ThenCreateBasketResponseValidWithJSONSchema()
        {
            
        }

        [Then(@"Hero image url matches the facade image url")]
        public void ThenHeroImageUrlMatchesTheFacadeImageUrl()
        {
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            String[] landingPageHeroImageUrl = _landingPage.GetHeroImageURL().Split('?');
            Assert.IsTrue(landingPageHeroImageUrl[0].Contains(deserializedResponse.widgets[0].heroImageUrl.Split('?')[0]), "Hero image URL miss match");
           //Assert.IsTrue(_landingPage.GetHeroImageURL().Contains(deserializedResponse.widgets[0].heroImageUrl), "Hero image URL miss match");
        }

        [Then(@"Headline widget heading and description matches with facade response")]
        public void ThenHeadlineWidgetHeadingAndDescriptionMatchesWithFacadeResponse()
        {
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            Assert.AreEqual("headlineWidget", deserializedResponse.widgets[2].alias, "Third widget is not Headline widget");
            Assert.AreEqual(deserializedResponse.widgets[2].header, _landingPage.GetHeadlinePriceWidgetText().Split("\r")[0]);
            Assert.AreEqual(deserializedResponse.widgets[2].details, _landingPage.GetHeadlinePriceWidgetText().Split("\r\n")[1]);
        }

        [Then(@"Headline widget should be third widget on hotel landing page")]
        public void ThenHeadlineWidgetShouldBeThirdWidgetOnHotelLandingPage()
        {
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            Assert.AreEqual("headlineWidget", deserializedResponse.widgets[2].alias, "Third widget is not Headline widget");
        }

        [Then(@"Intro widget heading and description matches with facade response (.*)")]
        public void ThenIntroWidgetHeadingAndDescriptionMatchesWithFacadeResponse(string productType)
        {
            int position = productType == "1" ? 3 : 2;
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            Assert.AreEqual("introTextWidget", deserializedResponse.widgets[position].alias, "Intro widget is not in posistion " + position + 1);
            Assert.AreEqual(deserializedResponse.widgets[position].sectionHeader, _landingPage.GetIntroWidgetHeading());
            Assert.AreEqual(deserializedResponse.widgets[position].introText, _landingPage.GetIntroWidgetDescription());
        }

        [Then(@"Breadcrumb should be below the hero widget")]
        public void ThenBreadcrumbShouldBeBelowTheHeroWidget()
        {
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            Assert.AreEqual("breadCrumbWidget", deserializedResponse.widgets[1].alias, "Bread Crumb widget is not in posistion");
            Assert.IsTrue(_landingPage.GetHeroImageLocation() < _breadCrumb.GetBreadCrumbLocation() && _breadCrumb.GetBreadCrumbLocation() < _landingPage.GetIntroWidgetLocation() , "Breadcrumb location is incorrect");
        }


        [Then(@"Breadcrumb item should match with facade response")]
        public void ThenBreadcrumbItemShouldMatchWithFacadeResponse()
        {
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var breadCrumbItemsOnAPI = deserializedResponse.widgets[1].items;

            Dictionary<string, string> breadCrumbItemsOnPage = _breadCrumb.GetLandingPageBreadCrumbLinks();
            Assert.AreEqual(breadCrumbItemsOnAPI.Count(), breadCrumbItemsOnPage.Count(), "BreadCrumb count on UI does not match with API");
            foreach (var breadCrumb in breadCrumbItemsOnAPI)
            {
                Assert.IsTrue(breadCrumbItemsOnPage[breadCrumb.title.ToUpper()].Contains(breadCrumb.url), "Breadcrumb item " + breadCrumb + "URL is not matching");
            }            
        }

        [Then(@"FAQ widget should be displayed")]
        public void ThenFAQWidgetShouldBeDisplayed()
        {
            CheckAndReturnWidgetPosition("faqWidget");            
            Assert.IsTrue(_landingPage.IsFAQWidgetVisible(), "FAQ widget is not present in UI");           
        }        

        [Then(@"FAQ heading and content of all available list should match with facade response")]
        public void ThenFAQHeadingAndContentOfAllAvailableListShouldMatchWithFacadeResponse()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("faqWidget");
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var FAQListOnAPI = deserializedResponse.widgets[widgetPosistion].items;
            
            Assert.AreEqual(FAQListOnAPI.Count(), _landingPage.GetFAQCount(), "FAQ count missmatch on API and UI");

            for(int count=0; count<FAQListOnAPI.Count(); count++)
            {
                _landingPage.ExpandFAQContent(count);
                Assert.IsTrue(_landingPage.IsFAQContentExpanded(count), "FAQ Content not expanded");
                Assert.AreEqual(_landingPage.GetFAQHeader(count), FAQListOnAPI[count].question, "FAQ heading missmatch");
                string contentOnUI = _landingPage.GetFAQContent(count).Replace("\r\n ", "");
                string contentOnAPI = HelperFunctions.RemoveContentBetweenCharacters(FAQListOnAPI[count].answer, '<', '>').Replace("\n ", "");
                Assert.AreEqual(contentOnAPI, contentOnUI, "FAQ content on UI does not match with API.");
                _landingPage.CollapseFAQContent(count);
                Assert.IsFalse(_landingPage.IsFAQContentExpanded(count), "FAQ Content not collapsed");
            }
        }

        public int CheckAndReturnWidgetPosition(string nameOfWidget)
        {
            List<string> widgetNames = GetWidgetListFromFacade();
            Assert.IsTrue(widgetNames.Contains(nameOfWidget), "Cannot find " + nameOfWidget + " in API response");
            widgetPosistion = widgetNames.IndexOf(nameOfWidget);
            return widgetPosistion;
        }

        public List<string> GetWidgetListFromFacade()
        {
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var widget = deserializedResponse.widgets;
            List<string> widgetName = new List<string>();
            foreach (var item in widget)
            {
                widgetName.Add(item.alias);
            }
            return widgetName;
        }

        [Then(@"Footer links widget should be displayed")]
        public void ThenFooterLinksWidgetShouldBeDisplayed()
        {
            CheckAndReturnWidgetPosition("footerLinksWidget");
            Assert.IsTrue(_landingPage.IsFooterLinkWidgetDisplayed(), "Footer link widget is not present in UI");
        }

        [Then(@"Footer links data should match with facade response (.*)")]
        public void ThenFooterLinksDataShouldMatchWithFacadeResponse(string productType)
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("footerLinksWidget");
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var footerListOnAPI = deserializedResponse.widgets[widgetPosistion].items;
            List<string> footerListOnUI = _landingPage.GetFooterWidgetLinks();

            if (productType == "1")
                Assert.IsTrue(footerListOnAPI.Count()== 2 && _landingPage.GetFooterLinkWidgetCount() == 2);
            else
                Assert.IsTrue(footerListOnAPI.Count() == 1 && _landingPage.GetFooterLinkWidgetCount() == 1);

            for (int count=0; count<footerListOnAPI.Count(); count++)
            {
                Assert.AreEqual(footerListOnAPI[count].linkText, footerListOnUI[count], "Links on UI are not matching with API");
            }
        }

        [Then(@"Validate hotel footer links widget navigation (.*)")]
        public void ThenValidateHotelFooterLinksWidgetNavigationSpain(string location)
        {
            Assert.IsTrue(_landingPage.ValidateHotelFooterWidgetLinksNavigation(location), " Hotel footer link widget links navigation validation failed");
        }

        [Then(@"Validate holiday footer links widget navigation (.*)")]
        public void ThenValidateHolidayFooterLinksWidgetNavigationSpain(string location)
        {
            Assert.IsTrue(_landingPage.ValidateHolidayFooterWidgetLinksNavigation(location), "Holiday footer link widget links navigation validation failed");
        }

        [Then(@"Map widget should be displayed and matches with the facade response")]
        public void ThenMapWidgetShouldBeDisplayedAndMatchesWithTheFacadeResponse()
        {
            CheckAndReturnWidgetPosition("mapWidget");
            Assert.IsTrue(_landingPage.IsMapWidgetDisplayed(), "Map widget not displayed on UI");
        }

        [Then(@"Usp on landing page should match with the facade response")]
        public void ThenUspsOnLandingPageShouldMatchWithTheFacadeResponse()
        {
            CheckAndReturnWidgetPosition("uspWidget");
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var uspListOnAPI = deserializedResponse.widgets[widgetPosistion].items;

            List<string> uspHeadingOnUI = _landingPage.GetUSPHeadings();
            List<string> uspDescriptionOnUI = _landingPage.GetUSPDescriptions();
            Assert.AreEqual(uspListOnAPI.Count(), uspHeadingOnUI.Count, "USP count miss match");
            for(int count=0; count<uspListOnAPI.Count(); count++)
            {
                Assert.AreEqual(uspListOnAPI[count].title, uspHeadingOnUI[count], "USP heading miss match");
                Assert.AreEqual(uspListOnAPI[count].description.Replace("&amp;", "&"), uspDescriptionOnUI[count], "USP description miss match");
            }         

        }

        [Then(@"Key facts on landing page should match with the facade response")]
        public void ThenKeyFactsOnLandingPageShouldMatchWithTheFacadeResponse()
        {
            CheckAndReturnWidgetPosition("keyFactsWidget");
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var widget = deserializedResponse.widgets[widgetPosistion];
            var keyInfoListOnAPI = deserializedResponse.widgets[widgetPosistion].items;
            Assert.AreEqual(widget.sectionHeader, _landingPage.GetKeyFactsSectionHeading(), "Key facts section heading miss match");
            Assert.AreEqual(widget.sectionSubHeader.Trim(), _landingPage.GetKeyFactsSectionDescription(), "Key facts section description miss match");

            List<string> keyInfoHeadingOnUI = _landingPage.GetKeyFactsItemHeadings();
            List<string> keyInfoDescriptionOnUI = _landingPage.GetKeyFactsItemDescriptions();
            for (int count = 0; count < keyInfoListOnAPI.Count(); count++)
            {
                Assert.AreEqual(keyInfoListOnAPI[count].factTitle, keyInfoHeadingOnUI[count], "Key facts content heading miss match");
                Assert.AreEqual(keyInfoListOnAPI[count].description1, keyInfoDescriptionOnUI[count], "key facts description miss match");
            }
        }

        [Then(@"Weather widget should be displayed and information should match with facade response")]
        public void ThenWeatherWidgetShouldBeDisplayedAndInformationShouldMatchWithFacadeResponse()
        {
            CheckAndReturnWidgetPosition("weatherWidget");
            var response = GetResponseFromContext();
            var widget = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.AreEqual(widget.sectionHeader, _landingPage.GetWeatherWidgetHeading());
            Assert.AreEqual(widget.sectionSubHeader, _landingPage.GetWeatherWidgetDescription());
            Assert.IsTrue(_landingPage.GetWeatherMonthlyBarCount() == 12, "Monthly bar is not displayed for all months of the year");
        }

        [Then(@"Flight route widget should be displayed and information should match with facade response")]
        public void ThenFlightRouteWidgetShouldBeDisplayedAndInformationShouldMatchWithFacadeResponse()
        {
            CheckAndReturnWidgetPosition("routesWidget");
            var response = GetResponseFromContext();
            var widget = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.IsTrue(_landingPage.IsFlightsRouteAvailable(), "Flight routes is not displayed");
            Assert.AreEqual(widget.sectionHeader, _landingPage.GetFlightsRoutesWidgetHeading());
            Assert.AreEqual(widget.sectionSubHeader, _landingPage.GetFlightstRoutesWidgetDescription());
        }

        [Then(@"Flight route widget should not be displayed")]
        public void ThenFlightRouteWidgetShouldNotBeDisplayed()
        {
            Assert.IsFalse(_landingPage.IsFlightsRouteAvailable(), "Flight routes should not displayed");
        }

        [Then(@"Blog widget should be displayed and information should match with facade response")]
        public void ThenBlogWidgetShouldBeDisplayedAndInformationShouldMatchWithFacadeResponse()
        {
            CheckAndReturnWidgetPosition("blogWidget");
            var response = GetResponseFromContext();
            var widget = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.AreEqual(widget.sectionHeader, _landingPage.GetBlogWidgetHeading());
            if (widget.sectionSubHeader == "")
                Assert.IsFalse(_landingPage.IsBlogWidgetDescriptionDisplayed(), "Blog description displayed on UI but not on API");
            else
                Assert.AreEqual(widget.sectionSubHeader, _landingPage.GetBlogWidgetDescription());
        }

        [Then(@"Image gallery widget should be displayed")]
        public void ThenImageGalleryWidgetShouldBeDisplayed()
        {
            CheckAndReturnWidgetPosition("imageGalleryWidget");
            Assert.IsTrue(_landingPage.IsImageGalleryWidgetDisplayed(), "Image gallery widget is not displayed on UI");
        }

        [Then(@"I should be able to navigate through image gallery and content should match with facade response")]
        public void ThenIShouldBeAbleToNavigateThroughImageGalleryAndContentShouldMatchWithFacadeResponse()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("imageGalleryWidget");
            var response = GetResponseFromContext();
            var deserializedResponse = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var imageGalleryListOnAPI = deserializedResponse.widgets[widgetPosistion].items;

            Assert.AreEqual(imageGalleryListOnAPI.Count().ToString(), _landingPage.GetImagesCount(), "Images count does not match on API and UI");
            for(int count=0; count<imageGalleryListOnAPI.Count(); count ++)
            {
                Assert.AreEqual(imageGalleryListOnAPI[count].title, _landingPage.GetImageGalleryDescription(), "Image description miss match");
                _landingPage.ClickNextButtonOnImageGallery();
            }
        }

        [Then(@"Interlinking widget should be displayed")]
        public void ThenInterlinkingWidgetShouldBeDisplayed()
        {
            CheckAndReturnWidgetPosition("interlinkingWidget");
            Assert.IsTrue(_landingPage.IsInterlinkingWidgetDisplayed(), "Interlinking widget is not displayed on UI");
            Assert.IsTrue(_landingPage.GetInterlinkingHotelCardsCount() > 0, "Hotel cards are not displayed on Interlinking widget");
        }

        [Then(@"Interlinking widget heading and description should match with the facade response")]
        public void ThenInterlinkingWidgetHeadingAndDescriptionShouldMatchWithTheFacadeResponse()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("interlinkingWidget");
            var response = GetResponseFromContext();
            var interlinkingWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.AreEqual(interlinkingWidgetOnAPI.sectionHeader.Trim(), _landingPage.GetInterlinkingWidgetHeading(), "Header text missmatch");
            Assert.AreEqual(interlinkingWidgetOnAPI.sectionSubHeader, _landingPage.GetInterlinkingWidgetDescription(), "Description text missmatch");
        }

        [Then(@"Validate interlinking widget tabs details")]
        public void ThenValidateInterlinkingWidgetTabsDetails()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("interlinkingWidget");
            var response = GetResponseFromContext();
            var interlinkingWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];

            Assert.AreEqual(interlinkingWidgetOnAPI.tabs.Count(), _landingPage.GetInterlinkingTabsCount(), "Tabs count miss match");
            List<string> tabsTextOnUI = _landingPage.GetInterlinkingTabsText();
            for (int count=0; count< interlinkingWidgetOnAPI.tabs.Count(); count++)
            {
                Assert.AreEqual(interlinkingWidgetOnAPI.tabs[count].tabName, tabsTextOnUI[count] , "Tabs text miss match");
            }            
        }

        [Then(@"Validate hotel cards details on each available tabs")]
        public void ThenValidateHotelCardsDetailsOnEachAvailableTabs()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("interlinkingWidget");
            var response = GetResponseFromContext();
            var interlinkingWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            int hotelToSelect = 0;
            for (int count = 0; count < interlinkingWidgetOnAPI.tabs.Count(); count++)
            {
                _landingPage.SelectInterlinkingTab(count);
                hotelToSelect = _landingPage.RandomizeHotelToSelect(_landingPage.GetInterlinkingHotelCardsCount());
                _landingPage.ScrollToHotelCardOnInterlinkingWidget(hotelToSelect);
                Assert.AreEqual(interlinkingWidgetOnAPI.tabs[count].cards[hotelToSelect].title, _landingPage.GetHotelNameOnInterlinkingWidget(hotelToSelect +1));
                Assert.AreEqual(interlinkingWidgetOnAPI.tabs[count].cards[hotelToSelect].price.formatted, _landingPage.GetPriceOnInterlinkingWidget(hotelToSelect +1));
                Assert.IsTrue(_landingPage.GetImageSourceOnInterLinkingWidget(hotelToSelect + 1).Contains(interlinkingWidgetOnAPI.tabs[count].cards[hotelToSelect].imageUrl));
            }
        }

        [Then(@"Top pick destination should match with the facade response")]
        public void ThenTopPickDestinationShouldMatchWithTheFacadeResponse()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("interlinkingWidget");
            var response = GetResponseFromContext();
            var interlinkingWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            int hotelToSelect = 0;
            for (int count = 0; count < interlinkingWidgetOnAPI.tabs.Count(); count++)
            {
                _landingPage.SelectInterlinkingTab(count);
                hotelToSelect = _landingPage.RandomizeHotelToSelect(_landingPage.GetInterlinkingHotelCardsCount());
                _landingPage.ScrollToHotelCardOnInterlinkingWidget(hotelToSelect);
                var topPicksOnAPI = interlinkingWidgetOnAPI.tabs[count].cards[hotelToSelect-1].childLinks;
                if (topPicksOnAPI != null)
                {
                    Assert.IsTrue(_landingPage.IsTopPicksDisplayed(hotelToSelect), "Top Picks links not displayed on UI");
                    Dictionary<string, string> topPicksDetailsOnUI = _landingPage.GetTopPicksLocationsAndLinks(hotelToSelect);
                    for(int i = 0; i< topPicksOnAPI.Count(); i++)
                    {
                        Assert.IsTrue(topPicksDetailsOnUI[topPicksOnAPI[i].title].Contains(topPicksOnAPI[i].linkUrl), "Top picks URL missmatch");
                    }                    
                }                    
                else
                {
                    Assert.IsFalse(_landingPage.IsTopPicksDisplayed(hotelToSelect), "Top Picks displayed on UI but not displayed on API");
                }
            }
        }

        [When(@"I click a top pick destination")]
        public void WhenIClickATopPickDestination()
        {
            int hotelToSelect = 0;
            int count = 0;
            while (!_landingPage.IsTopPicksDisplayed(hotelToSelect+1))
            {
                _landingPage.SelectInterlinkingTab(count);
                count++;
            }
            _scenarioContext.Add("TopPickLocation", _landingPage.ClickAndReturnTopPickLocation(hotelToSelect+1, 0));
        }

        [Then(@"Destination landing page should open in same tab")]
        public void ThenDestinationLandingPageShouldOpenInSameTab()
        {
            Assert.IsTrue(_homePage.GetCurrentPageURL().Contains(_scenarioContext["TopPickLocation"].ToString(), StringComparison.OrdinalIgnoreCase));
        }

        [Then(@"Hotel cards with more link should be displayed and description on modal should with the facade response")]
        public void ThenHotelCardsWithMoreLinkShouldBeDisplayedAndDescriptionShouldWithTheFacadeResponse()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("interlinkingWidget");
            var response = GetResponseFromContext();
            var interlinkingWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];

            int count = 0;
            int hotelToSelect = -1;
            while (hotelToSelect == -1)
            {
                _landingPage.SelectInterlinkingTab(count);
                hotelToSelect = _landingPage.GetHotelWithMoreDescriptionLink();
                count++;
            }
            Assert.IsTrue(hotelToSelect != -1, "Hotel with more description link is not present in UI. Please check test data");

            string descriptionOnAPI = interlinkingWidgetOnAPI.tabs[count-1].cards[hotelToSelect].description;
            descriptionOnAPI = HelperFunctions.RemoveContentBetweenCharacters(descriptionOnAPI, '<', '>').Replace("\n", "");
            descriptionOnAPI = descriptionOnAPI.Replace("\n", "");
            _scenarioContext.Add("SearchLocation", _landingPage.GetHotelNameOnInterlinkingWidget(hotelToSelect+1));
            string descriptionOnUI = _landingPage.GetLocationDescriptionOnInterlinkingWidget(hotelToSelect).Replace("\r", "");
            descriptionOnUI = descriptionOnUI.Replace("\n", "");
            Assert.AreEqual(descriptionOnAPI, descriptionOnUI, "Location description miss match");
        }

        [When(@"I click on search button on the more description modal")]
        public void WhenIClickOnViewButtonOnTheModal()
        {
            _landingPage.ClickSearchButtonOnMoreDescriptionModal();
        }

        [Then(@"I should be navigated to respective destination landing page")]
        public void ThenIShouldBeNavigatedToRespectiveDestinationLandingPage()
        {
            string url = _homePage.GetCurrentPageURL();
            Assert.IsTrue(url.Contains(_scenarioContext["SearchLocation"].ToString(), StringComparison.OrdinalIgnoreCase), "URL is does not contain location: "+ _scenarioContext["SearchLocation"].ToString());
        }

        [Then(@"Theme widget should be displayed")]
        public void ThenThemeWidgetShouldBeDisplayed()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("themeWidget");
            Assert.IsTrue(_landingPage.IsThemeWidgetDisplayed(), "Theme widget not displayed");
            Assert.IsTrue(_landingPage.GetThemeWidgetCardsCount() >= 3, "Card count on theme widget is less than 3");
        }

        [Then(@"Theme widget heading and description should match with the facade response")]
        public void ThenThemeWidgetHeadingAndDescriptionShouldMatchWithTheFacadeResponse()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("themeWidget");
            var response = GetResponseFromContext();
            var themeWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.AreEqual(themeWidgetOnAPI.sectionHeader, _landingPage.GetThemeWidgetHeading(), "Theme widget heading miss match");
            Assert.AreEqual(themeWidgetOnAPI.sectionSubHeader, _landingPage.GetThemeWidgetDescription(), "Theme widget description miss match");
        }

        [Then(@"Validate card details on theme widget")]
        public void ThenValidateCardDetailsOnThemeWidget()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("themeWidget");
            var response = GetResponseFromContext();
            var themeWidgetCardsOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion].items;

            for (int count= 0; count < themeWidgetCardsOnAPI.Count(); count ++)
            {
                _landingPage.ScrollToCardOnThemeWidget(count);
                Assert.AreEqual(themeWidgetCardsOnAPI[count].title.Trim(), _landingPage.GetCardHeadingOnThemeWidget(count+1), "Theme widget heading on card miss match");
                // Theme widget card description is blank, thats why commented below validation
                //Assert.AreEqual(themeWidgetCardsOnAPI[count].description, _landingPage.GetCardDescriptionOnThemeWidget(count+1), "Theme widget description on card miss match");
                Assert.IsTrue(_landingPage.GetCardLinkOnThemeWidget(count+1).Contains(themeWidgetCardsOnAPI[count].linkUrl));
                //Assert.AreEqual(themeWidgetCardsOnAPI[count].imageUrl, _landingPage.GetImageSourceOnThemeWidget(count+1), "Image source is not matching"); -- imageUrl not present on API response but there on postman
            }
        }

        [Then(@"Similar destination widget should be displayed on holiday landing page")]
        public void ThenSimilarDestinationWidgetShouldBeDisplayed()
        {
            CheckAndReturnWidgetPosition("similarDestinationsWidget");
            Assert.IsTrue(_landingPage.IsSimilarDestinationsWidgetDisplayed(), "similar destination widget not displayed");
            Assert.IsTrue(_landingPage.GetSimilarDestinationsWidgetHolidaysCardsCount() >= 3, "Card count on similar destination widget is less than 3");
        }

        [Then(@"Similar destination widget should be displayed on hotels landing page")]
        public void ThenSimilarDestinationWidgetShouldBeDisplayedOnHotelsLandingPage()
        {
            CheckAndReturnWidgetPosition("similarDestinationsWidget");
            Assert.IsTrue(_landingPage.IsSimilarDestinationsWidgetDisplayed(), "similar destination widget not displayed");
            Assert.IsTrue(_landingPage.GetSimilarDestinationsWidgetHotelCardsCount() >= 3, "Card count on similar destination widget is less than 3");
        }


        [Then(@"Similar destination title and description should match with the facade response")]
        public void ThenSimilarDestinationAndDescriptionShouldMatchWithTheFacadeResponse()
        {
            if(widgetPosistion == 0)
                CheckAndReturnWidgetPosition("similarDestinationsWidget");
            var response = GetResponseFromContext();
            var similarDestinationsWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.AreEqual(similarDestinationsWidgetOnAPI.sectionHeader, _landingPage.GetSimilarDestinationsWidgetHeading(), "similar destination widget heading miss match");
            Assert.AreEqual(similarDestinationsWidgetOnAPI.sectionSubHeader.Trim(), _landingPage.GetSimilarDestinationsWidgetDescription(), "similar destination widget description miss match");
        }

        [Then(@"(.*) similar destination tabs should be displayed on holidays landing page")]
        public void ThenSimilarDestinationTabsShouldBeDisplayedOnHolidaysLandingPage(int tabCount)
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("similarDestinationsWidget");
            var response = GetResponseFromContext();
            var similarDestinationsWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.AreEqual(_landingPage.GetSimilarDestinationsTabCount(), tabCount, "Tab count on API is not matching");

            List<string> tabsOnUI = _landingPage.GetSimilarDestinationsTabText();
            Assert.AreEqual(similarDestinationsWidgetOnAPI.destinationTabTitle, tabsOnUI[0], "Tab name on API is not matching");
            Assert.AreEqual(similarDestinationsWidgetOnAPI.holidayTypesTabTitle, tabsOnUI[1], "Tab name on API is not matching");
        }

        [Then(@"Similar destination tabs should not be displayed on hotels landing page")]
        public void ThenSimilarDestinationTabsShouldNotBeDisplayedOnHotelsLandingPage()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("similarDestinationsWidget");
            var response = GetResponseFromContext();
            var similarDestinationsWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            Assert.IsFalse(_landingPage.IsSimilarDestinationsTabDisplayed(), "Similar destination tabs should not be displayed on hotel landing page");
            //Assert.AreEqual(similarDestinationsWidgetOnAPI.destinationTabTitle, "", "Similar destination tabs should not be displayed on hotel landing page");
            Assert.AreEqual(similarDestinationsWidgetOnAPI.holidayTypesTabTitle, "", "Similar destination tabs should not be displayed on hotel landing page");
        }


        [Then(@"Validate holiday card details on each available similar destination tabs")]
        public void ThenValidateCardDetailsOnEachAvailableSimilarDestinationTabs()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("similarDestinationsWidget");
            var response = GetResponseFromContext();
            var ds = JsonConvert.DeserializeObject<FacadeResponse>(response.Content);
            var similarDestinationsWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            int cardToSelect = 0;
            for (int count = 0; count < 2; count++)
            {
                _landingPage.SelectSimilarDestinationsTab(count);
                cardToSelect = _landingPage.GetHolidayCardToSelectOnSimilarDestinationsWidget();
                _landingPage.ScrollToHolidayCardOnSimilarDestinationsWidget(cardToSelect);
                if (count == 0)
                {
                    Assert.AreEqual(similarDestinationsWidgetOnAPI.cards[cardToSelect].title, _landingPage.GetHeadingOnSimilarDestinationsWidgetHolidayCard(cardToSelect + 1));
                    Assert.AreEqual(similarDestinationsWidgetOnAPI.cards[cardToSelect].description, _landingPage.GetDescriptionOnSimilarDestinationsWidgetHolidayCard(cardToSelect + 1));
                    Assert.IsTrue(_landingPage.GetImageOnSimilarDestinationsWidgetHolidayCard(cardToSelect + 1).Contains(similarDestinationsWidgetOnAPI.cards[cardToSelect].imageUrl), "Image url missmatch");
                }
                else
                {
                    //var holidayTypes = similarDestinationsWidgetOnAPI.holidayTypes;

                    //foreach(object holidayType in holidayTypes)
                    //{

                    //   // holidayType.

                    //}
                    Assert.IsTrue(similarDestinationsWidgetOnAPI.holidayTypes.Count() == _landingPage.GetSimilarDestinationsWidgetHolidaysCardsCount(), "Cards count miss match on holiday types tab");
                }
               
            }
        }

        [Then(@"Validate hotel card details on similar destination widget")]
        public void ThenValidateHotelCardDetailsOnEachAvailableSimilarDestinationTabs()
        {
            if (widgetPosistion == 0)
                CheckAndReturnWidgetPosition("similarDestinationsWidget");
            var response = GetResponseFromContext();
            var similarDestinationsWidgetOnAPI = JsonConvert.DeserializeObject<FacadeResponse>(response.Content).widgets[widgetPosistion];
            int cardToSelect = _landingPage.GetHotelCardToSelectOnSimilarDestinationsWidget();
            _landingPage.ScrollToHotelCardOnSimilarDestinationsWidget(cardToSelect);
            Assert.AreEqual(similarDestinationsWidgetOnAPI.cards[cardToSelect].title, _landingPage.GetHeadingOnSimilarDestinationsWidgetHotelCard(cardToSelect + 1));
            //Description for card is not present
            //Assert.AreEqual(similarDestinationsWidgetOnAPI.cards[cardToSelect].description, _landingPage.GetDescriptionOnSimilarDestinationsWidgetHotelCard(cardToSelect + 1));
            Assert.IsTrue(_landingPage.GetImageOnSimilarDestinationsWidgetHotelCard(cardToSelect + 1).Contains(similarDestinationsWidgetOnAPI.cards[cardToSelect].imageUrl), "image url missmatch");
            Assert.IsTrue(_landingPage.GetHotelCardLinkOnSimilarDestinationsWidget(cardToSelect + 1).Contains(similarDestinationsWidgetOnAPI.cards[cardToSelect].linkUrl), "link url missmatch");
        }
    }
}
