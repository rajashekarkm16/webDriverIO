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
using System.Threading;
using NUnit.Framework;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class FiltersModal : MobileBasePage, IFiltersModal
    {
        #region[Constructor]
        private readonly IAtWebDriver _webDriver;

        public FiltersModal(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy byFiltersHeader => GetBy(LocatorType.XPath, "//h3[text()='Filter Results'] | //span[text()='Reset filters']");
        private AtWebElement FiltersHeader => _webDriver.FindElement(byFiltersHeader);
        private AtBy byHolidayPriceSection => GetBy(LocatorType.XPath, "//*[text()='Total Price']");
        private AtWebElement HolidayPriceSection => _webDriver.FindElement(byHolidayPriceSection);
        private AtBy byBoardtypeSection => GetBy(LocatorType.XPath, "//*[text()='Board Type']");
        private AtWebElement BoardtypeSection => _webDriver.FindElement(byBoardtypeSection);
        private AtBy byStartRatingSection => GetBy(LocatorType.XPath, "//*[text()='Star Rating']");
        private AtWebElement StartRatingSection => _webDriver.FindElement(byStartRatingSection);
        private AtWebElement CustomerRatingSection => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Our Customer Rating']//parent::div");
        private AtWebElement HolidayTypesSection => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Holiday Types']//parent::div");
        private AtWebElement PropertyAmenitiesSection => _webDriver.FindElement(LocatorType.XPath, "//*[text()='Property Amenities']");
        private AtBy byResetAllButton => GetBy(LocatorType.XPath, "(//span[text()='Reset filters']/parent::button)[1]");
        private AtWebElement ResetFilterButton => _webDriver.FindElement(byResetAllButton);
        private AtBy byViewMatchesButtonLoader => GetBy(LocatorType.CssSelector, "button.is-busy");
        private AtBy byViewMatchesButton => GetBy(LocatorType.XPath, "//span[contains(text(),'View Matches')]/parent::button");
        private AtWebElement ViewMatchesButton => _webDriver.FindElement(byViewMatchesButton);
        private ReadOnlyCollection<AtWebElement> StarRatingOptions => _webDriver.FindElements(LocatorType.XPath, "//input[contains(@id,'star-rating')]");
        private ReadOnlyCollection<AtWebElement> CustomerRatingOptions => _webDriver.FindElements(LocatorType.XPath, "//label[contains(@for,'customer-rating')]");
        private AtBy byCustomerRatingHeader => GetBy(LocatorType.XPath, "//span[text()='Our Customer Rating']/parent::div");
        private AtWebElement CustomerRatingHeader => _webDriver.FindElement(byCustomerRatingHeader);
        //left slider =1 and right slider =2
        private AtWebElement CustomerRatingSlider(int index) => _webDriver.FindElement(LocatorType.XPath, "(//span[text()= 'Our Customer Rating']//ancestor::div[contains(@class, 'sc-c-accordion sc-c-accordion--condensed')]//span[@class= 'sc-c-slider-handle'])[#]", index.ToString());
        private ReadOnlyCollection<AtWebElement> HolidayTypes => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@aria-labelledby,'holiday-type')]//label");
        private AtBy byPropertyAmenitiesHeader => GetBy(LocatorType.XPath, "//div[contains(@aria-controls,'property-amenity')]");
        private AtWebElement PropertyAmenitiesHeader => _webDriver.FindElement(byPropertyAmenitiesHeader);
        private ReadOnlyCollection<AtWebElement> PropertyAmenities => _webDriver.FindElements(LocatorType.XPath, "//div[@id='property-amenity-accordion-content']//label");
        private ReadOnlyCollection<AtWebElement> BoardTypeOptions => _webDriver.FindElements(LocatorType.XPath, "//div[@id='board-type-accordion-content']//label[@class='sc-c-switch-control-label']/span");
        private AtBy byResetButtonLoader => GetBy(LocatorType.CssSelector, "span.sc-c-button__throbber");
        private AtWebElement ResetButtonLoader => _webDriver.FindElement(byResetButtonLoader);
        List<string> boardTypeFilterOptionNames;
        List<string> propertyAmenititesFilterOptionNames;
        List<string> holidayTypeFilterOptionNames;
        public int ViewMatchesCount;

        #endregion

        #region[Methods]

        public string GetFilterModelHeading()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return FiltersHeader.Text;
        }

        public bool ValidateFiltersModal()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return (IsHolidayPriceSectionVisible() & IsBoardtypeSectionVisible() & IsStarRatingSectionVisible() &
                IsCustomerRatingSectionVisible() & IsHolidayTypesSectionVisible() & IsPropertyAmenitiesSectionVisible() &
                IsResetAllButtonVisible() & HelperFunctions.IsDesktop()? true:IsViewMatchesButtonVisible());
        }

        public bool IsResetAllButtonVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return ResetFilterButton.Displayed;
        }

        public bool IsViewMatchesButtonVisible()
        {   
            WaitForFilterSelectionToLoad();
            return ViewMatchesButton.Enabled;
        }

        public void WaitForFilterSelectionToLoad()
        {
            _webDriver.WaitForElementPresent(ResetFilterButton, 30);
            _webDriver.WaitUntilNotVisible(byResetButtonLoader, 20);
        }

        public int GetViewMatchesResultCount()
        {
            _webDriver.WaitUntilNotVisible(byResetButtonLoader, Constants.MediumWait);
            _webDriver.WaitUntilNotVisible(byViewMatchesButtonLoader, Constants.MediumWait);
            Thread.Sleep(2000);
            return Convert.ToInt32(CommonFunctions.RemoveCurrencyInfo(ViewMatchesButton.InnerText));            
        }

        public void ViewFilterMatches()
        {
            ViewMatchesCount = GetViewMatchesResultCount();
            ViewMatchesButton.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(byViewMatchesButton,Constants.MediumWait, "View Matches Button is not Clicked");
        }

        public int GetStoredViewMatchesCount()
        {
            return ViewMatchesCount;
        }

        public void ResetFilter()
        {
            _webDriver.WaitForElementVisible(byResetAllButton,Constants.MediumWait,"Reset all button is not visible");
            ResetFilterButton.ClickButtonUsingJs();
        }

        public bool IsViewMatchesButtonEnabled()
        {
            WaitForFilterSelectionToLoad();
            return ViewMatchesButton.Enabled;
        }

        #region [Price Filter]
        public bool IsHolidayPriceSectionVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return HolidayPriceSection.Visible;
        }

        #endregion

        #region [ Star Rating ]

        public bool IsStarRatingSectionVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return StartRatingSection.Visible;
        }

        public int GetAvailableStarRatingOptions()
        {
            return StarRatingOptions.Count();         
        }

        public string SetStarRatingFilter(int starRatingOption)
        {
            StarRatingOptions[starRatingOption - 1].ClickButtonUsingJs();
            return StarRatingOptions[starRatingOption - 1].GetAttribute("name").Split('-')[2];
        }
        public bool IsStarRatingOptionsDisabled()
        {
            bool isDisabled = false;
            foreach(var element in StarRatingOptions)
            {
                if (element.GetAttribute("disabled") == "true")
                {
                    isDisabled = true;
                    break;
                }
            }
            return isDisabled;
        }
       
        #endregion

        #region [Customer Rating]

        public bool IsCustomerRatingSectionVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return CustomerRatingSection.Visible;
        }
        public int GetAvailableCustomerRatingOptions()
        {
            return CustomerRatingOptions.Count();
        }
        public string SetCustomerRatingFilter(int customerRatingOption)
        {
            var customerRatingElement = CustomerRatingOptions[customerRatingOption - 1];
            _webDriver.ScrollToElement(customerRatingElement);
            _webDriver.WaitForElementClickable(customerRatingElement, Constants.ShortWait);
            customerRatingElement.Click();
            _webDriver.WaitUntilNotVisible(byResetButtonLoader, Constants.ShortWait, "Loader is still visible");
            return customerRatingElement.GetAttribute("for").Split('-')[2];
        }

        
        public void IncreaseCutomerRating(int xCoordinate, int yCoordinate = 0)
        { 
            _webDriver.ScrollElementToCenter(CustomerRatingHeader);
            CustomerRatingSlider(1).DragSliderByOffset(xCoordinate, yCoordinate);
        }

        public void DecreaseCutomerRating(int xCoordinate, int yCoordinate = 0)
        {
            _webDriver.ScrollElementToCenter(CustomerRatingHeader);
            _webDriver.WaitForElementClickable(CustomerRatingSlider(2), 10);
            CustomerRatingSlider(2).DragSliderByOffset(xCoordinate, yCoordinate);
        }
        #endregion

        #region [Board Types]

        public bool IsBoardtypeSectionVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return BoardtypeSection.Visible;
        }
        public int GetFilterBoardTypesCount()
        {
            return BoardTypeOptions.Count;
        }
        public List<string> GetFilterBoardTypes()
        {
            if (boardTypeFilterOptionNames == null)
                boardTypeFilterOptionNames = new List<string>();
            _webDriver.WaitForElementVisible(byResetAllButton,Constants.MediumWait,"Reset button is not visible");
            foreach (var element in BoardTypeOptions)
            {
                Console.WriteLine(element.Text);
                boardTypeFilterOptionNames.Add(element.Text);
            }
            return boardTypeFilterOptionNames;
        }
        public void SetBoardTypeFilter(string boardTypeFilterOption)
        {
            if (boardTypeFilterOptionNames == null)
                GetFilterBoardTypes();
            BoardTypeOptions[boardTypeFilterOptionNames.IndexOf(boardTypeFilterOption)].Click();
            WaitForFilterSelectionToLoad();
            Console.WriteLine("Board Type Filters Set :  {0}" + boardTypeFilterOption);
        }
        public void SetBoardTypeFilter(List<string> boardTypeFilterOptions)
        {
            foreach (var boardFilterOption in boardTypeFilterOptions)
            {
                SetBoardTypeFilter(boardFilterOption);
            }
        }

        #endregion

        #region [Property Types]

        public bool IsPropertyAmenitiesSectionVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return PropertyAmenitiesSection.Visible;
        }

        public int GetAvailablePropertyAmenitiesCount()
        {
            return PropertyAmenities.Count();
        }

        public List<string> GetAvailablePropertyAmenities()
        {
            if (propertyAmenititesFilterOptionNames == null)
                propertyAmenititesFilterOptionNames = new List<string>();
            _webDriver.WaitForElementVisible(byPropertyAmenitiesHeader, Constants.MediumWait, "Property Amenities not loaded ");
            if (PropertyAmenitiesHeader.GetAttribute("aria-expanded").Equals("false"))
                PropertyAmenitiesHeader.Click();
            foreach (var element in PropertyAmenities)
            {
                propertyAmenititesFilterOptionNames.Add(element.Text);
            }
            return propertyAmenititesFilterOptionNames;
        }

        public void SetPropertyAmenities(string propertyAmenityOption)
        {
            _webDriver.WaitForElementVisible(byPropertyAmenitiesHeader, Constants.MediumWait, "Property Amenities not loaded ");
            if (PropertyAmenitiesHeader.GetAttribute("aria-expanded").Equals("false"))
                PropertyAmenitiesHeader.Click();
            PropertyAmenities[propertyAmenititesFilterOptionNames.IndexOf(propertyAmenityOption)].ClickButtonUsingJs();
            Console.WriteLine("Property Filters Set :  {0}" + propertyAmenityOption);
            WaitForFilterSelectionToLoad();
        }

        public void SetPropertyAmenities(List<string> propertyAmenityOption)
        {
            foreach (var amenity in propertyAmenityOption)
            {
                SetPropertyAmenities(amenity);
            }
        }

        #endregion

        #region [Holiday Types]

        public bool IsHolidayTypesSectionVisible()
        {
            _webDriver.WaitForElementVisible(byFiltersHeader, 30, "Filters header is not visible");
            return HolidayTypesSection.Visible;
        }
        public int GetAvailableHolidayTypes()
        {
            return HolidayTypes.Count();
        }
        public List<string> GetAvailableHolidayTypeNames()
        {
            _webDriver.WaitForElementVisible(byResetAllButton, Constants.MediumWait, "Reset Button is not visible");
            if (HolidayTypesSection.GetAttribute("aria-expanded").Equals("false"))
                HolidayTypesSection.Click();
            if (holidayTypeFilterOptionNames == null)
                holidayTypeFilterOptionNames = new List<string>();

            foreach (var element in HolidayTypes)
            {
                holidayTypeFilterOptionNames.Add(element.Text);
            }
            return holidayTypeFilterOptionNames;
        }

        public void SetHolidayTypes(string holidayTypeOption)
        {
                HolidayTypes[holidayTypeFilterOptionNames.IndexOf(holidayTypeOption)].ClickButtonUsingJs();
                Console.WriteLine("Holiday Type Filters Set :  {0}" + holidayTypeOption);
                WaitForFilterSelectionToLoad();
        }


        #endregion

        #endregion
    }
    
}
