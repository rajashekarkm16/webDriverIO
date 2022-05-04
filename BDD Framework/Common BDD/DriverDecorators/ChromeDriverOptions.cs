using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace Dnata.Automation.BDDFramework.DriverDecorators
{
    public class ChromeDriverOptions : ChromeOptions
    {
        public Dictionary<string, object> Prefs { get; set; }
    }
}