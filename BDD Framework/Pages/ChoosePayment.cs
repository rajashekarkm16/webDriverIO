using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class ChoosePayment : MobileBasePage, IChoosePayment
    {
        private ReadOnlyCollection<AtWebElement> ProgressBar => _webDriver.FindElements(LocatorType.XPath, "//*[@class='sc-c-stepper']/*[contains(@class,'stepper-item--xs') or contains(@class,'stepper-item--s')]");
        private AtBy byPayDepositRadio => GetBy(LocatorType.XPath, "//span[text()='Pay deposit']/parent::div/parent::div/div[@class='sc-c-checkbox sc-c-checkbox--m']");
        private AtWebElement PayDepositRadio => _webDriver.FindElement(byPayDepositRadio);
        private AtBy byPayMonthlyRadio => GetBy(LocatorType.XPath, "//span[text()='Pay monthly']");
        private AtWebElement PayMonthlyRadio => _webDriver.FindElement(byPayMonthlyRadio);
        private AtWebElement DepositAmount => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Pay deposit']/parent::div/following-sibling::div/span[contains(@class,'color-accent')]");
        private AtWebElement DepositMessage => _webDriver.FindElement(LocatorType.XPath, "((//ul/li/div)[2]//div[contains(@class,'sc-o-box')])[2]");
        private AtWebElement BalanceDescription => _webDriver.FindElement(LocatorType.XPath, "(//ul/li/div)[2]//div[contains(@class,'sc-o-box')][2]");
        private AtWebElement BalanceDate => _webDriver.FindElement(LocatorType.XPath, "((//ul/li/div)[2]//div[contains(@class,'sc-o-flex')])[2]");
        private AtWebElement BalanceAmount => _webDriver.FindElement(LocatorType.XPath, "(//ul/li/div)[2]//span[contains(@class,'sc-o-text--color-grey')]");
        private AtWebElement DepositAdminFees => _webDriver.FindElement(LocatorType.XPath, "(//ul/li/div)[2]//div[contains(@class,'sc-o-box')][3]");
        private AtWebElement PayDepositLocalTaxMessage => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Pay deposit']//ancestor::li//span[contains(@class,'sc-u-color-body')]/parent::div");
        private AtWebElement PayDepositLocalTaxAmount => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Pay deposit']//ancestor::li//span[contains(@class,'sc-u-color-body')]/parent::div/following-sibling::div");
        private AtWebElement PayFullLocalTaxMessage => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Pay in full']//ancestor::li//span[contains(@class,'sc-u-color-body')]/parent::div");
        private AtWebElement PayFullLocalTaxAmount => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Pay in full']//ancestor::li//span[contains(@class,'sc-u-color-body')]/parent::div/following-sibling::div");
        private AtBy byPayFullAmountRadio => GetBy(LocatorType.XPath, "//span[text()='Pay in full']");
        private AtWebElement PayFullAmountRadio => _webDriver.FindElement(byPayFullAmountRadio);
        private AtWebElement FullAmountFees => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Pay in full']/parent::div/following-sibling::div/span[contains(@class,'color-accent')]");
        private AtBy byCardRadio => GetBy(LocatorType.XPath, "//div[text()='Credit / Debit card']");
        private AtWebElement CardRadio => _webDriver.FindElement(byCardRadio);
        private AtBy byPayPalRadio => GetBy(LocatorType.XPath, "//div[text()='PayPal']");
        private AtWebElement PayPalRadio => _webDriver.FindElement(byPayPalRadio);
        private AtWebElement UserAgreementText => _webDriver.FindElement(LocatorType.CssSelector, "p:nth-child(1)");
        private ReadOnlyCollection<AtWebElement> BookingConditionsLinks => _webDriver.FindElements(LocatorType.CssSelector, "p:nth-child(1) a");
        private AtWebElement BookingConditionsText => _webDriver.FindElement(LocatorType.CssSelector, "div[class*=content] h2:nth-child(1)");
        private ReadOnlyCollection<AtWebElement> PaymentSchedule => _webDriver.FindElements(LocatorType.XPath, "//span[text()='Pay monthly']/ancestor::div[@class='sc-c-option']/following-sibling::div//div[contains(@class,'items')]");
        private AtWebElement DepositPayableAmount => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Pay deposit']/parent::*/following-sibling::*/*[contains(@class,'color-accent')]");
        private AtWebElement MonthlyPayableAmount => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Pay monthly']/parent::*/following-sibling::*/*[contains(@class,'color-accent')]");
        private AtWebElement FullPayableAmount => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Pay in full']/parent::*/following-sibling::*/*[contains(@class,'color-accent')]");
        private AtBy ByCovidNoticeCheckbox => GetBy(LocatorType.XPath, "//*[@id='covidNotice']/parent::div[contains(@class,'checkbox')]");
        private AtWebElement CovidNoticeCheckbox => _webDriver.FindElement(ByCovidNoticeCheckbox);
        private ReadOnlyCollection<AtWebElement> BalanceInstallments(string paymentType) => _webDriver.FindElements(LocatorType.XPath, "//span[text()='#']//ancestor::div[@class='sc-c-option']/following-sibling::*//div[contains(@class,'baseline')]/div[not(contains(@class,'hold-space'))]", paymentType);
        private AtWebElement AdminFees(string paymentType) => _webDriver.FindElement(LocatorType.XPath, "//span[text()='#']//ancestor::div[@class='sc-c-option']/following-sibling::*//span[contains(.,'admin fee')]", paymentType);

        private readonly IAtWebDriver _webDriver;
        private readonly IBookingSummary _bookingSummary;
        private bool IsDepositPayment = false;
        private bool IsMonthlyPayment = false;
        private int BalanceInstallmentsCount { get; set; }
        private decimal AdminFeesAmount { get; set; }
        public ChoosePayment(IAtWebDriver webDriver, IBookingSummary bookingSummary)
            :base(webDriver)
        {
            _webDriver = webDriver;
            _bookingSummary = bookingSummary;
        }

        private void SetDepositPayable()
        {
            _bookingSummary.SetTotalPayableAmount(Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(DepositPayableAmount.Text)));
        }

        private void SetMonthlyPayable()
        {
            _bookingSummary.SetTotalPayableAmount(Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(MonthlyPayableAmount.Text)));
        }

        public void ChoosePaymentPlan(bool isDeposit = false, bool isPayMonthly = false)
        {
            _webDriver.WaitForDomReady();
            Thread.Sleep(3000);
            if (isDeposit)
            {
                _webDriver.WaitForElementClickable(byPayDepositRadio, 60);
                _webDriver.ScrollElementToCenter(PayDepositRadio);
                Thread.Sleep(1000);
                PayDepositRadio.Click();
                SetDepositPayable();
                SetBalanceInstallmentsCount(GetDepositBalanceInstallments());
                SetAdminFees(GetDepositAdminFees());
                IsDepositPayment = true;
            }
            else if (isPayMonthly)
            {
                try
                {
                    _webDriver.WaitForElementClickable(byPayMonthlyRadio, 60);
                    _webDriver.ScrollElementToCenter(PayMonthlyRadio);
                    Thread.Sleep(1000);
                    PayMonthlyRadio.Click();
                    SetMonthlyPayable();
                    SetBalanceInstallmentsCount(GetMonthlyBalanceInstallments());
                    SetAdminFees(GetMonthlyAdminFees());
                    IsMonthlyPayment = true;
                }
                catch (Exception e)
                {
                    Assert.Fail("Recurring payment option is not tested or not available!\n"+e.Message);
                }                    
            }                
            else if (PayFullAmountRadio.Visible)
            {
                _webDriver.ScrollElementToCenter(PayFullAmountRadio);
                Thread.Sleep(1000);
                PayFullAmountRadio.Click();
                _bookingSummary.SetTotalPayableAmount(Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(FullPayableAmount.Text)));
            }  
        }

        public string GetLocalTaxMessageFromPayDepositSection()
        {
            _webDriver.WaitForElementClickable(byPayDepositRadio, 30);
            _webDriver.ScrollElementToCenter(PayDepositRadio);
            PayDepositRadio.Click();
            _webDriver.WaitForTextPresent(PayDepositLocalTaxMessage, "Local", TimeSpan.FromSeconds(1), 3);
            return PayDepositLocalTaxMessage.Text;
        }

        public string GetLocalTaxMessageFromPayFullSection()
        {
            _webDriver.WaitForElementClickable(byPayFullAmountRadio, 30);
            _webDriver.ScrollElementToCenter(PayDepositRadio);
            PayFullAmountRadio.Click();
            _webDriver.WaitForTextPresent(PayFullLocalTaxMessage, "Local", TimeSpan.FromSeconds(1), 3);
            return PayFullLocalTaxMessage.Text;
        }

        public decimal GetLocalTaxAmountFromPayDepositSection()
        {
            _webDriver.WaitForElementClickable(byPayDepositRadio, 30);
            PayDepositRadio.Click();
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PayDepositLocalTaxAmount.Text));
        }

        public decimal GetLocalTaxAmountFromPayFullSection()
        {
            _webDriver.WaitForElementClickable(byPayFullAmountRadio, 30);
            PayFullAmountRadio.ClickButtonUsingJs();
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PayFullLocalTaxAmount.Text));
        }

        public int GetProgressBarCount()
        {
            return ProgressBar.Count;
        }

        public void ChoosePaypal(bool isPaypal = false)
        {
            if (isPaypal)
            {
                try
                {
                    _webDriver.WaitForElementVisible(byCardRadio, 20, "Card radio button is not visible");
                    _webDriver.WaitForElementPresent(byPayPalRadio, 20);
                    _webDriver.ScrollToElement(CardRadio);
                    PayPalRadio.Click();
                }
                catch (Exception)
                {
                    Assert.Fail("Paypal option is not available");
                }
            }            
        }

        public void CheckCovidNoticeCheckbox()
        {
            _webDriver.WaitForElementVisible(ByCovidNoticeCheckbox, 20, "Covid Notice Checkbox is not visible");
            _webDriver.ScrollElementToCenter(CovidNoticeCheckbox);
            CovidNoticeCheckbox.ClickButtonUsingJs();
        }

        public bool GetIsDepositPayment()
        {
            return IsDepositPayment;
        }

        public bool GetIsMonthlyPayment()
        {
            return IsMonthlyPayment;
        }

        private int GetDepositBalanceInstallments()
        {
            int count = 0;
            foreach(var element in BalanceInstallments("Pay deposit"))
            {
                if(!(element.Text.Equals("You pay today") || element.Text.Equals("Admin fees") || element.Text.Equals("Holiday Total") || element.Text.Equals("Hotel Total")))
                    count++;
            }
            count += count > 1 ? 1 : 0;
            return count;
        }
        private int GetMonthlyBalanceInstallments()
        {
            int count = 0;
            foreach (var element in BalanceInstallments("Pay monthly"))
            {
                if (!(element.Text.Equals("Admin fees") || element.Text.Equals("Holiday Total") || element.Text.Equals("Hotel Total")))
                    count++;
            }
            return count;
        }

        public int GetBalanceInstallmentsCount()
        {
            return BalanceInstallmentsCount;
        }

        public void SetBalanceInstallmentsCount(int count)
        {
            BalanceInstallmentsCount = count;
        }

        public decimal GetAdminFees()
        {
            return AdminFeesAmount;
        }

        public void SetAdminFees(decimal amount)
        {
            AdminFeesAmount = amount;
        }

        private decimal GetDepositAdminFees()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(AdminFees("Pay deposit").Text));
        }

        private decimal GetMonthlyAdminFees()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(AdminFees("Pay monthly").Text));
        }
    }
}
