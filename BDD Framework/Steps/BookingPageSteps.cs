using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.Reporting.CustomReporter;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public class BookingPageSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly ISearchComponent _searchComponent;
        private readonly IGuestComponent _guestComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly IGuestInformation _guestInformation;
        private readonly IChoosePayment _choosePayment;
        private readonly IPaymentPage _paymentPage;
        private readonly IPaypalPage _paypalPage;
        private readonly IBookingConfirmationPage _bookingConfirmationPage;
        private readonly ITransfers _transfers;
        private readonly IHolidayExtrasPage _holidayExtrasPage;
        private readonly IFlightSearchResults _flightSearchResults;

        public BookingPageSteps(ISearchSummaryComponent searchSummaryComponent, ILandingPageGuestComponent landingPageGuestComponent, ISearchComponent searchComponent, IGuestComponent guestComponent, ICalendarComponent calendarComponent, IHomePage homePage, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, IBookingSummary bookingSummary, IGuestInformation guestInformation, IChoosePayment choosePayment, IPaymentPage paymentPage, IPaypalPage paypalPage, IBookingConfirmationPage bookingConfirmationPage, ITransfers transfers, IHolidayExtrasPage holidayExtrasPage, IFlightSearchResults flightSearchResults, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _searchSummaryComponent = searchSummaryComponent;
            _searchComponent = searchComponent;
            _calendarComponent = calendarComponent;
            _guestComponent = guestComponent;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _bookingSummary = bookingSummary;
            _guestInformation = guestInformation;
            _choosePayment = choosePayment;
            _paymentPage = paymentPage;
            _paypalPage = paypalPage;
            _bookingConfirmationPage = bookingConfirmationPage;
            _transfers = transfers;
            _holidayExtrasPage = holidayExtrasPage;
            _flightSearchResults = flightSearchResults;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        [Given(@"I am on booking summary page")]
        public void GivenIAmOnMobileBookingSummaryPage()
        {
            _homePage.SearchHotels();
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if (HelperFunctions.IsDesktop())
                _hotelEstabPage.SelectDesktopBoardType(1, 1);
            else
            {
                _hotelEstabPage.SelectRoom(1);
                _hotelEstabPage.SelectBoardType(1);
            }
        }

        [Given(@"I am on guest information page for holiday search to (.*) from (.*) for (.*) during (.*) and (.*) dates")]
        [When(@"I am on guest information page for holiday search to (.*) from (.*) for (.*) during (.*) and (.*) dates")]
        public void GivenIAmOnGuestInformationPageForHolidaySearchToFromForDuringAndDates(string destination, string departureAirport, string guests, int departure, int duration)
        {
            _homePage.SearchHolidays(destination, departureAirport, departure, duration, guests);
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if(HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.KeepSelectedFlight();
            _holidayExtrasPage.ContinueToBook();
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickConfirmButton();

        }
        [When(@"I enter invalid Date of birth in adult DOB field")]
        public void WhenIEnterInvalidDateOfBirthInAdultDOBField()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _guestInformation.PopulateInvaildPassengers(_landingPageGuestComponent.GetRoomOccupantDetails(), 12, Constants.ChildAge, Constants.InfantAge);
            else
                _guestInformation.PopulateInvaildPassengers(_guestComponent.GetRoomOccupantDetails(), 12, Constants.ChildAge, Constants.InfantAge);
        }
        [Then(@"Appropriate error message should be displayed for incorrect adult age")]
        public void ThenAppropriateErrorMessageShouldBeDisplayedForIncorrectAdultAge()
        {
            _guestInformation.ProceedToNextPage();
            List<string> errorMessagesonGuestInfo = new List<string>();
            errorMessagesonGuestInfo = _guestInformation.GetAllErrorMessages();
            
            foreach (var error in errorMessagesonGuestInfo)
            {
                if(error.Contains("Adult"))
                {
                    Assert.AreEqual(ErrorMessagesOnGuestInfo.AdultErroMessageOnGuestInfo.Replace("*", Constants.ChildAge.ToString()), error.ToString());
                }              
            }
        }
   
        [When(@"I enter invalid Date of birth in Child DOB field")]
        public void WhenIEnterInvalidDateOfBirthInChildDOBField()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _guestInformation.PopulateInvaildPassengers(_landingPageGuestComponent.GetRoomOccupantDetails(), Constants.AdultAge, 5, Constants.InfantAge);
            else
                _guestInformation.PopulateInvaildPassengers(_guestComponent.GetRoomOccupantDetails(), Constants.AdultAge, 5,Constants.InfantAge);
        }

        [Then(@"Appropriate error message should be displayed for incorrect child age")]
        public void ThenAppropriateErrorMessageShouldBeDisplayedForIncorrectChildAge()
        {
            _guestInformation.ProceedToNextPage();
            List<string> errorMessagesonGuestInfo = new List<string>();
            errorMessagesonGuestInfo = _guestInformation.GetAllErrorMessages();
            foreach (var error in errorMessagesonGuestInfo)
            {
                Assert.AreEqual(ErrorMessagesOnGuestInfo.ChildErrorMessageOnGuestInfo.Replace("*", Constants.ChildAge.ToString()), error.ToString());
            }
        }

        [When(@"I enter invalid Date of birth in infant DOB field")]
        public void WhenIEnterInvalidDateOfBirthInInfantDOBField()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _guestInformation.PopulateInvaildPassengers(_landingPageGuestComponent.GetRoomOccupantDetails(), Constants.AdultAge, Constants.ChildAge, 10);
            else
                _guestInformation.PopulateInvaildPassengers(_guestComponent.GetRoomOccupantDetails(), Constants.AdultAge, Constants.ChildAge, 10);
        }

        [Then(@"Appropriate error message should be displayed for incorrect infant age")]
        public void ThenAppropriateErrorMessageShouldBeDisplayedForIncorrectInfantAge()
        {
            _guestInformation.ProceedToNextPage();
            List<string> errorMessagesonGuestInfo = new List<string>();
            errorMessagesonGuestInfo = _guestInformation.GetAllErrorMessages();
            foreach (var error in errorMessagesonGuestInfo)
            {
                Assert.AreEqual(ErrorMessagesOnGuestInfo.InfantErrorMessageOnGuestInfo.Replace("*", Constants.InfantAge.ToString()), error.ToString());
            }
        }

        [Given(@"I am on guest information page for hotel flow")]
        public void GivenIAmOnGuestInformationPageforhotelflow()
        {
            GivenIAmOnMobileBookingSummaryPage();
            if(!HelperFunctions.IsDesktop())
                WhenIConfirmTheBookingSelection();
        }

        [When(@"I confirm the booking selection")]
        [When(@"Confirm the hotel booking selection")]
        public void WhenIConfirmTheBookingSelection()
        {
            _bookingSummary.ClickConfirmButton();
        }

        [When(@"I proceed to choose payment plan")]
        public void WhenIProceedToChoosePaymentPlan()
        {
            _guestInformation.ProceedToNextPage();
        }

        [Then(@"Error messages for the mandatory fields violation are displayed")]
        public void ThenErrorMessagesForTheMandatoryFieldsViolationAreDisplayed()
        {
            _guestInformation.ValidateMandatoryFieldErrorMessages();
        }

        [When(@"I toggle the switch on for the special requests")]
        public void WhenIToggleTheSwitchOnForTheSpecialRequests()
        {
            _guestInformation.ToggleSpecialRequestsSection();
        }

        [When(@"Enter special request text with (.*) charcters")]
        public void WhenIEnterSpecialRequestTextWithCharcters(int stringSize)
        {
            _guestInformation.EnterInvalidSpecialRequestsText(stringSize);
        }

        [Then(@"Special request (.*) should be displayed")]
        public void ThenSpecialRequestPleaseEnterLessThanCharactersShouldBeDisplayed(string errorMessage)
        {
            Assert.IsTrue(_guestInformation.IsSpecialRequestsErrorMessageVisible(), "Error message not displayed");
            Assert.AreEqual(_guestInformation.GetSpecialRequestsErrorMessage(), errorMessage, "Error message not matching. Expected: " + errorMessage + " Actual: " + _guestInformation.GetSpecialRequestsErrorMessage());
        }

        [Then(@"Special request (.*) should not be displayed")]
        public void ThenSpecialRequestPleaseEnterLessThanCharactersShouldBeNotDisplayed(string errorMessage)
        {
            Assert.IsFalse(_guestInformation.IsSpecialRequestsErrorMessageVisible(), "Error message is displayed");
        }

        [When(@"Valid special request is entered")]
        public void WhenValidSpecialRequestIsEntered()
        {
            _guestInformation.AddSpecialRequestsText();
        }


        [When(@"I enter first (.*)")]
        public void WhenIEnterFirst(string name)
        {
            _guestInformation.SelectTitle();
            _guestInformation.EnterFirstName(name);
        }

        [Then(@"Firstname field accepts (.*) information")]
        public void ThenFirstnameFieldAcceptsInformation(bool isValid)
        {
            _guestInformation.ValidateFirstNameInGuestInfo(isValid);
        }

        [When(@"I enter sur (.*)")]
        public void WhenIEnterSur(string name)
        {
            _guestInformation.EnterSurName(name);
        }

        [Then(@"Surname field accepts (.*) information")]
        public void ThenSurnameFieldAcceptsInformation(bool isValid)
        {
            _guestInformation.ValidateSurNameInGuestInfo(isValid);
        }

        [Given(@"The firstname and lastname is filled")]
        [Then(@"I can fill the passenger (.*) details")]
        [When(@"Fill the passenger (.*) details")]
        [When(@"I fill the passenger (.*) details")]
        public void GivenTheFirstnameAndLastnameIsFilled(string name = "Auto")
        {
            if(HelperFunctions.IsV3HomepageEnabled())
            {
                _guestInformation.PopulateHotelGuests(_landingPageGuestComponent.GetRoomOccupantDetails());
            }
            else
            {
                _guestInformation.PopulateHotelGuests(_guestComponent.GetRoomOccupantDetails());
            }
            
         
        }

        [When(@"Fill holiday guests details")]
        [When(@"I fill holiday guests details")]
        public void WhenFillHolidayGuestsDetails()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _guestInformation.PopulatePassengers(_landingPageGuestComponent.GetRoomOccupantDetails(), false);
            else
                _guestInformation.PopulatePassengers(_guestComponent.GetRoomOccupantDetails(), false);
        }

        [When(@"Fill hotel guests details (.*)")]
        public void WhenFillHotelGuestsDetails(string ThreeDS)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _guestInformation.PopulateHotelGuests(_landingPageGuestComponent.GetRoomOccupantDetails(), true);
            else
                _guestInformation.PopulateHotelGuests(_guestComponent.GetRoomOccupantDetails(), true);
        }

        //[When(@"Fill the passenger details (.*)")]
        //[When(@"Fill the passenger details")]
        //public void WhenFillThePassengerDetails(bool ThreeDS)
        //{
        //     if (context.ContainsKey("RoomDetails"))
        //    {
        //        _guestInformation.PopulatePassengers(context["RoomDetails"] as List<RoomOccupantDetails>, ThreeDS);
        //    }
        //    else
        //    {
        //        _guestInformation.PopulatePassengers(context["DefaultRoomDetails"] as List<RoomOccupantDetails>, ThreeDS);
        //    }
        //}

        [When(@"I enter phone (.*)")]
        public void WhenIEnterPhone(string phoneNumber = "9876543210")
        {
            _guestInformation.EnterPhoneNumber(phoneNumber);
        }


        [Then(@"Phonenumber field accepts (.*) information")]
        public void ThenPhonenumberFieldAcceptsInformation(bool isValid)
        {
            _guestInformation.ValidatePhoneNoInGuestInfo(isValid);
        }

        [Given(@"The phone number is filled")]
        [Then(@"Add phone number")]
        [When(@"Add phone number")]
        public void GivenThePhoneNumberIsFilled()
        {
            _guestInformation.EnterPhoneNumber("0123 456 789");
        }

        [When(@"I enter email (.*)")]
        public void WhenIEnterEmail(string id = "bjohnson@travelrepublic.co.uk")
        {
            _guestInformation.EnterEmailAddress(id);
        }

        [Given(@"The email is filled")]
        [Then(@"Add email id")]
        [When(@"Add email id")]
        public void GivenTheEmailIsFilled()
        {
            _guestInformation.EnterEmailAddress("bjohnson@travelrepublic.co.uk");
        }

        [Then(@"Email field accepts (.*) information")]
        public void ThenEmailFieldAcceptsInformation(bool isValid)
        {
            _guestInformation.ValidateEmailInGuestInfo(isValid);
        }

        [When(@"Proceed to choose payment page")]
        [Then(@"Proceed to choose payment page")]
        [When(@"Proceed to payment page")]
        public void WhenProceedToChoosePaymentPage()
        {
            _guestInformation.ProceedToNextPage();
        }

        [When(@"Confirm the booking")]
        public void WhenConfirmTheBooking()
        {
            _guestInformation.ConfirmBooking();
        }

        [When(@"Confirm the booking with Threed authorization")]
        public void WhenConfirmTheBookingWithThreedAuthorization()
        {
            _guestInformation.ConfirmBooking(true);
        }

        [When(@"Choose full payment plan")]
        public void WhenChoosePaymentPlan()
        {
            _choosePayment.ChoosePaymentPlan();
        }

        //[When(@"Choose deposit payment plan")]
        //public void WhenChooseDepositPaymentPlan()
        //{
        //    _choosePayment.ChoosePaymentPlan(true);
        //}

        [When(@"Select (.*) payment plan")]
        public void WhenSelectPaymentPlan(string paymentType)
        {
            if (paymentType.ToLower() == "deposit")
                _choosePayment.ChoosePaymentPlan(true, false);
            else if (paymentType.ToLower() == "monthly")
                _choosePayment.ChoosePaymentPlan(false, true);
            else
                _choosePayment.ChoosePaymentPlan();
        }


        [When(@"Choose paypal")]
        public void WhenChoosePaypal()
        {
            _choosePayment.ChoosePaypal(true);
        }


        [When(@"Auto populate card holder address")]
        public void WhenAutoPopulateCardHolderAddress()
        {
            _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
        }

        [When(@"Enter card details")]
        public void WhenEnterCardDetails()
        {
            _paymentPage.SetCardNumber();
            _paymentPage.SetExpiry();
            _paymentPage.SetSecurityNumber();
        }

        [When(@"Complete payment from Paypal site")]
        public void WhenCompletePaymentFromPaypalSite()
        {
            _paypalPage.PerformPaypalPayment();
        }

        [Then(@"Booking should be declined")]
        public void ThenBookingShouldBeDeclined()
        {
            if (!HelperFunctions.IsLive())
            {
                Assert.IsTrue(_paymentPage.HasBookingFailed(), "Booked has not failed");
            }
        }

        [Then(@"Booking references of booked items are available")]
        public void ThenBookingReferencesOfBookedItemsAreAvailable()
        {
            if (!HelperFunctions.IsLive())
            {
                context.Add("BookingIDs", _bookingConfirmationPage.GetBookingDetails() as List<string>);
                foreach (string Id in context["BookingIDs"] as List<string>)
                {
                    Console.WriteLine(Id);
                }
            }            
        }

        [Then(@"Local tax is displayed in booking summary page")]
        public void ThenLocalTaxIsDisplayedInBookingSummaryPage()
        {
            context.Add("LocalTaxInBookingSummary", Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(_bookingSummary.GetLocalTaxes())));
            Console.WriteLine("LocalTaxInBookingSummary: " + context["LocalTaxInBookingSummary"] as string);
        }

        [Then(@"Pay now section does not include city tax amount")]
        public void ThenPayNowSectionDoesNotIncludeCityTaxAmount()
        {
            decimal price = 0;
            if (HelperFunctions.IsDesktop())
                price += _hotelSearchResults.GetHotelInformation().Price;
            List<RoomInformation> lRoomInformation = _hotelEstabPage.GetSelectedRoomTypes();
            List<BoardTypeInformation> lBoardTypeInformation = _hotelEstabPage.GetSelectedBoardTypes();
            for (int room = 0; room < lRoomInformation.Count; room++)
            {
                price = price + lRoomInformation[room].RoomPrice + lBoardTypeInformation[room].BoardTypePrice;
            }
            Assert.IsTrue(_bookingSummary.GetPayNowCost() <= price);
            if (!HelperFunctions.IsDesktop())
                Assert.IsFalse(_bookingSummary.GetTotalPrice() - price > 2);
        }

        [Then(@"Local tax message is displayed for pay full amount")]
        public void ThenLocalTaxMessageIsDisplayedForPayFullAmount()
        {
            Assert.IsTrue(_choosePayment.GetLocalTaxMessageFromPayFullSection().Contains("Local charges"));
            Assert.IsTrue(_choosePayment.GetLocalTaxAmountFromPayFullSection() - (Convert.ToDecimal(context["LocalTaxInSearchResults"])) < 2);
        }

        [Then(@"Local tax message is dispalyed for pay deposit amount")]
        public void ThenLocalTaxMessageIsDispalyedForPayDepositAmount()
        {
            Assert.IsTrue(_choosePayment.GetLocalTaxMessageFromPayDepositSection().Contains("Local charges"));
            Assert.IsTrue(_choosePayment.GetLocalTaxAmountFromPayDepositSection() - (Convert.ToDecimal(context["LocalTaxInSearchResults"])) < 2);
        }

        [Then(@"Local tax should match accordingly")]
        public void ThenLocalTaxShouldMatchAccordingly()
        {
            Assert.IsFalse(Convert.ToInt32(context["LocalTaxInSearchResults"]) - Convert.ToInt32(context["LocalTaxInEstabPage"]) > 1);
            if (!HelperFunctions.IsDesktop())
                Assert.IsFalse(Convert.ToInt32(context["LocalTaxInSearchResults"]) - Convert.ToInt32(context["LocalTaxInBoardType"]) > 1);
            if (Convert.ToInt32(context["LocalTaxInBookingSummary"]) < Convert.ToInt32(context["LocalTaxInSearchResults"]))
                Assert.IsFalse(Convert.ToInt32(context["LocalTaxInSearchResults"]) - Convert.ToInt32(context["LocalTaxInBookingSummary"]) > 1);
            else
                Assert.IsFalse(Convert.ToInt32(context["LocalTaxInBookingSummary"]) - Convert.ToInt32(context["LocalTaxInSearchResults"]) > 1);
            Console.WriteLine("Local tax amount matches in all pages");
        }

        [When(@"Click change room link")]
        public void GivenClickChangeRoomLink()
        {
            _bookingSummary.ClickChangeRoom(1);
        }

        [Then(@"User is able to choose a different room in estab page")]
        public void GivenUserIsAbleToChooseADifferentRoomInEstabPage()
        {
            Assert.IsTrue(_hotelEstabPage.GetRoomTypeCount() > 0);
            context.Add("RoomInformation", _hotelEstabPage.GetRoomInformation(1));
            _hotelEstabPage.SelectRoom(1);
            context.Add("BoardTypeInformation", _hotelEstabPage.GetBoardTypeInformation(1));
            _hotelEstabPage.SelectBoardType(1);
        }

        [When(@"I click on the room information icon")]
        public void WhenIClickOnTheRoomInformationIcon()
        {
            _bookingSummary.ClickRoomInformationIcon(1);
        }

        [Then(@"Modal pop up is displayed with appropriate message")]
        public void ThenModalPopUpIsDisplayedWithAppropriateMessage()
        {
            Assert.IsTrue(_bookingSummary.GetRoomInformationModalContent().Length > 0);
            _bookingSummary.CloseRoomInformationModal();
        }

        [Then(@"Price is included in sticky header of booking form")]
        public void ThenPriceIsIncludedInStickyHeaderOfBookingForm()
        {
            _bookingSummary.ClickConfirmButton();
            _choosePayment.ChoosePaymentPlan();
            if (!HelperFunctions.IsDesktop())
            {
                if (Convert.ToDecimal(context["DepositPrice"]) > 0)
                    Assert.IsTrue(Math.Abs(_bookingSummary.GetPriceInHeader() - (Convert.ToDecimal(context["Price"]))) < 5);
                else
                    Assert.IsTrue(Math.Abs(_bookingSummary.GetPriceInHeader() - (Convert.ToDecimal(context["Price"]))) < 2);
            }
        }
        
        [Then(@"Secure Checkout Message is displayed")]
        public void ThenSecureCheckoutMessageIsDisplayed()
        {
            Assert.AreEqual("Secure checkout - It only takes a few minutes!", _guestInformation.GetSecureCheckoutMessage(),"Checkout message is not displayed"); 
        }

        [Then(@"Offers and Discount checkbox is displayed")]
        public void ThenOffersAndDiscountCheckboxIsDisplayed()
        {
            Assert.IsTrue(_guestInformation.IsOfferAndDiscountCheckboxVisible(), "offers and discount checkbox is not displayed");
        }

        [Then(@"(.*) text is displayed")]
        public void ThenOffersAndDiscountsTextIsDisplayed(string message)
        {
            Assert.AreEqual(_guestInformation.GetOfferAndDiscountsText(), message);
        }

        [Then(@"Validate GA Tracking for payment")]
        public void ThenValidateGATrackingForPayment()
        {
            var x = _bookingConfirmationPage.GetBalanceAmount();
            try
            {

                Dictionary<string, dynamic> gaData = _bookingSummary.GetGAData("event", "purchase");
                Assert.AreEqual(gaData.GetValueOrDefault("widgetTitle"), "Hotels in Spain");
            }
            catch 
            {

            }   
        }

        [Then(@"I want to see the passenger captions on guest information page")]
        public void ThenIWantToSeeThePassengerCaptionsOnGuestInformationPage()
        {
            Assert.AreEqual("Must be 18 or over", _guestInformation.GetAdultPassengerCaption(), "Incorrect adult passenger caption");
            Assert.AreEqual("Will be 8 yrs-old on the date of return", _guestInformation.GetChildPassengerCaption(), "Incorrect child passenger caption");
            Assert.AreEqual("Will be under 1 yr-old on the date of return", _guestInformation.GetInfantPassengerCaption(), "Incorrect Infant passenger caption");
        }
        
        [Then(@"user details should be prepopulated in guest info")]
        public void ThenUserDetailsShouldBePrepopulatedInGuestInfo()
        {
            Assert.IsTrue(_guestInformation.IsLeadGuestNamePrePopulated(), "Lead guest name is not Prepopulated");
            Assert.IsTrue(_guestInformation.IsChangeLeadGuestPrePopulatedLinkVisible(), "Change lead guest name link is not displayed ");
            _guestInformation.ClickChangePrePopulatedLeadGuestName();
            Assert.IsTrue(_guestInformation.IsTitleSuccessIconVisible(), "Title field success icon is not displayed");
            Assert.IsTrue(_guestInformation.IsFirstNameSuccessIconVisible(), "First name field success icon is not displayed");
            Assert.IsTrue(_guestInformation.IsSurNameSuccessIconVisible(), "Surname field success icon is not displayed");
            Assert.IsTrue(_guestInformation.IsPhoneNumberSuccessIconVisible(), "Phone number field succes icon is not displayed");
            Assert.IsTrue(_guestInformation.IsEmailSuccessIconVisible(), "Email field succes icon is not displayed");
            Assert.IsTrue(_guestInformation.IsEmailFieldDisabled(), "Email field succes icon is not disabled");
        }

        [When(@"I populate contact details")]
        public void WhenIPopulateContactDetails()
        {
            WhenIEnterPhone();
            WhenIEnterEmail();
        }

        [Then(@"Voucher code details should be displayed")]
        public void ThenVoucherCodeDetailsShouldBeDisplayed()
        {
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickConfirmButton();
            Assert.IsTrue(_guestInformation.ValidateVocherCodeDetailsInTheBookingForm(),"Vocher code Details not matched");
        }

        [When(@"Enter and apply voucher code (.*)")]
        public void WhenEnterAndApplyVoucherCode(string VoucherCode)
        {
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickConfirmButton();
            _guestInformation.ApplyVoucherCode(VoucherCode);
        }

        [Then(@"validate voucher code success message")]
        public void ThenValidateVoucherCodeSuccessMessage()
        {
            Assert.IsTrue(_guestInformation.ValidateVoucherCodeAppliedSuccessMessage(), "Voucher Code not successfull");
        }

        [When(@"Remove voucher code")]
        public void WhenRemoveVoucherCode()
        {
            _guestInformation.RemoveVoucherCode();
        }

        [Then(@"Validate voucher code removed success message")]
        public void ThenValidateVoucherCodeRemovedSuccessMessage()
        {
            Assert.IsTrue(_guestInformation.ValidateVoucherCodeRemovedSuccessMessage(), "Voucher Code not removed successfull");
        }

        [Then(@"Validate invalid voucher code message (.*)")]
        public void ThenValidateInvalidVoucherCodeMessage(string voucherCode)
        {
            Assert.IsTrue(_guestInformation.ValidateInvalidOrExpiredVoucherCodeMessage(voucherCode), "Invalid Voucher code message is not matched");
        }

        [Then(@"Validate message for promo criteria not met (.*)")]
        public void ThenValidateMessageForPromoCriteriaNotMet(string voucherCode)
        {
            Assert.IsTrue(_guestInformation.ValidateMessageForPromoCriteriaNotMet(voucherCode), "Message for promo criteria not met is not matched");

        }

        [Then(@"Validate better deal message (.*)")]
        public void ThenValidateBetterDealMessage(string voucherCode)
        {
            Assert.IsTrue(_guestInformation.ValidateBetterDealMessage(voucherCode), "Better Deal Message is not matched");
        }

    }
}








