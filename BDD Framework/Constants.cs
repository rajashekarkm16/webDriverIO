using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI
{
    public class Constants
    {
        public const int ShortWait = 10;
        public const int DefaultWait = 30;
        public const int MediumWait = 60;
        public const int LongWait = 120;
        public const int AdultAge = 25;
        public const int ChildAge = 8;
        public const int InfantAge = 0;
        public const string CalendarMonthYearLabel = "MMMM yyyy";
        public const string SelectedDateFormat = "MMM dd, yyyy";
        public const string MenuHotels = "Hotels";
        public const string MenuHotelPlusFlight = "Flight + Hotel";
        public const string Blue = "rgba(0, 157, 217, 1)";
        public const string Grey = "rgba(150, 150, 150, 1)";
        public const string Green = "rgba(51, 153, 51, 1)";
        public const decimal MaximumPriceDifference = 3;
        public const string PolicyDocumentationPdf = "https://www.travelrepublic.co.uk/build/docs/BookingConditions/PW";
        public const string InsuranceProductInformationDocumentPdf = "https://www.travelrepublic.co.uk/build/docs/BookingConditions/TravelRepublicB2BIPID";
        public const string HelpCentreLinkUKURL = "https://support.travelrepublic.co.uk";
        public const string HelpCentreLinkIEURL = "https://support.travelrepublic.ie";
        public const string StandardCancellationTermsUK = "https://support.travelrepublic.co.uk/Before-I-Travel/BIT-Flights/1299322202/I-want-to-cancel-my-booking.htm";
        public const string StandardCancellationTermsIE = "https://support.travelrepublic.ie/Before-I-Travel/BIT-Flights/1299322202/I-want-to-cancel-my-booking.htm";
        public const string CallToBookUK = "0208 974 7200";
        public const string CallToBookIE = "01 536 0820";
        public const string DefaultHotelDestination = "London";
        public const string DefaultHolidayDestination = "Dubai";
        public const string DefaultDepartureAirport = "London";
        public const int DefaultDepartureDays = 90;
        public const int DefaultDuration = 7;
        public const string DefaultGuests = "2,0,0";
        public const string DefaultCardType = "VisaCredit";
        public const int OutBoundTimeThreshold = 4;
        public const int InBoundTimeThreshold = 3;
        public const string OutBoundArrivalCutOffTime = "04:00";
        public const string InBoundDepartureCutOffTime = "03:00";

        public const string TotalFinancialProtetionUKUrl = "www.travelrepublic.co.uk/total-financial-protection";
        public const string TotalFinancialProtetionIEUrl = "www.travelrepublic.ie/total-financial-protection";
        public const string PeaceOfMindURl = "www.packpeaceofmind.co.uk";
        public const string PriceMatchEmail = "pricematch@travelrepublic.co.uk";

        public const string ATOLUSPModalTextUK = "Travel Republic hold a full ATOL license (ATOL 10581) and have a Scheduled Airline Failure Protection Scheme.\r\nBook your holiday safe in the knowledge that you are financially protected.\r\nFor more information on your financial protection, you can read more here: www.travelrepublic.co.uk/total-financial-protection\r\nFor more information on the government’s ATOL protection scheme please visit: www.packpeaceofmind.co.uk";
        public const string LandingPageATOLUSPModalTextUK = "Travel Republic hold a full ATOL license (ATOL 10581) and have a Scheduled Airline Failure Protection Scheme.\r\nBook your holiday safe in the knowledge that you are financially protected.\r\nFor more information on your financial protection you can read more here: www.travelrepublic.co.uk/total-financial-protection\r\nFor more information on the government’s ATOL protection scheme please visit: www.packpeaceofmind.co.uk";
        public const string ATOLUSPModalTextIE = "Travel Republic hold a full ATOL license (ATOL 10581) and have a Scheduled Airline Failure Protection Scheme.\r\nBook your holiday safe in the knowledge that you are financially protected.\r\nFor more information on your financial protection you can read more here: www.travelrepublic.ie/total-financial-protection\r\nFor more information on the government’s ATOL protection scheme please visit: www.packpeaceofmind.co.uk";
        public const string LandingPageATOLUSPModalTextIE = "Travel Republic hold a full ATOL license (ATOL 10581) and have a Scheduled Airline Failure Protection Scheme.\r\nBook your holiday safe in the knowledge that you are financially protected.\r\nFor more information on your financial protection, you can read more here: www.travelrepublic.ie/total-financial-protection\r\nFor more information on the government’s ATOL protection scheme please visit: www.packpeaceofmind.co.uk";
        public const string  ABTAUSPModalText = "Travel Republic are full members of ABTA so you can book and travel with confidence.\r\nAll ABTA members are bound by a code of conduct, ensuring high standards when you book your holiday.\r\nIf you don’t feel like these have been met, ABTA offers support and advice.";
        public const string LandingPageABTAUSPModalText = "Travel Republic is a full member of ABTA so you can book and travel with confidence.\r\nAll ABTA members are bound by a code of conduct, ensuring high standards when you book your holiday.\r\nIf you don’t feel like these have been met, ABTA offers support and advice.";
        public const string TrustpilotModalText = "Take a look at the genuine reviews from our customers at https://uk.trustpilot.com/review/www.travelrepublic.ie";
        public const string ResortSupportText = "When you make a booking, you will receive confirmation emails and all your documentation. On this email look out for our dedicated In-Resort phone line. Keep this safe so that when you're away and have any trouble with your hotel or flights, you can call us for support.";
        public const string FlightAllocationBaggageText = "Includes 23kg hold luggage";
        public const decimal FlightAllocationBaggagePrice = 30;
        public const decimal DefaultHotelDepositPrice = 15;
        public const string FreeCancellationMessage = "You can cancel this hotel";
        public const int DefaultPlatform195AdsCount = 2;
        public const int DefaultSponserdHotelsCount = 2;
        public const string HandLuggageMessage = "Includes 2 hold luggage";
        public const string AddBagSectionText = "Add a Bag\r\nAre you sure everything is going to fit?\r\nPrices shown are for return flights";

        public const string HotelLandingPageUKURL = "https://pp2.travelrepublic.co.uk/1-246-1-0/hotels-in-majorca";
        public const string HolidaysLandingPageUKURL = "https://pp2.travelrepublic.co.uk/1-262-2-0/holidays-in-tenerife";
        public const string HotelLandingPageIEURL = "https://pp2.travelrepublic.ie/1-246-1-0/hotels-in-majorca";
        public const string HolidaysLandingPageIEURL = "https://pp2.travelrepublic.ie/1-262-2-0/holidays-in-tenerife";
        public const int FlyingRoutesPerPage = 2;
        public const string EstabPageTitle = "Cheap hotels, flights and holidays from Travel Republic";
        public const  int DynamicDefaultCancellationDays= 5;
        public const int DefaultCancellationDays = 7;
        public const int BufferDate = 4;
        public const string NonRefundableMessage = "A non-refundable rate is usually the hotel’s/provider’s best rate. Full payment is needed at the time of booking and if you decide to cancel, it will be a 100% loss, so no refund would be due. As this is a promotional rate, no changes at all can be made to your booking once it is confirmed.";
        public const string NonRefundableHeader = "Non-refundable";
        public const string VoucherModalHeader = "Do you have a discount code or voucher?";
        public const string VoucherCodeHeader = "Enter code here:";
        public const string VoucherCodeAppliedSuccessMessage = "Your code has been applied!";
        public const string VocherCodeRemoveSuccessMessage = "The code has been removed and your booking total updated";        
    }

    public class InsuranceErrorMessages
    {
        public const string GenericDOBError = "Please enter a valid date of birth";
        public const string AdultEMForUnderAge = "Adults must be at least 18 years-old at the time of purchase";
        public const string AdultEMForOverAge = "Unfortunately we cannot insure travellers over 84 years old.";
        public const string ChildEMForIncorrectAge = "This passenger must be age * on the return date";
    }

    public class ErrorMessagesOnGuestInfo
    {
        public const string AdultErroMessageOnGuestInfo = "Adults must be 18 or over on the departure date";
        public const string ChildErrorMessageOnGuestInfo = "This passenger must be age * on the return date";
        public const string InfantErrorMessageOnGuestInfo = "This passenger must be age 0 on the return date";
    }

    public class Cards
    {
        public const string VisaCredit = "4444333322221111";
        public const string MasterCardCredit = "5454609899031510";
        public const string Amex = "340000000000009";
        public const string Connect = "4539792000000004";
        public const string MasterCardDebit = "1003130000000000";
        public const string SwitchMaestro = "1000060000000002";
        public const string VisaDebitDelta = "4462030000000000";
        public const string VisaElectron = "4917300800000000";
        public const string OtherCvv = "111";
        public const string AmexCvv = "1234";
    }

    public class PromoDetails
    {
        public const string PromoMessage = "Promo set up for AutoTest";
        public const string TermsAndCondition = "AutoTest Terms and conditions for Promo";
        public const string UrlPromoMessage = "URLPromo set up for AutoTest";
        public const string PromoSashText = "PROMOTIONAUTOTESTDONOTEDIT";
        public const string URLPromoSashText = "URLPROMOAUTOTESTDONOTEDIT";
        public const string DiscountCode = "AutoTestURLPromo";
        public const string CustomerFavoritePillText = "Customer Favourite";
        public const decimal PriceAdjustmentValue = 1.20M;
        public const decimal ExchangeRate = 1.184273M;
        public const string NoavailablilityMessage = "We couldn't find any rooms available for your chosen dates";
        public const string StopSellMessage = "Stop Sell Header\r\nStop sell Description";
    }

    public class MetaReferences
    {
        public const string KayakHoliday = "kayakclickid";
        public const string KayakHotel= "KYH";
        public const string Trivago= "TV";
        public const string TripAdvisorDesktop= "TVMETA";
        public const string TripAdvisorMobile = "TVMOTA";        
        public const string TravelSuperMarket = "TSH";
        public const string IceLolllyUK = "ICLH";
        
    }

    public class MetaCookies
    {
        public const int KayakUK = 125;
        public const int KayakIE = 128;
        public const int TrivagoUK = 5;
        public const int TrivagoIE = 26;
        public const int TripAdvisorUK = 40;
        public const int TripAdvisorUKMobile = 35;
        public const int TripAdvisorIE = 41;
        public const int TripAdvisorIEMobile = 36;
        public const int KayakUKHoliday = 22;
        public const int KayakIEHoliday = 131;
        public const int IceLollyUK = 12682;
        public const int TravelSuperMarketUK =8;
        public const int DealCharacterUK =23;
    }

    public class MetaData
    {
        public const string MetaUrl = "https://franklin.uk.derwent.travelrepublic.com/Meta/tools/hotel-deep-link.htm";
        public const string MetaPageTitle = "Hotels Deeplink Test Harness";
        public const string EstabIds = "117436,231568,117305";
        public const string Cabin = "1";
        public const string FlightPriceAlertmessage = "Total flight price may be inaccurate whenever there are any children in the package.";
    }

    public class CovidCover
    {
        public const decimal CovidCoverPrice = 0;
        public const string Promotionboxtext = "COVIDCOVERPLUSINCLUDEDWITHEVERYHOLIDAY";
        public const string CovidCoverdailogContent ="What are you covered for? (1) Cancellation should you, a close relative, your travel companion or anyone you have arranged to stay with fall sick, suffer an injury or pass away before you go away. (2) Cancellation should you, your travel companion or any person you have arranged to stay with during your holiday test positive for coronavirus within 14 days of your departure date. (3) Cancellation should you, your travel companion or any person you have arranged to stay with during your holiday be admitted to hospital due to coronavirus within 28 days of the start of the trip. (4) Cancellation should you or your travel companion be denied boarding following a positive coronavirus test, receiving a temperature test or other medical test reading. (5) Cancellation should you be made redundant from a workplace provided that you were working at your current place of employment for a minimum of 2 years and that you were not aware of any impending redundancy at the time this policy was issued or the holiday was booked. (6) Cancellation should you, or any person you intended to travel with, who is a member of the Armed Forces, emergency services, the nursing profession or a government employee being ordered to return to duty. (7) Cancellation should you be called for jury service or as a witness in a Court of Law (but not as an expert witness or where your employment would normally require you to attend court). (8) Cancellation should your home being made uninhabitable due to accidental damage, burglary, flooding or fire. (9) Cancellation should the police request your presence following burglary or attempted burglary at your home. For full terms, exclusions, definitions and how to claim visit [www.travelrepublic.co.uk/covid-cover-plus]";            
    }

    public class AdditionalPackageInstructions
    {
        public const string Disney7DayTicket = "7 - Day Disney’s Ultimate Ticket with Memory Maker";
        public const string Disney7DayTickePopupMessage = "7 - Day Disney’s Ultimate Ticket with Memory Maker includes the following: • Seven days of admission to the Magic Kingdom Park, Epcot, Disney’s Hollywood Studios, Disney’s Animal Kingdom Theme Park, Disney’s Blizzard Beach Water Park, Disney’s Typhoon Lagoon Water Park, Disney’s Oak Trail Golf Course – greens fee only (neither Water Park will open until March 7, 2021; tee time reservations are required and subject to availability), and ESPN Wide World of Sports Complex (valid only on event days; some events require an additional admission charge). • One (1) visit to the following on each day of the Ticket: Disney’s Fantasia Gardens Miniature Golf Course or Disney’s Winter Summerland Miniature Golf Course (If Disney’s Fantasia Gardens Miniature Golf Course is visited, Disney’s Winter Summerland Miniature Golf Course may not be visited on the same day. If Disney’s Winter Summerland Miniature Golf Course is visited, Disney’s Fantasia Gardens Miniature Golf Course may not be visited on the same day. Valid for one round per day. Round must start prior to 4:00 p.m.) • Memory Maker For information on the Memory Maker Entitlements, please review the “Memory Maker” section of the My Disney Experience Terms and Conditions at http://disneyworld.disney.go.com/media/park-experience-terms-and-conditions.html or successor site (the “Memory Maker Terms and Conditions”). The Memory Maker Terms and Conditions are subject to change without notice. Memory Maker is subject to the Memory Maker Terms and Conditions. However, notwithstanding anything to the contrary contained in the Memory Maker Terms and Conditions, the Memory Maker window for the Memory Maker component of these Tickets is as set forth below and not as stated in the Memory Maker Terms and Conditions: 1. The Memory Maker window shall begin upon the first use of the Ticket. 2. The Memory Maker window shall end on the date of expiration of the Ticket. Ticket (including Memory Maker component) expires on the earlier of (x) 14 days after the date of first use of the Ticket and (y) January 14, 2023. Parking is not included. Tickets will be issued by reception upon check-in at the hotel.";
        public const string Disney14DayTicket = "14 - Day Disney’s Ultimate Ticket with Memory Maker";
        public const string Disney14DayTickePopupMessage = "14 – Day Disney’s Ultimate Ticket with Memory Maker includes the following: • Fourteen days of admission to the Magic Kingdom Park, Epcot, Disney’s Hollywood Studios, Disney’s Animal Kingdom Theme Park, Disney’s Blizzard Beach Water Park, Disney’s Typhoon Lagoon Water Park, Disney’s Oak Trail Golf Course – greens fee only (neither Water Park will open until March 7, 2021; tee time reservations are required and subject to availability), and ESPN Wide World of Sports Complex (valid only on event days; some events require an additional admission charge). • One (1) visit to the following on each day of the Ticket: Disney’s Fantasia Gardens Miniature Golf Course or Disney’s Winter Summerland Miniature Golf Course (If Disney’s Fantasia Gardens Miniature Golf Course is visited, Disney’s Winter Summerland Miniature Golf Course may not be visited on the same day. If Disney’s Winter Summerland Miniature Golf Course is visited, Disney’s Fantasia Gardens Miniature Golf Course may not be visited on the same day. Valid for one round per day. Round must start prior to 4:00 p.m.) • Memory Maker For information on the Memory Maker Entitlements, please review the “Memory Maker” section of the My Disney Experience Terms and Conditions at http://disneyworld.disney.go.com/media/park-experience-terms-and-conditions.html or successor site (the “Memory Maker Terms and Conditions”). The Memory Maker Terms and Conditions are subject to change without notice. Memory Maker is subject to the Memory Maker Terms and Conditions. However, notwithstanding anything to the contrary contained in the Memory Maker Terms and Conditions, the Memory Maker window for the Memory Maker component of these Tickets is as set forth below and not as stated in the Memory Maker Terms and Conditions: 1. The Memory Maker window shall begin upon the first use of the Ticket. 2. The Memory Maker window shall end on the date of expiration of the Ticket. Ticket (including Memory Maker component) expires on the earlier of (x) 14 days after the date of first use of the Ticket and (y) January 14, 2023. Parking is not included. Tickets will be issued by reception upon check-in at the hotel.";
        public const string CharacterBreakfast = "Character Breakfast";
        public const string FrontRowSpaces = "Front row spaces for the summer parade";
        public const string DisneyParadise = "Disney Parade";
        public const string CharacterBreakfastPopupMessage = "Enjoy a breakfast with some of your favourite Disney characters. Tickets will be provided at hotel check in. Please specify preferred date for your breakfast at the time of booking to ensure there is availability on the day that works best for you.";
        public const string FrontRowSpacesPopupMessage = "Tickets will be provided at reception upon check in";
    }

       

}
