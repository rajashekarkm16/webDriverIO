using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IBookingSummary : IBase
    {
        decimal? GetTotalPaidAmount();
        void SetTotalPayableAmount(decimal value);
        void CapturePayabaleAmount();
        string GetSelectedHotelName();
        string GetSearchItinerary();
        void VerifyItineraryDetails();
        void ClickConfirmButton();
        string GetRoomType(int roomNo);
        string GetBoardType(int roomNo);
        string GetOccupants(int roomNo);
        decimal GetRoomsTotalPrice();
        decimal CalculateWasTotalPrice();
        decimal GetATOLProtectionPrice();
        decimal GetFlightTotalPrice();
        decimal GetTotalPrice();
        decimal GetDiscountPrice();
        bool IsAdminFeeVisible();
        decimal GetAdminFee();
        bool IsLocalTaxVisible();
        string GetLocalTaxes();
        decimal GetPayNowCost();
        void ClickChangeRoom(int roomNo);
        void ClickRoomInformationIcon(int roomNo);
        string GetRoomInformationModalContent();
        void CloseRoomInformationModal();
        decimal GetPriceInHeader();
        bool IsBookingSummaryHeaderDisplayed();
        bool IsTotalPriceDisplayedOnSummaryHeadder();
        void ClickBookingSummaryButton();
        string GetOutBoundAirportDetails();
        string GetOutBoundDepartureAirport();
        string GetOutBoundArrivalAirport();
        string GetOutBoundDepartureDate();
        string GetOutBoundDepartureTime();
        string GetOutBoundArrivalTime();
        string GetOutBoundFlightItinerary();
        string GetInBoundAirportDetails();
        string GetInBoundDepartureAirport();
        string GetInBoundArrivalAirport();
        string GetInBoundDepartureDate();
        string GetInBoundDepartureTime();
        string GetInBoundArrivalTime();
        string GetInBoundFlightItinerary();
        decimal GetBaggagePrice();
        string GetTransferName();
        decimal GetTransferPrice();
        string GetInsurancePolicyName();
        decimal GetInsurancePrice();
        bool IsPackage();
        decimal GetPackagePrice();
        string GetMandatoryTransferName(int transferOption = 1);
        decimal GetMandatoryTransferPrice(int transferOption = 1);
        void CloseBookingSummaryModal();
        bool IsInsuranceSectionDisplayed();
        string GetFlightAllocationBaggageInfo();
        string GetFlightAllocationBaggageCost();
        decimal GetCovidCoverPlusPrice();
        bool IsTransferSectionDisplayed();
        bool IsAdditionalPackageInclusionsDisplayed();
        List<string> GetAdditionalPackageInclusionsLineItems();
        Dictionary<string, string> GetAdditionalPackageInclusionsLineItemsPopupTitleAndContent();
        bool IsOnBookingSummary();
        string GetFreeHotelCancellationText(int roomType=1);
        bool IsBookingSummaryPage();
        void NavigateBackTOBookingSummaryFromGuestInfoPage();
    }
}
