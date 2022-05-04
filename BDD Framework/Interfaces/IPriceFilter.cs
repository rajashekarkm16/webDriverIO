using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IPriceFilter
    {
        string GetPriceFilterHeader();
        decimal GetPriceTill();
        decimal GetPriceFrom();
        decimal GetUpToPrice();
        void SetPriceFilter(int xCoordinate,int yCoordinate=0);
    }
}
