using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface  IMetaPage 
    {
        void NavigateToMetaFranklinPage();
        string GetFranklinPageTitle();
        bool SelectProductOnFranklinPage(string product);
        void SelectMetaEnvironemt(bool isLive);
        void PerformMetaHotelSearch(string channel, int noOfRooms = 1, string occupancy = "2,1,1", int startDaysFromCurrentDate = 20, int duration = 8, string esabIds = MetaData.EstabIds);
        void PerformMetaHolidaySearch(string metaChannel, string departureAirport, string destinationAirport, int countryID, int provinceID, int startDaysFromCurrentDate, int duration, string occupancy);
        decimal GetMetaTotalPrice();
        bool ValidateMetaReferencesInUrl(string channel);
        bool ValidateCookieReferences(string channel);
        bool ValidateMetaImageReference(string channel);
        bool CheckFlightPriceAlertMessage();
        bool ValiidateFlightAndRoomPrice(decimal totalPrice,decimal flightsPrice,decimal hotelPrice, List<RoomOccupantDetails> roomOccupants);
    }
}
