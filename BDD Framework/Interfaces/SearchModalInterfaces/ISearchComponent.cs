using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface ISearchComponent
    {
        string GetDestination();
        void EditPassengers();
        void EditDates();
        void SelectNewSearch();
        void CloseSearchPage();
        void ClickCheckAvailability();
        bool ValidateHolidaySearchModal();
        bool IsDestinationVisible();
        bool IsFlyingFromVisible();
        bool IsPassengersVisible();
        bool IsDatesVisible();
        bool IsNewSearchVisible();
        bool IsCheckHotelAvailabilityVisible();
        string GetSelectedHotelText();
        string GetDatesOnSearchModal();
        void VerifyGuestsOnSearchModal(List<RoomOccupantDetails> roomOccupantDetails);
        void VerifyDatesOnSearchModal(int departure = Constants.DefaultDepartureDays, int duration = Constants.DefaultDuration);
        bool IsDestinationEditable();
        bool IsHotelNameEditable();
        void PopulateDestination(string destination);
        void PopulateAirport(string airport);
        void PopulateAirports(List<string> airportsToSelect);
        bool IsUpdateAvailabilityToggleVisible();
        string GetFlightsFromText();
        void SelectThisHotelOnlyToggleButton();
        bool IsThisHotelToggleSelected();
        void SelectAllHotelsToggleButton();
        bool IsAllHotelsToggleSelected();
        void SelectHolidaysTab();
        void SelectHotelsTab();
        bool IsHotelsTabSelected();
        bool IsHolidaysTabSelected();
        string GetHighlightedDestinationFromAutoCompleter();
        void ClickDestinationField();
        void EnterDestination(string destination);
        bool IsDestinationAutoCompleterDisplayed();
        void ClickFlyingFromField();
        List<string> GetSelectedAirports();
        void ConfirmNewDestination();
    }
}
