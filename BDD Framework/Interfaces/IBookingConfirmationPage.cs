using Dnata.TravelRepublic.MobileWeb.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IBookingConfirmationPage : IBase
    {
        List<string> GetBookingDetails();
        List<string> GetBookingID();
        string GetHotelName();
        int GetHotelStarRating();
        string GetHotelItinerary();
        string GetRoomType(int roomNo = 1);
        string GetBoardType(int roomNo = 1);
        string GetRoomOccupants(int roomNo = 1);
        decimal GetHotelPrice();
        string GetOutboundDepartureAirport();
        string GetOutboundArrivalAirport();
        string GetOutboundDepartureDate();
        string GetOutboundDepartureTime();
        string GetOutboundArrivalTime();
        string GetInboundDepartureAirport();
        string GetInboundArrivalAirport();
        string GetInboundDepartureDate();
        string GetInboundDepartureTime();
        string GetInboundArrivalTime();
        decimal GetFlightsPrice();
        decimal GetBaggagePrice();
        string GetTransferName();
        decimal GetTransferPrice();
        decimal GetTotalPrice();
        decimal GetDepositPaid();
        decimal GetBalanceAmount();
        string GetInsuranceName();
        decimal GetInsurancePrice();
        int GetBalanceAmountInstallments();
        string GetProductSpecificBookingId(ProductType productType);
        decimal GetLocalCharges();
        bool IsHolidayBooking();
    }
}
