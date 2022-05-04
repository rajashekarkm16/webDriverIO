using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class HolidaySearchSteps
    {
        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly ISearchSummaryComponent _searchSummaryComponent;
        private readonly ISearchComponent _searchComponent;
        private readonly IGuestComponent _guestComponent;
        private readonly ICalendarComponent _calendarComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;

        public HolidaySearchSteps(IHomePage homePage, ISearchSummaryComponent searchSummaryComponent, ISearchComponent searchComponent, IGuestComponent guestComponent, ICalendarComponent calendarComponent, IHotelSearchResults hotelSearchResults, IHotelEstabPage hotelEstabPage, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _searchSummaryComponent = searchSummaryComponent;
            _searchComponent = searchComponent;
            _guestComponent = guestComponent;
            _calendarComponent = calendarComponent;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
        }

        [Given(@"I perform a holiday search to (.*) from (.*) for (.*) during (.*) and (.*) dates")]
        public void GivenIPerformAHolidaySearchToFromForDuringAndDates(string destination, string departureAirport, string guests, int departure, int duration)
        {
            _homePage.SearchHolidays(destination, departureAirport, departure, duration, guests);
             context.Add("HotelName", destination);
        }

        [Given(@"I perform a mobile holiday search for (.*) from (.*)")]
        [Given(@"I perform a holiday search for (.*) from (.*)")]
        public void GivenIPerformAMobileHolidaySearchFor(string destination, string departure_airport)
        {
            _homePage.SearchHolidays(destination, departure_airport);
        }

        [Given(@"I am on holiday hotel estab page")]
        public void GivenIAmOnHolidayHotelEstabPage()
        {
            _homePage.SearchHolidays();
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
        }

    }
}
