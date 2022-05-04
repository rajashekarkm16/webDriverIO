using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class HotelsEndToEndSteps
    {
        private readonly ScenarioContext context;
        private readonly IGuestComponent _guestComponent;
        private readonly IBookingSummary _bookingSummary;
        private readonly IGuestInformation _guestInformation;
        private readonly IChoosePayment _choosePayment;
        private readonly IPaymentPage _paymentPage;
        private readonly IPaypalPage _paypalPage;
        private readonly IHolidayExtrasPage _holidayExtrasPage;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;

        public HotelsEndToEndSteps(IGuestComponent guestComponent, ILandingPageGuestComponent landingPageGuestComponent, IBookingSummary bookingSummary, IGuestInformation guestInformation, IChoosePayment choosePayment, IPaymentPage paymentPage, IPaypalPage paypalPage, IHolidayExtrasPage holidayExtrasPage, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _guestComponent = guestComponent;
            _bookingSummary = bookingSummary;
            _guestInformation = guestInformation;
            _choosePayment = choosePayment;
            _paymentPage = paymentPage;
            _paypalPage = paypalPage;
            _holidayExtrasPage = holidayExtrasPage;
            _landingPageGuestComponent = landingPageGuestComponent;
        }
        
        [When(@"Complete the (.*) payment hotel booking using (.*) payment from Payment step")]
        public void WhenCompleteTheFullPaymentHotelBookingUsingVisaCreditPaymentFromPaymentStep(string paymentPlan, string paymentType)
        {
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
                _guestInformation.ConfirmBooking();
                _paypalPage.PerformPaypalPayment();
            }
            else
            {
                _paymentPage.AutoPopulateAddress("Travel republic, KT2 6NH");
                _paymentPage.SetCardNumber(paymentType);
                _paymentPage.SetExpiry();
                _paymentPage.SetSecurityNumber(paymentType);
                _guestInformation.ConfirmBooking();
            }
        }


        [When(@"Complete the (.*) payment hotel booking using (.*) payment with ThreeDS (.*) authorization")]
        public void WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization(string paymentPlan, string paymentType, bool isThreedS)
        {
            if(!HelperFunctions.IsDesktop())
                _bookingSummary.ClickConfirmButton();
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            if (isThreedS)
                if (HelperFunctions.IsV3HomepageEnabled())
                    _guestInformation.PopulateHotelGuests(_landingPageGuestComponent.GetRoomOccupantDetails(), true);
              else
                    _guestInformation.PopulateHotelGuests(_guestComponent.GetRoomOccupantDetails(), true);
            else
                if(HelperFunctions.IsV3HomepageEnabled())
                  _guestInformation.PopulateHotelGuests(_landingPageGuestComponent.GetRoomOccupantDetails());
                else
                  _guestInformation.PopulateHotelGuests(_guestComponent.GetRoomOccupantDetails());
            _guestInformation.EnterPhoneNumber("0912 456 789");
            _guestInformation.EnterEmailAddress("Autottest@travelrepublic.co.uk");
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
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
                _guestInformation.ConfirmBooking(isThreedS);
            }
        }

        [When(@"Complete the (.*) payment hotel booking using (.*) payment with ThreeDS (.*) authorization and special request")]
        public void WhenCompleteThePaymentHotelBookingUsingPaymentWithThreeDSAuthorizationAndSpecial(string paymentPlan, string paymentType, bool isThreedS)
        {
            _bookingSummary.ClickConfirmButton();
            if (isThreedS)
                if (HelperFunctions.IsV3HomepageEnabled())
                    _guestInformation.PopulateHotelGuests(_landingPageGuestComponent.GetRoomOccupantDetails(), true);
                else
                    _guestInformation.PopulateHotelGuests(_guestComponent.GetRoomOccupantDetails(), true);
            else
                if (HelperFunctions.IsV3HomepageEnabled())
                  _guestInformation.PopulateHotelGuests(_landingPageGuestComponent.GetRoomOccupantDetails());
               else
                  _guestInformation.PopulateHotelGuests(_guestComponent.GetRoomOccupantDetails());
            _guestInformation.EnterPhoneNumber("0123 456 789");
            _guestInformation.EnterEmailAddress("bjohnson@travelrepublic.co.uk");
            _guestInformation.ToggleSpecialRequestsSection();
            _guestInformation.AddSpecialRequestsText();
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
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
                _guestInformation.ConfirmBooking(isThreedS);
            }
        }



        [When(@"Populate Hotel Guest details with first name (.*) and last name (.*)")]
        public void WhenCompleteThePaymentHotelBookingUsingPaymentWithThreeDSWithFirstAndLastName(string firstName , string lastName)
        {
            if (!HelperFunctions.IsDesktop())
            _bookingSummary.ClickConfirmButton();
            _guestInformation.PopulateHotelGuestsWithNames(_landingPageGuestComponent.GetRoomOccupantDetails(),firstName,lastName);
            _guestInformation.EnterPhoneNumber("0123 456 789");
            _guestInformation.EnterEmailAddress("bjohnson@travelrepublic.co.uk");
 
        }

        [When(@"Populate special request")]
        public void WhenPopulateSpecialRequests()
        {
            _guestInformation.ToggleSpecialRequestsSection();
            _guestInformation.AddSpecialRequestsText();
        }

        [When(@"Choose the (.*) payment hotel booking using (.*) payment and complete 3DS booking")]
        public void ChoosePaymentPlanAndComplete3DSBooking(string paymentPlan, string paymentType)
        {
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
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
                _guestInformation.ConfirmBooking(true);
            }
        }

        [When(@"Choose the (.*) payment hotel booking using (.*) payment and complete booking")]
        public void ChoosePaymentPlanAndCompleteBooking(string paymentPlan, string paymentType)
        {
            _choosePayment.ChoosePaymentPlan(paymentPlan.Equals("Deposit", StringComparison.CurrentCultureIgnoreCase), paymentPlan.Equals("monthly", StringComparison.CurrentCultureIgnoreCase));
            if (paymentType.Equals("Paypal", StringComparison.OrdinalIgnoreCase))
            {
                _choosePayment.ChoosePaypal(true);
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
                _guestInformation.ConfirmBooking(false);
            }
        }

        [When(@"Complete the hotel booking")]
        public void WhenCompleteTheHotelBooking()
        {
            WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization("full", "VisaCredit", false);
        }

        [When(@"Complete hotel booking with paypal payment")]
        public void WhenCompleteHotelBookingWithPaypalPayment()
        {
            WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization("full", "Paypal", false);
        }

        [When(@"Complete the hotel booking with deposit payment")]
        public void WhenCompleteTheHotelBookingWithDepositPayment()
        {
            WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization("deposit", "VisaCredit", false);
        }


        [When(@"Complete hotel booking with recurring payment")]
        public void WhenCompleteHotelBookingWithMonthlyPayment()
        {
            WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization("monthly", "VisaCredit", false);
        }


        [When(@"Complete the hotel booking with ThreeDS authorization")]
        public void WhenCompleteTheHotelBookingWithDSAuthorization()
        {
            WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization("full", "VisaCredit", true);
        }

        [When(@"Complete the hotel booking with different (.*)")]
        public void WhenCompleteTheHotelBookingWithDifferent(string cardType)
        {
            WhenCompleteTheHotelBookingUsingPaymentWithThreeDSAuthorization("full", cardType, false);
        }

    }
}
