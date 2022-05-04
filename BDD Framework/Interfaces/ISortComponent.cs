using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface ISortComponent
    {
        void CloseSortOption();
        string GetSelectedSortOption();
        void SelectSortOption(string option);
        void SelectFlightSortOption(string option);
    }
}
