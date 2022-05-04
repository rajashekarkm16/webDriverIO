using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Interfaces
{
    public interface IBase
    {
        void NavigateBack();

        IReadOnlyCollection<object> GetAllDataLayerEntries();
        Dictionary<string, dynamic> GetGAData(string key, string value);

        Dictionary<string, dynamic> GetGADataByKeyAndValueIgnoringEarlierData(IReadOnlyCollection<Object> ignoreData, string key, string value);
    }
}
