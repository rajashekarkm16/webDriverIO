using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using Dnata.Automation.BDDFramework.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dnata.TravelRepublic.MobileWeb.UI.Pages
{
    public class FlightDetailsPage : MobileBasePage, IFlightDetailsPage
    {

        FlightInformation flightDetailsModel ;

        #region[Constructor]
        private readonly IAtWebDriver _webDriver;

        public FlightDetailsPage(IAtWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }
        #endregion

        #region [WebElements]

        private ReadOnlyCollection<AtWebElement> DepartureTime(string flightWay) => _webDriver.FindElements(LocatorType.XPath, "//h3[text()='#']/parent::div/parent::div/parent::div//table//td/span", flightWay);
        private AtWebElement DepartureAirport(string flightWay, int flightLeg) => _webDriver.FindElement(LocatorType.XPath, "((//h3[text()='#']/parent::div/parent::div/parent::div//table)[#]//td/div)[1]", flightWay, flightLeg.ToString());
        private AtWebElement ArrivalTime(string flightWay, int flightLeg) => _webDriver.FindElement(LocatorType.XPath, "((//h3[text()='#']/parent::div/parent::div/parent::div//table)[#]//td/div)[2]", flightWay, flightLeg.ToString());        
        private AtWebElement ArrivalAirport(string flightWay, int flightLeg) => _webDriver.FindElement(LocatorType.XPath, "((//h3[text()='#']/parent::div/parent::div/parent::div//table)[#]//td/div)[3]", flightWay, flightLeg.ToString());
        private ReadOnlyCollection<AtWebElement> LayOverDuration(string flightWay) => _webDriver.FindElements(LocatorType.XPath, "//h3[text()='#']/parent::div/parent::div/parent::div//div[contains(@class,'dashed')]", flightWay);
        private AtWebElement Date(string flightWay) => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='#']/following-sibling::span", flightWay);
        private AtWebElement Stops(string flightWay) => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='#']/parent::div/following-sibling::div/span", flightWay);
        private AtWebElement Duration(string flightWay) => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='#']/parent::div/following-sibling::div", flightWay);
        private ReadOnlyCollection<AtWebElement> FlightClass(string flightWay) => _webDriver.FindElements(LocatorType.XPath, "//h3[text()='#']/parent::div/parent::div/parent::div//div[contains(@class,'start')]/div/span[contains(@class,'bold')]", flightWay);
        private ReadOnlyCollection<AtWebElement> FlightNumber(string flightWay) => _webDriver.FindElements(LocatorType.XPath, "//h3[text()='#']/parent::div/parent::div/parent::div//div[contains(@class,'start')]/div/span[contains(@class,'--s')]", flightWay);
        private ReadOnlyCollection<AtWebElement> FlightLogo(string flightWay) => _webDriver.FindElements(LocatorType.XPath, "//h3[text()='#']/parent::div/parent::div/following-sibling::div//img[contains(@class,'airline-logo')]", flightWay);
        private AtWebElement CloseButton => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='Flight Details']/ancestor::header//button[contains(@aria-label,'Close')]");
        private ReadOnlyCollection<AtWebElement> LegDuration(string flightWay) => _webDriver.FindElements(LocatorType.XPath, "//h3[text()='#']/parent::div/parent::div/parent::div//div[contains(@class,'start')]/div[1]", flightWay);
        private AtBy byFlightDetailsHeader => GetBy(LocatorType.XPath, "//h3[text()='Flight Details']");
        private AtWebElement FlightDetailsHeader => _webDriver.FindElement(byFlightDetailsHeader);
        private AtBy byFlightInclusionHeader => GetBy(LocatorType.XPath, "//span[text()='Flight inclusions']");
        private AtWebElement FlightinclusionHeader => _webDriver.FindElement(byFlightInclusionHeader);
        private AtWebElement FlightInclusionInfo(FlightType flightWay) => _webDriver.FindElement(LocatorType.XPath, "//h3[text()='#']/ancestor::div[3]//span[contains(text(),'luggage')]", flightWay.ToString());
        #endregion

        #region [Methods]

        public FlightInboundOutboudDetailsModel GetFlightInboundOutboudDetailsModel(FlightType flightType)
        {
            FlightInboundOutboudDetailsModel flightInboundOutboudDetailsModel = new FlightInboundOutboudDetailsModel();
            List<FlightLegModel> flightLeg = new List<FlightLegModel>();
            _webDriver.WaitForTextPresent(Duration(flightType.ToString()), ",", TimeSpan.FromSeconds(2), 5);
            flightInboundOutboudDetailsModel.Date = Date(flightType.ToString()).Text;
            flightInboundOutboudDetailsModel.NoOfStops = Stops(flightType.ToString()).Text;
            flightInboundOutboudDetailsModel.Duration = Duration(flightType.ToString()).Text.Split(",")[1];
            flightInboundOutboudDetailsModel.flightLeg = GetFlightLegDetails(flightType);
            return flightInboundOutboudDetailsModel;            
        }

        public List<FlightLegModel> GetFlightLegDetails(FlightType flightType)
        {
            List<FlightLegModel> flightLeg = new List<FlightLegModel>();
            for (int i = 0; i < DepartureTime(flightType.ToString()).Count; i++)
            {
                FlightLegModel flightLegInfo = new FlightLegModel();
                _webDriver.ScrollElementToCenter(FlightClass(flightType.ToString())[i]);
                flightLegInfo.FlightClass = FlightClass(flightType.ToString())[i].Text;
                flightLegInfo.FlightNumber = FlightNumber(flightType.ToString())[i].Text;
                flightLegInfo.FlightLogo = FlightLogo(flightType.ToString())[i].GetAttribute("src").Split("Airline/")[1];
                flightLegInfo.DepartureAirport = DepartureAirport(flightType.ToString(), i + 1).Text;
                _webDriver.WaitForTextPresent(DepartureTime(flightType.ToString())[i], ":", TimeSpan.FromSeconds(1), 10);
                flightLegInfo.DepartureTime = DepartureTime(flightType.ToString())[i].Text.Substring(0, 5);
                flightLegInfo.ArrivalAirport = ArrivalAirport(flightType.ToString(), i + 1).Text;
                flightLegInfo.ArrivalTime = ArrivalTime(flightType.ToString(), i + 1).Text.Substring(0, 5);
                flightLegInfo.AdditionalDays = ArrivalTime(flightType.ToString(), i + 1).Text.Replace(flightLegInfo.ArrivalTime+"\r\n", "");
                //flightLegInfo.FlightLegDuration = LegDuration(flightType.ToString())[i].Text.Split(",")[1].Split("\r\n")[0];

                //if (LayOverDuration(flightType.ToString()).Count > i)
                //{
                //    flightLegInfo.LayOverDuration = LayOverDuration(flightType.ToString())[i].Visible ? LayOverDuration(flightType.ToString())[i].Text : "";
                //}
                flightLeg.Add(flightLegInfo);
            }

            return flightLeg;
        }

        public void CloseModal()
        {
            CloseButton.Click();
        }

        public void CompareFlightLeg(List<FlightLegModel> FlightLegsInSrp, List<FlightLegModel> FlightLegsInEstab)
        {
            int counter = 0;
            foreach (var flightLeg in FlightLegsInSrp)
            {
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(flightLeg.FlightClass, FlightLegsInEstab[counter].FlightClass, "Flight Class is not matching on Flight details");
                Assert.AreEqual(flightLeg.FlightNumber, FlightLegsInEstab[counter].FlightNumber, "Flight Number is not matching on Flight details");
                Assert.AreEqual(flightLeg.FlightLogo, FlightLegsInEstab[counter].FlightLogo, "Flight Logo is not matching on Flight details");
                Assert.AreEqual(flightLeg.FlightLegDuration, FlightLegsInEstab[counter].FlightLegDuration, "Flight leg duration is not matching on Flight details");
                Assert.AreEqual(flightLeg.DepartureAirport, FlightLegsInEstab[counter].DepartureAirport, "Departure airport is not matching on Flight details");
                Assert.AreEqual(flightLeg.DepartureTime, FlightLegsInEstab[counter].DepartureTime, "Departure time is not matching on Flight details");
                Assert.AreEqual(flightLeg.ArrivalAirport, FlightLegsInEstab[counter].ArrivalAirport, "Arrival airport is not matching on Flight details");
                Assert.AreEqual(flightLeg.ArrivalTime, FlightLegsInEstab[counter].ArrivalTime, "Arrival time is not matching on Flight details");                
                Assert.AreEqual(flightLeg.LayOverDuration, FlightLegsInEstab[counter].LayOverDuration, "Layover duration is not matching on Flight details");
                counter++;
                });
            }            
        }


        public void CaptureFlightDetails()
        {
            flightDetailsModel = new FlightInformation();
            flightDetailsModel.flightOutBoundDetails = GetFlightInboundOutboudDetailsModel(FlightType.Outbound);
            flightDetailsModel.flightInBoundDetails = GetFlightInboundOutboudDetailsModel(FlightType.Return);
        }

        public FlightInformation GetFlightDetails()
        {
            return flightDetailsModel;
        }

        public FlightInformation CaptureAndReturnFlightDetails()
        {
            CaptureFlightDetails();
            return GetFlightDetails();
        }

        public bool IsFlightDetailsModalDisplayed()
        {
            _webDriver.WaitForElementVisible(byFlightDetailsHeader, 10, "Flight Details Header is not visible");
            return FlightDetailsHeader.Visible;
        }

        public bool IsFlightInclusionSectionIsVisible()
        {
            return FlightinclusionHeader.Visible;

        }

        public string GetFligthInclusionInfo(FlightType flightWay)
        {
            _webDriver.WaitForElementVisible(byFlightInclusionHeader, Constants.DefaultWait, "FlightInclusionHeader is nor visibe");
            return FlightInclusionInfo(flightWay).Text;
        }

        #endregion
    }
}
