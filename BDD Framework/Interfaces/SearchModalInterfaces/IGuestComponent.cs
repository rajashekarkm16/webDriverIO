using Dnata.TravelRepublic.MobileWeb.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IGuestComponent
    {
        string GetGuestPageTitle();
        List<RoomOccupantDetails> GetRoomOccupantDetails();
        List<RoomOccupantDetails> SetRoomsData(string roomsData = "2,0,0");
        void PopulateGuests(List<RoomOccupantDetails> roomOccupantDetails);
        void VerifyGuests(List<RoomOccupantDetails> roomOccupantDetails);
        void PopulateGuestsWithoutChildAge(List<RoomOccupantDetails> roomOccupantDetails);
        void ClearAddedGuests();
        void ConfirmNumberOfGuests();
        bool ValidateChildErrorMessage(int NoOfRooms);
        void SelectRooms(int roomCount);
        bool CheckRoomDetails(int roomCount);
        bool CheckAdultsIncrementButton(List<RoomOccupantDetails> roomOccupantDetails, int maxAdults);
        bool CheckAdultsDecrementButton(List<RoomOccupantDetails> roomOccupantDetails, int minAdults);
        bool CheckChildrenIncrementButton(List<RoomOccupantDetails> roomOccupantDetails, int maxChildren);
        bool CheckChildrenDecrementButton(List<RoomOccupantDetails> roomOccupantDetails, int minChildren);
        int GetNonInfantOccupants();
        string GetNeedMoreRoomsText();
        void PopulateNewChildAge(int newAge);
        void CloseIconButton();
        int GetSelectedChildAge(int childToSelect);
    }
}
