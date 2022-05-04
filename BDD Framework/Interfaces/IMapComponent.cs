using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IMapComponent
    {
        string GetMapPageHeader();
        HotelInformation GetHotelInformation();
        string GetHotelName();
        string GetHotelLocation();
        string GetHotelPrice();
        void SelectHotelFromMaps();
        void SelectHotelPin();
        int GetLocationPins();

    }
}
