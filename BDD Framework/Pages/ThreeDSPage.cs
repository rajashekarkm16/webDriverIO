using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class ThreeDSPage : MobileBasePage, IThreeDSPage
    {
        #region[Constructor]
        private readonly IAtWebDriver _webDriver;       
        public ThreeDSPage(IAtWebDriver webDriver)
           : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtWebElement Title => _webDriver.FindElement(LocatorType.XPath, "//h3[contains(@id,'threeDSecure')]");
        private AtBy bySubmitButton => GetBy(LocatorType.XPath, "//input[@value='Submit']");
        private AtWebElement SubmitButton => _webDriver.FindElement(bySubmitButton);
        #endregion

        #region[Methods]

        public void ClickSubmitButton()
        {
            _webDriver.SwitchToFrame("threeDSecureIframe");
            _webDriver.WaitForElementVisible(bySubmitButton, 30, "SubmitButton is not visible");
            SubmitButton.ClickButtonUsingJs();
        }

        #endregion

    }
}
