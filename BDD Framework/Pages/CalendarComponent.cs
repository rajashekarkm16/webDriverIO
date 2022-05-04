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
    public class CalendarComponent : MobileBasePage, ICalendarComponent
    {
        private readonly IAtWebDriver _webDriver;
        private DateTime DepartureDate { get; set; }
        private DateTime ReturnDate { get; set; }

        private AtWebElement Header => _webDriver.FindElement(LocatorType.CssSelector, "header[class*=elevated] div[class*=space] h3[id*=active]");
        private AtBy ByDeparture => GetBy(LocatorType.XPath, "//div[text()='Departure']/following-sibling::div");
        private AtWebElement Departure => _webDriver.FindElement(ByDeparture);
        private AtBy ByReturn => GetBy(LocatorType.XPath, "//div[text()='Return']/following-sibling::div");
        private AtWebElement Return => _webDriver.FindElement(ByReturn);
        private AtBy byPreviousMonth => GetBy(LocatorType.XPath, "//button[contains(@class,'previous')]");
        private AtWebElement PreviousMonth => _webDriver.FindElement(byPreviousMonth);
        private AtWebElement NextMonth => _webDriver.FindElement(LocatorType.XPath, "//button[contains(@class,'next')]");
        private AtWebElement MonthAndYear => _webDriver.FindElement(LocatorType.XPath, "//div[@class='DayPicker-Caption']/div");
        private AtBy byResetLink => GetBy(LocatorType.XPath, "//a[contains(text(),'Reset')]");
        private AtWebElement ResetLink => _webDriver.FindElement(byResetLink);
        private AtWebElement Days(int index) => _webDriver.FindElementByIndex(LocatorType.XPath, "(//div[contains(@class,'DayPicker-Day')]//div[contains(@class,'number')])", index);
        private AtWebElement DaysField(int index) => _webDriver.FindElementByIndex(LocatorType.XPath, "//div[contains(@class,'number')]/parent::div/parent::div", index);
        private AtBy byConfirmDates => GetBy(LocatorType.XPath, "//button//span[text()='Confirm dates']");
        private AtWebElement ConfirmDates => _webDriver.FindElement(byConfirmDates);
        private AtWebElement CloseModal => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='When would you like to go?']/parent::div/following-sibling::div//button");
        private AtBy byErrorMessage => GetBy(LocatorType.XPath, "//div[contains(@class,'error')]/div[2]");
        private ReadOnlyCollection<AtWebElement> ErrorMessage => _webDriver.FindElements(byErrorMessage);


        public CalendarComponent(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public DateTime GetDepartureDate()
        {
            return DepartureDate;
        }

        public DateTime GetReturnDate()
        {
            return ReturnDate;
        }

        public void SetDepartureDate(DateTime departureDate)
        {
            DepartureDate = departureDate;
        }

        public void SetReturnDate(DateTime returnDate)
        {
            ReturnDate = returnDate;
        }

        public string GetCalendarPageTitle()
        {
            return Header.Text;
        }

        public void ClickResetLink()
        {
            System.Threading.Thread.Sleep(1000);
            if (!IsSelectedDatesNull())
            {
                _webDriver.WaitForElementClickable(byResetLink, 30);
                ResetLink.Click();
            }
        }

        public void SelectDates(int departure, int duration)
        {
            ClickResetLink();
            DepartureDate = DateTime.Now.AddDays(departure);
            ReturnDate = DateTime.Now.AddDays(departure + duration);
            //_webDriver.WaitForElementClickable(byPreviousMonth, 10);
            //while (!PreviousMonth.GetAttribute("class").Contains("Disabled") && PreviousMonth.Enabled)
            while (!MonthAndYear.Text.Equals(DateTime.Now.ToString(Constants.CalendarMonthYearLabel)))
                PreviousMonth.Click();
            while (!MonthAndYear.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                NextMonth.Click();
            _webDriver.WaitForElementClickable(Days(DepartureDate.Day), 10);
            Days(DepartureDate.Day).Click();
            while (!MonthAndYear.Text.Equals(ReturnDate.ToString(Constants.CalendarMonthYearLabel)))
                NextMonth.Click();
            _webDriver.WaitForElementClickable(Days(ReturnDate.Day), 10);
            Days(ReturnDate.Day).Click();
        }

        public void SelectDepartureDate(int departure)
        {
            ClickResetLink();
            DepartureDate = DateTime.Now.AddDays(departure);
            _webDriver.WaitForElementVisible(byPreviousMonth, 10, "Previous month is not visible");
            while (!MonthAndYear.Text.Equals(DateTime.Now.ToString(Constants.CalendarMonthYearLabel)))
                PreviousMonth.Click();
            while (!MonthAndYear.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                NextMonth.Click();
            _webDriver.WaitForElementClickable(Days(DepartureDate.Day), 10);
            Days(DepartureDate.Day).Click();
        }

        public void VerifySelectedDates(int departure, int duration)
        {
            DateTime departureDate = DateTime.Now.AddDays(departure);
            DateTime returnDate = DateTime.Now.AddDays(departure + duration);
            _webDriver.WaitForElementVisible(ByDeparture, Constants.DefaultWait, "Departure field is not visible");
            Assert.AreEqual(Departure.Text, departureDate.ToString(Constants.SelectedDateFormat));
            _webDriver.WaitForElementVisible(ByReturn, Constants.DefaultWait, "Return field is not visible");
            Assert.AreEqual(Return.Text, returnDate.ToString(Constants.SelectedDateFormat));
        }

        public bool IsSelectedDatesNull()
        {
            return Departure.Text.Contains("Select a date",StringComparison.OrdinalIgnoreCase) && Return.Text.Contains("Select a date", StringComparison.OrdinalIgnoreCase);
        }

        public void ConfirmTheDates()
        {
            _webDriver.WaitForElementClickable(byConfirmDates, 30);
            _webDriver.ScrollToElement(ConfirmDates);
            ConfirmDates.Click();
        }

        public void CloseDatePickerModal()
        {
            CloseModal.Click();
        }

        public string GetItinerary(int departure, int duration, List<RoomOccupantDetails> roomOccupantDetails)
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
            //if(_webDriver.Url.Contains("qa"))
            return string.Format("{0} - {1} ({2} {3}{4}) | {5} {6}{7}, {8} {9}{10}", departureDate.ToString("d MMM"), returnDate.ToString("d MMM"), Convert.ToInt16((returnDate.Subtract(departureDate).TotalDays)), "night", (returnDate.Subtract(departureDate).TotalDays) > 1 ? "s" : "", totalAdults, "adult", totalAdults > 1 ? "s" : "", totalChildren, "Child", totalChildren > 1 ? "ren" : "");
            //else
            //    return string.Format("{0} - {1} ({2} {3}{4}) | {5} {6}{7}, {8} {9}{10}", DepartureDate.ToString("dd MMM"), ReturnDate.ToString("dd MMM"), Convert.ToInt16((ReturnDate.Subtract(DepartureDate).TotalDays)), "nt", (ReturnDate.Subtract(DepartureDate).TotalDays) > 1 ? "s" : "", totalAdults, "adult", totalAdults > 1 ? "s" : "", totalChildren, "Child", totalChildren > 1 ? "ren" : "");
        }

        public string GetItinerary(DateTime departureDate, DateTime returnDate, List<RoomOccupantDetails> roomOccupantDetails)
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

        public string GetDateErrorMessage()
        {
            _webDriver.WaitForElementVisible(byErrorMessage, 5, "Error message is not visible");
            return ErrorMessage[0].Text;
        }

        public DateTime GetDate(int daysFromCurrentdate)
        {
            var currentDate = DateTime.Today;
            return currentDate.AddDays(daysFromCurrentdate);
        }

        public bool IsDateEnabled(int noOfDays)
        {
            DepartureDate = DateTime.Now.AddDays(noOfDays);
            while (!MonthAndYear.Text.Equals(DateTime.Now.ToString(Constants.CalendarMonthYearLabel)))
                PreviousMonth.Click();
            while (!MonthAndYear.Text.Equals(DepartureDate.ToString(Constants.CalendarMonthYearLabel)))
                NextMonth.Click();
            _webDriver.WaitForElementVisible(Days(DepartureDate.Day), 10);
            return DaysField(DepartureDate.Day).GetAttribute("aria-disabled").Equals("false");
        }
    }
}

