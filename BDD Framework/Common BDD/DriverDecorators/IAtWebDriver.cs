using System;
using System.Collections.ObjectModel;
using System.Threading;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using OpenQA.Selenium;

namespace Dnata.Automation.BDDFramework.DriverDecorators
{
    public interface 
        IAtWebDriver : IWebDriver
    {
      
        bool IsDriverActive { get; set; }

        Guid InstanceId { get; }

        bool IsElementVisible(By by);

        bool IsElementPresent(By by);

        #region [ Find Elements ]

        AtWebElement FindElement(AtBy atBy);

        ReadOnlyCollection<AtWebElement> FindElements(AtBy atBy);

        AtWebElement FindElementByIndex(AtBy atBy, int index = 0);


        AtWebElement FindElement(LocatorType locatorType, string locatorValue, params string [] param);

        ReadOnlyCollection <AtWebElement> FindElements(LocatorType locatorType, string locatorValue, params string[] param);

        AtWebElement FindElementByIndex(LocatorType locatorType, string locatorValue, int index, params string[] replaceHashValues);

        ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, LocatorType locatorType,
        string locatorValue, params string[] replaceHashValues);

        ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, AtBy atBy);
        #endregion

        #region [ Browser ]

        string OpenNewActiveTab(string url);

        void CloseTab(string tabInstance);

        string GetActiveTabId();

        void GoToTab(string tabInstance);

        void LogBrowserLogsToConsole();

        void CloseAllTabs();

        void CloseAdditionalWindows(string mainWindow);

        void GotoUrl(string url);

        void NavigateBack();

        void NavigateForward();

        void Refresh();

        void SwitchToWindow(string title);

        void SwitchToWindow(int index);

        bool IsWindowPresent(string title);

        bool IsNewWindowOpened();

        void SwitchToActiveElement();

        void FullScreen();

        void Maximize();

        void Minimize();

        string TitleOfThePage();

        void SetCookie(string cookieName, string value);

        string GetCookieValue(string cookieName);

        void DeleteAllCookies();

        void DeleteCookie(string cookieName);

        void ClearWebStorage();

        #endregion

        #region[Java Script]
        object ExecuteScript(string script, params object[] args);

        string GetTextOfWebElement(string xpath);

        #endregion

        #region[Alert Handling]
        void AcceptAlert();

        void DismissAlert();

        string GetAlertText();

        void SetTextToAlert(string text);

        bool IsAlertPresent();
        #endregion

        #region [ Actions ]

        void MoveToElement(By by);

        void MoveToElement(IAtWebElement element);

        #endregion

        #region [ Wait Methods]
        void WaitInSec(int sec = 2);

        void WaitForPageLoaded();

        void WaitForDomReady(int pollingAttempts = 240);

        void WaitForDomReady(TimeSpan? pollingInterval, int pollingAttempts = 240);

        void WaitAjax();

        void SetImplicitWait(TimeSpan waitTimeSpan);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementVisible(AtWebElement atElement);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementVisible(AtWebElement atElement, int secs);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementVisible(AtWebElement atElement, TimeSpan pollingDuration, int iterations);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementPresent(AtWebElement atElement);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementPresent(AtWebElement atElement, int secs);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementPresent( AtWebElement atElement, TimeSpan pollingDuration, int iterations);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForTextPresent(AtWebElement atElement, string text, TimeSpan pollingDuration, int iterations);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForAttributePresent( AtWebElement atElement, string attribute, string attributeValue, TimeSpan pollingDuration, int iterations);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementClickable(IAtWebElement element, int secs, string message = "Element is not clickable ");

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitUntilNotVisible(AtWebElement element, int secs, string message = "Element is visible");

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForAnyVisible(IAtWebElement elementA, IAtWebElement elementB, int secs, string message);

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitForElementClickable(IAtWebElement element, string message = "Object is not clickable");

        [Obsolete("This Method will be Deprecated soon, use the ones with atby as parameter")]
        void WaitUntilNotVisible(AtWebElement element, string message = "Object is visible");

        void WaitForElementVisible(AtBy atBy, string message);

        void WaitForElementVisible(AtBy atBy, int secs, string message);

        void WaitForElementVisible(AtBy atBy, TimeSpan pollingDuration, int iterations);

        void WaitForElementPresent(AtBy atBy);

        void WaitForElementPresent(AtBy atBy, int secs);

        void WaitForElementPresent(AtBy atBy, TimeSpan pollingDuration, int iterations);

        void WaitForTextPresent(AtBy atBy, string text, TimeSpan pollingDuration, int iterations);

        void WaitForAttributePresent(AtBy atBy, string attribute, string attributeValue, TimeSpan pollingDuration, int iterations);

        void WaitForElementClickable(AtBy atBy, int secs, string message = "Element is not clickable");

        void WaitUntilNotVisible(AtBy atBy, int secs, string message = "Element is visible");

        void WaitForAnyVisible(AtBy atByA, AtBy atByB, int secs, string message = "Either of the elements are not visible");

        #endregion

        #region [Switch and Scroll]

        void SwitchToFrame(int index);

        void SwitchToFrame(string frame);

        void SwitchToDefaultContent();

        void SwitchToFrame(IAtWebElement atElement);

        void SwitchToParentFrame();

        void ScrollDown(int y = 200);

        void ScrollTo(int x, int y);

        void ScrollToElement(AtWebElement element);

        void ScrollToTop();

        void ScrollToBottom();

        void ScrollElementToCenter(AtWebElement element);

        #endregion
    }
}