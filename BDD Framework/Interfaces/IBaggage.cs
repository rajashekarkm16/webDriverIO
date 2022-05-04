using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IBaggage
    {
        int GetBaggageToSelect();
        void SelectBaggage(int bagCount);
        int RandomizeBagsToSelect();
        int GetBaggageCount();        
        decimal GetEachWayBaggagePrice();
        decimal GetTotalPricePerBagPrice();
        double GetBagWeight();
        string GetBaggageDefaultText();
        bool IsAddBaggageOptionAvailable();
        void CaptureBaggageInformation();
        BaggageInformation GetBaggageInformation();
        bool IsBaggageAdded();
        bool IsCustomBaggageTextDisplayed();
        string GetCustomBaggageWeightText();
        string GetCustomHandLuggageText();
        bool VerifyBaggageIncludeMessage();
        bool VerfiyAddBagSectionText();
        decimal GetTotalPriceonBaggaeToast();
        void RemoveBaggage();
        string GetBaggageToastMessage();
        bool IsBaggegAddedStatusUpdated();
    }
}
