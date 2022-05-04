using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class BreadCrumb : MobileBasePage, IBreadCrumb
    {
        private readonly IAtWebDriver _webDriver;

        #region [Constructor]
        public BreadCrumb(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region [ Web Elements]

        private AtBy byBreadCrumbOptions => GetBy(LocatorType.XPath, "//ol[@class='sc-c-breadcrumb_list']/li[@class='sc-c-breadcrumb__item']/*[contains(@class,'link')]");
        private ReadOnlyCollection<AtWebElement> DesktopBreadCrumbItems => _webDriver.FindElements(byBreadCrumbOptions);

        #endregion

        #region [ Methods]

        public Dictionary<string, bool> GetBreadCrumbListItemsWithStatus()
        {
            Dictionary<string, bool> breadCrumbListItemsWithStatus = new Dictionary<string, bool>();

            foreach ( AtWebElement element in DesktopBreadCrumbItems)
            {
                breadCrumbListItemsWithStatus.Add(element.Text, element.GetAttribute("class").Contains("active"));
            }
            return breadCrumbListItemsWithStatus;
        }

        public bool IsBreadCrumbActive(string breadCrumbItem)
        {
            bool availability = false;
            Dictionary<string, bool> breadCrumbListItemsWithStatus = GetBreadCrumbListItemsWithStatus();
            breadCrumbListItemsWithStatus.TryGetValue(breadCrumbItem, out availability);
            return availability;
        }

        public Dictionary<string, string> GetLandingPageBreadCrumbLinks()
        {
            Dictionary<string, string> breadCrumbListIWithLinks = new Dictionary<string, string>();

            foreach (AtWebElement element in DesktopBreadCrumbItems)
            {
                breadCrumbListIWithLinks.Add(element.Text, element.GetAttribute("href"));
            }
            return breadCrumbListIWithLinks;
        }

        public bool ValidateLandingPageBreadCrumbNavigation(bool isHoliday)
        {            
            bool isValidBreadCrumb = true;
            for (var count = 0; count<DesktopBreadCrumbItems.Count; count++)            
            {
                var currentBreadCrumb = DesktopBreadCrumbItems[count];
                string breadCrumbName = currentBreadCrumb.Text;
                
                if (currentBreadCrumb.GetAttribute("href").Contains("/1-") || currentBreadCrumb.GetAttribute("href").Contains("/h"))
                {
                    DesktopBreadCrumbItems[count].Click();
                    if (isHoliday)
                        isValidBreadCrumb = GetCurrentURL().Contains("holidays", StringComparison.OrdinalIgnoreCase);
                    else
                        isValidBreadCrumb = GetCurrentURL().Contains("hotels", StringComparison.OrdinalIgnoreCase);

                    isValidBreadCrumb = GetCurrentURL().Contains(breadCrumbName, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    currentBreadCrumb.Click();
                    isValidBreadCrumb = GetCurrentURL().Contains("travelrepublic", StringComparison.OrdinalIgnoreCase);
                }
                NavigateBack();
            }
            
            return isValidBreadCrumb;
        }

        public int GetBreadCrumbLocation()
        {
            return DesktopBreadCrumbItems[1].Location.Y;
        }

        #endregion
    }
}
