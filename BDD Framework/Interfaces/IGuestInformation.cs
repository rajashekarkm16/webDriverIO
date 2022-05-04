using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IGuestInformation
    {
        void ProceedToNextPage();
        void ConfirmBooking(bool isThreedDS = false, bool isAuthorised = true);
        void ValidateMandatoryFieldErrorMessages();
        void ToggleSpecialRequestsSection();
        void AddSpecialRequestsText();
        void SelectTitle(int roomNo = 1);
        void EnterFirstName(string name, int roomNo = 1);
        void ValidateFirstNameInGuestInfo(bool isValid, int roomNo = 1);
        void EnterSurName(string name, int roomNo = 1);
        void EnterPhoneNumber(string number);
        void EnterEmailAddress(string email);
        void ValidateSurNameInGuestInfo(bool isValid, int roomNo = 1);
        void ValidatePhoneNoInGuestInfo(bool isValid, int roomNo = 1);
        void ValidateEmailInGuestInfo(bool isValid);
        void PopulatePassengers(List<RoomOccupantDetails> roomOccupantDetails, bool isThreedDS = false);
        void PopulatePassengersWithNames(List<RoomOccupantDetails> roomOccupantDetails, string fname, string lastName);
        void PopulatePassengerInfo(int counter, int age, bool isAdult, string firstName, string lastName);
        void PopulateInvaildPassengers(List<RoomOccupantDetails> roomOccupantDetails, int adultAge, int childAge, int infantAge, bool isThreeDS = false);
        void PopulateHotelGuests(List<RoomOccupantDetails> roomOccupants, bool isThreeDS = false);
        void PopulateHotelGuestsWithNames(List<RoomOccupantDetails> roomOccupants, string firstName, string lastName);
        void EnterInvalidSpecialRequestsText(int size);
        bool IsSpecialRequestsErrorMessageVisible();
        string GetSpecialRequestsErrorMessage();
        string GetSecureCheckoutMessage();
        bool IsOfferAndDiscountCheckboxVisible();
        string GetOfferAndDiscountsText();
        List<string> GetAllErrorMessages();
        string GetAdultPassengerCaption();
        string GetChildPassengerCaption();
        string GetInfantPassengerCaption();
        bool IsEmailSuccessIconVisible();
        bool IsPhoneNumberSuccessIconVisible();
        bool IsEmailFieldDisabled();
        bool IsLeadGuestNamePrePopulated();
        bool IsChangeLeadGuestPrePopulatedLinkVisible();
        void ClickChangePrePopulatedLeadGuestName();
        bool IsTitleSuccessIconVisible();
        bool IsFirstNameSuccessIconVisible();
        bool IsSurNameSuccessIconVisible();
        List<PassengerInformation> GetPassengerInformation();
        bool ValidateVocherCodeDetailsInTheBookingForm();
        void ApplyVoucherCode(string VoucherCode);
        bool ValidateVoucherCodeAppliedSuccessMessage();
        void RemoveVoucherCode();
        bool ValidateVoucherCodeRemovedSuccessMessage();
        bool ValidateInvalidOrExpiredVoucherCodeMessage(string voucherCode);
        bool ValidateMessageForPromoCriteriaNotMet(string voucherCode);
        bool ValidateBetterDealMessage(string voucherCode);
    }
}
