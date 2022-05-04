using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.Automation.BDDFramework.Configuration;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class MetaSteps
    {
        private readonly ScenarioContext context;
        private readonly IMetaPage _metaPage;
        private readonly IGuestComponent _guestComponent;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IBookingConfirmationPage _bookingConfirmationPage;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;


        public MetaSteps(ScenarioContext injectedContext, ILandingPageGuestComponent landingPageGuestComponent, IMetaPage metaPage, IGuestComponent guestComponent, ICalendarComponent calendarComponent, IBookingConfirmationPage bookingConfirmationPage)
        {           
             context = injectedContext;
            _metaPage = metaPage;
            _guestComponent = guestComponent;
            _calendarComponent = calendarComponent;
            _bookingConfirmationPage = bookingConfirmationPage;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        #region[steps]
        [Given(@"I am on Meta Franklin site")]
        public void GivenIAmOnMetaFranklinSite()
        {
            _metaPage.NavigateToMetaFranklinPage();
            Assert.AreEqual(MetaData.MetaPageTitle,_metaPage.GetFranklinPageTitle(),"MetaFranklin Page is not Loaded");
        }

        [Given(@"I am on (.*) tab on meta search page and choose environment")]
        public void GivenIAmOnHotelTabOnMetaSearchPage(string product)
        {
            Assert.IsTrue(_metaPage.SelectProductOnFranklinPage(product), "No Search is Selected");
            _metaPage.SelectMetaEnvironemt(HelperFunctions.IsLive());
        }


        [Given(@"I run hotel search for (.*) (.*) (.*) (.*) (.*) (.*)")]
        public void GivenIRunHotelSearchFor(string metaChannel, int noOfRooms, string occupancy, int startDaysFromCurrentDate, int duration, string estabIds)
        {
            _calendarComponent.SetDepartureDate(DateTime.Now.AddDays(startDaysFromCurrentDate));
            _calendarComponent.SetReturnDate(DateTime.Now.AddDays(startDaysFromCurrentDate + duration));
            _guestComponent.SetRoomsData(occupancy);
            _landingPageGuestComponent.SetRoomsData(occupancy);
            _metaPage.PerformMetaHotelSearch(metaChannel,noOfRooms,occupancy,startDaysFromCurrentDate,duration,estabIds);
            if (metaChannel.Contains("UK"))
                AtConfiguration.UpdateConfiguration("Domain", "UK");
            else if (metaChannel.Contains("IE"))
                AtConfiguration.UpdateConfiguration("Domain", "IE");
        }

        [Given(@"I run holiday search for (.*) (.*) (.*) (.*) (.*) (.*) (.*) (.*)")]
        public void GivenIRunHolidaySearchFor(string metaChannel, string departureAirport, string destinationAirport, int countryID, int provinceID, int startDaysFromCurrentDate, int duration,string occupancy)
        {
            _calendarComponent.SetDepartureDate(DateTime.Now.AddDays(startDaysFromCurrentDate));
            _calendarComponent.SetReturnDate(DateTime.Now.AddDays(startDaysFromCurrentDate + duration));
            _guestComponent.SetRoomsData(occupancy);
            _metaPage.PerformMetaHolidaySearch(metaChannel, departureAirport, destinationAirport, countryID, provinceID, startDaysFromCurrentDate, duration, occupancy);
            if (metaChannel.Contains("UK"))
                AtConfiguration.UpdateConfiguration("Domain", "UK");
            else if (metaChannel.Contains("IE"))
                AtConfiguration.UpdateConfiguration("Domain", "IE");
        }


        [Then(@"Validate Meta Reference in url and cookies (.*)")]
        public void ThenValidateMetaReferenceInUrlAndCookies(string channel)
        {
            Assert.IsTrue(_metaPage.ValidateMetaReferencesInUrl(channel), "MetaReference are not shown in URL");
            Assert.IsTrue(_metaPage.ValidateCookieReferences(channel), "MetaReferences are not added in Cookie");
        }

        [Then(@"Validate ImageReference added for (.*)")]
        public void ThenValidateImageReferenceAddedForMetaChannel(string channel)
        {
            if(!HelperFunctions.IsLive())
                Assert.IsTrue(_metaPage.ValidateMetaImageReference(channel), "Meta Image Reference is not added");
        }

        [Then(@"Validate totalprice is matching from metachannel")]
        public void ThenValidateTotalpriceIsMatchingFromMetachannel()
        {
            if (!HelperFunctions.IsLive())
            {
                Assert.AreEqual(_metaPage.GetMetaTotalPrice(), _bookingConfirmationPage.GetTotalPrice(), "Total Price is Not Matched from MetaChannel and BookingConfirmationPage");
                Console.WriteLine("Meta Hotel Price :" + _metaPage.GetMetaTotalPrice() + "  HotelPrice : " + _bookingConfirmationPage.GetTotalPrice());
            }
        }


        [Then(@"Validate Flight and Room price are matching from metachannel")]
        public void ThenValidateFlightAndRoomPriceAreMatchingFromMetachannel()
        {
            if (!HelperFunctions.IsLive() && !HelperFunctions.IsAllHolidayPackage())
            {
                bool IsPriceMatched = _metaPage.ValiidateFlightAndRoomPrice(_bookingConfirmationPage.GetTotalPrice(), _bookingConfirmationPage.GetFlightsPrice(), _bookingConfirmationPage.GetHotelPrice(), _guestComponent.GetRoomOccupantDetails());
                Assert.IsTrue(IsPriceMatched, "Flight and Total Price are not Matched from meta channel");
            }
        }
        #endregion
    }

}
