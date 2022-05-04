using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System.Collections.ObjectModel;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class FooterComponent : MobileBasePage, IFooterComponent
    {
        #region[Constructor]
        private readonly IAtWebDriver _webDriver;


        public FooterComponent(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy ByITAALogo => GetBy(LocatorType.XPath, "//a[contains(@href,'itaa')]");
        private AtWebElement ITAALogo => _webDriver.FindElement(ByITAALogo);
        private AtBy ByABTALogo => GetBy(LocatorType.XPath, "//a[contains(@href,'abta')]");
        private AtWebElement ABTALogo => _webDriver.FindElement(ByABTALogo);
        private AtBy ByFooterLinks => GetBy(LocatorType.XPath, "//a[contains(@class,'white')]");
        private ReadOnlyCollection<AtWebElement> FooterLinks => _webDriver.FindElements(ByFooterLinks);
        private AtBy ByCallUsNumber => GetBy(LocatorType.XPath, "//h5[text()='Call us']/following-sibling::a");
        private AtWebElement CallUsNumber => _webDriver.FindElement(ByCallUsNumber);
        private AtBy ByAtolLogo => GetBy(LocatorType.XPath, "//a[contains(@href,'atol')]");
        private AtWebElement AtolLogo => _webDriver.FindElement(ByAtolLogo);
        private AtBy ByFooterText => GetBy(LocatorType.XPath, "//div[contains(@class,'pre-line')]/parent::div");
        private AtWebElement FooterText => _webDriver.FindElement(ByFooterText);
        private AtBy ByTermsOfBusinessLink => GetBy(LocatorType.XPath, "//a[text()='Terms of Business']");
        private AtWebElement TermsOfBusinessLink => _webDriver.FindElement(ByTermsOfBusinessLink);
        private AtBy ByStandardCancellationTermsLink => GetBy(LocatorType.XPath, "//a[text()='standard cancellation terms']");
        private AtWebElement StandardCancellationTermsLink => _webDriver.FindElement(ByStandardCancellationTermsLink);

        #endregion

        #region[Methods]
        public bool IsITAALogoDisplayed()
        {
            return ITAALogo.Visible;
        }

        public bool IsABTALogoDisplayed()
        {
            return ABTALogo.Visible;
        }

        public bool IsCallUsNumberDisplayed()
        {
            return CallUsNumber.Visible;
        }

        public bool IsFooterLinksDisplayed()
        {
            return FooterLinks[0].Visible;
        }

        public bool IsATOLLogoDisplayed()
        {
            return AtolLogo.Visible;
        }

        public bool IsFooterTextDisplayed()
        {
            return FooterText.Text != "";
        }

        public void ClickTermsOfBusinessLink()
        {
            _webDriver.ScrollElementToCenter(TermsOfBusinessLink);
            TermsOfBusinessLink.Click();
        }

        public void ClickStandardCancellationsTermsLink()
        {
            _webDriver.ScrollElementToCenter(StandardCancellationTermsLink);
            StandardCancellationTermsLink.Click();
        }


        public bool IsFooterLinksClickable()
        {
            bool islinksClickable = true;
            foreach (var link in FooterLinks)
            {
                if (link.GetAttribute("href") == null)
                    islinksClickable = false;
            }
            return islinksClickable;
        }
        public string GetCallUsNumber()
        {
            return CallUsNumber.Text;

        }
        #endregion

    }
}
