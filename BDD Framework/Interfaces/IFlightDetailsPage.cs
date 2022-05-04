using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IFlightDetailsPage
    {
         FlightInboundOutboudDetailsModel GetFlightInboundOutboudDetailsModel(FlightType flightType);
         List<FlightLegModel> GetFlightLegDetails(FlightType flightType);
         void CloseModal();
         void CompareFlightLeg(List<FlightLegModel> FlightLegsInSrp, List<FlightLegModel> FlightLegsInEstab);
         void CaptureFlightDetails();
         FlightInformation GetFlightDetails();
         FlightInformation CaptureAndReturnFlightDetails();
         bool IsFlightDetailsModalDisplayed();
         string GetFligthInclusionInfo(FlightType flightWay);
         bool IsFlightInclusionSectionIsVisible();

    }
}
