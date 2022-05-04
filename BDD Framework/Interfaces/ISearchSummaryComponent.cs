using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface ISearchSummaryComponent
    {
        string GetSearchItinerary();
        string GetSearchLocation();
        bool IsEditSearchButtonVisible();
        void WaitUntilPageLoads();
        void EditSearch();
    }
}
