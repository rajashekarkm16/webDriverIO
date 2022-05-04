using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class LandingPageCalendarComponent : CalendarComponent, ILandingPageCalendarComponent
    {
        private readonly IAtWebDriver _webDriver;
        private DateTime DepartureDate { get; set; }
        private DateTime ReturnDate { get; set; }      
        private AtWebElement Header => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-popper']//span");
        private AtBy BySelectionSummary => GetBy(LocatorType.XPath, "(//div[@class='sc-c-popper' or @class='sc-u-padding-m sc-u-elevation-1']//span[@class='sc-o-body sc-u-bold'])[last()]");
        private AtWebElement SelectionSummary => _webDriver.FindElement(BySelectionSummary);
        private AtBy byPreviousMonth => GetBy(LocatorType.XPath, "//button[contains(@class,'previous')]");
        private AtWebElement PreviousMonth => _webDriver.FindElement(byPreviousMonth);
        private AtWebElement NextMonth => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@class,'next')]");
        private AtWebElement MonthAndYear => _webDriver.FindElement(LocatorType.XPath, "(//div[@class='DayPicker-Caption'])[1]/div");
        private ReadOnlyCollection<AtWebElement> MonthAndYears => _webDriver.FindElements(LocatorType.XPath, "//div[@class='DayPicker-Caption']/div");
        private AtBy byResetLink => GetBy(LocatorType.XPath, "//a[contains(text(),'Reset')]");
        private AtWebElement ResetLink => _webDriver.FindElement(byResetLink);
        private AtWebElement SelectionDay(string MonthYear, int day) => _webDriver.FindElement(LocatorType.XPath, "(//div[div[@class='DayPicker-Caption']/div[.='#']]/div[@class='DayPicker-Body']//div[contains(@class,'DayPicker-Day')]/div[@class='DayPicker-Day__day-number'])[#]", MonthYear.ToString(),day.ToString());
        private AtBy byDays => GetBy(LocatorType.XPath, "(//div[contains(@class,'DayPicker-Day')]//div[contains(@class,'number')])");
        private AtWebElement Days(int index) => _webDriver.FindElementByIndex(byDays, index);
        private AtWebElement DaysField(int index) => _webDriver.FindElementByIndex(LocatorType.XPath, "//div[contains(@class,'number')]/parent::div/parent::div", index);
        private AtBy byConfirmDates => GetBy(LocatorType.XPath, "//button//span[text()='Done' or text()='Next' or text()='Confirm dates']");
        private AtWebElement ConfirmDates => _webDriver.FindElement(byConfirmDates);
        private AtWebElement CloseModal => _webDriver.FindElement(LocatorType.XPath, "//div[@class='sc-c-popper']//button[@aria-label='Close popup']");
        private AtBy byErrorMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'error')]/div[2]");
        private ReadOnlyCollection<AtWebElement> ErrorMessage => _webDriver.FindElements(byErrorMessage);


        public LandingPageCalendarComponent(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public new DateTime GetDepartureDate()
        {
            return DepartureDate;
        }

        public new DateTime GetReturnDate()
        {
            return ReturnDate;
        }

        public new void SetDepartureDate(DateTime departureDate)
        {
            DepartureDate = departureDate;
        }

        public new void SetReturnDate(DateTime returnDate)
        {
            ReturnDate = returnDate;
        }

        public new string GetCalendarPageTitle()
        {
            return Header.Text;
        }

        public new void ClickResetLink()
        {
            System.Threading.Thread.Sleep(1000);
            if (!IsSelectedDatesNull())
            {
                _webDriver.WaitForElementClickable(byResetLink, 30);
                ResetLink.Click();
            }
        }

        public new void SelectDates(int departure, int duration)
        {
            DepartureDate = DateTime.Now.AddDays(departure);
            ReturnDate = DateTime.Now.AddDays(departure + duration);
            System.Threading.Thread.Sleep(2000);
            if (!HelperFunctions.IsDesktop() && !NextMonth.Visible)
            {
                foreach (var element in MonthAndYears)
                {
                    if (element.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                    {
                        _webDriver.ScrollToElement(element);
                        break;
                    }
                    else
                    {
                        _webDriver.ScrollToElement(element);
                    }
                }
                SelectionDay(DepartureDate.ToString(Constants.CalendarMonthYearLabel), DepartureDate.Day).Click();
                foreach (var element in MonthAndYears)
                {
                    if (element.Text.Equals(ReturnDate.ToString(Constants.CalendarMonthYearLabel)))
                    {
                        _webDriver.ScrollToElement(element);
                        break;
                    }
                    else
                    {
                        _webDriver.ScrollToElement(element);
                    }
                }
                SelectionDay(ReturnDate.ToString(Constants.CalendarMonthYearLabel), ReturnDate.Day).Click();
            }
            else
            {
                while (!MonthAndYear.Text.Equals(DateTime.Now.ToString(Constants.CalendarMonthYearLabel)))
                    PreviousMonth.Click();
                while (!MonthAndYear.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                    NextMonth.Click();
                _webDriver.WaitForElementClickable(byDays, 10, "Days are not visible");
                Days(DepartureDate.Day).Click();
                while (!MonthAndYear.Text.Equals(ReturnDate.ToString(Constants.CalendarMonthYearLabel)))
                    NextMonth.Click();
                _webDriver.WaitForElementClickable(byDays, 10, "Days are not visible");
                Days(ReturnDate.Day).Click();
            }
        }

        public new void SelectDepartureDate(int departure)
        {
            DepartureDate = DateTime.Now.AddDays(departure);
            _webDriver.WaitForElementVisible(byPreviousMonth, 10, "Previous month is not visible");
            while (!MonthAndYear.Text.Equals(DateTime.Now.ToString(Constants.CalendarMonthYearLabel)))
                PreviousMonth.Click();
            while (!MonthAndYear.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                NextMonth.Click();
            _webDriver.WaitForElementClickable(byDays, 10, "Days are not visible");
            Days(DepartureDate.Day).Click();
        }

        public new void VerifySelectedDates(int departure, int duration)
        {
            DateTime departureDate = DateTime.Now.AddDays(departure);
            DateTime returnDate = DateTime.Now.AddDays(departure + duration);
            _webDriver.WaitForElementVisible(BySelectionSummary, 10, "Departure field is not visible");           
            Assert.IsTrue(SelectionSummary.Text.Contains(departureDate.ToString(Constants.SelectedDateFormat)));
            Assert.IsTrue(SelectionSummary.Text.Contains(returnDate.ToString(Constants.SelectedDateFormat)));
        }

        public new bool IsSelectedDatesNull()
        {
            return SelectionSummary.Text.Equals("Select a departure date");
        }

        public new void ConfirmTheDates()
        {
            //_webDriver.WaitForElementClickable(byConfirmDates, 30);
            //_webDriver.ScrollToElement(ConfirmDates);
            ConfirmDates.ClickButtonUsingJs();
        }

        public new void CloseDatePickerModal()
        {
            CloseModal.Click();
        }

        public new string GetItinerary(int departure, int duration, List<RoomOccupantDetails> roomOccupantDetails)
        {
            DateTime departureDate = DateTime.Now.AddDays(departure);
            DateTime returnDate = DateTime.Now.AddDays(departure + duration);
            int totalAdults = 0;
            int totalChildren = 0;
            roomOccupantDetails.ForEach(occupants =>
            {
                totalAdults += occupants.NoOfAdults;
                totalChildren = totalChildren + occupants.NoOfChildren + occupants.NoOfInfants;
            });
            return string.Format("{0} - {1} ({2} {3}{4}) | {5} {6}{7}, {8} {9}{10}", departureDate.ToString("d MMM"), returnDate.ToString("d MMM"), Convert.ToInt16((returnDate.Subtract(departureDate).TotalDays)), "night", (returnDate.Subtract(departureDate).TotalDays) > 1 ? "s" : "", totalAdults, "adult", totalAdults > 1 ? "s" : "", totalChildren, "Child", totalChildren > 1 ? "ren" : "");
        }

        public new string GetItinerary(DateTime departureDate, DateTime returnDate, List<RoomOccupantDetails> roomOccupantDetails)
        {
            int totalAdults = 0;
            int totalChildren = 0;
            roomOccupantDetails.ForEach(occupants =>
            {
                totalAdults += occupants.NoOfAdults;
                totalChildren = totalChildren + occupants.NoOfChildren + occupants.NoOfInfants;
            });

            return string.Format("{0} - {1} ({2} {3}{4}) | {5} {6}{7}{8}{9}",
                departureDate.ToString("d MMM"),
                returnDate.ToString("d MMM"),
                Convert.ToInt16((returnDate.Subtract(departureDate).TotalDays)),
                "night",
                (returnDate.Subtract(departureDate).TotalDays) > 1 ? "s" : "",
                totalAdults,
                "adult",
                totalAdults > 1 ? "s" : "",
                totalChildren > 0 ? ", " + totalChildren + " Child" : "",
                totalChildren > 1 ? "ren" : "");
        }

        public new string GetDateErrorMessage()
        {
            _webDriver.WaitForElementVisible(byErrorMessage, 5, "Error message is not visible");
            return ErrorMessage[0].Text;
        }

        public new DateTime GetDate(int daysFromCurrentdate)
        {
            var currentDate = DateTime.Today;
            return currentDate.AddDays(daysFromCurrentdate);
        }

        public new bool IsDateEnabled(int noOfDays)
        {
            DepartureDate = DateTime.Now.AddDays(noOfDays);
            while (!MonthAndYear.Text.Equals(DateTime.Now.ToString(Constants.CalendarMonthYearLabel)))
                PreviousMonth.Click();
            while (!MonthAndYear.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                NextMonth.Click();
            _webDriver.WaitForElementVisible(byDays, 10, "Days are not visible in calendar");
            return DaysField(DepartureDate.Day).GetAttribute("aria-disabled").Equals("false");
        }
    }
}

