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
    public class EndToEndSteps
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

        public EndToEndSteps(ICalendarComponent calendarComponent, IHotelSearchResults hotelSearchResults, IGuestComponent guestComponent, IHotelEstabPage hotelEstabPage, IFlightSearchResults flightSearchResults, IHolidayExtrasPage holidayExtrasPage, IBaggage baggage, ITransfers transfers, ITravelInsurance travelInsurance, IBookingSummary bookingSummary, IGuestInformation guestInformation, IChoosePayment choosePayment, IPaymentPage paymentPage, IPaypalPage paypalPage, ScenarioContext injectedContext)
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
        }        

        [When(@"Complete the (.*) holiday booking with (.*)")]
        public void WhenCompleteTheDepositHolidayBookingWithDS(string paymentPlan, string paymentType)
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            _bookingSummary.ClickConfirmButton();
            _guestInformation.PopulatePassengers(_guestComponent.GetRoomOccupantDetails(), true);
            _guestInformation.EnterPhoneNumber("0123 456 789");
            _guestInformation.EnterEmailAddress("bjohnson@travelrepublic.co.uk");
            _guestInformation.ProceedToNextPage();
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit",StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly recurring", StringComparison.CurrentCultureIgnoreCase));
            if (_choosePayment.GetProgressBarCount() == 3)
                _guestInformation.ProceedToNextPage();
            _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
            _paymentPage.SetCardNumber();
            _paymentPage.SetExpiry();
            _paymentPage.SetSecurityNumber();
            _guestInformation.ConfirmBooking(true);
        }        

        [When(@"Complete the (.*) holiday booking using payment type (.*)")]
        public void WhenCompleteTheDepositHolidayBookingWithPaymentType(string paymentPlan, string paymentMode)
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            _bookingSummary.ClickConfirmButton();
            _guestInformation.PopulatePassengers(_guestComponent.GetRoomOccupantDetails(), false);
            _guestInformation.EnterPhoneNumber("0123 456 789");
            _guestInformation.EnterEmailAddress("bjohnson@travelrepublic.co.uk");
            _guestInformation.ProceedToNextPage();
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly recurring", StringComparison.CurrentCultureIgnoreCase));

            if (paymentMode == "paypal")
            {
                _choosePayment.ChoosePaypal(true);
                _guestInformation.ConfirmBooking();
                _paypalPage.PerformPaypalPayment();
            }
            else
            {
                if (_choosePayment.GetProgressBarCount() == 3)
                    _guestInformation.ProceedToNextPage();
                _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
                _paymentPage.SetCardNumber();
                _paymentPage.SetExpiry();
                _paymentPage.SetSecurityNumber();
                _guestInformation.ConfirmBooking();
            }
        }

    }
}
