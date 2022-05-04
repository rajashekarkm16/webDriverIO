using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.Automation.BDDFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Threading;
using Dnata.Automation.BDDFramework.Configuration;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class HeaderComponent : MobileBasePage, IHeaderComponent
    {
        #region[Constructor]
        private readonly IAtWebDriver _webDriver;
        

        public HeaderComponent(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy ByTRLogo => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-app-header')]//a[@href='/']/*[local-name()='svg']");
        private AtWebElement TRLogo => _webDriver.FindElement(ByTRLogo);
        private AtBy ByHelpCentreLogo => GetBy(LocatorType.XPath, "//a[@aria-label='Help Centre']//*[local-name()='svg']");
        private AtWebElement HelpCentreLogo => _webDriver.FindElement(ByHelpCentreLogo);
        private AtBy ByATOLLogo => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-app-header')]//*[local-name()='svg'][contains(@class,'color-primary')] | //*[local-name()='svg'][contains(@class,'color-primary')]//following::h5[contains(text(), 'ATOL')]");
        private AtWebElement ATOLLogo => _webDriver.FindElement(ByATOLLogo);
        private AtWebElement PackPeaceOfMindLink => _webDriver.FindElement(LocatorType.PartialLinkText, "packpeaceofmind");
        private AtWebElement FinancialProtectionLink => _webDriver.FindElement(LocatorType.PartialLinkText, "total-financial-protection");
        private AtBy ByMainMenu => GetBy(LocatorType.XPath, "//button[@aria-label='Open main menu']");
        private AtWebElement MainMenu => _webDriver.FindElement(ByMainMenu);
        private AtBy ByMainMenuOptions => GetBy(LocatorType.XPath, "//ul[@aria-label='Main Menu']/li[contains(@class,'option')]");
        private ReadOnlyCollection<AtWebElement> MainMenuOptions => _webDriver.FindElements(ByMainMenuOptions);
        private AtBy ByMenuTitle => GetBy(LocatorType.XPath, "//div[@class='sc-c-modal']//h3[contains(@class,'heading')]");
        private AtWebElement MenuTitle => _webDriver.FindElement(ByMenuTitle);
        private AtBy BySecondLevelMenuOptions => GetBy(LocatorType.XPath, "(//div[contains(@class,'submenu is-open')])/ul[@aria-expanded='true']/li[contains(@class,'option')]");
        private ReadOnlyCollection<AtWebElement> SecondLevelMenuOptions => _webDriver.FindElements(BySecondLevelMenuOptions);       
        private AtBy ByThirdLevelMenuOptions => GetBy(LocatorType.XPath, "(//div[contains(@class,'submenu is-open')])[2]/ul[@aria-expanded='true']/li[contains(@class,'option')]/ul//a");
        private ReadOnlyCollection<AtWebElement> ThirdLevelMenuOptions => _webDriver.FindElements(ByThirdLevelMenuOptions);        
        private AtBy ByAccountMenu => GetBy(LocatorType.XPath, "//button[contains(@aria-label,'Open account menu')] | //span[contains(.,'Sign In') and contains(@class,'masthead')]");
        private AtWebElement AccountMenu => _webDriver.FindElement(ByAccountMenu);
        private AtBy ByAccountMenuBadge => GetBy(LocatorType.CssSelector, "span[class='sc-c-badge-container'] span");
        private AtWebElement AccountMenuBadge => _webDriver.FindElement(ByAccountMenuBadge);
        private AtBy ByAccountMenuOptions => GetBy(LocatorType.XPath, "//ul[@aria-label='Account Menu' or @aria-label='Account menu' or contains(@aria-label,'Welcome')]/li[not(contains(@class,'divider'))]");
        private ReadOnlyCollection<AtWebElement> AccountMenuOptions => _webDriver.FindElements(ByAccountMenuOptions);
        private AtBy ByAccountSignInUsername => GetBy(LocatorType.XPath, "//form//input[@id='emailaddress' or @id='email']");
        private AtWebElement AccountSignInUsername => _webDriver.FindElement(ByAccountSignInUsername);
        private AtWebElement ContinueWithEmail => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Continue with email']/parent::button");
        private AtBy ByAccountSignInPassword => GetBy(LocatorType.CssSelector, "input[type='password']");
        private AtWebElement AccountSignInPassword => _webDriver.FindElement(ByAccountSignInPassword);
        private AtBy ByAccountSignInButton => GetBy(LocatorType.XPath, "//button[contains(text(),'Sign in')]");
        private AtWebElement AccountSignInButton => _webDriver.FindElement(ByAccountSignInButton);
        private AtBy bySearchResultsLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private AtBy ByCallToBookNumber => GetBy(LocatorType.XPath, "//div[text()='Call to book']/following-sibling::div");
        private AtWebElement CallToBookNumber => _webDriver.FindElement(ByCallToBookNumber);
        private AtBy bySigninWithGoogle => GetBy(LocatorType.XPath, "//span[contains(text(), 'Sign in with Google')]");
        private AtWebElement SigninWithGoogle => _webDriver.FindElement(bySigninWithGoogle);
        private AtBy bySigninWithFacebook => GetBy(LocatorType.XPath, "//span[contains(text(), 'Sign in with Facebook')]");
        private AtWebElement SigninWithFacebook => _webDriver.FindElement(bySigninWithFacebook);
        #endregion

        private string SelectedDestination;

        #region[Methods]
        public void ClickTRLogo()
        {
            _webDriver.WaitForElementVisible(ByTRLogo, 30, "TR Logo is not visible on header");
            TRLogo.Click();
        }

        public void ClickHelpCentreLogo()
        {
            _webDriver.WaitForElementVisible(ByHelpCentreLogo, 30, "Help Centre is not visible on header");
            HelpCentreLogo.Click();
        }

        public bool IsATOLLogoVisible()
        {
            return ATOLLogo.Visible;
        }

        public void ClickATOLLogo()
        {
            _webDriver.WaitForElementVisible(ByATOLLogo, 30, "ATOL logo is not visible on header");
            ATOLLogo.Click();
        }

        public bool IsLinksOnATOLModalEnbaled()
        {
            return PackPeaceOfMindLink.Visible && FinancialProtectionLink.Visible;
        }

        public void ClickMainMenu()
        {
            _webDriver.WaitForElementVisible(ByMainMenu, 30, "Main menu is not visible on header");
            MainMenu.Click();           
        }

        public bool IsMainMenuDisplayed()
        {
            return MainMenuOptions.Count > 0;
        }

        public void SelectFromMainMenuOptions(string option)
        {
            MainMenuOptions.Where(element => element.Text.Equals(option, StringComparison.OrdinalIgnoreCase)).First().Click();
        }

        public string GetMenuTitle()
        {
            return MenuTitle.Text;
        }

        public void SelectDestinationFromMenu()
        {
            SelectedDestination = SecondLevelMenuOptions[1].Text;
            SecondLevelMenuOptions[1].Click();
        }

        public bool IsThirdLevelMenuDisplayed()
        {
            return ThirdLevelMenuOptions.Count > 0;
        }

        public List<string> GetMainMenuOptions()
        {
            Thread.Sleep(2000);
            _webDriver.WaitForElementClickable(ByMainMenuOptions, 10, "Main menu is not open");
            List<string> mainMenuOptions = new List<string>();
            foreach (var element in MainMenuOptions)
            {
                mainMenuOptions.Add(element.Text);
            }
            return mainMenuOptions;
        }

        public string GetSelectedDestination()
        {
            return SelectedDestination;
        }

        public void ClickAccountMenu()
        {           
            _webDriver.WaitUntilNotVisible(bySearchResultsLoader, Constants.MediumWait);
            _webDriver.WaitForElementVisible(ByAccountMenu, Constants.MediumWait, "Account menu is not visible on header");
            _webDriver.ScrollElementToCenter(AccountMenu);
            _webDriver.WaitForElementClickable(ByAccountMenu, Constants.MediumWait, "Account menu is not visible on header");
            AccountMenu.Click();
            _webDriver.WaitInSec(3);
            _webDriver.WaitForAnyVisible(ByAccountSignInUsername,ByAccountMenuOptions, Constants.MediumWait, "Account menu is not clicked");
            if(!HelperFunctions.IsDesktop() && !HelperFunctions.IsV3SignInEnabled())
                _webDriver.WaitForElementClickable(ByAccountMenuOptions, Constants.ShortWait, "Account menu is not open");
        }
        public List<string> GetAccountMenuOptions()
        {            
            _webDriver.WaitForElementClickable(ByAccountMenuOptions, Constants.DefaultWait, "Account menu is not open");
            List<string> menuOptions = new List<string>();
            foreach (var element in AccountMenuOptions)
            {
                menuOptions.Add(element.Text);
            }
            return menuOptions;
        }

        public void SelectFromAccountMenuOptions(string option)
        {
            _webDriver.WaitForElementClickable(ByAccountMenuOptions, 20);
            AccountMenuOptions.Where(element => element.Text.Equals(option, StringComparison.OrdinalIgnoreCase)).First().Click();
        }

        public void PopulateAndSignInToAccount()
        {
            _webDriver.WaitForElementVisible(ByAccountSignInUsername, Constants.DefaultWait, "Email address sign in is not visisble");
            AccountSignInUsername.SendKeys(AtConfiguration.GetConfiguration<string>("AccountSignInUsername"));
            if(HelperFunctions.IsV3SignInEnabled())
                ContinueWithEmail.Click();
            _webDriver.WaitForElementVisible(ByAccountSignInPassword, Constants.DefaultWait, "Password sign in is not visisble");
            AccountSignInPassword.SendKeys(AtConfiguration.GetConfiguration<string>("AccountSignInPassword"));
            if (HelperFunctions.IsV3SignInEnabled())
                ContinueWithEmail.Click();
            else
                AccountSignInButton.Click();
            _webDriver.WaitForElementVisible(ByAccountMenu, Constants.DefaultWait, "Account Menu is not visisble");
            _webDriver.WaitForElementVisible(ByAccountMenuBadge, Constants.DefaultWait, "Account menu badge is not visible");
        }

        public bool IsSigninOptionDisplayed()
        {
            _webDriver.WaitForElementVisible(ByAccountSignInUsername, Constants.DefaultWait, "Email address sign in is not visisble");
            if (AccountSignInUsername.Visible && ContinueWithEmail.Visible && SigninWithGoogle.Visible && SigninWithFacebook.Visible)
                return true;
            else
                return false;
        }
        public bool IsUserSignedIn()
        {            
          return AccountMenuBadge.GetCssValue("opacity").Equals("1");            
        }
        public bool IsTravelRepublicLogoDisplayed()
        {
            return TRLogo.Displayed;
        }
        public bool IsHelpCentreLogoDisplayed()
        {
            return HelpCentreLogo.Displayed;
        }
        public bool IsAccountMenuDisplayed()
        {
            return AccountMenu.Displayed;
        }
        public bool IsMenuOptionDisplayed()
        {
           return MainMenu.Displayed;
        }
        
        public string GetCallToBookNumber()
        {
            return CallToBookNumber.Text;
        }

        public bool IsCallToBookNumberAHyperLink()
        {
            return !(CallToBookNumber.GetAttribute("href") == null);
        }

        #endregion
    }
}
