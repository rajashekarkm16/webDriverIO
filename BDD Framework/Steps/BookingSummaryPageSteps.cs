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
    public sealed class BookingSummaryPageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IHomePage _homePage;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly IGuestComponent _guestComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private readonly ICalendarComponent _calendarComponent;
        private readonly ILandingPageCalendarComponent _landingPageCalendarComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly IHolidayExtrasPage _holidayExtrasPage;
        private readonly IBaggage _baggage;
        private readonly ITransfers _transfers;
        private readonly ITravelInsurance _travelInsurance;
        private readonly IBookingSummary _bookingSummary;
        private readonly IChoosePayment _choosePayment;

        public BookingSummaryPageSteps(IHomePage homePage, ISearchSummaryComponent searchSummaryComponent, ILandingPageGuestComponent landingPageGuestComponent, ILandingPageCalendarComponent landingPageCalendarComponent, ICalendarComponent calendarComponent, IGuestComponent guestComponent, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, IFlightSearchResults flightSearchResults, IHolidayExtrasPage holidayExtrasPage, IBaggage baggage, ITransfers transfers, ITravelInsurance travelInsurance, IBookingSummary bookingSummary, IChoosePayment choosePayment, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _homePage = homePage;
            _searchSummaryComponent = searchSummaryComponent;
            _calendarComponent = calendarComponent;
            _landingPageCalendarComponent = landingPageCalendarComponent;
            _guestComponent = guestComponent;
            _landingPageGuestComponent = landingPageGuestComponent;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _flightSearchResults = flightSearchResults;
            _holidayExtrasPage = holidayExtrasPage;
            _baggage = baggage;
            _transfers = transfers;
            _travelInsurance = travelInsurance;
            _bookingSummary = bookingSummary;
            _choosePayment = choosePayment;
        }

        [When(@"Check mandatory transfers in booking summary")]
        public void WhenCheckMandatoryTransfersInBookngSummary()
        {
            if (!HelperFunctions.IsDesktop())
                _holidayExtrasPage.ContinueToBook();
            List<TransferInformation> transfers = _transfers.GetMandatoryTransferDetails();
            List<RoomOccupantDetails> roomOccupants;
            if (HelperFunctions.IsV3HomepageEnabled())
                roomOccupants = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                roomOccupants = _guestComponent.GetRoomOccupantDetails();
            decimal transfersTotal = 0;
            for (int i = 1; i <= roomOccupants.Count; i++)
            {
                Assert.AreEqual(_bookingSummary.GetMandatoryTransferName(i), transfers[i - 1].TransferName, "Mandatory transfer name validation");
                int occupants = roomOccupants[i - 1].NoOfAdults + roomOccupants[i - 1].NoOfChildren;
                if (_homePage.GetDepartureAirport() == null)
                    Assert.LessOrEqual((_bookingSummary.GetMandatoryTransferPrice(i)-(transfers[i - 1].PerPersonPrice * occupants)), 1, "Mandatory transfer price validation");
                transfersTotal += (transfers[i - 1].PerPersonPrice * occupants);
            }
            Assert.IsTrue((_bookingSummary.GetTransferPrice() - transfersTotal) < 1, "Total transfers price validation");
        }

        [Then(@"I should get the selected rooms in booking summary page")]
        [When(@"Check the selected rooms and total cost")]
        public void ThenIShouldGetTheSelectedRoomsInBookingSummaryPage()
        {
            decimal price = 0;
            HotelInformation hotelInformation = _hotelSearchResults.GetHotelInformation();
            if (HelperFunctions.IsDesktop())
                price = hotelInformation.Price;
            if (hotelInformation != null)
                Assert.AreEqual(_bookingSummary.GetSelectedHotelName(), hotelInformation.HotelName);
            else
                Assert.AreEqual(_bookingSummary.GetSelectedHotelName(), _homePage.GetDestination());
            string itinerary;
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                itinerary = string.Format("{0} - {1} ({2} night{3})",
               _landingPageCalendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
               _landingPageCalendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
               _landingPageCalendarComponent.GetReturnDate().Subtract(_landingPageCalendarComponent.GetDepartureDate()).Days,
               _landingPageCalendarComponent.GetReturnDate().Subtract(_landingPageCalendarComponent.GetDepartureDate()).Days > 1 ? "s" : "");
            }
            else {
                itinerary = string.Format("{0} - {1} ({2} night{3})",
               _calendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
               _calendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
               _calendarComponent.GetReturnDate().Subtract(_calendarComponent.GetDepartureDate()).Days,
               _calendarComponent.GetReturnDate().Subtract(_calendarComponent.GetDepartureDate()).Days > 1 ? "s" : "");
            }
            Assert.AreEqual(itinerary, _bookingSummary.GetSearchItinerary());
            int noOfRooms = 0;
            if(HelperFunctions.IsV3HomepageEnabled())
                noOfRooms = _landingPageGuestComponent.GetRoomOccupantDetails().Count;
            else
                noOfRooms = _guestComponent.GetRoomOccupantDetails().Count;

            List<RoomInformation> lRoomInformation = _hotelEstabPage.GetSelectedRoomTypes();
            List<BoardTypeInformation> lBoardTypeInformation = _hotelEstabPage.GetSelectedBoardTypes();
            for (int i = 1; i <= noOfRooms; i++)
            {
                var roomInfo = lRoomInformation[i - 1];
                var boardTypeInfo = lBoardTypeInformation[i - 1];
                if (!roomInfo.RoomType.ToLower().Equals(_bookingSummary.GetRoomType(i).ToLower()))
                    Console.WriteLine("Expected: " + roomInfo.RoomType + " Actual: " + _bookingSummary.GetRoomType(i));
                //Assert.Warn("Expected: " + roomInfo.RoomType + " Actual: " + _bookingSummary.GetRoomType(i));
                if (!boardTypeInfo.BoardType.ToLower().Contains(_bookingSummary.GetBoardType(i).ToLower()))
                    Assert.Warn("Expected: " + boardTypeInfo.BoardType + " Actual: " + _bookingSummary.GetBoardType(i));
                if (boardTypeInfo.LocalTax > 0)
                    Assert.IsTrue(boardTypeInfo.LocalTax - (Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(_bookingSummary.GetLocalTaxes()))) < 1);
                price += roomInfo.RoomPrice + boardTypeInfo.BoardTypePrice;
            }
            List<RoomOccupantDetails> roomOccupants = new List<RoomOccupantDetails>();
            if (HelperFunctions.IsV3HomepageEnabled())
                roomOccupants = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                roomOccupants = _guestComponent.GetRoomOccupantDetails();
            if (_transfers.GetMandatoryTransferDetails() != null)
            {
                List<TransferInformation> transfers = _transfers.GetMandatoryTransferDetails();
                for (int i = 1; i <= roomOccupants.Count; i++)
                {
                    int occupants = roomOccupants[i].NoOfAdults + roomOccupants[i].NoOfChildren;
                    price += (transfers[i - 1].PerPersonPrice * occupants);
                }
            }
            _scenarioContext.Add("Price", price);
            _scenarioContext.Add("DepositPrice", _bookingSummary.GetPayNowCost());
            _choosePayment.ChoosePaymentPlan();
            if ((_bookingSummary.GetTotalPrice() - price) > Constants.MaximumPriceDifference || (price - _bookingSummary.GetTotalPrice()) > Constants.MaximumPriceDifference)
                Assert.Warn("Expected: " + price + " Actual: " + _bookingSummary.GetTotalPrice());

            Console.WriteLine("Selected Room type and board type matches in Booking summary page");
            _bookingSummary.CapturePayabaleAmount();
        }

        [When(@"Check selected holiday products and total price")]
        public void WhenCheckSelectedHolidayProductsAndTotalPrice()
        {
            if (!HelperFunctions.IsDesktop())
                _holidayExtrasPage.ContinueToBook();
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            FlightInboundOutboudInformationModel flightInformation = _flightSearchResults.GetFlightInformation();
            List<FlightLegInformationModel> flightLegs = flightInformation.flightLeg;
            int outBoundArrivalTime = Convert.ToInt32(flightLegs[0].ArrivalTime.Split(":")[0]);
            DateTime checkInDate;
            if(HelperFunctions.IsV3HomepageEnabled())
                checkInDate = _landingPageCalendarComponent.GetDepartureDate();
            else
                checkInDate = _calendarComponent.GetDepartureDate();
            //if(flightLegs[0].AdditionalDays > 0)
            //{
            //    if(outBoundArrivalTime >= Constants.OutBoundTimeThreshold)
            //        checkInDate = checkInDate.AddDays(1);
            //}
            int inBoundDepartureTime = Convert.ToInt32(flightLegs[1].DepartureTime.Split(":")[0]);
            DateTime checkOutDate;
            if(HelperFunctions.IsV3HomepageEnabled())
                checkOutDate = _landingPageCalendarComponent.GetReturnDate();
            else
                checkOutDate = _calendarComponent.GetReturnDate();
            if (inBoundDepartureTime < Constants.InBoundTimeThreshold)
                checkOutDate = checkOutDate.AddDays(-1);

            decimal hotelPrice = 0;
            HotelInformation hotelInformation = _hotelSearchResults.GetHotelInformation();
            Assert.AreEqual(_bookingSummary.GetSelectedHotelName(), hotelInformation.HotelName);
            string hotelItinerary = string.Format("{0} - {1} ({2} night{3})",
                checkInDate.ToString("ddd dd MMM yyyy"),
                checkOutDate.ToString("ddd dd MMM yyyy"),
                checkOutDate.Subtract(checkInDate).Days,
                checkOutDate.Subtract(checkInDate).Days > 1 ? "s" : "");
            if (!hotelItinerary.Equals(_bookingSummary.GetSearchItinerary()))
                Assert.Warn("Expected: " + hotelItinerary + "\nActual: " + _bookingSummary.GetSearchItinerary());
            int noOfRooms;
            if(HelperFunctions.IsV3HomepageEnabled())
                noOfRooms = _landingPageGuestComponent.GetRoomOccupantDetails().Count;
            else
                noOfRooms = _guestComponent.GetRoomOccupantDetails().Count;
            List<RoomInformation> lRoomInformation = _hotelEstabPage.GetSelectedRoomTypes();
            List<BoardTypeInformation> lBoardTypeInformation = _hotelEstabPage.GetSelectedBoardTypes();
            for (int i = 1; i <= noOfRooms; i++)
            {
                var roomInfo = lRoomInformation[i - 1];
                var boardTypeInfo = lBoardTypeInformation[i - 1];
                if (!roomInfo.RoomType.ToLower().Equals(_bookingSummary.GetRoomType(i).ToLower()))
                    Console.WriteLine("Expected: " + roomInfo.RoomType + " Actual: " + _bookingSummary.GetRoomType(i));
                if (!boardTypeInfo.BoardType.ToLower().Equals(_bookingSummary.GetBoardType(i).ToLower()))
                    Assert.Warn("Expected: " + boardTypeInfo.BoardType + " Actual: " + _bookingSummary.GetBoardType(i));
                if (boardTypeInfo.LocalTax > 0)
                    Assert.IsTrue(boardTypeInfo.LocalTax - (Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(_bookingSummary.GetLocalTaxes()))) < 1);
                hotelPrice = hotelPrice + roomInfo.RoomPrice + boardTypeInfo.BoardTypePrice;
            }
            if (HelperFunctions.IsDesktop())
                hotelPrice += hotelInformation.Price;

            WhenCheckSelectedFlightAndPriceInBookingSummary();

            decimal extrasPrice = 0;

            if (_baggage.IsBaggageAdded())
            {
                WhenCheckSelectedBaggageAndPriceInBookingSummary();
                extrasPrice += _bookingSummary.GetBaggagePrice();
                ContextHelper.SaveToContext<decimal>(_scenarioContext,"BaggagePrice",extrasPrice);                
            }

            if (_transfers.IsTransferAdded())
            {
                decimal transferPrice=0;
                WhenCheckSelectedTransfersAndPriceInBookingSummary();
                transferPrice = _bookingSummary.GetTransferPrice();
                extrasPrice += transferPrice;
                ContextHelper.SaveToContext<decimal>(_scenarioContext, "TransferPrice", transferPrice);
            }

            if (_travelInsurance.IsInsuranceAdded())
            {
                decimal insurancePrice = 0;
                WhenCheckSelectedTravelInsuranceAndPriceInBookingSummary();
                insurancePrice = _bookingSummary.GetInsurancePrice();
                extrasPrice += insurancePrice;
                ContextHelper.SaveToContext<decimal>(_scenarioContext, "InsurancePrice", insurancePrice);
            }
            _choosePayment.ChoosePaymentPlan();
            decimal holidayPrice;
            if(HelperFunctions.IsV3HomepageEnabled())
                holidayPrice = hotelPrice + (_flightSearchResults.GetFlightInformation().Price * _landingPageGuestComponent.GetNonInfantOccupants()) + extrasPrice;
            else
                holidayPrice = hotelPrice + (_flightSearchResults.GetFlightInformation().Price * _guestComponent.GetNonInfantOccupants()) + extrasPrice;
            if (_bookingSummary.GetTotalPrice() - holidayPrice > Constants.MaximumPriceDifference | holidayPrice - _bookingSummary.GetTotalPrice() > Constants.MaximumPriceDifference)
                Assert.Warn("Total price validation! Expected: " + (hotelPrice + (_flightSearchResults.GetFlightInformation().Price * _landingPageGuestComponent.GetNonInfantOccupants()) + extrasPrice).ToString() + "\nActual: " + _bookingSummary.GetTotalPrice());
            _bookingSummary.CapturePayabaleAmount();
        }

        [When(@"Check selected flight and price in booking summary")]
        public void WhenCheckSelectedFlightAndPriceInBookingSummary()
        {
            FlightInboundOutboudInformationModel flightInformation = _flightSearchResults.GetFlightInformation();
            List<FlightLegInformationModel> flightLegs = flightInformation.flightLeg;

            Assert.AreEqual(string.Format("{0} - {1}",
                flightLegs[(int)FlightType.Outbound].DepartureLocation,
                flightLegs[(int)FlightType.Outbound].ArrivalLocation),
                _bookingSummary.GetOutBoundAirportDetails(), "Outbound airports validation");
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                Assert.AreEqual(string.Format("{0} ({1} - {2})",
                _landingPageCalendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
                flightLegs[0].DepartureTime,
                flightLegs[0].ArrivalTime), _bookingSummary.GetOutBoundFlightItinerary(), "Outbound departure itinerary");
            }
            else
            {
                Assert.AreEqual(string.Format("{0} ({1} - {2})",
                _calendarComponent.GetDepartureDate().ToString("ddd dd MMM yyyy"),
                flightLegs[0].DepartureTime,
                flightLegs[0].ArrivalTime), _bookingSummary.GetOutBoundFlightItinerary(), "Outbound departure itinerary");
            }
            Assert.AreEqual(string.Format("{0} - {1}",
                flightLegs[(int)FlightType.Return].DepartureLocation,
                flightLegs[(int)FlightType.Return].ArrivalLocation),
                _bookingSummary.GetInBoundAirportDetails(), "Inbound airports validation");
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                Assert.AreEqual(string.Format("{0} ({1} - {2})",
                _landingPageCalendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
                flightLegs[1].DepartureTime,
                flightLegs[1].ArrivalTime), _bookingSummary.GetInBoundFlightItinerary(), "Inbound departure itinerary");
            }
            else
            {
                Assert.AreEqual(string.Format("{0} ({1} - {2})",
                _calendarComponent.GetReturnDate().ToString("ddd dd MMM yyyy"),
                flightLegs[1].DepartureTime,
                flightLegs[1].ArrivalTime), _bookingSummary.GetInBoundFlightItinerary(), "Inbound departure itinerary");
            }
        }

        [When(@"Check package price")]
        public void WhenCheckPackagePrice()
        {
            var roomInfo = _hotelEstabPage.GetSelectedRoomTypes();
            var boardInfo = _hotelEstabPage.GetSelectedBoardTypes();
            decimal packagePrice = 0;
            foreach (var room in roomInfo)
            {
                packagePrice += room.RoomPrice;
            }
            foreach (var board in boardInfo)
            {
                packagePrice += board.BoardTypePrice;
            }
            packagePrice += _flightSearchResults.GetFlightInformation().Price;
            if (_bookingSummary.GetPackagePrice() - packagePrice > roomInfo.Count)
                Assert.Warn("Package price validation! Expected: " + packagePrice + " | Actual: " + _bookingSummary.GetPackagePrice());
        }

        [When(@"Check selected baggage and price in booking summary")]
        public void WhenCheckSelectedBaggageAndPriceInBookingSummary()
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            BaggageInformation baggageInformation = _baggage.GetBaggageInformation();
            if (_bookingSummary.GetBaggagePrice() != baggageInformation.PricePerBagEachWay * _baggage.GetBaggageToSelect() * _flightSearchResults.GetFlightInformation().flightLeg.Count)
                Assert.Warn("Baggage price does not match in booking summary! Expected: " + (baggageInformation.PricePerBagEachWay * (_baggage.GetBaggageToSelect() * _flightSearchResults.GetFlightInformation().flightLeg.Count)).ToString() + " Actual: " + _bookingSummary.GetBaggagePrice());
        }

        [When(@"Check selected transfers and price in booking summary")]
        [Then(@"Check selected transfer and price is updated in booking summary")]
        public void WhenCheckSelectedTransfersAndPriceInBookingSummary()
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            TransferInformation transferInformation = _transfers.GetTransferInformation();
            if(_bookingSummary.GetTransferName().Split("x ")[1].Contains(" "))
            {
                if (!transferInformation.TransferName.Contains(_bookingSummary.GetTransferName().Split("x ")[1].Split(" ")[0]))
                    Assert.Warn("Ëxpected: " + transferInformation.TransferName + "\nActual: " + _bookingSummary.GetTransferName().Split("x ")[1]);
            }
            else if(!transferInformation.TransferName.Contains(_bookingSummary.GetTransferName().Split("x ")[1]))
                Assert.Warn("Ëxpected: " + transferInformation.TransferName + "\nActual: " + _bookingSummary.GetTransferName().Split("x ")[1]);

            if (!_bookingSummary.GetTransferPrice().Equals(transferInformation.TotalPrice))
                Assert.Warn("Transfer price does not match in booking summary! Expected: " + transferInformation.TotalPrice + " Actual: " + _bookingSummary.GetTransferPrice());
        }

        [When(@"Check selected travel insurance and price in booking summary")]
        public void WhenCheckSelectedTravelInsuranceAndPriceInBookingSummary()
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            TravelInsuranceInformation travelInsuranceInformation = _travelInsurance.GetTravelInsuranceInformation();
            Assert.AreEqual(_bookingSummary.GetInsurancePolicyName().Split(" x ")[1].Trim(), travelInsuranceInformation.PolicyName, "Travel insurance name validation");
            if (!_bookingSummary.GetInsurancePrice().Equals(travelInsuranceInformation.TotalPrice))
                Assert.Warn("Insurance price does not match in booking summary! Expected: " + travelInsuranceInformation.TotalPrice + " Actual: " + _bookingSummary.GetInsurancePrice());
        }

        [When(@"Check selected travel insurance and price is updated in booking summary")]
        [Then(@"Check selected travel insurance and price is updated in booking summary")]
        public void ThenCheckSelectedTravelInsuranceAndPriceIsUpdatedInBookingSummary()
        {
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickBookingSummaryButton();
            TravelInsuranceInformation travelInsuranceInformation = _travelInsurance.GetTravelInsuranceInformation();
            Assert.AreEqual(_bookingSummary.GetInsurancePolicyName().Split(" x ")[1].Trim(), travelInsuranceInformation.PolicyName, "Travel insurance name validation");
            if (!_bookingSummary.GetInsurancePrice().Equals(travelInsuranceInformation.TotalPrice))
                Assert.Warn("Insurance price does not match in booking summary! Expected: " + travelInsuranceInformation.TotalPrice + " Actual: " + _bookingSummary.GetInsurancePrice());
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.CloseBookingSummaryModal();
        }

        [Then(@"Travel insurance should be removed from booking summary")]
        public void ThenTravelInsuranceShouldBeRemovedFromBookingSummary()
        {
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickBookingSummaryButton();
            Assert.IsFalse(_bookingSummary.IsInsuranceSectionDisplayed(),"Travel insurance is displayed on booking summary");
        }

        [Then(@"Transfers should be removed from booking summary")]
        public void ThenTransfersShouldBeRemovedFromBookingSummary()
        {
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickBookingSummaryButton();
            Assert.IsFalse(_bookingSummary.IsTransferSectionDisplayed(), "Transfers should not be displayed on booking summary");
        }

        [Then(@"Flight allocation baggage details is displayed in booking summary")]
        public void ThenFlightAllocationBaggageDetailsIsDisplayedInBookingSummary()
        {
            _holidayExtrasPage.ProceedIfExtrasPageIsVisible();
            Assert.AreEqual(string.Format("{0} {1}", _guestComponent.GetNonInfantOccupants().ToString(), "x 23kg hold luggage"), _bookingSummary.GetFlightAllocationBaggageInfo(), "Baggage weight is not matching");
            Assert.AreEqual(string.Format("{0}{1} {2}", "Save £", Constants.FlightAllocationBaggagePrice * _guestComponent.GetNonInfantOccupants(), "today!"), _bookingSummary.GetFlightAllocationBaggageCost(), "Baggage price is not matching");
            Assert.AreEqual(0, _bookingSummary.GetBaggagePrice(), "Baggage cost is added to the total");
        }

        [Then(@"Discount should be displayed on booking summary")]
        public void ThenDiscountShouldBeDisplayedOnBookingsummary()
        {
            // Need to Test discount is displayed or not when IsAllHolidayPackage() is false 
            //if(!HelperFunctions.IsAllHolidayPackage() && _bookingSummary.IsPackage())
             //   Assert.Warn("Discount is not displyed for package journey")
            Assert.IsTrue(_bookingSummary.GetDiscountPrice().ToString() != "", "Discount Price is not Displayed on BoardType");
            Assert.IsTrue((Convert.ToDecimal(_scenarioContext["Discount"])-Convert.ToDecimal(_bookingSummary.GetDiscountPrice().ToString().Replace("-", "")))<=1, "Discount Price is not Matching");          
        }

        [Then(@"Voucher code discount has to updated in booking summary")]
        public void ThenVoucherCodeDiscountHasToUpdatedInBasket()
        {
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.ClickBookingSummaryButton();
            _scenarioContext.Add("VoucherCodeDiscount", Convert.ToDecimal(_bookingSummary.GetDiscountPrice().ToString().Replace("-", "")));            
            Assert.IsTrue((Math.Round((_bookingSummary.GetRoomsTotalPrice() / 100) * 3)-Convert.ToDecimal(_bookingSummary.GetDiscountPrice().ToString().Replace("-",""))<=1), "Discount Value is not macthed in Booking Summary");
            if (!HelperFunctions.IsDesktop())
                _bookingSummary.NavigateBackTOBookingSummaryFromGuestInfoPage();
        }

        [Then(@"Wasnow price on booking summary should be matched")]
        public void ThenWasnowPriceOnBookingSummaryShouldBeMatched()
        {
            //Comparing NowPrice of EstabPage and BookingSummary  
            if (_bookingSummary.IsAdminFeeVisible())
                Assert.IsTrue(((Convert.ToDecimal(_scenarioContext["NowPrice"]) + _bookingSummary.GetAdminFee()) - Convert.ToDecimal(_bookingSummary.GetTotalPrice()))<=1, "Total Price to Pay Price is not Matching");
            else
                Assert.IsTrue((Convert.ToDecimal(_scenarioContext["NowPrice"])- Convert.ToDecimal(_bookingSummary.GetTotalPrice()))<=1, "Total Price to Pay Price is not Matching");

            //Comparing WasPrice of EstabPage and BookingSummary 
            if (_bookingSummary.IsPackage())
                Assert.AreEqual(Convert.ToDecimal(_scenarioContext["WasPrice"]), _bookingSummary.CalculateWasTotalPrice()+ Convert.ToDecimal(_scenarioContext["Discount"]), "Package Total Price is not Matching");
            else
                Assert.IsTrue((Convert.ToDecimal(_scenarioContext["WasPrice"])- _bookingSummary.CalculateWasTotalPrice())<=1, "Rooms Total Price is not Matching");
        }

        [Then(@"Covid cover plus entry should be in booking summary")]
        public void ThenCovidcoverplusEntryShouldBeInBookingsummary()
        {
            Assert.AreEqual(CovidCover.CovidCoverPrice, _bookingSummary.GetCovidCoverPlusPrice(), "Covid Cover Plus Price is not matched");
        }

        [Then(@"Additional Package Details should be shown on booking summary for (.*) days booking")]
        public void ThenAdditionalPackageDetailsShouldBeShownOnBookingSummaryForDaysBooking(int days)
        {
            Assert.IsTrue(_bookingSummary.IsAdditionalPackageInclusionsDisplayed(), "Additional Package Details heading is not displayed");
            var additionalPackageInclusionsLineItems = _bookingSummary.GetAdditionalPackageInclusionsLineItems();

            if (days >= 5 && days <= 7)
                Assert.AreEqual(AdditionalPackageInstructions.Disney7DayTicket, additionalPackageInclusionsLineItems[0]);
            else if (days >= 8 && days <= 14)
                Assert.AreEqual(AdditionalPackageInstructions.Disney14DayTicket, additionalPackageInclusionsLineItems[0]);

            Assert.AreEqual(AdditionalPackageInstructions.CharacterBreakfast, additionalPackageInclusionsLineItems[1]);
            Assert.AreEqual(AdditionalPackageInstructions.FrontRowSpaces, additionalPackageInclusionsLineItems[2]);

            Dictionary<string,string>  PopupContents= _bookingSummary.GetAdditionalPackageInclusionsLineItemsPopupTitleAndContent();

            foreach (KeyValuePair<string,string> popupContent in PopupContents)
            {
                switch (popupContent.Key)
                {
                    case AdditionalPackageInstructions.Disney7DayTicket:
                        {
                            Assert.AreEqual(AdditionalPackageInstructions.Disney7DayTickePopupMessage, popupContent.Value);
                            break;
                        }

                    case AdditionalPackageInstructions.Disney14DayTicket:
                        {
                            Assert.AreEqual(AdditionalPackageInstructions.Disney14DayTickePopupMessage, popupContent.Value);
                            break;
                        }
                    case AdditionalPackageInstructions.FrontRowSpaces:
                        {
                            Assert.AreEqual(AdditionalPackageInstructions.DisneyParadise, popupContent.Value);
                            break;
                        }
                    case AdditionalPackageInstructions.CharacterBreakfast:
                        {
                            Assert.AreEqual(AdditionalPackageInstructions.CharacterBreakfastPopupMessage, popupContent.Value);
                            break;
                        }
                }
            }
        }


        [Then(@"Additional Package Details should not be shown on booking summary for (.*) days booking")]
        public void ThenAdditionalPackageDetailsShouldNotBeShownOnBookingSummaryForDaysBooking(int days)
        {
            Assert.IsTrue(_bookingSummary.IsAdditionalPackageInclusionsDisplayed(), "Additional Package Details heading is not displayed");
            var additionalPackageInclusionsLineItems = _bookingSummary.GetAdditionalPackageInclusionsLineItems();
            Assert.IsFalse(additionalPackageInclusionsLineItems.Contains(AdditionalPackageInstructions.Disney7DayTicket));
            Assert.IsFalse(additionalPackageInclusionsLineItems.Contains(AdditionalPackageInstructions.Disney14DayTicket));
        }

        [Then(@"Check Free Cancellation Date on BookingSummary")]
        public void ThenCheckFreeCancellationDateOnBookingSummary()
        {
            String expectedDateValue = _bookingSummary.GetFreeHotelCancellationText();
            Assert.IsTrue(expectedDateValue.Contains(_scenarioContext["FreeCancellationDateValue"].ToString()), "Hotel Free Cancellation date is not matched on Booking Summary");
        }

       
        [Then(@"Check Free Cancellation Date for MultiRoom on BookingSummary")]
        public void ThenCheckFreeCancellationDateForMultiRoomOnBookingSummary()
        {
            //Room1
            String expectedDateValue = _bookingSummary.GetFreeHotelCancellationText(1);
            Assert.IsTrue(expectedDateValue.Contains(_scenarioContext["ContractFreeCancellationDate"].ToString()), "Hotel Free Cancellation date is not matched on Booking Summary");

            //Room2
            expectedDateValue = _bookingSummary.GetFreeHotelCancellationText(2);
            Assert.IsTrue(expectedDateValue.Contains(_scenarioContext["ExtranetFreeCancellationDate"].ToString()), "Hotel Free Cancellation date is not matched on Booking Summary");
        }

    }
}
