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
    public class GuestComponent : MobileBasePage, IGuestComponent
    {
        private readonly IAtWebDriver _webDriver;
        private List<RoomOccupantDetails> lRoomOccupantDetails;

        private AtWebElement Header => _webDriver.FindElement(LocatorType.CssSelector, "header[class*=elevated] div[class*=space] h3[id*=active]");
        private AtBy byCloseIcon => GetBy(LocatorType.XPath, "//div[@class='sc-c-modal' and not(contains(@aria-hidden,'true'))]//button[@aria-label='Close']");
        private AtWebElement CloseIcon => _webDriver.FindElement(byCloseIcon);
        private AtWebElement GuestsInGoing => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class, 'integrated-field__control--presentation')]");
        private AtWebElement GuestsInSearch => _webDriver.FindElement(LocatorType.XPath, "(//div[contains(text(), 'Guest')]//following::button)[1]");
        private AtBy byRoomOne => GetBy(LocatorType.Id, "room-toggle-1");
        private AtWebElement RoomOne => _webDriver.FindElement(byRoomOne);
        private AtWebElement RoomTwo => _webDriver.FindElement(LocatorType.Id, "room-toggle-2");
        private AtWebElement RoomThree => _webDriver.FindElement(LocatorType.Id, "room-toggle-3");
        private AtBy byRoomNumber => GetBy(LocatorType.XPath, "//input[contains(@id,'room-toggle')]//ancestor::div[@class='sc-o-flex-grid-item']//div[@class='sc-o-flex-grid-item']");
        private ReadOnlyCollection<AtWebElement> RoomNumber => _webDriver.FindElements(byRoomNumber);
        private ReadOnlyCollection<AtWebElement> DecreaseAdultCount => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@aria-controls,'adults') and @aria-label='Decrease']");
        private ReadOnlyCollection<AtWebElement> AdultCountValue => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@id,'adults')]");
        private ReadOnlyCollection<AtWebElement> IncreaseAdultCount => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@aria-controls,'adults') and @aria-label='Increase']");
        private ReadOnlyCollection<AtWebElement> DecreaseChildCount => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@aria-controls,'children') and @aria-label='Decrease']");
        private ReadOnlyCollection<AtWebElement> ChildCountValue => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@id,'children')]");
        private ReadOnlyCollection<AtWebElement> IncreaseChildCount => _webDriver.FindElements(LocatorType.XPath, "//button[contains(@aria-controls,'children')  and @aria-label='Increase']");
        private ReadOnlyCollection<AtWebElement> ChildAgeField => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'2x')]/input[@type='text']");
        private ReadOnlyCollection<AtWebElement> ChildAgeValue => _webDriver.FindElements(LocatorType.CssSelector, "label[for*=age-toggle]");
        private AtBy byChildAgeErrorMessage => GetBy(LocatorType.XPath, "//span[text()='Please enter ages of all children.']");
        private ReadOnlyCollection<AtWebElement> ChildAgeErrorMessage => _webDriver.FindElements(byChildAgeErrorMessage);
        private AtBy byConfirmGuests => GetBy(LocatorType.XPath, "//span[text()='Confirm number of guests']");
        private AtWebElement ConfirmGuests => _webDriver.FindElement(byConfirmGuests);
        private AtBy ByNeedMoreRoomsText => GetBy(LocatorType.XPath, "//form/div[contains(@class,'dialog-content__main')]//div[contains(@class,'background')]");
        private AtWebElement NeedMoreRoomsText => _webDriver.FindElement(ByNeedMoreRoomsText);
        

        public IEnumerable<object> childAgefield { get; private set; }

        public GuestComponent(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public virtual string GetGuestPageTitle()
        {
            return Header.Text;
        }

        public List<RoomOccupantDetails> GetRoomOccupantDetails()
        {
            return lRoomOccupantDetails;
        }

        public List<RoomOccupantDetails> SetRoomsData(string roomsData = "2,0,0")
        {
            lRoomOccupantDetails = new List<RoomOccupantDetails>();

            roomsData.Split(':').Select(room => room).ToList().ForEach(data =>
            {
                RoomOccupantDetails roomDetails = new RoomOccupantDetails();
                roomDetails.NoOfAdults = int.Parse(data.Split(',')[0]);
                roomDetails.NoOfChildren = int.Parse(data.Split(',')[1]);
                roomDetails.NoOfInfants = int.Parse(data.Split(',')[2]);
                lRoomOccupantDetails.Add(roomDetails);
            });
            return lRoomOccupantDetails;
        }

        public void PopulateGuests(List<RoomOccupantDetails> roomOccupantDetails)
        {
            int roomCount = 1;
            _webDriver.WaitForElementClickable(byRoomNumber, 30);
            RoomNumber[roomOccupantDetails.Count - 1].WrappedElement.Click();

            foreach (RoomOccupantDetails RoomOccupants in roomOccupantDetails)
            {
                //Removed scroll after sticky search implementation
                while (Convert.ToInt32(AdultCountValue[roomCount - 1].GetAttribute("value")) != RoomOccupants.NoOfAdults)
                {
                   // _webDriver.ScrollToElement(AdultCountValue[roomCount - 1]);
                    if (Convert.ToInt32(AdultCountValue[roomCount - 1].GetAttribute("value")) < RoomOccupants.NoOfAdults)
                        IncreaseAdultCount[roomCount - 1].Click();
                    else
                        DecreaseAdultCount[roomCount - 1].Click();
                }

                while (Convert.ToInt32(ChildCountValue[roomCount - 1].GetAttribute("value")) < RoomOccupants.NoOfChildren)
                {
                    //_webDriver.ScrollToElement(IncreaseChildCount[roomCount - 1]);
                    IncreaseChildCount[roomCount - 1].Click();
                    ChildAgeField.Last().Click();
                    ChildAgeValue[Constants.ChildAge].Click();
                }

                while (Convert.ToInt32(ChildCountValue[roomCount - 1].GetAttribute("value")) < (RoomOccupants.NoOfChildren + RoomOccupants.NoOfInfants))
                {
                    //_webDriver.ScrollToElement(IncreaseChildCount[roomCount - 1]);
                    IncreaseChildCount[roomCount - 1].Click();
                    ChildAgeField.Last().Click();
                    ChildAgeValue[Constants.InfantAge].Click();
                }
                roomCount++;
            }
        }

        public void PopulateGuestsWithoutChildAge(List<RoomOccupantDetails> roomOccupantDetails)
        {
            int roomCount = 1;
            _webDriver.WaitForElementClickable(byRoomNumber, 30);
            RoomNumber[roomOccupantDetails.Count - 1].WrappedElement.Click();
            foreach (RoomOccupantDetails RoomOccupants in roomOccupantDetails)
            {
                while (Convert.ToInt32(AdultCountValue[roomCount - 1].GetAttribute("value")) != RoomOccupants.NoOfAdults)
                {
                    _webDriver.ScrollToElement(AdultCountValue[roomCount - 1]);
                    if (Convert.ToInt32(AdultCountValue[roomCount - 1].GetAttribute("value")) < RoomOccupants.NoOfAdults)
                        IncreaseAdultCount[roomCount - 1].Click();
                    else
                        DecreaseAdultCount[roomCount - 1].Click();
                }

                while (Convert.ToInt32(ChildCountValue[roomCount - 1].GetAttribute("value")) < RoomOccupants.NoOfChildren)
                {
                    _webDriver.ScrollToElement(IncreaseChildCount[roomCount - 1]);
                    IncreaseChildCount[roomCount - 1].Click();
                }

                while (Convert.ToInt32(ChildCountValue[roomCount - 1].GetAttribute("value")) < (RoomOccupants.NoOfChildren + RoomOccupants.NoOfInfants))
                {
                    _webDriver.ScrollToElement(IncreaseChildCount[roomCount - 1]);
                    IncreaseChildCount[roomCount - 1].Click();
                }
                roomCount++;
            }
        }

        public void ClearAddedGuests()
        {
            RoomNumber[0].Click();
            while (DecreaseChildCount[0].Enabled)
                DecreaseChildCount[0].Click();
        }

        public bool CheckAdultsIncrementButton(List<RoomOccupantDetails> roomOccupantDetails, int maxAdults)
        {
            bool isIncrementButtonDisabled = true;
            _webDriver.WaitForElementVisible(byRoomOne, 30, "Room One button is not visible");
            int roomCount = 1;
            foreach (RoomOccupantDetails RoomOccupants in roomOccupantDetails)
            {
                _webDriver.ScrollToElement(IncreaseAdultCount[roomCount - 1]);
                int adultValue = Convert.ToInt32(AdultCountValue[roomCount - 1].GetAttribute("value"));
                for (int adultCounter = adultValue; adultCounter < maxAdults; adultCounter++)
                {
                    IncreaseAdultCount[roomCount - 1].Click();
                }
                isIncrementButtonDisabled = !(IncreaseAdultCount[roomCount - 1].Enabled);
                roomCount++;
            }
            return isIncrementButtonDisabled;
        }
        public bool CheckAdultsDecrementButton(List<RoomOccupantDetails> roomOccupantDetails, int minAdults)
        {
            bool isDecrementButtonDisabled = true;
            _webDriver.WaitForElementVisible(byRoomOne, 30, "Room One button is not visible");
            int roomCount = 1;
            foreach (RoomOccupantDetails RoomOccupants in roomOccupantDetails)
            {
                _webDriver.ScrollToElement(DecreaseAdultCount[roomCount - 1]);
                int adultValue = Convert.ToInt32(AdultCountValue[roomCount - 1].GetAttribute("value"));
                for (int adultCounter = adultValue; adultCounter > minAdults; adultCounter--)
                {
                    DecreaseAdultCount[roomCount - 1].Click();
                }
                isDecrementButtonDisabled = !(DecreaseAdultCount[roomCount - 1].Enabled);
                roomCount++;
            }
            return isDecrementButtonDisabled;
        }
        public bool CheckChildrenIncrementButton(List<RoomOccupantDetails> roomOccupantDetails, int maxChildren)
        {
            bool isIncrementButtonDisabled = true;
            _webDriver.WaitForElementVisible(byRoomOne, 30, "Room One button is not visible");
            int roomCount = 1;
            foreach (RoomOccupantDetails RoomOccupants in roomOccupantDetails)
            {
                _webDriver.ScrollToElement(IncreaseChildCount[roomCount - 1]);
                int childValue = Convert.ToInt32(ChildCountValue[roomCount - 1].GetAttribute("value"));
                for (int childCounter = childValue; childCounter < maxChildren; childCounter++)
                {
                    IncreaseChildCount[roomCount - 1].Click();
                }
                isIncrementButtonDisabled = !(IncreaseChildCount[roomCount - 1].Enabled);
                roomCount++;
            }
            return isIncrementButtonDisabled;
        }
        public bool CheckChildrenDecrementButton(List<RoomOccupantDetails> roomOccupantDetails, int minChildren)
        {
            bool isDecrementButtonDisabled = true;
            _webDriver.WaitForElementVisible(byRoomOne, 30, "Room One button is not visible");
            int roomCount = 1;
            foreach (RoomOccupantDetails RoomOccupants in roomOccupantDetails)
            {
                _webDriver.ScrollToElement(DecreaseChildCount[roomCount - 1]);
                int childValue = Convert.ToInt32(ChildCountValue[roomCount - 1].GetAttribute("value"));
                for (int childCounter = childValue; childCounter > minChildren; childCounter--)
                {
                    DecreaseChildCount[roomCount - 1].Click();
                }
                isDecrementButtonDisabled = !(DecreaseChildCount[roomCount - 1].Enabled);
                roomCount++;
            }
            return isDecrementButtonDisabled;
        }
        public void SelectRooms(int roomCount)
        {
            _webDriver.ScrollToElement(RoomNumber[roomCount - 1]);
            RoomNumber[roomCount - 1].Click();
        }

        public bool CheckRoomDetails(int roomCount)
        {
            return IncreaseChildCount.Count().Equals(roomCount);
        }

        public virtual bool ValidateChildErrorMessage(int NoOfRooms)
        {
            _webDriver.WaitForElementVisible(byChildAgeErrorMessage, 5, "Error message is not visible");
            return NoOfRooms.Equals(ChildAgeErrorMessage.Count);
        }

        public virtual void ConfirmNumberOfGuests()
        {
            _webDriver.WaitForElementClickable(byConfirmGuests, 30);
            _webDriver.ScrollToElement(ConfirmGuests);
            ConfirmGuests.Click();
            //_webDriver.WaitUntilNotVisible(byConfirmGuests, 3);
        }

        public void VerifyGuests(List<RoomOccupantDetails> roomOccupantDetails)
        {
            int totalAdults = 0;
            int totalChildren = 0;
            int totalRooms = roomOccupantDetails.Count;

            roomOccupantDetails.ForEach(room =>
            {
                totalAdults += room.NoOfAdults;
                totalChildren = totalChildren + room.NoOfChildren + room.NoOfInfants;
            });

            if (totalChildren == 0)
                Assert.IsTrue(GetGuestsText().Contains(string.Format("{0} {1}{2}, {3} {4}{5}", totalAdults, "Adult", totalAdults > 1 ? "s" : "", totalRooms, "Room", totalRooms > 1 ? "s" : "")));
            else
                Assert.IsTrue(GetGuestsText().Contains(string.Format("{0} {1}{2}, {3} {4}{5}, {6} {7}{8}", totalAdults, "Adult", totalAdults > 1 ? "s" : "", totalChildren, "Child", totalChildren > 1 ? "ren" : "", totalRooms, "Room", totalRooms > 1 ? "s" : "")));
        }

        public string GetGuestsText()
        {
            System.Threading.Thread.Sleep(2000);
            if (ConfirmGuests.Visible)
                return GuestsInGoing.Text;
            else
                return GuestsInSearch.Text;
        }

        public int GetNonInfantOccupants()
        {
            int nonInfantOccupants = 0;
            foreach (var room in lRoomOccupantDetails)
            {
                nonInfantOccupants += room.NoOfAdults + room.NoOfChildren;
            }
            return nonInfantOccupants;
        }

        public virtual string GetNeedMoreRoomsText()
        {
            _webDriver.WaitForElementVisible(ByNeedMoreRoomsText, Constants.ShortWait, "Need more rooms text is not displayed");
            return NeedMoreRoomsText.Text;
        }

        public void PopulateNewChildAge(int newAge)
        {
            foreach (var age in ChildAgeField)
            {
                age.Click();
                ChildAgeValue[newAge].Click();
            }
        }

        public virtual void CloseIconButton()
        {
            _webDriver.WaitForElementVisible(byCloseIcon, 30, "close button  is not visible on occupancy model");
             CloseIcon.Click();
        }

        public int GetSelectedChildAge(int childToSelect)
        {
            return Convert.ToInt32(ChildAgeField[childToSelect - 1].Value);
        }
    }
}





