using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Models
{
    public class BoardTypeInformation
    {
        public string BoardType = "";
        public decimal BoardTypePrice = 0;
        public bool isNonRefundable = false;
        public bool isAmendmentAvailable = false;
        public bool isDeposit = false;
        public bool isFlexible = false;
        public bool isRefundable = false;
        public decimal LocalTax = 0;
    }
}
