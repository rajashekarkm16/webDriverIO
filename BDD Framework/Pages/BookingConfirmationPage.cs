using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Enums;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class BookingConfirmationPage :MobileBasePage, IBookingConfirmationPage
    {
        private readonly IAtWebDriver _webDriver;
        private List<string> BookingID;

        private AtBy byBookingReference => GetBy(LocatorType.XPath, "//div[contains(text(),'REFERENCE')]/parent::div//span | //div[contains(@class,'tertiary')]/following-sibling::div//div[contains(text(),'REF')]");
        private ReadOnlyCollection<AtWebElement> BookingReference => _webDriver.FindElements(byBookingReference);
        private AtBy byPageLoader => GetBy(LocatorType.XPath, "//div[contains(@class,'sc-c-modal__backdrop--light')]");
        private ReadOnlyCollection<AtWebElement> ProductSection => _webDriver.FindElements(LocatorType.CssSelector, "div.sc-u-padding-x-default.sc-u-padding-top-m");
        private ReadOnlyCollection<AtWebElement> BookedItemName => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'x-default') and contains(@class,'top-m')]//*[contains(@class,'heading')]");
        private AtBy byStarRating => GetBy(LocatorType.XPath, "//div[contains(@class,'x-default') and contains(@class,'top-m')]//*[contains(@class,'star-rating')]/*");
        private ReadOnlyCollection<AtWebElement> StarRating => _webDriver.FindElements(byStarRating);
        private AtWebElement Itinerary => _webDriver.FindElement(LocatorType.XPath, "//div[contains(@class,'x-default') and contains(@class,'top-m')]//*[contains(text(),'night')]");
        private ReadOnlyCollection<AtWebElement> RoomType => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'x-default') and contains(@class,'top-m')]//*[contains(@class,'display-block')][1]");
        private ReadOnlyCollection<AtWebElement> BoardType => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'x-default') and contains(@class,'top-m')]//*[contains(@class,'display-block')][2]");
        private ReadOnlyCollection<AtWebElement> RoomOccupants => _webDriver.FindElements(LocatorType.XPath, "//div[contains(@class,'x-default') and contains(@class,'top-m')]//*[contains(@class,'display-block')][3]");
        private AtWebElement ProductDescription(string ProductName) => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'# total')]/parent::div", ProductName);
        private AtWebElement ProductPrice(string ProductName) => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'# total')]/parent::div/following-sibling::*/*[contains(@class,'bold')]", ProductName);
        private ReadOnlyCollection<AtWebElement> FlightLegs => _webDriver.FindElements(LocatorType.CssSelector, "div.sc-u-margin-y-m");
        private ReadOnlyCollection<AtWebElement> FlightTimings => _webDriver.FindElements(LocatorType.CssSelector, "div.sc-u-margin-y-m span");
        private AtWebElement TotalPrice => _webDriver.FindElement(LocatorType.XPath, "(//span[contains(@class,'sc-u-color-accent')]//span[@class='sc-o-body sc-o-body--xl'])[1] | (//span[text()='Total price']/parent::div/following-sibling::div/span)[1]");
        private AtWebElement DepositPaid => _webDriver.FindElement(LocatorType.XPath, "//div[contains(text(),'deposit')]/following-sibling::div/span");
        private AtWebElement BalanceAmount => _webDriver.FindElement(LocatorType.XPath, "//div[contains(text(),'Balance')]/following-sibling::div/span");
        private AtWebElement TransferName => _webDriver.FindElement(LocatorType.XPath, "(//*[contains(text(),'TRANSFERS')]/parent::div//following-sibling::div/*[contains(@class,'heading')])[1]");
        private AtWebElement InsuranceName => _webDriver.FindElement(LocatorType.XPath, "(//*[text()='Insurance'])[1]/following-sibling::div");
        private ReadOnlyCollection<AtWebElement> BalancePayInstallments => _webDriver.FindElements(LocatorType.XPath, "//*[text()='Still to pay']/parent::div//span[contains(@class,'sc-u-bold')]");
        private AtWebElement LocalCharges => _webDriver.FindElement(LocatorType.XPath, "//*[contains(text(),'Local charges')]/parent::div//span");

        public BookingConfirmationPage(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        public List<string> GetBookingDetails()
        {
            BookingID = new List<string>();
            if(!HelperFunctions.IsLive())
            {
                _webDriver.WaitUntilNotVisible(byPageLoader, 120);
                _webDriver.WaitForElementVisible(byBookingReference, 60, "Booking Reference is not visible");
                foreach (var reference in BookingReference)
                {
                    if (reference.Text.Contains("REF:"))
                        BookingID.Add(reference.Text.Replace("REF: ", ""));
                    else
                        BookingID.Add(reference.Text);
                }
            }
            else
                Console.WriteLine("Booking not made in Live!!!");
            return BookingID;
        }

        public List<string> GetBookingID()
        {
            return BookingID;
        }

        public string GetHotelName()
        {
            return ProductSection[0].FindElement(BookedItemName[0]).Text;
        }

        public int GetHotelStarRating()
        {
            return ProductSection[0].FindElements(byStarRating).Count;
        }

        public string GetHotelItinerary()
        {
            return ProductSection[0].FindElement(Itinerary).Text;
        }

        public string GetRoomType(int roomNo = 1)
        {
            return RoomType[roomNo - 1].Text;
        }

        public string GetBoardType(int roomNo = 1)
        {
            return BoardType[roomNo - 1].Text;
        }

        public string GetRoomOccupants(int roomNo = 1)
        {
            return RoomOccupants[roomNo - 1].Text;
        }

        public decimal GetHotelPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(ProductPrice("Hotel").Text));
        }

        public string GetOutboundDepartureAirport()
        {
            return FlightLegs[0].Text.Split(" -")[0];
        }

        public string GetOutboundArrivalAirport()
        {
            return FlightLegs[0].Text.Split("- ")[1].Split("\r")[0];
        }

        public string GetOutboundDepartureDate()
        {
            return FlightTimings[0].Text.Split(" (")[0];
        }

        public string GetOutboundDepartureTime()
        {
            return FlightTimings[0].Text.Split(" (")[1].Split(" -")[0];
        }

        public string GetOutboundArrivalTime()
        {
            return FlightTimings[0].Text.Split("- ")[1].Split(")")[0];
        }

        public string GetInboundDepartureAirport()
        {
            return FlightLegs[1].Text.Split(" -")[0];
        }

        public string GetInboundArrivalAirport()
        {
            return FlightLegs[1].Text.Split("- ")[1].Split("\r")[0];
        }

        public string GetInboundDepartureDate()
        {
            return FlightTimings[1].Text.Split(" (")[0];
        }

        public string GetInboundDepartureTime()
        {
            return FlightTimings[1].Text.Split(" (")[1].Split(" -")[0];
        }

        public string GetInboundArrivalTime()
        {
            return FlightTimings[1].Text.Split("- ")[1].Split(")")[0];
        }

        public decimal GetFlightsPrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(ProductPrice("Flights").Text));
        }

        public decimal GetBaggagePrice()
        {
            _webDriver.ScrollElementToCenter(ProductDescription("Bags"));
            return ProductPrice("Bags").Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(ProductPrice("Bags").Text)) : 0;
        }

        public string GetTransferName()
        {
            return TransferName.Text;
        }

        public decimal GetTransferPrice()
        {
            _webDriver.ScrollElementToCenter(ProductPrice("Transfers"));
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(ProductPrice("Transfers").Text));
        }

        public decimal GetTotalPrice()
        {
            _webDriver.WaitForElementVisible(byBookingReference, 100, "Booking Reference is not visible");
            _webDriver.ScrollToElement(TotalPrice);
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(TotalPrice.Text));
        }

        public decimal GetDepositPaid()
        {
            return DepositPaid.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(DepositPaid.Text)) : 0;
            //return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(DepositPaid.Text));
        }

        public decimal GetBalanceAmount()
        {
            return BalanceAmount.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BalanceAmount.Text)) : 0;
            //return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(BalanceAmount.Text));
        }

        public string GetInsuranceName()
        {
            return InsuranceName.Text;
        }

        public decimal GetInsurancePrice()
        {
            return Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(ProductPrice("Insurance").Text));
        }

        public int GetBalanceAmountInstallments()
        {
            return BalancePayInstallments.Count;
        }

        public string GetProductSpecificBookingId(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Hotels: return BookingID.Where(id => id.StartsWith("ACM")).FirstOrDefault().Remove(0, 4);
                case ProductType.Flights: return BookingID.Where(id => id.StartsWith("EUF") || id.StartsWith("SCH")).FirstOrDefault().Remove(0, 4);
                case ProductType.Transfers: return BookingID.Where(id => id.StartsWith("TRN")).FirstOrDefault().Remove(0, 4);
                case ProductType.Insurance: return BookingID.Where(id => id.StartsWith("INS")).FirstOrDefault().Remove(0, 4);
                case ProductType.Attractions: return BookingID.Where(id => id.StartsWith("OLF")).FirstOrDefault().Remove(0, 4);
                case ProductType.Parking: return BookingID.Where(id => id.StartsWith("PRK")).FirstOrDefault().Remove(0, 4);
                case ProductType.CarHire: return BookingID.Where(id => id.StartsWith("AUT")).FirstOrDefault().Remove(0, 4);
                default: return "";
            }
        }

        public decimal GetLocalCharges()
        {
            return LocalCharges.Visible ? Convert.ToDecimal(CommonFunctions.RemoveCurrencyInfo(LocalCharges.Text)) : 0;
        }

        public bool IsHolidayBooking()
        {
            bool IsHoliday = false;
            foreach (AtWebElement bookingRef in BookingReference)
            {
                if (bookingRef.Text.Contains("EUF") || bookingRef.Text.Contains("SCH"))
                    IsHoliday = true;
            }
            return IsHoliday;
        }
    }
}
