using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class LandingPageGuestComponent : GuestComponent, ILandingPageGuestComponent
    {
        private readonly IAtWebDriver _webDriver;
        private List<RoomOccupantDetails> lRoomOccupantDetails;

        private AtWebElement Header => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-popper']//span");
        private AtBy byCloseIcon => GetBy(LocatorType.XPath, "//div[@class='sc-c-popper']//button[@aria-label='Close popup']");
        private AtWebElement CloseIcon => _webDriver.FindElement(byCloseIcon);
        private AtBy byChildAgeErrorMessage => GetBy(LocatorType.XPath, "//span[text()='Please enter ages of all children']");
        private ReadOnlyCollection<AtWebElement> ChildAgeErrorMessage => _webDriver.FindElements(byChildAgeErrorMessage);
        private AtBy byConfirmGuests => GetBy(LocatorType.XPath, "//span[text()='Done'] | //span[text()='Confirm number of guests']");
        private AtWebElement ConfirmGuests => _webDriver.FindElement(byConfirmGuests);
        private AtBy ByNeedMoreRoomsText => GetBy(LocatorType.XPath, "//div[@class='sc-c-popper']//div[contains(@class,'sc-u-background')]");
        private AtWebElement NeedMoreRoomsText => _webDriver.FindElement(ByNeedMoreRoomsText);

        public LandingPageGuestComponent(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public override string GetGuestPageTitle()
        {
            return Header.Text;
        }
     
        public override bool ValidateChildErrorMessage(int NoOfRooms)
        {
            _webDriver.WaitForElementVisible(byChildAgeErrorMessage, 5, "Error message is not visible");
            return NoOfRooms.Equals(ChildAgeErrorMessage.Count);
        }
     
        public override string GetNeedMoreRoomsText()
        {
            _webDriver.WaitForElementVisible(ByNeedMoreRoomsText, Constants.ShortWait, "Need more rooms text is not displayed");
            return NeedMoreRoomsText.Text;
        }

        public override void CloseIconButton()
        {
            _webDriver.WaitForElementVisible(byCloseIcon, 30, "close button  is not visible on occupancy model");
             CloseIcon.Click();
        }

        public override void ConfirmNumberOfGuests()
        {
            //_webDriver.WaitForElementClickable(byConfirmGuests, 30);
           // _webDriver.ScrollToElement(ConfirmGuests);
            ConfirmGuests.ClickButtonUsingJs();
        }
    }
}





