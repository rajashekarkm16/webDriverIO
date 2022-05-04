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
using NUnit.Framework;
using System.Threading;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class TravelInsurance : MobileBasePage, ITravelInsurance
    {

        #region[WebElements]
        private AtBy ByPageLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byTravelInsuranceHeader => GetBy(LocatorType.XPath, "//h3[text()='Add Travel Insurance']");
        private AtWebElement TravelInsuranceHeader => _webDriver.FindElement(byTravelInsuranceHeader);
        private AtBy byInsuranceResults => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article[contains(@class,'card')]");
        private ReadOnlyCollection<AtWebElement> InsuranceResults => _webDriver.FindElements(byInsuranceResults);        
        private AtBy ByInsuranceName => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article//*[contains(@class,'heading') and not(contains(@class,'color'))]");
        private ReadOnlyCollection<AtWebElement> InsuranceName => _webDriver.FindElements(ByInsuranceName);
        private AtBy ByInsurancePerPersonPrice => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article//*[contains(@class,'color-accent')]");
        private ReadOnlyCollection<AtWebElement> InsurancePerPersonPrice => _webDriver.FindElements(ByInsurancePerPersonPrice);
        private AtBy ByInsuranceTotalPrice => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article//*[contains(@class,'color-grey')]/strong");
        private ReadOnlyCollection<AtWebElement> InsuranceTotalPrice => _webDriver.FindElements(ByInsuranceTotalPrice);
        private AtBy ByChildrenInsurancePrices => GetBy(LocatorType.XPath, "//label[contains(text(),'Child')]/ancestor::div[contains(@class,'y-m')]//*[contains(@class,'color-accent')]");
        private ReadOnlyCollection<AtWebElement> ChildrenInsurancePrices => _webDriver.FindElements(ByChildrenInsurancePrices);
        private AtBy bySelectInsuranceButton => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article[contains(@class,'card')]//button");
            //*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article//span[text()='Select']/parent::button"
        private ReadOnlyCollection<AtWebElement> SelectInsuranceButton => _webDriver.FindElements(bySelectInsuranceButton);
        private AtWebElement InsuranceNotNeeded => _webDriver.FindElement(LocatorType.Id, "own-insurance");
        private AtBy ByAdultDates(int roomNo) => GetBy(LocatorType.XPath, "//input[contains(@id,'Adult-#') and contains(@id,'dob')]", roomNo.ToString());
        private ReadOnlyCollection<AtWebElement> AdultDates(int roomNo) => _webDriver.FindElements(ByAdultDates(roomNo));
        private AtBy ByChildDates(int roomNo) => GetBy(LocatorType.XPath, "//input[contains(@id,'Child-#') and contains(@id,'dob')]", roomNo.ToString());
        private ReadOnlyCollection<AtWebElement> ChildDates(int roomNo) => _webDriver.FindElements(ByChildDates(roomNo));
        private AtBy ByInsuranceCalculationLoader => GetBy(LocatorType.XPath, "//span[@class='sc-c-throbber']");
        private AtWebElement AddInsuranceToBasket => _webDriver.FindElement(LocatorType.XPath, "//button[@id='insuranceConfirmButton']");
        private AtWebElement DefaultInsuranceSelectButton  => _webDriver.FindElement(LocatorType.XPath, "//span[text()='I have my own travel insurance']/parent::div/following-sibling::div//button");
        private AtWebElement MoreInsuranceInfoLink => _webDriver.FindElement(LocatorType.XPath, "//a[text()='More insurance info']");
        private AtBy byInsuranceModal => GetBy(LocatorType.XPath, "//div[contains(@class,'dialog')]//*[contains(@class,'heading')]");
        private AtWebElement InsuranceModal => _webDriver.FindElement(byInsuranceModal);
        private AtWebElement CloseInsuranceModal => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'dialog')]//button[@aria-label='Close']");
        private AtWebElement ViewPolicyDocumentationLink => _webDriver.FindElement(LocatorType.XPath, "//a[text()='View Policy Documentation']");
        private AtWebElement ViewInsuranceProductInformationDocumentLink => _webDriver.FindElement(LocatorType.XPath, "//a[text()='View Insurance Product Information Document']");
        private AtBy byViewPolicyInformation => GetBy(LocatorType.XPath, "//a[text()='View policy information']");
        private ReadOnlyCollection<AtWebElement> ViewPolicyInformation => _webDriver.FindElements(byViewPolicyInformation);
        private AtBy bySuccessToastMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'notification--success')]");
        private AtWebElement SuccessToastMessage => _webDriver.FindElement(bySuccessToastMessage);
        private AtBy byIndividualOccupantInsurancePrice => GetBy(LocatorType.XPath, "//div[contains(@class,'s-3')]/div[contains(@class,'wrap')]");
        private ReadOnlyCollection<AtWebElement> IndividualOccupantInsurancePrice => _webDriver.FindElements(byIndividualOccupantInsurancePrice);
        private AtBy ByUpdateBasketButton => GetBy(LocatorType.XPath, "//span[text()='Update basket']/parent::button");
        private AtWebElement UpdateBasketButton => _webDriver.FindElement(ByUpdateBasketButton);
        private AtBy BySelectedInsuranceCard => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article[contains(@class,'selected')]");
        private AtWebElement SelectedInsuranceCard => _webDriver.FindElement(BySelectedInsuranceCard);        
        private AtWebElement InsuranceNameOnSelectedCard => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article[contains(@class,'selected')]//*[contains(@class,'heading') and not(contains(@class,'color'))]");        
        private ReadOnlyCollection<AtWebElement> InsurancePerPersonPriceOnSelectedCard => _webDriver.FindElements(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article[contains(@class,'selected')]//span[(contains(@class,'body--l')) and contains(@class,'color-accent')]");
        private AtBy ByInsuranceTotalPriceOnSelectedCard => GetBy(LocatorType.XPath, "//*[contains(text(),'Insurance')]/parent::div/following-sibling::div//article[contains(@class,'selected')]//span[(contains(@class,'body--xl')) and contains(@class,'color-accent')]");
        private AtWebElement InsuranceTotalPriceOnSelectedCard => _webDriver.FindElement(ByInsuranceTotalPriceOnSelectedCard);
        private AtBy byAdultErrorMessage => GetBy(LocatorType.XPath, "//label[contains(@for,'Adult')]/ancestor::div[contains(@class,'padding-y-m')]//span[contains(@class,'error')]");
        private ReadOnlyCollection<AtWebElement> AdultErrorMessages => _webDriver.FindElements(byAdultErrorMessage);
        private AtWebElement AdultErrorMessage => _webDriver.FindElement(byAdultErrorMessage);
        private AtBy byChildErrorMessage => GetBy(LocatorType.XPath, "//label[contains(@for,'Child')]/ancestor::div[contains(@class,'padding-y-m')]//span[contains(@class,'error')]");
        private ReadOnlyCollection<AtWebElement> ChildErrorMessages => _webDriver.FindElements(byChildErrorMessage);
        private AtBy byAddInsuranceFromPopUp => GetBy(LocatorType.XPath, "//a[text()='Add Insurance']");
        private AtWebElement AddInsuranceFromPopUp => _webDriver.FindElement(byAddInsuranceFromPopUp);
        private AtBy byGreenTickIcon => GetBy(LocatorType.XPath, "//div[contains(@class,'hold-space')]/*[contains(@class,'success')]");
        private ReadOnlyCollection<AtWebElement> GreenTickIcon => _webDriver.FindElements(byGreenTickIcon);
        #endregion

        #region[Constructor]
        private readonly IAtWebDriver _webDriver;
        private int insuranceToSelect = 0;
        private TravelInsuranceInformation insuranceInformation;
        private bool isInsuranceAdded = false;        
        public TravelInsurance(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[Methods]

        public int GetInsuranceToSelect()
        {
            RandomizeInsuranceToSelect();
            return insuranceToSelect;
        }

        public void RandomizeInsuranceToSelect()
        {
            if(insuranceToSelect == 0)
            {
                _webDriver.WaitForElementVisible(byInsuranceResults, 30, "Insurance is not visible");
                insuranceToSelect = HelperFunctions.RandomNumber(GetTravelInsuranceCount());
            }            
        }

        public int GetTravelInsuranceCount()
        {
            _webDriver.WaitForElementVisible(byInsuranceResults, 30, "Insurance is not visible");
            return InsuranceResults.Count();
        }

        public bool IsTravelInsuranceAvailable()
        {
            if (TravelInsuranceHeader.Visible)
                return true;
            else
                return false;
        }

        public string GetTravelInsuranceName(int insuranceToSelect)
        {
            return InsuranceName[insuranceToSelect - 1].Text;
        }

        public decimal GetInsurancePerPersonPrice(int insuranceToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(InsurancePerPersonPrice[insuranceToSelect - 1].Text));
        }

        public decimal GetTotalInusrancePrice(int insuranceToSelect)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(InsuranceTotalPrice[insuranceToSelect - 1].Text));
        }
        
        public bool IsInsuranceNotRequiredboxSelected()
        {
            return InsuranceNotNeeded.Selected;
        }
        
        public void ClickInsuranceCard(int insuranceToSelect)
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, 60);
            _webDriver.WaitForElementClickable(bySelectInsuranceButton, 20);
            _webDriver.ScrollToElement(InsuranceResults[insuranceToSelect - 1]);
            SelectInsuranceButton[insuranceToSelect - 1].Click();
        }

        public void AddTravelInsurance(int insuranceToSelect, List<RoomOccupantDetails> roomOccupantDetails)
        {
            bool isToastMessageDisplayed = false;
            _webDriver.WaitUntilNotVisible(ByPageLoader, 60);
            _webDriver.WaitForElementClickable(bySelectInsuranceButton, 20);
            _webDriver.ScrollToElement(InsuranceResults[insuranceToSelect - 1]);
            CaptureTravelInsuranceInformation(insuranceToSelect);
            SelectInsuranceButton[insuranceToSelect - 1].Click();
            PopulateDOBOnInsuranceCard(roomOccupantDetails, Constants.AdultAge, Constants.ChildAge, Constants.InfantAge);
            _webDriver.ScrollElementToCenter(AddInsuranceToBasket);
            AddInsuranceToBasket.Click();
            isInsuranceAdded = true;
            _webDriver.WaitUntilNotVisible(ByPageLoader, 60);
            isToastMessageDisplayed = SuccessToastMessage.Displayed;
            if (HelperFunctions.IsDesktop())
                Assert.IsFalse(isToastMessageDisplayed, "Toast message not displayed on Desktop after selecting insurance");
            else if(!isToastMessageDisplayed)
                Assert.Warn("Toast message is not displayed on mobile after selecting insurance");
        }

        public void PopulateDOBOnInsuranceCard(List<RoomOccupantDetails> roomOccupantDetails, int AdultAge, int ChildAge, int InfantAge)
        {
            for (int i = 0; i < roomOccupantDetails.Count; i++)
            {
                int adultCount = roomOccupantDetails[i].NoOfAdults;
                int childCount = roomOccupantDetails[i].NoOfChildren;
                int infantCount = roomOccupantDetails[i].NoOfInfants;
                int adultCounter = 1;
                int childCounter = 1;
                while (adultCounter <= adultCount)
                {
                    _webDriver.WaitForElementClickable(ByAdultDates(i), 30);
                    _webDriver.ScrollElementToCenter(AdultDates(i)[adultCounter - 1]);
                    AdultDates(i)[adultCounter - 1].Clear();
                    AdultDates(i)[adultCounter - 1].SendKeys(HelperFunctions.GetDateOfBirth(AdultAge).ToString("ddMMyyyy"));
                    _webDriver.WaitUntilNotVisible(ByInsuranceCalculationLoader, 20);
                    adultCounter++;
                }

                while (childCounter <= childCount)
                {
                    _webDriver.WaitForElementClickable(ByChildDates(i), 30);
                    _webDriver.ScrollElementToCenter(ChildDates(i)[childCounter - 1]);                    
                    ChildDates(i)[childCounter - 1].Clear();
                    ChildDates(i)[childCounter - 1].SendKeys(HelperFunctions.GetDateOfBirth(ChildAge).ToString("ddMMyyyy"));
                    _webDriver.WaitUntilNotVisible(ByInsuranceCalculationLoader, 20);
                    childCounter++;
                }

                while (childCounter <= childCount + infantCount)
                {
                    _webDriver.WaitForElementClickable(ByChildDates(i), 30);
                    _webDriver.ScrollElementToCenter(ChildDates(i)[childCounter - 1]);
                    ChildDates(i)[childCounter - 1].Clear();
                    ChildDates(i)[childCounter - 1].SendKeys(HelperFunctions.GetDateOfBirth(InfantAge).ToString("ddMMyyyy"));
                    _webDriver.WaitUntilNotVisible(ByInsuranceCalculationLoader, 20);
                    childCounter++;
                }
            }
        }

        public bool IsInfantInsurncePriceZero(List<RoomOccupantDetails> roomOccupantDetails)
        {
            bool isPriceZero = true;
            for (int i = 0; i < roomOccupantDetails.Count; i++)
            {
                int childCount = roomOccupantDetails[i].NoOfChildren;
                int infantCount = roomOccupantDetails[i].NoOfInfants;
                int childCounter = 1;
                while (childCounter <= childCount)
                {
                    childCounter++;
                }

                while (childCounter <= childCount + infantCount)
                {
                    _webDriver.ScrollElementToCenter(ChildDates(i)[childCounter - 1]);
                    isPriceZero = ChildrenInsurancePrices[childCounter-1].Text.Contains("0.00");
                     childCounter++;
                }

            }
            return isPriceZero;
        }

        public void ValidateRetainedDOBOnInsuranceCard(List<RoomOccupantDetails> roomOccupantDetails)
        {

            for (int i = 0; i < roomOccupantDetails.Count; i++)
            {
                int adultCount = roomOccupantDetails[i].NoOfAdults;
                int childCount = roomOccupantDetails[i].NoOfChildren;
                int infantCount = roomOccupantDetails[i].NoOfInfants;
                int adultCounter = 1;
                int childCounter = 1;
                while (adultCounter <= adultCount)
                {
                    _webDriver.WaitForElementClickable(ByAdultDates(i), 30);
                    _webDriver.ScrollElementToCenter(AdultDates(i)[adultCounter - 1]);
                    Assert.AreEqual(AdultDates(i)[adultCounter - 1].Value, HelperFunctions.GetDateOfBirth(Constants.AdultAge).ToString("dd/MM/yyyy"));
                    adultCounter++;
                }

                while (childCounter <= childCount)
                {
                    _webDriver.WaitForElementClickable(ByChildDates(i), 30);
                    _webDriver.ScrollElementToCenter(ChildDates(i)[childCounter - 1]);
                    Assert.AreEqual(ChildDates(i)[childCounter - 1].Value, HelperFunctions.GetDateOfBirth(Constants.ChildAge).ToString("dd/MM/yyyy"));                    
                    childCounter++;
                }

                while (childCounter <= childCount + infantCount)
                {
                    _webDriver.WaitForElementClickable(ByChildDates(i), 30);
                    _webDriver.ScrollElementToCenter(ChildDates(i)[childCounter - 1]);
                    Assert.AreEqual(ChildDates(i)[childCounter - 1].Value, HelperFunctions.GetDateOfBirth(Constants.InfantAge).ToString("dd/MM/yyyy"));
                    childCounter++;
                }
            }
            _webDriver.WaitUntilNotVisible(ByInsuranceCalculationLoader, 60);
        }

        public void CaptureTravelInsuranceInformation(int insuranceToSelect)
        {
            if (InsuranceResults[insuranceToSelect - 1].GetAttribute("class").Contains("selected"))
            {
                insuranceInformation = new TravelInsuranceInformation()
                {
                    PolicyName = InsuranceNameOnSelectedCard.Text,
                    TotalPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(InsuranceTotalPriceOnSelectedCard.Text))
                };
            }
            else
            {
                insuranceInformation = new TravelInsuranceInformation()
                {
                    PolicyName = GetTravelInsuranceName(insuranceToSelect),
                    PerPersonPrice = GetInsurancePerPersonPrice(insuranceToSelect),
                    TotalPrice = GetTotalInusrancePrice(insuranceToSelect)
                };
                
            }
        }

        public TravelInsuranceInformation GetTravelInsuranceInformation()
        {
            return insuranceInformation;
        }

        public TravelInsuranceInformation CaptureAndGetTravelInsuranceInformation(int insuranceToSelect)
        {
            CaptureTravelInsuranceInformation(insuranceToSelect);
            return GetTravelInsuranceInformation();
        }

        public bool IsInsuranceAdded()
        {
            return isInsuranceAdded;
        }

        public bool IsDefaultInsuranceSelected()
        {
            return !DefaultInsuranceSelectButton.Enabled;
        }

        public void ClickMoreInsuranceInfoLink()
        {
            MoreInsuranceInfoLink.ClickButtonUsingJs();
        }

        public void ClickViewPolicyDocumentationLink()
        {
            ViewPolicyDocumentationLink.Click();
        }

        public string GetPDFLinkUrl()
        {
            string title = _webDriver.GetActiveTabId();
            _webDriver.SwitchToWindow(_webDriver.WindowHandles.Count - 1);
            string pdfUrl = _webDriver.Url;
            _webDriver.CloseAdditionalWindows(title);
            return pdfUrl;
        }

        public void ClickViewInsuranceProductInformationDocumentLink()
        {
            ViewInsuranceProductInformationDocumentLink.Click();
        }

        public bool IsInsuranceModalDisplayed()
        {
            Thread.Sleep(1000);
            //_webDriver.WaitForElementVisible(byInsuranceModal, 10, "Insurance modal is not visible");
            return InsuranceModal.Displayed;
        }
        public void ClickViewPolicyInformationLink()
        {
            ViewPolicyInformation[0].ClickButtonUsingJs();
        }

        public void CloseInsurancePopUpModal()
        {
            CloseInsuranceModal.Click();
        }

        public bool IsAddInsuranceToBasketButtonEnabled()
        {
            return AddInsuranceToBasket.Enabled;
        }

        public bool IsFromQualifierAbovePriceIsDisplayed()
        {
            foreach (var price in IndividualOccupantInsurancePrice)
            {
                if (!price.Text.Contains("from"))
                     return false;
            }
            
            return IndividualOccupantInsurancePrice.Count.Equals(0) ? false : true;
        }

        public void ClickUpdateBasketBuuton()
        {
            _webDriver.WaitForElementVisible(ByUpdateBasketButton, 15, "Update Basket Button is not visible");
            _webDriver.ScrollElementToCenter(UpdateBasketButton);
            UpdateBasketButton.Click();
            _webDriver.WaitUntilNotVisible(ByInsuranceCalculationLoader, 60);
        }

        public string GetAdultErrorMessage()
        {
            return AdultErrorMessages[0].Text;
        }

        public List<string> GetAllAdultErrorMessages()
        {
            //IEnumerable<string> errorMessages;
            //errorMessages = AdultErrorMessages.Select(data => data.Text);
            //return  errorMessages.ToList();

            List<string> errorMessage = new List<string>();
            foreach (var element in AdultErrorMessages)
            {
                errorMessage.Add(element.Text);
            }
            return errorMessage;
        }

        public List<string> GetAllChildErrorMessages()
        {
            List<string> errorMessage = new List<string>();
            foreach (var element in ChildErrorMessages)
            {
                errorMessage.Add(element.Text);
            }
            return errorMessage;
        }
        
        public void SelectIHaveMyOwnTravelInsurance()
        {
            if (!IsDefaultInsuranceSelected())
            {
                _webDriver.ScrollToElement(DefaultInsuranceSelectButton);
                DefaultInsuranceSelectButton.ClickButtonUsingJs();
            }
                
        }

        public void ClickAddInsuranceFromPopUp()
        {
            AddInsuranceFromPopUp.Click();
            _webDriver.WaitUntilNotVisible(byAddInsuranceFromPopUp, 10);
        }

        public int ReturnGreenTickCount()
        {
            return GreenTickIcon.Count;
        }
        #endregion
    }
}
