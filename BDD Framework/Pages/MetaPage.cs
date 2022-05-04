using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.Automation.BDDFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class MetaPage : MobileBasePage, IMetaPage
    {
        private MetaInformation _metaInformation;
        private readonly IAtWebDriver _webDriver;
        public MetaPage(IAtWebDriver webDriver, MetaInformation metaInformation)
            : base(webDriver)
        {
            _webDriver = webDriver;
            _metaInformation = metaInformation;
        }

        private bool IsRetrySearchComplete = false;

        #region[ WebElements]      
        private AtWebElement HotelSearch => _webDriver.FindElement(LocatorType.CssSelector, "a[href*=hotel]");
        private AtWebElement FlightSearch => _webDriver.FindElement(LocatorType.CssSelector, "a[href*=flight]");
        private AtWebElement PreProdEnvironment => _webDriver.FindElement(LocatorType.Id, "env1");
        private AtWebElement LiveEnvironment => _webDriver.FindElement(LocatorType.Id, "env2");
        private AtWebElement MetaChannel => _webDriver.FindElement(LocatorType.Id, "metachannel");
        private AtWebElement EstabIds => _webDriver.FindElement(LocatorType.Id, "estabIds");
        private AtWebElement CheckInDate => _webDriver.FindElement(LocatorType.Id, "checkInDate");
        private AtWebElement CheckOutDate => _webDriver.FindElement(LocatorType.Id, "checkOutDate");
        private AtWebElement Adults(int roomNo) => _webDriver.FindElement(LocatorType.XPath, "//input[@id='adults#']", roomNo.ToString());
        private AtWebElement ChildAges(int roomNo) => _webDriver.FindElement(LocatorType.XPath, "//input[@id='childAges#']", roomNo.ToString());
        private AtWebElement Search => _webDriver.FindElement(LocatorType.ClassName, "runTest");
        private AtBy byMetaDeepLink => GetBy(LocatorType.CssSelector, "div#finalLink a");
        private AtWebElement MetaDeepLink => _webDriver.FindElement(byMetaDeepLink);
        private AtWebElement TotalPrice(int noOfRooms) => _webDriver.FindElement(LocatorType.XPath, "//div[@id ='finalLink']/table//tr[#]//td[3]/b", (noOfRooms + 1).ToString());
        private AtBy byNoAvailabilityError => GetBy(LocatorType.XPath, "//b[contains(text(),'No availability')]");
        private AtWebElement NoAvailabilityError => _webDriver.FindElement(byNoAvailabilityError);
        private AtBy byMetaReference(string reference) => GetBy(LocatorType.XPath, "//body/img[contains(@src,'#')]", reference);
        private AtWebElement MetaImageReference(string reference) => _webDriver.FindElement(byMetaReference(reference));
        private AtBy byMetaFrame(string reference) => GetBy(LocatorType.XPath, "(//iframe[contains(@src,'#') and contains(@src,'fls')])", reference);
        private AtWebElement MetaFrame(string reference) => _webDriver.FindElement(byMetaFrame(reference));
        #endregion

        #region[HolidayWebElements]
        private AtWebElement HolidaySearch => _webDriver.FindElement(LocatorType.CssSelector, "a[href*=holiday]");
        private AtWebElement DepartureAirport => _webDriver.FindElement(LocatorType.Id, "departureAirport");
        private AtWebElement DestinationAirport => _webDriver.FindElement(LocatorType.Id, "destinationAirport");
        private AtWebElement CountryId => _webDriver.FindElement(LocatorType.Id, "countryId");
        private AtWebElement ProvinceId => _webDriver.FindElement(LocatorType.Id, "provinceId");
        private AtWebElement Cabin => _webDriver.FindElement(LocatorType.Id, "cabin");
        private AtWebElement AdultsonHoliday => _webDriver.FindElement(LocatorType.Id, "adults");
        private AtWebElement childAgesonHoliday => _webDriver.FindElement(LocatorType.Id, "childAges");
        private AtBy byHolidayMetaDeepLink => GetBy(LocatorType.CssSelector, "div#customLink a");
        private AtWebElement HolidayMetaDeepLink => _webDriver.FindElement(byHolidayMetaDeepLink);
        private AtWebElement HolidayTotalPrice => _webDriver.FindElement(LocatorType.XPath, "//tr/td[contains(text(),'TOTAL')]/following-sibling::td");
        private AtWebElement FlightPrice => _webDriver.FindElement(LocatorType.XPath, "//tr/td[contains(text(),'Flight')]/following-sibling::td");
        private AtWebElement HotelPrice => _webDriver.FindElement(LocatorType.XPath, "//tr/td[contains(text(),'Room price')]/following-sibling::td");
        private AtWebElement FlightPriceAlertMessage => _webDriver.FindElement(LocatorType.XPath, "//p[contains(text(),'children')]");

        #endregion


        #region[Methods]

        public void NavigateToMetaFranklinPage()
        {
            _webDriver.GotoUrl(MetaData.MetaUrl);
            //_webDriver.DeleteAllCookies();
        }

        public string GetFranklinPageTitle()
        {
            return _webDriver.Title;
        }

        public bool SelectProductOnFranklinPage(string product)
        {
            bool IsProductSelected = true;
            switch (product)
            {
                case "Hotel":if (HotelSearch.Visible)
                                 HotelSearch.Click();
                    break;
                case "Holiday":_metaInformation.IsHoliday = true;
                                if (HolidaySearch.Visible)
                                     HolidaySearch.Click();
                    break;
                case "Flight": if (FlightSearch.Visible)
                                 FlightSearch.Click();
                    break;
                default: IsProductSelected = false;
                    break;
            }
            if (IsProductSelected)
                return true;
            else
                return false;
        }

        public void SelectMetaEnvironemt(bool isLive)
        {
            if (isLive)
                LiveEnvironment.Check();
            else             
                PreProdEnvironment.Check();           
        }

        public int GetMetaCookieValue(string channel)
        {
            if (channel == "Trivago UK")
                return MetaCookies.TrivagoUK;
            else if (channel == "Trivago IE")
                return MetaCookies.TrivagoIE;
            else if (channel == "Trip Advisor (desktop) IE")
                return MetaCookies.TripAdvisorIE;
            else if (channel == "Trip Advisor (mobile) IE")
                return MetaCookies.TripAdvisorIEMobile;
            else if (channel == "Trip Advisor (desktop) UK")
                return MetaCookies.TripAdvisorUK;
            else if (channel == "Kayak UK" && _metaInformation.IsHoliday)
                return MetaCookies.KayakUKHoliday;
            else if (channel == "Kayak IE" && _metaInformation.IsHoliday)
                return MetaCookies.KayakIEHoliday;
            else if (channel == "Kayak UK")
                return MetaCookies.KayakUK;
            else if (channel == "Kayak IE")
                return MetaCookies.KayakIE;            
            else if (channel == "Trip Advisor (mobile) UK")
                return MetaCookies.TripAdvisorUKMobile;           
            else if (channel == "IceLolly UK")
                return MetaCookies.IceLollyUK;
            else if (channel == "Deal Checker UK")
                return MetaCookies.DealCharacterUK;
            else if (channel == "Travel Supermarket UK")
                return MetaCookies.TravelSuperMarketUK;
            else
                return 0;
        }

        public string GetMetaReference(string channel)
        {
            if ((channel == "Kayak IE" || channel == "Kayak UK") && _metaInformation.IsHoliday)
                return MetaReferences.KayakHoliday;
            else if (channel == "Kayak IE" || channel == "Kayak UK")
                return MetaReferences.KayakHotel;
            else if (channel == "Trivago UK" || channel == "Trivago IE")
                return MetaReferences.Trivago;
            else if (channel == "IceLolly UK")
                return MetaReferences.IceLolllyUK;
            else if (channel == "Trip Advisor (desktop) IE" || channel == "Trip Advisor (desktop) UK")
                return MetaReferences.TripAdvisorDesktop;
            else if (channel == "Trip Advisor (mobile) IE" || channel == "Trip Advisor (mobile) UK")
                return MetaReferences.TripAdvisorMobile;
            else
                return MetaReferences.TravelSuperMarket;
        }



        public void SetCheckinDate(int startdaysfromcurrentDate)
        {
            //CheckInDate.SendKeys(DateTime.Now.AddDays(startdaysfromcurrentDate).Month.ToString().PadLeft(2, '0'));
            //CheckInDate.SendKeys(DateTime.Now.AddDays(startdaysfromcurrentDate).Day.ToString());
            //CheckInDate.SendKeys(DateTime.Now.AddDays(startdaysfromcurrentDate).Year.ToString());
            CheckInDate.SendKeys(DateTime.Now.AddDays(startdaysfromcurrentDate).ToString("MMddyyyy"));
        }

        public void SetCheckOutDate(int startdaysfromcurrentDate, int duration)
        {
            CheckOutDate.SendKeys(DateTime.Now.AddDays(startdaysfromcurrentDate + duration).ToString("MMddyyyy"));
        }

        public void SetOccupancy(int noOfRooms, string occupancy)
        {
            //Rooms data
            string[] occupancyList = occupancy.Split(',');

            for (int rooms = 1; rooms <= noOfRooms; rooms++)
            {
                string childAges = "";
                for (int counter = 1; counter <= Convert.ToInt16(occupancyList[3 * rooms - 2]); counter++)
                {
                    childAges = childAges + Constants.ChildAge+",";
                }

                for (int innfantcounter = 1; innfantcounter <= Convert.ToInt16(occupancyList[3 * rooms - 1]); innfantcounter++)
                {
                    childAges = childAges + Constants.InfantAge + ",";
                }

                Adults(rooms).SendKeysUsingJs(occupancyList[3 * rooms - 3]);
                ChildAges(rooms).SendKeysUsingJs(childAges.Length >= 1 ? childAges.Substring(0, childAges.Length - 1) : "");
            }
        }

        public void SetOccupancyForHoliday(string occupancy)
        {
            //Rooms data
            string[] occupancyList = occupancy.Split(',');
            AdultsonHoliday.SendKeysUsingJs(occupancyList[0]);
            string childAges = "";
            for (int counter = 1; counter <= Convert.ToInt16(occupancyList[1]); counter++)
            {
                childAges +="8,";
            }
            for (int innfantcounter = 1; innfantcounter <= Convert.ToInt16(occupancyList[2]); innfantcounter++)
            {
                childAges +="0,";
            }

            childAgesonHoliday.SendKeysUsingJs(childAges.Length >= 1 ? childAges.Substring(0, childAges.Length - 1) : "");
        }

        public void PerformMetaHotelSearch(string channel, int noOfRooms = 1, string occupancy = "2,1,1", int startDaysFromCurrentDate = 20, int duration = 8, string estabIds = MetaData.EstabIds)
        {
            try
            {
                //Set the Supplier
                MetaChannel.SelectDropdownOptionByValue(channel);
                //EstabId to be searched 
                EstabIds.SendKeysUsingJs(estabIds);
                //Checkin and ChecOut Date 
                SetCheckinDate(startDaysFromCurrentDate);
                SetCheckOutDate(startDaysFromCurrentDate, duration);
                //set the Occupancy for rooms and search 
                SetOccupancy(noOfRooms, occupancy);
                Search.ClickButtonUsingJs();
                //Capture Total Price 
                _webDriver.WaitForAnyVisible(byMetaDeepLink, byNoAvailabilityError, Constants.LongWait, "Meta DeepLink is not visible");
                _metaInformation.TotalPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TotalPrice(noOfRooms).Text));
                _webDriver.WaitForElementClickable(byMetaDeepLink, Constants.LongWait);
                _webDriver.ScrollElementToCenter(MetaDeepLink);
                Console.WriteLine("Deep Link URL : " + MetaDeepLink.Text);
                MetaDeepLink.Click();
                Assert.IsTrue(_webDriver.IsNewWindowOpened(), "Estab page is not opened");
                _webDriver.SwitchToWindow(_webDriver.WindowHandles.Count - 1);
                new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.MediumWait)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(Constants.EstabPageTitle));
            }
            catch(Exception e)
            {
                if (!IsRetrySearchComplete && NoAvailabilityError.Displayed)
                {
                    IsRetrySearchComplete = true;
                    PerformMetaHotelSearch(channel,noOfRooms,occupancy,startDaysFromCurrentDate+10,duration+2,estabIds);
                }
                else if (NoAvailabilityError.Displayed)
                    Assert.Fail("No availability. Please try with different search criteria! "+e.Message);
            }
        }

        public void PerformMetaHolidaySearch(string metaChannel, string departureAirport, string destinationAirport, int countryID, int provinceID, int startDaysFromCurrentDate, int duration, string occupancy)
        {
            try
            {
                //Set the Supplier
                MetaChannel.SelectDropdownOptionByValue(metaChannel);
                //Enter Departure and Destination airports
                DepartureAirport.SendKeysUsingJs(departureAirport);
                DestinationAirport.SendKeysUsingJs(destinationAirport);
                //Enter Country and provinceId
                CountryId.SendKeysUsingJs(countryID.ToString());
                ProvinceId.SendKeysUsingJs(provinceID.ToString());
                //Checkin and ChecOut Date 
                SetCheckinDate(startDaysFromCurrentDate);
                SetCheckOutDate(startDaysFromCurrentDate, duration);
                //Enter the cabin
                Cabin.SendKeysUsingJs(MetaData.Cabin);
                //Set Occupancy and search
                SetOccupancyForHoliday(occupancy);
                Search.ClickButtonUsingJs();
                _webDriver.WaitForAnyVisible(byHolidayMetaDeepLink, byNoAvailabilityError, Constants.LongWait);
                //Capture Total Price
                _metaInformation.TotalPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HolidayTotalPrice.Text));
                _metaInformation.FlightPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(FlightPrice.Text));
                _metaInformation.HotelPrice = Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(HotelPrice.Text));
                _webDriver.ScrollElementToCenter(HolidayMetaDeepLink);
                Console.WriteLine("Deep Link URL : " + HolidayMetaDeepLink.Text);
                HolidayMetaDeepLink.Click();
                Assert.IsTrue(_webDriver.IsNewWindowOpened(), "Estab page is not opened");
                _webDriver.SwitchToWindow(_webDriver.WindowHandles.Count - 1);
                new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.MediumWait)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(Constants.EstabPageTitle));
            }
            catch(Exception e)
            {
                if (!IsRetrySearchComplete && NoAvailabilityError.Displayed)
                {
                    IsRetrySearchComplete = true;
                    PerformMetaHolidaySearch(metaChannel,departureAirport,destinationAirport,countryID,provinceID,startDaysFromCurrentDate+15,duration+3,occupancy);
                }      
                else if (NoAvailabilityError.Displayed)
                    Assert.Fail("No availability for different search criteria!! "+e.Message);
            }        
        }


        public decimal GetMetaTotalPrice()
        {
            return _metaInformation.TotalPrice;
        }

        public bool ValidateMetaReferencesInUrl(string channel)
        {
            if (_webDriver.Url.Contains("v3") && _webDriver.Url.Contains(GetMetaReference(channel)))
                return true;
            else
                return false;
        }

        public bool ValidateCookieReferences(string channel)
        {
            _metaInformation.TrpUtmId = Convert.ToDecimal(_webDriver.GetCookieValue("trpUTM"));
            _metaInformation.VisitSourceCookie = Convert.ToDecimal(_webDriver.GetCookieValue("visitSource"));
            Console.WriteLine(channel+"\n" + "TrpUtmID : " + _metaInformation.TrpUtmId + "  VisitSourceCookie : " + _metaInformation.VisitSourceCookie);
            if(_metaInformation.VisitSourceCookie == GetMetaCookieValue(channel))
                return true;
            else
                return false;
        }

        public bool ValidateMetaImageReference(string channel)
        {
            bool IsReferenceExists = false;
            _webDriver.WaitForElementPresent(byMetaFrame(GetMetaReference(channel)), Constants.MediumWait);
            _webDriver.SwitchToFrame(MetaFrame(GetMetaReference(channel)));
            _webDriver.WaitForElementVisible(byMetaReference(GetMetaReference(channel)), 20, "Meta reference is not visible");
            IsReferenceExists= MetaImageReference(GetMetaReference(channel)).GetAttribute("src").Contains(GetMetaReference(channel));
            Console.WriteLine("Image Reference : "+MetaImageReference(GetMetaReference(channel)).GetAttribute("src"));
            _webDriver.SwitchToDefaultContent();
            return IsReferenceExists;
        }
            
        public bool CheckFlightPriceAlertMessage()
        {
            return FlightPriceAlertMessage.Equals(MetaData.FlightPriceAlertmessage);
        }

        public bool ValiidateFlightAndRoomPrice(decimal totalPrice, decimal flightsTotalPrice, decimal hotelPrice,List<RoomOccupantDetails> roomOccupants)
        {
            bool IsPriceMatched = true;
            int noOfAdults = 0;
            int noOfChildren = 0;
            foreach(var roomOccupant in roomOccupants)
            {
                noOfAdults += roomOccupant.NoOfAdults;
                noOfChildren += roomOccupant.NoOfChildren + roomOccupant.NoOfInfants;
            }
            if (noOfChildren > 0)
            {
                decimal minPrice = _metaInformation.FlightPrice * noOfAdults;
                decimal maxPrice = _metaInformation.FlightPrice * (noOfAdults + noOfChildren);
                if(flightsTotalPrice < minPrice || flightsTotalPrice > maxPrice)
                    IsPriceMatched = false;
                Console.WriteLine(MetaData.FlightPriceAlertmessage + " : Total price will not Match ");
            }
            else
            {
                _metaInformation.FlightPrice = _metaInformation.FlightPrice * noOfAdults;
                if (_metaInformation.HotelPrice != hotelPrice)
                    IsPriceMatched = false;
                if (Math.Abs(flightsTotalPrice - _metaInformation.FlightPrice) > 1)
                    IsPriceMatched = false;
                if (Math.Abs(Math.Round(totalPrice) - _metaInformation.TotalPrice) > 1)
                    IsPriceMatched = false;
            }

            Console.WriteLine("Meta Hotel Price :" + _metaInformation.HotelPrice + "  HotelPrice in BookingConfirmation : " + hotelPrice);
            Console.WriteLine("Meta Flight Price :" + _metaInformation.FlightPrice + "  FlightPrice in BookingConfirmation : " + flightsTotalPrice);
            Console.WriteLine("Meta HolidayTotal Price :" + _metaInformation.TotalPrice + " Holiday in BookingConfirmation : " + totalPrice);
            return IsPriceMatched;
        }
        #endregion

    }
}
