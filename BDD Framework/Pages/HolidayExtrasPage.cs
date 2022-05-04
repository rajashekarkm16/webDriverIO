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

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class HolidayExtrasPage : MobileBasePage, IHolidayExtrasPage
    {
        #region[WebElements]
        private AtBy byContinueToBookButton => GetBy(LocatorType.XPath, "//span[text()='Continue To Book' or text()='Continue to book']/parent::button");
        private AtWebElement ContinueToBookButton => _webDriver.FindElement(byContinueToBookButton);
        private AtBy ByConfirmInsuranceAndContinueButton => GetBy(LocatorType.XPath, "//button/span[text()='Confirm and Continue to Book']");
        private AtWebElement ConfirmInsuranceAndContinueButton => _webDriver.FindElement(ByConfirmInsuranceAndContinueButton);
        private AtBy byPageLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        #endregion

        #region[constructor]
        private readonly IAtWebDriver _webDriver;
        private readonly ITravelInsurance _travelInsurance;
        private readonly IBookingSummary _bookingSummary;

        public HolidayExtrasPage(IAtWebDriver webDriver, ITravelInsurance travelInsurance,BookingSummary bookingSummary)
            : base(webDriver)
        {
            _webDriver = webDriver;
            _travelInsurance = travelInsurance;
            _bookingSummary = bookingSummary;
        }

        #endregion

        #region[Methods]
        public void ContinueToBook()
        {
            _webDriver.WaitForElementClickable(byContinueToBookButton, 30);
            if (_travelInsurance.IsTravelInsuranceAvailable() && !_travelInsurance.IsInsuranceAdded())
            {
                ContinueToBookButton.ClickButtonUsingJs();
                _webDriver.WaitForElementClickable(ByConfirmInsuranceAndContinueButton, 10);
                ConfirmInsuranceAndContinueButton.ClickButtonUsingJs();
            }
            else
                ContinueToBookButton.ClickButtonUsingJs();
        }

        public void ProceedIfExtrasPageIsVisible()
        {
            _webDriver.WaitUntilNotVisible(byPageLoader, Constants.MediumWait, "Page loader is still visible");
            if (ContinueToBookButton.Visible && !_bookingSummary.IsBookingSummaryPage())
                ContinueToBook();
        }

        public bool IsContinueToBookEnabled()
        {
            return ContinueToBookButton.Enabled;
        }

        public void ClickContinueToBookButton() 
        {
            _webDriver.WaitForElementClickable(byContinueToBookButton, Constants.DefaultWait);
            ContinueToBookButton.ClickButtonUsingJs();
        }

        public void ClickConfirmandContinueToBook()
        {
            _webDriver.WaitForElementClickable(ByConfirmInsuranceAndContinueButton, Constants.DefaultWait);
            ConfirmInsuranceAndContinueButton.ClickButtonUsingJs();
        }
        #endregion
    }
}
