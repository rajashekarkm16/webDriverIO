using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Derwent.UI.Helpers;
using Dnata.Derwent.UI.Models;
using Dnata.TravelRepublic.MobileWeb.UI.Enums;
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
    public sealed class BookingConfirmationPageSteps
    {
        private readonly ScenarioContext context;
        private readonly IBookingConfirmationPage _bookingConfirmationPage;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IGuestComponent _guestComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly ITransfers _transfers;
        private readonly IBaggage _baggage;
        private readonly ITravelInsurance _travelInsurance;
        private readonly IBookingSummary _bookingSummary;
        private readonly IChoosePayment _choosePayment;
        private readonly IGuestInformation _guestInformation;
        private readonly ILandingPageCalendarComponent _landingPageCalendarComponent;

        public BookingConfirmationPageSteps(IBookingConfirmationPage bookingConfirmationPage, ILandingPageCalendarComponent landingPageCalendarComponent, ICalendarComponent calendarComponent, IGuestComponent guestComponent, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, IFlightSearchResults flightSearchResults, ITransfers transfers, IBaggage baggage, ITravelInsurance travelInsurance, IBookingSummary bookingSummary, IChoosePayment choosePayment, IGuestInformation guestInformation, ScenarioContext scenarioContext)
        {
            context = scenarioContext;
            _bookingConfirmationPage = bookingConfirmationPage;
            _calendarComponent = calendarComponent;
            _guestComponent = guestComponent;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _flightSearchResults = flightSearchResults;
            _transfers = transfers;
            _baggage = baggage;
            _travelInsurance = travelInsurance;
            _bookingSummary = bookingSummary;
            _choosePayment = choosePayment;
            _guestInformation = guestInformation;
            _landingPageCalendarComponent = landingPageCalendarComponent;
        }

        [Then(@"Booked hotel details matches")]
        public void ThenBookedItemsMatches()
        {
            if (!HelperFunctions.IsLive())
            {
                Assert.AreEqual(_bookingConfirmationPage.GetHotelName(), _hotelSearchResults.GetHotelInformation().HotelName, "Hotel name validation");
                Assert.AreEqual(_bookingConfirmationPage.GetHotelStarRating(), _hotelSearchResults.GetHotelInformation().StarRating, "Hotel star rating validation");
                string itinerary;
                if(HelperFunctions.IsV3HomepageEnabled())
                    itinerary = string.Format("{0} - {1} ({2} night{3})", _landingPageCalendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
                   _landingPageCalendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
                   _landingPageCalendarComponent.GetReturnDate().Subtract(_landingPageCalendarComponent.GetDepartureDate()).Days,
                   _landingPageCalendarComponent.GetReturnDate().Subtract(_landingPageCalendarComponent.GetDepartureDate()).Days > 1 ? "s" : "");
                else
                    itinerary = string.Format("{0} - {1} ({2} night{3})", _calendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
                    _calendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
                    _calendarComponent.GetReturnDate().Subtract(_calendarComponent.GetDepartureDate()).Days,
                    _calendarComponent.GetReturnDate().Subtract(_calendarComponent.GetDepartureDate()).Days > 1 ? "s" : "");
                Assert.AreEqual(_bookingConfirmationPage.GetHotelItinerary(), itinerary, "Hotel itinerary validation");
                decimal hotelPrice = 0;
                if (HelperFunctions.IsDesktop())
                    hotelPrice = _hotelSearchResults.GetHotelInformation().Price;
                List<RoomInformation> roomInformation = _hotelEstabPage.GetSelectedRoomTypes();
                List<BoardTypeInformation> boardTypeInformation = _hotelEstabPage.GetSelectedBoardTypes();
                for (int room = 1; room <= roomInformation.Count; room++)
                {
                    if (!roomInformation[room - 1].RoomType.Equals(_bookingConfirmationPage.GetRoomType(room), StringComparison.OrdinalIgnoreCase))
                        Assert.Warn("Room type validation failed! Expected:" + roomInformation[room - 1].RoomType + " Actual: " + _bookingConfirmationPage.GetRoomType(room));
                    if (!boardTypeInformation[room - 1].BoardType.Equals(_bookingConfirmationPage.GetBoardType(room), StringComparison.OrdinalIgnoreCase))
                        Assert.Warn("Board type validationn failed! Expected:" + boardTypeInformation[room - 1].BoardType + " Actual: " + _bookingConfirmationPage.GetBoardType(room));
                    hotelPrice += roomInformation[room - 1].RoomPrice + boardTypeInformation[room - 1].BoardTypePrice;
                }
                if ((_bookingConfirmationPage.GetHotelPrice() - hotelPrice) > Constants.MaximumPriceDifference || (hotelPrice - _bookingConfirmationPage.GetHotelPrice()) > Constants.MaximumPriceDifference)
                    Assert.Warn("Hotel price validation!\nExpected: " + hotelPrice + "\nActual: " + _bookingConfirmationPage.GetHotelPrice());
            }            
        }

        [Then(@"Validate that the (.*) mandatory extras are shown on confirmation page")]
        public void ThenValidateThatTheMandatoryExtrasAreShownOnConfirmationPage(int days)
        {
            Assert.IsTrue(_bookingSummary.IsAdditionalPackageInclusionsDisplayed(), "Additional Package Details heading is not displayed");
            var additionalPackageInclusionsLineItems = _bookingSummary.GetAdditionalPackageInclusionsLineItems();

            if (days >= 5 && days <= 7)
                Assert.AreEqual(AdditionalPackageInstructions.Disney7DayTicket, additionalPackageInclusionsLineItems[0]);
            else if (days >= 8 && days <= 14)
                Assert.AreEqual(AdditionalPackageInstructions.Disney14DayTicket, additionalPackageInclusionsLineItems[0]);
            Assert.AreEqual(AdditionalPackageInstructions.CharacterBreakfast, additionalPackageInclusionsLineItems[1]);
            Assert.AreEqual(AdditionalPackageInstructions.FrontRowSpaces, additionalPackageInclusionsLineItems[2]);
        }


        [Then(@"Booked holiday details matches")]
        public void ThenBookedHolidayDetailsMatches()
        {
            if (!HelperFunctions.IsLive())
            {
                Assert.AreEqual(_bookingConfirmationPage.GetHotelName(), _hotelSearchResults.GetHotelInformation().HotelName, "Hotel name validation");
                Assert.AreEqual(_bookingConfirmationPage.GetHotelStarRating(), _hotelSearchResults.GetHotelInformation().StarRating, "Hotel star rating validation");
                string itinerary;
                if (HelperFunctions.IsV3HomepageEnabled())
                {
                    itinerary = string.Format("{0} - {1} ({2} night{3})", _landingPageCalendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
                    _landingPageCalendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
                    _landingPageCalendarComponent.GetReturnDate().Subtract(_landingPageCalendarComponent.GetDepartureDate()).Days,
                    _landingPageCalendarComponent.GetReturnDate().Subtract(_landingPageCalendarComponent.GetDepartureDate()).Days > 1 ? "s" : "");
                }
                else
                {
                    itinerary = string.Format("{0} - {1} ({2} night{3})", _calendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
                    _calendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
                    _calendarComponent.GetReturnDate().Subtract(_calendarComponent.GetDepartureDate()).Days,
                    _calendarComponent.GetReturnDate().Subtract(_calendarComponent.GetDepartureDate()).Days > 1 ? "s" : "");
                }
                    
                if(!_bookingConfirmationPage.GetHotelItinerary().Equals(itinerary))
                    Assert.Warn("Expected: "+ itinerary, "Actual: "+ _bookingConfirmationPage.GetHotelItinerary(), "Hotel itinerary validation");
                decimal holidayPrice = 0;
                if (HelperFunctions.IsDesktop())
                    holidayPrice = _hotelSearchResults.GetHotelInformation().Price;
                List<RoomInformation> roomInformation = _hotelEstabPage.GetSelectedRoomTypes();
                List<BoardTypeInformation> boardTypeInformation = _hotelEstabPage.GetSelectedBoardTypes();
                for (int room = 1; room <= roomInformation.Count; room++)
                {
                    if (!roomInformation[room - 1].RoomType.Equals(_bookingConfirmationPage.GetRoomType(room), StringComparison.OrdinalIgnoreCase))
                        Console.WriteLine("Room type validationn failed! Expected:" + roomInformation[room - 1].RoomType + " Actual: " + _bookingConfirmationPage.GetRoomType(room));
                    if (!boardTypeInformation[room - 1].BoardType.Equals(_bookingConfirmationPage.GetBoardType(room), StringComparison.OrdinalIgnoreCase))
                        Assert.Warn("Board type validationn failed! Expected:" + boardTypeInformation[room - 1].BoardType + " Actual: " + _bookingConfirmationPage.GetBoardType(room));
                    holidayPrice += roomInformation[room - 1].RoomPrice + boardTypeInformation[room - 1].BoardTypePrice;
                }

                FlightInboundOutboudInformationModel flightInboundOutboudInformationModel = _flightSearchResults.GetFlightInformation();
                List<FlightLegInformationModel> flightLegs = flightInboundOutboudInformationModel.flightLeg;
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].DepartureLocation, _bookingConfirmationPage.GetOutboundDepartureAirport(), "OutBound departure location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].ArrivalLocation, _bookingConfirmationPage.GetOutboundArrivalAirport(), "OutBound arrival location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].DepartureTime, _bookingConfirmationPage.GetOutboundDepartureTime(), "OutBound departure time validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].ArrivalTime, _bookingConfirmationPage.GetOutboundArrivalTime(), "OutBound arrival time validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].DepartureLocation, _bookingConfirmationPage.GetInboundDepartureAirport(), "Inbound departure location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].ArrivalLocation, _bookingConfirmationPage.GetInboundArrivalAirport(), "Inbound arrival location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].DepartureTime, _bookingConfirmationPage.GetInboundDepartureTime(), "Inbound departure time validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].ArrivalTime, _bookingConfirmationPage.GetInboundArrivalTime(), "Inbound arrival time validation");

                decimal extrasPrice = 0;
                if (_baggage.IsBaggageAdded())
                {
                    //Baggage Total is not shown in BookingConfirmation Page
                    //ThenBaggageDetailsAreDisplayedInBookingConfirmationPage();
                    //extrasPrice += _bookingConfirmationPage.GetBaggagePrice();
                    extrasPrice += ContextHelper.GetFromContext<decimal>(context, "BaggagePrice");
                    //_baggage.GetBaggageInformation().
                }

                if (_transfers.IsTransferAdded())
                {
                    ThenTransferDetailsAreDisplayedInBookingConfirmationPage();
                    extrasPrice += _bookingConfirmationPage.GetTransferPrice();
                }

                if (_travelInsurance.IsInsuranceAdded())
                {
                    ThenInsuranceDetailsAreDisplayedInBookingConfirmationPage();
                    extrasPrice += _bookingConfirmationPage.GetInsurancePrice();
                }

                decimal adminFees = _choosePayment.GetIsDepositPayment() ? Convert.ToDecimal(_choosePayment.GetBalanceInstallmentsCount() * _choosePayment.GetAdminFees()) : 0;
                if(adminFees == 0)
                    adminFees = _choosePayment.GetIsMonthlyPayment() ? Convert.ToDecimal(_choosePayment.GetBalanceInstallmentsCount() * _choosePayment.GetAdminFees()) : 0;

                //Price Calculation
                if (true)
                {                    
                    if (Math.Abs(holidayPrice + extrasPrice + adminFees - _bookingConfirmationPage.GetTotalPrice()) > Constants.MaximumPriceDifference)
                        Assert.Warn("Total price validation! Expected: " + (holidayPrice + extrasPrice + adminFees) + " Actual: " + _bookingConfirmationPage.GetTotalPrice());
                    if (_bookingConfirmationPage.GetDepositPaid() > 0)
                    {
                        if (!_bookingSummary.GetTotalPaidAmount().Equals(_bookingConfirmationPage.GetDepositPaid()))
                            Assert.Warn("Deposit price validation! Expected: " + _bookingSummary.GetTotalPaidAmount() + " Actual: " + _bookingConfirmationPage.GetDepositPaid());
                        if (!(holidayPrice + extrasPrice + adminFees - _bookingSummary.GetTotalPaidAmount()).Equals(_bookingConfirmationPage.GetBalanceAmount()))
                            Assert.Warn("Balance due amount validation! Expected: " + ((holidayPrice + extrasPrice + adminFees) - _bookingSummary.GetTotalPaidAmount()) + " Actual: " + _bookingConfirmationPage.GetBalanceAmount());
                    }

                }
                else
                {
                    if (holidayPrice + adminFees - _bookingConfirmationPage.GetFlightsPrice() - (_bookingConfirmationPage.GetHotelPrice()) > Constants.MaximumPriceDifference)
                        Assert.Warn("Hotels price validation! Expected: " + (holidayPrice - _bookingConfirmationPage.GetFlightsPrice()) + " Actual: " + _bookingConfirmationPage.GetHotelPrice());
                    if ((holidayPrice + extrasPrice + adminFees - _bookingConfirmationPage.GetTotalPrice()) > Constants.MaximumPriceDifference)
                        Assert.Warn("Total price validation! Expected: " + (holidayPrice + extrasPrice + adminFees) + " Actual: " + _bookingConfirmationPage.GetTotalPrice());
                    if (_bookingConfirmationPage.GetDepositPaid() > 0)
                    {
                        if (!_bookingSummary.GetTotalPaidAmount().Equals(_bookingConfirmationPage.GetDepositPaid()))
                            Assert.Warn("Deposit price validation! Expected: " + _bookingSummary.GetTotalPaidAmount() + " Actual: " + _bookingConfirmationPage.GetDepositPaid());
                        if (!(holidayPrice + extrasPrice + adminFees - _bookingSummary.GetTotalPaidAmount()).Equals(_bookingConfirmationPage.GetBalanceAmount()))
                            Assert.Warn("Balance due amount validation! Expected: " + ((holidayPrice + extrasPrice + adminFees) - _bookingSummary.GetTotalPaidAmount()) + " Actual: " + _bookingConfirmationPage.GetBalanceAmount());
                    }
                }
                    
            }
        }

        [Then(@"Baggage details are displayed in booking confirmation page")]
        public void ThenBaggageDetailsAreDisplayedInBookingConfirmationPage()
        {
            if (!HelperFunctions.IsLive())
            {
                BaggageInformation baggageInformation = _baggage.GetBaggageInformation();
                if ((baggageInformation.PricePerBagEachWay * _baggage.GetBaggageToSelect() * 2) > _bookingConfirmationPage.GetBaggagePrice())
                    Assert.Warn("Baggage Price validation");
            }       
        }

        [Then(@"Transfer details are displayed in booking confirmation page")]
        public void ThenTransferDetailsAreDisplayedInBookingConfirmationPage()
        {
            if (!HelperFunctions.IsLive())
            {
                TransferInformation transferInformation = _transfers.GetTransferInformation();
                if (!transferInformation.TransferName.Contains(_bookingConfirmationPage.GetTransferName().Split(" ")[0]))
                    Assert.Warn("Transfer name validation! \nExpected: " + transferInformation.TransferName + "\nActual: " + (_bookingConfirmationPage.GetTransferName()));
                Assert.AreEqual(transferInformation.TotalPrice, _bookingConfirmationPage.GetTransferPrice(), "Transfer price validation");
            }
        }

        [Then(@"Insurance details are displayed in booking confirmation page")]
        public void ThenInsuranceDetailsAreDisplayedInBookingConfirmationPage()
        {
            if (!HelperFunctions.IsLive())            {
                TravelInsuranceInformation insuranceInformation = _travelInsurance.GetTravelInsuranceInformation();
                Assert.IsTrue(_bookingConfirmationPage.GetInsuranceName().Contains(insuranceInformation.PolicyName.Split(" ")[0]), "Insurance policy name validation");
                if (!insuranceInformation.PolicyName.Contains(_bookingConfirmationPage.GetInsuranceName()))
                    Console.WriteLine("Expected: " + insuranceInformation.PolicyName + " Actual: " + _bookingConfirmationPage.GetInsuranceName() + "Insurance name validation");
                Assert.GreaterOrEqual(Math.Round(insuranceInformation.TotalPrice), _bookingConfirmationPage.GetInsurancePrice(), "Insurance price validation");
            }
        }

        [Then(@"Booked flight details matches")]
        public void ThenBookedFlightDetailsMatches()
        {
            if (!HelperFunctions.IsLive())
            {
                FlightInboundOutboudInformationModel flightInboundOutboudInformationModel = _flightSearchResults.GetFlightInformation();
                List<FlightLegInformationModel> flightLegs = flightInboundOutboudInformationModel.flightLeg;

                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].DepartureLocation, (_bookingConfirmationPage.GetOutboundDepartureAirport(), StringComparison.OrdinalIgnoreCase), "OutBound departure location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].ArrivalLocation, (_bookingConfirmationPage.GetOutboundArrivalAirport(), StringComparison.OrdinalIgnoreCase), "OutBound arrival location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].DepartureTime, (_bookingConfirmationPage.GetOutboundDepartureTime(), StringComparison.OrdinalIgnoreCase), "OutBound departure time validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Outbound].ArrivalTime, (_bookingConfirmationPage.GetOutboundArrivalTime(), StringComparison.OrdinalIgnoreCase), "OutBound arrival time validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].DepartureLocation, (_bookingConfirmationPage.GetInboundDepartureAirport(), StringComparison.OrdinalIgnoreCase), "Inbound departure location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].ArrivalLocation, (_bookingConfirmationPage.GetInboundArrivalAirport(), StringComparison.OrdinalIgnoreCase), "Inbound arrival location validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].DepartureTime, (_bookingConfirmationPage.GetInboundDepartureTime(), StringComparison.OrdinalIgnoreCase), "Inbound departure time validation");
                Assert.AreEqual(flightLegs[(int)FlightLegType.Inbound].ArrivalTime, (_bookingConfirmationPage.GetInboundArrivalTime(), StringComparison.OrdinalIgnoreCase), "Inbound arrival time validation");

                //if (!flightInboundOutboudInformationModel.Price.Equals(_bookingConfirmationPage.GetFlightsPrice()))
                //    Assert.Warn("Flight Price validation! Expected: " + flightInboundOutboudInformationModel.Price + " Actual: " + _bookingConfirmationPage.GetFlightsPrice());
            }
        }

        [Then(@"Validate hotel information, lead contact and price details in database")]
        public void ThenValidateHotelLeadContactAndPriceDetailsInDatabase()
        {
            if (!HelperFunctions.IsLive())
            {
                string bookingid = _bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels);
                LeadContactModel leadContactModel = DBBookingsHelper.GetBookingLeadContactDetails(bookingid);
                BookingInformationModel dBookingInformationModel = DBBookingsHelper.CaptureBookingDetailsFromDB(_bookingConfirmationPage.GetBookingID().Where(id => id.StartsWith("ACM")).FirstOrDefault().Remove(0, 4), false);

                Assert.IsTrue(dBookingInformationModel.BookingTitle.Contains(_hotelSearchResults.GetHotelInformation().HotelName), "Hotel name validation");
                decimal hotelPrice = 0;
                List<RoomInformation> roomInformation = _hotelEstabPage.GetSelectedRoomTypes();
                List<BoardTypeInformation> boardTypeInformation = _hotelEstabPage.GetSelectedBoardTypes();
                for (int room = 1; room <= roomInformation.Count; room++)
                {
                    hotelPrice += roomInformation[room - 1].RoomPrice + boardTypeInformation[room - 1].BoardTypePrice;
                }
                Console.WriteLine(dBookingInformationModel.TotalPaid + " Hotel price paid in DB");
                if(boardTypeInformation.First().LocalTax > 0)
                {
                    if (!(dBookingInformationModel.TotalPaid - (hotelPrice - boardTypeInformation.First().LocalTax) <= Constants.MaximumPriceDifference))
                        Assert.Warn("Total price validation! Expected: " + (hotelPrice - boardTypeInformation.First().LocalTax) + " Actual: " + dBookingInformationModel.TotalPaid);
                }
                else
                {
                    if (!dBookingInformationModel.TotalPaid.Equals(_bookingSummary.GetTotalPaidAmount()))
                        Assert.Warn("Total paid amount validation! Expected: " + dBookingInformationModel.TotalPaid + " Actual: " + _bookingSummary.GetTotalPaidAmount());
                    if (_bookingSummary.GetTotalPaidAmount() < hotelPrice)
                    {
                        if (!dBookingInformationModel.BalanceDue.Equals(hotelPrice - _bookingSummary.GetTotalPaidAmount()))
                            Assert.Warn("Balance due validation! Expected: " + dBookingInformationModel.BalanceDue + " Actual: " + (hotelPrice - _bookingSummary.GetTotalPaidAmount()));
                    }
                }

                string leadContactName = _guestInformation.GetPassengerInformation().First().Title +" "+ _guestInformation.GetPassengerInformation().First().FirstName +" "+ _guestInformation.GetPassengerInformation().First().LastName;
                Assert.AreEqual(leadContactModel.LeadPaxName, leadContactName, "Lead contact details information");
            }            
        }

        [Then(@"Validate hotel information, flights, passengers, lead contact and price details in database")]
        public void ThenValidateHotelInformationFlightsPassengersLeadContactAndPriceDetailsInDatabase()
        {
            ThenValidateHotelLeadContactAndPriceDetailsInDatabase();
            if (!HelperFunctions.IsLive())
            {
                List<PassengersModel> passengersModel = DBBookingsHelper.GetBookingPassengerDetails(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Flights));
                List<PassengerInformation> passengerInformation = _guestInformation.GetPassengerInformation();
                for (int passenger = 0; passenger < passengersModel.Count; passenger++)
                {
                    Assert.AreEqual(passengersModel[passenger].FirstName, passengerInformation[passenger].FirstName);
                    Assert.AreEqual(passengersModel[passenger].LastName, passengerInformation[passenger].LastName);
                    Assert.AreEqual(passengersModel[passenger].DOB, passengerInformation[passenger].DOB);
                }

                FlightInboundOutboudInformationModel flightInformation = _flightSearchResults.GetFlightInformation();
                List<FlightSegmentsModel> flightSegment = DBBookingsHelper.CaptureFlightSegmentFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Flights), false);
                Console.WriteLine(flightSegment);
                for(int flightLeg = 0; flightLeg < 2; flightLeg++)
                {
                    Assert.AreEqual(Convert.ToDateTime(flightInformation.flightLeg[flightLeg].DepartureTime), flightSegment[flightLeg].Depart);
                    Assert.AreEqual(Convert.ToDateTime(flightInformation.flightLeg[flightLeg].ArrivalTime), flightSegment[flightLeg].Arrive);
                    Assert.AreEqual(Convert.ToDateTime(flightInformation.flightLeg[flightLeg].DepartureLocation), flightSegment[flightLeg].DepartAirport);
                    Assert.AreEqual(Convert.ToDateTime(flightInformation.flightLeg[flightLeg].ArrivalLocation), flightSegment[flightLeg].ArrivalAirport);
                }
            }
        }

        [Then(@"Booked item in database has covid protection cover")]
        public void ThenBookedItemInDatabaseHasCovidProtectionCover()
        {
            if (!HelperFunctions.IsLive())
            {
                BookedItemsModel bookedItemsModel = DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false).Where(item => item.Description.Equals("Covid Protection Cover")).FirstOrDefault();
                Assert.AreEqual("Covid Protection Cover", bookedItemsModel.Type, "Covid Protection cover Entry is not in DB");
                Assert.AreEqual(0, bookedItemsModel.TotalClientGross, "Covid cover price validation is not matched");
            }
        }


        [Then(@"Booked item in databse has Promotional Discount")]
        public void ThenBookedItemInDatabseHasPromotionalDiscount()
        {
            if (!HelperFunctions.IsLive())
            {
                BookedItemsModel bookedItemsModel = DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false).Where(item => item.Type.Equals("Promotional Discount")).FirstOrDefault();
                Assert.AreEqual("Promotional Discount", bookedItemsModel.Type, "Promotional Discount cover Entry is not in DB");
                Assert.IsTrue(bookedItemsModel.Description.Contains("AutoTest"), "Promotion Description is matched ");
                Assert.IsTrue(Math.Abs(bookedItemsModel.TotalClientGross) - (Convert.ToDecimal(context["Discount"])) <= 1, "Promotional Discount price validation is not matched");
            }
        }

        [Then(@"Booked item in databse has voucher code Discount")]
        public void ThenBookedItemInDatabseHasVoucherCodeDiscount()
        {
            if (!HelperFunctions.IsLive())
            {
                BookedItemsModel bookedItemsModel = DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false).Where(item => item.Type.Equals("Promotional Discount")).FirstOrDefault();
                Assert.AreEqual("Promotional Discount", bookedItemsModel.Type, "Promotional Discount cover Entry is not in DB");
                Assert.IsTrue(bookedItemsModel.Description.Contains("Derwent entryTest for voucher"), "Voucher Code Discount Description is matched ");
                bookedItemsModel = DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false).Where(item => item.Type.Equals("Accommodation")).FirstOrDefault();
                Assert.IsTrue(((Math.Abs(bookedItemsModel.TotalClientGross)/100)*3) - (Convert.ToDecimal(context["VoucherCodeDiscount"])) <= 1, "Promotional Discount price validation is not matched");
            }
        }

        [Then(@"Booked item in database has Price Adjustment")]
        public void ThenBookedItemInDatabaseHasPriceAdjustment()
        {
            if (!HelperFunctions.IsLive())
            {
                BookedItemsModel bookedItemsModel = DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false).Where(item => item.Type.Equals("Price Adjustment")).FirstOrDefault();
                Assert.AreEqual("Price Adjustment", bookedItemsModel.Type, "Price Adjustment cover Entry is not in DB");
                Assert.IsTrue(bookedItemsModel.Description.Contains("PriceAdj AutoTest"), "Price Adjustment Description is matched ");
                //Capture Accomodation Price
                bookedItemsModel = DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false).Where(item => item.Type.Equals("Accommodation")).FirstOrDefault();
                //Multiply client Exchangerate for IE bookings
                if (!HelperFunctions.IsTRUK())
                    bookedItemsModel.TotalClientGross = Math.Round(bookedItemsModel.TotalClientGross * PromoDetails.ExchangeRate);
                //caluclate PriceAdjusment
                decimal PriceAdjstment = (bookedItemsModel.TotalClientGross * PromoDetails.PriceAdjustmentValue) / 100;
                Console.WriteLine("Accomodation Price in BCP : " + (_bookingConfirmationPage.GetHotelPrice() - _bookingConfirmationPage.GetLocalCharges()));
                Console.WriteLine("Accomodation Price in Database : " + bookedItemsModel.TotalClientGross + "  and  Price Adjustment : " + PriceAdjstment);
                //caluclate Price Adjustment for Accomodation
                Assert.IsTrue(Math.Abs((_bookingConfirmationPage.GetHotelPrice() - _bookingConfirmationPage.GetLocalCharges()) - (bookedItemsModel.TotalClientGross + PriceAdjstment)) <= 1, "Price Adjusment for Accomodation is not matched");
            }
        }

        [Then(@"Validate the (.*) mandatory disney ticket details are saved")]
        public void ThenValidateTheMandatortyDisneyTicketDetailsAreSaved(int days)
        {
            List<BookedItemsModel> listOfItems =  DBBookingsHelper.CaptureBookedItemFromDB(_bookingConfirmationPage.GetProductSpecificBookingId(ProductType.Hotels), false);
            List<RoomInformation> roomInformation = _hotelEstabPage.GetSelectedRoomTypes();
            if (days > 5 && days <= 7)
            {
                Assert.AreEqual(roomInformation.Count, listOfItems.Where(item => item.Description.Equals(AdditionalPackageInstructions.Disney7DayTicket)).Count(),"More than one 7 days ticket got added");
            }
            else if (days > 7 && days <= 14)
            {
                Assert.AreEqual(roomInformation.Count, listOfItems.Where(item => item.Description.Equals(AdditionalPackageInstructions.Disney14DayTicket)).Count(), "More than one 14 days ticket got added");
            }
            Assert.AreEqual(roomInformation.Count, listOfItems.Where(item => item.Description.Equals(AdditionalPackageInstructions.CharacterBreakfast)).Count(), "More than one Character breakfast got added");
            Assert.AreEqual(roomInformation.Count, listOfItems.Where(item => item.Description.Equals(AdditionalPackageInstructions.FrontRowSpaces)).Count(), "More than one Front row spaces got added ");
        }
    }
}
