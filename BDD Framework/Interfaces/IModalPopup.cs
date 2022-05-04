using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IModalPopup
    {
        void ClosePopUp();
        bool IsModalDisplayed();
        string GetModalHeading();
        string GetModalContent();
        List<string> GetAllLinks();
    }
}
