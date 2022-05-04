using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IFlightSearchResults
    {
        FlightInboundOutboudInformationModel GetFlightInformation();
        int GetFlightToSelect();
        void KeepSelectedFlight();
        int GetTotalFlightCount();
        int GetAdditionalFlightResultsCount();
        void LoadAllFlights();
        string GetBaggageOptionText();
        bool IsSortOptionVisible();
        bool IsAdditionalFlightSearchResultsDisplayed();
        bool IsPreselectedFlightCardDisplayed();
        bool IsShowMoreFlightsButtonVisible();
        decimal GetPreSelectedFlightHolidayPrice();
        void SelectAlternateFlight(int flightToSelect);
        void ViewPreSelectedFlightDetails();
        void ViewAlternateFlightDetails(int flightToSelect);
        int GetShowMoreFlightsLocation();
        void ClickShowMoreFlights();
        void OpenSortOptions();
        bool VerifyAppliedFlightSortOption(string option);
        FlightLegInformationModel GetFlightLegInfo(int flightToSelect, FlightLegType flightType);
        FlightLegInformationModel GetDeaprtureAndArrivalTimefromFlightLegInfo(int flightToSelect, FlightLegType flightType);
        void CaptureFlightInformation(int flightToSelect);
        FlightInboundOutboudInformationModel CaptureAndReturnFlightInformation(int flightToSelect);
        int GetOpenJawFlight();
        void CapturePreSelectedFlightInformation();
        int GetConnectingFlight();
        int GetDirectFlightDifferentCarrier();
        int GetDirectFlightSameCarrier();
        int GetDifferentCarrierForEachFlight();
        void OpenFlightFilters();
        bool IsFlightFiltersVisible();
        int GetFilteredResultsCount();
        bool IsFilteredMessageVisisble();
        bool ValidateDepartureAirport(string depAirport);
        bool IsArrivingOnFutureDays();
        List<FlightInboundOutboudInformationModel> CaptureAllFlightResults();
        bool ValidateFlightResults(List<FlightInboundOutboudInformationModel> oldFlightsInfo, List<FlightInboundOutboudInformationModel> newFlightsInfo);
        string GetFlightAllocationBaggageText();
        string GetFlightAllocationSashText();
        bool CompareFlightLegs(FlightLegInformationModel prevValue, FlightLegInformationModel newValue);
        bool VerifyFlightStopOverInfo(string stopOverInfo);
        string GetStopOverInfoInPreSelctedFlight(FlightType flightType);
        bool VerifyStopOverInfoInAdditionalFlightCards(FlightType flightType);
        bool IsHoldLuggageMessageDisplayedOnAnyFlightCard();
        string GetHoldLuggageMessageonPreSelectedFligthCard();
        bool IsHoldLuggageMessageDisplayedOnPreSelectedFlightCard();
        bool IsAccomadationAdjustmentPopupDisplayed();
        bool AreNumberOfDaysDecreased();
        void AcceptAccomadationDateAdjustment();
    }
}
