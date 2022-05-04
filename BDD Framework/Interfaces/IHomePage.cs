using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IHomePage
    {
        void NavigateToHotelLandingPage(string url = Constants.HotelLandingPageUKURL);
        void NavigateToHolidaysLandingPage(string url = Constants.HolidaysLandingPageUKURL);
        void NavigateToLandingPage(string url);
        string GetDestination();
        void SetDestination(string destination);
        string GetDepartureAirport();
        void SetDepartureAirport(string departureAirport);
        void AcceptAndCloseCookies();
        void SelectHotelResultsPage();
        void SearchHotels(string destination = Constants.DefaultHotelDestination, int departure = Constants.DefaultDepartureDays, int noOfDays = Constants.DefaultDuration, string guests = Constants.DefaultGuests);
        void NavigateBack();
        void VerifyHomePage();
        bool IsTRLogoVisible();
        void SearchHolidays(string destination = Constants.DefaultHolidayDestination, string airport = Constants.DefaultDepartureAirport, int departure = Constants.DefaultDepartureDays, int noOfDays = Constants.DefaultDuration, string guests = Constants.DefaultGuests);
        void SearchHolidaysForSpecificDates(string destination, string departure, string departureDate, string returnDate, string guests);
        string GetURlOfNewWindow();
        string GetCurrentPageURL();
        bool GetIsHolidayFlow();
        void ClickPromoTermsandConditions();
        bool CheckPromoText(string text);
        void AddDiscountCodeToUrl();
        string GetDiscountCookieValue();
        void CloseNewsletter();
    }
}
