using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface ITravelInsurance
    {
        int GetInsuranceToSelect();
        int GetTravelInsuranceCount();
        bool IsTravelInsuranceAvailable();
        string GetTravelInsuranceName(int insuranceToSelect);
        decimal GetInsurancePerPersonPrice(int insuranceToSelect);
        decimal GetTotalInusrancePrice(int insuranceToSelect);
        bool IsInsuranceNotRequiredboxSelected();
        void AddTravelInsurance(int insuranceToSelect, List<RoomOccupantDetails> roomOccupantDetails);
        void CaptureTravelInsuranceInformation(int insuranceToSelect);
        TravelInsuranceInformation GetTravelInsuranceInformation();
        TravelInsuranceInformation CaptureAndGetTravelInsuranceInformation(int insuranceToSelect);
        bool IsInsuranceAdded();
        bool IsDefaultInsuranceSelected();
        void ClickMoreInsuranceInfoLink();
        void ClickViewPolicyDocumentationLink();
        string GetPDFLinkUrl();
        void ClickViewInsuranceProductInformationDocumentLink();
        bool IsInsuranceModalDisplayed();
        void ClickViewPolicyInformationLink();
        void CloseInsurancePopUpModal();
        void ClickInsuranceCard(int insuranceToSelect);
        bool IsAddInsuranceToBasketButtonEnabled();
        void PopulateDOBOnInsuranceCard(List<RoomOccupantDetails> roomOccupantDetails,int AdultAge= Constants.AdultAge, int ChildAge = Constants.ChildAge, int InfantAge = Constants.InfantAge);       
        bool IsFromQualifierAbovePriceIsDisplayed();
        void ValidateRetainedDOBOnInsuranceCard(List<RoomOccupantDetails> roomOccupantDetails);
        void ClickUpdateBasketBuuton();
        List<string> GetAllAdultErrorMessages();
        List<string> GetAllChildErrorMessages();
        void SelectIHaveMyOwnTravelInsurance();
        void ClickAddInsuranceFromPopUp();
        int ReturnGreenTickCount();
        bool IsInfantInsurncePriceZero(List<RoomOccupantDetails> roomOccupantDetails);
    }
}
