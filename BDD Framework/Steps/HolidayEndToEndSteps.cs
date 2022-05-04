using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class HolidayEndToEndSteps
    {
        private readonly ScenarioContext context;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IGuestComponent _guestComponent;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly IBaggage _baggage;
        private readonly ITransfers _transfers;
        private readonly ITravelInsurance _travelInsurance;
        private readonly IHolidayExtrasPage _holidayExtrasPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly IGuestInformation _guestInformation;
        private readonly IChoosePayment _choosePayment;
        private readonly IPaymentPage _paymentPage;
        private readonly IPaypalPage _paypalPage;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;

        public HolidayEndToEndSteps(ICalendarComponent calendarComponent, ILandingPageGuestComponent landingPageGuestComponent, IHotelSearchResults hotelSearchResults, IGuestComponent guestComponent, IHotelEstabPage hotelEstabPage, IFlightSearchResults flightSearchResults, IHolidayExtrasPage holidayExtrasPage, IBaggage baggage, ITransfers transfers, ITravelInsurance travelInsurance, IBookingSummary bookingSummary, IGuestInformation guestInformation, IChoosePayment choosePayment, IPaymentPage paymentPage, IPaypalPage paypalPage, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _calendarComponent = calendarComponent;
            _hotelSearchResults = hotelSearchResults;
            _guestComponent = guestComponent;
            _hotelEstabPage = hotelEstabPage;
            _flightSearchResults = flightSearchResults;
            _baggage = baggage;
            _transfers = transfers;
            _travelInsurance = travelInsurance;
            _holidayExtrasPage = holidayExtrasPage;
            _bookingSummary = bookingSummary;
            _guestInformation = guestInformation;
            _choosePayment = choosePayment;
            _paymentPage = paymentPage;
            _paypalPage = paypalPage;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        [When(@"Complete the (.*) payment holiday booking using (.*) payment from Payment Details")]
        public void WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorizationFromPayment(string paymentPlan, string paymentType)
        {
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking();
                _paypalPage.PerformPaypalPayment();
            }
            else
            {
                _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
                _paymentPage.SetCardNumber(paymentType);
                _paymentPage.SetExpiry();
                _paymentPage.SetSecurityNumber(paymentType);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking();
            }
        }

        [When(@"Populate Holiday Guest details with first name (.*) and last name (.*)")]
        public void WhenPopulateHolidayGuestDetailsWithFirstNameAndLastName(string firstName, string lastName)
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickConfirmButton();
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                    _guestInformation.PopulatePassengersWithNames(_landingPageGuestComponent.GetRoomOccupantDetails(), firstName, lastName);
            }
            else
            {
                    _guestInformation.PopulatePassengersWithNames(_guestComponent.GetRoomOccupantDetails(), firstName, lastName);

            }
            _guestInformation.EnterPhoneNumber("0123 456 789");
            _guestInformation.EnterEmailAddress("rkumar@travelrepublic.co.uk");
        }

        [When(@"Choose the (.*) payment holiday booking using (.*) payment and complete 3DS booking")]
        public void WhenChooseTheFullPaymentHolidayBookingUsingVisaCreditPaymentAndCompleteDSBooking(string paymentPlan, string paymentType)
        {
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));

            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking();
                _paypalPage.PerformPaypalPayment();
            }
            else
            {
                _guestInformation.ProceedToNextPage();
                _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
                _paymentPage.SetCardNumber(paymentType);
                _paymentPage.SetExpiry();
                _paymentPage.SetSecurityNumber(paymentType);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking(true);
            }
        }


        [When(@"Choose the (.*) payment holiday booking using (.*) payment and complete booking")]
        public void WhenChooseTheFullPaymentHolidayBookingUsingVisaCreditPaymentAndCompleteBooking(string paymentPlan, string paymentType)
        {
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));

            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking();
                _paypalPage.PerformPaypalPayment();
            }
            else
            {
                _guestInformation.ProceedToNextPage();
                _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
                _paymentPage.SetCardNumber(paymentType);
                _paymentPage.SetExpiry();
                _paymentPage.SetSecurityNumber(paymentType);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking(false);
            }
        }



        [When(@"Complete the (.*) payment holiday booking using (.*) payment with ThreeDS (.*) authorization")]
        public void WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization(string paymentPlan, string paymentType, bool isThreedS)
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            if(!HelperFunctions.IsDesktop())
                _bookingSummary.ClickConfirmButton();
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                if (isThreedS)
                    _guestInformation.PopulatePassengers(_landingPageGuestComponent.GetRoomOccupantDetails(), true);
                else
                    _guestInformation.PopulatePassengers(_landingPageGuestComponent.GetRoomOccupantDetails(), false);
            }
            else
            {
                if (isThreedS)
                    _guestInformation.PopulatePassengers(_guestComponent.GetRoomOccupantDetails(), true);
                else
                    _guestInformation.PopulatePassengers(_guestComponent.GetRoomOccupantDetails(), false);
            }
            _guestInformation.EnterPhoneNumber("0123 456 789");
            _guestInformation.EnterEmailAddress("bjohnson@travelrepublic.co.uk");
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking();
                _paypalPage.PerformPaypalPayment();
            }
            else
            {
                _guestInformation.ProceedToNextPage();
                _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
                _paymentPage.SetCardNumber(paymentType);
                _paymentPage.SetExpiry();
                _paymentPage.SetSecurityNumber(paymentType);
                _paymentPage.ReenterPaymentDetailsIfError();
                if (HelperFunctions.IsCovidNoticeEnabled())
                    _choosePayment.CheckCovidNoticeCheckbox();
                _guestInformation.ConfirmBooking(isThreedS);
            }
        }

        [When(@"Complete the ThreeDS holiday booking")]
        public void WhenCompleteTheThreeDSHolidayBooking()
        {
            WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization("full", "VisaCredit", true);
        }

        [When(@"Complete the holiday booking")]
        public void WhenCompleteTheHolidayBooking()
        {
            WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization("full", "VisaCredit", false);
        }

        [When(@"Complete the holiday booking with paypal payment")]
        public void WhenCompleteTheHolidayBookingWithPaypalPayment()
        {
            WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization("full", "Paypal", false);
        }

        [When(@"Complete the deposit holiday booking")]
        public void WhenCompleteTheDepositHolidayBooking()
        {
            WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization("deposit", "VisaCredit", false);
        }

        [When(@"Complete the holiday booking with different (.*)")]
        public void WhenCompleteTheHolidayBookingWith(string cardType)
        {
            WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization("full", cardType, false);
        }

        [When(@"Complete the holiday booking with recurring (.*) payment")]
        public void WhenCompleteTheHolidayBookingWithRecurringPayment(string paymentType)
        {
            WhenCompleteTheHolidayBookingUsingPaymentWithThreeDSAuthorization("monthly", "VisaCredit", false);
        }

        [When(@"Continuetobook without any Extras")]
        public void WhenContinuetobookWithoutAnyExtras()
        {
            _holidayExtrasPage.ClickContinueToBookButton();
            if(HelperFunctions.IsTRUK())
                _holidayExtrasPage.ClickConfirmandContinueToBook();
        }
    }
}
