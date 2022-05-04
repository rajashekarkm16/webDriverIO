using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class LandingPage: MobileBasePage, ILandingPage
    {
        private readonly IAtWebDriver _webDriver;

        private int selectedWeatherWidgetMonth = -1;
        private int hotelToSelect = 0;

        #region[constructor]
        public LandingPage(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[Web Elements]
        private AtWebElement SearchText => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@class,'sc-c-input-button')]");
        private AtWebElement OpenSearchButton => _webDriver.FindElement(LocatorType.XPath, "//button[@aria-label='Open Search']");
        private AtWebElement CloseSearchButton => _webDriver.FindElement(LocatorType.XPath, "(//button[@aria-label='Close'])[2]");
        private AtWebElement OverviewTab => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-tabs__tablist']/button[.='Overview']");
        private AtWebElement OffersTab => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-tabs__tablist']/button[.='Offers']");
        private AtBy byHeadlinePriceWidget => GetBy(LocatorType.XPath, "//h3[contains(@class,'color-grey')]/parent::div");
        private AtWebElement HeadlinePriceWidget => _webDriver.FindElement(byHeadlinePriceWidget);
        private AtBy byIntroWidget => GetBy(LocatorType.XPath, "//section[contains(@class,'6xl')]//div[contains(@class,'m-10')]");
        private AtWebElement IntroWidget => _webDriver.FindElement(byIntroWidget);
        private AtWebElement IntroWidgetHeading => _webDriver.FindElement(LocatorType.XPath, "//section[contains(@class,'6xl')]//div[contains(@class,'m-10')]//div[contains(@class,'-xl')]");
        private AtWebElement IntroWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "//section[contains(@class,'6xl')]//div[contains(@class,'m-10')]//span/div");
        private AtBy byIntroWidgetTextFader => GetBy(LocatorType.XPath, "//section[contains(@class,'6xl')]//div[contains(@class,'m-10')]//div[contains(@class,'fader')]");
        private AtWebElement IntroWidgetTextFader => _webDriver.FindElement(byIntroWidgetTextFader);
        private AtWebElement IntroWidgetShowMoreButton => _webDriver.FindElement(LocatorType.XPath, "//span[contains(@class,'content-cropper') and text()='More']");
        private AtWebElement IntroWidgetShowLessButton => _webDriver.FindElement(LocatorType.XPath, "//span[contains(@class,'content-cropper') and text()='Less']");
        private ReadOnlyCollection<AtWebElement> IntroWidgetLinks => _webDriver.FindElements(LocatorType.XPath, "//section[contains(@class,'6xl')]//div[contains(@class,'m-10')]//a");
        private AtBy byHeroWidget => GetBy(LocatorType.XPath, "//img[contains(@class,'superhero')]");
        private AtWebElement HeroWidget => _webDriver.FindElement(byHeroWidget);
        private AtWebElement HeroImage => _webDriver.FindElement(LocatorType.XPath, "(//img[contains(@class,'superhero')]/preceding::source)[1]");
        private AtBy byFAQSection => GetBy(LocatorType.XPath, "//h2[text()='Frequently Asked Questions']");
        private AtWebElement FAQSection => _webDriver.FindElement(byFAQSection);
        private ReadOnlyCollection<AtWebElement> FAQList => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id,'faq') and contains(@class,'accordion-header')]");
        private ReadOnlyCollection<AtWebElement> FAQHeader => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id,'faq') and contains(@class,'accordion-header')]/span");
        private ReadOnlyCollection<AtWebElement> FAQContent => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id,'faq') and contains(@class,'accordion-content')]/div[contains(@itemprop,'text')]");
        private ReadOnlyCollection<AtWebElement> FAQContentControlButton => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'accordion-header__button')]");
        private AtBy byFooterLinkWidget => GetBy(LocatorType.XPath, "//section[contains(@class,'2xl')]//a[contains(@class,'decoration-underline')]");
        private ReadOnlyCollection<AtWebElement> FooterLinkWidget => _webDriver.FindElements(byFooterLinkWidget);
        private AtBy byMapWidgetImage => GetBy(LocatorType.XPath, "//img[contains(@src,'maps')]");
        private AtWebElement MapWidgetImage => _webDriver.FindElement(byMapWidgetImage);
        private AtWebElement MapWidget => _webDriver.FindElement(LocatorType.XPath, "//img[contains(@src,'maps')]/parent::div");
        private AtBy byUSPOptions => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-usp') and @role='button']");
        private ReadOnlyCollection<AtWebElement> USPOptions => _webDriver.FindElements(byUSPOptions);
        private ReadOnlyCollection<AtWebElement> USPHeading => _webDriver.FindElements(LocatorType.XPath, "//h5[contains(@class,'usp__title')]");
        private ReadOnlyCollection<AtWebElement> USPDescription => _webDriver.FindElements(LocatorType.XPath, "//h5[contains(@class,'usp__title')]/following-sibling::p");
        private AtBy byKeyFactsSectionHeading => GetBy(LocatorType.XPath, "//div[contains(@class,'item--m-4')]//h4/ancestor::section//h3");
        private AtWebElement KeyFactsSectionHeading => _webDriver.FindElement(byKeyFactsSectionHeading);
        private AtWebElement KeyFactsSectionDescription => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'item--m-4')]//h4/ancestor::section//h3/following-sibling::span");
        private ReadOnlyCollection<AtWebElement> KeyFactsItemHeading => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'item--m-4')]//h4");
        private ReadOnlyCollection<AtWebElement> KeyFactsItemDescription => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'item--m-4')]//h4/ancestor::section//p");
        private ReadOnlyCollection<AtWebElement> KeyFactsItems => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'item--m-4')]//h4/parent::div");
        private AtWebElement WeatherWidgetHeader => _webDriver.FindElement(LocatorType.XPath, "//*[@role='listitem']/ancestor::section//h2");
        private AtWebElement WeatherWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "//*[@role='listitem']/ancestor::section//span");
        private ReadOnlyCollection<AtWebElement> WeatherMonthlyBar => _webDriver.FindElements(LocatorType.XPath, "//*[contains(@class,'chart-month') and @role='listitem']");
        private ReadOnlyCollection<AtWebElement> TemperatureChart => _webDriver.FindElements(LocatorType.XPath, "//*[contains(@class,'legend')]/parent::*[@role='listitem']");
        private AtBy byBestRoutesWidgetHeading => GetBy(LocatorType.XPath, "//section[contains(@class,'2xl')]//h3[contains(text(), 'Best Routes to Fly')]");
        private AtWebElement FlightsRoutesWidgetHeading => _webDriver.FindElement(byBestRoutesWidgetHeading);
        private AtWebElement FlightsRoutesWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "//section[contains(@class,'2xl')]//h3/following-sibling::span");
        private AtWebElement FlightsRoutesWidgetPreviousButton => _webDriver.FindElement(LocatorType.XPath, "//section[contains(@class,'2xl')]//button[contains(@aria-label, 'previous')]");
        private AtWebElement FlightsRoutesWidgetNextButton => _webDriver.FindElement(LocatorType.XPath, "//section[contains(@class,'2xl')]//button[contains(@aria-label, 'next')]");
        private ReadOnlyCollection<AtWebElement> FlightsRoutesContent => _webDriver.FindElements(LocatorType.XPath, "//section[contains(@class,'2xl')]//div[contains(@class,'--fill-space')]");
        private ReadOnlyCollection<AtWebElement> FlightsRoutesScrollerButton => _webDriver.FindElements(LocatorType.XPath, "//section[contains(@class,'2xl')]//ul/li/button");
        private AtWebElement BlogWidgetHeading => _webDriver.FindElement(LocatorType.XPath, "//h4[contains(@class,'--xl')]");
        private AtWebElement BlogWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "//h4[contains(@class,'--xl')]/following-sibling::span");
        private ReadOnlyCollection<AtWebElement> BlogContentHeading => _webDriver.FindElements(LocatorType.XPath, "//h4[contains(@class,'--xl')]//ancestor::section//article//a");
        private AtWebElement BlogWidgetPreviousButton => _webDriver.FindElement(LocatorType.XPath, "//h4[contains(@class,'--xl')]//ancestor::section//button[contains(@aria-label, 'previous')]");
        private AtWebElement BlogWidgetNextButton => _webDriver.FindElement(LocatorType.XPath, "//h4[contains(@class,'--xl')]//ancestor::section//button[contains(@aria-label, 'next')]");
        private ReadOnlyCollection<AtWebElement> BlogsScrollerButton => _webDriver.FindElements(LocatorType.XPath, "//h4[contains(@class,'--xl')]//ancestor::section//li/button");
        private AtWebElement ImageGalleryWidget => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'image-gallery-content')]");
        private AtWebElement ImageGalleryNextButton => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-gallery__nav']/button[2]");
        private AtWebElement ImageGalleryDescriptions => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-gallery__description']");
        private AtWebElement ImageGalleryFullScreenButton => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-gallery__fullscreen']");
        private AtWebElement ImageGalleryIndex => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-gallery__index']");
        //private AtWebElement ImageGallerySlides => _webDriver.FindElement(LocatorType.XPath, "//div[@class='image-gallery-slides']");
        private AtWebElement ImageGallerySlides => _webDriver.FindElement(LocatorType.XPath, "//div[@class='image-gallery-slides']");
        private AtWebElement InterlinkingWidgetSection => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]//ancestor::section");
        private AtWebElement InterlinkingWidgetHeading => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]//ancestor::section//h2");
        private AtWebElement InterlinkingWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]//ancestor::section//h2/following-sibling::span");
        private AtBy byInterlinkingWidgetTabs => GetBy(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]");
        private ReadOnlyCollection<AtWebElement> InterlinkingWidgetTabs => _webDriver.FindElements(byInterlinkingWidgetTabs);
        private ReadOnlyCollection<AtWebElement> InterlinkingWidgetCards => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article");
        private AtWebElement ImageOnInterlinkingWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//img", index);        
        private AtWebElement HotelNameOnInterlinkingWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//h3", index);
        private AtWebElement PriceOnInterlinkingWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//div[contains(@class,'color-accent')]", index);
        private AtWebElement LocationDescriptionOnInterlinkingWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//div[contains(@class,'raw-html-block')]", index);
        private AtBy byLocationDescriptionFromModal => GetBy(LocatorType.XPath, "//div[@class='sc-c-dialog-content__main']//div[contains(@class,'raw-html-block')]");
        private AtWebElement LocationDescriptionFromModal => _webDriver.FindElement(byLocationDescriptionFromModal);
        private AtWebElement MoreDescriptionButtonOnInterlinkingWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//span[@role='button']", index);
        private AtWebElement LocationDescriptionModalCloseButton => _webDriver.FindElement(LocatorType.CssSelector, "div[class*='sc-c-dialog'] button");
        private ReadOnlyCollection<AtWebElement> TopPicksOnInterlinkingWidget(string index) => _webDriver.FindElements(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//a[contains(@class,'underline')]", index);
        private AtWebElement SearchButtonOnInterlinkingWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//a[@role='button']", index);
        private AtWebElement InterlinkingWidgetNextButton => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]//ancestor::section//button[contains(@aria-label, 'next')]");
        private AtWebElement SearchButtonOnMoreDescriptionModal => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-dialog-content__footer']//a");
        private ReadOnlyCollection<AtWebElement> InterlinkingScrollerButton => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@id,'interlinking-tab')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//li/button");
        private AtWebElement ThemeWidgetSection => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'3xl')]/h2/ancestor::section)[3]");
        private AtWebElement ThemeWidgetHeading => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'3xl')]/h2/ancestor::section//h2)[3]");
        private AtWebElement ThemeWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'3xl')]/h2/ancestor::section//h2/following-sibling::span)[3]");
        private ReadOnlyCollection<AtWebElement> ThemeWidgetCards => _webDriver.FindElements(LocatorType.XPath, "(//div[contains(@class,'slick-list')]/ancestor::section)[3]//article");
        private AtWebElement HeadingOnThemeWidgetCard(string index) => _webDriver.FindElement(LocatorType.XPath, "((//div[contains(@class,'3xl')]/h2/ancestor::section)[3]//article)[#]//a[contains(@class,'xs')]", index);
        private AtWebElement DescriptionOnThemeWidgetCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//a[contains(@class,'3xs')]/h3/ancestor::section//article)[#]//a[contains(@class,'3xs')]/following-sibling::div", index);
        private AtWebElement ViewMoreButtonOnThemeWidgetCard(string index) => _webDriver.FindElement(LocatorType.XPath, "((//div[contains(@class,'3xl')]/h2/ancestor::section)[3]//article)[#]//a[contains(@class,'button')]", index);
        private AtWebElement ImageOnThemeWidgetCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//a[contains(@class,'3xs')]/h3/ancestor::section//article)[#]//img", index);
        private ReadOnlyCollection<AtWebElement> ThemeWidgetScrollerButton => _webDriver.FindElements(LocatorType.XPath, "(//div[contains(@class,'3xl')]/h2/ancestor::section)[3]//li/button");
        private AtWebElement SimilarDestinationsSection => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@id,'similar-destinations-panel-0')] | //span[text()=' Then, what about...']/ancestor::section");
        private AtWebElement SimilarDestinationsWidgetHeading => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'3xl')]/ancestor::section//h3[contains(@class, 'sc-u-margin-bottom-xs')])[3] | //span[contains(text(),'Then, what about')]/ancestor::section//h3[contains(@class, 'sc-u-margin-bottom-xs')]");
        private AtWebElement SimilarDestinationsWidgetDescription => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'3xl')]/ancestor::section//h3[contains(@class, 'sc-u-margin-bottom-xs')]/following-sibling::span)[3] | //span[contains(text(),'Then')]");
        private ReadOnlyCollection<AtWebElement> SimilarDestinationsWidgetTabs => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@id,'similar-destinations')]");
        private ReadOnlyCollection<AtWebElement> SimilarDestinationsWidgetHolidayCards => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@id,'similar-destinations')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article");
        private AtWebElement HeadingOnSimilarDestinationsWidgetHolidayCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'similar-destinations')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//a[contains(@class,'xs')]", index);
        private AtWebElement DescriptionOnSimilarDestinationsWidgetHolidayCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'similar-destinations')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//a[contains(@class,'xs')]/following-sibling::div", index);
        private AtWebElement SelectHolidayCardOnSimilarDestinationsWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'similar-destinations')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//a[contains(@class,'button')]", index);
        private AtWebElement ImageOnSimilarDestinationsWidgetHolidayCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//button[contains(@id,'similar-destinations')]//ancestor::section//div[@role='tabpanel' and not(@hidden)]//article)[#]//img", index);
        private ReadOnlyCollection<AtWebElement> SimilarDestinationsWidgetScrollerButton => _webDriver.FindElements(LocatorType.XPath, "//span[contains(text(),'Then, what about')]//ancestor::section//li/button | //div[contains(@id,'similar-destination')]//ancestor::section//li/button");
        private ReadOnlyCollection<AtWebElement> SimilarDestinationsWidgetHotelCards => _webDriver.FindElements(LocatorType.XPath, "//span[contains(text(),'Then, what about')]//ancestor::div[contains(@class,'sc-c-container')]//div[@class='slick-list']//article");
        private AtWebElement HeadingOnSimilarDestinationsWidgetHotelCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'slick-slide')]//a[contains(@class,'xs')])[#]", index);
        private AtWebElement DescriptionOnSimilarDestinationsWidgetHotelCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'slick-slide')]//a/h4//ancestor::article)[#]//a[contains(@class,'3xs')]/following-sibling::div", index);
        private AtWebElement SelectHotelCardOnSimilarDestinationsWidget(string index) => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'slick-slide')]//a[contains(@class, 'xs')]//ancestor::article)[#]//a[contains(@class,'button')]", index);
        private AtWebElement ImageOnSimilarDestinationsWidgetHotelCard(string index) => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'slick-slide')]//a[contains(@class,'xs')]//ancestor::article)[#]//img", index);
        #endregion


        #region[Methods]
        public void OpenSearch()
        {
            OpenSearchButton.Click();
        }
        public void CloseSearch()
        {
            CloseSearchButton.ClickButtonUsingJs();
        }

        public void SelectOverviewTab()
        {
            OverviewTab.ClickButtonUsingJs();
        }

        public void SelectOffersTab()
        {
            OffersTab.ClickButtonUsingJs();
        }

        public bool IsOverviewAndOffersTabDisplayed()
        {
            return OverviewTab.Visible && OffersTab.Visible;
        }

        public bool IsSearchSectionDisplayed()
        {
            return SearchText.Visible && OpenSearchButton.Visible;
        }

        public string GetHeadlinePriceWidgetText()
        {
            _webDriver.WaitForElementVisible(byHeadlinePriceWidget, Constants.ShortWait, "Head line price widget is not visible");
            return HeadlinePriceWidget.Text;
        }

        public string GetIntroWidgetHeading()
        {
            _webDriver.WaitForElementVisible(byIntroWidget, Constants.ShortWait, "Intro widget is not visible");
            return IntroWidgetHeading.Text;
        }

        public string GetIntroWidgetDescription()
        {
            _webDriver.WaitForElementVisible(byIntroWidget, Constants.ShortWait, "Intro widget is not visible");
            return IntroWidgetDescription.Text;
        }

        public bool IsIntroTextFaderVisible()
        {
            _webDriver.WaitForElementVisible(byIntroWidget, Constants.ShortWait, "Intro widget is not visible");
            return IntroWidgetTextFader.Visible;
        }

        public bool IsIntroWidgetShowMoreButtonVisible()
        {
            _webDriver.WaitForElementVisible(byIntroWidget, Constants.ShortWait, "Intro widget is not visible");
            return IntroWidgetShowMoreButton.Visible;
        }

        public bool IsIntroWidgetShowLessButtonVisible()
        {
            _webDriver.WaitForElementVisible(byIntroWidget, Constants.ShortWait, "Intro widget is not visible");
            return IntroWidgetShowLessButton.Visible;
        }

        public void ClickIntroWidgetShowMoreButton()
        {
            _webDriver.ScrollElementToCenter(IntroWidgetShowMoreButton);
            IntroWidgetShowMoreButton.Click();
        }

        public void ClickIntroWidgetShowLessButton()
        {
            _webDriver.ScrollElementToCenter(IntroWidgetShowLessButton);
            IntroWidgetShowLessButton.Click();
        }

        public bool IsIntroWidgetLinksClickable()
        {
            bool IsLinksClickable = true;
            foreach (var link in IntroWidgetLinks)
            {
                if (!link.GetAttribute("href").Contains("http"))
                    IsLinksClickable = false;
            }
            return IsLinksClickable;
        }

        public bool IsHeroWidgetDisplayed()
        {
            return HeroWidget.Visible;
        } 

        public string GetHeroImageURL()
        {
            _webDriver.WaitForElementVisible(byHeroWidget, 30, "hero widget is not visible");
            return HeroImage.GetAttribute("srcset");
        }

        public int GetHeroImageLocation()
        {
            return HeroImage.Location.Y;
        }

        public int GetIntroWidgetLocation()
        {
            return IntroWidget.Location.Y;
        }

        public bool IsFAQWidgetVisible()
        {
            return FAQSection.Visible;
        }

        public int GetFAQCount()
        {
            return FAQList.Count;
        }

        public string GetFAQHeader(int listNumber)
        {
            _webDriver.ScrollToElement(FAQHeader[listNumber]);
            return FAQHeader[listNumber].Text;
        }

        public string GetFAQContent(int listNumber)
        {
            Thread.Sleep(1000);
            _webDriver.ScrollToElement(FAQContent[listNumber]);
            return FAQContent[listNumber].Text;
        }        
        
        public void ExpandFAQContent(int listNumber)
        {
            if(FAQList[listNumber].GetAttribute("aria-expanded") == "false")
            {
                _webDriver.ScrollElementToCenter(FAQList[listNumber]);
                FAQContentControlButton[listNumber].Click();
            }                
        }

        public void CollapseFAQContent(int listNumber)
        {
            if (FAQList[listNumber].GetAttribute("aria-expanded") == "true")
            {
                _webDriver.ScrollElementToCenter(FAQList[listNumber]);
                 FAQContentControlButton[listNumber].Click();
            }                
        }

        public bool IsFAQContentExpanded(int listNumber)
        {
            return (FAQList[listNumber].GetAttribute("aria-expanded") == "true");           
        }        

        public bool IsFooterLinkWidgetDisplayed()
        {
            return FooterLinkWidget.Count > 0;
        }

        public int GetFooterLinkWidgetCount()
        {
            return FooterLinkWidget.Count;
        }

        public List<string> GetFooterWidgetLinks()
        {
            List<string> footerLinks = new List<string>();
            foreach(var link in FooterLinkWidget)
            {
                footerLinks.Add(link.Text);
            }
            return footerLinks;
        }

        public bool ValidateHolidayFooterWidgetLinksNavigation(string location)
        {
            bool isLinksValidated = true;
            _webDriver.ScrollToElement(FooterLinkWidget[0]);
            FooterLinkWidget[0].Click();
            isLinksValidated = GetCurrentURL().Contains("hotels-in-" + location.ToLower());
            NavigateBack();
            return isLinksValidated;
        }

        public bool ValidateHotelFooterWidgetLinksNavigation(string location)
        {
            bool isLinksValidated = true;
            _webDriver.ScrollElementToCenter(FooterLinkWidget[0]);
            FooterLinkWidget[0].Click();
            isLinksValidated = GetCurrentURL().Contains("holidays-in-" + location.ToLower());
            NavigateBack();

            _webDriver.ScrollElementToCenter(FooterLinkWidget[1]);
            FooterLinkWidget[1].Click();
            isLinksValidated = GetCurrentURL().Contains("sitemap") && GetCurrentURL().Contains("hotels-in-" + location.ToLower());
            NavigateBack();
            return isLinksValidated;
        }

        public bool IsMapWidgetDisplayed()
        {
            return MapWidget.Displayed;
        }

        public bool IsMapWidgetInteractable()
        {
            bool isMapClickable = false;
            isMapClickable = MapWidgetImage.GetAttribute("href") != null;

            string currentURL = GetCurrentURL();
            _webDriver.ScrollElementToCenter(MapWidgetImage);
            MapWidgetImage.Click();
            if (GetCurrentURL() == currentURL)
                isMapClickable = false;
            else
                isMapClickable = true;

            return isMapClickable;
        }

        public List<string> GetUSPHeadings()
        {
            List<string> uspHeading = new List<string>();
            foreach (var usp in USPHeading)
            {
                uspHeading.Add(usp.Text);
            }
            return uspHeading;
        }

        public List<string> GetUSPDescriptions()
        {
            List<string> uspDescription = new List<string>();
            foreach (var usp in USPDescription)
            {
                uspDescription.Add(usp.Text);
            }
            return uspDescription;
        }

        public void ClickUSP(int index)
        {
            _webDriver.ScrollElementToCenter(USPOptions[index]);
            USPOptions[index].Click();
        }

        public List<string> GetKeyFactsItemHeadings()
        {
            List<string> headings = new List<string>();
            foreach (var keyFact in KeyFactsItemHeading)
            {
                headings.Add(keyFact.Text);
            }
            return headings;
        }

        public List<string> GetKeyFactsItemDescriptions()
        {
            List<string> descriptions = new List<string>();
            foreach (var keyFact in KeyFactsItemDescription)
            {
                descriptions.Add(keyFact.Text);
            }
            return descriptions;
        }

        public string GetKeyFactsSectionHeading()
        {
            return KeyFactsSectionHeading.Text;
        }

        public string GetKeyFactsSectionDescription()
        {
            return KeyFactsSectionDescription.Text;
        }

        public bool IsKeyFactsItemsAlignedToCenter()
        {
            bool isAligned = true;
            foreach(var keyFacts in KeyFactsItems)
            {
                isAligned = keyFacts.GetAttribute("class").Contains("align-center");
            }
            return isAligned;
        }

        public string GetWeatherWidgetHeading()
        {
            _webDriver.ScrollElementToCenter(WeatherWidgetHeader);
            return WeatherWidgetHeader.Text;
        }

        public string GetWeatherWidgetDescription()
        {
            return WeatherWidgetDescription.Text;
        }

        public int GetWeatherMonthlyBarCount()
        {
            return WeatherMonthlyBar.Count;
        }

        public int GetSelectedWeatherWidget()
        {         
            if(selectedWeatherWidgetMonth < 0)
            {
                for (int count = 0; count < WeatherMonthlyBar.Count; count++)
                {
                    if (WeatherMonthlyBar[count].GetAttribute("Class").Contains("active"))
                        selectedWeatherWidgetMonth = count + 1;
                }
            }            
            return selectedWeatherWidgetMonth; 
        }

        public Dictionary<string, bool> GetMonthlyWeatherWigetMonthlyBarStatus()
        {
            Dictionary<string, bool> monthlyBarStatus = new Dictionary<string, bool>();
            foreach (AtWebElement element in WeatherMonthlyBar)
            {
                monthlyBarStatus.Add(element.Text, element.GetAttribute("Class").Contains("active"));
            }
            return monthlyBarStatus;
        }

        public bool ValidateWeatherWidgetSelection(int selectedMonth)
        {
            bool isSelectedMonthValidated = true;
            bool isNotSelectedMonthValidated = true;

            for (int count = 0; count < WeatherMonthlyBar.Count; count++)
            {
                if (count == selectedMonth - 1)
                    isSelectedMonthValidated = WeatherMonthlyBar[count].GetAttribute("Class").Contains("active");
                else
                    isNotSelectedMonthValidated = !WeatherMonthlyBar[count].GetAttribute("Class").Contains("active");
            }
            return isSelectedMonthValidated && isNotSelectedMonthValidated;
        }

        public bool IsWeatherWidgetSelected(int month)
        {
            return WeatherMonthlyBar[month-1].GetAttribute("Class").Contains("active");
        }

        public void SelectRandomMonthBar()
        {
            int selectedMonth = GetSelectedWeatherWidget();
            int random = selectedMonth;
            while (random == selectedMonth)
            {
                random = HelperFunctions.RandomNumber(12, 1);
            }
            SelectWeatherMonth(random);
        }

        public void SelectWeatherMonth(int month)
        {
            _webDriver.ScrollElementToCenter(WeatherMonthlyBar[month - 1]);
            WeatherMonthlyBar[month-1].Click();
            selectedWeatherWidgetMonth = month;
        }

        public string GetAverageTemperatureOnMonthlyBar(int month)
        {
            return WeatherMonthlyBar[month-1].Text.Split("\r\n")[0];
        }

        public string GetAverageTemperatureOnTemperatureChart()
        {
            return TemperatureChart[1].Text.Split("\r\n")[0];
        }

        public bool IsFlightsRouteAvailable()
        {
            return FlightsRoutesWidgetHeading.Visible;
        }

        public string GetFlightsRoutesWidgetHeading()
        {
            return FlightsRoutesWidgetHeading.Text;
        }

        public string GetFlightstRoutesWidgetDescription()
        {
            return FlightsRoutesWidgetDescription.Text;
        }

        public bool ValidateFlightRoutesContentCountPerPage(int expectedCountPerPage)
        {
            bool isContentCountValidated = true;
            int count = 0;
            _webDriver.ScrollElementToCenter(FlightsRoutesScrollerButton[count]);
            while (FlightsRoutesWidgetNextButton.GetAttribute("disabled") == null)
            {               
                FlightsRoutesScrollerButton[count].Click();
                Thread.Sleep(1000);
                for(int i=0; i<expectedCountPerPage; i++)
                {
                    isContentCountValidated = FlightsRoutesContent[(count * expectedCountPerPage) + i].Displayed;
                }               
                count++;
            }
            return isContentCountValidated;
        }

        public string GetBlogWidgetHeading()
        {
            return BlogWidgetHeading.Text;
        }

        public bool IsBlogWidgetDescriptionDisplayed()
        {
            return BlogWidgetDescription.Visible;
        }

        public string GetBlogWidgetDescription()
        {
            return BlogWidgetDescription.Text;
        }

        public bool ValidateBlogWidget()
        {
            bool isBlogWidgetValidated = true;
            _webDriver.ScrollElementToCenter(BlogsScrollerButton[0]);
            int scrollerCount = 0;
            for (int count=0; count<BlogContentHeading.Count; count++)
            {                
                if (!BlogContentHeading[count].Visible)
                {
                    while (!BlogContentHeading[count].Visible)
                    {
                        BlogsScrollerButton[scrollerCount+1].Click();
                        Thread.Sleep(1000);
                        scrollerCount++;
                    }
                }
                isBlogWidgetValidated = BlogContentHeading[count].GetAttribute("href").Contains("blog");
            }            
            return isBlogWidgetValidated;
        }

        public bool IsImageGalleryWidgetDisplayed()
        {
            return ImageGalleryWidget.Displayed;
        }

        public void ClickNextButtonOnImageGallery()
        {
            _webDriver.ScrollElementToCenter(ImageGalleryWidget);
            if (HelperFunctions.IsDesktop())
            {
                if (ImageGalleryNextButton.GetAttribute("disabled") == null)
                {
                    ImageGalleryNextButton.Click();
                    Thread.Sleep(1000);
                }
            }
            else
            {
                ImageGallerySlides.SendKeysUsingActions(OpenQA.Selenium.Keys.Right);
                Thread.Sleep(1000);
            }                           
        }

        public string GetImageGalleryDescription()
        {
            return ImageGalleryDescriptions.Text;
        }

        public string GetImagesCount()
        {
            return ImageGalleryIndex.Text.Split("/")[1];
        }

        public string GetFullScreenText()
        {
            return ImageGalleryFullScreenButton.Text;
        }

        public void ClickFullScreenButton()
        {
            ImageGalleryFullScreenButton.Click();
        }

        public bool IsImageDisplayedInFullScreen()
        {
            return ImageGalleryWidget.GetAttribute("class").Contains("fullscreen", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsInterlinkingWidgetDisplayed()
        {
            return InterlinkingWidgetSection.Displayed;
        }

        public string GetInterlinkingWidgetHeading()
        {
            return InterlinkingWidgetHeading.Text;
        }

        public string GetInterlinkingWidgetDescription()
        {
            return InterlinkingWidgetDescription.Text;
        }

        public int GetInterlinkingTabsCount()
        {
            return InterlinkingWidgetTabs.Count;
        }

        public List<string> GetInterlinkingTabsText()
        {
            List<string> tabsList = new List<string>();
            foreach(var tabs in InterlinkingWidgetTabs)
            {
                tabsList.Add(tabs.Text);
            }
            return tabsList;
        }

        public int GetInterlinkingHotelCardsCount()
        {
            return InterlinkingWidgetCards.Count;
        }

        public void SelectInterlinkingTab(int index)
        {
            _webDriver.WaitForElementVisible(byInterlinkingWidgetTabs, Constants.ShortWait, "InterLInking Widget is not visible");
            _webDriver.ScrollElementToCenter(InterlinkingWidgetTabs[index]);
            if (!InterlinkingWidgetTabs[index].GetAttribute("aria-selected").Contains("true"))
                InterlinkingWidgetTabs[index].Click();
        }
        public int GetHotelToSelectOnInterlinkingWidget()
        {
            if (hotelToSelect == 0)
                RandomizeHotelToSelect(GetInterlinkingHotelCardsCount());
            return hotelToSelect;
        }

        public int RandomizeHotelToSelect(int totalHotelCount)
        {            
            hotelToSelect = HelperFunctions.RandomNumber(totalHotelCount);
            return hotelToSelect;
        }

        public string GetHotelNameOnInterlinkingWidget(int hotelToSelect)
        {
            return HotelNameOnInterlinkingWidget(hotelToSelect.ToString()).Text;
           // return InterlinkingWidgetCards[hotelToSelect].FindElement(HotelNameOnInterlinkingWidget).Text;
        }

        public string GetPriceOnInterlinkingWidget(int hotelToSelect)
        {
            return PriceOnInterlinkingWidget(hotelToSelect.ToString()).Text;
            //return InterlinkingWidgetCards[hotelToSelect].FindElement(PriceOnInterlinkingWidget).Text;
        }

        public string GetImageSourceOnInterLinkingWidget(int hotelToSelect)
        {
            return ImageOnInterlinkingWidget(hotelToSelect.ToString()).GetAttribute("src");
        }

        public void ScrollToHotelCardOnInterlinkingWidget(int hotelToSelect)
        {
            InterlinkingScrollerButton[0].Click();
            int count = 1;
            Thread.Sleep(1000);
            while (!InterlinkingWidgetCards[hotelToSelect].Visible)
            {
                InterlinkingScrollerButton[count].Click();
                Thread.Sleep(1000);
                count++;
            }
        }

        public bool IsTopPicksDisplayed(int hotelToSelect)
        {
            return TopPicksOnInterlinkingWidget(hotelToSelect.ToString()).Count > 0;
        }

        public Dictionary<string, string> GetTopPicksLocationsAndLinks(int hotelToSelect)
        {
            Dictionary<string, string> topPicks = new Dictionary<string, string>();

            foreach (AtWebElement element in TopPicksOnInterlinkingWidget(hotelToSelect.ToString()))
            {
                topPicks.Add(element.Text, element.GetAttribute("href"));
            }
            return topPicks;
        }

        public string ClickAndReturnTopPickLocation(int hotelToSelect, int linkToSelect)
        {
            ScrollToHotelCardOnInterlinkingWidget(hotelToSelect);
            string location = TopPicksOnInterlinkingWidget(hotelToSelect.ToString())[linkToSelect].Text;
            TopPicksOnInterlinkingWidget(hotelToSelect.ToString())[linkToSelect].Click();
            return location;
        }

        public void ClickLocationDescriptionMoreButton(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(MoreDescriptionButtonOnInterlinkingWidget(hotelToSelect + 1.ToString()));
            MoreDescriptionButtonOnInterlinkingWidget(hotelToSelect + 1.ToString()).Click();
            _webDriver.WaitForElementVisible(byLocationDescriptionFromModal, 10, "modal is not displayed");
        }

        public string GetLocationDescriptionOnInterlinkingWidget(int hotelToSelect)
        {
            ClickLocationDescriptionMoreButton(hotelToSelect);
            return LocationDescriptionFromModal.Text;
        }

        public void CloseLocationDescriptionModal()
        {
            LocationDescriptionModalCloseButton.Click();
        }

        public bool IsLocationDescriptionModalDisplayed()
        {
            return LocationDescriptionFromModal.Displayed;
        }

        public int GetHotelWithMoreDescriptionLink()
        {
            int hotelToSelect = -1;

            for(int count=0; count< InterlinkingWidgetCards.Count; count++)
            {
                ScrollToHotelCardOnInterlinkingWidget(count + 1);
                if (MoreDescriptionButtonOnInterlinkingWidget(count + 1.ToString()).Visible)
                {
                    hotelToSelect = count;
                    break;
                }                 

            }
            return hotelToSelect;
        }

        public string GetLocationOnSearchButtonOnMoreDescriptionModal()
        {
            return SearchButtonOnMoreDescriptionModal.Text;
        }

        public void ClickSearchButtonOnMoreDescriptionModal()
        {
            SearchButtonOnMoreDescriptionModal.Click();
        }

        public void ClickOnInterlinkingWidgetImage(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(ImageOnInterlinkingWidget(hotelToSelect.ToString()));
            ImageOnInterlinkingWidget(hotelToSelect.ToString()).ClickUsingActions();
        }

        public void ClickSearchButtonOnInterlinkingWidget(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(SearchButtonOnInterlinkingWidget(hotelToSelect.ToString()));
            SearchButtonOnInterlinkingWidget(hotelToSelect.ToString()).Click();
        }

        public bool IsThemeWidgetDisplayed()
        {
            return ThemeWidgetSection.Displayed;
        }

        public string GetThemeWidgetHeading()
        {
            return ThemeWidgetHeading.Text;
        }

        public string GetThemeWidgetDescription()
        {
            return ThemeWidgetDescription.Text;
        }

        public int GetThemeWidgetCardsCount()
        {
            return ThemeWidgetCards.Count;
        }

        public int GetCardToSelectOnThemeWidget()
        {
            if (hotelToSelect == 0)
                RandomizeHotelToSelect(GetThemeWidgetCardsCount());
            return hotelToSelect;
        }

        public string GetCardHeadingOnThemeWidget(int cardToSelect)
        {
            return HeadingOnThemeWidgetCard(cardToSelect.ToString()).Text;
        }

        public string GetCardDescriptionOnThemeWidget(int cardToSelect)
        {
            return DescriptionOnThemeWidgetCard(cardToSelect.ToString()).Text;
        }

        public bool IsDescriptionDisplayedOnThemeWidgetCard(int cardToSelect)
        {
            return DescriptionOnThemeWidgetCard(cardToSelect.ToString()).Displayed;
        }

        public string GetImageSourceOnThemeWidget(int cardToSelect)
        {
            return ImageOnThemeWidgetCard(cardToSelect.ToString()).GetAttribute("src");
        }

        public string GetCardLinkOnThemeWidget(int cardToSelect)
        {
            return HeadingOnThemeWidgetCard(cardToSelect.ToString()).GetAttribute("href");
        }

        public void ScrollToCardOnThemeWidget(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(ThemeWidgetScrollerButton[0]);
            ThemeWidgetScrollerButton[0].ClickButtonUsingJs();
            int count = 1;
            Thread.Sleep(1000);
            while (!ThemeWidgetCards[cardToSelect].Visible)
            {
                ThemeWidgetScrollerButton[count].ClickButtonUsingJs();
                Thread.Sleep(1000);
                count++;
            }
        }

        public void ClickViewMoreButtonOnThemeWidget(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(ViewMoreButtonOnThemeWidgetCard(cardToSelect.ToString()));
            ViewMoreButtonOnThemeWidgetCard(cardToSelect.ToString()).Click();
        }

        public int GetSimilarDestinationsTabCount()
        {
            return SimilarDestinationsWidgetTabs.Count;
        }

        public bool IsSimilarDestinationsTabDisplayed()
        {
            return SimilarDestinationsWidgetTabs.Count>0;
        }

        public List<string> GetSimilarDestinationsTabText()
        {
            List<string> tabsList = new List<string>();
            foreach (var tabs in SimilarDestinationsWidgetTabs)
            {
                tabsList.Add(tabs.Text);
            }
            return tabsList;
        }
        public void SelectSimilarDestinationsTab(int index)
        {
            if (!SimilarDestinationsWidgetTabs[index].GetAttribute("aria-selected").Contains("true"))
                _webDriver.ScrollElementToCenter(SimilarDestinationsWidgetTabs[index]);
                SimilarDestinationsWidgetTabs[index].ClickButtonUsingJs();
        }

        public bool IsSimilarDestinationsWidgetDisplayed()
        {
            return SimilarDestinationsSection.Displayed;
        }

        public string GetSimilarDestinationsWidgetHeading()
        {
            return SimilarDestinationsWidgetHeading.Text;
        }

        public string GetSimilarDestinationsWidgetDescription()
        {
            return SimilarDestinationsWidgetDescription.Text;
        }

        public int GetSimilarDestinationsWidgetHolidaysCardsCount()
        {
            return SimilarDestinationsWidgetHolidayCards.Count;
        }

        public int GetHolidayCardToSelectOnSimilarDestinationsWidget()
        {
            if (hotelToSelect == 0)
                RandomizeHotelToSelect(GetSimilarDestinationsWidgetHolidaysCardsCount());
            return hotelToSelect;
        }

        public string GetHeadingOnSimilarDestinationsWidgetHolidayCard(int cardToSelect)
        {
            return HeadingOnSimilarDestinationsWidgetHolidayCard(cardToSelect.ToString()).Text;
        }

        public string GetDescriptionOnSimilarDestinationsWidgetHolidayCard(int cardToSelect)
        {
            return DescriptionOnSimilarDestinationsWidgetHolidayCard(cardToSelect.ToString()).Text;

        }

        public string GetImageOnSimilarDestinationsWidgetHolidayCard(int cardToSelect)
        {
            return ImageOnSimilarDestinationsWidgetHolidayCard(cardToSelect.ToString()).GetAttribute("src");
        }

        public string GetHolidayCardLinkOnSimilarDestinationsWidget(int cardToSelect)
        {
            return HeadingOnSimilarDestinationsWidgetHolidayCard(cardToSelect.ToString()).GetAttribute("href");
        }

        public void ScrollToHolidayCardOnSimilarDestinationsWidget(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(SimilarDestinationsWidgetScrollerButton[0]);
            SimilarDestinationsWidgetScrollerButton[0].ClickButtonUsingJs();
            int count = 1;
            Thread.Sleep(1000);
            while (!SimilarDestinationsWidgetHolidayCards[cardToSelect].Visible)
            {
                SimilarDestinationsWidgetScrollerButton[count].ClickButtonUsingJs();
                Thread.Sleep(1000);
                count++;
            }
        }

        public void ClickSelectHolidayCardOnSimilarDestinationsWidget(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(SelectHolidayCardOnSimilarDestinationsWidget(cardToSelect.ToString()));
            SelectHolidayCardOnSimilarDestinationsWidget(cardToSelect.ToString()).Click();
        }

        public void ClickOnSimilarDestinationsWidgetHolidayImage(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(ImageOnSimilarDestinationsWidgetHolidayCard(cardToSelect.ToString()));
            ImageOnSimilarDestinationsWidgetHolidayCard(cardToSelect.ToString()).ClickUsingActions();
        }

        public int GetSimilarDestinationsWidgetHotelCardsCount()
        {
            return SimilarDestinationsWidgetHotelCards.Count;
        }

        public int GetHotelCardToSelectOnSimilarDestinationsWidget()
        {
            if (hotelToSelect == 0)
                RandomizeHotelToSelect(GetSimilarDestinationsWidgetHotelCardsCount());
            return hotelToSelect;
        }

        public string GetHeadingOnSimilarDestinationsWidgetHotelCard(int cardToSelect)
        {
            return HeadingOnSimilarDestinationsWidgetHotelCard(cardToSelect.ToString()).Text;
        }

        public string GetDescriptionOnSimilarDestinationsWidgetHotelCard(int cardToSelect)
        {
            return DescriptionOnSimilarDestinationsWidgetHotelCard(cardToSelect.ToString()).Text;

        }

        public string GetImageOnSimilarDestinationsWidgetHotelCard(int cardToSelect)
        {
            return ImageOnSimilarDestinationsWidgetHotelCard(cardToSelect.ToString()).GetAttribute("src");
        }

        public string GetHotelCardLinkOnSimilarDestinationsWidget(int cardToSelect)
        {
            return HeadingOnSimilarDestinationsWidgetHotelCard(cardToSelect.ToString()).GetAttribute("href");
        }

        public void ScrollToHotelCardOnSimilarDestinationsWidget(int cardToSelect)
        {
            _webDriver.ScrollToElement(SimilarDestinationsSection);
            int count = 1;
            while (!SimilarDestinationsWidgetHotelCards[cardToSelect].Visible)
            {
                SimilarDestinationsWidgetScrollerButton[count].Click();
                Thread.Sleep(1000);
                count++;
            }
        }

        public void ClickSelectHotelCardOnSimilarDestinationsWidget(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(SelectHotelCardOnSimilarDestinationsWidget(cardToSelect.ToString()));
            SelectHotelCardOnSimilarDestinationsWidget(cardToSelect.ToString()).Click();
        }

        public void ClickOnSimilarDestinationsWidgetHotelImage(int cardToSelect)
        {
            _webDriver.ScrollElementToCenter(ImageOnSimilarDestinationsWidgetHotelCard(cardToSelect.ToString()));
            ImageOnSimilarDestinationsWidgetHotelCard(cardToSelect.ToString()).ClickUsingActions();
        }

        #endregion
    }
}
