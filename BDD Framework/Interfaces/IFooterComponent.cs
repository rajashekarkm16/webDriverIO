using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IFooterComponent
    {
        bool IsITAALogoDisplayed();
        bool IsABTALogoDisplayed();
        bool IsCallUsNumberDisplayed();
        bool IsFooterLinksDisplayed();
        bool IsATOLLogoDisplayed();
        bool IsFooterTextDisplayed();
        void ClickTermsOfBusinessLink();
        void ClickStandardCancellationsTermsLink();
        bool IsFooterLinksClickable();

        string GetCallUsNumber();

    }
}
