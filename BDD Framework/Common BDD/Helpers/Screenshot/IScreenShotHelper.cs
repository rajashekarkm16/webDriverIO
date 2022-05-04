
using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.Helpers.Screenshot
{
    public interface IScreenShotHelper
    {
        string TakeScreenshot(IWebDriver driver, string outputDirectory, string filePath, bool isFullScreenShot=false);
    }
}