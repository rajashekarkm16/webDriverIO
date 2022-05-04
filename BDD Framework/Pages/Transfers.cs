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
using Dnata.Automation.BDDFramework.Exceptions;
using NUnit.Framework;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class Transfers : MobileBasePage, ITransfers
    {

        #region[WebElements]
        private AtBy ByPageLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byTransferResults => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']/parent::div/following-sibling::div/div[@data-room-no]//article[contains(@class,'card')]");
        private ReadOnlyCollection<AtWebElement> TransferResults => _webDriver.FindElements(byTransferResults);
        private AtBy byTransferName => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']/parent::div/following-sibling::div/div[@data-room-no]//article//h3[contains(@class,'heading') and not(contains(@class,'color-accent'))]");
        private ReadOnlyCollection<AtWebElement> TransferName => _webDriver.FindElements(byTransferName);
        private AtBy bySelectTransferButton => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']/parent::div/following-sibling::div/div[@data-room-no]//article//span[contains(text(),'Add')]/parent::button");
        private ReadOnlyCollection<AtWebElement> SelectTransferButton => _webDriver.FindElements(bySelectTransferButton);
        private AtBy byRemoveTransfer => GetBy(LocatorType.XPath, "//span[text()='remove']");
        private AtWebElement RemoveTransfer => _webDriver.FindElement(byRemoveTransfer);
        private AtBy ByTransferPerPersonPrice => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']/parent::div/following-sibling::div/div[@data-room-no]//article//*[contains(@class,'color-accent')]");
        private ReadOnlyCollection<AtWebElement> TransferPerPersonPrice => _webDriver.FindElements(ByTransferPerPersonPrice);
        private AtBy ByTransferTotalPrice => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']/parent::div/following-sibling::div/div[@data-room-no]//article//*[contains(@class,'color-grey')]/strong");
        private ReadOnlyCollection<AtWebElement> TransferTotalPrice => _webDriver.FindElements(ByTransferTotalPrice);
        private AtBy byTransferHeader => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']");
        private AtWebElement TransferHeader => _webDriver.FindElement(byTransferHeader);
        private AtBy ByTransferDuration => GetBy(LocatorType.XPath, "//h3[text()='Add a Transfer']/parent::div/following-sibling::div/div[@data-room-no]//article//*[contains(@class,'margin-top-m')]/*");
        private ReadOnlyCollection<AtWebElement> TransferDuration => _webDriver.FindElements(ByTransferDuration);
        private AtBy BySelectedMandatoryTransferName => GetBy(LocatorType.XPath, "//article[contains(@class,'selected')]//h3[@class='sc-o-heading']");
        private ReadOnlyCollection<AtWebElement> SelectedMandatoryTransferName => _webDriver.FindElements(BySelectedMandatoryTransferName);
        private ReadOnlyCollection<AtWebElement> SelectedMandatoryTransferPrice => _webDriver.FindElements(LocatorType.XPath, "//article[contains(@class,'selected')]//div[contains(@class,'color-accent')]");
        private ReadOnlyCollection<AtWebElement> SelectedComplimentaryTransferFlag => _webDriver.FindElements(LocatorType.XPath, "//article[contains(@class,'selected')]//div[@class='sc-c-sash__label']");
        private ReadOnlyCollection<AtWebElement> SelectedMandatoryTransferAddButton => _webDriver.FindElements(LocatorType.XPath, "//article[contains(@class,'selected')]//button");

        private AtBy ByMandatoryTransferOptions(int roomNo) => GetBy(LocatorType.CssSelector, "div[data-room-no=room-#] article[class*=card--offset]", roomNo.ToString());
        private ReadOnlyCollection<AtWebElement> MandatoryTransferOptions(int roomNo) => _webDriver.FindElements(ByMandatoryTransferOptions(roomNo));
        private AtWebElement MandatoryTransferName => _webDriver.FindElement(LocatorType.CssSelector, "h3[class=sc-o-heading]");
        private AtWebElement MandatoryTransferPrice => _webDriver.FindElement(LocatorType.CssSelector, "div[class*=color-accent]");
        private AtWebElement MandatoryTransferAddButton => _webDriver.FindElement(LocatorType.CssSelector, "button span[class*=button__label]");
        private AtBy ByOutBoundFlightNumber => GetBy(LocatorType.XPath, "//input[@id='outboundFlightNumber']");
        private AtWebElement OutBoundFlightNumber => _webDriver.FindElement(ByOutBoundFlightNumber);
        private AtWebElement OutBoundArrivalTimeHours => _webDriver.FindElement(LocatorType.XPath, "//select[@id='outboundFlightTimeHours']");
        private AtWebElement OutBoundArrivalTimeMinutes => _webDriver.FindElement(LocatorType.XPath, "//select[@id='outboundFlightTimeMinutes']");
        private AtWebElement InBoundFlightNumber => _webDriver.FindElement(LocatorType.XPath, "//input[@id='inboundFlightNumber']");
        private AtWebElement InBoundArrivalTimeHours => _webDriver.FindElement(LocatorType.XPath, "//select[@id='inboundFlightTimeHours']");
        private AtWebElement InBoundArrivalTimeMinutes => _webDriver.FindElement(LocatorType.XPath, "//select[@id='inboundFlightTimeMinutes']");
        private AtBy byTransferImportantInformation => GetBy(LocatorType.XPath, "//h5[text()='Important Information']");
        private AtWebElement TransferImportantInformation => _webDriver.FindElement(byTransferImportantInformation);
        private AtBy byToastMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'notification--toast')]");
        private AtWebElement ToastMessage => _webDriver.FindElement(byToastMessage);
        private AtBy byTransferHeaderText => GetBy(LocatorType.XPath, "//*[text()='Select your Transfer']/parent::div");
        private AtWebElement TransferHeaderText => _webDriver.FindElement(byTransferHeaderText);        
        private AtBy ByFlightsValidationError => GetBy(LocatorType.XPath, "//p[contains(@class,'error')]");
        private ReadOnlyCollection<AtWebElement> FlightsValidationError => _webDriver.FindElements(ByFlightsValidationError);
        #endregion

        #region[Constructor]
        private readonly IAtWebDriver _webDriver;
        private readonly GuestComponent _guestComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private int transferToSelect = 0;
        private TransferInformation transferInformation;
        private bool isTransferAdded = false;
        private List<TransferInformation> ltransferInformation;
        private bool isToastMessageDisplayed = false;

        public Transfers(GuestComponent guestComponent, ILandingPageGuestComponent landingPageGuestComponent, IAtWebDriver webDriver)
            : base(webDriver)
        {
            _guestComponent = guestComponent;
            _webDriver = webDriver;
            _landingPageGuestComponent = landingPageGuestComponent;
        }
        #endregion

        #region[Methods]

        public int GetTransferToSelect()
        {
            RandomizeTransferToSelect();
            return transferToSelect;
        }

        public int GetTransferResultsCount()
        {
            _webDriver.WaitForElementVisible(byTransferResults, 30, "TransferResults is not visible");
            return TransferResults.Count;
        }

        public void RandomizeTransferToSelect()
        {
            if(transferToSelect == 0)
            {
                try
                {
                    _webDriver.WaitForElementVisible(byTransferResults, 30, "TransferResults is not visible");
                    transferToSelect = HelperFunctions.RandomNumber(GetTransferResultsCount());
                }
                catch (AtElementNotPresentException)
                {
                    Assert.Fail("Transfers are not available to select!!");
                }
            }
        }

        public void SelectTransfer(int transferToSelect)
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, 60);            
            _webDriver.ScrollToElement(TransferResults[transferToSelect - 1]);
            _webDriver.WaitForElementVisible(SelectTransferButton[transferToSelect - 1], Constants.MediumWait);
            CaptureTransferInformation(transferToSelect);
            SelectTransferButton[transferToSelect - 1].Click();
            isTransferAdded = true;
            _webDriver.WaitUntilNotVisible(ByPageLoader, 60);
        }

        public string GetTransferName(int transferToSelect)
        {
            return TransferName[transferToSelect - 1].Text;
        }

        public string GetTransferDurationText(int transferToSelect)
        {
            return TransferDuration[transferToSelect - 1].Text;
        }
        public decimal GetPerPersonTransferPrice(int transferToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TransferPerPersonPrice[transferToSelect - 1].Text));
        }

        public decimal GetTotalTransferPrice(int transferToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TransferTotalPrice[transferToSelect - 1].Text));
        }

        public bool IsTransferSectionVisible()
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, 30);
            return TransferHeader.Visible;
        }

        public void CaptureTransferInformation(int transferToSelect)
        {
            transferInformation = new TransferInformation
            {
                TransferName = GetTransferName(transferToSelect),
                Duration = GetTransferDurationText(transferToSelect),
                PerPersonPrice = GetPerPersonTransferPrice(transferToSelect),
                TotalPrice = GetTotalTransferPrice(transferToSelect)
            };
        }

        public TransferInformation GetTransferInformation()
        {
            return transferInformation;
        }

        public bool IsTransferAdded()
        {
            return isTransferAdded;
        }

        public void CaptureDefaultMandatoryTransferDetails()
        {
            ltransferInformation = new List<TransferInformation>();
            _webDriver.WaitForElementVisible(BySelectedMandatoryTransferName, 30, "SelectedMandatoryTransferName is not visible");
            int noOfRooms;
            if (HelperFunctions.IsV3HomepageEnabled())
                noOfRooms = _landingPageGuestComponent.GetRoomOccupantDetails().Count;
            else
                noOfRooms = _guestComponent.GetRoomOccupantDetails().Count;
            for (int i = 0; i < noOfRooms; i++)
            {
                transferInformation = new TransferInformation();
                transferInformation.TransferName = SelectedMandatoryTransferName[i].Text;
                transferInformation.PerPersonPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(SelectedMandatoryTransferPrice[i].Text));
                ltransferInformation.Add(transferInformation);
            }
        }

        public List<TransferInformation> GetMandatoryTransferDetails()
        {
            return ltransferInformation;
        }

        public List<TransferInformation> CaptureAllMandatoryTransferDetails(int roomNo = 1)
        {
            List<TransferInformation> transferInformation = new List<TransferInformation>();
            TransferInformation transfer = new TransferInformation();
            transfer.TransferName = SelectedMandatoryTransferName[roomNo - 1].Text;
            transfer.PerPersonPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(SelectedMandatoryTransferPrice[roomNo - 1].Text));
            transferInformation.Add(transfer);
            foreach (var option in MandatoryTransferOptions(roomNo))
            {
                transfer = new TransferInformation();
                transfer.TransferName = option.FindElement(MandatoryTransferName).Text;
                transfer.PerPersonPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(option.FindElement(MandatoryTransferPrice).Text));
                transferInformation.Add(transfer);
            }
            return transferInformation;
        }     

        public void PopulateTransferFlightDetails()
        {
            _webDriver.WaitForElementVisible(ByOutBoundFlightNumber, 60, "OutBoundFlightNumber is not visible");
            _webDriver.ScrollElementToCenter(OutBoundFlightNumber);
            OutBoundFlightNumber.EnterText("BA1234");
            OutBoundArrivalTimeHours.SelectDropdownOptionByValue("11");
            OutBoundArrivalTimeMinutes.SelectDropdownOptionByValue("30");
            _webDriver.ScrollElementToCenter(InBoundFlightNumber);
            InBoundFlightNumber.EnterText("EK1234");
            InBoundArrivalTimeHours.SelectDropdownOptionByValue("12");
            InBoundArrivalTimeMinutes.SelectDropdownOptionByValue("30");
        }


        public void PopulateInvalidFlightDetails()
        {
            _webDriver.WaitForElementVisible(ByOutBoundFlightNumber, 60, "OutBoundFlightNumber is not visible");
            _webDriver.ScrollElementToCenter(OutBoundFlightNumber);
            OutBoundFlightNumber.EnterText("BA###");
            OutBoundArrivalTimeHours.SelectDropdownOptionByValue("11");
            OutBoundArrivalTimeMinutes.SelectDropdownOptionByValue("30");
            OutBoundArrivalTimeHours.SelectDropdownOptionByIndex(0);
            OutBoundArrivalTimeMinutes.SelectDropdownOptionByIndex(0);
            _webDriver.ScrollElementToCenter(InBoundFlightNumber);
            InBoundFlightNumber.EnterText("EK###");
            InBoundArrivalTimeHours.SelectDropdownOptionByValue("12");
            InBoundArrivalTimeMinutes.SelectDropdownOptionByValue("30");
            InBoundArrivalTimeHours.SelectDropdownOptionByIndex(0);
            InBoundArrivalTimeMinutes.SelectDropdownOptionByIndex(0);
        }

        public List<string> GetFlightsValidationErrorMessages()
        {
            List<string> errorMessages = new List<string>();
            foreach (var error in FlightsValidationError)
            {
                errorMessages.Add(error.Text);
            }
            
            return errorMessages;
        }

        public string GetFlightsValidationErrorMessage(int fieldNo = 1)
        {
            return FlightsValidationError[fieldNo - 1].Text;
        }


        public bool IsComplimentaryTransferFlagDisplayed()
        {
            return SelectedComplimentaryTransferFlag[0].Visible;
        }

        public void CaptureAndSelectMandatoryTransferDetails()
        {
            ltransferInformation = new List<TransferInformation>();
            int occupantCount;
            if (HelperFunctions.IsV3HomepageEnabled())
                occupantCount = _landingPageGuestComponent.GetRoomOccupantDetails().Count;
            else
                occupantCount = _guestComponent.GetRoomOccupantDetails().Count;
            _webDriver.WaitForElementVisible(ByMandatoryTransferOptions(1), 30, "MandatoryTransferOptions is not visible");
            for (int roomNo = 1; roomNo <= occupantCount; roomNo++)
            {
                transferInformation = new TransferInformation();
                isToastMessageDisplayed = false;
                int transferOptionToSelect = HelperFunctions.RandomNumber(MandatoryTransferOptions(roomNo).Count);
                _webDriver.ScrollElementToCenter(MandatoryTransferOptions(roomNo)[transferOptionToSelect - 1]);
                transferInformation.TransferName = MandatoryTransferOptions(roomNo)[transferOptionToSelect - 1].FindElement(MandatoryTransferName).Text;
                transferInformation.PerPersonPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(MandatoryTransferOptions(roomNo)[transferOptionToSelect - 1].FindElement(MandatoryTransferPrice).Text));
                ltransferInformation.Add(transferInformation);
                MandatoryTransferOptions(roomNo)[transferOptionToSelect - 1].FindElement(MandatoryTransferAddButton).Click();
                _webDriver.WaitUntilNotVisible(ByPageLoader, 20);
                isToastMessageDisplayed = ToastMessage.Displayed || ToastMessage.Visible;
                if (!HelperFunctions.IsDesktop())
                    Assert.IsTrue(isToastMessageDisplayed, "Toast message not displayed on mobile after selecting transfers");
                else
                    Assert.IsFalse(isToastMessageDisplayed, "Toast message is displayed on Desktop after selecting transfers");
            }
        }

        public bool IsToastMessageDisplayed()
        {
            return isToastMessageDisplayed;
        }
        public bool IsImportantInformationDisplayed()
        {
            return TransferImportantInformation.Visible;
        }
        
        public string ReturnHeaderText()
        {
            return TransferHeaderText.Text;
        }

        public bool IsTransferSelected(int transferToSelect)
        {
            _webDriver.WaitForElementVisible(byTransferResults, 20, "Transfer not displayed");
            return TransferResults[transferToSelect - 1].GetAttribute("class").Contains("selected");
        }

        public void RemoveSelectedTransfer(int transferToSelect)
        {
            if (IsTransferSelected(transferToSelect))
            {
                _webDriver.WaitForElementVisible(byRemoveTransfer, 20, "Remove trasnfer is not visible");
                _webDriver.ScrollElementToCenter(RemoveTransfer);
                RemoveTransfer.ClickButtonUsingJs();
                _webDriver.WaitUntilNotVisible(ByPageLoader, Constants.MediumWait);
                isToastMessageDisplayed = ToastMessage.Displayed || ToastMessage.Visible;
                if (!HelperFunctions.IsDesktop())
                    Assert.IsTrue(isToastMessageDisplayed, "Toast message not displayed on mobile after removing transfers");
                else
                    Assert.IsFalse(isToastMessageDisplayed, "Toast message is displayed on Desktop after removing transfers");
                isTransferAdded = false;
            }                
        }

        #endregion

    }
}
