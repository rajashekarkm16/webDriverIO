using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnata.TravelRepublic.MobileWeb.UI.Models;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IHolidayExtrasPage
    {
        void ContinueToBook();
        void ProceedIfExtrasPageIsVisible();
        bool IsContinueToBookEnabled();
        void ClickContinueToBookButton();
        void ClickConfirmandContinueToBook();
    }
}
