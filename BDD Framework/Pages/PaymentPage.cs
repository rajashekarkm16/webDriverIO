using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class PaymentPage :MobileBasePage, IPaymentPage
    {
        private AtBy bySearchAddress => GetBy(LocatorType.Id, "dialogTrigger");
        private AtWebElement SearchAddress => _webDriver.FindElement(bySearchAddress);
        private AtWebElement ManualAddress => _webDriver.FindElement(LocatorType.XPath, "//a[text()='Enter address manually']");
        private AtBy byAddressKeyWord => GetBy(LocatorType.Id, "addressSearch");
        private AtWebElement AddressKeyWord => _webDriver.FindElement(byAddressKeyWord);
        private AtBy byAddressAutoCompleter => GetBy(LocatorType.CssSelector, "div[class*=primary-content]:nth-child(1)");
        private ReadOnlyCollection<AtWebElement> AddressAutoCompleter => _webDriver.FindElements(byAddressAutoCompleter);
        private AtWebElement CloseButton => _webDriver.FindElement(LocatorType.XPath, "//button[@aria-label='Close']");
        private AtWebElement Address => _webDriver.FindElement(LocatorType.Id, "address");
        private AtWebElement City => _webDriver.FindElement(LocatorType.Id, "city");
        private AtWebElement PostCode => _webDriver.FindElement(LocatorType.Id, "postCode");
        private AtWebElement Country => _webDriver.FindElement(LocatorType.Id, "countryCode");
        private AtBy byCardNumberFrame => GetBy(LocatorType.Id, "pciFrame_cc");
        private AtWebElement CardNumberFrame => _webDriver.FindElement(byCardNumberFrame);
        private AtBy byCardNumber => GetBy(LocatorType.CssSelector,"input[name=cardnumber]");
        private AtWebElement CardNumber => _webDriver.FindElement(byCardNumber);
        private AtWebElement ExpiryMonth => _webDriver.FindElement(LocatorType.CssSelector, "#expiryDate-month");
        private AtWebElement ExpiryYear => _webDriver.FindElement(LocatorType.CssSelector, "#expiryDate-year");
        private AtWebElement Expiry => _webDriver.FindElement(LocatorType.CssSelector, "#expiryDate");
        private AtWebElement CVVNumberFrame => _webDriver.FindElement(LocatorType.Id, "pciFrame_cvv3");
        private AtWebElement CVVNumber => _webDriver.FindElement(LocatorType.CssSelector, "#field");
        private AtBy byPaymentError => GetBy(LocatorType.XPath, "//div[@class='sc-c-notification sc-c-notification--error']");
        private AtWebElement PaymentError => _webDriver.FindElement(byPaymentError);
        private AtBy byErrorMessages => GetBy(LocatorType.CssSelector, "p.sc-u-color-error");
        private ReadOnlyCollection<AtWebElement> ErrorMessages => _webDriver.FindElements(byErrorMessages);



        private readonly IAtWebDriver _webDriver;

        public PaymentPage(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        public void SetCardNumber(string cardType)
        {
            _webDriver.WaitForElementVisible(byCardNumberFrame, Constants.DefaultWait, "Card number frame is not visible");
            _webDriver.ScrollElementToCenter(CardNumberFrame);
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt("pciFrame_cc"));
            _webDriver.WaitForElementVisible(byCardNumber, Constants.ShortWait, "Card Number is NOt Visible");
            //_webDriver.ScrollElementToCenter(CardNumber);
            Cards cards = new Cards();
            CardNumber.EnterText(cards.GetType().GetField(cardType).GetValue(this).ToString());
            Thread.Sleep(3000);
            _webDriver.SwitchToDefaultContent();            
        }

        public void ReenterPaymentDetailsIfError(string cardType)
        {
            foreach (var errorMessage in ErrorMessages)
            {
                if (errorMessage.Text.Contains("Enter a valid card number"))
                {
                    SetCardNumber(cardType);
                }
                else if (errorMessage.Text.Contains("Enter a valid expiry date"))
                {
                    SetExpiry();
                }
                else if (errorMessage.Text.Contains("Enter a valid security code"))
                {
                    SetSecurityNumber(cardType);
                }                 
            }                       
        }

        public void SetExpiry()
        {
            if (Expiry.Visible)
            {
                _webDriver.ScrollElementToCenter(Expiry);
                Expiry.EnterText("0223");
            }
            else
            {
                _webDriver.ScrollElementToCenter(ExpiryMonth);
                ExpiryMonth.EnterText("02");
                ExpiryYear.EnterText("23");
            }
        }

        public void SetSecurityNumber(string cardType)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            if (cardType.Equals("Amex"))
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt("pciFrame_cvv4"));
            else
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt("pciFrame_cvv3"));
            _webDriver.ScrollElementToCenter(CVVNumber);
            CVVNumber.EnterText(GetCVV(cardType));
            _webDriver.SwitchToDefaultContent();
            Expiry.Click();
            Thread.Sleep(3000);
        }

        public string GetCVV(string cardType = "VisaCredit")
        {
            return cardType == "Amex" ? Cards.AmexCvv : Cards.OtherCvv;
        }

        public void AutoPopulateAddress(string addressKeyword)
        {
            _webDriver.WaitForElementVisible(bySearchAddress, Constants.MediumWait, "Search Address is not visible");
            SearchAddress.Click();
            try
            {
                _webDriver.WaitForElementVisible(byAddressKeyWord, Constants.DefaultWait, "Address keyword is not visible");
                AddressKeyWord.EnterText(addressKeyword);
                _webDriver.WaitForElementVisible(byAddressAutoCompleter, 15, "Address AutoCompleter is not visible");
                AddressAutoCompleter[0].Click();
                _webDriver.WaitUntilNotVisible(byAddressAutoCompleter, Constants.DefaultWait);
                //_webDriver.WaitUntilNotVisible(byAddressKeyWord, 10);
            }
            catch 
            {
                Assert.Warn("Address auto completer is not tested!");
                CloseButton.Click();
                _webDriver.WaitUntilNotVisible(byAddressKeyWord, 10);
                ManualAddress.Click();
                Address.EnterText("Kingston Upon Thames");
                City.EnterText("London");
                PostCode.EnterText("KT2 6NH");
                Country.SelectDropdownOptionByValue("United Kingdom");
            }
        }

        public bool HasBookingFailed()
        {
            _webDriver.WaitForElementVisible(byPaymentError, 60, "Payment error is not visible");
            return true;
        }
    }
}
