using System.ComponentModel;

namespace Dnata.Automation.BDDFramework.Enums
{
    public enum AtExceptionTypes
    {
        [Description("Navigation Error")] Navigation,
        [Description("Seeding Error")] Seeding,
        [Description("Assertion Error")] Assertion,
        [Description("Configuration Error")] Configuration,
        [Description("Driver Initialization Error")] DriverInitialization,
    }
}