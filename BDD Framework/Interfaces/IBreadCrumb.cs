using System.Collections.Generic;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IBreadCrumb
    {
        Dictionary<string,bool> GetBreadCrumbListItemsWithStatus();
        bool IsBreadCrumbActive(string breadCrumbItem);
        Dictionary<string, string> GetLandingPageBreadCrumbLinks();
        bool ValidateLandingPageBreadCrumbNavigation(bool isHoliday);
        int GetBreadCrumbLocation();
    }
}
