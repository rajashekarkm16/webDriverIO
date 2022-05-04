using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.Automation.BDDFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NUnit.Framework;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class HotelSearchResults : MobileBasePage, IHotelSearchResults
    {
        private AtBy byHotelResults => GetBy(LocatorType.CssSelector, "div[class*=spacing-default] article.sc-c-card");
        private ReadOnlyCollection<AtWebElement> HotelResults => _webDriver.FindElements(byHotelResults);
        private AtWebElement HotelTile => _webDriver.FindElement(LocatorType.CssSelector, "article div.sc-c-card__content");
        private AtBy byHotelName => GetBy(LocatorType.CssSelector, "h4[class*=heading]");
        private AtWebElement HotelName => _webDriver.FindElement(byHotelName);
        private AtBy byHotelStarRating => GetBy(LocatorType.CssSelector, "span.sc-c-star-rating svg");
        private AtWebElement HotelStarRating => _webDriver.FindElement(byHotelStarRating);
        private AtWebElement HotelLocation => _webDriver.FindElement(LocatorType.CssSelector, "span[class*=color-body]");
        private AtBy byHotelRating => GetBy(LocatorType.CssSelector, "span[class*=badge--secondary]");
        private AtWebElement HotelRating => _webDriver.FindElement(byHotelRating);
        private AtBy byHotelPrice => GetBy(LocatorType.CssSelector, "div[class*=direction-column] div[class*=sc-u-color-accent]");
        private AtWebElement HotelPrice => _webDriver.FindElement(byHotelPrice);
        private AtBy byHotelWasPrice => GetBy(LocatorType.CssSelector, "div.sc-u-decoration-line-through");
        private AtWebElement HotelWasPrice => _webDriver.FindElement(byHotelWasPrice);
        private AtBy bySashLabel => GetBy(LocatorType.XPath, "//div[contains(@class,'--sale-primary') or @class='sc-c-sash__label'] ");
        private AtWebElement SashLabel => _webDriver.FindElement(bySashLabel);
        private AtBy byPromoMessageonHotelcardorEstab => GetBy(LocatorType.XPath, "//span[contains(text(),'AutoTest')]");
        private AtWebElement PromoMessageonHotelcardorEstab => _webDriver.FindElement(byPromoMessageonHotelcardorEstab);
        private AtBy byHotelPill => GetBy(LocatorType.CssSelector, "div.sc-c-pill__surface span");
        private AtWebElement HotelPill => _webDriver.FindElement(byHotelPill);
        private AtBy byRecommenderPriceCaption => GetBy(LocatorType.XPath, "//div[contains(@class,'slick-slide')] //article//div[contains(@class,'-4xs')]");
        private ReadOnlyCollection<AtWebElement> RecommenderPriceCaption => _webDriver.FindElements(byRecommenderPriceCaption);
        private AtWebElement HotelPerPersonPrice => _webDriver.FindElement(LocatorType.XPath, "//span[contains(@class,'color-accent')]/parent::div/parent::div/following-sibling::div/span");
        private AtBy BySelectHotelButton => GetBy(LocatorType.XPath, "//button/*[text()='Select'] | //button/*[contains(text(),'View More')]");
        private ReadOnlyCollection<AtWebElement> SelectHotelButton => _webDriver.FindElements(BySelectHotelButton);
        private AtBy byHotelTotalReviews => GetBy(LocatorType.CssSelector, "span[class*=color-secondary]");
        private AtWebElement HotelTotalReviews => _webDriver.FindElement(byHotelTotalReviews);
        private AtBy byCMAComplianceText => GetBy(LocatorType.CssSelector, "main div[class*=container] div[class*=margin-top-l] div[class*=italic]");
        private AtWebElement CMAComplianceText => _webDriver.FindElement(byCMAComplianceText);
        private AtBy byHotelLocalChargesLink => GetBy(LocatorType.XPath, "//span[contains(text(),'Local charges')]/parent::div");
        private AtWebElement HotelLocalChargesLink => _webDriver.FindElement(byHotelLocalChargesLink);
        private AtBy byHotelLocalChargesAmount => GetBy(LocatorType.CssSelector, "#city-tax-dialog-description p:first-child strong");
        private AtWebElement HotelLocalChargesAmount => _webDriver.FindElement(byHotelLocalChargesAmount);
        private AtBy byHotelLocalChargesDescription => GetBy(LocatorType.CssSelector, "#city-tax-dialog-description p:last-child");
        private AtWebElement HotelLocalChargesDescription => _webDriver.FindElement(byHotelLocalChargesDescription);
        private AtBy bySearchResultsLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byLoadMore => GetBy(LocatorType.XPath, "//span[text()='Show more hotels' or text()='Show More Hotels']");
        private AtWebElement LoadMore => _webDriver.FindElement(byLoadMore);
        private AtBy byFilterButton => GetBy(LocatorType.CssSelector, "div[class*=margin-top-m] div[class=sc-o-flex-grid-item] button");
        private AtWebElement FilterButton => _webDriver.FindElement(byFilterButton);
        private AtBy bySortOption => GetBy(LocatorType.XPath, "//span[contains(text(),'Sort')]/a");
        private AtWebElement SortOption => _webDriver.FindElement(bySortOption);
        private AtBy byMapViewLink => GetBy(LocatorType.XPath, "//a[contains(.,'Map')] | //*[contains(text(),'map')]/parent::button");
        private AtWebElement MapViewLink => _webDriver.FindElement(byMapViewLink);
        private AtWebElement CloseButton => _webDriver.FindElement(LocatorType.XPath, "//header[contains(@class,'elevated')]//button[@aria-label='Close']");
        private AtBy byCityTaxCloseButton => GetBy(LocatorType.XPath, "//div[contains(@aria-labelledby,'city')]//button[@aria-label='Close']");
        private AtWebElement CityTaxCloseButton => _webDriver.FindElement(byCityTaxCloseButton);
        private AtWebElement HolidayPrice => _webDriver.FindElement(LocatorType.CssSelector, "div[class*=color-accent]");
        private ReadOnlyCollection<AtWebElement> HolidayPerPersonPrice => _webDriver.FindElements(LocatorType.XPath, "//*[contains(@class,'color-grey') and not(contains(@class,'line-through'))]/span");
        private AtWebElement FlightDetails => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Return flights']"); 
        private AtWebElement BoardType(int hotelToSelect) => _webDriver.FindElement(LocatorType.XPath, "(//span[contains(text(),'Half Board')] | //span[contains(text(),'Bed And Breakfast')] | //span[contains(text(),'Room Only')] | //span[contains(text(),'Half Board')] | //span[contains(text(),'All Inclusive')])[#]", hotelToSelect.ToString());
        private AtBy byNoRoomsMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-u-background-highlight-20')]//span[contains(@class,'sc-o-body--l')] | //div[@class='sc-c-notification sc-c-notification--error']");
        private AtWebElement NoRoomsMessage => _webDriver.FindElement(byNoRoomsMessage);
        private AtBy byStartNewSearchButton => GetBy(LocatorType.XPath, "//div[contains(@class,'xxl')]/a[text()='Start a new search'] | //a[text()='Start a new search']");
        private AtWebElement StartNewSearchButton => _webDriver.FindElement(byStartNewSearchButton);
        private AtWebElement SearchAlternativeDateButton => _webDriver.FindElement(LocatorType.XPath, "//span[(text()='Search alternative dates') or (text()='Search Alternative Dates')]/parent::button");
        private AtBy byEditSearchButton => GetBy(LocatorType.XPath, "//main/div[contains(@class,'padding-y-m')]//button");
        private AtWebElement HotelsInText => _webDriver.FindElement(LocatorType.CssSelector, "h1.sc-o-heading");
        private AtBy ByFilteredHotelsMessage => GetBy(LocatorType.XPath, "//h1[contains(text(),'match')]");
        private AtWebElement FilteredHotelsMessage => _webDriver.FindElement(ByFilteredHotelsMessage);
        private ReadOnlyCollection<AtWebElement> BoardTypes => _webDriver.FindElements(LocatorType.XPath, "//article[contains(@class,'sc-c-card')]//ul[contains(@class,'sc-c-info-list')]/li//span[contains(text(),'Room Only') or contains(text(),'Self Catering') or contains(text(),'Bed And Breakfast') or contains(text(),'All Inclusive') or contains(text(),'Half Board')]");
        private AtBy ByRecommenderHotelResults => GetBy(LocatorType.CssSelector, "div[class*=slick-slide] article.sc-c-card");
        private ReadOnlyCollection<AtWebElement> RecommenderHotelResults => _webDriver.FindElements(ByRecommenderHotelResults);
        private AtBy ByRecommenderScrollerButton => GetBy(LocatorType.CssSelector, "//ul[@class='sc-c-carousel__dots']/li/button");
        private ReadOnlyCollection<AtWebElement> RecommenderScrollerButton => _webDriver.FindElements(ByRecommenderHotelResults);
        private AtBy ByCloseSortButton => GetBy(LocatorType.CssSelector, "div[aria-labelledby=sort-dialog-title] button[aria-label=Close]");
        private AtWebElement CloseSortButton => _webDriver.FindElement(ByCloseSortButton);
        private AtWebElement SecureTodayPill => _webDriver.FindElement(LocatorType.XPath, "//button[@class='sc-c-pill__surface']//span[contains(text(),'Secure today from')]");
        private AtBy ByPayDepositDialogContent => GetBy(LocatorType.CssSelector, "#deposit-dialog-description p");
        private AtWebElement PayDepositDialogContent => _webDriver.FindElement(ByPayDepositDialogContent);
        private AtWebElement PayMonthlyPill => _webDriver.FindElement(LocatorType.XPath, "//button[@class='sc-c-pill__surface']//span[contains(text(),'Pay monthly')]");
        private AtBy ByPayMonthlyDialogContent => GetBy(LocatorType.CssSelector, "#pay-monthly-dialog-description p strong");
        private AtWebElement PayMonthlyDialogContent => _webDriver.FindElement(ByPayMonthlyDialogContent);
        private AtWebElement FreeCancellationMessage => _webDriver.FindElement(LocatorType.XPath, "//span[contains(text(),'Free hotel cancellation')]");
        private AtBy ByDialogContent => GetBy(LocatorType.CssSelector, "#hype-message-description");
        private AtBy ByDialogHeader => GetBy(LocatorType.CssSelector, "#hype-message-title");
        private AtWebElement CloseDialogContent => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-dialog button");
        private AtWebElement DialogContent => _webDriver.FindElement(ByDialogContent);
        private AtWebElement NonRefundableMessage(int hotelToSelect) => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(@class,'spacing-default')]//article[contains(@class,'sc-c-card')])[#]//span[contains(text(),'Non Refundable')]", hotelToSelect.ToString());
        private AtWebElement DialogHeader => _webDriver.FindElement(ByDialogHeader);
        private ReadOnlyCollection<AtWebElement> Platform195Ads => _webDriver.FindElements(LocatorType.XPath, "//iframe[@title='3rd party ad content']");
        private ReadOnlyCollection<AtWebElement> SponseredHotels => _webDriver.FindElements(LocatorType.XPath, "//span[.='Sponsored']");

        private AtBy byCovidCoverPromotion => GetBy(LocatorType.XPath, "//div[contains(text(),'Covid Cover Plus')]/parent::span");
        private AtWebElement CovidCoverPromotion => _webDriver.FindElement(byCovidCoverPromotion);
        private AtBy byCovidCoverModalHeading => GetBy(LocatorType.XPath,"//h3[@id='covid-protection-dialog-title' or @id='hype-message-title']");
        private AtWebElement CovidCoverModalHeading => _webDriver.FindElement(byCovidCoverModalHeading);
        private AtWebElement CovidCoverDailogContent => _webDriver.FindElement(LocatorType.CssSelector, "div[class='sc-c-dialog-content']");
        private AtWebElement CloseCovidCoverdailog => _webDriver.FindElement(LocatorType.CssSelector, "div[class='sc-c-modal'] button[aria-label='Close']");
        private AtBy byCovidcoverInPriceIncludes => GetBy(LocatorType.XPath,"//span[contains(text(),'Covid Cover Plus')]");
        private AtWebElement CovidcoverInPriceIncludes => _webDriver.FindElement(byCovidcoverInPriceIncludes);
        private AtBy byNoAvailablityMessage => GetBy(LocatorType.CssSelector, "div[class*='sc-c-notification sc-c-notification--error']");
        private AtWebElement NoAvailablityMessage => _webDriver.FindElement(byNoAvailablityMessage);
        private AtBy byFiltersResultLoader => GetBy(LocatorType.XPath, "//span[contains(text(),'This may take a few seconds')]");
        private AtBy byStopSellMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-u-bold')]/parent::span");
        private AtWebElement StopSellMessage => _webDriver.FindElement(byStopSellMessage);
        private AtWebElement datesAdjustedMessage => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'sc-u-background-highlight')]//span");
        private readonly IAtWebDriver _webDriver;
        private int HotelToSelect = 0;
        private HotelInformation hotelInformation;

        #region[constructor]
        public HotelSearchResults(IAtWebDriver webDriver)
             : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        public int GetHotelToSelect()
        {
            if (HotelToSelect == 0)
                RandomizeHotelToSelect();
            return HotelToSelect;
        }

        public int GetHotelToSelect(string hotelNameToSelect)
        {
            try
            {
                _webDriver.WaitForElementVisible(bySearchResultsLoader, 60, "Search Results Loader is not visible");
            }
            catch{ }
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not visible");
            bool isAvailable = false;
            int hotelCounter = 0;
            int loadMoreCounter = 0;
            do
            {
                for (; hotelCounter < HotelResults.Count; hotelCounter++)
                {
                    if (HotelResults[hotelCounter].FindElement(HotelName).Text.Contains(hotelNameToSelect, StringComparison.OrdinalIgnoreCase))
                    {
                        HotelToSelect = hotelCounter + 1;
                        isAvailable = true;
                        break;
                    }
                }
                if (isAvailable)
                    break;
                else
                {
                    _webDriver.WaitForElementClickable(byLoadMore, 10, "No more matching search results!");
                    LoadMore.Click();
                    loadMoreCounter++;
                }
            } while (!isAvailable && loadMoreCounter < 1);
            return HotelToSelect;
        }

        private void WaitForSearchResultsToLoad()
        {
            try
            {
                if (HotelResults.Count == 0)
                    _webDriver.WaitForElementVisible(bySearchResultsLoader, 30, "Search Results Loader is not visible");
            }
            catch { }
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 120);
            //Workaround fix for PP stability issue
            if (HotelResults.Count == 0 && !(HelperFunctions.IsLive()))
                _webDriver.Refresh();
            _webDriver.WaitForElementVisible(byHotelResults, 90, "Hotel results are not visible");
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 5);
        }

        public int RandomizeHotelToSelect()
        {
            WaitForSearchResultsToLoad();
            HotelToSelect = HelperFunctions.RandomNumber(GetResultsCount());
            return HotelToSelect;
        }

        public string GetHotelsInDestinationText()
        {
            return HotelsInText.Text;
        }

        public int GetResultsCount()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            try
            { _webDriver.WaitForElementVisible(byHotelResults, Constants.DefaultWait, ""); }
            catch (Exception e) {  }
            return HotelResults.Count;
        }

        public void SelectHotel(int hotelToSelect)
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.ScrollToElement(HotelResults[hotelToSelect - 1]);
            _webDriver.WaitForElementClickable(BySelectHotelButton, 30);
            SelectHotelButton[hotelToSelect - 1].ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            try
            {
                _webDriver.WaitForAnyVisible(byHotelPrice, byEditSearchButton, 90, "Estab page is not loaded");
            } catch (Exception e)
            {
                System.Threading.Thread.Sleep(4000);
                _webDriver.WaitForAnyVisible(byHotelPrice, byEditSearchButton, 90, "Estab page is not loaded");
            }
        }

        public void ViewFlightDetails(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(HotelResults[hotelToSelect - 1]);
            HotelResults[hotelToSelect - 1].FindElement(FlightDetails).Click();
        }

        public void CaptureHotelInformation(int hotelToSelect)
        {
            hotelInformation = new HotelInformation();
            _webDriver.WaitForElementVisible(byHotelResults, 60, "Hotel results are not visible");
            _webDriver.ScrollToElement(HotelResults[hotelToSelect - 1]);
            hotelInformation.HotelName = HotelResults[hotelToSelect - 1].FindElement(HotelName).Text;
            hotelInformation.Location = HotelResults[hotelToSelect - 1].FindElement(HotelLocation).Text;
            hotelInformation.Price = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[hotelToSelect - 1].FindElement(HotelPrice).Text));
            hotelInformation.Rating = HotelResults[hotelToSelect - 1].FindElements(byHotelRating).Count > 0 ? HotelResults[hotelToSelect - 1].FindElement(HotelRating).Text : "";
            hotelInformation.StarRating = HotelResults[hotelToSelect - 1].FindElements(HotelStarRating).Count;
            hotelInformation.ReviewsCount = HotelResults[hotelToSelect - 1].FindElements(byHotelTotalReviews).Count > 0 ? Convert.ToInt32(HotelResults[hotelToSelect - 1].FindElement(HotelTotalReviews).Text.Split(' ')[2]) : 0;
        }

        public HotelInformation GetHotelInformation()
        {
            return hotelInformation;
        }

        public HotelInformation CaptureAndReturnHotelInformation(int hotelToSelect)
        {
            CaptureHotelInformation(hotelToSelect);
            return GetHotelInformation();
        }

        public Boolean AreDatesAdjusted()
        {
            return datesAdjustedMessage.Visible;
        }

        public void CaptureRecommenderHotelInformation(int hotelToSelect)
        {
            hotelInformation = new HotelInformation();
            _webDriver.WaitForElementVisible(ByRecommenderHotelResults, 30, "Recommender hotel results is not visible");
            _webDriver.ScrollToElement(RecommenderHotelResults[hotelToSelect - 1]);
            hotelInformation.HotelName = RecommenderHotelResults[hotelToSelect - 1].FindElement(HotelName).Text;
            hotelInformation.Location = RecommenderHotelResults[hotelToSelect - 1].FindElement(HotelLocation).Text;
            hotelInformation.Price = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(RecommenderHotelResults[hotelToSelect - 1].FindElement(HotelPrice).Text));
            hotelInformation.Rating = RecommenderHotelResults[hotelToSelect - 1].FindElements(byHotelRating).Count > 0 ? RecommenderHotelResults[hotelToSelect - 1].FindElement(HotelRating).Text : "";
            hotelInformation.StarRating = RecommenderHotelResults[hotelToSelect - 1].FindElements(HotelStarRating).Count;
            hotelInformation.ReviewsCount = RecommenderHotelResults[hotelToSelect - 1].FindElements(byHotelTotalReviews).Count > 0 ? Convert.ToInt32(RecommenderHotelResults[hotelToSelect - 1].FindElement(HotelTotalReviews).Text.Split(' ')[2]) : 0;
        }

        public HotelInformation CaptureAndReturnRecommenderHotelInformation(int hotelToSelect)
        {
            CaptureRecommenderHotelInformation(hotelToSelect);
            return GetHotelInformation();
        }

        public List<decimal> GetAllHotelPrices()
        {
            List<decimal> hotelPrices = new List<decimal>();
            for (int counter = 0; counter < GetResultsCount(); counter++)
            {
                _webDriver.WaitForElementVisible(byHotelPrice, 30, "Hotel price is not visible");
                hotelPrices.Add(Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[counter].FindElement(HotelPrice).Text)));
            }
            return hotelPrices;
        }

        public decimal GetHotelPrice(int hotelToSelect)
        {
           return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[hotelToSelect - 1].FindElement(HotelPrice).Text));
        }

        public decimal GetHotelWasPrice(int hotelToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[hotelToSelect - 1].FindElement(HotelWasPrice).Text));
        }

        public string GetHotelSahLabelText(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(bySashLabel, 30, "Sash Label is not Visible");
            return HotelResults[hotelToSelect - 1].FindElement(SashLabel).Text;             
        }

        public string GetPromoMessageonHotelcardorEstab()
        {
            _webDriver.WaitForElementVisible(byPromoMessageonHotelcardorEstab, Constants.DefaultWait, "Promo Message is not visible");
            _webDriver.ScrollElementToCenter(PromoMessageonHotelcardorEstab);
            return PromoMessageonHotelcardorEstab.Text;
        }

        public string GetHotelPillText(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(byHotelPill, 30, "Hotel pill  is not Visible");
            return HotelResults[hotelToSelect - 1].FindElement(HotelPill).Text;
        }
        #region [ Filter Validation ]

        public bool ValidatePriceFilter(decimal filterPrice)
        {
            foreach (var hotelPrice in GetAllHotelPrices())
            {
                if (hotelPrice > filterPrice)
                    return false;
            }
            return true;
        }

        public bool ValidateStarRating(string filteredOption)
        {
            foreach (var result in HotelResults)
            {
                if (result.FindElements(HotelStarRating).Count != Convert.ToInt16(filteredOption))
                    return false;
            }
            return true;
        }

        public bool ValidateCustomerRating(string filteredOption)
        {
            int customerRating = (Convert.ToInt16(filteredOption) * 2)-1;
            foreach (var result in HotelResults)
            {
                if (result.FindElements(HotelRating).Count >= customerRating && result.FindElements(HotelRating).Count <= customerRating+1)
                    return false;
            }
            return true;
        }

        public bool ValidateBoardType(string filteredOption)
        {
            foreach (var boardType in BoardTypes)
            {
                if (boardType.Text != filteredOption)
                    return false;
            }
            return true;
        }

        public bool ValidateBoardType(List<string> filteredOptions)
        {
            _webDriver.WaitForElementVisible(ByFilteredHotelsMessage, Constants.MediumWait, "Filter message is  not visible");
            foreach (var boardType in BoardTypes)
            { 
                if (!filteredOptions.Contains(boardType.Text.Replace("And", "&").Split("(")[0].Trim()))
                    return false;
            }
            return true;
        }

        public string GetBoardTypeByHotelName(string hotelName)
        {
            int boardTypeToSelect = GetHotelToSelect(hotelName);
            return BoardTypes[boardTypeToSelect-1].Text;
        }
        #endregion

        public string GetCMAComplianceText()
        {
            _webDriver.WaitForElementVisible(byCMAComplianceText, 60, "CMA Compliance text is not visible");
            return CMAComplianceText.Text;
        }

        public string GetLocalCharges(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(byHotelLocalChargesLink, 20, "Hotel Local Charges link is not visible");
            _webDriver.ScrollToElement(HotelResults[hotelToSelect - 1]);
            HotelResults[hotelToSelect - 1].FindElement(HotelLocalChargesLink).Click();
            _webDriver.WaitForElementVisible(byHotelLocalChargesAmount, 30, "Hotel Local Charges amount is not visible");
            return HotelLocalChargesAmount.Text;
        }

        public void CloseLocalTaxPopup()
        {
            _webDriver.WaitForElementClickable(byCityTaxCloseButton, 10);
            CityTaxCloseButton.Click();
        }

        public string GetIncludesLocalChargesDescription()
        {
            _webDriver.WaitForElementVisible(byHotelLocalChargesDescription, 30, "Hotel Local Charges Description is not visible");
            return HotelLocalChargesDescription.Text;
        }

        public void SelectFilters()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            _webDriver.WaitForElementClickable(byFilterButton, Constants.MediumWait);
            FilterButton.ClickButtonUsingJs();
        }

        public bool HasLoadMoreResults()
        {
            _webDriver.WaitForElementVisible(byHotelResults, 60, "Hotel results are not visible");
            return LoadMore.Visible;
        }

        public void LoadMoreResults(int counter=1)
        {
            while (HasLoadMoreResults() && (counter > 0))
            {
                LoadMore.Click();
                counter--;
            }
        }

        public int GetLoadMoreLocation()
        {
            _webDriver.WaitForDomReady();
            _webDriver.WaitForElementVisible(byHotelResults, 60, "Hotel results are not visible");
            return LoadMore.Location.Y;
        }

        public void EditSortOption()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);                       
            _webDriver.WaitForElementClickable(bySortOption, Constants.MediumWait);
            SortOption.Click();
        }

        public bool VerifyAppliedSortOption(string option)
        {
            bool isSortApplied = true;
            int counter = 1;
            double prevValue;
            double newValue;

            while (counter <= 2)
            {
                if (option.Equals("Customer Rating (Highest first)"))
                {
                    _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not visible");
                    _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 30);
                    _webDriver.WaitForElementClickable(byHotelResults, 30);
                    System.Threading.Thread.Sleep(2000);
                    prevValue = 0;
                    //Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(HotelResults[0].FindElement(HotelRating).Text));
                    foreach (var result in HotelResults)
                    {
                        System.Threading.Thread.Sleep(1000);
                        _webDriver.MoveToElement(result);
                        if (result.FindElements(byHotelPill).Count!=0?result.FindElement(HotelPill).Text != "Sponsored": result.FindElements(byHotelPill).Count == 0)
                        {
                            newValue = Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(result.FindElement(HotelRating).Text));
                            if (prevValue == 0)
                                prevValue = newValue;
                            if (prevValue >= newValue)
                                prevValue = newValue;
                            else
                            {
                                isSortApplied = false;
                                Console.WriteLine("Previous Value: " + prevValue + " | New Value: " + newValue);
                                Assert.GreaterOrEqual(prevValue, newValue, "Customer rating should be higher first");
                                break;
                            }
                        }
                    }
                }

                else if (option.Equals("Price (Cheapest first)"))
                {
                    _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not visible");
                    _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 30);
                    _webDriver.WaitForElementClickable(byHotelResults, 30);
                    System.Threading.Thread.Sleep(2000);
                    prevValue = 0;
                    //Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(HotelResults[0].FindElement(HotelPrice).Text));
                    foreach (var result in HotelResults)
                    {
                        System.Threading.Thread.Sleep(1000);
                        //_webDriver.WaitForElementVisible(result, 30); - code cleam up
                        _webDriver.MoveToElement(result);
                        if (result.FindElements(byHotelPill).Count != 0 ? result.FindElement(HotelPill).Text != "Sponsored" : result.FindElements(byHotelPill).Count == 0)
                        {
                            newValue = Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(result.FindElement(HotelPrice).Text));
                            if (prevValue == 0)
                                prevValue = newValue;
                            if (prevValue <= newValue)
                                prevValue = newValue;
                            else
                            {
                                isSortApplied = false;
                                Assert.LessOrEqual(prevValue, newValue, "Price should be lower first");
                                break;
                            }
                        }
                    }
                }

                else if (option.Equals("Star Rating (Highest first)"))
                {
                    _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not visible");
                    _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 30);
                    _webDriver.WaitForElementClickable(byHotelResults, 30);
                    System.Threading.Thread.Sleep(2000);
                    prevValue = 0;
                    //Convert.ToDouble(HotelResults[0].FindElements(HotelStarRating).Count);
                    foreach (var result in HotelResults)
                    {
                        System.Threading.Thread.Sleep(1000);
                        _webDriver.MoveToElement(result);
                        if (result.FindElements(byHotelPill).Count != 0 ? result.FindElement(HotelPill).Text != "Sponsored" : result.FindElements(byHotelPill).Count == 0)
                        {
                            newValue = Convert.ToDouble(result.FindElements(HotelStarRating).Count);
                            if (prevValue == 0)
                                prevValue = newValue;
                            if (prevValue >= newValue)
                                prevValue = newValue;
                            else
                            {
                                isSortApplied = false;
                                Assert.GreaterOrEqual(prevValue, newValue, "Star rating should be higher first");
                                break;
                            }
                        }
                    }
                }
                _webDriver.WaitForElementVisible(byLoadMore, 10, "LoadMore button is not visible");
                LoadMore.ClickButtonUsingJs();
                System.Threading.Thread.Sleep(5000);
                counter++;
                _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 30);
                prevValue = 0.0;
                newValue = 0.0;
            }
            return isSortApplied;
        }

        public bool VerifyNoDuplicates()
        {
            bool isDuplicate = false;
            List<string> HotelNames = new List<string>();
            _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not visible");
            System.Threading.Thread.Sleep(5000);
            HotelNames.Clear();
            foreach (var result in HotelResults)
                HotelNames.Add(result.FindElement(HotelName).Text);
            if (HotelNames.Count != HotelNames.Distinct().Count())
            {
                IEnumerable<string> duplicates = HotelNames.GroupBy(x => x)
                    .Where(g => g.Count() > 1)
                    .Select(x => x.Key);
                if (duplicates.Count() > 1)
                {
                    isDuplicate = true;
                    Console.WriteLine("Duplicates: " + String.Join(",", duplicates));
                }
            }           
            return isDuplicate;
        }

        public void SelectMapView()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementClickable(byMapViewLink, 60);
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementClickable(byMapViewLink, 60);
            MapViewLink.Click();
        }

        public decimal GetHolidayTotalPrice(int hotelToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[hotelToSelect - 1].FindElement(HolidayPrice).Text));
        }

        public decimal GetHolidayPerPersonPrice(int hotelToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HolidayPerPersonPrice[hotelToSelect - 1].Text));
        }

        public string CaptureNoRoomsMessage()
        {
            _webDriver.WaitForElementVisible(byNoRoomsMessage, 60, "No Rooms Message is not visible");
            return NoRoomsMessage.Text;
        }

        public void ClickStartNewSearchButton()
        {
            _webDriver.WaitForElementVisible(byStartNewSearchButton, 60, "Start New Search Button is not visible");
            StartNewSearchButton.Click();
        }

        public bool IsStartNewSearchButtonDisplayed()
        {
            return StartNewSearchButton.Visible;
        }

        public bool IsNoRoomsMessageDisplayed()
        {
            WaitforPageToLoad();
            return NoRoomsMessage.Visible;
        }

        public void ClickSearchAlternativeDatesButton()
        {
            _webDriver.WaitForElementPresent(byNoRoomsMessage, 30);
            _webDriver.ScrollToElement(SearchAlternativeDateButton);
            SearchAlternativeDateButton.ClickButtonUsingJs();
        }

        public double GetHotelReviewsCount(int hotelToSelect)
        {
            return HotelResults[hotelToSelect - 1].FindElements(byHotelTotalReviews).Count > 0 ? Convert.ToInt32(HotelResults[hotelToSelect - 1].FindElement(HotelTotalReviews).Text.Split(' ')[2]) : 0;
        }

        public string GetHotelBoardType(int hotelToSelect)
        {
            WaitforPageToLoad();
            return BoardType(hotelToSelect).Text;
        }

        public bool IsHotelSearchResultsPageLoaded()
        {
            return GetResultsCount() > 0 || IsNoRoomsMessageDisplayed();
        }
        public int GetHotelSearchResultsCount()
        {
            System.Threading.Thread.Sleep(2000);
            _webDriver.WaitUntilNotVisible(byFiltersResultLoader, Constants.MediumWait, "Filter text is not visible");
            _webDriver.WaitForElementVisible(ByFilteredHotelsMessage, Constants.ShortWait, "Filtered Hotel message is not displayed");
            return Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(FilteredHotelsMessage.Text.Replace("-","")));
        }

        private void WaitforPageToLoad()
        {
            try
            {
                _webDriver.WaitForElementVisible(bySearchResultsLoader, 30, "Search Results Loader is not visible");
            }
            catch { }
            finally
            {
                _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            }
        }

        public bool IsRecommenderDisplayed()
        {
            WaitforPageToLoad();
            return RecommenderHotelResults.Count > 0;
        }        

        public void SelectHotelFromRecommender(int recommenderHotelToSelect)
        {  
            _webDriver.ScrollToElement(RecommenderHotelResults[recommenderHotelToSelect - 1]);
            RecommenderHotelResults[recommenderHotelToSelect -1].Click();
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 30);
        }
        
        public string GetRecommenderPriceCaption()
        {
            return RecommenderPriceCaption[0].Text;
        }    
        
        public List<string> GetAllHotelNames()
        {
            _webDriver.WaitForElementVisible(byHotelResults, Constants.MediumWait, "Hotel results are not visible!");
            List<string> hotelNames = new List<string>();
            foreach(var hotel in HotelResults)
            {
                hotelNames.Add(hotel.FindElement(HotelName).Text);
            }
            return hotelNames;
        }

        public void CloseSortModel()
        {
            CloseSortButton.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(ByCloseSortButton, 10);
        }

        public bool IsSecureTodayPillDisplayed(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not displayed!");
            return HotelResults[hotelToSelect - 1].FindElement(SecureTodayPill).Visible;
        }

        public decimal GetAmountFromSecureTodayPill(int hotelToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[hotelToSelect - 1].FindElement(SecureTodayPill).Text));
        }

        public void ClickSecureTodayPill(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(HotelResults[hotelToSelect - 1].FindElement(SecureTodayPill));
            HotelResults[hotelToSelect - 1].FindElement(SecureTodayPill).Click();
        }

        public decimal GetAmountFromDepositDialogModal()
        {
            _webDriver.WaitForElementVisible(ByPayDepositDialogContent, 20, "Pay deposit dialog modal is not displayed!");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PayDepositDialogContent.Text));
        }

        public bool IsPayMonthlyPillDisplayed(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(byHotelResults, 30, "Hotel results are not displayed!");
            return HotelResults[hotelToSelect - 1].FindElement(PayMonthlyPill).Visible;
        }

        public decimal GetAmountFromPayMonthlyPill(int hotelToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelResults[hotelToSelect - 1].FindElement(PayMonthlyPill).Text));
        }

        public void ClickPayMonthlyPill(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(HotelResults[hotelToSelect - 1].FindElement(PayMonthlyPill));
            HotelResults[hotelToSelect - 1].FindElement(PayMonthlyPill).Click();
        }

        public decimal GetAmountFromPayMonthlyDialogModal()
        {
            _webDriver.WaitForElementVisible(ByPayMonthlyDialogContent, 20, "Pay deposit dialog modal is not displayed!");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PayMonthlyDialogContent.Text));
        }

        public bool IsFreeCancellationMessageDisplayed(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(byHotelResults, 20, "Hotel results are not visible!");
            return HotelResults[hotelToSelect - 1].FindElement(FreeCancellationMessage).Visible;
        }

        public void ClickFreeCancellationMessage(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(HotelResults[hotelToSelect - 1].FindElement(FreeCancellationMessage));
            HotelResults[hotelToSelect - 1].FindElement(FreeCancellationMessage).Click();
        }

        public string GetDialogHeader()
        {
            _webDriver.WaitForElementVisible(ByDialogHeader, Constants.DefaultWait, "Dialog modal pop up is not displayed!");
            _webDriver.WaitForTextPresent(ByDialogHeader, " ", TimeSpan.FromSeconds(5), 4);
            return DialogHeader.Text;
        }

        public string GetDialogContent()
        {
            _webDriver.WaitForElementVisible(ByDialogContent, Constants.DefaultWait, "Dialog modal pop up is not displayed!");
            _webDriver.WaitForTextPresent(ByDialogContent, " ", TimeSpan.FromSeconds(5), 4);
            return DialogContent.Text;
        }

        public void CloseDialogContentPopUp()
        {
            CloseDialogContent.ClickButtonUsingJs();
        }

        public bool IsNonRefunableMessageDisplayed(int hotelToSelect)
        {
            _webDriver.WaitForElementVisible(byHotelResults, 20, "Hotel results are not visible!");
            return NonRefundableMessage(hotelToSelect).Visible;
        }

        public void ClickNonRefundableMessage(int hotelToSelect)
        {
            _webDriver.ScrollElementToCenter(NonRefundableMessage(hotelToSelect));
            NonRefundableMessage(hotelToSelect).Click();
        }
        public int GetPlatformAdsCount()
        {
            return Platform195Ads.Count;
        }

        public int GetSponseredHotelsCount()
        {
            return SponseredHotels.Count;
        }

        public string GetCovidCoverPlusText()
        {
            _webDriver.WaitForElementVisible(byCovidCoverPromotion, Constants.DefaultWait, "CovidCover Promotionbox is not visible");
            return CovidCoverPromotion.Text.Replace("\n", "").Replace("\r", "").Replace(" ", "").ToUpper();
        }

        public string GetCovidPlusTextinPriceincludesSection()
        {
            _webDriver.WaitForElementPresent(byHotelStarRating, Constants.MediumWait);
            _webDriver.WaitForElementPresent(byCovidcoverInPriceIncludes, Constants.DefaultWait);
            _webDriver.ScrollToElement(CovidcoverInPriceIncludes);
            return CovidcoverInPriceIncludes.Text.Replace("\n", "").Replace("\r", "").Replace(" ", "").ToUpper();
        }

        public string GetCovidCoverDailogContentInPromotionbox()
        {
            _webDriver.WaitForAnyVisible(byHotelName, byHotelStarRating, Constants.MediumWait);           
            try
            {
                _webDriver.WaitForElementPresent(byCovidCoverPromotion, Constants.DefaultWait);
                _webDriver.ScrollToElement(CovidCoverPromotion);
                CovidCoverPromotion.Click();
                _webDriver.WaitForElementPresent(byCovidCoverModalHeading,Constants.DefaultWait);
            }
            catch
            {
                if (!CovidCoverModalHeading.Visible)
                {
                    CovidCoverPromotion.Click();
                }
            }                 
            return GetCovidCoverDailogContent();
        }

        public string GetCovidCoverDailogContentinPriceincludes()
        {
            _webDriver.WaitForElementPresent(byCovidcoverInPriceIncludes,Constants.DefaultWait);
            _webDriver.ScrollElementToCenter(CovidcoverInPriceIncludes);
            CovidcoverInPriceIncludes.Click();
            return GetCovidCoverDailogContent();
        }

        public string GetCovidCoverDailogContent()
        {
            _webDriver.WaitForElementVisible(byCovidCoverModalHeading, Constants.DefaultWait, "Covid cover Dailog modal is not visible");
            _webDriver.WaitForTextPresent(byCovidCoverModalHeading, "Covid Cover Plus", TimeSpan.FromSeconds(10), 2);
            string CovidCoverDailogText = CovidCoverDailogContent.Text;
            CloseCovidCoverdailog.Click();
            return CovidCoverDailogText;
        }

        public string GetNoAvailablityMessage()
        {
            _webDriver.WaitForAnyVisible(byNoAvailablityMessage,byStopSellMessage, Constants.MediumWait, "No availablityMessage is not disaplayed");
            if (NoAvailablityMessage.Visible)
                return NoAvailablityMessage.Text;
            else
                return StopSellMessage.Text;
        }

    }
}
