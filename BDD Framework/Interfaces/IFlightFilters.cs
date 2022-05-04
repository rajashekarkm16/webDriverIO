using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IFlightFilters
    {
        void CloseFlightsFliter();
        bool IsResetAllButtonDisplayed();
        bool IsSameDepartureArrivalAirportFilterDisplayed();
        bool IsDepartureAirportFilterDisplayed();
        bool IsOutboundDepartureTimeFilterDisplayed();
        bool IsReturnDepartureTimeFilterDisplayed();
        bool IsAirlineFilterDisplayed();
        bool IsDepartureAirportsDisplayed();
        int GetViewMatchesCount();
        void SelectMultipleDepartureAirports();
        void ToggleDepartureReturnSameAirport(bool isSelect);
        bool IsSelectedToggle(string expectedColour);
        bool IsFlightFliterModalOpen();
        void ClickViewMatchesButton();
        bool IsAirlinesDisplayed();
        void SelectMultipleAirlines();
        bool IsNoOfStopsFilterDisplayed();
        void ClickResetFilters();
        bool IsNumberOfStopsDisplayed();
        void AddAvailableFilters();
        bool IsAllAvailableFiltersReset();
        void SelectMultipleStops();
        void SelectNoOfStopsFilter(string numberOfStops);
        void SelectDepartureAirportFilter(string filterName);
        int GetNoOfStops(string numberOfStops);
    }
}
