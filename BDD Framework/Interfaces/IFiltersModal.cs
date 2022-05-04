using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IFiltersModal
    {
        // Filter Modal
        string GetFilterModelHeading();
        bool ValidateFiltersModal();
        void WaitForFilterSelectionToLoad();

        // Reset Buttons
        bool IsResetAllButtonVisible();
        void ResetFilter();

        // View Matches
        bool IsViewMatchesButtonVisible();
        int GetViewMatchesResultCount();
        int GetStoredViewMatchesCount();
        void ViewFilterMatches();

        // Total Price Filter
        bool IsHolidayPriceSectionVisible();

        // Star Rating Filter
        bool IsStarRatingSectionVisible();
        int GetAvailableStarRatingOptions();
        string SetStarRatingFilter(int starRatingOption);

        // Customer Rating Filter
        bool IsCustomerRatingSectionVisible();
        int GetAvailableCustomerRatingOptions();
        string SetCustomerRatingFilter(int customerRatingOption);
        void IncreaseCutomerRating(int xCoordinate, int yCoordinate = 0);
        void DecreaseCutomerRating(int xCoordinate, int yCoordinate = 0);

        // Board Type Filter
        bool IsBoardtypeSectionVisible();
        int GetFilterBoardTypesCount();
        List<string> GetFilterBoardTypes();
        void SetBoardTypeFilter(string boardTypeFilterOption);

        // Property Amenities Filter
        bool IsPropertyAmenitiesSectionVisible();
        int GetAvailablePropertyAmenitiesCount();
        List<string> GetAvailablePropertyAmenities();
        void SetPropertyAmenities(string propertyAmenityOption);

        // Holiday Types Filter
        bool IsHolidayTypesSectionVisible();
        int GetAvailableHolidayTypes();
        List<string> GetAvailableHolidayTypeNames();
        void SetHolidayTypes(string holidayTypeOption);
        bool IsViewMatchesButtonEnabled();
        bool IsStarRatingOptionsDisabled();
    }
}
