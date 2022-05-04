using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Models
{
    public enum FlightType
    {
        Outbound,
        Return
    }

    public class FlightInformation
    {
        public FlightInboundOutboudDetailsModel flightOutBoundDetails;
        public FlightInboundOutboudDetailsModel flightInBoundDetails;
    }


    public class FlightInboundOutboudDetailsModel
    {
        public string Date;
        public string Duration;
        public string NoOfStops;
        public List<FlightLegModel> flightLeg = new List<FlightLegModel>();
    }

    public class FlightLegModel
    {
        public string DepartureAirport;
        public string DepartureTime;
        public string ArrivalAirport;
        public string ArrivalTime;
        public string LayOverDuration;
        public string FlightClass;
        public string FlightNumber;
        public string FlightLogo;
        public string FlightLegDuration;
        public string AdditionalDays;
    }
}
