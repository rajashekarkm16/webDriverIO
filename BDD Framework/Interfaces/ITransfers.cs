using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
   public interface ITransfers
    {
        int GetTransferToSelect();
        int GetTransferResultsCount();
        void SelectTransfer(int transferToSelect);
        string GetTransferName(int transferToSelect);
        string GetTransferDurationText(int transferToSelect);
        decimal GetPerPersonTransferPrice(int transferToSelect);
        decimal GetTotalTransferPrice(int transferToSelect);
        void CaptureTransferInformation(int transferToSelect);
        TransferInformation GetTransferInformation();
        bool IsTransferSectionVisible();
        bool IsTransferAdded();
        bool IsTransferSelected(int transferToSelect);
        void CaptureDefaultMandatoryTransferDetails();
        List<TransferInformation> GetMandatoryTransferDetails();
        void PopulateTransferFlightDetails();
        bool IsComplimentaryTransferFlagDisplayed();
        void CaptureAndSelectMandatoryTransferDetails();
        bool IsImportantInformationDisplayed();
        bool IsToastMessageDisplayed();
        List<TransferInformation> CaptureAllMandatoryTransferDetails(int roomNo = 1);
        string ReturnHeaderText();
        void PopulateInvalidFlightDetails();
        List<string> GetFlightsValidationErrorMessages();
        void RemoveSelectedTransfer(int transferToSelect);
    }
}
