using BoDi;
using Dnata.Automation.BDDFramework.Helpers.Screenshot;
using Dnata.Automation.BDDFramework.Reporting.TestRail;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Tests.Hooks
{
    [Binding]
    public sealed class RegisterHook
    {
        [BeforeScenario(Order = 1)]
        public void RegisterPages(IObjectContainer _objectContainer)
        {
            //_objectContainer.RegisterTypeAs<TestRailManager, ITestRailManager>();
            _objectContainer.RegisterTypeAs<ScreenShotHelper, IScreenShotHelper>();
            _objectContainer.RegisterTypeAs<HomePage, IHomePage>();
            _objectContainer.RegisterTypeAs<SearchSummaryComponent, ISearchSummaryComponent>();
            _objectContainer.RegisterTypeAs<HotelSearchResults, IHotelSearchResults>();
            _objectContainer.RegisterTypeAs<HotelEstabPage, IHotelEstabPage>();
            _objectContainer.RegisterTypeAs<BookingSummary, IBookingSummary>();
            _objectContainer.RegisterTypeAs<GuestInformation, IGuestInformation>();
            _objectContainer.RegisterTypeAs<ChoosePayment, IChoosePayment>();
            _objectContainer.RegisterTypeAs<PaymentPage, IPaymentPage>();
            _objectContainer.RegisterTypeAs<PaypalPage, IPaypalPage>();
            _objectContainer.RegisterTypeAs<SearchComponent, ISearchComponent>();
            _objectContainer.RegisterTypeAs<LandingPageSearchComponent, ILandingPageSearchComponent>();
            _objectContainer.RegisterTypeAs<GuestComponent, IGuestComponent>();
            _objectContainer.RegisterTypeAs<LandingPageGuestComponent, ILandingPageGuestComponent>();
            _objectContainer.RegisterTypeAs<CalendarComponent, ICalendarComponent>();
            _objectContainer.RegisterTypeAs<LandingPageCalendarComponent, ILandingPageCalendarComponent>();
            _objectContainer.RegisterTypeAs<SortComponent, ISortComponent>();
            _objectContainer.RegisterTypeAs<MapComponent, IMapComponent>();
            _objectContainer.RegisterTypeAs<BookingConfirmationPage, IBookingConfirmationPage>();
            _objectContainer.RegisterTypeAs<FlightSearchResults, IFlightSearchResults>();
            _objectContainer.RegisterTypeAs<Baggage, IBaggage>();
            _objectContainer.RegisterTypeAs<Transfers, ITransfers>();
            _objectContainer.RegisterTypeAs<TravelInsurance, ITravelInsurance>();
            _objectContainer.RegisterTypeAs<HolidayExtrasPage, IHolidayExtrasPage>();
            _objectContainer.RegisterTypeAs<FlightDetailsPage, IFlightDetailsPage>();
            _objectContainer.RegisterTypeAs<FiltersModal, IFiltersModal>();
            _objectContainer.RegisterTypeAs<ThreeDSPage, IThreeDSPage>();
            _objectContainer.RegisterTypeAs<FlightFilters, IFlightFilters>();
            _objectContainer.RegisterTypeAs<BreadCrumb, IBreadCrumb>();
            _objectContainer.RegisterTypeAs<ModalPopup, IModalPopup>();
            _objectContainer.RegisterTypeAs<USP, IUSP>();
            _objectContainer.RegisterTypeAs<PriceFilter, IPriceFilter>();
            _objectContainer.RegisterTypeAs<FilterSlider, IFilterSlider>();
            _objectContainer.RegisterTypeAs<HeaderComponent, IHeaderComponent>();
            _objectContainer.RegisterTypeAs<FooterComponent, IFooterComponent>();
            _objectContainer.RegisterTypeAs<RoomSelectionSummary, IRoomSelectionSummary > ();
            _objectContainer.RegisterTypeAs<LandingPage, ILandingPage>();
            _objectContainer.RegisterTypeAs<MetaPage, IMetaPage>();
            _objectContainer.RegisterTypeAs<AirportComponent, IAirportComponent>();
        }
    }
}
