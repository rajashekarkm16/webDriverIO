using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class BookingSummary : MobileBasePage, IBookingSummary
    {
        private AtWebElement BookingSummaryPageHeader => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='Booking Summary']");
        private AtBy byCloseBookingSummary => GetBy(LocatorType.XPath, "//header[contains(@class,'elevated')]//button[@aria-label='Close']");
        private AtWebElement CloseBookingSummary => _webDriver.FindElement(byCloseBookingSummary); 
        private AtBy bySelectedHotelName => GetBy(LocatorType.XPath, "//span[contains(@class,'star')]/parent::div/h3[contains(@class,'sc-o-heading')]");
        private AtWebElement SelectedHotelName => _webDriver.FindElement(bySelectedHotelName);
        private ReadOnlyCollection<AtWebElement> SelectedHotelStarRating => _webDriver.FindElements(LocatorType.CssSelector, "span.sc-c-star-rating svg");
        private AtWebElement Duration => _webDriver.FindElement(LocatorType.XPath, "//span[contains(@class,'star')]/parent::div/following-sibling::div/span");
        private AtBy byRooms => GetBy(LocatorType.XPath, "//*[contains(@class,'tertiary')]/parent::div/preceding-sibling::div");
        private ReadOnlyCollection<AtWebElement> Rooms => _webDriver.FindElements(byRooms);
        private AtBy byroomType => GetBy(LocatorType.XPath, "//div[@class='sc-u-padding-default']//div[contains(@class,'xl')]//*[contains(@class,'display-block') and not(contains(@class,'tertiary'))][1]");
        private ReadOnlyCollection<AtWebElement> RoomType => _webDriver.FindElements(byroomType);
        private AtBy byBoardType => GetBy(LocatorType.XPath, "//div[@class='sc-u-padding-default']//div[contains(@class,'xl')]//*[contains(@class,'display-block')][2]");
        private ReadOnlyCollection<AtWebElement> BoardType => _webDriver.FindElements(byBoardType);
        private AtBy byOccupants => GetBy(LocatorType.XPath, "//div[@class='sc-u-padding-default']//div[contains(@class,'xl')]//*[contains(@class,'display-block')][3]");
        private ReadOnlyCollection<AtWebElement> Occupants => _webDriver.FindElements(byOccupants);
        private AtWebElement AmmendmentType => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'nowrap')]//*[contains(@class,'tertiary')]");
        private AtWebElement AmmendmentTypeInfoIcon => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'nowrap')]//*[contains(@class,'tertiary')]/*");
        private ReadOnlyCollection<AtWebElement> AmendmentInfoText => _webDriver.FindElements(LocatorType.CssSelector, "div[id=hype-message-description] p");
        private ReadOnlyCollection<AtWebElement> AmendmentInfoLinks => _webDriver.FindElements(LocatorType.CssSelector, "div[id=hype-message-description] a");
        private AtWebElement RoomstotalPrice => _webDriver.FindElement(LocatorType.XPath, "//div[text()='Rooms total']//parent::div[contains(@class,'sc-o-flex-grid--items-baseline')]/div/span");
        private AtWebElement TotalPriceDescription => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Total price']");
        private AtBy byDiscountPrice => GetBy(LocatorType.XPath, "//div[text()='Discount']//parent::div[contains(@class,'sc-o-flex-grid--items-baseline')]/div/span");
        private AtWebElement DiscountPrice => _webDriver.FindElement(byDiscountPrice);
        private AtWebElement AdminFee => _webDriver.FindElement(LocatorType.XPath, "//div[text()='Admin Fee']//parent::div[contains(@class,'sc-o-flex-grid--items-baseline')]/div/span");
        private AtBy byTotalPrice => GetBy(LocatorType.XPath, "//span[text()='Total price' or text()='Holiday Total']/parent::div/parent::div//div[2]//span[contains(@class,'color-accent')]");
        private AtWebElement TotalPrice => _webDriver.FindElement(byTotalPrice);
        private ReadOnlyCollection<AtWebElement> CostBreakdownDescription => _webDriver.FindElements(LocatorType.CssSelector, "span[class*=description]");
        private ReadOnlyCollection<AtWebElement> CostBreakdownPrice => _webDriver.FindElements(LocatorType.CssSelector, "span[class*=price]");
        private AtBy bySubmitButton => GetBy(LocatorType.CssSelector, "button[class*=accent] span[class*=label]");
        private AtWebElement SubmitButton => _webDriver.FindElement(bySubmitButton);
        private AtBy byLocalTaxAmount => GetBy(LocatorType.XPath, "//span[contains(text(),'tax')]/parent::div/following-sibling::div");
        private AtWebElement LocalTaxAmount => _webDriver.FindElement(byLocalTaxAmount);
        private AtBy byPayNowAmount => GetBy(LocatorType.XPath, "//*[text()='Secure Today For']/ancestor::div[contains(@class,'baseline')]/div[2]/span");
        private AtWebElement PayNowAmount => _webDriver.FindElement(byPayNowAmount);        
        private AtBy byInsuranceAmount => GetBy(LocatorType.XPath, "//div[text()='Insurance Total']/following-sibling::div/span");
        private AtWebElement InsuranceAmount => _webDriver.FindElement(byInsuranceAmount);
        private AtBy byAdditionalBagInfo => GetBy(LocatorType.XPath, "//h3[text()='Additional Bags']/following-sibling::div");
        private AtWebElement AdditionalBagInfo => _webDriver.FindElement(byAdditionalBagInfo);
        private AtBy byFlightsTotal => GetBy(LocatorType.XPath, "//div[text()='Flights total']/following-sibling::div/span");
        private AtWebElement FlightsTotal => _webDriver.FindElement(byFlightsTotal);
        private AtWebElement ATOLProtectionPrice => _webDriver.FindElement(LocatorType.XPath,"//div[text()='ATOL Protection']/following-sibling::div/span");
        private AtBy byChangeRoom => GetBy(LocatorType.XPath, "//a[text()='Change room']");
        private ReadOnlyCollection<AtWebElement> ChangeRoom => _webDriver.FindElements(byChangeRoom);
        private AtBy byRoomInfoLink => GetBy(LocatorType.XPath, "//span[contains(@class, 'sc-c-info-item__text')]");
        private ReadOnlyCollection<AtWebElement> RoomInfoLink => _webDriver.FindElements(byRoomInfoLink);
        private AtWebElement RoomInfoModalContent => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-dialog div.sc-c-dialog-content div");
        private AtWebElement CloseRoomInfoModel => _webDriver.FindElement(LocatorType.CssSelector, "div.sc-c-dialog button svg");
        private AtBy byPriceInHeader => GetBy(LocatorType.CssSelector, "*.sc-u-no-wrap");
        private AtWebElement PriceInHeader => _webDriver.FindElement(byPriceInHeader);
        private AtBy byBookingSummaryHeader => GetBy(LocatorType.XPath, "//span[text()='Booking Summary']//ancestor::header");
        private AtWebElement BookingSummaryHeader => _webDriver.FindElement(byBookingSummaryHeader);        
        private AtWebElement TotalPriceOnHeader => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Booking Summary']//ancestor::header//span[contains(@class,'heading')]");
        private AtBy ByFlightSection => GetBy(LocatorType.XPath, "//*[text()='Flights']/parent::div[@class='sc-u-padding-default']");
        private AtWebElement FlightSection => _webDriver.FindElement(ByFlightSection);
        private AtBy ByFlightLegs => GetBy(LocatorType.XPath, "//div[contains(@class,'margin-y-m')]");
        private AtBy ByAirports => GetBy(LocatorType.XPath, "//*[text()='Flights']/parent::div[@class='sc-u-padding-default']//span[@class='sc-o-body' and not(contains(@class,'bold'))][1]");
        private AtBy ByFlightJourneyDetails => GetBy(LocatorType.XPath, "//div[@class='sc-u-padding-default']//span[2]");
        private AtBy ByBaggageSection => GetBy(LocatorType.XPath, "//*[text()='Additional Bags']/parent::*");
        private AtBy ByBaggageAmount => GetBy(LocatorType.XPath, "//div[text()='Bags total']/following-sibling::div/span");
        private AtWebElement BaggageAmount => _webDriver.FindElement(ByBaggageAmount);
        private AtBy ByTranfserSection => GetBy(LocatorType.XPath, "//*[text()='Transfers']/parent::div");
        private AtWebElement TransferSection => _webDriver.FindElement(ByTranfserSection);
        private AtBy ByTransferName => GetBy(LocatorType.XPath, "//*[text()='Transfers']/parent::div//*[contains(@class,'margin-y-l')][1]");
        private AtWebElement TransferName => _webDriver.FindElement(ByTransferName);
        private AtBy ByTransfersAmount => GetBy(LocatorType.XPath, "//div[text()='Transfers Total']/following-sibling::div/span");
        private AtBy ByInsuranceSection => GetBy(LocatorType.XPath, "//*[text()='Insurance']/parent::div");
        private AtWebElement InsuranceSection => _webDriver.FindElement(ByInsuranceSection);
        private AtBy ByInsuranceName => GetBy(LocatorType.XPath, "//*[text()='Insurance']/parent::div//*[contains(@class,'margin-y-l')][1]");
        private AtWebElement InsuranceName => _webDriver.FindElement(ByInsuranceName);
        private AtBy ByInsuranceAmount => GetBy(LocatorType.XPath, "//div[text()='Insurance Total']/following-sibling::div/span");
        private AtBy ByPackageAmount => GetBy(LocatorType.XPath, "//div[text()='Package price']/following-sibling::div/span");
        private AtWebElement PackageAmount => _webDriver.FindElement(ByPackageAmount);
        private AtWebElement TotalPayableAmount => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'color-accent')]//span[contains(@class,'body--xl')]");
        private AtBy ByMandatoryTransferName => GetBy(LocatorType.XPath, "//*[text()='Transfers']/parent::div//*[contains(@class,'margin-y-l') and not(contains(@class,'divider'))]");
        private AtBy ByMandatoryTransferPrice => GetBy(LocatorType.XPath, "//*[text()='Transfers']/parent::div//span[@class='sc-o-body']");
        private AtBy ByFlightAllocationBaggageInfo => GetBy(LocatorType.XPath, "//*[text()='Baggage']/parent::div/div[@class='sc-o-body']");
        private AtWebElement FlightAllocationBaggageInfo => _webDriver.FindElement(ByFlightAllocationBaggageInfo);
        private AtWebElement FlightAllocationBaggageCost => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Baggage']/parent::div/div[contains(@class,'sc-u-color-success')]");
        private AtBy byPageLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byCovidCoverPlus => GetBy(LocatorType.XPath, "//div[text()='Covid Cover Plus']//parent::div[contains(@class,'sc-o-flex-grid--items-baseline')]/div/span");
        private AtWebElement CovidCoverPlus => _webDriver.FindElement(byCovidCoverPlus);
        private ReadOnlyCollection<AtWebElement> FreeHotelCancellationMessage => _webDriver.FindElements(LocatorType.XPath, "//span[contains(text(),'Free hotel cancellation')]");
        private AtBy ByAdditionalPackageInclusionsHeading => GetBy(LocatorType.XPath, "//h3[normalize-space()='Additional Package Inclusions']");
        private AtWebElement AdditionalPackageInclusionsHeading => _webDriver.FindElement(ByAdditionalPackageInclusionsHeading);
        private ReadOnlyCollection<AtWebElement> AdditionalPackageInclusionsItems => _webDriver.FindElements(LocatorType.XPath, "//div[h3[normalize-space()='Additional Package Inclusions']]//span");
        private ReadOnlyCollection<AtWebElement> AdditionalPackageInclusionsItemInfoIcons => _webDriver.FindElements(LocatorType.XPath, "//div[h3[normalize-space()='Additional Package Inclusions']]//*[local-name()='svg']");
        private readonly IAtWebDriver _webDriver;
        private readonly IModalPopup _modalPopup;

        private decimal? TotalPayable { get; set; }
        public BookingSummary(IAtWebDriver webDriver, IModalPopup modalPopup)
            : base(webDriver)
        {
            _webDriver = webDriver;
            _modalPopup = modalPopup;
        }

        public decimal? GetTotalPaidAmount()
        {
            return TotalPayable;
        }

        public void SetTotalPayableAmount(decimal value)
        {
            TotalPayable = value;
        }

        public bool IsBookingSummaryPage()
        {
            return BookingSummaryPageHeader.Visible;

        }
        public void CapturePayabaleAmount()
        {
            if(TotalPayableAmount.Visible)
                SetTotalPayableAmount(Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TotalPayableAmount.Text)));
        }

        public string GetSelectedHotelName()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, Constants.LongWait, "Selected hotel name is not visible");
            return SelectedHotelName.Text;
        }

        public string GetSearchItinerary()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 60, "Selected hotel name is not visible");
            return Duration.Text;
        }

        public void VerifyItineraryDetails()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 60, "Selected hotel name is not visible");
            Assert.IsTrue(SelectedHotelName.Visible);
            Assert.IsTrue(SelectedHotelStarRating[0].Visible);
            Assert.IsTrue(Duration.Visible);
            Assert.IsTrue(AmmendmentType.Visible);
            Assert.IsTrue(TotalPriceDescription.Visible);
            Assert.IsTrue(TotalPrice.Visible);
            Assert.IsTrue(SubmitButton.Visible);
            
        }

        public void ClickConfirmButton()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, Constants.DefaultWait, "Selected hotel name is not visible");
            _webDriver.WaitForElementClickable(bySubmitButton, Constants.DefaultWait);
            _webDriver.ScrollElementToCenter(SubmitButton);
            SubmitButton.ClickButtonUsingJs();
        }

        public string GetRoomType(int roomNo)
        {
            _webDriver.WaitForElementVisible(byroomType, 60, "Room type is not visible");
            return RoomType[roomNo - 1].Text;
        }

        public string GetBoardType(int roomNo)
        {
            _webDriver.WaitForElementVisible(byBoardType, 20, "Board type is not visible");
            return BoardType[roomNo - 1].Text;
        }

        public string GetOccupants(int roomNo)
        {
            _webDriver.WaitForElementVisible(byRooms, 20, "Room type is not visible");
            return Occupants[roomNo - 1].Text;
        }

        public decimal GetRoomsTotalPrice()
        {
           return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(RoomstotalPrice.Text));
        }

        public decimal GetFlightTotalPrice()
        {
            _webDriver.ScrollToElement(FlightsTotal);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(FlightsTotal.Text));
        }
        public decimal GetATOLProtectionPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(ATOLProtectionPrice.Text));
        }

        public decimal CalculateWasTotalPrice()
        {
            decimal totalPrice=0;
            if (IsLocalTaxVisible() &&!IsPackage())
                totalPrice += Convert.ToDecimal(GetLocalTaxes());
            if (FlightsTotal.Visible)
                totalPrice += GetFlightTotalPrice();
            if (ATOLProtectionPrice.Visible)
                totalPrice += GetATOLProtectionPrice();

            if (IsPackage())
                return GetPackagePrice() + totalPrice;
            else
                return GetRoomsTotalPrice() + totalPrice;
        }

        public decimal GetTotalPrice()
        {
            _webDriver.WaitForElementPresent(byTotalPrice, Constants.MediumWait);
            _webDriver.ScrollElementToCenter(TotalPrice);
            new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(driver => driver.FindElement(byTotalPrice.by).Text.Length != 0);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TotalPrice.Text));
        }

        public decimal GetDiscountPrice()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 30, "Selected hotel name is not visible");
            _webDriver.ScrollToBottom();
            _webDriver.WaitForElementVisible(byDiscountPrice, 50, "Discount price is not displayed");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(DiscountPrice.Text));
        }
        public bool IsAdminFeeVisible()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 30, "Selected hotel name is not visible");            
             if (!AdminFee.Visible)
             {
                System.Threading.Thread.Sleep(1000);
             }
            return AdminFee.Visible;
        }
        public decimal GetAdminFee()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 30, "Selected hotel name is not visible");
            _webDriver.ScrollToBottom();
            _webDriver.WaitForElementVisible(AdminFee, 30);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(AdminFee.Text));
        }

        public bool IsLocalTaxVisible()
        {
            if (LocalTaxAmount.Visible)
                return true;
            else
                return false;
        }

        public string GetLocalTaxes()
        {            
            _webDriver.WaitForElementVisible(byLocalTaxAmount, 60, "Local tax is not visible");
            return CommonFunctions.RemoveCurrencyInfo(LocalTaxAmount.Text);
        }

        public decimal GetPayNowCost()
        {
            //_webDriver.WaitForElementVisible(byPayNowAmount, 60, "Pay now amount is not visible");
            return PayNowAmount.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PayNowAmount.Text)) : 0;
        }

        public void ClickChangeRoom(int roomNo)
        {
            _webDriver.WaitForElementClickable(byChangeRoom, 60);
            _webDriver.ScrollToElement(ChangeRoom[roomNo - 1]);
            ChangeRoom[roomNo - 1].Click();
        }

        public void ClickRoomInformationIcon(int roomNo)
        {
            _webDriver.WaitUntilNotVisible(byPageLoader, 60);
            _webDriver.WaitForElementClickable(byRoomInfoLink, 30);
            _webDriver.ScrollElementToCenter(RoomInfoLink[roomNo - 1]);
            RoomInfoLink[roomNo - 1].Click();
        }

        public string GetRoomInformationModalContent()
        {
            _webDriver.WaitForElementVisible(RoomInfoModalContent, 30);
            _webDriver.WaitForTextPresent(RoomInfoModalContent, "a", TimeSpan.FromSeconds(1), 3);
            return RoomInfoModalContent.Text;
        }

        public void CloseRoomInformationModal()
        {
            CloseRoomInfoModel.Click();
        }

        public decimal GetPriceInHeader()
        {
            _webDriver.WaitForElementVisible(byPriceInHeader, 30, "Price header is not visible");
            _webDriver.WaitForTextPresent(byPriceInHeader, ".", TimeSpan.FromSeconds(5), 5);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PriceInHeader.Text));
        }

        public bool IsBookingSummaryHeaderDisplayed()
        {
            _webDriver.WaitForElementVisible(byBookingSummaryHeader, 30, "Booking summary header is not visible");
            return BookingSummaryHeader.Visible;
        }

        public void ClickBookingSummaryButton()
        {
            System.Threading.Thread.Sleep(3000);
            _webDriver.WaitForElementClickable(byBookingSummaryHeader, 60);
            BookingSummaryHeader.ClickButtonUsingJs();
        }

        public bool IsTotalPriceDisplayedOnSummaryHeadder()
        {
            return TotalPriceOnHeader.Visible;
        }

        public void NavigateBackTOBookingSummaryFromGuestInfoPage()
        {
            _webDriver.NavigateBack();
        }

        public string GetOutBoundAirportDetails()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, Constants.DefaultWait, "Flight section is not visible");
            _webDriver.WaitForElementVisible(ByFlightLegs, Constants.DefaultWait, "Flight  legs section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByAirports)[(int)FlightType.Outbound].Text;
        }

        public string GetOutBoundDepartureAirport()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByAirports)[(int)FlightType.Outbound].Text.Split("-")[0].Trim();
        }

        public string GetOutBoundArrivalAirport()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByAirports)[(int)FlightType.Outbound].Text.Split("-")[1].Trim();
        }

        public string GetOutBoundDepartureDate()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByFlightJourneyDetails)[(int)FlightType.Outbound].Text.Split("(")[0].Trim();
        }

        public string GetOutBoundDepartureTime()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByFlightJourneyDetails)[(int)FlightType.Outbound].Text.Split("(")[1].Split("-")[0].Trim();
        }

        public string GetOutBoundArrivalTime()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByFlightJourneyDetails)[(int)FlightType.Outbound].Text.Split("-")[1].Split(")")[0].Trim();
        }

        public string GetOutBoundFlightItinerary()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByFlightJourneyDetails)[(int)FlightType.Outbound].Text;
        }

        public string GetInBoundAirportDetails()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Return].FindElements(ByAirports)[(int)FlightType.Return].Text;
        }

        public string GetInBoundDepartureAirport()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Return].FindElements(ByAirports)[(int)FlightType.Return].Text.Split("-")[0].Trim();
        }

        public string GetInBoundArrivalAirport()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Return].FindElements(ByAirports)[(int)FlightType.Return].Text.Split("-")[1].Trim();
        }

        public string GetInBoundDepartureDate()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Outbound].FindElements(ByFlightJourneyDetails)[(int)FlightType.Return].Text.Split("(")[0].Trim();
        }

        public string GetInBoundDepartureTime()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Return].FindElements(ByFlightJourneyDetails)[(int)FlightType.Return].Text.Split("(")[1].Split("-")[0].Trim();
        }

        public string GetInBoundArrivalTime()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Return].FindElements(ByFlightJourneyDetails)[(int)FlightType.Return].Text.Split("-")[1].Split(")")[0].Trim();
        }

        public string GetInBoundFlightItinerary()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return FlightSection.FindElements(ByFlightLegs)[(int)FlightType.Return].FindElements(ByFlightJourneyDetails)[(int)FlightType.Return].Text;
        }

        public decimal GetBaggagePrice()
        {
            _webDriver.WaitForElementVisible(ByBaggageAmount, 60, "Baggage section is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BaggageAmount.Text));
        }

        public string GetTransferName()
        {
            _webDriver.WaitForElementVisible(ByTranfserSection, 60, "Transfer section is not visible");
            _webDriver.MoveToElement(TransferSection);
            return TransferName.Text;
        }

        public decimal GetTransferPrice()
        {
            _webDriver.WaitForElementVisible(ByTranfserSection, 60, "Transfer section is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TransferSection.FindElement(ByTransfersAmount).Text));
        }

        public string GetInsurancePolicyName()
        {
            _webDriver.WaitForElementVisible(ByInsuranceSection, 60, "Insurance section is not visible");
            return InsuranceName.Text;
        }

        public decimal GetInsurancePrice()
        {
            _webDriver.WaitForElementVisible(ByInsuranceSection, 60, "Insurance section is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(InsuranceSection.FindElement(ByInsuranceAmount).Text));
        }

        public bool IsInsuranceSectionDisplayed()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 30, "Selected hotel name is not visible");
            return InsuranceSection.Displayed;
        }

        public bool IsTransferSectionDisplayed()
        {
            _webDriver.WaitForElementVisible(bySelectedHotelName, 30, "Selected hotel name is not visible");
            return TransferSection.Displayed;
        }

        public decimal GetPackagePrice()
        {
            _webDriver.WaitForElementVisible(ByFlightSection, 60, "Flight section is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PackageAmount.Text));
        }

        public bool IsPackage()
        {
            if (!PackageAmount.Visible)
            {
                System.Threading.Thread.Sleep(2000);
            }
            return PackageAmount.Visible;
        }

        public string GetMandatoryTransferName(int transferOption = 1)
        {
            _webDriver.WaitForElementVisible(ByTranfserSection, 20, "Transfer section is not visible");
            _webDriver.ScrollElementToCenter(TransferSection);
            return TransferSection.FindElements(ByMandatoryTransferName)[transferOption - 1].Text;
        }

        public decimal GetMandatoryTransferPrice(int transferOption = 1)
        {
            _webDriver.WaitForElementVisible(ByTranfserSection, 20, "Transfer section is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TransferSection.FindElements(ByMandatoryTransferPrice)[transferOption - 1].Text));
        }

        public void CloseBookingSummaryModal()
        {
            _webDriver.WaitForElementClickable(byCloseBookingSummary, 10);
            CloseBookingSummary.Click();
        }

        public string GetFlightAllocationBaggageInfo()
        {
            _webDriver.WaitForElementVisible(ByFlightAllocationBaggageInfo, 60, "Baggage information is not displayed in booking summary");
            return FlightAllocationBaggageInfo.Text;
        }

        public string GetFlightAllocationBaggageCost()
        {
            return FlightAllocationBaggageCost.Text;
        }

        public decimal GetCovidCoverPlusPrice()
        {
            _webDriver.WaitForElementPresent(byCovidCoverPlus, Constants.MediumWait);
            _webDriver.ScrollElementToCenter(CovidCoverPlus);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(CovidCoverPlus.Text));
        }

        public bool IsOnBookingSummary()
        {
            return SubmitButton.Displayed;
        }

        public bool IsAdditionalPackageInclusionsDisplayed()
        {
            _webDriver.WaitForElementVisible(ByAdditionalPackageInclusionsHeading, "Additional Package Included messaage is missing");
            return AdditionalPackageInclusionsHeading.Visible;
        }

        public List<string> GetAdditionalPackageInclusionsLineItems()
        {
            List<string> AdditionalPackageInclusionsLineItems = new List<string>();
            foreach (AtWebElement element in AdditionalPackageInclusionsItems)
            {
                AdditionalPackageInclusionsLineItems.Add(element.Text);
            }

            return AdditionalPackageInclusionsLineItems;
        }


        public Dictionary<string,string> GetAdditionalPackageInclusionsLineItemsPopupTitleAndContent()
        {
            Dictionary<string,string> AdditionalPackageInclusionsLineItemsPopupText = new Dictionary<string,string>();
            foreach (AtWebElement inclusionItemIcon in AdditionalPackageInclusionsItemInfoIcons)
            {
                _webDriver.WaitForElementClickable(inclusionItemIcon);
                _webDriver.ScrollElementToCenter(inclusionItemIcon);
                inclusionItemIcon.Click();
                AdditionalPackageInclusionsLineItemsPopupText.Add(_modalPopup.GetModalHeading(), _modalPopup.GetModalContent());
                _modalPopup.ClosePopUp();
            }
            return AdditionalPackageInclusionsLineItemsPopupText;
        }

        public string GetFreeHotelCancellationText(int roomType=1)
        {
            _webDriver.ScrollElementToCenter(FreeHotelCancellationMessage[roomType-1]);
             return FreeHotelCancellationMessage[roomType-1].Text;         
        }
    }
}
