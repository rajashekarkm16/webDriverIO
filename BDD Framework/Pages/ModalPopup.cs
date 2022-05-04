using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class ModalPopup : MobileBasePage,IModalPopup
    {
        private readonly IAtWebDriver _webDriver;

        #region [Constructor]
        public ModalPopup(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region [ Web Elements]

        private AtBy byModalPopupDialog => GetBy(LocatorType.CssSelector, "div[class*='sc-c-dialog'][role='document'][style*='opacity: 1']");
        private AtWebElement ModalPopupDialog => _webDriver.FindElement(byModalPopupDialog);
        private AtBy byModalHeading => GetBy(LocatorType.CssSelector, "div[class*='sc-c-dialog'][role='document'][style*='opacity: 1'] header");
        private AtWebElement ModalHeading => _webDriver.FindElement(byModalHeading);
        private AtBy byModalContent => GetBy(LocatorType.CssSelector, "div[class*='sc-c-dialog'][role='document'][style*='opacity: 1'] div[class='sc-c-dialog-content'] div");
        private AtWebElement ModalContent => _webDriver.FindElement(byModalContent);
        private AtBy byModalCloseButton => GetBy(LocatorType.CssSelector, "div[class*='sc-c-dialog'][role='document'][style*='opacity: 1'] button");
        private AtWebElement ModalCloseButton => _webDriver.FindElement(byModalCloseButton);
        private IReadOnlyCollection<AtWebElement> AllLinks => _webDriver.FindElements(LocatorType.CssSelector, "div[class*='sc-c-dialog'][role='document'][style*='opacity: 1'] a");
        #endregion

        #region [ Methods ]
        public void ClosePopUp()
        {
            _webDriver.WaitForElementVisible(byModalCloseButton, 30, "ModalPopupDialog is not visible");
            ModalCloseButton.Click();
        }

        public string GetModalContent()
        {
            Thread.Sleep(2000);
            _webDriver.WaitForElementVisible(byModalContent, 30, "ModalPopupDialog is not visible");
            return ModalContent.Text;
        }
        
        public string GetModalHeading()
        {
            Thread.Sleep(2000);
            _webDriver.WaitForElementVisible(byModalHeading, 30, "ModalPopupDialog is not visible");
            return ModalHeading.Text;
        }

        public bool IsModalDisplayed()
        {
            Thread.Sleep(2000);
            return ModalPopupDialog.Visible;
        }

        public List<string> GetAllLinks()
        {
            List<string> allLinks = new List<string>();
            foreach (var link in AllLinks)
                allLinks.Add(link.Text);
            return allLinks;
        }

        #endregion

    }
}
