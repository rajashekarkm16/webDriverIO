using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface ICalendarComponent
    {
        DateTime GetDepartureDate();
        DateTime GetReturnDate();
        void SetDepartureDate(DateTime departureDate);
        void SetReturnDate(DateTime returnDate);
        string GetCalendarPageTitle();
        void SelectDates(int departure, int duration);
        void VerifySelectedDates(int departure, int duration);
        void ConfirmTheDates();
        string GetItinerary(int departure, int duration, List<RoomOccupantDetails> roomOccupantDetails);
        string GetItinerary(DateTime departureDate, DateTime returnDate, List<RoomOccupantDetails> roomOccupantDetails);
        void ClickResetLink();
        bool IsSelectedDatesNull();
        void CloseDatePickerModal();
        string GetDateErrorMessage();
        void SelectDepartureDate(int departure);
        bool IsDateEnabled(int noOfDays);
    }
}
