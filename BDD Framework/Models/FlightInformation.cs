using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Models
{
    public enum FlightLegType
    {
        Outbound = 0,
        Inbound = 1
    }
    
    public class FlightInboundOutboudInformationModel
    {
        public decimal Price;
        public string BaggageInformation = "";
        public List<FlightLegInformationModel> flightLeg = new List<FlightLegInformationModel>();
    }
    
    public class FlightLegInformationModel
    {
        public string DepartureTime;
        public string ArrivalTime;
        public string DepartureLocation;
        public string ArrivalLocation;
        public string Duration;        
        public int Stops;
        public int AdditionalDays = 0;
        public string FlightLogo;
        public string CarrierOperatedBy = "";
    }
}
