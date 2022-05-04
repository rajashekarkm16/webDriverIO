using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.Reporting.CustomReporter;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using NUnit.Framework;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class ExtrasPageSteps
    {
        private readonly ScenarioContext context;
        private readonly IHomePage _homePage;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IHotelEstabPage _hotelEstabPage;
        private readonly IBookingSummary _bookingSummary;
        private readonly IFlightSearchResults _flightSearchResults;
        private readonly IBaggage _baggage;
        private readonly ITransfers _transfers;
        private readonly ITravelInsurance _travelInsurance;
        private readonly IHolidayExtrasPage _holidayExtrasPage;
        private readonly IGuestComponent _guestComponent;
        private readonly ILandingPageGuestComponent _landingPageGuestComponent;
        private readonly IModalPopup _modalPopup;

        public ExtrasPageSteps(IHomePage homePage, IHotelSearchResults hotelSearchResults, ILandingPageGuestComponent landingPageGuestComponent, IHotelEstabPage hotelEstabPage, IBookingSummary bookingSummary, ISearchComponent searchComponent, IFlightSearchResults flightSearchResults, IBaggage baggage, ITransfers transfers, ITravelInsurance travelInsurance, IHolidayExtrasPage holidayExtrasPage, IGuestComponent guestComponent, IModalPopup modalPopup, ScenarioContext injectedContext)
        {
            context = injectedContext;
            _homePage = homePage;
            _hotelSearchResults = hotelSearchResults;
            _hotelEstabPage = hotelEstabPage;
            _bookingSummary = bookingSummary;
            _flightSearchResults = flightSearchResults;
            _baggage = baggage;
            _transfers = transfers;
            _travelInsurance = travelInsurance;
            _holidayExtrasPage = holidayExtrasPage;
            _guestComponent = guestComponent;
            _modalPopup = modalPopup;
            _landingPageGuestComponent = landingPageGuestComponent;
        }

        [When(@"I am on holiday extras page with preselected flight")]
        public void WhenIAmOnHolidayExtrasPageWithPreselectedFlight()
        {
            _hotelSearchResults.CaptureAndReturnHotelInformation(_hotelSearchResults.GetHotelToSelect());
            _hotelSearchResults.SelectHotel(_hotelSearchResults.GetHotelToSelect());
            if(HelperFunctions.IsV3HomepageEnabled())
                _hotelEstabPage.SelectRoomsAndBoardTypes(_landingPageGuestComponent.GetRoomOccupantDetails().Count);
            else
                _hotelEstabPage.SelectRoomsAndBoardTypes(_guestComponent.GetRoomOccupantDetails().Count);
            _flightSearchResults.KeepSelectedFlight();
        }

        #region[Baggage]

        [Then(@"Baggage included info should be displayed")]
        public void ThenBaggageIncludedInfoShouldBeDisplayed()
        {
            Assert.IsTrue(_baggage.VerifyBaggageIncludeMessage(), "Baggage IncludeMessage is not matched");
        }

        [Then(@"Add bag section should be dislayed")]
        public void ThenAddBagSectionShouldBeDislayed()
        {
            if (_baggage.IsAddBaggageOptionAvailable())
                Assert.IsTrue(_baggage.VerfiyAddBagSectionText(), "Add Bag Section not matched");
            else
                Assert.Inconclusive("Hold Baggage section is not displayed to Add Bags");
        }

        [When(@"Select random baggage")]
        public void WhenSelectRandomBag()
        {
            _baggage.SelectBaggage(_baggage.GetBaggageToSelect());
        }


        [When(@"Select Bags if available")]
        public void WhenSelectBagsIfAvailable()
        {
            if (_baggage.IsAddBaggageOptionAvailable() == true)
                _baggage.SelectBaggage(1);
        }

        [When(@"Select random bag if add baggage option is available")]
        public void WhenSelectRandomBagIfAddBaggageOptionIsAvailable()
        {
            if (_baggage.IsAddBaggageOptionAvailable())
            {
                _baggage.SelectBaggage(_baggage.GetBaggageToSelect());
            }
        }

        #endregion

        [When(@"Select random transfers")]
        public void WhenSelectRandomTransfers()
        {
            _transfers.SelectTransfer(_transfers.GetTransferToSelect());
        }

        [When(@"Select random transfers if transfer is available")]
        public void WhenSelectRandomTransfersIfTransferIsAvailable()
        {
            if(_transfers.IsTransferSectionVisible())
                _transfers.SelectTransfer(_transfers.GetTransferToSelect());
        }

        [When(@"Select option (.*) from available transfers")]
        public void WhenSelectOptionFromAvailableTransfers(int transferToSelect)
        {
            _transfers.SelectTransfer(transferToSelect);
        }

        [Then(@"Transfer option (.*) should be added")]
        public void ThenTransferOptionShouldBeAdded(int transferToSelect)
        {
            Assert.IsTrue(_transfers.IsTransferSelected(transferToSelect), "Expected transfer is not added");
        }

        [When(@"Remove the added transfer")]
        public void WhenRemoveTheAddedTransfer()
        {
            _transfers.RemoveSelectedTransfer(_transfers.GetTransferToSelect());
        }

        [Then(@"Transfer option (.*) should be removed")]
        public void ThenTransferOptionShouldBeRemoved(int transferToSelect)
        {
            Assert.IsFalse(_transfers.IsTransferSelected(transferToSelect), "Expected transfer should be removed");
        }


        [Then(@"Tansfer should be removed")]
        public void ThenTansferShouldBeRemoved()
        {
            Assert.IsFalse(_transfers.IsTransferAdded(), "Transfer is not removed");
        }

        [When(@"click on continue to book")]
        public void WhenClickOnContinueToBook()
        {
            _holidayExtrasPage.ContinueToBook();
        }

        [When(@"Select random Travel Insurance if Insurance is available")]
        public void WhenSelectRandomTravelInsuranceIfInsuranceIsAvailable()
        {
            if (HelperFunctions.IsV3HomepageEnabled())
            {
                if (_travelInsurance.IsTravelInsuranceAvailable())
                    _travelInsurance.AddTravelInsurance(_travelInsurance.GetInsuranceToSelect(), _landingPageGuestComponent.GetRoomOccupantDetails());
            }

            else
            {
                if (_travelInsurance.IsTravelInsuranceAvailable())
                    _travelInsurance.AddTravelInsurance(_travelInsurance.GetInsuranceToSelect(), _guestComponent.GetRoomOccupantDetails());
            }
            
        }

        [When(@"Add a Travel Insurance")]
        public void WhenSelectRandomTravelInsurance()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _travelInsurance.AddTravelInsurance(_travelInsurance.GetInsuranceToSelect(), _landingPageGuestComponent.GetRoomOccupantDetails());
            else
                _travelInsurance.AddTravelInsurance(_travelInsurance.GetInsuranceToSelect(), _guestComponent.GetRoomOccupantDetails());
        }

        [Then(@"I have my own travel insurance should be selected by default if insurance is available")]
        public void ThenIHaveMyOwnTravelInsuranceShouldBeSelectedByDefaultIfInsuranceIsAvailable()
        {
            if (_travelInsurance.IsTravelInsuranceAvailable())
                Assert.IsTrue(_travelInsurance.IsDefaultInsuranceSelected(), "Default insurance is not selected");
        }


        [Then(@"Mandatory transfers is available and selected by default")]
        public void ThenMandatoryTransfersIsAvailableAndSelectedByDefault()
        {
            _transfers.CaptureDefaultMandatoryTransferDetails();            
        }

        [Then(@"Cheapest transfer should be displayed first")]
        public void ThenCheapestTransferShouldBeDisplayedFirst()
        {
            int occupantCount;
            if (HelperFunctions.IsV3HomepageEnabled())
                occupantCount = _landingPageGuestComponent.GetRoomOccupantDetails().Count;
            else
                occupantCount = _guestComponent.GetRoomOccupantDetails().Count;
            for (int roomNo = 1; roomNo<= occupantCount; roomNo++)
            {
                List<TransferInformation> transferInformation = new List<TransferInformation>();
                transferInformation = _transfers.CaptureAllMandatoryTransferDetails(roomNo);
                for(int transfer = 0; transfer < transferInformation.Count - 1; transfer++)
                {
                    Assert.LessOrEqual(transferInformation[transfer].PerPersonPrice, transferInformation[transfer + 1].PerPersonPrice);
                }
            }
        }

        [Then(@"Requests user to enter flight details")]
        public void ThenRequestsUserToEnterFlightDetails()
        {
            _transfers.PopulateTransferFlightDetails();
        }

        [When(@"Enter invalid flight details")]
        public void WhenEnterInvalidFlightDetails()
        {
            _transfers.PopulateInvalidFlightDetails();
        }

        [Then(@"Flight validation Error messages should be displayed")]
        public void ThenFlightValidationErrorMessagesShouldBeDisplayed()
        {
            List<string> errorMessages = _transfers.GetFlightsValidationErrorMessages();
            Assert.AreEqual("Please enter a valid flight number", errorMessages[0]);
            Assert.AreEqual("Please enter a valid flight number", errorMessages[2]);
            Assert.AreEqual("Please specify an arrival time", errorMessages[1]);
            Assert.AreEqual("Please specify a departure time", errorMessages[3]);
        }

        [Then(@"User should not be able to continue with the booking")]
        public void ThenUserShouldNotBeAbleToContinueWithTheBooking()
        {
           Assert.IsFalse(_holidayExtrasPage.IsContinueToBookEnabled());
        }


        [Then(@"Compliemtary transfers details are displayed")]
        public void ThenCompliemtaryTransfersDetailsAreDisplayed()
        {
            Assert.IsTrue(_transfers.GetMandatoryTransferDetails()[0].PerPersonPrice.Equals(0));
            Assert.IsTrue(_transfers.IsComplimentaryTransferFlagDisplayed());
        }

        [Then(@"Mandatory transfers header text is displayed (.*) and (.*)")]
        public void ThenMandatoryRansfersHeaderTextIsDisplayed(string headerMessage, string headerText)
        {
            headerText = "Transfers are for all passengers, to and from your accommodation";
            headerMessage = "We have pre-selected the best option for you"; 
            string text = _transfers.ReturnHeaderText();

            Assert.IsTrue(headerMessage.Equals(text.Split("\r\n")[1]));
            Assert.IsTrue(headerText.Equals(text.Split("\r\n")[2]));          

        }

        [Then(@"Mandatory transfer important information section is displayed")]
        public void ThenMandatoryTransferImportantInformationSectionIsDisplayed()
        {
            Assert.IsTrue(_transfers.IsImportantInformationDisplayed());
        }



        [When(@"Choose random transfers available for each room")]
        public void WhenChooseRandomTransfersAvailableForEachRoom()
        {
            _transfers.CaptureAndSelectMandatoryTransferDetails();          
            
        }

        [When(@"Click on More insurance info link")]
        public void WhenClickOnMoreInsuranceInfoLink()
        {
            _travelInsurance.ClickMoreInsuranceInfoLink();
        }

        [Then(@"Insurance information modal should be displayed")]
        public void ThenInsuranceInformationModalShouldBeDisplayed()
        {
            Assert.IsTrue(_travelInsurance.IsInsuranceModalDisplayed(), "Insurance information modal is not displayed");
        }

        [Then(@"Insurance information modal should be closed")]
        public void ThenInsuranceInformationModalShouldBeClosed()
        {
            Assert.IsFalse(_travelInsurance.IsInsuranceModalDisplayed(), "Insurance information modal is not closed");
        }


        [When(@"I click on View Policy Documentation link")]
        public void WhenIClickOnViewPolicyDocumentationLink()
        {
            _travelInsurance.ClickViewPolicyDocumentationLink();
        }

        [When(@"I close the modal")]
        public void WhenICloseTheModal()
        {
            _modalPopup.ClosePopUp();  
        }

        [Then(@"Policy documentation pdf link should be displayed")]
        public void ThenPolicyDocumentationPdfLinkShouldBeDisplayed()
        {
            string pdfURL = _travelInsurance.GetPDFLinkUrl();
            Assert.IsTrue(pdfURL.Contains(Constants.PolicyDocumentationPdf), "PDF URL is incorrect");
            Assert.IsTrue(pdfURL.Contains(".pdf"), "URL doesnt look to be PDF");
        }

        [When(@"I click on View Insurance Product Information Document")]
        public void WhenIClickOnViewInsuranceProductInformationDocument()
        {
            _travelInsurance.ClickViewInsuranceProductInformationDocumentLink();
        }

        [Then(@"Insurance Product Information Document pdf link should be displayed")]
        public void ThenInsuranceProductInformationDocumentPdfLinkShouldBeDisplayed()
        {
            string pdfURL = _travelInsurance.GetPDFLinkUrl();
            Assert.IsTrue(pdfURL.Contains(Constants.InsuranceProductInformationDocumentPdf),"PDF URL is incorrect");
            Assert.IsTrue(pdfURL.Contains(".pdf"), "URL doesnt look to be PDF");
        }


        [When(@"Click on View policy information link")]
        public void WhenClickOnViewPolicyInformationLink()
        {
            _travelInsurance.ClickViewPolicyInformationLink();
        }

        [Then(@"Insurance details modal should be displayed")]
        public void ThenInsuranceDetailsModalShouldBeDisplayed()
        {
            Assert.IsTrue(_travelInsurance.IsInsuranceModalDisplayed());
        }

        [Then(@"Insurance details modal should be closed")]
        public void ThenInsuranceDetailsModalShouldBeClosed()
        {
            Assert.IsFalse(_travelInsurance.IsInsuranceModalDisplayed(), "Insurance details modal is not closed");
        }

        [When(@"Click on available insurance card")]
        public void WhenClickOnAvailableInsuarceCard()
        {
            _travelInsurance.ClickInsuranceCard(_travelInsurance.GetInsuranceToSelect());
        }

        [When(@"Click on option (.*) on the available insuarce card")]
        public void WhenClickOnOptionOnTheAvailableInsuarceCard(int insuranceToSelect)
        {
            _travelInsurance.ClickInsuranceCard(insuranceToSelect);
        }

        [Then(@"Add to basket button should be (.*)")]
        [Then(@"Update basket button should be (.*)")]
        public void ThenAddToBasketButtonShouldBe(string isEnabled)
        {           
            if((string.Equals(isEnabled, "enabled", StringComparison.OrdinalIgnoreCase)))
                Assert.IsTrue(_travelInsurance.IsAddInsuranceToBasketButtonEnabled(), "Add to basket button is not enabled after entering occupants DOB");
            else
                Assert.IsFalse(_travelInsurance.IsAddInsuranceToBasketButtonEnabled(), "Add to basket button is enabled before entering occupants DOB");
        }

        [When(@"I enter valid DOB for all occupants")]
        public void WhenIEnterValidDOBForAllOccupants()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _travelInsurance.PopulateDOBOnInsuranceCard(_landingPageGuestComponent.GetRoomOccupantDetails());
            else
                _travelInsurance.PopulateDOBOnInsuranceCard(_guestComponent.GetRoomOccupantDetails());
        }

        [When(@"I enter invalid DOB for all occupants")]
        public void WhenIEnterInvalidDOBForAllOccupants()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _travelInsurance.PopulateDOBOnInsuranceCard(_landingPageGuestComponent.GetRoomOccupantDetails(), -10, -8, -8);
            else
               _travelInsurance.PopulateDOBOnInsuranceCard(_guestComponent.GetRoomOccupantDetails(), -10,-8,-8);
        }

        [Then(@"From qualifier should be displayed along with individual occupants insurance price")]
        public void ThenQualifierShouldBeDisplayedAlongWithIndividualOccupantsInsurancePrice()
        {
            Assert.IsTrue(_travelInsurance.IsFromQualifierAbovePriceIsDisplayed(), "From qualifier is not displayed above occupant insurance price");
        }

        [Then(@"From qualifier should not be displayed along with individual occupants insurance price")]
        public void ThenFromQualifierShouldNotBeDisplayedAlongWithIndividualOccupantsInsurancePrice()
        {
            Assert.IsFalse(_travelInsurance.IsFromQualifierAbovePriceIsDisplayed(), "From qualifier is displayed above occupant insurance price");
        }

        [Then(@"Occupants DOB data should be retained on the new selection")]
        public void ThenOccupantsDOBDataShouldBeRetainedOnTheNewSelection()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _travelInsurance.ValidateRetainedDOBOnInsuranceCard(_landingPageGuestComponent.GetRoomOccupantDetails());
            else
                _travelInsurance.ValidateRetainedDOBOnInsuranceCard(_guestComponent.GetRoomOccupantDetails());
        }

        [When(@"Change adult DOB to other insurance band")]
        public void WhenChangeDOBForAllOccupants()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _travelInsurance.PopulateDOBOnInsuranceCard(_landingPageGuestComponent.GetRoomOccupantDetails(), 60, 8, 0);
            else
                _travelInsurance.PopulateDOBOnInsuranceCard(_guestComponent.GetRoomOccupantDetails(), 60, 8, 0);
            _travelInsurance.CaptureTravelInsuranceInformation(_travelInsurance.GetInsuranceToSelect());
        }

        [When(@"Click on update basket button")]
        public void WhenClickOnUpdateBasketButton()
        {
            _travelInsurance.ClickUpdateBasketBuuton();
        }

        [Then(@"""(.*)"" error message should be displayed on inusrance card")]
        public void ThenErrorMessageShouldBeDisplayedOnInusranceCard(string errorMessage)
        {
            List<string> adultErrorMessages = new List<string>();
            adultErrorMessages = _travelInsurance.GetAllAdultErrorMessages();
            foreach (var error in adultErrorMessages)
            {
                Assert.AreEqual(errorMessage, error.ToString());
            }
            List<string> childErrorMessages = new List<string>();
            childErrorMessages = _travelInsurance.GetAllChildErrorMessages();
            foreach (var error in childErrorMessages)
            {
                Assert.AreEqual(errorMessage, error.ToString());
            }
            int totalOccupants = 0;
            List<RoomOccupantDetails> lRoomOccupantDetails;
            if (HelperFunctions.IsV3HomepageEnabled())
                lRoomOccupantDetails = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                lRoomOccupantDetails = _guestComponent.GetRoomOccupantDetails();
            for (int i = 0; i < lRoomOccupantDetails.Count; i++)
            {
                totalOccupants += lRoomOccupantDetails[i].NoOfAdults + lRoomOccupantDetails[i].NoOfChildren + lRoomOccupantDetails[i].NoOfInfants;
            }
            Assert.AreEqual(adultErrorMessages.Count + childErrorMessages.Count, totalOccupants);
        }

        [Then(@"Appropriate error message is displayed for child age different from searched age")]
        public void ThenAppropriateErrorMessageIsDisplayedForChildAgeDifferentFromSearchedAge()
        {
            List<RoomOccupantDetails> lRoomOccupantDetails;
            if (HelperFunctions.IsV3HomepageEnabled())
                lRoomOccupantDetails = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                lRoomOccupantDetails = _guestComponent.GetRoomOccupantDetails();
            _travelInsurance.PopulateDOBOnInsuranceCard(lRoomOccupantDetails, 25, 10, 0);            
            List<string> errorMessages = new List<string>();
            errorMessages = _travelInsurance.GetAllChildErrorMessages();
            foreach (var error in errorMessages)
            {
                Assert.AreEqual(InsuranceErrorMessages.ChildEMForIncorrectAge.Replace("*", Constants.ChildAge.ToString()), error.ToString());
            }
            int totalChildren = 0;
            for (int i = 0; i < lRoomOccupantDetails.Count; i++)
            {
                totalChildren += lRoomOccupantDetails[i].NoOfChildren;
            }
            Assert.AreEqual(totalChildren, errorMessages.Count, "error message count does not match with number of occupants");
        }

        [Then(@"Appropriate error message is displayed for infant age different from searched age")]
        public void ThenAppropriateErrorMessageIsDisplayedForInfantAgeDifferentFromSearchedAge()
        {
            List<RoomOccupantDetails> lRoomOccupantDetails;
            if (HelperFunctions.IsV3HomepageEnabled())
                lRoomOccupantDetails = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                lRoomOccupantDetails = _guestComponent.GetRoomOccupantDetails();
            _travelInsurance.PopulateDOBOnInsuranceCard(lRoomOccupantDetails, 25,8,18);
            List<string> errorMessages = new List<string>();
            errorMessages = _travelInsurance.GetAllChildErrorMessages();
            foreach (var error in errorMessages)
            {
                Assert.AreEqual(InsuranceErrorMessages.ChildEMForIncorrectAge.Replace("*", Constants.InfantAge.ToString()), error.ToString());
            }
            int totalInfant = 0;
            for (int i = 0; i < lRoomOccupantDetails.Count; i++)
            {
                totalInfant += lRoomOccupantDetails[i].NoOfInfants;
            }
            Assert.AreEqual(errorMessages.Count, totalInfant, "error message count does not match with number of occupants");
        }


        [Then(@"Appropriate error message is displayed for adult age more than (.*)")]
        public void ThenAppropriateErrorMessageIsDisplayedForAdultAgeMoreThan(int adultAgeLimit)
        {
            List<RoomOccupantDetails> lRoomOccupantDetails;
            if (HelperFunctions.IsV3HomepageEnabled())
                lRoomOccupantDetails = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                lRoomOccupantDetails = _guestComponent.GetRoomOccupantDetails();
            _travelInsurance.PopulateDOBOnInsuranceCard(lRoomOccupantDetails, adultAgeLimit + 2);
            List<string> errorMessages = new List<string>();
            errorMessages = _travelInsurance.GetAllAdultErrorMessages();
            foreach (var error in errorMessages)
            {
                Assert.AreEqual(InsuranceErrorMessages.AdultEMForOverAge, error.ToString());
            }

            int totalAdults = 0;
            for (int i = 0; i < lRoomOccupantDetails.Count; i++)
            {
                totalAdults += lRoomOccupantDetails[i].NoOfAdults;
            }
            Assert.AreEqual(errorMessages.Count, totalAdults, "error message count does not match with number of occupants");
        }
        
        [Then(@"Appropriate error message is displayed for adult age less than (.*)")]
        public void ThenAppropriateErrorMessageIsDisplayedForAdultAgeLessThan(int adultAgeLimit)
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                _travelInsurance.PopulateDOBOnInsuranceCard(_landingPageGuestComponent.GetRoomOccupantDetails(), adultAgeLimit - 1);
            else
                 _travelInsurance.PopulateDOBOnInsuranceCard(_guestComponent.GetRoomOccupantDetails(), adultAgeLimit - 1);
            List<string> errorMessages = new List<string>();
            errorMessages = _travelInsurance.GetAllAdultErrorMessages();
            foreach (var error in errorMessages)
            {
                Assert.AreEqual(InsuranceErrorMessages.AdultEMForUnderAge, error.ToString());
            }

            int totalAdults = 0;
            List<RoomOccupantDetails> lRoomOccupantDetails;
            if (HelperFunctions.IsV3HomepageEnabled())
                lRoomOccupantDetails = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                lRoomOccupantDetails = _guestComponent.GetRoomOccupantDetails();
            for (int i = 0; i < lRoomOccupantDetails.Count; i++)
            {
                totalAdults += lRoomOccupantDetails[i].NoOfAdults;
            }
            Assert.AreEqual(errorMessages.Count, totalAdults, "error message count does not match with number of occupants");
        }

        [Then(@"Green tick should be displayed on entering valid DOB for all occupants")]
        public void ThenGreenTickShouldBeDisplayedOnEnteringValidDOBForAllOccupants()
        {

            int totalOccupants = 0;
            List<RoomOccupantDetails> lRoomOccupantDetails;
            if (HelperFunctions.IsV3HomepageEnabled())
                lRoomOccupantDetails = _landingPageGuestComponent.GetRoomOccupantDetails();
            else
                lRoomOccupantDetails = _guestComponent.GetRoomOccupantDetails();
            _travelInsurance.PopulateDOBOnInsuranceCard(lRoomOccupantDetails);
            for (int i = 0; i < lRoomOccupantDetails.Count; i++)
            {
                totalOccupants += lRoomOccupantDetails[i].NoOfAdults + lRoomOccupantDetails[i].NoOfChildren + lRoomOccupantDetails[i].NoOfInfants;
            }
            Assert.AreEqual(_travelInsurance.ReturnGreenTickCount(), totalOccupants);
        }

        [When(@"Select I have my own travel insurance option")]
        public void WhenSelectIHaveMyOwnTravelInsuranceOption()
        {
            _travelInsurance.SelectIHaveMyOwnTravelInsurance();
        }

        [Then(@"Insurance not added pop up should be displayed")]
        public void ThenInsuranceNotAddedPopUpShouldBeDisplayed()
        {
            Assert.IsTrue(_modalPopup.IsModalDisplayed(),"Pop up Modal is not displayed");
        }

        [When(@"I click on add insurance button")]
        public void WhenIClickOnAddInsuranceButton()
        {
            _travelInsurance.ClickAddInsuranceFromPopUp();
        }

        [Then(@"Pop up should be closed and available Insurance options should be displayed")]
        public void ThenPopUpShouldBeClosedAndAvailableInsuranceOptionsShouldBeDisplayed()
        {
           Assert.IsFalse(_modalPopup.IsModalDisplayed(), "Pop up modal is not closed");
           Assert.IsTrue(_travelInsurance.IsTravelInsuranceAvailable(), "Insurance options are not available");
        }

        [When(@"Click on continue to book button")]
        public void WhenClickOnContinueToBookButton()
        {
            _holidayExtrasPage.ClickContinueToBookButton();
        }

        [Then(@"Insurance product name should be based on (.*)")]
        public void ThenInsuranceProductNameShouldBeBasedOn(string destination)
        {
            if (_travelInsurance.IsTravelInsuranceAvailable())
            {
                TravelInsuranceInformation travelInsuranceInformation = new TravelInsuranceInformation();
                switch (destination.ToUpper())
                {
                    case "TENERIFE":
                        travelInsuranceInformation = _travelInsurance.CaptureAndGetTravelInsuranceInformation(1);
                        Assert.AreEqual(travelInsuranceInformation.PolicyName, "Europe - Essential Single Trip");
                        travelInsuranceInformation = _travelInsurance.CaptureAndGetTravelInsuranceInformation(2);
                        Assert.AreEqual(travelInsuranceInformation.PolicyName, "Europe - Super Single Trip");
                        break;

                    case "DUBAI":
                        travelInsuranceInformation = _travelInsurance.CaptureAndGetTravelInsuranceInformation(1);
                        Assert.AreEqual(travelInsuranceInformation.PolicyName, "Worldwide (exc USA and Canada) - Essential Single Trip");
                        travelInsuranceInformation = _travelInsurance.CaptureAndGetTravelInsuranceInformation(2);
                        Assert.AreEqual(travelInsuranceInformation.PolicyName, "Worldwide (exc USA and Canada) - Super Single Trip");
                        break;

                    case "NEW YORK":
                        travelInsuranceInformation = _travelInsurance.CaptureAndGetTravelInsuranceInformation(1);
                        Assert.AreEqual(travelInsuranceInformation.PolicyName, "Worldwide - Essential Single Trip");
                        travelInsuranceInformation = _travelInsurance.CaptureAndGetTravelInsuranceInformation(2);
                        Assert.AreEqual(travelInsuranceInformation.PolicyName, "Worldwide - Super Single Trip");
                        break;

                    default:
                        Assert.Fail("Invalid location, please check the test data");
                        break;

                }
            }
        }

        [Then(@"Baggage is not available in the extras page")]
        public void ThenBaggageIsNotAvailableInTheExtrasPage()
        {
            Assert.IsFalse(_baggage.IsAddBaggageOptionAvailable(), "Add baggage section is visible in flight allocation");
        }

        [Then(@"Custom baggage message is displayed")]
        public void ThenCustomBaggageMessageIsDisplayed()
        {
            Assert.IsTrue(_baggage.IsCustomBaggageTextDisplayed(), "Custom baggage text is not displayed");
            Assert.AreEqual(string.Format("{0} {1}", _guestComponent.GetNonInfantOccupants().ToString(), "x 23kg hold luggage"), _baggage.GetCustomBaggageWeightText(), "Baggage weight validation in custom baggage text section");
        }

        [Then(@"Infant price should be zero")]
        public void ThenInfantPriceShouldBeZero()
        {
            if(HelperFunctions.IsV3HomepageEnabled())
                Assert.IsTrue(_travelInsurance.IsInfantInsurncePriceZero(_landingPageGuestComponent.GetRoomOccupantDetails()), "Infant price is not zero");
            else
               Assert.IsTrue(_travelInsurance.IsInfantInsurncePriceZero(_guestComponent.GetRoomOccupantDetails()), "Infant price is not zero");
        }

        [Then(@"Baggage added status should be displayed")]
        public void ThenBaggageAddedStatusShouldBeDisplayed()
        {
            Assert.IsTrue(_baggage.IsBaggegAddedStatusUpdated(), "Baggage Status is not updated");
        }

        [Then(@"Baggage Added message is displayed on toast")]
        public void ThenBaggageAddedMessageIsDisplayedOnToast()
        {
            Assert.IsTrue(_baggage.GetBaggageToastMessage().Contains("Baggage added to your basket"), "Baggage Added message is not displayed on Toast");
        }

        [When(@"Remove Baggage")]
        public void WhenRemoveBaggage()
        {
            _baggage.RemoveBaggage();
        }

        [Then(@"Baggage Remove message is displayed on toast")]
        public void ThenBaggageRemoveMessageIsDisplayedOnToast()
        {
            Assert.IsTrue(_baggage.GetBaggageToastMessage().Contains("Baggage removed from your basket"), "Baggage Added message is not displayed on Toast");
        }
        
    }
}