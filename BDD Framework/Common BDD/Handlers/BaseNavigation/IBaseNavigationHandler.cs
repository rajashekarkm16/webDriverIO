using System;
using System.Collections.ObjectModel;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.Handlers.BaseNavigation
{
    public interface IBaseNavigationHandler
    {
        AtWebElement FindElement(IAtWebDriver driver, AtBy atBy);

        ReadOnlyCollection<AtWebElement> FindElements(IAtWebDriver driver, AtBy atBy);

        ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, IAtWebDriver driver, AtBy atBy);

        AtWebElement FindElementByIndex(IAtWebDriver driver, AtBy atBy, int index = 0);

        AtWebElement FindElement(IAtWebDriver driver, LocatorType locatorType, string locatorValue, params string[] replaceHashValues);

        AtWebElement FindElementByIndex(IAtWebDriver driver, LocatorType locatorType, string locatorValue, int index, params string[] replaceHashValues);

        ReadOnlyCollection<AtWebElement> FindElements(IAtWebDriver driver, LocatorType locatorType, string locatorValue, params string[] replaceHashValues);

        ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, IAtWebDriver driver, LocatorType locatorType,
         string locatorValue, params string[] replaceHashValues);

        bool IsElementPresent(IAtWebDriver driver, By by);

        #region [ Browser ]
        string OpenNewActiveTab(IAtWebDriver driver, string url);

        void CloseTab(IAtWebDriver driver, string tabInstance);

        string GetActiveTabId(IAtWebDriver driver);

        void GoToTab(IAtWebDriver driver, string tabInstance);

        void GotoUrl(IAtWebDriver driver, string url);

        void NavigateBack(IAtWebDriver driver);

        void NavigateForward(IAtWebDriver driver);

        void Refresh(IAtWebDriver driver);

        void SwitchToWindow(IAtWebDriver driver, string title, int waitingTime);

        bool IsWindowPresent(IAtWebDriver driver, string title);

        bool IsNewWindowOpened(IAtWebDriver driver, int currentWindowCount = 1);

        void SwitchToWindow(IAtWebDriver driver, int index);

        void SwitchToActiveElement(IAtWebDriver driver);

        void FullScreen(IAtWebDriver driver);

        void Maximize(IAtWebDriver driver);

        void Minimize(IAtWebDriver driver);

        void SetCookie(IAtWebDriver driver, string cookieName, string value);
        
        string GetCookieValue(IAtWebDriver driver, string cookieName);
        
        void DeleteAllCookies(IAtWebDriver driver);

        void DeleteCookie(IAtWebDriver driver, string cookieName);

        void ClearWebStorage(IAtWebDriver driver);
        #endregion

        #region [ Javascript ]

        object ExecuteScript(IAtWebDriver driver, string script, params object[] args);

        string GetTextOfWebElement(IAtWebDriver driver, string xpath);
        #endregion

        #region [ Alerts Handling ]

        void AcceptAlert(IAtWebDriver driver);

        void DismissAlert(IAtWebDriver driver);

        string GetAlertText(IAtWebDriver driver);

        void SetTextToAlert(IAtWebDriver driver, string text);

        bool IsAlertPresent(IAtWebDriver driver);
        #endregion

        #region [ Actions ]

        void MoveToElement(IAtWebDriver driver, By by);

        void MoveToElement(IAtWebDriver driver, IAtWebElement element);

        #endregion

        #region [ Frames Handling ]
        void SwitchToFrame(IAtWebDriver driver, int index);

        void SwitchToFrame(IAtWebDriver driver, string frame);

        void SwitchToFrame(IAtWebDriver driver, IAtWebElement atElement);

        void SwitchToDefaultContent(IAtWebDriver driver);

        void SwitchToParentFrame(IAtWebDriver driver);
        #endregion

        #region [ Wait Methods ]

        void WaitInSec(int sec = 2);

        bool IsElementVisible(IAtWebDriver driver, By by);

        void WaitForDomReady(IAtWebDriver driver, TimeSpan pollingDuration, int iterations);

        void WaitAjax(IAtWebDriver driver);

        void SetImplicitWait(IAtWebDriver driver, TimeSpan waitTimeSpan);

        void WaitForPageLoaded(IAtWebDriver driver);

        void WaitForElementVisible(IAtWebDriver driver, AtWebElement atElement, int secs);

        void WaitForElementVisible(IAtWebDriver driver, AtBy atBy, int secs, string message);

        void WaitForElementVisible(IAtWebDriver driver, AtWebElement atElement, TimeSpan pollingInterval, int iterations);

        void WaitForElementVisible(IAtWebDriver driver, AtBy atBy,  TimeSpan pollingInterval, int iterations);

        void WaitForElementPresent(IAtWebDriver driver, AtWebElement atElement, int secs);
        void WaitForElementPresent(IAtWebDriver driver, AtBy atBy, int secs);

        void WaitForElementPresent(IAtWebDriver driver, AtWebElement atElement, TimeSpan pollingInterval, int iterations);
        void WaitForElementPresent(IAtWebDriver driver, AtBy atBy, TimeSpan pollingInterval, int iterations);

        void WaitForTextPresent(IAtWebDriver driver, AtWebElement atElement, string text, TimeSpan pollingInterval, int iterations);
        void WaitForTextPresent(IAtWebDriver driver, AtBy atBy, string text, TimeSpan pollingInterval, int iterations);

        void WaitForAttributePresent(IAtWebDriver driver, AtWebElement atElement, string attribute, string attributeValue, TimeSpan pollingInterval, int iterations);
        void WaitForAttributePresent(IAtWebDriver driver, AtBy atBy, string attribute, string attributeValue, TimeSpan pollingInterval, int iterations);

        void WaitForElementClickable(IAtWebDriver driver, IAtWebElement element, int secs, string message = "Element is not clickable");
        void WaitForElementClickable(IAtWebDriver driver, AtBy atBy, int secs, string message = "Element is not clickable");

        void WaitUntilNotVisible(IAtWebDriver driver, AtWebElement element, int secs, string message = "Element is visible");
        void WaitUntilNotVisible(IAtWebDriver driver, AtBy atBy, int secs, string message = "Element is visible");

        void WaitForAnyVisible(IAtWebDriver driver, IAtWebElement elementA, IAtWebElement elementB, int secs, string message);
        void WaitForAnyVisible(IAtWebDriver driver, AtBy atByA, AtBy atByB, int secs, string message);

        void LogBrowserLogsToConsole(IAtWebDriver driver);

        void CloseAllTabs(IAtWebDriver driver);

        void CloseAdditionalWindows(IAtWebDriver driver, string mainWindow, TimeSpan pollingInterval, int iterations);
        #endregion

        #region [Handling Scroll]

        void ScrollDown(IAtWebDriver driver, int y = 200);

        void ScrollTo(IAtWebDriver driver, int x, int y);

        void ScrollToElement(IAtWebDriver driver, AtWebElement element);

        void ScrollToTop(IAtWebDriver driver);

        void ScrollToBottom(IAtWebDriver driver);

        void ScrollElementToCenter(IAtWebDriver driver, AtWebElement element);
        #endregion
    }

}