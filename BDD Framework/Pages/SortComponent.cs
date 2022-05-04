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
    public class SortComponent : MobileBasePage,ISortComponent
    {
        private readonly IAtWebDriver _webDriver;

        private AtBy bySelectedSortOption => GetBy(LocatorType.XPath, "//div[contains(@class,'dialog')]//input[@checked]/ancestor::li//div[contains(@class,'primary-content')]");
        private AtWebElement SelectedSortOption => _webDriver.FindElement(bySelectedSortOption);
        private AtWebElement SortOptions(string option) => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'dialog')]//ul/li[contains(@class,'option-list')]/div/div[contains(@class,'primary-content') and text()='#']", option);
        private AtWebElement CloseSort => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'dialog')]//button[@aria-label='Close']");
       
        public SortComponent(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        public void CloseSortOption()
        {
            _webDriver.WaitForElementVisible(CloseSort, 10);
            CloseSort.ClickButtonUsingJs();
        }

        public string GetSelectedSortOption()
        {
            _webDriver.WaitForElementVisible(bySelectedSortOption, 30, "SelectedSortOption is not visible");
            return SelectedSortOption.Text;
        }

        public void SelectSortOption(string option)
        {
            _webDriver.WaitForElementVisible(SortOptions(option), 10);
            if (!(option.Equals("Customer Rating (Highest first)") || option.Equals("Price (Cheapest first)") || option.Equals("Star Rating (Highest first)") || option.Equals("Recommended")))
                Assert.IsTrue(false, "Invalid sort option: " + option);
            SortOptions(option).Click();
        }

        public void SelectFlightSortOption(string option)
        {
            _webDriver.WaitForElementVisible(SortOptions(option), 10);
            if (!(option.Equals("Price (Cheapest first)") || option.Equals("Duration (Quickest first)") || option.Equals("Recommended")))
                Assert.IsTrue(false, "Invalid sort option: " + option);
            SortOptions(option).Click();
        }
    }
}
