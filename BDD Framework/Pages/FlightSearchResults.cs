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
using NUnit.Framework;
using System.Threading;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class FlightSearchResults : MobileBasePage, IFlightSearchResults
    {
        

        #region[Constructor]
        private readonly IAtWebDriver _webDriver;
        private int flightToSelect = 0;
        private FlightInboundOutboudInformationModel flightInformation;

        public FlightSearchResults(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region[WebElements]
        private AtBy byFlightResults => GetBy(LocatorType.CssSelector, "article.sc-c-card");
        private ReadOnlyCollection<AtWebElement> FlightResults => _webDriver.FindElements(byFlightResults);
        private AtBy byAdditionalFlightResults => GetBy(LocatorType.CssSelector, "article[class=sc-c-card]");        
        private ReadOnlyCollection<AtWebElement> AdditionalFlightResults => _webDriver.FindElements(byAdditionalFlightResults);
        private AtBy ByPreSelectedFlightCard => GetBy(LocatorType.CssSelector, "article.sc-c-card--selected");
        private AtWebElement PreSelectedFlightCard => _webDriver.FindElement(ByPreSelectedFlightCard);
        private AtWebElement SelectFlightButton => _webDriver.FindElement(LocatorType.CssSelector, "button[class*=sc-c-button]");
        private AtWebElement PreSelectedFlightPrice => _webDriver.FindElement(LocatorType.CssSelector, "article.sc-c-card--selected div[class*=color-accent]");
        private AtWebElement FlightPrice => _webDriver.FindElement(LocatorType.CssSelector, "*[class*=color-body]");
        private AtBy byKeepFlight => GetBy(LocatorType.XPath, "//span[text()='Keep']");
        private AtWebElement KeepFlight => _webDriver.FindElement(byKeepFlight);
        private AtWebElement BaggageOptionNotificationText => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'notification')]//div[contains(@class,'fill-space')]/div[1]");
        private AtBy bySortOption => GetBy(LocatorType.XPath, "//span[text()='Sort by: ']/parent::div/a");
        private AtWebElement SortOption => _webDriver.FindElement(bySortOption);
        private AtBy byShowMoreFlightsButton => GetBy(LocatorType.XPath, "//span[text()='Show more flights']/parent::button");
        private AtWebElement ShowMoreFlightsButton => _webDriver.FindElement(byShowMoreFlightsButton);       
        private AtWebElement FlightDetails => _webDriver.FindElement(LocatorType.XPath, "//span[text()='flight details']");
        private AtBy byFlightsSortOption => GetBy(LocatorType.XPath, "//span[contains(text(),'Sort by')]/a");
        private AtWebElement FlightsSortOption => _webDriver.FindElement(byFlightsSortOption);
        private AtBy byFlightfilters => GetBy(LocatorType.XPath, "//span[contains(text(),'Filter')]/parent::button");
        private AtWebElement Flightfilters => _webDriver.FindElement(byFlightfilters);
        private AtBy byFlightFilterCount => GetBy(LocatorType.XPath, "//*[text()='Filters applied']/parent::div");
        private AtWebElement FlightFilterCount => _webDriver.FindElement(byFlightFilterCount);
        private AtBy byAccomadationAdjustmentPopUp => GetBy(LocatorType.XPath, "//table");
        private AtWebElement AccomadationAdjustmentPopUp => _webDriver.FindElement(byAccomadationAdjustmentPopUp);
        private AtWebElement InitialNumberOfDays => _webDriver.FindElement(LocatorType.XPath, "((//td[@class='sc-c-timeline-cell'])[1]//span[contains(@class, 'sc-u-bold')])[2]");
        private AtWebElement UpdatedNumberOfDays => _webDriver.FindElement(LocatorType.XPath, "((//td[@class='sc-c-timeline-cell'])[2]//span[contains(@class, 'sc-u-bold')])[2]");
        private AtWebElement AccomadationAdjustmentPopupProceed => _webDriver.FindElement(LocatorType.XPath, "//span[text()='Proceed']");

        #region [Flight Information locator elements]
        private AtBy ByFlightTimings => GetBy(LocatorType.CssSelector, "div[class*=flex-start] span[class*=color-primary][class*=--xl]");
        private ReadOnlyCollection<AtWebElement> FlightTimings => _webDriver.FindElements(ByFlightTimings);
        private AtBy ByFlightLogo => GetBy(LocatorType.CssSelector, "div[class*=flex-start] img");
        private ReadOnlyCollection<AtWebElement> FlightLogo => _webDriver.FindElements(ByFlightLogo);
        private AtBy ByDepartureLocation => GetBy(LocatorType.CssSelector, "div[class*=margin-bottom-min] div[class*=grid-item]:first-child");
        private ReadOnlyCollection<AtWebElement> DepartureLocation => _webDriver.FindElements(ByDepartureLocation);
        private AtBy ByArrivalLocation => GetBy(LocatorType.CssSelector, "div[class*=margin-bottom-min] div[class*=grid-item]:last-child");
        private ReadOnlyCollection<AtWebElement> ArrivalLocation => _webDriver.FindElements(ByArrivalLocation);
        private AtBy ByStops => GetBy(LocatorType.CssSelector, "div.sc-o-body--s span.sc-o-body");
        private ReadOnlyCollection<AtWebElement> Stops => _webDriver.FindElements(ByStops);
        private AtBy ByDuration => GetBy(LocatorType.CssSelector, "div[class*=sc-o-body--s][class*=xs]:not([class*=pale-grey])");
        private ReadOnlyCollection<AtWebElement> Duration => _webDriver.FindElements(ByDuration);
        private AtWebElement Price => _webDriver.FindElement(LocatorType.CssSelector, "*[class*=color-body]");
        private AtBy ByLegTimings => GetBy(LocatorType.CssSelector, "div[class*=space-between]");
        private AtBy ByAdditionalDays => GetBy(LocatorType.CssSelector, "span[class*=color-primary][class*=--s]");
        private ReadOnlyCollection<AtWebElement> PreSelectedFlightCardLeg => _webDriver.FindElements(LocatorType.CssSelector, "article.sc-c-card--selected div[class*=flex-start]");
        private AtBy ByFlightCarrier => GetBy(LocatorType.CssSelector, "div[class*=sc-o-body--s][class*=margin-top][class*=xs]");
        private ReadOnlyCollection<AtWebElement> FlightCarrier => _webDriver.FindElements(ByFlightCarrier);
        private AtBy BySearchResultsLoader => GetBy(LocatorType.XPath, "//*[contains(@class,'sc-c-modal__backdrop--light') or contains(@class, 'sc-c-throbber')]");
        private AtBy ByOutBoundFlightCarrier => GetBy(LocatorType.XPath, "//hr/preceding-sibling::div[contains(@class,'sc-o-body--s') and contains(@class,'xs') and contains(@class,'pale-grey')]");
        private AtBy ByInBoundFlightCarrier => GetBy(LocatorType.XPath, "//hr/following-sibling::div[contains(@class,'sc-o-body--s') and contains(@class,'xs') and contains(@class,'pale-grey')]");
        private AtBy ByFlightAllocationBaggageInfo => GetBy(LocatorType.CssSelector, "span.sc-u-color-success");
        private AtBy ByFlightAllocationSashInfo => GetBy(LocatorType.CssSelector, "div.sc-c-sash__label");
        private AtBy ByHandluggage => GetBy(LocatorType.XPath, "//span[contains(text(),'hold luggage')]");
        private AtWebElement HandLuggageInPreSelectedFligthCard => _webDriver.FindElement(LocatorType.XPath, "//article[contains(@class,'sc-c-card--selected')]//span[contains(text(),'hold luggage')]");
        private ReadOnlyCollection<AtWebElement> HandLuggageInAdditionalFligthCards => _webDriver.FindElements(LocatorType.XPath, "//article[@class='sc-c-card']//span[contains(text(),'hold luggage')]");
        #endregion

        #endregion

        #region[Methods]

        public int GetFlightToSelect()
        {
            RandomizeFlightToSelect();
            return flightToSelect;
        }

        public int GetTotalFlightCount()
        {
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 10, "Result loader is visible");
            _webDriver.WaitForElementVisible(byFlightResults, 30, "Flight results is not visible");
            return FlightResults.Count;
        }

        public bool IsPreselectedFlightCardDisplayed()
        {
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, Constants.MediumWait);
            return PreSelectedFlightCard.Visible;
        }

        public int GetAdditionalFlightResultsCount()
        {            
            //while (ShowMoreFlightsButton.Visible)
            //{
            //    ShowMoreFlightsButton.Click();
            //}
            return AdditionalFlightResults.Count;
        }

        public void LoadAllFlights()
        {
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byFlightResults, 30, "Flight results is not visible");
            while (ShowMoreFlightsButton.Visible)
            {
                _webDriver.ScrollElementToCenter(ShowMoreFlightsButton);
                ShowMoreFlightsButton.Click();
                _webDriver.WaitUntilNotVisible(BySearchResultsLoader, Constants.DefaultWait);
                _webDriver.WaitForElementVisible(byFlightResults, 60, "Flight results is not visible");
            }
        }

        public bool IsAdditionalFlightSearchResultsDisplayed()
        {
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byFlightResults, 60, "Flight results is not visible");
            return AdditionalFlightResults.Count > 0;
        }

        public void RandomizeFlightToSelect()
        {
            if(flightToSelect == 0)
            {
                _webDriver.WaitForElementVisible(byAdditionalFlightResults, 60, "Additional flight results is not visible");
                flightToSelect = HelperFunctions.RandomNumber(AdditionalFlightResults.Count);
            }
        }

        public void SelectAlternateFlight(int flightToSelect)
        {
            _webDriver.ScrollToElement(AdditionalFlightResults[flightToSelect - 1]);
            AdditionalFlightResults[flightToSelect - 1].FindElement(SelectFlightButton).Click();
            if(!IsAccomadationAdjustmentPopupDisplayed())
            {
                _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 90);
                //_webDriver.Refresh();
                //_webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
            } 
        }

        public bool IsAccomadationAdjustmentPopupDisplayed()
        {
            return AccomadationAdjustmentPopUp.Visible;
        }

        public bool AreNumberOfDaysDecreased()
        {
            Int32.TryParse(InitialNumberOfDays.Text, out int daysCount);
            Int32.TryParse(UpdatedNumberOfDays.Text, out int updatedDaysCount);
            if (daysCount > updatedDaysCount)
                return true;
            else
                return false;
        }

        public void AcceptAccomadationDateAdjustment()
        {
            AccomadationAdjustmentPopupProceed.Click();
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 90);
            //_webDriver.Refresh();
            //_webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
        }

        public void ViewPreSelectedFlightDetails()
        {
            _webDriver.ScrollElementToCenter(PreSelectedFlightCard);
            _webDriver.ScrollElementToCenter(PreSelectedFlightCard.FindElement(FlightDetails));
            PreSelectedFlightCard.FindElement(FlightDetails).Click();
        }

        public void ViewAlternateFlightDetails(int flightToSelect)
        {
            _webDriver.ScrollElementToCenter(AdditionalFlightResults[flightToSelect - 1].FindElement(FlightDetails));
            AdditionalFlightResults[flightToSelect - 1].FindElement(FlightDetails).Click();
        }

        public void KeepSelectedFlight()
        {
            _webDriver.WaitForElementClickable(byKeepFlight, 30);
            _webDriver.ScrollElementToCenter(KeepFlight);
            KeepFlight.Click();
            try
            {
                _webDriver.WaitForElementVisible(BySearchResultsLoader, 20, "Results loader is not visible");
            }
            catch
            {

            }
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
            //_webDriver.Refresh();
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
        }

        public string GetBaggageOptionText()
        {
            return BaggageOptionNotificationText.Text;
        }
        public bool IsSortOptionVisible()
        {
            return SortOption.Visible;
        }

        public bool IsShowMoreFlightsButtonVisible()
        {
            return ShowMoreFlightsButton.Visible;
        }

        public decimal GetPreSelectedFlightHolidayPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PreSelectedFlightPrice.Text));
        }

        public int GetShowMoreFlightsLocation()
        {
            _webDriver.WaitForDomReady();
            _webDriver.WaitForElementVisible(byShowMoreFlightsButton, 60, "Show More Flights button is not visible");
            return ShowMoreFlightsButton.Location.Y;
        }

        public void ClickShowMoreFlights()
        {
            if (IsShowMoreFlightsButtonVisible())
            {
                _webDriver.WaitForElementClickable(byShowMoreFlightsButton, 30);
                _webDriver.ScrollElementToCenter(ShowMoreFlightsButton);
                ShowMoreFlightsButton.Click();
                _webDriver.WaitForElementVisible(byFlightResults, 60, "Flight results is not visible");

            }            
        }

        public void OpenSortOptions()
        {
            _webDriver.WaitForElementClickable(byFlightsSortOption, 30);
            _webDriver.ScrollElementToCenter(FlightsSortOption);
            FlightsSortOption.Click();
        }

        public bool VerifyAppliedFlightSortOption(string option)
        {
            bool isSortApplied = true;            
            if (option.Equals("Price (Cheapest first)"))
            {
                double prevValue;
                double newValue;
                _webDriver.WaitUntilNotVisible(BySearchResultsLoader,30, "Search Results Loader is still visible");
                _webDriver.WaitForElementVisible(byAdditionalFlightResults, 30, "Additional flight results is not visible");
                if (ShowMoreFlightsButton.Visible)
                    ClickShowMoreFlights();
               
                prevValue = Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(AdditionalFlightResults[0].FindElement(FlightPrice).Text));

                foreach (var result in AdditionalFlightResults)
                {
                    System.Threading.Thread.Sleep(1000);
                    newValue = Convert.ToDouble(CommonFunctions.RemoveCurrencyInfo(result.FindElement(FlightPrice).Text));
                    if (prevValue <= newValue)
                        prevValue = newValue;
                    else
                    {
                        isSortApplied = false;
                        Console.WriteLine("Previous Value: " + prevValue + " | New Value: " + newValue);
                        break;
                    }
                }
            }
            else if(option.Equals("Duration (Quickest first)"))
            {
                TimeSpan outBoundDuration;
                TimeSpan inBoundDuration;
                TimeSpan prevTotalDuration;
                TimeSpan newTotalDuration;

                _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 30, "Search Results Loader is still visible");
                _webDriver.WaitForElementClickable(byAdditionalFlightResults, 30, "Additional flight results is not visible");
                if (ShowMoreFlightsButton.Visible)
                    ClickShowMoreFlights();

                string outBoundDurationText = AdditionalFlightResults[0].FindElements(ByDuration)[(int)FlightLegType.Outbound].Text;
                string inBoundDurationText = AdditionalFlightResults[0].FindElements(ByDuration)[(int)FlightLegType.Inbound].Text;

                outBoundDuration = new TimeSpan(Convert.ToInt16(outBoundDurationText.Split(',', 'h')[1]), Convert.ToInt16(outBoundDurationText.Split('h', 'm')[1]),0);
                inBoundDuration = new TimeSpan(Convert.ToInt16(inBoundDurationText.Split(',', 'h')[1]), Convert.ToInt16(inBoundDurationText.Split('h', 'm')[1]), 0);
                prevTotalDuration = outBoundDuration.Add(inBoundDuration);
                foreach (var result in AdditionalFlightResults)
                {
                    outBoundDurationText = result.FindElements(ByDuration)[(int)FlightLegType.Outbound].Text;
                    inBoundDurationText = result.FindElements(ByDuration)[(int)FlightLegType.Inbound].Text;

                    outBoundDuration = new TimeSpan(Convert.ToInt16(outBoundDurationText.Split(',', 'h')[1]), Convert.ToInt16(outBoundDurationText.Split('h', 'm')[1]), 0);
                    inBoundDuration = new TimeSpan(Convert.ToInt16(inBoundDurationText.Split(',', 'h')[1]), Convert.ToInt16(inBoundDurationText.Split('h', 'm')[1]), 0);
                    newTotalDuration = outBoundDuration.Add(inBoundDuration);
                    if (prevTotalDuration <= newTotalDuration)
                        prevTotalDuration = newTotalDuration;
                    else
                    {
                        isSortApplied = false;
                        Console.WriteLine("Previous Value: " + prevTotalDuration + " | New Value: " + newTotalDuration);
                        break;
                    }
                }

            }
            return isSortApplied;
        }

        public FlightLegInformationModel GetFlightLegInfo(int flightToSelect, FlightLegType flightType)
        {                        
            FlightLegInformationModel flightLegInfo = new FlightLegInformationModel();
            flightLegInfo.DepartureTime = AdditionalFlightResults[flightToSelect - 1].FindElements(ByFlightTimings)[(int)flightType].Text.Split("-")[0].Trim();
            flightLegInfo.ArrivalTime = AdditionalFlightResults[flightToSelect - 1].FindElements(ByFlightTimings)[(int)flightType].Text.Split("-")[1].Trim();
            flightLegInfo.DepartureLocation = AdditionalFlightResults[flightToSelect - 1].FindElements(ByDepartureLocation)[(int)flightType].Text;
            flightLegInfo.ArrivalLocation = AdditionalFlightResults[flightToSelect - 1].FindElements(ByArrivalLocation)[(int)flightType].Text;
            flightLegInfo.Stops = AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[(int)flightType].Text == "Direct" ? 0 : 
                Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[(int)flightType].Text));
            flightLegInfo.Duration = AdditionalFlightResults[flightToSelect - 1].FindElements(ByDuration)[(int)flightType].Text.Split(", ")[1];
            flightLegInfo.FlightLogo = AdditionalFlightResults[flightToSelect - 1].FindElements(ByFlightLogo)[(int)flightType].GetAttribute("src").Split("Airline/")[1];

            if(AdditionalFlightResults[flightToSelect - 1].FindElements(ByLegTimings)[(int)flightType].FindElements(ByAdditionalDays).Count > 0)
                flightLegInfo.AdditionalDays = Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(AdditionalFlightResults[flightToSelect - 1].FindElements(ByLegTimings)[(int)flightType].FindElements(ByAdditionalDays)[0].Text));
            if(flightType == FlightLegType.Outbound)
            {
                if (AdditionalFlightResults[flightToSelect - 1].FindElements(ByOutBoundFlightCarrier).Count > 0)
                    flightLegInfo.CarrierOperatedBy = AdditionalFlightResults[flightToSelect - 1].FindElement(ByOutBoundFlightCarrier).Text.Replace("Operated by ", "");
            }
            else if(flightType == FlightLegType.Inbound)
            {
                if (AdditionalFlightResults[flightToSelect - 1].FindElements(ByInBoundFlightCarrier).Count > 0)
                    flightLegInfo.CarrierOperatedBy = AdditionalFlightResults[flightToSelect - 1].FindElement(ByInBoundFlightCarrier).Text.Replace("Operated by ", "");
            }

            return flightLegInfo;
        }

        public FlightLegInformationModel GetDeaprtureAndArrivalTimefromFlightLegInfo(int flightToSelect, FlightLegType flightType)
        {
            FlightLegInformationModel flightLegInfo = new FlightLegInformationModel();
            flightLegInfo.DepartureTime = AdditionalFlightResults[flightToSelect - 1].FindElements(ByFlightTimings)[(int)flightType].Text.Split("-")[0].Trim();
            flightLegInfo.ArrivalTime = AdditionalFlightResults[flightToSelect - 1].FindElements(ByFlightTimings)[(int)flightType].Text.Split("-")[1].Trim();            
            return flightLegInfo;
        }

        public void CaptureFlightInformation(int flightToSelect)
        {
            flightInformation = new FlightInboundOutboudInformationModel();
            List<FlightLegInformationModel> flightLegs = new List<FlightLegInformationModel>();
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(byAdditionalFlightResults, 10, "Additional flight results is not visible");
            _webDriver.ScrollToElement(AdditionalFlightResults[flightToSelect - 1]);
            flightInformation.Price = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(AdditionalFlightResults[flightToSelect - 1].FindElement(Price).Text));
            flightLegs.Add(GetFlightLegInfo(flightToSelect, FlightLegType.Outbound));
            flightLegs.Add(GetFlightLegInfo(flightToSelect, FlightLegType.Inbound));
            flightInformation.flightLeg = flightLegs;
        }

        public void CapturePreSelectedFlightInformation()
        {
            flightInformation = new FlightInboundOutboudInformationModel();
            List<FlightLegInformationModel> flightLegs = new List<FlightLegInformationModel>();
            _webDriver.WaitUntilNotVisible(BySearchResultsLoader, 60);
            _webDriver.WaitForElementVisible(ByPreSelectedFlightCard, 30, "PreSelected Flight Card is not visible");
            flightInformation.Price = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(PreSelectedFlightCard.FindElement(PreSelectedFlightPrice).Text));
            flightLegs.Add(GetPreSelectedFlightLegInfo(FlightLegType.Outbound));
            flightLegs.Add(GetPreSelectedFlightLegInfo(FlightLegType.Inbound));
            flightInformation.flightLeg = flightLegs;
        }

        public FlightLegInformationModel GetPreSelectedFlightLegInfo(FlightLegType flightType)
        {
            FlightLegInformationModel flightLegInfo = new FlightLegInformationModel();
            flightLegInfo.DepartureTime = PreSelectedFlightCard.FindElements(ByFlightTimings)[(int)flightType].Text.Split("-")[0].Trim();
            flightLegInfo.ArrivalTime = PreSelectedFlightCard.FindElements(ByFlightTimings)[(int)flightType].Text.Split("-")[1].Trim();
            flightLegInfo.DepartureLocation = PreSelectedFlightCard.FindElements(ByDepartureLocation)[(int)flightType].Text;
            flightLegInfo.ArrivalLocation = PreSelectedFlightCard.FindElements(ByArrivalLocation)[(int)flightType].Text;
            flightLegInfo.Stops = PreSelectedFlightCard.FindElements(ByStops)[(int)flightType].Text == "Direct" ? 0 :
                Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(PreSelectedFlightCard.FindElements(ByStops)[(int)flightType].Text));
            flightLegInfo.Duration = PreSelectedFlightCard.FindElements(ByDuration)[(int)flightType].Text.Split(", ")[1];
            flightLegInfo.FlightLogo = PreSelectedFlightCard.FindElements(ByFlightLogo)[(int)flightType].GetAttribute("src").Split("Airline/")[1];

            if (PreSelectedFlightCard.FindElements(ByLegTimings)[(int)flightType].FindElements(ByAdditionalDays).Count > 0)
                flightLegInfo.AdditionalDays = Convert.ToInt16(CommonFunctions.RemoveCurrencyInfo(PreSelectedFlightCard.FindElements(ByLegTimings)[(int)flightType].FindElements(ByAdditionalDays)[0].Text));
            if (flightType == FlightLegType.Outbound)
            {
                if (PreSelectedFlightCard.FindElements(ByOutBoundFlightCarrier).Count > 0)
                    flightLegInfo.CarrierOperatedBy = PreSelectedFlightCard.FindElement(ByOutBoundFlightCarrier).Text.Replace("Operated by ", "");
            }
            else if (flightType == FlightLegType.Inbound)
            {
                if (PreSelectedFlightCard.FindElements(ByInBoundFlightCarrier).Count > 0)
                    flightLegInfo.CarrierOperatedBy = PreSelectedFlightCard.FindElement(ByInBoundFlightCarrier).Text.Replace("Operated by ", "");
            }

            return flightLegInfo;
        }

        public FlightInboundOutboudInformationModel GetFlightInformation()
        {
            return flightInformation;
        }

        public FlightInboundOutboudInformationModel CaptureAndReturnFlightInformation(int flightToSelect)
        {
            CaptureFlightInformation(flightToSelect);
            return GetFlightInformation();
        }

        public bool IsArrivingOnFutureDays()
        {
            int flightToSelect = 1;
            if (PreSelectedFlightCard.FindElements(ByLegTimings)[(int)FlightLegType.Outbound].FindElements(ByAdditionalDays).Count > 0)
                return true;
            
            while (flightToSelect<=AdditionalFlightResults.Count)
            {
                if (AdditionalFlightResults[flightToSelect - 1].FindElements(ByLegTimings)[(int)FlightLegType.Outbound].FindElements(ByAdditionalDays).Count > 0)
                {
                    return true;
                }             
                    flightToSelect++;
            }
            return false;
        }

        public bool ValidateDepartureAirport(string depAirport)
        {
            _webDriver.WaitForElementVisible(byFlightResults, 30, "Flight results is not visible");
            bool IsDepAirportSame = true;
            CapturePreSelectedFlightInformation();
            FlightInboundOutboudInformationModel preSelectedFlightInfo = flightInformation;
            if (!depAirport.Equals(preSelectedFlightInfo.flightLeg[(int)FlightLegType.Outbound].DepartureLocation, StringComparison.OrdinalIgnoreCase))
            {
                IsDepAirportSame = false;
                return IsDepAirportSame;
            }

            for (int i=1; i<FlightResults.Count; i++)
            {
                FlightInboundOutboudInformationModel info = CaptureAndReturnFlightInformation(i);
                if (!preSelectedFlightInfo.flightLeg[(int)FlightLegType.Outbound].DepartureLocation.Equals(info.flightLeg[(int)FlightLegType.Outbound].DepartureLocation))
                    IsDepAirportSame = false;
            }
            return IsDepAirportSame;
        }

        public List<FlightInboundOutboudInformationModel> CaptureAllFlightResults()
        {
            List<FlightInboundOutboudInformationModel> flightsInfo = new List<FlightInboundOutboudInformationModel>();            
            _webDriver.WaitForElementVisible(byAdditionalFlightResults, 30, "Additional Flight results is not visible");
            for (int resultCounter = 1; resultCounter < FlightResults.Count; resultCounter++)
            {
                flightsInfo.Add(CaptureAndReturnFlightInformation(resultCounter));
            }
            return flightsInfo;
        }

        public bool ValidateFlightResults(List<FlightInboundOutboudInformationModel> oldFlightsInfo, List<FlightInboundOutboudInformationModel> newFlightsInfo)
        {
            bool isResultsMatch = true;
            for(int flightResult = 0; flightResult < oldFlightsInfo.Count; flightResult++)
            {
                    if (!oldFlightsInfo[flightResult].flightLeg[1].DepartureLocation.Equals(newFlightsInfo[flightResult].flightLeg[1].DepartureLocation))
                    {
                        isResultsMatch = false;
                        break;
                    }
                
            }
            return isResultsMatch;
        }
       
        public int GetOpenJawFlight()
        {
            int flightToSelect = 1;
            bool isAvailable = false;

            while (AdditionalFlightResults[flightToSelect - 1].Visible)
            {
                if (!AdditionalFlightResults[flightToSelect - 1].FindElement(DepartureLocation[0]).Text.Equals(AdditionalFlightResults[flightToSelect - 1].FindElement(ArrivalLocation[1]).Text))
                {
                    isAvailable = true;
                    break;
                }
                else
                    flightToSelect++;
            }
            Assert.IsTrue(isAvailable, "Open jaw flight is not available");
            return flightToSelect;
        }

        public int GetConnectingFlight()
        {
            int flightToSelect = 1;
            bool isAvailable = false;

            while (flightToSelect <= AdditionalFlightResults.Count)
            {
                if (!AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[0].Text.Equals("Direct"))
                {
                    isAvailable = true;
                    break;
                }
                else
                    flightToSelect++;
            }
            Assert.IsTrue(isAvailable, "Connecting flights is not available");
            return flightToSelect;
        }

        public int GetDirectFlightDifferentCarrier()
        {
            int flightToSelect = 1;
            bool isAvailable = false;

            while (flightToSelect <= AdditionalFlightResults.Count)
            {
                if ((AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[0].Text.Equals("Direct") 
                    && AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[1].Text.Equals("Direct"))
                    && ((AdditionalFlightResults[flightToSelect - 1].FindElements(ByOutBoundFlightCarrier).Count > 0)
                    || (AdditionalFlightResults[flightToSelect - 1].FindElements(ByInBoundFlightCarrier).Count > 0)))
                {
                    isAvailable = true;
                    break;
                }
                else
                    flightToSelect++;
            }
            Assert.IsTrue(isAvailable, "Flights with different carrier is not available");
            return flightToSelect;
        }

        public int GetDirectFlightSameCarrier()
        {
            int flightToSelect = 1;
            bool isAvailable = false;

            while (flightToSelect <= AdditionalFlightResults.Count)
            {
                if ((AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[0].Text.Equals("Direct")
                    && AdditionalFlightResults[flightToSelect - 1].FindElements(ByStops)[1].Text.Equals("Direct"))
                    && ((AdditionalFlightResults[flightToSelect - 1].FindElements(ByOutBoundFlightCarrier).Count == 0)
                    || (AdditionalFlightResults[flightToSelect - 1].FindElements(ByInBoundFlightCarrier).Count == 0)))
                {
                    isAvailable = true;
                    break;
                }
                else
                    flightToSelect++;
            }
            Assert.IsTrue(isAvailable, "Flights with same carrier is not available");
            return flightToSelect;
        }

        public int GetDifferentCarrierForEachFlight()
        {
            int flightToSelect = 1;
            bool isAvailable = false;

            while (flightToSelect <= AdditionalFlightResults.Count)
            {
                if (AdditionalFlightResults[flightToSelect - 1].FindElements(ByFlightCarrier).Count == 2)
                {
                    //if (!AdditionalFlightResults[flightToSelect - 1].FindElements(ByLegs)[0].FindElements(ByFlightCarrier)[0].Text.Equals(AdditionalFlightResults[flightToSelect - 1].FindElements(ByLegs)[0].FindElements(ByFlightCarrier)[1].Text))
                    //{
                        isAvailable = true;
                        break;
                    //}
                }
                else
                    flightToSelect++;
            }
            Assert.IsTrue(isAvailable, "Flights with different carrier for each way is not available");
            return flightToSelect;
        }

        public void OpenFlightFilters()
        {
            _webDriver.WaitForElementClickable(byFlightfilters, 60);            
            _webDriver.ScrollElementToCenter(Flightfilters);
            Flightfilters.ClickButtonUsingJs();            
        }

        public bool IsFlightFiltersVisible()
        {
            return Flightfilters.Visible;
        }

        public int GetFilteredResultsCount()
        {
            _webDriver.WaitForElementVisible(byFlightResults, 60, "Flight results is not visible");
            _webDriver.WaitForElementVisible(byFlightFilterCount, 10, "Flight filter count is not visible");
            return Convert.ToInt32(CommonFunctions.RemoveCurrencyInfo(FlightFilterCount.Text));
        }

        public bool IsFilteredMessageVisisble()
        {
            _webDriver.WaitForElementVisible(byFlightResults, 30, "Flight results is not visible");
            return FlightFilterCount.Visible;
        }

        public string GetFlightAllocationBaggageText()
        {
            _webDriver.WaitForElementVisible(ByPreSelectedFlightCard, 10, "Flight card is not visible");
            return PreSelectedFlightCard.FindElement(ByFlightAllocationBaggageInfo).Text;
        }

        public string GetFlightAllocationSashText()
        {
            _webDriver.WaitForElementVisible(ByPreSelectedFlightCard, 10, "Flight card is not visible");
            return PreSelectedFlightCard.FindElement(ByFlightAllocationSashInfo).Text;
        }

        public bool CompareFlightLegs(FlightLegInformationModel prevValue, FlightLegInformationModel newValue)
        {
            if (prevValue.DepartureLocation != newValue.DepartureLocation || prevValue.DepartureTime != newValue.DepartureTime
                || prevValue.ArrivalLocation != newValue.ArrivalLocation || prevValue.ArrivalTime != newValue.ArrivalTime
                || prevValue.Duration != newValue.Duration || prevValue.FlightLogo != newValue.FlightLogo)
                return false;
            else
                return true;
        }


        public bool VerifyFlightStopOverInfo(string stopOverInfo)
        {

            if (stopOverInfo.Contains("Direct") || stopOverInfo.Contains("1 Stop") || stopOverInfo.Contains("2 Stop"))
                return true;
            else
                return false;
        }

        public string GetStopOverInfoInPreSelctedFlight(FlightType flightType)
        {
            return  PreSelectedFlightCard.FindElements(ByStops)[(int)flightType].Text;
        }

        public bool VerifyStopOverInfoInAdditionalFlightCards(FlightType flightType)
        {
            bool result = false;
                int flightCount = AdditionalFlightResults.Count;                      
                while (flightCount>0)
                {
                    result = VerifyFlightStopOverInfo(AdditionalFlightResults[flightCount - 1].FindElements(ByStops)[(int)flightType].Text);
                    if (!result)
                        break;
                    flightCount--;
                }                           
            return result;
        }


        public string GetHoldLuggageMessageonPreSelectedFligthCard()
        {
            _webDriver.WaitForElementVisible(ByHandluggage, Constants.MediumWait, "HandLuggage is not visible");
            return HandLuggageInPreSelectedFligthCard.Text;
        }

        public bool IsHoldLuggageMessageDisplayedOnAnyFlightCard()
        {           
            if (HandLuggageInPreSelectedFligthCard.Text.Equals(Constants.HandLuggageMessage))
                return true;
            int additionalFlightResultCount = AdditionalFlightResults.Count;
            while (additionalFlightResultCount > 0)
            {                 
                if (HandLuggageInAdditionalFligthCards[additionalFlightResultCount - 1].Text.Equals(Constants.HandLuggageMessage))
                    return true;
                additionalFlightResultCount--;
            }
            return false;
        }

        public bool IsHoldLuggageMessageDisplayedOnPreSelectedFlightCard()
        {
            return HandLuggageInPreSelectedFligthCard.Text.Equals(Constants.HandLuggageMessage);
        }

        #endregion
    }
}
