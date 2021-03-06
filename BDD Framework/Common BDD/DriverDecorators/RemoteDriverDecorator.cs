using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dnata.Automation.BDDFramework.BrowserStack;
using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Handlers.BaseNavigation;
using Dnata.Automation.BDDFramework.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace Dnata.Automation.BDDFramework.DriverDecorators
{
    public class RemoteDriverDecorator : RemoteWebDriver, IAtWebDriver
    {
        private readonly IBaseNavigationHandler _navigationHandler;

        public Guid InstanceId { get; } = Guid.NewGuid();

        public bool IsDriverActive { get; set; } = true;

        private static string ChromeDriverDirectory => AtConfiguration.GetDriverDirectory();

        private static int DefaultPollingInterval =>
            AtConfiguration.GetDefaultConfiguration("DefaultPollingInterval", 250);

        private static int DefaultHttpRequestTimeout =>
            AtConfiguration.GetDefaultConfiguration("DefaultHttpRequestTimeOut", 120);

        private static int DefaultJavaScriptTimeout =>
            AtConfiguration.GetDefaultConfiguration("DefaultJavaScriptTimeout", 120);

        private static int DefaultWaitForElement =>
            AtConfiguration.GetDefaultConfiguration("DefaultWaitForElement", 30);

        private static int DefaultPageLoadTimeout =>
            AtConfiguration.GetDefaultConfiguration("SetPageLoadTimeout", 30);

        private static int DefaultIterations => AtConfiguration.GetDefaultConfiguration("DefaultPollingAttempts", 5);

        private static readonly bool SetIncognito = AtConfiguration.GetDefaultConfiguration("IsIncognitoEnabled", false);

        private static ChromeDriverOptions ChromeOptions
        {
            get
            {
                var chromeDriverOptions = new ChromeDriverOptions();
                if (SetIncognito)
                    chromeDriverOptions.AddArgument("--incognito");
                chromeDriverOptions.AddArgument("test-type");
                chromeDriverOptions.AddArgument("start-maximized");
                chromeDriverOptions.AddArgument("--disable-extensions");
                chromeDriverOptions.AddUserProfilePreference("credentials_enable_service", false);
                chromeDriverOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                chromeDriverOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
                chromeDriverOptions.Prefs = new Dictionary<string, object>
                {
                    {
                        "ensureCleanSession",
                        true
                    },
                    {
                        "browser.cache.disk.enable",
                        true
                    },
                    {
                        "browser.cache.memory.enable",
                        true
                    },
                    {
                        "browser.cache.offline.enable",
                        true
                    },
                    {
                        "network.http.use-cache",
                        true
                    }
                };
                return chromeDriverOptions;
            }
        }

        private static FirefoxOptions FirefoxOptions
        {
            get
            {
                var firefoxOptions = new FirefoxOptions
                {
                    AcceptInsecureCertificates = true
                };

                return firefoxOptions;
            }
        }

        private static InternetExplorerOptions InternetExplorerDriverOptions
        {
            get
            {
                var internetExplorerOptions = new InternetExplorerOptions
                {
                    EnsureCleanSession = true,
                    EnableNativeEvents = false,
                    IgnoreZoomLevel = true
                };

                return internetExplorerOptions;
            }
        }

        private static EdgeOptions EdgeDriverOptions => new EdgeOptions();

        public static string RemoteURL = AtConfiguration.GetConfiguration<string>("RemoteURL");

        //public static DesiredCapabilities GetDesiredCaps(string browser)
        //{
        //    DesiredCapabilities caps = new DesiredCapabilities();
        //    caps.SetCapability("browserstack.user", "svsprasad3");
        //    caps.SetCapability("browserstack.key", "FQSpwvLe3LPrJSNsLGDU");
        //    caps.SetCapability("browser", browser);
        //    caps.SetCapability("os", "OS X");
        //    caps.SetCapability("os_version", "Big Sur");
        //    caps.SetCapability("browserstack.local", "true");
        //    caps.AcceptInsecureCerts = true;
        //    return caps;
        //}

        public static DriverOptions GetDriverOptions(BrowserStackConfig browserStackConfig, bool isLocalNetwork, string projectName, string featureName, string scenarioName)
        {
            ChromeOptions chrome = new ChromeOptions();
            EdgeOptions edge = new EdgeOptions();
            FirefoxOptions firefox = new FirefoxOptions();
            SafariOptions safari = new SafariOptions();
            if (browserStackConfig.Browser.Equals("Chrome"))
            {
                chrome.AddAdditionalCapability("project", projectName, true);
                chrome.AddAdditionalCapability("build", featureName, true);
                chrome.AddAdditionalCapability("name", scenarioName, true);
                if (browserStackConfig.Environment.Equals("Desktop"))
                    chrome.AddAdditionalCapability("os", browserStackConfig.OS, true);
                chrome.AddAdditionalCapability("os_version", browserStackConfig.OSVersion, true);
                if (isLocalNetwork)
                    chrome.AddAdditionalCapability("browserstack.local", "true", true);
                if (!browserStackConfig.Environment.Equals("Desktop"))
                {
                    chrome.AddAdditionalCapability("device", browserStackConfig.Device, true);
                    chrome.AddAdditionalCapability("real_mobile", "true", true);
                }                    
                chrome.AddAdditionalCapability("browserstack.user", AtConfiguration.GetConfiguration("BrowserStackUser"), true);
                chrome.AddAdditionalCapability("browserstack.key", AtConfiguration.GetConfiguration("BrowserStackKey"), true);
                chrome.AddArgument("--ignore-certificate-errors");
                return chrome;
            }

            else if (browserStackConfig.Browser.Equals("Edge"))
            {
                edge.AddAdditionalCapability("project", projectName);
                edge.AddAdditionalCapability("build", featureName);
                edge.AddAdditionalCapability("name", scenarioName);
                if (browserStackConfig.Environment.Equals("Desktop"))
                    edge.AddAdditionalCapability("os", browserStackConfig.OS);
                edge.AddAdditionalCapability("os_version", browserStackConfig.OSVersion);
                if (isLocalNetwork)
                    edge.AddAdditionalCapability("browserstack.local", "true");
                if (!browserStackConfig.Environment.Equals("Desktop"))
                {
                    edge.AddAdditionalCapability("device", browserStackConfig.Device);
                    edge.AddAdditionalCapability("real_mobile", "true");
                }                    
                edge.AddAdditionalCapability("browserstack.user", AtConfiguration.GetConfiguration("BrowserStackUser"));
                edge.AddAdditionalCapability("browserstack.key", AtConfiguration.GetConfiguration("BrowserStackKey"));
                return edge;
            }

            else if (browserStackConfig.Browser.Equals("Firefox"))
            {
                firefox.AddAdditionalCapability("project", projectName);
                firefox.AddAdditionalCapability("build", featureName);
                firefox.AddAdditionalCapability("name", scenarioName);
                if (browserStackConfig.Environment.Equals("Desktop"))
                    firefox.AddAdditionalCapability("os", browserStackConfig.OS);
                firefox.AddAdditionalCapability("os_version", browserStackConfig.OSVersion);
                if (isLocalNetwork)
                    firefox.AddAdditionalCapability("browserstack.local", "true");
                if (!browserStackConfig.Environment.Equals("Desktop"))
                {
                    firefox.AddAdditionalCapability("device", browserStackConfig.Device);
                    firefox.AddAdditionalCapability("real_mobile", "true");
                }                    
                firefox.AddAdditionalCapability("browserstack.user", AtConfiguration.GetConfiguration("BrowserStackUser"));
                firefox.AddAdditionalCapability("browserstack.key", AtConfiguration.GetConfiguration("BrowserStackKey"));
                return firefox;
            }

            else if (browserStackConfig.Browser.Equals("Safari"))
            {
                safari.AddAdditionalCapability("project", projectName);
                safari.AddAdditionalCapability("build", featureName);
                safari.AddAdditionalCapability("name", scenarioName);
                safari.AddAdditionalCapability("browserstack.user", AtConfiguration.GetConfiguration("BrowserStackUser"));
                safari.AddAdditionalCapability("browserstack.key", AtConfiguration.GetConfiguration("BrowserStackKey"));
                safari.AddAdditionalCapability("os_version", browserStackConfig.OSVersion);
                safari.AddAdditionalCapability("device", browserStackConfig.Device);
                safari.AddAdditionalCapability("real_mobile", browserStackConfig.IsRealMobile);
                if (isLocalNetwork)
                    safari.AddAdditionalCapability("browserstack.local", "true");
                return safari;
            }

            else
                return null;
        }

        public RemoteDriverDecorator(BrowserStackConfig browserStackConfig, string projectName, string featureName, string scenarioName, string remoteURL, bool isLocalNetwork)
            : base(new Uri(RemoteURL), GetDriverOptions(browserStackConfig, isLocalNetwork, projectName, featureName, scenarioName))
        {
            _navigationHandler = new BaseNavigationHandler();
            var implicitWait = AtConfiguration.GetConfiguration<int>("ImplicitlyWait");

            Manage().Cookies.DeleteAllCookies();
            if(browserStackConfig.Environment.Equals("Desktop"))
                Manage().Window.Maximize();
            Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(DefaultPageLoadTimeout);
            Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(DefaultJavaScriptTimeout);
        }

        public RemoteDriverDecorator(string browser, string remoteURL)
                :base(new Uri(RemoteURL), GetOptions(browser))
        {
            _navigationHandler = new BaseNavigationHandler();
            var implicitWait = AtConfiguration.GetConfiguration<int>("ImplicitlyWait");

            Manage().Cookies.DeleteAllCookies();
            Manage().Window.Maximize();
            Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(DefaultPageLoadTimeout);
            Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(DefaultJavaScriptTimeout);
        }

        public RemoteDriverDecorator(DriverOptions driverOptions, string remoteURL)
          : base(new Uri(RemoteURL), driverOptions)
        {

            _navigationHandler = new BaseNavigationHandler();
            var implicitWait = AtConfiguration.GetConfiguration<int>("ImplicitlyWait");

            Manage().Cookies.DeleteAllCookies();
            Manage().Window.Maximize();
            Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(DefaultPageLoadTimeout);
            Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(DefaultJavaScriptTimeout);
        }

        public static DriverOptions GetOptions(string browser)
        {
            DriverOptions driverOptions = null;
            if (browser.Equals("Chrome", StringComparison.InvariantCultureIgnoreCase))
            {
                driverOptions = ChromeOptions;
            }
            if (browser.Equals("IE", StringComparison.InvariantCultureIgnoreCase))
            {
                driverOptions = InternetExplorerDriverOptions;
            }

            if (browser.Equals("Firefox", StringComparison.InvariantCultureIgnoreCase))
            {
                driverOptions = FirefoxOptions;
            }
            if (browser.Equals("Edge", StringComparison.InvariantCultureIgnoreCase))
            {
                driverOptions = EdgeDriverOptions;
            }
            return driverOptions;
        }

        public bool IsElementVisible(By by)
        {
            return _navigationHandler.IsElementVisible(this, by);
        }

        public bool IsElementPresent(By by)
        {
            return _navigationHandler.IsElementPresent(this, by);
        }

        #region [ Find Elements ]
        
        public AtWebElement FindElement(LocatorType locatorType, string locatorValue, params string[] replaceHashValues)
        {
            var element = _navigationHandler.FindElement(this, locatorType, locatorValue, replaceHashValues);

            return new RemoteAtWebElement(element)
            {
                Selector = element?.Selector,
                Locator = element?.Locator ?? LocatorType.ClassName,
                LocatorValue = element?.LocatorValue,
                Driver = this
            };
        }

        public AtWebElement FindElementByIndex(LocatorType locatorType, string locatorValue, int index,
            params string[] replaceHashValues)
        {
            var element = _navigationHandler.FindElementByIndex(this, locatorType, locatorValue, index, replaceHashValues);

            return new RemoteAtWebElement(element)
            {
                Selector = element?.Selector,
                Locator = element?.Locator ?? LocatorType.ClassName,
                LocatorValue = element?.LocatorValue,
                Driver = this
            };
        }

        public ReadOnlyCollection<AtWebElement> FindElements(LocatorType locatorType, string locatorValue,
            params string[] replaceHashValues)
        {
            var remoteElements = new List<AtWebElement>();
            var atElements = _navigationHandler.FindElements(this, locatorType, locatorValue, replaceHashValues);

            foreach (var element in atElements)
            {
                remoteElements.Add(new RemoteAtWebElement(element)
                    {
                    Selector = element?.Selector,
                    Locator = element?.Locator ?? LocatorType.ClassName,
                    LocatorValue = element?.LocatorValue,
                    Driver = this
                }
                );
            }

            return remoteElements.AsReadOnly();
        }
        public ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, LocatorType locatorType,
      string locatorValue, params string[] replaceHashValues)
        {
            var remoteElements = new List<AtWebElement>();
            var atElements = _navigationHandler.FindElements(parentElement,this, locatorType, locatorValue, replaceHashValues);

            foreach (var element in atElements)
            {
                remoteElements.Add(new RemoteAtWebElement(element)
                {
                    Selector = element?.Selector,
                    Locator = element?.Locator ?? LocatorType.ClassName,
                    LocatorValue = element?.LocatorValue,
                    Driver = this
                }
                );
            }

            return remoteElements.AsReadOnly();
        }


        public AtWebElement FindElement(AtBy atBy)
        {
            var element = _navigationHandler.FindElement(this, atBy);

            return new ChromeAtWebElement(element)
            {
                Selector = element?.Selector,
                Locator = element?.Locator ?? LocatorType.ClassName,
                LocatorValue = element?.LocatorValue,
                Driver = this
            };
        }

        public AtWebElement FindElementByIndex(AtBy atBy, int index = 0)
        {
            var element = _navigationHandler.FindElementByIndex(this, atBy, index);

            return new ChromeAtWebElement(element)
            {
                Selector = element?.Selector,
                Locator = element?.Locator ?? LocatorType.ClassName,
                LocatorValue = element?.LocatorValue,
                Driver = this
            };
        }

        public ReadOnlyCollection<AtWebElement> FindElements(AtBy atBy)
        {
            var remoteElements = new List<AtWebElement>();
            var atElements = _navigationHandler.FindElements(this, atBy);

            foreach (var element in atElements)
            {
                remoteElements.Add(new ChromeAtWebElement(element)
                {
                    Selector = element?.Selector,
                    Locator = element?.Locator ?? LocatorType.ClassName,
                    LocatorValue = element?.LocatorValue,
                    Driver = this
                }
                );
            }
            return remoteElements.AsReadOnly();
        }

        public ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, AtBy atBy)
        {
            var remoteElements = new List<AtWebElement>();
            var atElements = _navigationHandler.FindElements(parentElement, this, atBy);
            foreach (var element in atElements)
            {
                remoteElements.Add(new ChromeAtWebElement(element)
                {
                    Selector = element?.Selector,
                    Locator = element?.Locator ?? LocatorType.ClassName,
                    LocatorValue = element?.LocatorValue,
                    Driver = this
                }
                );
            }

            return remoteElements.AsReadOnly();
        }
        #endregion

        #region [ Browser ]

        public string OpenNewActiveTab(string url)
        {
            return _navigationHandler.OpenNewActiveTab(this, url);
        }

        public void CloseTab(string tabInstance)
        {
            _navigationHandler.CloseTab(this, tabInstance);
        }

        public string GetActiveTabId()
        {
            return _navigationHandler.GetActiveTabId(this);
        }

        public void GoToTab(string tabInstance)
        {
            _navigationHandler.GoToTab(this, tabInstance);
        }

        public void LogBrowserLogsToConsole()
        {
            _navigationHandler.LogBrowserLogsToConsole(this);
        }

        public void CloseAllTabs()
        {
            _navigationHandler.CloseAllTabs(this);
        }

        public void CloseAdditionalWindows(string mainWindow)
        {
            _navigationHandler.CloseAdditionalWindows(this, mainWindow, TimeSpan.FromMilliseconds(DefaultPollingInterval), DefaultIterations);
        }

        public void GotoUrl(string url)
        {
            _navigationHandler.GotoUrl(this, url);
        }

        public string TitleOfThePage()
        {
            return this.Title.ToString();
        }

        public void NavigateBack()
        {
            _navigationHandler.NavigateBack(this);
        }
        public void NavigateForward()
        {
            _navigationHandler.NavigateForward(this);
        }

        public void Refresh()
        {
            _navigationHandler.Refresh(this);
        }

        public void SwitchToWindow(string title)
        {
            _navigationHandler.SwitchToWindow(this, title, DefaultPageLoadTimeout);
        }

        public void SwitchToWindow(int index)
        {
            _navigationHandler.SwitchToWindow(this, index);
        }

        public void SwitchToFrame(int index)
        {
            _navigationHandler.SwitchToFrame(this, index);
        }

        public bool IsWindowPresent(string title)
        {
            return _navigationHandler.IsWindowPresent(this, title);
        }

        public void SwitchToActiveElement()
        {
            _navigationHandler.SwitchToActiveElement(this);
        }

        public bool IsNewWindowOpened()
        {
            return _navigationHandler.IsNewWindowOpened(this);
        }

        public void FullScreen()
        {
            _navigationHandler.FullScreen(this);
        }

        public void Maximize()
        {
            _navigationHandler.FullScreen(this);
        }

        public void Minimize()
        {
            _navigationHandler.Minimize(this);
        }

        public void SetCookie(string cookieName, string value)
        {
            _navigationHandler.SetCookie(this, cookieName, value);
        }

        public string GetCookieValue(string cookieName)
        {
            return _navigationHandler.GetCookieValue(this, cookieName);
        }

        public void DeleteAllCookies()
        {
            _navigationHandler.DeleteAllCookies(this);
        }

        public void DeleteCookie(string cookieName)
        {
            _navigationHandler.DeleteCookie(this, cookieName);
        }

        public void ClearWebStorage()
        {
        }

        #endregion

        #region [ Actions ]

        public void MoveToElement(By by)
        {
            _navigationHandler.MoveToElement(this, by);
        }

        public void MoveToElement(IAtWebElement element)
        {
            _navigationHandler.MoveToElement(this, element);
        }

        #endregion

        #region[JavaScript]

        public object ExecuteScript(string script, params object[] args)
        {
            return _navigationHandler.ExecuteScript(this, script, args);
        }

        public string GetTextOfWebElement(string xpath)
        {
            return _navigationHandler.GetTextOfWebElement(this, xpath);
        }

        #endregion

        #region[Alert Handling]

        public void AcceptAlert()
        {
            _navigationHandler.AcceptAlert(this);
        }

        public void DismissAlert()
        {
            _navigationHandler.DismissAlert(this);
        }

        public string GetAlertText()
        {
            return _navigationHandler.GetAlertText(this);
        }

        public void SetTextToAlert(string text)
        {
            _navigationHandler.SetTextToAlert(this, text);
        }

        public bool IsAlertPresent()
        {
            return _navigationHandler.IsAlertPresent(this);
        }

        #endregion

        #region [ Wait Methods]
        public void WaitInSec(int sec = 2)
        {
            _navigationHandler.WaitInSec(sec);
        }

        public void WaitForPageLoaded()
        {
            _navigationHandler.WaitForPageLoaded(this);
        }

        public void WaitForDomReady(TimeSpan? pollingInterval, int pollingAttempts = 5)
        {
            _navigationHandler.WaitForDomReady(this, pollingInterval ?? TimeSpan.FromMilliseconds(DefaultPollingInterval), pollingAttempts);
        }

        public void WaitForDomReady(int pollingAttempts = 5)
        {
            _navigationHandler.WaitForDomReady(this, TimeSpan.FromMilliseconds(DefaultPollingInterval), pollingAttempts);
        }

        public void WaitAjax()
        {
            _navigationHandler.WaitAjax(this);
        }

        public void SetImplicitWait(TimeSpan waitTimeSpan)
        {
            _navigationHandler.SetImplicitWait(this, waitTimeSpan);
        }

        public void WaitForElementVisible(AtWebElement atElement)
        {
            _navigationHandler.WaitForElementVisible(this, atElement, DefaultWaitForElement);
        }

        public void WaitForElementVisible(AtWebElement atElement, int secs)
        {
            _navigationHandler.WaitForElementVisible(this, atElement, secs);
        }

        public void WaitForElementVisible(AtWebElement atElement, TimeSpan pollingInterval, int iterations = 5)
        {
            _navigationHandler.WaitForElementVisible(this, atElement, pollingInterval, iterations);
        }

        public void WaitForElementPresent(AtWebElement atElement)
        {
            _navigationHandler.WaitForElementPresent(this, atElement, DefaultWaitForElement);
        }

        public void WaitForElementPresent(AtWebElement atElement, int secs)
        {
            _navigationHandler.WaitForElementPresent(this, atElement, secs);
        }

        public void WaitForElementPresent(AtWebElement atElement, TimeSpan pollingInterval, int iterations = 5)
        {
            _navigationHandler.WaitForElementPresent(this, atElement, pollingInterval, iterations);
        }

        public void WaitForTextPresent(AtWebElement atElement, string text, TimeSpan pollingInterval, int iterations = 5)
        {
            _navigationHandler.WaitForTextPresent(this, atElement, text, pollingInterval, iterations);
        }

        public void WaitForAttributePresent(AtWebElement atElement, string attribute, string attributeValue, TimeSpan pollingInterval, int iterations = 5)
        {
            _navigationHandler.WaitForAttributePresent(this, atElement, attribute, attributeValue, pollingInterval, iterations);
        }
        public void WaitForElementClickable(IAtWebElement element, int secs, string message = "Object is not visible")
        {
            _navigationHandler.WaitForElementClickable(this, element, secs, message);
        }

        public void WaitForElementClickable(IAtWebElement element, string message = "Object is not visible")
        {
            _navigationHandler.WaitForElementClickable(this, element, DefaultWaitForElement, message);
        }

        public void WaitUntilNotVisible(AtWebElement element, string message = "Object is visible")
        {
            _navigationHandler.WaitUntilNotVisible(this, element, DefaultWaitForElement, message);
        }

        public void WaitUntilNotVisible(AtWebElement element, int secs, string message = "Object is visible")
        {
            _navigationHandler.WaitUntilNotVisible(this, element, secs, message);
        }

        public void WaitForAnyVisible(IAtWebElement elementA, IAtWebElement elementB, int secs, string message)
        {
            _navigationHandler.WaitForAnyVisible(this, elementA, elementB, secs, message);
        }

        public void WaitForAnyVisible(IAtWebElement elementA, IAtWebElement elementB, string message)
        {
            _navigationHandler.WaitForAnyVisible(this, elementA, elementB, DefaultWaitForElement, message);
        }

        public void WaitForElementVisible(AtBy atBy, string message)
        {
            _navigationHandler.WaitForElementVisible(this, atBy, DefaultWaitForElement, message);
        }

        public void WaitForElementVisible(AtBy atBy, int secs, string message)
        {
            _navigationHandler.WaitForElementVisible(this, atBy, secs, message);
        }

        public void WaitForElementVisible(AtBy atBy, TimeSpan pollingDuration, int iterations)
        {
            _navigationHandler.WaitForElementVisible(this, atBy, pollingDuration, iterations);
        }

        public void WaitForElementPresent(AtBy atBy)
        {
            _navigationHandler.WaitForElementPresent(this, atBy, DefaultWaitForElement);
        }

        public void WaitForElementPresent(AtBy atBy, int secs)
        {
            _navigationHandler.WaitForElementPresent(this, atBy, secs);
        }

        public void WaitForElementPresent(AtBy atBy, TimeSpan pollingDuration, int iterations)
        {
            _navigationHandler.WaitForElementPresent(this, atBy, pollingDuration, iterations);
        }

        public void WaitForTextPresent(AtBy atBy, string text, TimeSpan pollingDuration, int iterations)
        {
            _navigationHandler.WaitForTextPresent(this, atBy, text, pollingDuration, iterations);
        }

        public void WaitForAttributePresent(AtBy atBy, string attribute, string attributeValue, TimeSpan pollingDuration, int iterations)
        {
            _navigationHandler.WaitForAttributePresent(this, atBy, attribute, attributeValue, pollingDuration, iterations);
        }

        public void WaitForElementClickable(AtBy atBy, int secs, string message = "Element is not clickable")
        {
            _navigationHandler.WaitForElementClickable(this, atBy, secs, message);
        }

        public void WaitUntilNotVisible(AtBy atBy, int secs, string message = "Element is visible")
        {
            _navigationHandler.WaitUntilNotVisible(this, atBy, secs, message);
        }

        public void WaitForAnyVisible(AtBy atByA, AtBy atByB, int secs, string message)
        {
            _navigationHandler.WaitForAnyVisible(this, atByA, atByB, secs, message);
        }
        #endregion

        #region[Frame Handling]
        public void SwitchToFrame(string frame)
        {
            _navigationHandler.SwitchToFrame(this, frame);
        }

        public void SwitchToDefaultContent()
        {
            _navigationHandler.SwitchToDefaultContent(this);
        }

        public void SwitchToFrame(IAtWebElement atElement)
        {
            _navigationHandler.SwitchToFrame(this, atElement);
        }

        public void SwitchToParentFrame()
        {
            _navigationHandler.SwitchToParentFrame(this);
        }
        #endregion

        #region[Scroll Handling]

        public void ScrollDown(int y = 200)
        {
            _navigationHandler.ScrollDown(this, y);
        }

        public void ScrollTo(int x, int y)
        {
            _navigationHandler.ScrollTo(this, x, y);
        }

        public void ScrollToElement(AtWebElement element)
        {
            _navigationHandler.ScrollToElement(this, element);
        }

        public void ScrollToTop()
        {
            _navigationHandler.ScrollToTop(this);
        }

        public void ScrollToBottom()
        {
            _navigationHandler.ScrollToBottom(this);
        }

        public void ScrollElementToCenter(AtWebElement element)
        {
            _navigationHandler.ScrollElementToCenter(this, element);
        }

        #endregion
    }
}