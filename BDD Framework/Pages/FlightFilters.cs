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
using NUnit.Framework;
using System.Configuration;
using System.Threading;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class FlightFilters : MobileBasePage, IFlightFilters
    {
        #region[Constructor]
        private readonly IAtWebDriver _webDriver;

        public FlightFilters(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy byFilterHeader => GetBy(LocatorType.XPath, "//h3[text()='Filter Results']");
        private AtWebElement FilterHeader => _webDriver.FindElement(byFilterHeader);
        private AtBy byCloseFilterButton => GetBy(LocatorType.XPath, "//div[contains(@class,'space')]//button[contains(@aria-label,'Close')]");
        private AtWebElement CloseFilterButton => _webDriver.FindElement(byCloseFilterButton);
        private AtBy byResetFilters => GetBy(LocatorType.XPath, "//div[@class='sc-c-dialog-content'] //button[.='Reset filters']");
        private AtWebElement ResetFilters => _webDriver.FindElement(byResetFilters);
        private AtBy byAirports => GetBy(LocatorType.XPath, "//span[text()='Airports']");
        private AtWebElement Airports => _webDriver.FindElement(byAirports);
        private AtWebElement SameDepartureArrivalAirportToggle => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'sc-c-switch__handle')]");
        private AtBy byDepartureAirportFilter => GetBy(LocatorType.XPath, "//span[text()='Departure Airport']");
        private AtWebElement DepartureAirportFilter => _webDriver.FindElement(byDepartureAirportFilter);
        private ReadOnlyCollection<AtWebElement> DepartureAirports => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id, 'departure-airports-accordion-content')] //label[contains(@class,'control-label')]");
        private ReadOnlyCollection<AtWebElement> DepartureAirportsCheckBox => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id, 'departure-airports-accordion-content')] //label[contains(@class,'control-label')] //input");
        private AtBy byOutboundDepartureTimes => GetBy(LocatorType.XPath, "//span[text()='Outbound Departure Times']");
        private AtWebElement OutboundDepartureTimes => _webDriver.FindElement(byOutboundDepartureTimes);
        private AtBy byReturnDepartureTimes => GetBy(LocatorType.XPath, "//span[text()='Return Departure Times']");
        private AtWebElement ReturnDepartureTimes => _webDriver.FindElement(byReturnDepartureTimes);       
        private AtBy byAirlineFilter => GetBy(LocatorType.XPath, "//span[text()='Airline']");
        private AtWebElement AirlineFilter => _webDriver.FindElement(byAirlineFilter);
        private AtBy ByAirline => GetBy(LocatorType.XPath, "//div[contains(@id, 'airline-accordion-content')] //label[contains(@class,'control-label')]");
        private ReadOnlyCollection<AtWebElement> Airline => _webDriver.FindElements(ByAirline);
        private ReadOnlyCollection<AtWebElement> AirlineCheckBox => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id, 'airline-accordion-content')] //label[contains(@class,'control-label')] //input");
        //div[contains(@class,'s-6')]//div[contains(@class,'sc-c-pill')]//label
        private AtBy byViewMatchesButton => GetBy(LocatorType.XPath, "//div[contains(@class,'footer')]//button[not(contains(@class,'busy'))]");
        private AtWebElement ViewMatchesButton => _webDriver.FindElement(byViewMatchesButton);
        private AtBy byNoOfStopsFilter => GetBy(LocatorType.XPath, "//span[text()='Number of stops']");
        private AtWebElement NoOfStopsFilter => _webDriver.FindElement(byNoOfStopsFilter);        
        private AtBy ByNoOfStops => GetBy(LocatorType.XPath, "//div[contains(@id, 'stops-accordion-content')] //label[contains(@class,'control-label')]");
        private ReadOnlyCollection<AtWebElement> NoOfStopsCheckBox => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@id, 'stops-accordion-content')] //label[contains(@class,'control-label')] //input");
        private ReadOnlyCollection<AtWebElement> NoOfStops => _webDriver.FindElements(ByNoOfStops);
        private AtBy byFiltersPageLoader => GetBy(LocatorType.CssSelector, "div.sc-c-dimmer");
        private AtBy byViewMatchesButtonLoader => GetBy(LocatorType.CssSelector, "button.is-busy");
        #endregion

        #region[Methods]

        public void CloseFlightsFliter()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            CloseFilterButton.ClickButtonUsingJs();
            _webDriver.WaitUntilNotVisible(byCloseFilterButton, 30);
        }

        public bool IsResetAllButtonDisplayed()
        {
            _webDriver.WaitForElementVisible(byResetFilters, 10, "Reset filter is not visible");
            return (ResetFilters.Displayed);
        }

        public bool IsSameDepartureArrivalAirportFilterDisplayed()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            return Airports.Visible;
        }

        public bool IsDepartureAirportFilterDisplayed()
        {
            _webDriver.WaitForElementVisible(byCloseFilterButton, 30, "close filter button is not visible");
            return DepartureAirportFilter.Visible;
        }
        public bool IsOutboundDepartureTimeFilterDisplayed()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            return OutboundDepartureTimes.Visible;
        }
        public bool IsReturnDepartureTimeFilterDisplayed()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            return ReturnDepartureTimes.Visible;
        }
        public bool IsAirlineFilterDisplayed()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            return AirlineFilter.Visible;
        }

        public bool IsNoOfStopsFilterDisplayed()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            return NoOfStopsFilter.Visible;
        }

        public bool IsNumberOfStopsDisplayed()
        {
            _webDriver.WaitForElementVisible(byCloseFilterButton, 30, "close filter button is not visible");
            _webDriver.WaitForElementVisible(byNoOfStopsFilter, 10, "No of stops filter is not visible");
            return NoOfStops[0].Visible;
        }


        public bool IsDepartureAirportsDisplayed()
        {
            _webDriver.WaitForElementVisible(byCloseFilterButton, 30, "close filter button is not visible");
            return DepartureAirports[0].Visible;
        }
                
        public int GetViewMatchesCount()
        {
            _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
            _webDriver.WaitForTextPresent(byViewMatchesButton, "View", TimeSpan.FromSeconds(10), 3);
            _webDriver.WaitUntilNotVisible(byViewMatchesButtonLoader, Constants.MediumWait);
            Thread.Sleep(2000);
            return Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(ViewMatchesButton.Text));
        }

        public void SelectMultipleDepartureAirports()
        {
            int NoOfAirports = DepartureAirports.Count;
            if (NoOfAirports >= 3)
            {
                SelectDepartureAirports(NoOfAirports-1);
            }
            else
                SelectDepartureAirports(NoOfAirports);
        }
        public void SelectDepartureAirports(int NoOfAirports)
        {
            for (int i = 0; i < NoOfAirports; i++)
            {
                _webDriver.ScrollToElement(DepartureAirports[i]);
                DepartureAirports[i].Click();
                _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
                bool isFilterselected = DepartureAirportsCheckBox[i].Selected;
                Assert.IsTrue(isFilterselected, "Departure airport selection validation");
            }
        }
        public void SelectDepartureAirportFilter(int FilterToSelect)
        {
            _webDriver.WaitForElementVisible(byDepartureAirportFilter, 30, "Departure Airport Filter is not visible");
            _webDriver.ScrollToElement(DepartureAirports[FilterToSelect - 1]);
            DepartureAirports[FilterToSelect - 1].Click();
            _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
        }

        public void SelectDepartureAirportFilter(string filterName)
        {
            _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 30);
            _webDriver.WaitForElementVisible(byDepartureAirportFilter, 30, "Departure airport filter is not visible");
            bool isFilterSelected = false;
            foreach (var filter in DepartureAirports)
            {
                if (filter.Text.Contains(filterName))
                {
                    //_webDriver.WaitForElementClickable(filter);  - code cleanup
                    _webDriver.ScrollToElement(filter);
                    filter.Click();
                    _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 60);
                    isFilterSelected = true;
                    break;
                }
            }
            Assert.IsTrue(isFilterSelected, filterName + " filter is not available");
        }
        public bool IsDepartureAirportsSelected()
        {
            bool IsSelected = false;
            _webDriver.WaitForElementVisible(byDepartureAirportFilter, 10, "Departure airport filter is not visible");
            for (int i = 0; i < DepartureAirports.Count; i++)
            {
                if (DepartureAirports[i].GetCssValue("background-color").Equals(Constants.Blue))
                    IsSelected = true;
            }
            return IsSelected;
        }
        public bool IsAirlinesDisplayed()
        {
            return Airline[0].Visible;
        }
        public bool IsAirportsFilterVisibe()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter header is not visible");
            return Airports.Visible;
        }
        public void SelectMultipleAirlines()
        {
            int NoOfAirlines = Airline.Count;
            if (NoOfAirlines >= 3)
            {
                SelectAirlines(NoOfAirlines - 1);
            }
            else
                SelectAirlines(NoOfAirlines);
        }
        public void SelectAirlines(int NoOfAirlines)
        {
            for (int i = 0; i < NoOfAirlines; i++)
            {
                _webDriver.ScrollToElement(Airline[i]);
                Airline[i].Click();
                _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
                _webDriver.WaitForElementClickable(ByAirline, 20);
                bool isFilterSelected = AirlineCheckBox[i].Selected;
                Assert.IsTrue(isFilterSelected, "Airline selection validation");
            }
        }
        public void SelectAirlinesFilter(int FilterToSelect)
        {
            _webDriver.WaitForElementVisible(ByAirline, 30, "Airline is not visible");
            Airline[FilterToSelect - 1].Click();
            _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
        }
        public void SelectAirlinesFilter(string filterName)
        {
            _webDriver.WaitForElementVisible(ByAirline, 30, "Airline is not visible");
            foreach (var filter in Airline)
            {
                if (filter.Text.Contains(filterName))
                    filter.Click();
            }
        }

        public bool IsAirlineFiltersSelected()
        {
            bool IsSelected = false;
            _webDriver.WaitForElementVisible(ByAirline, 10, "Airline is not visible");
            for (int i = 0; i < Airline.Count; i++)
            {
                if (Airline[i].GetCssValue("background-color").Equals(Constants.Blue))
                    IsSelected = true;
            }
            return IsSelected;
        }
               
        public void ToggleDepartureReturnSameAirport(bool isSelect)
        {
            _webDriver.WaitForElementVisible(byAirports, 20, "Airports is not visible");
            string selectedToggleColour = SameDepartureArrivalAirportToggle.GetCssValue("background-color");
            if (isSelect)
            {
                if (!selectedToggleColour.Equals(Constants.Blue))
                {
                    SameDepartureArrivalAirportToggle.ClickUsingActions();
                    _webDriver.WaitForElementVisible(byViewMatchesButton, 20, "View matches button is not visible");                    
                }
                
            }
            else
            {
                if (selectedToggleColour.Equals(Constants.Blue))
                {
                    SameDepartureArrivalAirportToggle.ClickUsingActions();
                    _webDriver.WaitForElementVisible(byViewMatchesButton, 20, "View matches button is not visible");
                }               
            }                                 
            
        }

        public bool IsDepartureReturnSameAirportSelected()
        {
            _webDriver.WaitForElementVisible(byAirports, 20, "Airports is not visible");
            string selectedToggleColour = SameDepartureArrivalAirportToggle.GetCssValue("background-color");
            return selectedToggleColour.Equals(Constants.Blue);
        }

        public bool IsSelectedToggle(string expectedColour)
        {
            string selectedToggleColour = SameDepartureArrivalAirportToggle.GetCssValue("background-color");
            if (expectedColour.ToLower() == "blue")
            {
                string expectedToggleColour = Constants.Blue;
                return expectedToggleColour.Equals(selectedToggleColour);
            }
            
            else if(expectedColour.ToLower() == "grey")
            {
                string expectedToggleColour = Constants.Grey;
                return expectedToggleColour.Equals(selectedToggleColour);
            }
            return false;
        }

        public bool IsFlightFliterModalOpen()
        {
            return CloseFilterButton.Visible;
        }

        public void ClickViewMatchesButton()
        {
            _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
            ViewMatchesButton.Click();
            _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 20);
        }

        public void ClickResetFilters()
        {
            _webDriver.WaitForElementVisible(byFilterHeader, 10, "Filter Header is not visible");
            _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 20);
            _webDriver.ScrollToElement(ResetFilters);
            ResetFilters.Click();
            _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 20);
        }

        public void SelectMultipleStops()
        {
            _webDriver.WaitForElementClickable(ByNoOfStops, 20);
            int availableStops = NoOfStops.Count;
            if (availableStops >= 3)
            {
                SelectMultilpleNoOfStopsFilter(availableStops - 1);
            }
            else
                SelectMultilpleNoOfStopsFilter(availableStops);
        }
        public void SelectMultilpleNoOfStopsFilter(int availableStops)
        {
            for (int i = 0; i < availableStops; i++)
            {
                _webDriver.MoveToElement(NoOfStops[i]);
                NoOfStops[i].Click();
                _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
                _webDriver.WaitForElementClickable(ByNoOfStops, 20);
                bool isFilterSelected = NoOfStopsCheckBox[i].Selected;
                Assert.IsTrue(isFilterSelected, "Number of stops selection validation");
            }
        }

        public void SelectNoOfStopsFilter(int FilterToSelect)
        {
            _webDriver.WaitForElementVisible(ByNoOfStops, 30, "NoOfStops is not visible");
            NoOfStops[FilterToSelect - 1].Click();
            _webDriver.WaitForElementVisible(byViewMatchesButton, 30, "View matches button is not visible");
        }

        public void SelectNoOfStopsFilter(string numberOfStops)
        {
            _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 30);            
            _webDriver.WaitForElementClickable(ByNoOfStops, 20);
            bool isFilterSelected = false;
            if (numberOfStops.Equals("Direct", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var element in NoOfStops)
                {
                    if (element.Text.Equals("Direct"))
                    {
                        element.Click();
                        isFilterSelected = true;
                        _webDriver.WaitForElementClickable(byViewMatchesButton, 20);
                        break;
                    }      
                }
            }
            else if (numberOfStops.Equals("One", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var element in NoOfStops)
                {
                    if (element.Text.Contains("One"))
                    {
                        element.Click();
                        isFilterSelected = true;
                    }                        
                }
            }
            else if (numberOfStops.Equals("Two", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var element in NoOfStops)
                {
                    if (element.Text.Contains("2"))
                    {
                        element.Click();
                        isFilterSelected = true;
                    }
                }
            }
            _webDriver.WaitForElementClickable(ByNoOfStops, 20);
            if (!isFilterSelected || Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(ViewMatchesButton.Text)) == 0)
                Assert.Inconclusive(numberOfStops + " filter is not available");
        }
       
        public bool IsNoOfStopsSelected()
        {
            bool IsSelected = false;
            _webDriver.WaitForElementVisible(ByNoOfStops, 10, "NoOfStops is not visible");
            for (int i = 0; i < NoOfStops.Count; i++)
            {
                if (NoOfStops[i].GetCssValue("background-color").Equals(Constants.Blue))
                    IsSelected = true;
            }
            return IsSelected;
        }

        public void AddAvailableFilters()
        {
            if(IsAirportsFilterVisibe())
                ToggleDepartureReturnSameAirport(true);
            if (IsNoOfStopsFilterDisplayed())
                SelectNoOfStopsFilter(1);
            if (IsAirlineFilterDisplayed())
                SelectAirlines(1);
            if (IsDepartureAirportFilterDisplayed())
                SelectDepartureAirportFilter(1);
        }

        public bool IsAllAvailableFiltersReset()
        {
            bool isFiltersReset = true;
            _webDriver.WaitUntilNotVisible(byFiltersPageLoader, 20);
            if (IsAirportsFilterVisibe() && IsDepartureReturnSameAirportSelected())
                isFiltersReset = false;

            if (IsNoOfStopsFilterDisplayed() && IsNoOfStopsSelected())
                isFiltersReset = false;
           
            if (IsAirlineFilterDisplayed() && IsAirlineFiltersSelected())
                isFiltersReset = false;
            
            if (IsDepartureAirportFilterDisplayed() && IsDepartureAirportsSelected())
                isFiltersReset = false;
            
            return isFiltersReset;
        }

        public int GetNoOfStops(string numberOfStops)
        {
            switch(numberOfStops){
                case "Direct":
                    numberOfStops = "0";                    
                    break;
                case "One":
                    numberOfStops = "1";
                    break;
                case "Two":
                    numberOfStops = "2";
                    break;
            }
            return Convert.ToInt32(numberOfStops);            
        }
        #endregion
    }
}
