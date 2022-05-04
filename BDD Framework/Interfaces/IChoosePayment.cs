using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IChoosePayment
    {
        void ChoosePaypal(bool isPaypal = false);
        void ChoosePaymentPlan(bool isDeposit = false, bool isPayMonthly = false);
        int GetProgressBarCount();
        string GetLocalTaxMessageFromPayDepositSection();
        string GetLocalTaxMessageFromPayFullSection();
        decimal GetLocalTaxAmountFromPayDepositSection();
        decimal GetLocalTaxAmountFromPayFullSection();
        void CheckCovidNoticeCheckbox();
        bool GetIsDepositPayment();
        bool GetIsMonthlyPayment();
        int GetBalanceInstallmentsCount();
        void SetBalanceInstallmentsCount(int count);
        decimal GetAdminFees();
        void SetAdminFees(decimal amount);
    }
}
