using Dnata.Automation.BDDFramework.BasePage;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.TravelRepublic.MobileWeb.UI
{
    public class MobileBasePage : BasePage, IBase
    {

        IAtWebDriver _webDriver;
        public MobileBasePage(IAtWebDriver webDriver)
            :base(webDriver)
        {
            _webDriver = webDriver;
        }

        public IReadOnlyCollection<object> GetAllDataLayerEntries()
        {
            return HelperFunctions.GetAllDataLayerEntries(_webDriver);
        }
        public Dictionary<string, dynamic> GetGAData(string key, string value)
        {
            return HelperFunctions.GetGADataByKeyAndValue(_webDriver, key, value);
        }

        public Dictionary<string, dynamic> GetGADataByKeyAndValueIgnoringEarlierData(IReadOnlyCollection<Object> ignoreData, string key, string value)
        {
            return HelperFunctions.GetGADataByKeyAndValueIgnoringEarlierData(_webDriver, ignoreData, key, value);
        }

    }
}
