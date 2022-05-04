using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;


namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class AirportComponent : MobileBasePage, IAirportComponent
    {
        private readonly IAtWebDriver _webDriver;

        #region[Constructor]
        public AirportComponent(IAtWebDriver webDriver)
           : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy BySelectedAirports => GetBy(LocatorType.XPath, "//ul[@id='airport-autocompleter-list']//li//div[contains(@class,'selected')]");
        private ReadOnlyCollection<AtWebElement> SelectedAirports => _webDriver.FindElements(BySelectedAirports);
        private AtBy byFlyingFromHeaderText => GetBy(LocatorType.XPath, "//h3[contains(@id,'departure')]");
        private AtWebElement FlyingFromHeaderText => _webDriver.FindElement(byFlyingFromHeaderText);
        private AtBy BySelectedAirportsName => GetBy(LocatorType.XPath, "//ul[@id='airport-autocompleter-list']//li//div[contains(@class,'selected')]//div[contains(@class,'bold')]");
        private ReadOnlyCollection<AtWebElement> SelectedAirportsName => _webDriver.FindElements(BySelectedAirportsName);
        private AtBy ByNearBySuggestedAirports => GetBy(LocatorType.XPath, "//hr[contains(@class,'--dashed')]/following-sibling::li//ul");
        private ReadOnlyCollection<AtWebElement> NearBySuggestedAirports => _webDriver.FindElements(ByNearBySuggestedAirports);
        private AtBy byConfirmDepartureAirportButton => GetBy(LocatorType.XPath, "//span[text()='Confirm airports']/ancestor::button");
        private AtWebElement ConfirmDepartureAirportButton => _webDriver.FindElement(byConfirmDepartureAirportButton);
        private AtWebElement ErrorMessageAirportPickerModal => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'field--error')]//input");
        private AtBy byFlyingFromText => GetBy(LocatorType.XPath, "//input[@id='airport-autocompleter-field']");
        private AtWebElement FlyingFromText => _webDriver.FindElement(byFlyingFromText);
        private AtBy byCloseAirportPicker => GetBy(LocatorType.XPath, "//h3[contains(@id,'departure')]/ancestor::header//button");
        private AtWebElement CloseAirportPicker => _webDriver.FindElement(byCloseAirportPicker);
        #endregion

        #region[Methods]
        public string GetFlyingFromHeaderText()
        {
            _webDriver.WaitForElementVisible(byFlyingFromHeaderText, 10, "Flying from header is not visible");
            return FlyingFromHeaderText.Text;
        }

        public bool IsCloseButtonDisplayedOnAirpotSelectionModal()
        {
            return CloseAirportPicker.Visible;
        }

        public bool IsConfirmDepartureAirportButtonDisplayed()
        {
            return ConfirmDepartureAirportButton.Visible;
        }

        public bool IsNearBySuggestedAirpotsDisplayed()
        {
            return NearBySuggestedAirports.Count > 1;
        }

        public void RemoveAllSelectedAirports()
        {
            while (SelectedAirports.Count > 0)
                SelectedAirports.First().Click();
        }

        public void ClickConfirmDepartureAirportButton()
        {
            ConfirmDepartureAirportButton.Click();
        }

        public string GetErrorMessageOnAirportPickerModal()
        {
            return ErrorMessageAirportPickerModal.GetAttribute("placeholder");
        }
        public void CloseAirportPickerModal()
        {
            _webDriver.WaitForElementVisible(byCloseAirportPicker, 15, "Airport picker is not visible");
            CloseAirportPicker.Click();
        }

        public string GetFlyingFromSummaryText()
        {
            _webDriver.WaitForElementVisible(byFlyingFromText, 10, "Flying from text is not visible");
            return FlyingFromText.GetAttribute("value");
        }
        #endregion
    }
}
