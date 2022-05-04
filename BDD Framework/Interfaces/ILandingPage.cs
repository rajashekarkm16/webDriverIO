using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface ILandingPage
    {
        //Search widget methods:
        void OpenSearch();
        void CloseSearch();
        bool IsSearchSectionDisplayed();
        //Overview and Offers tab methods:
        bool IsOverviewAndOffersTabDisplayed();
        void SelectOffersTab();
        void SelectOverviewTab();
        //Hero Widget Methods:
        bool IsHeroWidgetDisplayed();
        string GetHeroImageURL();
        int GetHeroImageLocation();
        //Headline Price Widget Method:
        string GetHeadlinePriceWidgetText();
        //Intro Widget Methods:
        string GetIntroWidgetHeading();
        string GetIntroWidgetDescription();
        bool IsIntroTextFaderVisible();
        bool IsIntroWidgetShowMoreButtonVisible();
        bool IsIntroWidgetShowLessButtonVisible();
        void ClickIntroWidgetShowMoreButton();
        void ClickIntroWidgetShowLessButton();
        bool IsIntroWidgetLinksClickable();
        int GetIntroWidgetLocation();
        //FAQ widget methods:
        bool IsFAQWidgetVisible();
        int GetFAQCount();
        string GetFAQHeader(int ListNumber);
        string GetFAQContent(int ListNumber);
        void ExpandFAQContent(int ListNumber);
        void CollapseFAQContent(int ListNumber);
        //Footer Link widget methods:
        bool IsFooterLinkWidgetDisplayed();
        int GetFooterLinkWidgetCount();
        List<string> GetFooterWidgetLinks();
        bool ValidateHolidayFooterWidgetLinksNavigation(string location);
        bool ValidateHotelFooterWidgetLinksNavigation(string location);
        bool IsFAQContentExpanded(int listNumber);
        //Map widget methods:
        bool IsMapWidgetDisplayed();
        bool IsMapWidgetInteractable();
        //USP widget methods:
        List<string> GetUSPHeadings();
        List<string> GetUSPDescriptions();
        void ClickUSP(int index);
        //Key facts wiget methods:
        string GetKeyFactsSectionHeading();
        string GetKeyFactsSectionDescription();
        List<string> GetKeyFactsItemHeadings();
        List<string> GetKeyFactsItemDescriptions();
        bool IsKeyFactsItemsAlignedToCenter();
        //Weather widget methods:
        string GetWeatherWidgetHeading();
        string GetWeatherWidgetDescription();
        int GetWeatherMonthlyBarCount();
        int GetSelectedWeatherWidget();
        void SelectRandomMonthBar();
        bool ValidateWeatherWidgetSelection(int selectedMonth);
        void SelectWeatherMonth(int month);
        bool IsWeatherWidgetSelected(int month);
        string GetAverageTemperatureOnMonthlyBar(int month);
        string GetAverageTemperatureOnTemperatureChart();
        bool IsFlightsRouteAvailable();
        string GetFlightsRoutesWidgetHeading();
        string GetFlightstRoutesWidgetDescription();
        bool ValidateFlightRoutesContentCountPerPage(int expectedCountPerPage = Constants.FlyingRoutesPerPage);
        //Blog widget methods:
        string GetBlogWidgetHeading();
        bool IsBlogWidgetDescriptionDisplayed();
        string GetBlogWidgetDescription();
        bool ValidateBlogWidget();
        //Image gallery widget methods:
        bool IsImageGalleryWidgetDisplayed();
        void ClickNextButtonOnImageGallery();
        string GetImageGalleryDescription();
        string GetImagesCount();
        string GetFullScreenText();
        void ClickFullScreenButton();
        bool IsImageDisplayedInFullScreen();
        //Interlinking widget methods:
        bool IsInterlinkingWidgetDisplayed();
        string GetInterlinkingWidgetHeading();
        string GetInterlinkingWidgetDescription();
        int GetInterlinkingTabsCount();
        List<string> GetInterlinkingTabsText();
        int GetInterlinkingHotelCardsCount();
        void SelectInterlinkingTab(int index);
        int GetHotelToSelectOnInterlinkingWidget();
        int RandomizeHotelToSelect(int totalHotelCount);
        string GetHotelNameOnInterlinkingWidget(int hotelToSelect);
        string GetPriceOnInterlinkingWidget(int hotelToSelect);
        string GetImageSourceOnInterLinkingWidget(int hotelToSelect);
        string GetCardLinkOnThemeWidget(int cardToSelect);
        void ScrollToHotelCardOnInterlinkingWidget(int hotelToSelect);
        bool IsTopPicksDisplayed(int hotelToSelect);
        Dictionary<string, string> GetTopPicksLocationsAndLinks(int hotelToSelect);
        string ClickAndReturnTopPickLocation(int hotelToSelect, int linkToSelect);
        string GetLocationDescriptionOnInterlinkingWidget(int hotelToSelect);
        bool IsLocationDescriptionModalDisplayed();
        int GetHotelWithMoreDescriptionLink();
        void CloseLocationDescriptionModal();
        string GetLocationOnSearchButtonOnMoreDescriptionModal();
        void ClickSearchButtonOnMoreDescriptionModal();
        void ClickOnInterlinkingWidgetImage(int hotelToSelect);
        void ClickSearchButtonOnInterlinkingWidget(int hotelToSelect);
        //Theme widget methods:
        bool IsThemeWidgetDisplayed();
        string GetThemeWidgetHeading();
        string GetThemeWidgetDescription();
        int GetThemeWidgetCardsCount();
        int GetCardToSelectOnThemeWidget();
        string GetCardHeadingOnThemeWidget(int cardToSelect);
        string GetCardDescriptionOnThemeWidget(int cardToSelect);
        string GetImageSourceOnThemeWidget(int cardToSelect);
        void ScrollToCardOnThemeWidget(int cardToSelect);
        void ClickViewMoreButtonOnThemeWidget(int cardToSelect);
        //Similar Destinations methods:
        void SelectSimilarDestinationsTab(int index);
        List<string> GetSimilarDestinationsTabText();
        bool IsSimilarDestinationsTabDisplayed();
        int GetSimilarDestinationsTabCount();
        bool IsSimilarDestinationsWidgetDisplayed();
        string GetSimilarDestinationsWidgetHeading();
        string GetSimilarDestinationsWidgetDescription();
        int GetSimilarDestinationsWidgetHolidaysCardsCount();
        int GetHolidayCardToSelectOnSimilarDestinationsWidget();
        string GetHeadingOnSimilarDestinationsWidgetHolidayCard(int cardToSelect);
        string GetDescriptionOnSimilarDestinationsWidgetHolidayCard(int cardToSelect);
        string GetImageOnSimilarDestinationsWidgetHolidayCard(int cardToSelect);
        string GetHolidayCardLinkOnSimilarDestinationsWidget(int cardToSelect);
        void ScrollToHolidayCardOnSimilarDestinationsWidget(int cardToSelect);
        void ClickSelectHolidayCardOnSimilarDestinationsWidget(int cardToSelect);
        void ClickOnSimilarDestinationsWidgetHolidayImage(int cardToSelect);
        int GetSimilarDestinationsWidgetHotelCardsCount();
        int GetHotelCardToSelectOnSimilarDestinationsWidget();
        string GetHeadingOnSimilarDestinationsWidgetHotelCard(int cardToSelect);
        string GetDescriptionOnSimilarDestinationsWidgetHotelCard(int cardToSelect);
        string GetImageOnSimilarDestinationsWidgetHotelCard(int cardToSelect);
        string GetHotelCardLinkOnSimilarDestinationsWidget(int cardToSelect);
        void ScrollToHotelCardOnSimilarDestinationsWidget(int cardToSelect);
        void ClickSelectHotelCardOnSimilarDestinationsWidget(int cardToSelect);
        void ClickOnSimilarDestinationsWidgetHotelImage(int cardToSelect);
    }
}
