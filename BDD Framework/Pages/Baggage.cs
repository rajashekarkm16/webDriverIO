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
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class Baggage : MobileBasePage, IBaggage
    {

        #region[WebElements]
        private AtBy ByPageLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy byAddBags => GetBy(LocatorType.XPath, "//h3[text()='Add a Bag']/ancestor::div//select");
        private AtWebElement AddBags => _webDriver.FindElement(byAddBags);
        private AtBy byBagAddedStatus => GetBy(LocatorType.XPath, "//div[@class='sc-c-card-header']//span[text()='Added']");
        private AtWebElement BagAddedStatus => _webDriver.FindElement(byBagAddedStatus);
        private AtBy byRemoveBag => GetBy(LocatorType.XPath, "//span[text()='remove']");
        private AtWebElement RemoveBag => _webDriver.FindElement(byRemoveBag);
        private AtBy byBaggageHeader => GetBy(LocatorType.XPath, "//h3[text()='Hold Baggage']");
        private AtWebElement BaggageHeader => _webDriver.FindElement(byBaggageHeader);
        private AtWebElement TotalBaggagePricePerBag => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'Baggage')]//ancestor::div[contains(@class,'content')]//div[contains(@class,'grey')]/strong");
        private AtWebElement BaggagePriceEachWay => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'Baggage')]//ancestor::div[contains(@class,'content')]//div[contains(@class,'color-accent')]");
        private AtWebElement BaggageWeight => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'Baggage')]//ancestor::div[contains(@class,'content')]//span[contains(@class,'bold')]");
        private AtBy ByCustomBaggageText => GetBy(LocatorType.CssSelector, "div.sc-c-notification div.sc-o-body");
        private ReadOnlyCollection<AtWebElement> CustomBaggageText => _webDriver.FindElements(ByCustomBaggageText);
        private AtBy byBaggageIncludeMessage => GetBy(LocatorType.CssSelector, "div.sc-c-notification--highlight");
        private AtWebElement AddBagSection => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='Add a Bag']/parent::div");
        private AtBy byBaggageToastMessage => GetBy(LocatorType.CssSelector, "div.sc-c-notification--toast div.sc-o-flex-grid-item--fill-space");
        private AtWebElement BaggageToastMessage => _webDriver.FindElement(byBaggageToastMessage);
        #endregion

        #region[Constructor]
        private readonly IAtWebDriver _webDriver;
        private int bagsToSelect = 0;
        private BaggageInformation baggageInformation;
        private bool isBaggageAdded = false;

        public Baggage(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[Methods]

        public int GetBaggageToSelect()
        {
            RandomizeBagsToSelect();
            return bagsToSelect;
        }

        public int RandomizeBagsToSelect()
        {
            if(bagsToSelect == 0)
            {
                _webDriver.WaitForElementVisible(byAddBags, 30, "Add bags button is not visible");
                bagsToSelect = HelperFunctions.RandomNumber(GetBaggageCount());
            }  
            return bagsToSelect;
        }

        public int GetBaggageCount()
        {
            _webDriver.WaitForElementVisible(byAddBags, 30, "Add bags button is not visible");
            SelectElement baggage = new SelectElement(AddBags);
            return baggage.Options.Count - 1;
        }

        public void SelectBaggage(int bagCount)
        {
            CaptureBaggageInformation();
            _webDriver.WaitForElementVisible(byAddBags, 30, "Add bags button is not visible");
            SelectElement baggage = new SelectElement(AddBags);
            baggage.SelectByValue(bagCount.ToString());
            _webDriver.WaitUntilNotVisible(ByPageLoader, 60);
            if(bagCount > 0)
                isBaggageAdded = true;
        }

        public decimal GetTotalPricePerBagPrice()
        {
            _webDriver.WaitForElementVisible(byAddBags, 30, "Add bags button is not visible");
            return TotalBaggagePricePerBag.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TotalBaggagePricePerBag.Text)) : 0;
        }

        public decimal GetEachWayBaggagePrice()
        {
            _webDriver.WaitForElementVisible(byAddBags, 30, "Add bags button is not visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BaggagePriceEachWay.Text));
        }

        public double GetBagWeight()
        {
            _webDriver.WaitForElementVisible(byAddBags, 30, "Add bags button is not visible");
            return Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(BaggageWeight.Text));
        }

        public void CaptureBaggageInformation()
        {
            baggageInformation = new BaggageInformation();
            baggageInformation.BaggageWeight = GetBagWeight();
            baggageInformation.PricePerBagEachWay = GetEachWayBaggagePrice();
            baggageInformation.TotalPricePerBag = GetTotalPricePerBagPrice();
        }

        public BaggageInformation GetBaggageInformation()
        {
            return baggageInformation;
        }

        public string GetBaggageDefaultText()
        {
            return "";
        }

        public bool IsAddBaggageOptionAvailable()
        {
            if (BaggageHeader.Visible)
                return true;
            else
                return false;
        }

        public bool IsBaggageAdded()
        {
            return isBaggageAdded;
        }

        public bool IsBaggegAddedStatusUpdated()
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, Constants.MediumWait, "Baggage not Updated");
            _webDriver.WaitForElementVisible(byBagAddedStatus, Constants.DefaultWait, "Baggage Added status is not updated");
            return BagAddedStatus.Visible && RemoveBag.Visible;
        }

        public bool IsCustomBaggageTextDisplayed()
        {
            _webDriver.WaitForElementVisible(byBaggageIncludeMessage, Constants.MediumWait, "Baggage Included Message is not displayed");
            return CustomBaggageText.Count > 0;
        }

        public string GetCustomeBaggageHeader()
        {
            _webDriver.WaitForElementVisible(byBaggageIncludeMessage, Constants.MediumWait, "Baggage Included Message is not displayed");
            return CustomBaggageText[0].Text;

        }
        public string GetCustomBaggageWeightText()
        {
            _webDriver.WaitForElementVisible(byBaggageIncludeMessage, Constants.MediumWait, "Baggage Included Message is not displayed");
            return CustomBaggageText[1].Text;
        }

        public string GetCustomHandLuggageText()
        {
            _webDriver.WaitForElementVisible(byBaggageIncludeMessage, Constants.MediumWait, "Baggage Included Message is not displayed");
            return CustomBaggageText[2].Text;
        }                

        public bool  VerifyBaggageIncludeMessage()
        {
            bool result = false;
            if (GetCustomeBaggageHeader().Equals("Included with your flight") && GetCustomHandLuggageText().Equals("Hand luggage for each passenger"))
                result = true;
            if (!IsAddBaggageOptionAvailable())
            {
                if (GetCustomBaggageWeightText().Equals("1 hold bag per person up to 23kg/bag"))
                    result = true;
                else
                    result = false;
            }                   
            return result;
        }

        public bool VerfiyAddBagSectionText()
        {
            return AddBagSection.Text.Equals(Constants.AddBagSectionText);
        }

        public string GetBaggageToastMessage()
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, Constants.MediumWait, "Baggage not added");
            _webDriver.WaitForElementVisible(byBaggageToastMessage,Constants.DefaultWait,"Baggage Toast Message is not Visible");
            return BaggageToastMessage.Text;
        }

        public decimal GetTotalPriceonBaggaeToast()
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, Constants.MediumWait, "Baggage not added");
            _webDriver.WaitForElementVisible(byBaggageToastMessage, Constants.DefaultWait, "Baggage Toast Message is not Visible");
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BaggageToastMessage.Text));
        }

        public void RemoveBaggage()
        {
            _webDriver.WaitUntilNotVisible(ByPageLoader, Constants.MediumWait, "Baggage not added");
            _webDriver.WaitForElementVisible(byRemoveBag, Constants.DefaultWait, "Baggage Remove link  is not Visible");
            RemoveBag.Click();
            _webDriver.WaitUntilNotVisible(byRemoveBag, Constants.DefaultWait, "Baggage is not removed");
        }
        #endregion


    }
}
