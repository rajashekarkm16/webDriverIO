using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IRoomSelectionSummary
    {
        string GetDialogHeader();
        string GetRoomType(int roomNo);
        string GetOccupants(int roomNo);
        void ClickChangeRoom(int roomNo);
        decimal GetRoomPrice(int roomNo);
        decimal GetTotalPrice();
        void CloseRoomSelectionModal();
    }
}
