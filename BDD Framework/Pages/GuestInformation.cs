using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using OpenQA.Selenium;
using System.Threading;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class GuestInformation : MobileBasePage, IGuestInformation
    {
        private AtWebElement ProgressBar => _webDriver.FindElement(LocatorType.CssSelector, "ol[class*=progress-bar] li[class*=active] span[class*=label]");
        private AtBy byTitle => GetBy(LocatorType.XPath, "//select[contains(@id,'title')]");
        private ReadOnlyCollection<AtWebElement> Title => _webDriver.FindElements(byTitle);
        private AtWebElement TitleSuccessIcon => _webDriver.FindElement(LocatorType.XPath, "//select[contains(@id,'title')]/ancestor::div[contains(@class,'items')]/div[2]/*[contains(@class,'success')]");
        private ReadOnlyCollection<AtWebElement> FirstName => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@id,'firstName')]");
        private AtWebElement FirstNameSuccessIcon => _webDriver.FindElement(LocatorType.XPath, "//input[contains(@id,'firstName')]/ancestor::div[contains(@class,'items')]/div[2]/*[contains(@class,'success')]");
        private ReadOnlyCollection<AtWebElement> SurName => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@id,'lastName')]");
        private AtWebElement SurNameSuccessIcon => _webDriver.FindElement(LocatorType.XPath, "//input[contains(@id,'lastName')]/ancestor::div[contains(@class,'items')]/div[2]/*[contains(@class,'success')]");
        private AtWebElement PhoneNumber => _webDriver.FindElement(LocatorType.Id, "mobileTel");
        private AtWebElement PhoneNumberSuccessIcon => _webDriver.FindElement(LocatorType.XPath, "//input[@id='mobileTel']/ancestor::div[contains(@class,'items')]/div[contains(@class,'hold')]");
        private AtBy byEmail => GetBy(LocatorType.XPath, "//input[@id='email' and @placeholder='someone@somewhere.com']");
        private AtWebElement Email => _webDriver.FindElement(byEmail);
        private AtWebElement EmailSuccessIcon => _webDriver.FindElement(LocatorType.XPath, "//input[@id='email']/ancestor::div[contains(@class,'items')]/div[contains(@class,'hold')]");
        private AtBy bySpecialRequestToggle => GetBy(LocatorType.XPath, "//button[contains(@class,'sc-c-button--round')]");
        private AtWebElement SpecialRequestToggle => _webDriver.FindElement(bySpecialRequestToggle);
        private AtWebElement SpecialRequestLabel => _webDriver.FindElement(LocatorType.CssSelector, "label[class*=toggle]");
        private AtBy bySpecialRequestTextBox => GetBy(LocatorType.Id, "specialRequest");
        private AtWebElement SpecialRequestTextBox => _webDriver.FindElement(bySpecialRequestTextBox);
        private AtBy bySpecialRequestError => GetBy(LocatorType.XPath, "//textarea[@id='specialRequest']/ancestor::div[contains(@class,'padding-y-m')]//p[contains(@class,'error')]");
        private AtWebElement SpecialRequestError => _webDriver.FindElement(bySpecialRequestError);
        private AtWebElement SpecialRequestHeader => _webDriver.FindElement(LocatorType.XPath, "//h2[text()='Special Requests']");
        private AtBy byErrorMessages => GetBy(LocatorType.CssSelector, "p.sc-u-color-error");
        private ReadOnlyCollection<AtWebElement> ErrorMessages => _webDriver.FindElements(byErrorMessages);
        private AtBy byEmailErrorMessage => GetBy(LocatorType.XPath, "//label[text()='Email']/parent::div//p[contains(@class,'error')]");
        private AtWebElement EmailErrorMessage => _webDriver.FindElement(byEmailErrorMessage);
        private AtBy bySubmitButton => GetBy(LocatorType.CssSelector, "button[class*=accent] span[class*=label]");
        private AtWebElement SubmitButton => _webDriver.FindElement(bySubmitButton);
        private ReadOnlyCollection<AtWebElement> DateOfBirth => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@name,'dob')]");
        private ReadOnlyCollection<AtWebElement> DayOfBirth => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@name,'day')]");
        private ReadOnlyCollection<AtWebElement> MonthOfBirth => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@name,'month')]");
        private ReadOnlyCollection<AtWebElement> YearOfBirth => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@name,'year')]");
        private AtBy byThreeDSSubmitButton => GetBy(LocatorType.XPath, "//input[@value='Submit']");
        private AtWebElement ThreeDSSubmitButton => _webDriver.FindElement(byThreeDSSubmitButton);
        private AtBy byThreeDS2SubmitButton => GetBy(LocatorType.XPath, "//input[@value='OK']");
        private AtWebElement ThreeDS2SubmitButton => _webDriver.FindElement(byThreeDS2SubmitButton);
        private AtBy bySecureCheckOutMessage => GetBy(LocatorType.XPath, "//span[text()='Secure checkout - It only takes a few minutes!']");
        private AtWebElement SecureCheckOutMessage => _webDriver.FindElement(bySecureCheckOutMessage);
        private AtBy byOfferAndDiscountCheckbox => GetBy(LocatorType.CssSelector, "div input#emailSignUp");
        private AtWebElement OffersAndDiscountsCheckbox => _webDriver.FindElement(byOfferAndDiscountCheckbox);
        private AtBy byOffersAndDiscountsText => GetBy(LocatorType.XPath, "//label[contains(@class,'switch-control')]//span[contains(text(),'offers')]");
        private AtWebElement OffersAndDiscountsText => _webDriver.FindElement(byOffersAndDiscountsText);
        private AtWebElement EmailDiv => _webDriver.FindElement(LocatorType.XPath, "//input[@id='email' and @placeholder='someone@somewhere.com']/parent::div");
        private AtWebElement PassengerCaption(string PassengerType) => _webDriver.FindElement(LocatorType.XPath, "//*[text()='#' and @class='sc-o-heading']/parent::div", PassengerType);
        private AtBy byLeadGuestPrePopulated => GetBy(LocatorType.XPath, "//form//div[contains(@class,'baseline')]//span[contains(@class,'body--l')]");
        private AtWebElement LeadGuestPrePopulated => _webDriver.FindElement(byLeadGuestPrePopulated);
        private AtWebElement PrePopulatedNameChangeLink => _webDriver.FindElement(LocatorType.XPath, "//form//div[contains(@class,'baseline')]//a");

        #region[VoucherCodeWebElements]
        private AtBy byVoucherModalHeader => GetBy(LocatorType.XPath, "//h2[contains(text(),'voucher')]");
        private AtWebElement VoucherModalHeader => _webDriver.FindElement(byVoucherModalHeader);
        private AtWebElement VoucherCodeHeader => _webDriver.FindElement(LocatorType.XPath, "//label[text()='Enter code here:']");
        private AtWebElement InputVoucherCode => _webDriver.FindElement(LocatorType.Id, "voucherCode");
        private AtWebElement ApplyButton => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Apply']/parent::button");
        private AtBy byVoucherCodeLoader => GetBy(LocatorType.CssSelector, "span.sc-c-button__throbber");
        private AtWebElement RemoveButton => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Remove']/parent::button");
        private AtBy byVoucherCodeSuccessMessage => GetBy(LocatorType.CssSelector, "div.sc-u-margin-top-4xs span.sc-u-color-success");
        private AtWebElement VoucherCodeSuccessMessage => _webDriver.FindElement(byVoucherCodeSuccessMessage);
        private AtBy byInValidVoucherCodeMessage => GetBy(LocatorType.CssSelector, "span.sc-u-color-secondary");
        private AtWebElement InValidVoucherCodeMessage => _webDriver.FindElement(byInValidVoucherCodeMessage);
        #endregion

        private readonly IAtWebDriver _webDriver;
        private readonly ITravelInsurance travelInsurance;
        private List<PassengerInformation> passengerInformation;
        public GuestInformation(IAtWebDriver webDriver, ITravelInsurance travelInsurance)
            : base(webDriver)
        {
            _webDriver = webDriver;
            this.travelInsurance = travelInsurance;
        }

        public void ProceedToNextPage()
        {
            _webDriver.WaitForElementClickable(bySubmitButton, 10);
            _webDriver.ScrollToElement(SubmitButton);
            SubmitButton.ClickButtonUsingJs();
        }

        public void ConfirmBooking(bool isThreeDS = false, bool isAuthorised = true)
        {
            _webDriver.WaitForElementClickable(bySubmitButton, 10);
            _webDriver.ScrollElementToCenter(SubmitButton);
            if (!HelperFunctions.IsLive())
            {
                SubmitButton.Click();
                _webDriver.WaitUntilNotVisible(bySubmitButton, 60);
                if (isThreeDS && isAuthorised)
                {
                    try
                    {
                        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));
                        if(HelperFunctions.IsPDS2EnabledOnV3())
                        {
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt("threeDSecure2Iframe"));
                            _webDriver.WaitForElementVisible(byThreeDS2SubmitButton, 30, "3DS button is not visible");
                            ThreeDS2SubmitButton.ClickButtonUsingJs();
                            _webDriver.SwitchToDefaultContent();                            
                        }
                        //else
                        //{
                        //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt("threeDSecureIframe"));
                        //    _webDriver.WaitForElementVisible(byThreeDSSubmitButton, 30, "3DS button is not visible");
                        //    ThreeDSSubmitButton.ClickButtonUsingJs();
                        //}                        
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Assert.Inconclusive("Threeds page did not load!!!");
                    }
                }
                else if (isThreeDS && !isAuthorised)
                {
                    //Add logic 
                }
            }
            else
                Console.WriteLine("Completed test till booking form!!!");
        }

        public void ValidateMandatoryFieldErrorMessages()
        {
            _webDriver.WaitForElementVisible(byErrorMessages, 30, "Error message is not visible");
            _webDriver.ScrollElementToCenter(ErrorMessages[0]);
            Assert.AreEqual(ErrorMessages[0].Text, "Please select a title");
            Assert.AreEqual(ErrorMessages[1].Text, "Please enter a name");
            Assert.AreEqual(ErrorMessages[2].Text, "Please enter a surname");
            Assert.AreEqual(ErrorMessages[3].Text, "Please enter a phone number");
            Assert.AreEqual(ErrorMessages[4].Text, "Please enter an email address");
        }

        public void ToggleSpecialRequestsSection()
        {
            _webDriver.WaitForElementClickable(bySpecialRequestToggle, 20);
            _webDriver.ScrollElementToCenter(SpecialRequestToggle);
            if (!SpecialRequestTextBox.Visible)
                SpecialRequestToggle.ClickButtonUsingJs();
        }

        public void AddSpecialRequestsText()
        {
            _webDriver.WaitForElementVisible(bySpecialRequestTextBox, 10, "Special request textbox is not visible");
            SpecialRequestTextBox.EnterText("Non smoking room");
            Assert.IsFalse(SpecialRequestTextBox.Text == "");
        }

        public void EnterInvalidSpecialRequestsText(int size)
        {
            string randomizedRequest = HelperFunctions.RandomString(size);
            SpecialRequestTextBox.EnterText(randomizedRequest);
        }

        public bool IsSpecialRequestsErrorMessageVisible()
        {
            SpecialRequestHeader.Click();
            return SpecialRequestError.Visible;
        }
        public string GetSpecialRequestsErrorMessage()
        {
            return SpecialRequestError.Text;
        }

        public void SelectTitle(int roomNo = 1)
        {
            _webDriver.WaitForElementVisible(byTitle, 30, "Title is not visible");
            for (int room = 1; room <= roomNo; room++)
            {
                SelectElement dd = new SelectElement(Title[room - 1]);
                dd.SelectByValue("Mr");
            }
        }

        public void EnterFirstName(string name, int roomNo = 1)
        {
            _webDriver.ScrollElementToCenter(FirstName[roomNo - 1]);
            FirstName[roomNo - 1].Clear();
            FirstName[roomNo - 1].SendKeys(name);
        }

        public void ValidateFirstNameInGuestInfo(bool isValid, int roomNo = 1)
        {
            _webDriver.ScrollElementToCenter(Title[roomNo - 1]);
            Title[roomNo - 1].Click();
            if (isValid)
                Assert.IsTrue(FirstNameSuccessIcon.Visible);
            else
            {
                _webDriver.WaitForElementVisible(byErrorMessages, 10, "Error Message is not visible");
                Assert.AreEqual("Names cannot contain numbers or special characters", ErrorMessages[0].Text);
            }
        }

        public void EnterSurName(string name, int roomNo = 1)
        {
            _webDriver.ScrollElementToCenter(SurName[roomNo - 1]);
            SurName[roomNo - 1].Clear();
            SurName[roomNo - 1].SendKeys(name);
        }

        public void ValidateSurNameInGuestInfo(bool isValid, int roomNo = 1)
        {
            _webDriver.ScrollElementToCenter(FirstName[roomNo - 1]);
            FirstName[roomNo - 1].Click();
            if (isValid)
                Assert.IsTrue(SurNameSuccessIcon.Visible);
            else
            {
                _webDriver.WaitForElementVisible(byErrorMessages, 10, "Error Message is not visible");
                for (int i = 0; i < 2; i++)
                {
                    Assert.AreEqual("Names cannot contain numbers or special characters", ErrorMessages[i].Text);
                }
            }
        }

        public void ValidatePhoneNoInGuestInfo(bool isValid, int roomNo = 1)
        {
            _webDriver.ScrollElementToCenter(Email);
            Email.Click();
            if (isValid)
                Assert.IsTrue(PhoneNumberSuccessIcon.Visible);
            else
            {
                _webDriver.WaitForElementVisible(byErrorMessages, 10, "Error Message is not visible");
                Assert.AreEqual("Please enter a valid phone number", ErrorMessages[0].Text);
            }
        }

        public void EnterPhoneNumber(string number)
        {
            _webDriver.ScrollElementToCenter(PhoneNumber);
            PhoneNumber.Clear();
            PhoneNumber.SendKeys(number);
        }

        public void EnterEmailAddress(string email)
        {
            _webDriver.ScrollElementToCenter(Email);
            Email.Clear();
            Email.SendKeys(email);
        }

        public void ValidateEmailInGuestInfo(bool isValid)
        {
            _webDriver.ScrollElementToCenter(PhoneNumber);
            PhoneNumber.Click();
            if (isValid)
                Assert.IsTrue(EmailSuccessIcon.Visible);
            else
            {
                _webDriver.WaitForElementVisible(byErrorMessages, 10, "Error Message is not visible");
                Assert.AreEqual("Please enter a valid email address", EmailErrorMessage.Text);
            }
        }

        public void PopulatePassengers(List<RoomOccupantDetails> roomOccupantDetails, bool isThreeDS = false)
        {
            int counter = 0;
            int roomOccupantCount = 0;
            foreach (var room in roomOccupantDetails)
            {
                while (room.NoOfAdults != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, Constants.AdultAge, true, isThreeDS);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
                while (room.NoOfChildren != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, Constants.ChildAge, false, isThreeDS);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
                while (room.NoOfInfants != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, Constants.InfantAge, false, isThreeDS);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
            }
        }
    

        public void PopulatePassengersWithNames(List<RoomOccupantDetails> roomOccupantDetails, string firstName, string lastName)
        {
            int counter = 0;
            int roomOccupantCount = 0;
            foreach (var room in roomOccupantDetails)
            {
                while (room.NoOfAdults != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, Constants.AdultAge, true, firstName, lastName);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
                while (room.NoOfChildren != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, Constants.ChildAge, false, firstName, lastName);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
                while (room.NoOfInfants != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, Constants.InfantAge, false, firstName, lastName);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
            }
        }


        public void PopulateInvaildPassengers(List<RoomOccupantDetails> roomOccupantDetails, int adultAge, int childAge, int infantAge, bool isThreeDS = false)
        {
            int counter = 0;
            int roomOccupantCount = 0;
            foreach (var room in roomOccupantDetails)
            {
                while (room.NoOfAdults != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, adultAge, true, isThreeDS);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
                while (room.NoOfChildren != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, childAge, false, isThreeDS);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
                while (room.NoOfInfants != roomOccupantCount)
                {
                    PopulatePassengerInfo(counter, infantAge, false, isThreeDS);
                    roomOccupantCount++;
                    counter++;
                }
                roomOccupantCount = 0;
            }
        }

        public void PopulatePassengerInfo(int counter, int age, bool isAdult, string firstName, string lastName)
        {
            passengerInformation = new List<PassengerInformation>();
            PassengerInformation passenger = new PassengerInformation();
            _webDriver.WaitForElementVisible(byTitle, Constants.MediumWait, "Title is not visible");
            if (isAdult)
                passenger.Title = "Mr";
            else
                passenger.Title = "Mstr";
            passenger.DOB = HelperFunctions.GetDateOfBirth(age);
  

                passenger.FirstName = firstName != ""? firstName : "Threed";
                passenger.LastName = lastName != "" ? lastName : "Authorised";
    
            SelectElement dd = new SelectElement(Title[counter]);
            dd.SelectByValue(passenger.Title);
            FirstName[counter].Clear();
            FirstName[counter].SendKeysUsingActions(passenger.FirstName);
            SurName[counter].Clear();
            SurName[counter].SendKeysUsingActions(passenger.LastName);
            if (!travelInsurance.IsInsuranceAdded())
            {
                DateOfBirth[counter].Clear();
                DateOfBirth[counter].SendKeysUsingActions(passenger.DOB.ToString("ddMMyyyy"));
            }
            else
                Assert.IsTrue(DateOfBirth[counter].Value.Equals(passenger.DOB.ToString("dd/MM/yyyy")));

            passengerInformation.Add(passenger);
        }


        public void PopulatePassengerInfo(int counter, int age, bool isAdult, bool isThreeDS)
        {
            passengerInformation = new List<PassengerInformation>();
            PassengerInformation passenger = new PassengerInformation();
            _webDriver.WaitForElementVisible(byTitle, Constants.MediumWait, "Title is not visible");
            if (isAdult)
                passenger.Title = "Mr";
            else
                passenger.Title = "Mstr";
            passenger.DOB = HelperFunctions.GetDateOfBirth(age);
            if (isThreeDS)
            {
                passenger.FirstName = "ThreeDSVONE";
                passenger.LastName = "CHALLENGEIDENTIFIED";
            }
            else
            {
                passenger.FirstName = HelperFunctions.RandomString(10);
                passenger.LastName = HelperFunctions.RandomString(10);
            }
            SelectElement dd = new SelectElement(Title[counter]);
            dd.SelectByValue(passenger.Title);
            FirstName[counter].Clear();
            FirstName[counter].SendKeysUsingActions(passenger.FirstName);
            SurName[counter].Clear();
            SurName[counter].SendKeysUsingActions(passenger.LastName);
            if (!travelInsurance.IsInsuranceAdded())
            {
                DateOfBirth[counter].Clear();
                DateOfBirth[counter].SendKeysUsingActions(passenger.DOB.ToString("ddMMyyyy"));
            }
            else
                Assert.IsTrue(DateOfBirth[counter].Value.Equals(passenger.DOB.ToString("dd/MM/yyyy")));

            passengerInformation.Add(passenger);
        }

        public void PopulateHotelGuests(List<RoomOccupantDetails> roomOccupants, bool isThreeDS = false)
        {
            SelectTitle(roomOccupants.Count);
            passengerInformation = new List<PassengerInformation>();
            foreach (var room in roomOccupants)
            {
                PassengerInformation passenger = new PassengerInformation();
                passenger.Title = "Mr";
                if (isThreeDS)
                {
                    passenger.FirstName = "ThreeDSVONE";
                    passenger.LastName = "CHALLENGEIDENTIFIED";
                }
                else
                {
                    passenger.FirstName = HelperFunctions.RandomString(10);
                    passenger.LastName = HelperFunctions.RandomString(10);
                }
                FirstName[roomOccupants.IndexOf(room)].SendKeysUsingActions(passenger.FirstName);
                SurName[roomOccupants.IndexOf(room)].SendKeysUsingActions(passenger.LastName);
                passengerInformation.Add(passenger);
            }
        }

        public void PopulateHotelGuestsWithNames(List<RoomOccupantDetails> roomOccupants, string firstName, string lastName)
        {
            SelectTitle(roomOccupants.Count);
            passengerInformation = new List<PassengerInformation>();
            foreach (var room in roomOccupants)
            {
                PassengerInformation passenger = new PassengerInformation();
                passenger.Title = "Mr";
                if (firstName != "" && lastName !="")
                {
                    passenger.FirstName = firstName;
                    passenger.LastName = lastName;
                }
                else
                {
                    passenger.FirstName = HelperFunctions.RandomString(10);
                    passenger.LastName = HelperFunctions.RandomString(10);
                }
                FirstName[roomOccupants.IndexOf(room)].SendKeysUsingActions(passenger.FirstName);
                SurName[roomOccupants.IndexOf(room)].SendKeysUsingActions(passenger.LastName);
                passengerInformation.Add(passenger);
            }
        }

        public string GetSecureCheckoutMessage()
        {
            _webDriver.WaitForElementVisible(bySecureCheckOutMessage, Constants.ShortWait, "secure checkout message is not displayed");
            return SecureCheckOutMessage.Text;
        }

        public bool IsOfferAndDiscountCheckboxVisible()
        {
            _webDriver.WaitForElementVisible(byEmail, Constants.DefaultWait, "Email field is not visible");
            _webDriver.ScrollToElement(Email);
            return OffersAndDiscountsCheckbox.Enabled;
        }

        public string GetOfferAndDiscountsText()
        {
            _webDriver.WaitForElementVisible(byOffersAndDiscountsText, Constants.ShortWait, "Offers and discount text is not displayed");
            _webDriver.ScrollToElement(OffersAndDiscountsText);
            return OffersAndDiscountsText.Text;
        }

        public List<string> GetAllErrorMessages()
        {
            List<string> errorMessage = new List<string>();
            foreach (var element in ErrorMessages)
            {
                errorMessage.Add(element.Text);
            }
            return errorMessage;
        }

        public bool IsEmailSuccessIconVisible()
        {
            return EmailSuccessIcon.Visible;
        }

        public bool IsPhoneNumberSuccessIconVisible()
        {
            return PhoneNumberSuccessIcon.Visible;
        }

        public bool IsEmailFieldDisabled()
        {
            return EmailDiv.GetAttribute("class").Contains("disabled");
        }

        public string GetAdultPassengerCaption()
        {
            _webDriver.WaitForElementVisible(byTitle, 30, "Guest info page is not visible");
            return PassengerCaption("Adult").Text.Split("\r\n")[1];
        }

        public string GetChildPassengerCaption()
        {
            return PassengerCaption("Child").Text.Split("\r\n")[1];
        }

        public string GetInfantPassengerCaption()
        {
            return PassengerCaption("Infant").Text.Split("\r\n")[1];
        }

        public bool IsLeadGuestNamePrePopulated()
        {
            _webDriver.WaitForElementVisible(byLeadGuestPrePopulated, Constants.DefaultWait, "Lead guest is not displayed");
            return LeadGuestPrePopulated.Visible;
        }

        public bool IsChangeLeadGuestPrePopulatedLinkVisible()
        {
            return PrePopulatedNameChangeLink.Visible;
        }

        public void ClickChangePrePopulatedLeadGuestName()
        {
            PrePopulatedNameChangeLink.Click();
        }

        public bool IsTitleSuccessIconVisible()
        {
            return TitleSuccessIcon.Visible;
        }

        public bool IsFirstNameSuccessIconVisible()
        {
            return FirstNameSuccessIcon.Visible;
        }

        public bool IsSurNameSuccessIconVisible()
        {
            return SurNameSuccessIcon.Visible;
        }

        public List<PassengerInformation> GetPassengerInformation()
        {
            return passengerInformation;
        }

        #region[VoucherCodeMethods]
        public bool ValidateVocherCodeDetailsInTheBookingForm()
        {
            _webDriver.WaitForElementVisible(byVoucherModalHeader, Constants.MediumWait, "Vocher Modal header is not Displayed");
            _webDriver.ScrollElementToCenter(VoucherModalHeader);
            if (VoucherModalHeader.Text.Equals(Constants.VoucherModalHeader) && VoucherCodeHeader.Text.Equals(Constants.VoucherCodeHeader) && !ApplyButton.Enabled && InputVoucherCode.GetAttribute("value") == "")
                return true;
            else
                return false;
        }


        public void ApplyVoucherCode(string VoucherCode)
        {
            _webDriver.WaitForElementVisible(byVoucherModalHeader, Constants.MediumWait, "Vocher Modal header is not Displayed");
            _webDriver.ScrollElementToCenter(VoucherModalHeader);
            InputVoucherCode.SendKeys(VoucherCode);                
            ApplyButton.Click();            
        }


        public bool ValidateVoucherCodeAppliedSuccessMessage()
        {
            _webDriver.WaitUntilNotVisible(byVoucherCodeLoader, Constants.DefaultWait, "Voucher code not Applied");
            _webDriver.WaitForElementVisible(byVoucherCodeSuccessMessage, Constants.DefaultWait, "VoucherCode success Message is not Displayed ");
            if (VoucherCodeSuccessMessage.Text.Equals(Constants.VoucherCodeAppliedSuccessMessage) && RemoveButton.Enabled)
                return true;
            else
                return false;
        }

        public void RemoveVoucherCode()
        {
            _webDriver.WaitForElementVisible(byVoucherCodeSuccessMessage, Constants.DefaultWait, "VoucherCode success Message is not Displayed ");
            RemoveButton.Click();
            _webDriver.WaitUntilNotVisible(byVoucherCodeLoader, Constants.DefaultWait, "Voucher code not Applied");
        }

        public bool ValidateVoucherCodeRemovedSuccessMessage()
        {
            _webDriver.WaitUntilNotVisible(byVoucherCodeLoader, Constants.DefaultWait, "Voucher code not Applied");
            _webDriver.WaitForElementVisible(byVoucherCodeSuccessMessage, Constants.MediumWait, "VoucherCode success Message is not Displayed ");            
            Thread.Sleep(5000);
            if (VoucherCodeSuccessMessage.Text.Equals(Constants.VocherCodeRemoveSuccessMessage) && !ApplyButton.Enabled)
                return true;
            else
                return false;
        }

        public bool ValidateInvalidOrExpiredVoucherCodeMessage(string voucherCode)
        {
            _webDriver.WaitUntilNotVisible(byVoucherCodeLoader, Constants.DefaultWait, "Voucher code not Applied");
            _webDriver.WaitForElementVisible(byInValidVoucherCodeMessage, Constants.MediumWait, "Voucher code Message is not Displayed");
            if (InValidVoucherCodeMessage.Text.Equals("The code "+ voucherCode +" you entered is incorrect or has expired") && !ApplyButton.Enabled)
                return true;
            else
                return false;
        }

        public bool ValidateMessageForPromoCriteriaNotMet(string voucherCode)
        {
            _webDriver.WaitUntilNotVisible(byVoucherCodeLoader, Constants.DefaultWait, "Voucher code not Applied");
            _webDriver.WaitForElementVisible(byInValidVoucherCodeMessage, Constants.MediumWait, "Voucher code Message is not Displayed");
            if (InValidVoucherCodeMessage.Text.Equals("The code " + voucherCode + " you entered is not valid for this booking") && !ApplyButton.Enabled)
                return true;
            else
                return false;
        }

        public bool ValidateBetterDealMessage(string voucherCode)
        {
            _webDriver.WaitUntilNotVisible(byVoucherCodeLoader, Constants.DefaultWait, "Voucher code not Applied");
            _webDriver.WaitForElementVisible(byInValidVoucherCodeMessage, Constants.MediumWait, "Voucher code Message is not Displayed");
            if (InValidVoucherCodeMessage.Text.Equals("Voucher code " + voucherCode + " not applied. You already have a better deal!") && !ApplyButton.Enabled)
                return true;
            else
                return false;
        }

        #endregion

    }
}
       
    



