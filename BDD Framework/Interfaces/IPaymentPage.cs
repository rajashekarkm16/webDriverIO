using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IPaymentPage
    {
        void SetCardNumber(string cardType = Constants.DefaultCardType);
        void SetExpiry();
        void SetSecurityNumber(string cardType = Constants.DefaultCardType);
        void ReenterPaymentDetailsIfError(string cardType = Constants.DefaultCardType);
        void AutoPopulateAddress(string addressKeyword);
        bool HasBookingFailed();
    }
}
