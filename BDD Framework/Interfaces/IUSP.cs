using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IUSP
    {
        string GetHeading(int index);
        string GetBody(int index);
        void ClickUSP(int index);
    }
}
