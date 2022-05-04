using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class RoomSelectionSummary: MobileBasePage, IRoomSelectionSummary
    {
        #region[Constructor]
        private readonly IAtWebDriver _webDriver;

        public RoomSelectionSummary(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy ByDialogHeader => GetBy(LocatorType.CssSelector, "#summary-dialog-dialog-title");
        private AtWebElement DialogHeader => _webDriver.FindElement(ByDialogHeader);
        private ReadOnlyCollection<AtWebElement> RoomType => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'dialog-content')]//*[contains(@class,'sc-o-heading--s') and not(contains(@class,'color'))]");
        private ReadOnlyCollection<AtWebElement> Occupants => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'dialog-content')]//span[@class='sc-o-body']");
        private ReadOnlyCollection<AtWebElement> ChangeRoom => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'dialog-content')]//a");
        private ReadOnlyCollection<AtWebElement> RoomPrice => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'dialog-content')]//span[contains(@class,'heading') and contains(@class,'color') and not(contains(@class,'accent'))]");
        private AtWebElement TotalPrice => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'dialog-content')]//span[contains(@class,'accent')]");
        private AtWebElement CloseRoomSelectionButton => _webDriver.FindElement(LocatorType.CssSelector, "div[aria-labelledby='summary-dialog-dialog-title'] button");
        #endregion

        #region[Methods]
        public string GetDialogHeader()
        {
            _webDriver.WaitForElementVisible(ByDialogHeader, Constants.ShortWait, "Dialog is not displayed");
            return DialogHeader.Text;
        }

        public string GetRoomType(int roomNo)
        {
            _webDriver.WaitForTextPresent(RoomType[roomNo - 1], " ", TimeSpan.FromSeconds(2), 5);
            return RoomType[roomNo - 1].Text;
        }

        public string GetOccupants(int roomNo)
        {
            return Occupants[roomNo - 1].Text;
        }

        public void ClickChangeRoom(int roomNo)
        {
            ChangeRoom[roomNo - 1].Click();
        }

        public decimal GetRoomPrice(int roomNo)
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(RoomPrice[roomNo - 1].Text));
        }

        public decimal GetTotalPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TotalPrice.Text));
        }

        public void CloseRoomSelectionModal()
        {
            CloseRoomSelectionButton.Click();
        }
        #endregion

    }
}
