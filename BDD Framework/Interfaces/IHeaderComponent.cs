using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IHeaderComponent
    {
        void ClickTRLogo();
        void ClickHelpCentreLogo();
        bool IsATOLLogoVisible();
        void ClickATOLLogo();
        bool IsLinksOnATOLModalEnbaled();
        void ClickMainMenu();
        bool IsMainMenuDisplayed();
        void SelectFromMainMenuOptions(string option);
        string GetMenuTitle();
        void SelectDestinationFromMenu();
        bool IsThirdLevelMenuDisplayed();
        List<string> GetMainMenuOptions();
        string GetSelectedDestination();
        void ClickAccountMenu();
        bool IsUserSignedIn();
        List<string> GetAccountMenuOptions();
        void SelectFromAccountMenuOptions(string option);
        void PopulateAndSignInToAccount();
        bool IsTravelRepublicLogoDisplayed();
        bool IsHelpCentreLogoDisplayed();
        bool IsAccountMenuDisplayed();
        bool IsMenuOptionDisplayed();
        string GetCallToBookNumber();
        bool IsCallToBookNumberAHyperLink();
        bool IsSigninOptionDisplayed();
    }
}
