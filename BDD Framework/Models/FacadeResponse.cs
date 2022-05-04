using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Models
{

    public class FacadeResponse
    {
        public string alias { get; set; }
        public int destinationId { get; set; }
        public int tier { get; set; }
        public string metaTitle { get; set; }
        public string metaDescription { get; set; }
        public string headerH1 { get; set; }
        public string subHeader { get; set; }
        public string promoMessage { get; set; }
        public int contentId { get; set; }
        public bool noIndex { get; set; }
        public bool noFollow { get; set; }
        public Widget[] widgets { get; set; }
        public Destinationdata destinationData { get; set; }
        public Defaultsearchdestination defaultSearchDestination { get; set; }
        public string canonicalUrl { get; set; }
        public Hreflang[] hreflangs { get; set; }
    }

    public class Destinationdata
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int LegacyType { get; set; }
        public int LegacyId { get; set; }
        public Stats Stats { get; set; }
        public Timezone Timezone { get; set; }
    }

    public class Stats
    {
        public Cheapestroompricebystarrating[] CheapestRoomPriceByStarRating { get; set; }
        public Averageroomcostsbystarrating[] AverageRoomCostsByStarRating { get; set; }
        public int HotelCount { get; set; }
        public int ChildCount { get; set; }
        public int NumRoomsBooked { get; set; }
        public int NumNightsBooked { get; set; }
        public float CheapestRoomPrice { get; set; }
        public float AverageRoomPrice { get; set; }
        public int AverageDurationOfStay { get; set; }
        public Mostpopularchildren[] MostPopularChildren { get; set; }
        public Mostpopulargrandchildren[] MostPopularGrandchildren { get; set; }
        public Mostpopularhotel[] MostPopularHotels { get; set; }
        public int MostPopularStarRating { get; set; }
        public float AverageCostOfMostPopularHotelStar { get; set; }
        public int HighSeasonMonth { get; set; }
        public int LowSeasonMonth { get; set; }
        public Cheapestairport CheapestAirport { get; set; }
        public int DirectAirlineCount { get; set; }
        public Mostpopularairline[] MostPopularAirlines { get; set; }
        public Mostpopularairport[] MostPopularAirports { get; set; }
        public Cheapestairline CheapestAirline { get; set; }
    }

    public class Cheapestairport
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Cheapestairline
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Cheapestroompricebystarrating
    {
        public int Key { get; set; }
        public float Value { get; set; }
    }

    public class Averageroomcostsbystarrating
    {
        public int Key { get; set; }
        public float Value { get; set; }
    }

    public class Mostpopularchildren
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Tags { get; set; }
    }

    public class Mostpopulargrandchildren
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Tags { get; set; }
    }

    public class Mostpopularhotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Mostpopularairline
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Mostpopularairport
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Timezone
    {
        public float Start { get; set; }
        public object End { get; set; }
    }

    public class Defaultsearchdestination
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int LegacyId { get; set; }
        public int LegacyType { get; set; }
    }

    public class Widget
    {
        public string alias { get; set; }
        public int contentId { get; set; }
        public string heroImageUrl { get; set; }
        public Item[] items { get; set; }
        public string header { get; set; }
        public string details { get; set; }
        public string sectionHeader { get; set; }
        public string sectionSubHeader { get; set; }
        public string introText { get; set; }
        public Tab[] tabs { get; set; }
        public string showMoreText { get; set; }
        public string showLessText { get; set; }
        public string childLinkIntroText { get; set; }
        public string pricePrefixText { get; set; }
        public string priceSuffixText { get; set; }
        public string buttonText { get; set; }
        public string modalButtonText { get; set; }
        public string offersTabTitle { get; set; }
        public string popularTabTitle { get; set; }
        public Othertab[] otherTabs { get; set; }
        public int maxCards { get; set; }
        public bool showMoreLink { get; set; }
        public string moreLinkText { get; set; }
        public int zoomLevel { get; set; }
        public string destinationTabTitle { get; set; }
        public string holidayTypesTabTitle { get; set; }
        public object[] holidayTypes { get; set; }
        public Card1[] cards { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string url { get; set; }
        public string alias { get; set; }
        public string factType { get; set; }
        public string iconClass { get; set; }
        public string factData { get; set; }
        public string factTitle { get; set; }
        public string description1 { get; set; }
        public string description2 { get; set; }
        public int contentId { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string imageIcon { get; set; }
        public string description { get; set; }
        public string linkText { get; set; }
        public string linkUrl { get; set; }
    }

    public class Tab
    {
        public string alias { get; set; }
        public string tabName { get; set; }
        public string tabType { get; set; }
        public Card[] cards { get; set; }
    }

    public class Card
    {
        public Childlink[] childLinks { get; set; }
        public string airportName { get; set; }
        public string airportCode { get; set; }
        public Price price { get; set; }
        public string description { get; set; }
        public string buttonText { get; set; }
        public string modalButtonText { get; set; }
        public int destinationId { get; set; }
        public string linkUrl { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public int tier { get; set; }
    }

    public class Price
    {
        public string formatted { get; set; }
        public float value { get; set; }
        public string rounded { get; set; }
        public string currencySymbol { get; set; }
    }

    public class Childlink
    {
        public int destinationId { get; set; }
        public string linkUrl { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public int tier { get; set; }
    }

    public class Othertab
    {
        public string alias { get; set; }
        public string title { get; set; }
        public Theme theme { get; set; }
    }

    public class Theme
    {
        public string alias { get; set; }
        public int themeId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string urlLinkText { get; set; }
        public int contentId { get; set; }
        public string imageUrl { get; set; }
    }

    public class Card1
    {
        public string description { get; set; }
        public string buttonText { get; set; }
        public object modalButtonText { get; set; }
        public int destinationId { get; set; }
        public string linkUrl { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public int tier { get; set; }
    }

    public class Hreflang
    {
        public string Url { get; set; }
        public string Language { get; set; }
    }

}
