using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.Automation.BDDFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class HotelEstabPage: MobileBasePage, IHotelEstabPage
    {
        private AtBy bySearchItinerary => GetBy(LocatorType.CssSelector, "div[class*=nowrap] div p.sc-o-body--s");
        private AtWebElement SearchItinerary => _webDriver.FindElement(bySearchItinerary);
        private AtBy byHotelName => GetBy(LocatorType.XPath, "(//div/h1[contains(@class,'heading')])[1]");
        private AtWebElement HotelName => _webDriver.FindElement(byHotelName);
        private ReadOnlyCollection<AtWebElement> HotelStarRating => _webDriver.FindElements(LocatorType.CssSelector, "span.sc-c-star-rating:nth-child(1) svg");
        private AtWebElement HotelLocation => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'spacing-m')]//div[contains(@class,'grid-item')]//span[@class='sc-o-body']");
        private AtWebElement HotelRating => _webDriver.FindElement(LocatorType.CssSelector, "div[class*=grid-item] span[class*=badge--secondary]");
        private AtBy byHotelPrice => GetBy(LocatorType.CssSelector, ".sc-o-heading.sc-u-color-accent");
        private AtWebElement HotelPrice => _webDriver.FindElement(byHotelPrice);
        private AtBy byHotelWasPrice => GetBy(LocatorType.CssSelector, "div.sc-u-decoration-line-through");
        private AtWebElement HotelWasPrice => _webDriver.FindElement(byHotelWasPrice);
        private AtBy bySashLabel => GetBy(LocatorType.XPath, "//div[contains(@class,'--sale-primary') or @class='sc-c-sash__label' or contains(@class,'sc-c-strip--')]");
        private AtWebElement SashLabel => _webDriver.FindElement(bySashLabel);
        private AtBy byHotelPill => GetBy(LocatorType.XPath, "(//div[@class='sc-c-pill__surface']/span)[1]");
        private AtWebElement HotelPill => _webDriver.FindElement(byHotelPill);
        private AtWebElement HotelPerPersonPrice => _webDriver.FindElement(LocatorType.CssSelector, ".sc-o-body.sc-u-color-grey");
        private AtWebElement HolidayTotalPrice => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Holiday price from']/parent::div/following-sibling::div/div/span");
        private AtWebElement HolidayPerPersonPrice => _webDriver.FindElement(LocatorType.XPath, "//p[text()='per person']/span");
        private AtBy bySearchResultsLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byRoomsTab => GetBy(LocatorType.Id, "rooms-tab");
        private AtWebElement RoomsTab => _webDriver.FindElement(byRoomsTab);
        private AtBy byChooseRoomsButton => GetBy(LocatorType.XPath, "//span[contains(text(),'Choose Room')]/parent::button");
        private AtWebElement ChooseRoomsButton => _webDriver.FindElement(byChooseRoomsButton);
        private ReadOnlyCollection<AtWebElement> HotelRooms => _webDriver.FindElements(LocatorType.CssSelector, "#rooms-tabpanel .sc-o-flex-grid-item *.sc-c-card");
        private AtBy byHotelRoomType => GetBy(LocatorType.CssSelector, "#rooms-tabpanel .sc-o-flex-grid-item .sc-o-heading.sc-o-heading--s");
        private ReadOnlyCollection<AtWebElement> HotelRoomType => _webDriver.FindElements(byHotelRoomType);
        private ReadOnlyCollection<AtWebElement> HotelRoomPrice => _webDriver.FindElements(LocatorType.CssSelector, "#rooms-tabpanel .sc-o-flex-grid-item [class*=color-accent]");
        private AtBy byHotelBoardType => GetBy(LocatorType.CssSelector, "div.sc-c-container ul li div[class*=primary-content] div[class*=body--l]");
        private ReadOnlyCollection<AtWebElement> HotelBoardType => _webDriver.FindElements(byHotelBoardType);
        private AtBy byRoomPromoMessage => GetBy(LocatorType.CssSelector, "div[class='sc-o-flex-grid-item'] div[class*=sale-primary]");
        private ReadOnlyCollection<AtWebElement> RoomPromoMessage => _webDriver.FindElements(byRoomPromoMessage);
        private AtBy byBoardPromoMessage => GetBy(LocatorType.CssSelector, "div.sc-c-container ul li div[class*=primary-content] div[class*=sale]");
        private ReadOnlyCollection<AtWebElement> BoardPromoMessage => _webDriver.FindElements(byBoardPromoMessage);
        private AtBy byBoardWasPrice => GetBy(LocatorType.CssSelector, "div[style*=line-through]");
        private ReadOnlyCollection<AtWebElement> BoardWasPrice => _webDriver.FindElements(byBoardWasPrice);
        private AtBy byBoardNowPrice => GetBy(LocatorType.XPath, "//div[@class='sc-c-option__secondary-content']/div[3]");
        private ReadOnlyCollection<AtWebElement> BoardNowPrice => _webDriver.FindElements(byBoardNowPrice);
        private ReadOnlyCollection<AtWebElement> HotelBoardTypeOptions(int boardOptions) => _webDriver.FindElements(LocatorType.XPath, "(//ul[not(@aria-expanded)]/li/div//div[contains(@class,'primary-content')])[#]/*", boardOptions.ToString());
        private ReadOnlyCollection<AtWebElement> HotelBoardPrice => _webDriver.FindElements(LocatorType.CssSelector, "div.sc-c-container ul li div[class*=secondary-content] :nth-child(1)");
        private AtWebElement LocalTaxPriceInBoardType => _webDriver.FindElement(LocatorType.XPath, "//ul[@class='sc-c-option-list']/following-sibling::div/*[1]");
        private AtWebElement ConfirmSelection => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-dialog-content button"); 
        private AtBy byPerPersonPriceRadio => GetBy(LocatorType.XPath, "//span[text()='Per person price']/parent::button | //span[text()='Per Person Price']/parent::button");
        private AtWebElement PerPersonPriceRadio => _webDriver.FindElement(byPerPersonPriceRadio);
        private AtWebElement TotalPriceRadio => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Total price']/parent::button | //span[text()='Total Price']/parent::button");
        private AtBy byLocalChargesLink => GetBy(LocatorType.XPath, "//*/span[contains(text(),'Local charges')]");
        private AtWebElement LocalChargesLink => _webDriver.FindElement(byLocalChargesLink);
        private AtBy byHotelLocalChargesAmount => GetBy(LocatorType.CssSelector, "#city-tax-dialog-description p:first-child strong");
        private AtWebElement HotelLocalChargesAmount => _webDriver.FindElement(byHotelLocalChargesAmount);
        private AtBy byCityTaxCloseButton => GetBy(LocatorType.XPath, "//div[contains(@aria-labelledby,'city')]//button[@aria-label='Close']");
        private AtWebElement CityTaxCloseButton => _webDriver.FindElement(byCityTaxCloseButton);
        private AtBy byFlightDetails => GetBy(LocatorType.XPath, "//*[text()='Return flights']");
        private AtWebElement FlightDetails => _webDriver.FindElement(byFlightDetails);
        private ReadOnlyCollection<AtWebElement> PriceIncludesSectionContent => _webDriver.FindElements(LocatorType.XPath, "//div[.='Price includes:']/following-sibling::ul/li[@class='sc-c-info-list__item']");
        private AtBy ByOverviewTab => GetBy(LocatorType.Id, "overview-tab");
        private AtWebElement OverviewTab => _webDriver.FindElement(ByOverviewTab);
        private AtBy ByOverViewSections(string section) => GetBy(LocatorType.XPath, "//*[text()='#']", section);
        private AtWebElement OverViewSections(string section) => _webDriver.FindElement(ByOverViewSections(section));
        private AtWebElement HotelInformationContent => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Hotel Information']/parent::div//div[contains(@class,'cropper__content')]");
        private AtWebElement HotelInfoToggle => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Hotel Information']/parent::div//a");
        private ReadOnlyCollection<AtWebElement> FacilitiesOptions => _webDriver.FindElements(LocatorType.XPath, "//*[text()='Facilities']/parent::div//span[contains(@class,'label')]");
        private ReadOnlyCollection<AtWebElement> OverviewCustomerReviews => _webDriver.FindElements(LocatorType.XPath, "//*[text()='Our customer reviews']/parent::div//div[contains(@class,'cropper__content')]");
        private AtWebElement SeeAllReviews => _webDriver.FindElement(LocatorType.XPath, "//span[text()='See all customer reviews'] | //span[text()='See All Customer Reviews']");
        private AtBy ByReviewsTab => GetBy(LocatorType.Id, "reviews-tab");
        private AtWebElement ReviewsTab => _webDriver.FindElement(ByReviewsTab);
        private ReadOnlyCollection<AtWebElement> ReviewsContent => _webDriver.FindElements(LocatorType.XPath, "//div[@id='reviews-tabpanel']//div[contains(@class,'cropper__content')]");
        private ReadOnlyCollection<AtWebElement> ReviewsDate => _webDriver.FindElements(LocatorType.XPath, "//div[@id='reviews-tabpanel']//div[contains(@class,'pale-grey')]");
        private AtBy bySeeMoreReviews => GetBy(LocatorType.XPath, "//span[text()='See more reviews'] | //span[text()='Show More Reviews']");
        private AtWebElement SeeMoreReviews => _webDriver.FindElement(bySeeMoreReviews);
        private AtWebElement ReviewContentCropper(string linkText) => _webDriver.FindElement(LocatorType.XPath, "//*[text()='#']", linkText);
        private AtWebElement NoReviewsMessage => _webDriver.FindElement(LocatorType.CssSelector, "#reviews-tabpanel div div");
        private AtBy byBackToRoomsButton => GetBy(LocatorType.CssSelector, "div.sc-c-dialog header button[class*=edge-start]");
        private AtWebElement BackToRoomsButton => _webDriver.FindElement(byBackToRoomsButton);
        private AtBy byDesktopRoomOptions => GetBy(LocatorType.CssSelector, "#rooms-tabpanel div[class*=spacing-default] div[class*=spacing-default]");
        private ReadOnlyCollection<AtWebElement> DesktopRoomOptions => _webDriver.FindElements(byDesktopRoomOptions);
        private AtBy byDesktopHotelRoomType => GetBy(LocatorType.CssSelector, "#rooms-tabpanel *[class*='sc-o-heading sc-u-margin-bottom-m']");
        private ReadOnlyCollection<AtWebElement> DesktopHotelRoomType => _webDriver.FindElements(byDesktopHotelRoomType);
        private AtBy byDesktopBoardType => GetBy(LocatorType.CssSelector, "ul li[class*=option] div[class*=primary] div.sc-o-body:first-child:not([class*=pill])");
        private ReadOnlyCollection<AtWebElement> DesktopBoardType => _webDriver.FindElements(byDesktopBoardType);
        private AtBy byDesktopBoardTypeBox => GetBy(LocatorType.CssSelector, "ul li[class*=option]");
        private AtBy byDesktopBoardOption => GetBy(LocatorType.CssSelector, "ul li[class*=option] div[class*=option--align-top]");
        private AtBy byDesktopBoardTypeOption => GetBy(LocatorType.CssSelector, "div[class*=primary] div");
        private ReadOnlyCollection<AtWebElement> DesktopBoardTypeOption => _webDriver.FindElements(byDesktopBoardTypeOption);
        private AtBy byDesktopRoomDeltaPrice => GetBy(LocatorType.CssSelector, "div[class*=secondary] [class='sc-o-heading sc-u-color-body sc-u-display-block sc-u-no-wrap']");
        private ReadOnlyCollection<AtWebElement> DesktopRoomDeltaPrice => _webDriver.FindElements(byDesktopRoomDeltaPrice);
        private AtBy ByDesktopBoardTypeCheckbox => GetBy(LocatorType.CssSelector, "ul li[class*=option] div.sc-c-checkbox");
       // private AtBy ByDesktopConfirmRoomSelection => GetBy(LocatorType.XPath, "//div[contains(@class,'elevation')]//span[contains(@class,'button__label')]/parent::button");
        private AtBy ByDesktopConfirmRoomSelection => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-sticky')]//button[span[@class='sc-c-button__label']]");
        private AtWebElement DesktopConfirmRoomSelection => _webDriver.FindElement(ByDesktopConfirmRoomSelection);
        private AtBy byPriceCaption => GetBy(LocatorType.XPath, "(//div[contains(@class,'sc-u-align-end')])[1]");
        private AtWebElement PriceCaption => _webDriver.FindElement(byPriceCaption);
        private AtBy ByDesktopBoardTypeCheckBox(int roomNo, int boardTypeNo) => GetBy(LocatorType.XPath, "(//div[@id='rooms-tabpanel']//div[contains(@class,'spacing-default')]//div[contains(@class,'spacing-default')])[#]//ul/li[contains(@class,'option')][#]//div[contains(@class,'sc-c-checkbox--m')]", roomNo.ToString(), boardTypeNo.ToString());
        private AtWebElement DesktopBoardTypeCheckBox(int roomNo, int boardTypeNo) => _webDriver.FindElement(ByDesktopBoardTypeCheckBox(roomNo, boardTypeNo));
        private AtWebElement EstabPageTabs => _webDriver.FindElement(LocatorType.XPath, "//button[@id='rooms-tab']//ancestor::div[contains(@class,'sc-c-sticky')]");
        private AtBy byShowLessLink => GetBy(LocatorType.XPath, "//span[normalize-space()='Show less']");
        private AtWebElement ShowLessLink => _webDriver.FindElement(byShowLessLink);
        private AtBy byOverviewReviewContentCropper => GetBy(LocatorType.XPath, "//*[text()='Our customer reviews']//following::span[contains(@class,'cropper__toggle')]");
        private ReadOnlyCollection<AtWebElement> OverviewReviewContentCropper => _webDriver.FindElements(byOverviewReviewContentCropper);
        private AtBy byOverviewReviewContentFaded => GetBy(LocatorType.XPath, "//*[text()='Our customer reviews']//following::div[contains(@class,'sc-c-content-cropper__content')]");
        private ReadOnlyCollection<AtWebElement> OverviewReviewContentFaded => _webDriver.FindElements(byOverviewReviewContentFaded);
        private AtBy byYourRoomOptionsButton => GetBy(LocatorType.XPath, "//span[contains(text(),'Your Room Options')]/parent::button");
        private AtWebElement YourRoomOptionsButton => _webDriver.FindElement(byYourRoomOptionsButton);
        private AtWebElement SecureTodayPill => _webDriver.FindElement(LocatorType.XPath, "//button[@class='sc-c-pill__surface']//span[contains(text(),'Secure today from')]");
        private AtWebElement FreeCancellationMessage => _webDriver.FindElement(LocatorType.XPath, "//span[contains(text(),'Free hotel cancellation')]");
        private AtWebElement PayMonthlyPill => _webDriver.FindElement(LocatorType.XPath, "//button[@class='sc-c-pill__surface']//span[contains(text(),'Pay monthly')]");
        private AtBy byReturnTransfersMessage => GetBy(LocatorType.XPath, "//span[contains(text(),'Return transfers to hotel')]");
        private AtWebElement ReturnTransfersMessage => _webDriver.FindElement(byReturnTransfersMessage);
        private AtBy byRoomSelectionBadge => GetBy(LocatorType.XPath, "//div[@class='sc-c-stepper']//span[contains(@class,'badge')]");
        private ReadOnlyCollection<AtWebElement> RoomSelectionBadge => _webDriver.FindElements(byRoomSelectionBadge);
        private AtWebElement RoomOccupantsHeader => _webDriver.FindElement(LocatorType.XPath, "//div[@id='rooms-tabpanel']//span[contains(@class,'sc-o-body')]");
        private AtBy byToastMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'toast sc-c-notification--success')]//div[contains(@class,'bold')]");
        private AtWebElement ToastMessage => _webDriver.FindElement(byToastMessage);
        private AtBy byYourRoomsStickyButton => GetBy(LocatorType.XPath, "//div[contains(@class,'background-primary')]//button");
        private AtWebElement YourRoomsStickyButton => _webDriver.FindElement(byYourRoomsStickyButton);
        private AtBy byPreselectedRoomCard => GetBy(LocatorType.CssSelector, "article.sc-c-card--selected");
        private AtWebElement PreselectedRoomCard => _webDriver.FindElement(byPreselectedRoomCard);
        private AtWebElement PreselectedRoomType => _webDriver.FindElement(LocatorType.CssSelector, "*.sc-o-heading--s");
        private AtWebElement PreselectedBoardType => _webDriver.FindElement(LocatorType.CssSelector, "*.sc-u-margin-top-3xs");
        private AtWebElement PreselectedRoomPrice => _webDriver.FindElement(LocatorType.CssSelector, "*.sc-u-color-accent");
        private ReadOnlyCollection<AtWebElement> FreeCancellationMessageInBoardSelection => _webDriver.FindElements(LocatorType.XPath, "//div[contains(text(),'Free hotel cancellation')]");
        private AtWebElement FreeCancellationMessageIngivenRoomandBoardType(int roomType, int boardType) => _webDriver.FindElement(LocatorType.XPath, "((//div[@id='rooms-tabpanel']//div[contains(@class,'spacing-default')]//div[contains(@class,'spacing-default')])[#]//div[contains(text(),'Free hotel cancellation')])[#]", roomType.ToString(), boardType.ToString());
        private ReadOnlyCollection<AtWebElement> SecureTodayPillInBoardSelection => _webDriver.FindElements(LocatorType.XPath, "//ul//span[contains(text(),'Secure today')]");
        private ReadOnlyCollection<AtWebElement> PayMonthlyPillInBoardSelection => _webDriver.FindElements(LocatorType.XPath, "//ul//span[contains(text(),'Pay monthly')]");
        private AtWebElement BoardTypeFilterDropdown => _webDriver.FindElement(LocatorType.Id, "boardTypeFilter");
        private AtBy byRoomsSoldOutError => GetBy(LocatorType.XPath, "//span[contains(text(),'sold out')]");
        private AtBy byBoardTypeInEstabPriceIncludeSection => GetBy(LocatorType.XPath, "(//span[contains(text(),'Room Only')])[1]  | (//span[contains(text(),'Self Catering')])[1] |  (//span[contains(text(),'Bed And Breakfast')]) [1]");
        private AtWebElement BoardTypeInEstabPriceIncludeSection => _webDriver.FindElement(byBoardTypeInEstabPriceIncludeSection);
        private AtBy byNonRefundableInPriceIncludesSection => GetBy(LocatorType.XPath, "//span[text()='Non Refundable']");
        private AtWebElement NonRefundableInPriceIncludesSection => _webDriver.FindElement(byNonRefundableInPriceIncludesSection);
        private readonly IAtWebDriver _webDriver;
        private List<RoomInformation> lRoomInformation;
        private List<BoardTypeInformation> lBoardTypeInformation;

        public HotelEstabPage(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetHotelName()
        {
            _webDriver.WaitForElementVisible(byHotelName, 30, "Hotel name is not visible");
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            return HotelName.Text;
        }

        public int GetHotelStarRating()
        {
            return HotelStarRating.Count;
        }

        public string GetHotelLocation()
        {
            return HotelLocation.Text;
        }

        public string GetHotelRating()
        {
            return HotelRating.Visible ? HotelRating.Text : "0";
        }

        public decimal GetHotelPrice()
        {
            _webDriver.WaitForElementVisible(byHotelName, Constants.DefaultWait, "Hotel name is not visible");
            _webDriver.WaitForElementVisible(byHotelPrice, Constants.DefaultWait, "Hotel price is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelPrice.Text));
        }
        public decimal GetHotelWasPrice()
        {
            _webDriver.WaitForElementVisible(byHotelWasPrice, Constants.MediumWait, "Hotel Was Price is not Visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelWasPrice.Text));
        }
        public string GetSahLabelText()
        {
            _webDriver.WaitForElementVisible(bySashLabel, Constants.MediumWait, "Sash Label is not Visible");
            return SashLabel.Text;
        }
        public string GetHotelPillText()
        {
            _webDriver.WaitForElementVisible(byHotelPill, Constants.MediumWait, "Customer Favourite pill  is not Visible");
            return HotelPill.Text;
        }
        public string GetBoardPromoMessage(int count=0)
        {
            _webDriver.WaitForElementVisible(byBoardPromoMessage, Constants.DefaultWait, "Board promo message is not displayed"); ;
            return BoardPromoMessage[count].Text;
        }
        public string GetRoomPromoMessage(int count = 0)
        {
            _webDriver.WaitForElementVisible(byRoomPromoMessage, Constants.DefaultWait, "Room promo message is not displayed");
            return RoomPromoMessage[count].Text;
        }
        public decimal GetBoardWasPrice(int count = 0)
        {
            _webDriver.WaitForElementVisible(byBoardWasPrice, Constants.DefaultWait, "Board was price in not displayed");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BoardWasPrice[count].Text));
        }
        public decimal GetBoardNowPrice(int count = 0)
        {
            _webDriver.WaitForElementVisible(byBoardNowPrice, Constants.DefaultWait, "Board now price is not displayed");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BoardNowPrice[count].Text));
        }

        public decimal GetHotelPerPersonPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelPerPersonPrice.Text));
        }

        public decimal GetHolidayPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HolidayTotalPrice.Text));
        }

        public decimal GetHolidayPerPersonPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HolidayPerPersonPrice.Text));
        }
        public string GetSearchItinerary()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            _webDriver.WaitForAnyVisible(byRoomsTab, byHotelPrice, Constants.MediumWait, "Rooms tab is not visible");            
            return SearchItinerary.Text;
        }

        public int GetCheckinDayfromSearchItinerary()
        {
            return int.Parse(GetSearchItinerary().Substring(0, 2).Trim());
        }

        public void MoveToRoomsTab()
        {
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            _webDriver.ScrollToElement(RoomsTab);
            if (RoomsTab.GetAttribute("aria-selected").Equals("false"))
            {
                _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
                RoomsTab.ClickButtonUsingJs();
            }
        }

        public void SelectRoom(int roomNo)
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            _webDriver.WaitForElementVisible(byRoomsTab, Constants.DefaultWait, "Rooms tab is not visible");
            MoveToRoomsTab();
            if (!HelperFunctions.IsDesktop())
            {
                _webDriver.ScrollElementToCenter(HotelRooms[roomNo - 1]);
                HotelRooms[roomNo - 1].ClickButtonUsingJs();
            }
        }

        public string GetSelectedRoomType(int roomNo)
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            return HotelRoomType[roomNo - 1].Text;
        }

        public decimal GetSelectedRoomPrice(int roomNo)
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelRoomPrice[roomNo - 1].Text));
        }

        public void SelectBoardType(int boardTypeNo)
        {
            _webDriver.WaitForElementVisible(byHotelBoardType, Constants.DefaultWait, "Board type is not visible");
            _webDriver.ScrollElementToCenter(HotelBoardType[boardTypeNo - 1]);
            HotelBoardType[boardTypeNo - 1].Click();
            _webDriver.MoveToElement(ConfirmSelection);
            ConfirmSelection.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 120);
        }

        public string GetBoardType(int boardTypeNo)
        {
            return HotelBoardType[boardTypeNo - 1].Text;
        }

        public RoomInformation GetRoomInformation(int roomNo)
        {
            RoomInformation roomInfo = new RoomInformation();
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            MoveToRoomsTab();
            _webDriver.WaitForAnyVisible(byHotelRoomType, byDesktopRoomOptions, Constants.MediumWait);
            if (HelperFunctions.IsDesktop())
                roomInfo.RoomType = DesktopHotelRoomType[roomNo - 1].Text;
            else
            {
                roomInfo.RoomType = HotelRoomType[roomNo - 1].Text;
                roomInfo.RoomPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelRoomPrice[roomNo - 1].Text));
            }            
            return roomInfo;
        }

        public BoardTypeInformation GetBoardTypeInformation(int boardTypeNo)
        {
            BoardTypeInformation boardTypeInfo = new BoardTypeInformation();
            _webDriver.WaitForElementVisible(byHotelBoardType, "Boardtype is not visible");
            boardTypeInfo.BoardType = HotelBoardType[boardTypeNo - 1].Text;
            boardTypeInfo.BoardTypePrice = HotelBoardPrice[boardTypeNo - 1].InnerText.Equals("Selected", StringComparison.OrdinalIgnoreCase) ? 0 : Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelBoardPrice[boardTypeNo - 1].InnerText));
            boardTypeInfo.LocalTax = LocalTaxPriceInBoardType.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(LocalTaxPriceInBoardType.Text)) : 0;
            foreach (var boardOption in HotelBoardTypeOptions(boardTypeNo))
            {
                if (boardOption.Text.ToLower().Equals("non-refundable"))
                    boardTypeInfo.isNonRefundable = true;
                else if (boardOption.Text.ToLower().StartsWith("amendment"))
                    boardTypeInfo.isAmendmentAvailable = true;
                else if (boardOption.Text.ToLower().Equals("flexible"))
                    boardTypeInfo.isFlexible = true;
                else if (boardOption.Text.ToLower().Contains("deposit"))
                    boardTypeInfo.isDeposit = true;
                else if (boardOption.Text.ToLower().Contains("refundable"))
                    boardTypeInfo.isRefundable = true;
            }
            return boardTypeInfo;
        }

        public void SelectRoomsAndBoardTypes(int NoOfRooms, string boardType = "")
        {
            int roomCounter = 1;
            int roomTypeCounter = 1;
            int roomToSelect = 1;
            int boardToSelect = 1;
            bool Flexible = boardType == "Flexible" ? true : false;
            bool Deposit = boardType == "Flexible" ? true : false;
            bool NonRefundable = boardType == "NonRefundable" ? true : false;
            bool Amendment = boardType == "Amendment" ? true : false;
            bool Refundable = boardType == "Flexible" ? true : false;
            bool isRoomSelected = false;
            lRoomInformation = new List<RoomInformation>();
            lBoardTypeInformation = new List<BoardTypeInformation>();
            if(boardType != "")
            {
                while (roomCounter <= NoOfRooms)
                {
                    while (roomTypeCounter <= GetRoomTypeCount())
                    {
                        RoomInformation roomInfo = new RoomInformation();
                        BoardTypeInformation boardInfo = new BoardTypeInformation();
                        roomInfo = GetRoomInformation(roomTypeCounter);
                        if(!HelperFunctions.IsDesktop())
                            SelectRoom(roomTypeCounter);
                        int boardTypeCount = HelperFunctions.IsDesktop() ? GetDesktopBoardTypeCount() : GetBoardTypeCount();
                        for (int i = 1; i <= boardTypeCount; i++)
                        {
                            boardInfo = HelperFunctions.IsDesktop() ? GetDesktopBoardTypeInformation(i, roomTypeCounter) : GetBoardTypeInformation(i);
                            //if ((lBoardTypeInformation[i - 1].isDeposit == true && Deposit == true) || (lBoardTypeInformation[i - 1].isFlexible == true && Flexible == true) || (lBoardTypeInformation[i - 1].isNonRefundable == true && NonRefundable == true) || (lBoardTypeInformation[i - 1].isAmendmentAvailable == true && Amendment == true))
                            if ((boardInfo.isFlexible == true && Flexible == true) || (boardInfo.isRefundable == true && Refundable == true) || (boardInfo.isNonRefundable == true && NonRefundable == true))
                            {
                                if (HelperFunctions.IsDesktop())
                                    SelectDesktopBoardType(i, roomTypeCounter);
                                else
                                    SelectBoardType(i);
                                isRoomSelected = true;
                                break;
                            }
                        }
                        if (!isRoomSelected)
                        {
                            roomInfo = new RoomInformation();
                            boardInfo = new BoardTypeInformation();
                            if(!HelperFunctions.IsDesktop())
                                BackToRoomsButton.Click();
                            roomTypeCounter++;
                        }
                        else
                        {
                            lRoomInformation.Add(roomInfo);
                            lBoardTypeInformation.Add(boardInfo);
                            break;
                        }
                    }                    
                    roomCounter++;
                }
                Assert.IsTrue(isRoomSelected, boardType + " room type is not available");
            }
            else
            {
                while (roomCounter <= NoOfRooms)
                {
                    roomToSelect = HelperFunctions.RandomNumber(GetRoomTypeCount());
                    lRoomInformation.Add(GetRoomInformation(roomToSelect));
                    if (HelperFunctions.IsDesktop())
                    {
                        boardToSelect = HelperFunctions.RandomNumber(GetDesktopBoardTypeCount(roomToSelect));
                        lBoardTypeInformation.Add(GetDesktopBoardTypeInformation(boardToSelect, roomToSelect));
                        SelectDesktopBoardType(boardToSelect, roomToSelect);
                    }
                    else
                    {
                        SelectRoom(roomToSelect);
                        boardToSelect = HelperFunctions.RandomNumber(GetBoardTypeCount());
                        lBoardTypeInformation.Add(GetBoardTypeInformation(boardToSelect));
                        SelectBoardType(boardToSelect);
                    }
                    roomCounter++;
                }
            }
            WaitForHotelRoomSelection();
        }

        public void SelectDistinctRoomsTypesOnMobile(int NoOfRooms)
        {
            int roomCounter = 1;
            int boardToSelect = 1;
            lRoomInformation = new List<RoomInformation>();
            lBoardTypeInformation = new List<BoardTypeInformation>();

            if (GetRoomTypeCount() >= NoOfRooms)
            {
                while (roomCounter <= NoOfRooms)
                {
                    lRoomInformation.Add(GetRoomInformation(roomCounter));
                    SelectRoom(roomCounter);
                    boardToSelect = HelperFunctions.RandomNumber(GetBoardTypeCount());
                    lBoardTypeInformation.Add(GetBoardTypeInformation(boardToSelect));
                    SelectBoardType(boardToSelect);
                    roomCounter++;
                }
            }
            else
            {
                throw new Exception("Room types available are " + GetRoomTypeCount()+ " Exepected is "+ (NoOfRooms-1));
            }
           
            WaitForHotelRoomSelection();

        }

        private void WaitForHotelRoomSelection()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait, "Page is still loading after room selection");
            if (_webDriver.FindElement(byRoomsSoldOutError).Visible)
                throw new Exception("Room has sold out Error!");
        }

        public List<RoomInformation> GetSelectedRoomTypes()
        {
            return lRoomInformation;
        }

        public List<BoardTypeInformation> GetSelectedBoardTypes()
        {
            return lBoardTypeInformation;
        }

        public int GetRoomTypeCount()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byRoomsTab, 60, "Rooms tab is not visible");
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            MoveToRoomsTab();
            _webDriver.WaitForAnyVisible(byHotelRoomType, byDesktopRoomOptions, 30);
            if (HelperFunctions.IsDesktop())
                return DesktopRoomOptions.Count;
            else
                return HotelRoomType.Count;
        }

        public int GetRooomToSelectByName(string roomName)
        {
            int i = 0;
            int roomToSelect = 0;
            int noOfRooms = GetRoomTypeCount();
            while (i < noOfRooms)
            {
                if (DesktopHotelRoomType[i].Text.Equals(roomName))
                {
                    roomToSelect = i;
                    break;
                }
                else roomToSelect = -1;
                i++;
            }
            return roomToSelect+1;
        }

        public int GetBoardToSelectByNameInGivenRoom(int room,string boardType)
        {
            int i = 0;
            int boardToSelect = 0;
            int noOfBoards = GetDesktopBoardTypeCount(room);
            while (i < noOfBoards)
            {
                if (DesktopRoomOptions[room - 1].FindElements(byDesktopBoardType)[i].Text.Contains(boardType))
                {
                    boardToSelect = i;
                    break;
                }
                else boardToSelect = -1;
            }
            return boardToSelect+1;
        }

        public int GetBoardTypeCount()
        {
            _webDriver.WaitForElementVisible(byHotelBoardType, 30, "Boardtype is not visible");
            return HotelBoardType.Count;
        }

        public void SelectPerPersonPriceToggle()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            MoveToRoomsTab();
            _webDriver.WaitForAnyVisible(byHotelRoomType, byDesktopRoomOptions, 10);
            _webDriver.WaitForElementClickable(byPerPersonPriceRadio, 10);
            PerPersonPriceRadio.ClickButtonUsingJs();
        }

        public void SelectTotalPriceToggle()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            MoveToRoomsTab();
            _webDriver.WaitForAnyVisible(byHotelRoomType, byDesktopRoomOptions, 10);
            _webDriver.WaitForElementClickable(byPerPersonPriceRadio, 10);
            TotalPriceRadio.ClickButtonUsingJs();
        }

        public bool VerifyDefaultSelectedPrice()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            MoveToRoomsTab();
            _webDriver.WaitForAnyVisible(byHotelRoomType, byDesktopRoomOptions, 10);
            _webDriver.WaitForElementVisible(byPerPersonPriceRadio, 10, "Per person price radio button is not visible");
            return TotalPriceRadio.GetAttribute("class").Contains("selected");
        }

        public string GetTotalPriceToggleSwitchBackgorundColor()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            MoveToRoomsTab();
            _webDriver.WaitForElementVisible(byHotelRoomType, "Roomtype is not visible");

            return TotalPriceRadio.GetCssValue("background-color");
        }

        public string GetPerPersonPriceToggleSwitchBackgorundColor()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            MoveToRoomsTab();
            _webDriver.WaitForElementVisible(byHotelRoomType, "Roomtype is not visible");

            return PerPersonPriceRadio.GetCssValue("background-color");
        }

        public string GetLocalTaxes()
        {
            _webDriver.WaitForElementVisible(ByOverviewTab, 60, "Overview tab is not visible");
            _webDriver.WaitForElementClickable(byLocalChargesLink, 30);
            _webDriver.ScrollToElement(LocalChargesLink);
            LocalChargesLink.ClickButtonUsingJs();
            _webDriver.WaitForElementVisible(byHotelLocalChargesAmount, 20, "Local tax is not visible");
            return HotelLocalChargesAmount.Text;
        }

        public void CloseLocalTaxPopup()
        {
            _webDriver.WaitForElementClickable(byCityTaxCloseButton, 10);
            CityTaxCloseButton.ClickButtonUsingJs();
        }

        public void ViewFlightDetails()
        {
            _webDriver.WaitForElementClickable(byFlightDetails, 60);
            _webDriver.ScrollToElement(FlightDetails);
            FlightDetails.ClickButtonUsingJs();
        }

        public bool IsBoardTypeDisplayedInPriceIncludesSection()
        {
            return PriceIncludesSectionContent.Count > 1;
        }

        public void VerifyOverviewTab()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(ByOverviewTab, 60, "Overview tab is not visible");
            _webDriver.ScrollToElement(OverviewTab);
            Assert.IsTrue(OverviewTab.GetAttribute("aria-selected").Equals("true"), "Overview tab is not selected");
        }

        public void ClickOverviewTab()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(ByOverviewTab, 60, "Overview tab is not visible");
            _webDriver.ScrollToElement(OverviewTab);
            OverviewTab.Click();
        }

        public void ClickRoomsTab()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementClickable(byRoomsTab, 60);
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.ScrollElementToCenter(RoomsTab);
            _webDriver.WaitForElementClickable(byRoomsTab, 60);
            RoomsTab.Click();
        }

        public void ClickReviewsTab()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(ByReviewsTab, 60, "Reviews tab is not visible");
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, 60);
            _webDriver.ScrollToElement(ReviewsTab);
            ReviewsTab.Click();
        }

        public void VerifyHotelInformationSection()
        {
            _webDriver.WaitForElementVisible(ByOverViewSections("Hotel Information"), 60, "Hotel information section is not visible");
            _webDriver.ScrollToElement(OverViewSections("Hotel Information"));
            Assert.IsTrue(HotelInformationContent.GetAttribute("aria-expanded").Equals("false"));
        }

        public void VerifyHotelInfoToggleSwitch()
        {
            if (HotelInfoToggle.Visible)
            {
                _webDriver.ScrollElementToCenter(HotelInfoToggle);
                Assert.AreEqual("Show more", HotelInfoToggle.Text);
                HotelInfoToggle.Click();
                Assert.IsTrue(HotelInformationContent.GetAttribute("aria-expanded").Equals("true"));
                Assert.AreEqual("Show less", HotelInfoToggle.Text);
                _webDriver.ScrollElementToCenter(HotelInfoToggle);
                HotelInfoToggle.Click();
                Assert.IsTrue(HotelInformationContent.GetAttribute("aria-expanded").Equals("false"));
            }
        }

        public void VerifyFacilitiesSection()
        {
            _webDriver.WaitForElementVisible(ByOverViewSections("Facilities"), 60, "Facilities section is not visible");
            _webDriver.ScrollToElement(OverViewSections("Facilities"));
            Assert.Greater(FacilitiesOptions.Count, 0);
        }

        public List<string> GetFacilities()
        {
            _webDriver.WaitForElementVisible(ByOverViewSections("Facilities"), 60, "Facilities section is not visible");
            List<string> facilities = new List<string>();
            foreach(var option in FacilitiesOptions)
            {
                facilities.Add(option.Text.Trim());
            }
            return facilities;
        }

        public void VerifyReviewsSectionInOverView()
        {
            _webDriver.WaitForElementVisible(ByOverViewSections("Our customer reviews"), 30, "Our customer reviews section is not visible");
            _webDriver.ScrollToElement(OverViewSections("Our customer reviews"));
            Assert.IsTrue(OverviewCustomerReviews.Count > 0 && OverviewCustomerReviews.Count <= 2);
        }

        public void ClickSeeAllCustomerReviews()
        {
            if (!SeeAllReviews.Visible)
                Assert.Fail("Reviews is not available for selected hotel");
            _webDriver.ScrollElementToCenter(SeeAllReviews);
            SeeAllReviews.Click();
            Assert.IsTrue(ReviewsTab.GetAttribute("aria-selected").Equals("true"));
        }

        public int GetReviewsCount()
        {
            return ReviewsContent.Count;
        }


        public List<string> GetAllBoardTypeNames()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            _webDriver.WaitForElementVisible(byHotelName, 30, "Hotel name is not visible");
            MoveToRoomsTab();
            List<string> boardNames = new List<string>(); 
            foreach(var boardType in HotelBoardType)
            {
                boardNames.Add(boardType.Text);
            }
            return boardNames;
        }

        public void ClickSeeMoreReviews()
        {
            if (SeeMoreReviews.Visible)
            {
                _webDriver.ScrollElementToCenter(SeeMoreReviews);
                SeeMoreReviews.Click();
                _webDriver.WaitForElementClickable(bySeeMoreReviews, 10);
            }
            else
                Assert.Inconclusive("See more reviews option is not available!");
        }

        public bool VerifyReviewsDisplayedInAscendingOrderByDate()
        {
            for(int reviewCounter = 1; reviewCounter < ReviewsDate.Count; reviewCounter++)
            {
                if (Convert.ToDateTime(ReviewsDate[reviewCounter - 1].Text.Replace("Checked in ", "")) < Convert.ToDateTime(ReviewsDate[reviewCounter].Text.Replace("Checked in ", "")))
                    return false;
            }
            return true;
        }

        public void VerifySeeWholeReview()
        {
            if (!(ReviewContentCropper("Show whole review...").Visible) & SeeMoreReviews.Visible)
            {
                ClickSeeMoreReviews();
            }
            if (ReviewContentCropper("Show whole review...").Visible)
            {
                _webDriver.ScrollElementToCenter(ReviewContentCropper("Show whole review..."));
                ReviewContentCropper("Show whole review...").Click();
                Assert.IsTrue(ReviewContentCropper("Show less").Visible);
            }                
            if (!(SeeMoreReviews.Visible) && !(ReviewContentCropper("Show whole review...").Visible))
                Assert.Inconclusive("See whole reviews functionality is not tested!");
        }

        public void VerifyNoReviewsMessage()
        {
            if (!NoReviewsMessage.Visible)
                Assert.Inconclusive("No reviews functionality is not tested");
            else
            {
                Assert.IsTrue(NoReviewsMessage.Visible);
                Assert.AreEqual("We are still waiting for our first customer review!", NoReviewsMessage.Text);
            }
        }

        public int GetDesktopBoardTypeCount(int room = 1)
        {
            _webDriver.WaitForElementVisible(byDesktopRoomOptions, 30, "Room options is not visible");
            return DesktopRoomOptions[room - 1].FindElements(byDesktopBoardType).Count;
        }

        public BoardTypeInformation GetDesktopBoardTypeInformation(int boardToSelect, int room = 1)
        {
            BoardTypeInformation boardTypeInfo = new BoardTypeInformation();
            _webDriver.WaitForElementVisible(byDesktopRoomOptions, 30, "Room options is not visible");
            boardTypeInfo.BoardType = DesktopRoomOptions[room - 1].FindElements(byDesktopBoardType)[boardToSelect - 1].Text;
            boardTypeInfo.BoardTypePrice = DesktopRoomOptions[room - 1].FindElements(byDesktopRoomDeltaPrice)[boardToSelect - 1].Text.Equals("Selected", StringComparison.OrdinalIgnoreCase) ? 0 : Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(DesktopRoomOptions[room - 1].FindElements(byDesktopRoomDeltaPrice)[boardToSelect - 1].Text));
            //boardTypeInfo.LocalTax = LocalTaxPriceInBoardType.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(LocalTaxPriceInBoardType.Text)) : 0;
            foreach (var boardOption in DesktopRoomOptions[room - 1].FindElements(byDesktopBoardOption)[boardToSelect - 1].FindElements(byDesktopBoardTypeOption))
            {
                if (boardOption.Text.ToLower().Equals("non-refundable"))
                    boardTypeInfo.isNonRefundable = true;
                else if (boardOption.Text.ToLower().StartsWith("amendment"))
                    boardTypeInfo.isAmendmentAvailable = true;
                else if (boardOption.Text.ToLower().Equals("flexible"))
                    boardTypeInfo.isFlexible = true;
                else if (boardOption.Text.ToLower().Contains("deposit"))
                    boardTypeInfo.isDeposit = true;
                else if (boardOption.Text.ToLower().Contains("refundable"))
                    boardTypeInfo.isRefundable = true;
            }
            return boardTypeInfo;
        }

        public void SelectDesktopBoardType(int boardToSelect, int room = 1)
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            _webDriver.WaitForElementVisible(byRoomsTab, 20, "Rooms tab is not visible");
            MoveToRoomsTab();
            _webDriver.WaitForElementClickable(ByDesktopBoardTypeCheckBox(room, boardToSelect), 10);
            _webDriver.ScrollElementToCenter(DesktopBoardTypeCheckBox(room, boardToSelect));
            DesktopBoardTypeCheckBox(room, boardToSelect).Click();
            _webDriver.WaitForElementClickable(ByDesktopConfirmRoomSelection, Constants.DefaultWait);
            DesktopConfirmRoomSelection.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            WaitForHotelRoomSelection();
        }

        public string GetPriceCaption()
        {
            _webDriver.WaitForElementVisible(byPriceCaption, Constants.MediumWait, "Price caption is not visible");
            return PriceCaption.Text;
        }

        public string GetContinueButtonText()
        {
            return ConfirmSelection.Text;
        }

        public bool IsEstabPageDisplayed()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            return OverviewTab.Visible;
        }

        public bool ValidatePropertyAmenities(string filteredOption)
        {
            _webDriver.WaitForElementClickable(ByOverviewTab, Constants.DefaultWait,"Overview Tab is not visible");
            OverviewTab.ClickButtonUsingJs();
            return GetFacilities().Contains(filteredOption);
        }

        public bool ValidateBoardType(string filterdBoardType)
        {
            List<string> estabBoardNames = GetAllBoardTypeNames();
            bool IsBoardAvaialble = false;
            foreach (string estabBoardName in estabBoardNames)
            {
                if (estabBoardName.Contains(filterdBoardType.Replace("&","And").Replace("-"," ")))
                {
                    IsBoardAvaialble = true;
                    break;
                }
            }
            return IsBoardAvaialble;
        }



        public bool ValidatePropertyAmenities(List<string> filteredOptions)
        {
            _webDriver.WaitForElementClickable(ByOverviewTab,20,"Overview Tab is not Clickable");
            OverviewTab.ClickButtonUsingJs();
            List<string> facilities = GetFacilities();
            foreach (var propertyAmenity in filteredOptions)
            {
                if (!facilities.Contains(propertyAmenity))
                    return false;
            }
            return true;
        }

        public bool IsChooseRoomsButtonDisplayed()
        {
            return ChooseRoomsButton.Visible;
        }

        public void ClickChooseRoomButton()
        {
            ChooseRoomsButton.Click();
            _webDriver.WaitUntilNotVisible(byChooseRoomsButton, Constants.ShortWait);
        }

        public bool IsRoomsTabSelected()
        {
            return RoomsTab.GetAttribute("aria-selected").Equals("true");
        }

        public bool IsRoomsTabScrolledToTop()
        {
            _webDriver.WaitForDomReady();
            return EstabPageTabs.GetAttribute("class").Contains("sticky--fixed");
        }
        public void ClickShowWholeReviewLink()
        {
            _webDriver.WaitForElementVisible(byOverviewReviewContentCropper, 30, "ShowWholeReview Link is visible");
            _webDriver.ScrollElementToCenter(OverviewReviewContentCropper.First());
            OverviewReviewContentCropper.First().Click();
        }
        public bool IsReviewContentFaded()
        {
            _webDriver.WaitForElementVisible(byOverviewReviewContentFaded, 30, "Review content is Faded");
            _webDriver.ScrollElementToCenter(OverviewReviewContentFaded.First());
            return OverviewReviewContentFaded.First().Visible;
        }

        public bool IsShowLessLinkDisplayed()
        {
            return ShowLessLink.Visible;
        }

        public bool IsSeeAllReviewsDisplayed()
        {
            return SeeAllReviews.Visible;
        }

        public bool IsYourRoomOptionsButtonDisplayed()
        {
            return YourRoomOptionsButton.Visible;
        }

        public void ClickYourRoomOptionsButton()
        {
            _webDriver.ScrollElementToCenter(YourRoomOptionsButton);
            YourRoomOptionsButton.Click();
            //_webDriver.WaitUntilNotVisible(byYourRoomOptionsButton, Constants.ShortWait);
        }

        public int GetPriceIncludesOptionsCount()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return PriceIncludesSectionContent.Count;
        }

        public bool IsSecureTodayPillDisplayed()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return SecureTodayPill.Visible;
        }

        public decimal GetDepositPriceFromSecurePill()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(SecureTodayPill.Text));
        }

        public void ClickSecureTodayPill()
        {
            _webDriver.ScrollElementToCenter(SecureTodayPill);
            SecureTodayPill.Click();
        }

        public bool IsFreeCancellationLinkDisplayed()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return FreeCancellationMessage.Visible;
        }

        public void ClickFreeCancellationMessage()
        {
            _webDriver.ScrollElementToCenter(FreeCancellationMessage);
            FreeCancellationMessage.Click();
        }

        public string GetFreeHotelCancellationTextInPriceIncludesSection()
        {            
                _webDriver.WaitForElementVisible(byHotelPrice, Constants.DefaultWait, "Hotel price is not visible");
                _webDriver.ScrollElementToCenter(FreeCancellationMessage);
                return FreeCancellationMessage.Text;                                                      
        }

        public string GetFreeHotelCancellationTextInBoardType(int roomType,int boardType)
        {
            _webDriver.ScrollElementToCenter(FreeCancellationMessageIngivenRoomandBoardType(roomType,boardType));
            return FreeCancellationMessageIngivenRoomandBoardType(roomType, boardType).Text;
        }

        public bool IsPayMonthlyPillDisplayed()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return PayMonthlyPill.Visible;
        }

        public decimal GetAmountFromPayMonthlyPill()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PayMonthlyPill.Text));
        }

        public void ClickPayMonthlyPill()
        {
            _webDriver.ScrollElementToCenter(PayMonthlyPill);
            PayMonthlyPill.Click();
        }

        public bool IsReturnTransfersMessageDisplayed()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait, "Search results loader is still visible");
            _webDriver.WaitForElementVisible(byReturnTransfersMessage, Constants.MediumWait, "Return Transfer MEssage in not Visible");
            _webDriver.ScrollElementToCenter(ReturnTransfersMessage);
            return ReturnTransfersMessage.Visible;
        }

        public string GetRoomNumberColor(int roomNo)
        {
            _webDriver.WaitForElementVisible(byRoomSelectionBadge, Constants.ShortWait, "Room selection badge isnot visible");
            _webDriver.ScrollElementToCenter(RoomSelectionBadge[roomNo - 1]);
            return RoomSelectionBadge[roomNo - 1].GetCssValue("background-color");
        }

        public string GetRoomHeaderText()
        {
            return RoomOccupantsHeader.Text;
        }

        public string GetToastMessage()
        {
            _webDriver.WaitForElementVisible(byToastMessage, Constants.ShortWait, "Toast Message is not visible");
            return ToastMessage.Text;
        }

        public void ClickRoomSelectionBadge(int roomNo)
        {
            _webDriver.ScrollElementToCenter(RoomSelectionBadge[roomNo - 1]);
            RoomSelectionBadge[roomNo - 1].Click();
        }

        public bool IsYourRoomStickyButtonDisplayed()
        {
            _webDriver.WaitForElementVisible(byYourRoomsStickyButton, Constants.ShortWait, "Your room sticky footer button is not visible");
            return YourRoomsStickyButton.Visible;
        }

        public void ClickYourRoomStickyButton()
        {
            _webDriver.ScrollElementToCenter(YourRoomsStickyButton);
            YourRoomsStickyButton.ClickButtonUsingJs();
        }

        public bool IsPreSelectedRoomDisplayed()
        {
            _webDriver.WaitForElementVisible(byPreselectedRoomCard, 20, "Preselcted Room card is not visible");
            _webDriver.ScrollElementToCenter(PreselectedRoomCard);
            return PreselectedRoomCard.Visible;
        }

        public string GetPreSelectedRoomType()
        {
            return PreselectedRoomCard.FindElement(PreselectedRoomType).Text;
        }

        public string GetPreSelectedBoardType()
        {
            return PreselectedRoomCard.FindElement(PreselectedBoardType).Text;
        }

        public decimal GetPreSelectedRoomPrice()
        {
            _webDriver.WaitForElementVisible(byPreselectedRoomCard, 10, "preselected room card is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PreselectedRoomCard.FindElement(PreselectedRoomPrice).Text));
        }

        public bool IsFreeCancellationMessageDisplayedInBoardType()
        {
            _webDriver.WaitForElementVisible(byHotelBoardType, 20, "Board type is not visible");
            _webDriver.ScrollElementToCenter(FreeCancellationMessageInBoardSelection.FirstOrDefault());
            return FreeCancellationMessageInBoardSelection.FirstOrDefault().Visible;
        }

        public bool IsSecureTodayPillDisplayedInBoardType()
        {
            _webDriver.WaitForElementVisible(byHotelBoardType, 20, "Board type is not visible");
            _webDriver.ScrollElementToCenter(SecureTodayPillInBoardSelection.FirstOrDefault());
            return SecureTodayPillInBoardSelection.FirstOrDefault().Visible;
        }

        public bool IsPayMonthlyPillDisplayedInBoardType()
        {
            _webDriver.WaitForElementVisible(byHotelBoardType, 20, "Board type is not visible");
            _webDriver.ScrollElementToCenter(PayMonthlyPillInBoardSelection.FirstOrDefault());
            return PayMonthlyPillInBoardSelection.FirstOrDefault().Visible;
        }

        public string SelectRandomBoardFilter()
        {
            SelectElement boardType = new SelectElement(BoardTypeFilterDropdown);
            int valueToSelect = HelperFunctions.RandomNumber(boardType.Options.Count, 2);
            boardType.SelectByIndex(valueToSelect - 1);
            return boardType.SelectedOption.Text;
        }
        public bool IsLocalChargesMessageDisplayed()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            return LocalChargesLink.Visible;
        }
        public void ClickLocalChargesMessage()
        {
            _webDriver.ScrollElementToCenter(LocalChargesLink);
            LocalChargesLink.Click();
        }
        public void ClickBackToRooms()
        {
            _webDriver.WaitForElementClickable(byBackToRoomsButton, 20, "Back room button is not clickable");
            BackToRoomsButton.Click();
        }

        public bool IsHolidayJourney()
        {
            return _webDriver.Url.Contains("holidays");
        }

        public string GetBoardTypeInEstabPriceIncludeSection()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.ShortWait, "Search results loader is still visible");
            _webDriver.WaitForElementVisible(byBoardTypeInEstabPriceIncludeSection, 20, "Board type is not visible");
            return BoardTypeInEstabPriceIncludeSection.Text;
        }

        public bool IsNonRefundable()
        {
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait, "Search results loader is still visible");
            _webDriver.WaitForElementVisible(byHotelPrice, Constants.DefaultWait, "Hotel price is not visible");
            return NonRefundableInPriceIncludesSection.Visible;
        }

        public void ClickOnNonRefundableMessageLink()
        {
            _webDriver.ScrollElementToCenter(NonRefundableInPriceIncludesSection);
            NonRefundableInPriceIncludesSection.Click();
        }
    }
}
