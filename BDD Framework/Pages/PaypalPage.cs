using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class PaypalPage: MobileBasePage, IPaypalPage
    {
        private readonly IAtWebDriver _webDriver;

        public PaypalPage(IAtWebDriver driver)
            : base(driver)
        {
            _webDriver = driver;
        }

        private AtBy byPayPalPage => GetBy(LocatorType.XPath, "//*[@id='login']");
        private AtBy byPayPalEmail => GetBy(LocatorType.XPath, "//*[@id='email']");
        private AtWebElement PayPalEmail => _webDriver.FindElement(byPayPalEmail);
        private AtBy byPayPalNext => GetBy(LocatorType.Id, "btnNext");
        private AtWebElement PayPalNext => _webDriver.FindElement(byPayPalNext);
        private AtBy byPayPalPassword => GetBy(LocatorType.XPath, "//*[@id='password']");
        private AtWebElement PayPalPassword => _webDriver.FindElement(byPayPalPassword);
        private AtWebElement PayPalLogin => _webDriver.FindElement(LocatorType.Id, "btnLogin");
        private AtBy byPayPalLoader => GetBy(LocatorType.XPath, "//p[@class='loader']");
        private AtBy byPayPalConfirmPay => GetBy(LocatorType.XPath, "//input[@id='confirmButtonTop'] | //button[@id='payment-submit-btn']");
        private AtWebElement PayPalConfirmPay => _webDriver.FindElement(byPayPalConfirmPay);
        private AtBy byPayPalContinue => GetBy(LocatorType.XPath, "//input[contains(@class,'continueButton')] | //button[contains(@class,'continueButton')]");
        private AtWebElement PayPalContinue => _webDriver.FindElement(byPayPalContinue);
        private AtBy byPayPalSpinner => GetBy(LocatorType.XPath, "//div[@id='spinner']");
        private AtWebElement PaypalCookies => _webDriver.FindElement(LocatorType.Id, "acceptAllButton");

        public void PerformPaypalPayment()
        {
            if (!HelperFunctions.IsLive())
            {
                _webDriver.WaitForElementVisible(byPayPalPage, 120, "PayPal Page is not visible");

                if (PayPalEmail.Visible)
                {
                    PayPalEmail.EnterText("paypaltest@travelrepublic.co.uk");
                    if (PayPalNext.Visible)
                        PayPalNext.Click();
                    _webDriver.WaitForElementVisible(byPayPalPassword, 60, "PayPal password field is not visible");
                    PayPalPassword.EnterText("Owens123!");
                    _webDriver.ScrollToElement(PayPalLogin);
                    PayPalLogin.Click();
                }
                else
                {
                    _webDriver.SwitchToFrame("injectedUl");
                    PayPalEmail.EnterText("paypaltest@travelrepublic.co.uk");
                    PayPalPassword.EnterText("Owens123!");
                    PayPalLogin.Click();
                    _webDriver.SwitchToDefaultContent();
                }

                _webDriver.WaitUntilNotVisible(byPayPalLoader, 30);
                if (PaypalCookies.Visible)
                    PaypalCookies.Click();
                _webDriver.WaitForAnyVisible(byPayPalConfirmPay, byPayPalContinue, 60, "Paypal continue or confirmation button is not visible");
                if (PayPalContinue.Visible)
                {
                    if (PaypalCookies.Visible)
                        PaypalCookies.Click();
                    _webDriver.WaitUntilNotVisible(byPayPalLoader, 30);
                    _webDriver.WaitForElementClickable(byPayPalContinue, 20);
                    _webDriver.WaitUntilNotVisible(byPayPalLoader, 30, "Paypal loader is still displayed");
                    _webDriver.WaitUntilNotVisible(byPayPalSpinner, 30, "Paypal loader is still displayed");
                    if (PaypalCookies.Visible)
                        PaypalCookies.Click();
                    _webDriver.WaitForElementClickable(byPayPalContinue, 20, "Element is still not clickable");
                    PayPalContinue.Click();
                }
                if (PaypalCookies.Visible)
                    PaypalCookies.Click();
                if (PayPalConfirmPay.Visible)
                {
                    _webDriver.ScrollElementToCenter(PayPalConfirmPay);
                    PayPalConfirmPay.Click();
                }
                System.Threading.Thread.Sleep(5000);
                if (PayPalConfirmPay.Visible)
                {
                    _webDriver.ScrollElementToCenter(PayPalConfirmPay);
                    PayPalConfirmPay.Click();
                }
            }
            else
                Console.WriteLine("Booking not made in Live!!");
        }
    }
}
