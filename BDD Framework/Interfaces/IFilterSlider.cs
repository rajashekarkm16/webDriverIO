using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IFilterSlider
    {
        string GetOutBoundDepartureTimeValues();
        string GetReturnDepartureTimeValues();
        void SetReturnDepartureTimeMinFilter(int xCoordinate, int yCoordinate = 0);
        void SetReturnDepartureTimeMaxFilter(int xCoordinate, int yCoordinate = 0);
        void SetOutBoundDepartureTimeMaxFilter(int xCoordinate, int yCoordinate = 0);
        void SetOutBoundDepartureTimeMinFilter(int xCoordinate, int yCoordinate = 0);
    }
}
