using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Exceptions;
using Dnata.Automation.BDDFramework.Helpers;
using Dnata.Automation.BDDFramework.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Dnata.Automation.BDDFramework.Handlers.BaseNavigation
{
    public class  BaseNavigationHandler : IBaseNavigationHandler
    {

        private static readonly Dictionary<string, string> ByMechanisms = new Dictionary<string, string>
                                                                     {
                                                                         {"name", "Name"},
                                                                         {"class", "ClassName"},
                                                                         {"linktext", "LinkText"},
                                                                         {"tag", "TagName"},
                                                                         {"id", "Id"},
                                                                         {"xpath", "XPath"},
                                                                         {"css", "CssSelector"},
                                                                         {"partiallinktext", "PartialLinkText"}
                                                                     };

        public virtual bool IsElementStale(IAtWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        public virtual bool IsElementPresent(IAtWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool IsElementVisible(IAtWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  AtWebElement FindElement(IAtWebDriver driver, LocatorType locatorType, string locatorValue, params string[] replaceHashValues)
        {
            return FindElements(driver, locatorType, locatorValue, replaceHashValues).FirstOrDefault();
        }

        public  AtWebElement FindElementByIndex(IAtWebDriver driver, LocatorType locatorType, string locatorValue, int index, params string[] replaceHashValues)
        {
            return FindElements(driver, locatorType, locatorValue, replaceHashValues)[--index];
        }

        private string GetLocatorWithReplacementValues(LocatorType locatorType,
            string locatorValue, params string[] replaceHashValues)
        {
            if (replaceHashValues.Length == 1)
            {
                locatorValue = locatorValue.Replace("#", replaceHashValues[0]);
            }
            else if (replaceHashValues.Length > 1)
            {
                var splitLocator = locatorValue.Split('#');
                locatorValue = string.Empty;

                if (replaceHashValues.Length != splitLocator.Length - 1)
                    throw new IndexOutOfRangeException(
                        "Hashvalues count and hashes count in the locator did not match");

                for (var index = 0; index < splitLocator.Length; index++)
                {
                    locatorValue += splitLocator[index];

                    if (index < replaceHashValues.Length)
                    {
                        locatorValue = locatorValue + replaceHashValues[index];
                    }
                }
            }
            return locatorValue;
        }

        public By GetBy(LocatorType locatorType,
            string locatorValue)
        {
            MethodInfo byMethod;
            byMethod = typeof(By).GetMethod(ByMechanisms.ContainsKey(locatorType.ToString())
              ? ByMechanisms[locatorType.ToString()]
              : locatorType.ToString());
            return (By)byMethod?.Invoke(null, new object[1] { locatorValue });
        }

        public ReadOnlyCollection<AtWebElement> FindElements(IAtWebDriver driver, LocatorType locatorType,
            string locatorValue, params string[] replaceHashValues)
        {
            IEnumerable<IWebElement> childElements = null;

            locatorValue = GetLocatorWithReplacementValues(locatorType,
               locatorValue,replaceHashValues);

            By by = GetBy(locatorType, locatorValue);

            try
            {                
                childElements = driver.FindElements(by);
            }
            catch (StaleElementReferenceException)
            {
                /*Ignore exception*/
            }
            catch (NoSuchElementException)
            {

            }
            var elements = new List<AtWebElement>();
            if (!childElements.Any()) return new ReadOnlyCollection<AtWebElement>(elements);

            foreach (var element in childElements)
            {
                elements.Add(new AtWebElement(element)
                {
                    Selector = by,
                    Locator = locatorType,
                    LocatorValue = locatorValue,
                    Driver = driver
                }
                );
            }

            return new ReadOnlyCollection<AtWebElement>(elements);
        }

        public ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, IAtWebDriver driver, LocatorType locatorType,
         string locatorValue, params string[] replaceHashValues)
        {
            IEnumerable<IWebElement> childElements = null;

            locatorValue = GetLocatorWithReplacementValues(locatorType,
               locatorValue, replaceHashValues);

            By by = GetBy(locatorType, locatorValue);


            try
            {

                childElements = parentElement.FindElements(by);
            }
            catch (StaleElementReferenceException)
            {
                /*Ignore exception*/
            }
            catch (NoSuchElementException)
            {

            }

            var elements = new List<AtWebElement>();
            if (!childElements.Any()) return new ReadOnlyCollection<AtWebElement>(elements);

            foreach (var element in childElements)
            {
                elements.Add(new AtWebElement(element)
                {
                    Selector = by,
                    Locator = locatorType,
                    LocatorValue = locatorValue,
                    Driver = driver
                }
                );
            }

            return new ReadOnlyCollection<AtWebElement>(elements);
        }

        public AtWebElement FindElement(IAtWebDriver driver, AtBy atBy)
        {
            return FindElements(driver, atBy).FirstOrDefault();
        }

        public AtWebElement FindElementByIndex(IAtWebDriver driver, AtBy atBy, int index = 0)
        {
            return FindElements(driver, atBy)[--index];
        }


        public ReadOnlyCollection<AtWebElement> FindElements(IAtWebDriver driver, AtBy atBy)
        {
            IEnumerable<IWebElement> childElements = null;

            try
            {

                childElements = driver.FindElements(atBy.by);
            }
            catch (StaleElementReferenceException)
            {
                /*Ignore exception*/
            }
            catch (NoSuchElementException)
            {

            }

            var elements = new List<AtWebElement>();
            if (!childElements.Any()) return new ReadOnlyCollection<AtWebElement>(elements);

            foreach (var element in childElements)
            {
                elements.Add(new AtWebElement(element)
                {
                    Selector = atBy.by,
                    Locator = atBy.locatorType,
                    LocatorValue = atBy.locatorValue,
                    Driver = driver
                }
                );
            }

            return new ReadOnlyCollection<AtWebElement>(elements);
        }

        public ReadOnlyCollection<AtWebElement> FindElements(IWebElement parentElement, IAtWebDriver driver, AtBy atBy)
        {
            IEnumerable<IWebElement> childElements = null;

            try
            {

                childElements = parentElement.FindElements(atBy.by);
            }
            catch (StaleElementReferenceException)
            {
                /*Ignore exception*/
            }
            catch (NoSuchElementException)
            {

            }

            var elements = new List<AtWebElement>();
            if (!childElements.Any()) return new ReadOnlyCollection<AtWebElement>(elements);

            foreach (var element in childElements)
            {
                elements.Add(new AtWebElement(element)
                {
                    Selector = atBy.by,
                    Locator = atBy.locatorType,
                    LocatorValue = atBy.locatorValue,
                    Driver = driver
                }
                );
            }

            return new ReadOnlyCollection<AtWebElement>(elements);
        }

        #region [ Browser ]
        public virtual string OpenNewActiveTab(IAtWebDriver driver, string url)
        {
            var windowName = string.Empty;
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.open();", Array.Empty<object>());
                windowName = driver.WindowHandles[driver.WindowHandles.Count - 1];
                driver.SwitchTo().Window(windowName);
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception)
            {
                throw new AtGenericException($"Unable to open a new tab and return the Tab ID: {windowName}");
            }
            return windowName;
        }

        public virtual void CloseTab(IAtWebDriver driver, string tabInstance)
        {
            try
            {
                var javaScriptExecutor = (IJavaScriptExecutor)driver;
                driver.SwitchTo().Window(tabInstance);
                var script = "window.close();";
                var objArray = Array.Empty<object>();
                javaScriptExecutor.ExecuteScript(script, objArray);
            }
            catch
            {
                throw new AtGenericException($"Unable to close the defined Tab ID: {tabInstance}");
            }
        }

        public virtual string GetActiveTabId(IAtWebDriver driver)
        {
            try
            {
                var currentWindowHandle = driver.CurrentWindowHandle;
                if (string.IsNullOrEmpty(currentWindowHandle))
                    throw new Exception();
                return currentWindowHandle;
            }
            catch
            {
                throw new AtGenericException("Unable to retreive the current driver Tab Id");
            }
        }

        public virtual void GoToTab(IAtWebDriver driver, string tabInstance)
        {
            try
            {
                driver.SwitchTo().Window(tabInstance);
            }
            catch
            {
                throw new AtGenericException($"Unable to Go To the defined Tab ID: {tabInstance}");
            }
        }

        public void LogBrowserLogsToConsole(IAtWebDriver driver)
        {
            try
            {
                var logs = driver.Manage().Logs.GetLog(LogType.Browser).ToList();
                foreach (var log in logs)
                {
                    CommonFunctions.LogToConsole(log.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Taking log from console for IE browser are currently unavailable: {0}", ex);
            }
        }

        public void CloseAllTabs(IAtWebDriver driver)
        {
            var tabs = driver.WindowHandles;

            foreach (var tab in tabs)
            {
                try
                {
                    driver.SwitchTo().Window(tab);
                    driver.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            driver.Quit();
        }

        public void GotoUrl(IAtWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
            }

            catch (Exception e)
            {
                throw new TimeoutException("Web page is not loaded. " + e.Message);
            }
        }

        public void NavigateBack(IAtWebDriver driver)
        {
            driver.Navigate().Back();
        }

        public void NavigateForward(IAtWebDriver driver)
        {
            driver.Navigate().Forward();
        }

        public void Refresh(IAtWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        public void SwitchToWindow(IAtWebDriver driver, string title, int waitingTime)
        {
            var switched = false;
            //Wait for all the windows to be loaded
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => driver.WindowHandles.Count >= 1);
            var elapsedTime = 0;

            while (!switched && elapsedTime <= waitingTime)
            {
                foreach (var handle in driver.WindowHandles)
                {
                    driver.SwitchTo().Window(handle);
                    if (string.Equals(driver.Title, title, StringComparison.OrdinalIgnoreCase))
                    {
                        switched = true;
                        break;
                    }
                }
                WaitInSec(1);
                elapsedTime += 1;
            }

            if (!switched)
            {
                throw new NoSuchWindowException("Window with title " + title + " not found");
            }
        }

        public void CloseAdditionalWindows(IAtWebDriver driver, string mainWindow, TimeSpan pollingInterval, int iterations)
        {
            WaitForDomReady(driver, pollingInterval, iterations);

            foreach (var handle in driver.WindowHandles)
            {
                if (handle == mainWindow) continue;
                driver.SwitchTo().Window(handle);
                driver.Close();
            }

            driver.SwitchTo().Window(mainWindow);
        }

        public bool IsNewWindowOpened(IAtWebDriver driver, int currentWindowCount = 1)
        {
            if (driver.WindowHandles.Count > currentWindowCount)
                return true;
            else
                return false;
        }

        public void SwitchToWindow(IAtWebDriver driver, int index)
        {
            //Wait for all the windows to be loaded
            WaitForDomReady(driver, TimeSpan.FromSeconds(30), 2);

            try
            { 
                driver.SwitchTo().Window(driver.WindowHandles[index]);
            }

            catch (Exception ex)
            {
                throw new NoSuchWindowException(" Window with index " + index + " not found. " + ex);
            }
        }

        public bool IsWindowPresent(IAtWebDriver driver, string title)
        {
            var isPresent = false;


            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (!string.Equals(driver.Title, title, StringComparison.OrdinalIgnoreCase)) continue;
                isPresent = true;
                break;
            }

            return isPresent;
        }

        public void SwitchToActiveElement(IAtWebDriver driver)
        {
            try
            {
                driver.SwitchTo().ActiveElement();
            }
            catch(NoSuchElementException ex)
            {
               throw new NoSuchElementException("Element not found" + ex.Message);
            }
        }

        public void FullScreen(IAtWebDriver driver)
        {
            driver.Manage().Window.FullScreen();
        }
        
        public void Maximize(IAtWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        public void Minimize(IAtWebDriver driver)
        {
            driver.Manage().Window.Minimize();
        }

        public void SetCookie(IAtWebDriver driver, string cookieName, string value)
        {
            try
            {
                Cookie cookie = new Cookie(cookieName, value);
                driver.Manage().Cookies.AddCookie(cookie);
            }
            catch
            {
                throw new UnableToSetCookieException("Unable to set cookie, " + cookieName);
            }
        }

        public string GetCookieValue(IAtWebDriver driver, string cookieName)
        {
            string cookieValue = "";
            try
            {
                cookieValue = driver.Manage().Cookies.GetCookieNamed(cookieName).Value;
            }
            catch
            {
                throw new Exception("Unable to get the cookie value");
            }

            return cookieValue;
        }

        public void DeleteAllCookies(IAtWebDriver driver)
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        public void DeleteCookie(IAtWebDriver driver, string cookieName)
        {
            driver.Manage().Cookies.DeleteCookieNamed(cookieName);
        }

        public void ClearWebStorage(IAtWebDriver driver)
        {
            (driver as RemoteWebDriver)?.WebStorage.LocalStorage.Clear();
            (driver as RemoteWebDriver)?.WebStorage.SessionStorage.Clear();
        }

        #endregion

        #region [ Javascript ]

        /// <summary>
        /// Execute Java script in the browser
        /// </summary>
        /// <param name="driver"> Webdriver</param>
        /// /// <param name="script">Script</param>
        /// <param name="args">Arguments</param>
        /// <returns>Result of the script if any</returns>
        /// 
        public object ExecuteScript(IAtWebDriver driver, string script, params object[] args)
        {
            object returnValue = ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
            Thread.Sleep(1000);
            return returnValue;
        }

        public string GetTextOfWebElement(IAtWebDriver driver, string xpath)
        {
            string jsx = "return  document.evaluate(\"" + xpath + "/text()\", document, null, XPathResult.STRING_TYPE, null).stringValue;";

            object returnValue = ExecuteScript(driver, jsx);
            return returnValue.ToString();
        }
       
        #endregion 

        #region [ Alerts Handling ]

        /// <summary>
        /// Method to accept Alert
        /// </summary>
        public void AcceptAlert(IAtWebDriver driver)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch
            {
                throw new NoAlertPresentException("Alert is not present");
            }
        }

        /// <summary>
        /// Method to dismiss alert
        /// </summary>
        public void DismissAlert(IAtWebDriver driver)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Dismiss();
            }
            catch
            {
                throw new NoAlertPresentException("Alert is not present");
            }
        }

        /// <summary>
        /// Method to retrieve the Alert text
        /// </summary>
        /// <returns></returns>
        public string GetAlertText(IAtWebDriver driver)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                return alert.Text;
            }
            catch
            {
                throw new NoAlertPresentException("Alert is not present");
            }
        }

        /// <summary>
        /// Method to set text to alert
        /// </summary>
        /// <param name="text"></param>
        public void SetTextToAlert(IAtWebDriver driver, string text)
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.SendKeys(text);
            }
            catch
            {
                throw new NoAlertPresentException("Alert is not present");
            }
        }

        /// <summary>
        /// Method to check if alert is present
        /// </summary>
        /// <returns></returns>
        public bool IsAlertPresent(IAtWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region [ Actions ]

        public void MoveToElement(IAtWebDriver driver, By by)
        {
            try
            {
                // this makes sure the element is visible before you try to do anything
                // for slow loading pages
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var element = wait.Until(ExpectedConditions.ElementIsVisible(by));

                var action = new Actions(driver);
                action.MoveToElement(element).Perform();
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException("Unable to move to element " + ex.Message);
            }
        }

        public void MoveToElement(IAtWebDriver driver, IAtWebElement element)
        {
            var action = new Actions(driver);

            action.MoveToElement(element.WrappedElement).Perform();
        }

        #endregion

        #region [ Frames Handling ]

        public void SwitchToFrame(IAtWebDriver driver, int index)
        {
            try
            {
                driver.WaitForDomReady();
                driver.SwitchTo().Frame(index);
            }
            catch
            {
                throw new NoSuchFrameException("Frame " + index + " is not present");
            }
        }

        public void SwitchToFrame(IAtWebDriver driver, string frame)
        {
            try
            {
                driver.SwitchTo().Frame(frame);
            }
            catch
            {
                throw new NoSuchFrameException("Frame " + frame + " is not present");
            }
        }

        public void SwitchToFrame(IAtWebDriver driver, IAtWebElement atElement)
        {
            // Wait for all the windows to be loaded
            WaitForDomReady(driver, TimeSpan.FromMilliseconds(30), 5);

            try
            {
                IWebElement element = driver.FindElement(atElement.Selector);
                driver.SwitchTo().Frame(element);
            }
            catch(Exception ex)
            {
                throw new NoSuchFrameException("Frame  is not present" + ex.Message);
            }
        }

        public void SwitchToDefaultContent(IAtWebDriver driver)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
            }
            catch
            {
                throw new NoSuchFrameException("Unable to switch to default context");
            }
        }


        public void SwitchToParentFrame(IAtWebDriver driver)
        {
            try
            {
                driver.SwitchTo().ParentFrame();
            }
            catch
            {
                throw new NoSuchFrameException("Unable to switch to parent frame");
            }
        }
        #endregion

        #region [ Wait Methods ]

        public void WaitInSec(int sec = 2)
        {
            Thread.Sleep(sec * 1000);
        }

        public void WaitForPageLoaded(IAtWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => ((IJavaScriptExecutor)d)
                .ExecuteAsyncScript("var callback = arguments[arguments.length - 1];" +
                "if (document.readyState !== 'complete') {" +
                "  callback('document not ready');" +
                "} else {" +
                "  try {" +
                "    var testabilities = window.getAllAngularTestabilities();" +
                "    var count = testabilities.length;" +
                "    var decrement = function() {" +
                "      count--;" +
                "      if (count === 0) {" +
                "        callback('complete');" +
                "      }" +
                "    };" +
                "    testabilities.forEach(function(testability) {" +
                "      if (testability._ngZone.isStable) decrement();" +
                "    });" +
                "  } catch (err) {" +
                "    callback(err.message);" +
                "  }" +
                "}"
                ).ToString().Equals("complete"));
        }

        public virtual void WaitForDomReady(IAtWebDriver driver, TimeSpan pollingDuration, int iterations)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            if (!Equals(javaScriptExecutor, null))
            {
                for (var index = 0; index < iterations; ++index)
                {
                    try
                    {
                        if (javaScriptExecutor.ExecuteScript("return document.readyState", Array.Empty<object>()).ToString().Equals("complete"))
                            return;
                    }
                    catch
                    {
                    }

                    Thread.Sleep(pollingDuration);
                }
                throw new AtElementNotPresentException()
                {
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitAjax(IAtWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => (bool)((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active == 0"));
        }
        /// <summary>
        /// This method set the Driver Implicit Wait
        /// </summary>
        /// <param name="driver">Set the current Driver</param>
        /// <param name="waitTimeSpan">Set new Implicit Wait value</param>
        public void SetImplicitWait(IAtWebDriver driver, TimeSpan waitTimeSpan)
        {
            driver.Manage().Timeouts().ImplicitWait = waitTimeSpan;
        }

        public void WaitForElementVisible(IAtWebDriver driver, AtBy atBy, int secs, string message)
        {
            driver.WaitForDomReady();
            try
            {
                var driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secs));
                driverWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(AtElementNotPresentException), typeof(NoSuchElementException));
                driverWait.Until(ExpectedConditions.ElementIsVisible(atBy.by));
            }
            catch (Exception)
            {
                throw new AtElementNotPresentException(message)
                {
                    ElementSelector = atBy.by.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }
      
        public void WaitForElementVisible(IAtWebDriver driver, AtWebElement atElement, int secs)
        {
            driver.WaitForDomReady();
            try
            {
                var driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secs));
                driverWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(AtElementNotPresentException), typeof(NoSuchElementException));
                driverWait.Until(ExpectedConditions.ElementIsVisible(atElement.Selector));
            }
            catch (Exception)
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atElement.Selector.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitForElementVisible(IAtWebDriver driver, AtWebElement atElement, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                if (atElement.Visible) return;

                for (var index = 0; index < iterations; ++index)
                {
                    try
                    {
                        if (IsElementPresent(driver, atElement.Selector))
                            return;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    Thread.Sleep(pollingInterval);
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atElement.Selector.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitForElementVisible(IAtWebDriver driver, AtBy atBy, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                for (var index = 0; index < iterations; ++index)
                {
                    try
                    {
                        if (IsElementPresent(driver, atBy.by))
                            return;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    Thread.Sleep(pollingInterval);
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atBy.by.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitForElementPresent(IAtWebDriver driver, AtWebElement atElement, int secs)
        {
            try
            {
                var driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secs));
                driverWait.Until(ExpectedConditions.ElementExists(atElement.Selector));
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atElement.Selector.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitForElementPresent(IAtWebDriver driver, AtBy atBy, int secs)
        {
            try
            {
                var driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secs));
                driverWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(AtElementNotPresentException), typeof(NoSuchElementException));
                driverWait.Until(ExpectedConditions.ElementExists(atBy.by));
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atBy.by.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitForElementPresent(IAtWebDriver driver, AtWebElement atElement, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                if (atElement.Exists) return;

                for (var index = 0; index < iterations; ++index)
                {
                    try
                    {
                        if (IsElementPresent(driver, atElement.Selector))
                            return;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    Thread.Sleep(pollingInterval);
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atElement.Selector.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public void WaitForElementPresent(IAtWebDriver driver, AtBy atBy, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                
                for (var index = 0; index < iterations; ++index)
                {
                    try
                    {
                        if (IsElementPresent(driver, atBy.by))
                            return;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    Thread.Sleep(pollingInterval);
                }
                throw new Exception();
            }
            catch (Exception)
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = atBy.by.ToString(),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public virtual void WaitForTextPresent(IAtWebDriver driver, AtWebElement atElement, string text,
            TimeSpan pollingInterval, int iterations)
        {
            try
            {
                for (var index = 0;
                    index < iterations && !driver.FindElement(atElement.Selector).Text.Contains(text);
                    ++index)
                {
                    if (index.Equals(iterations))
                        throw new Exception(
                            $"Operation timed out after {TimeSpan.FromSeconds(iterations * pollingInterval.TotalMilliseconds)}.");

                    Thread.Sleep(pollingInterval);
                }
            }
            catch (Exception ex)
            {
                var presentException = new AtTextNotPresentException(ex.Message, ex)
                {
                    ErrorContext = AtExceptionTypes.Navigation,
                    ElementSelector = atElement.Selector.ToString(),
                    SearchText = text
                };

                throw presentException;
            }
        }

        public void WaitForTextPresent(IAtWebDriver driver, AtBy atBy, string text, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                for (var index = 0;
                    index < iterations && !driver.FindElement(atBy.by).Text.Contains(text);
                    ++index)
                {
                    if (index.Equals(iterations))
                        throw new Exception(
                            $"Operation timed out after {TimeSpan.FromSeconds(iterations * pollingInterval.TotalMilliseconds)}.");

                    Thread.Sleep(pollingInterval);
                }
            }
            catch (Exception ex)
            {
                var presentException = new AtTextNotPresentException(ex.Message, ex)
                {
                    ErrorContext = AtExceptionTypes.Navigation,
                    ElementSelector = atBy.by.ToString(),
                    SearchText = text
                };

                throw presentException;
            }
        }

        public virtual void WaitForAttributePresent(IAtWebDriver driver, AtWebElement atElement, string attribute, string attributeValue, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                for (var index = 0; index < iterations; ++index)
                {
                    var attributeActualValue = atElement.GetAttribute(attribute);
                    if (!string.IsNullOrEmpty(attributeActualValue) && attributeActualValue.Contains(attributeValue))
                        break;
                    Thread.Sleep(pollingInterval);
                }
            }
            catch
            {
                throw new AtGenericException("Value with attribute not found.");
            }
        }

        public void WaitForAttributePresent(IAtWebDriver driver, AtBy atBy, string attribute, string attributeValue, TimeSpan pollingInterval, int iterations)
        {
            try
            {
                for (var index = 0; index < iterations; ++index)
                {
                    var attributeActualValue = FindElement(driver,atBy).GetAttribute(attribute);
                    if (!string.IsNullOrEmpty(attributeActualValue) && attributeActualValue.Contains(attributeValue))
                        break;
                    Thread.Sleep(pollingInterval);
                }
            }
            catch
            {
                throw new AtGenericException("Value with attribute not found.");
            }
        }

        public void WaitForElementClickable(IAtWebDriver driver, IAtWebElement element, int secs, string message = "Object is not visible")
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, 0, secs);
                WebDriverWait myWait = new WebDriverWait(driver, ts);
                myWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException), typeof(WebDriverException));
                myWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element.Selector));
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }

        }

        public void WaitForElementClickable(IAtWebDriver driver, AtBy atBy, int secs, string message = "Element is not clickable")
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, 0, secs);
                WebDriverWait myWait = new WebDriverWait(driver, ts);
                myWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException), typeof(WebDriverException));
                myWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(atBy.by));
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }
        }

        public void WaitUntilNotVisible(IAtWebDriver driver, AtWebElement element, int secs, string message = "Object is visible")
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, 0, secs);
                WebDriverWait myWait = new WebDriverWait(driver, ts);

                myWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(element.Selector));
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }
        }

        public void WaitUntilNotVisible(IAtWebDriver driver, AtBy atBy, int secs, string message = "Element is not visible")
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, 0, secs);
                WebDriverWait myWait = new WebDriverWait(driver, ts);

                myWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(atBy.by));
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }
        }

        public void WaitUntilNotExists(IAtWebDriver driver, AtWebElement element, int secs, string message = "Object exists")
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, 0, secs);
                WebDriverWait myWait = new WebDriverWait(driver, ts);

                myWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(element.Selector));
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }
        }

        public void WaitUntilNotExists(IAtWebDriver driver, AtBy atBy, int secs, string message = "Element is not visible")
        {
            try
            {
                TimeSpan ts = new TimeSpan(0, 0, secs);
                WebDriverWait myWait = new WebDriverWait(driver, ts);

                myWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(atBy.by));
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }
        }

        public void WaitForAnyVisible(IAtWebDriver driver, IAtWebElement elementA, IAtWebElement elementB, int secs, string message)
        {
            bool isAnyVisible = false;
            int count = 0;

            while (count <= secs)
            {
                if (IsElementVisible(driver, elementA.Selector) || IsElementVisible(driver, elementB.Selector))
                {
                    isAnyVisible = true;
                    break;
                }
                count += 2;
            }
            if (!isAnyVisible)
            {
                string msg = "\r\n Element " + elementA.Selector + " and " + elementB.Selector + " are not visible \r\n";
                throw new Exception(message + msg);
            }
        }


        public void WaitForAnyVisible(IAtWebDriver driver, AtBy atByA, AtBy atByB, int secs, string message)
        {
            bool isAnyVisible = false;
            int count = 0;

            while (count <= secs)
            {
                if (IsElementVisible(driver, atByA.by) || IsElementVisible(driver, atByB.by))
                {
                    isAnyVisible = true;
                    break;
                }
                count += 2;
            }
            if (!isAnyVisible)
            {
                string msg = "\r\n Element " + atByA.by + " and " + atByB.by + " are not visible \r\n";
                throw new Exception(message + msg);
            }
        }

        #endregion

        #region [Handling Scroll]

        public void ScrollDown(IAtWebDriver driver, int y = 200)
        {
            IJavaScriptExecutor jsx = (IJavaScriptExecutor)driver;
            jsx.ExecuteScript("window.scrollBy(0," + y + ")");
        }

        public void ScrollTo(IAtWebDriver driver, int x, int y)
        {
            IJavaScriptExecutor jsx = (IJavaScriptExecutor)driver;
            jsx.ExecuteScript("window.scrollBy(" + x + "," + y + ")");
        }

        public void ScrollToElement(IAtWebDriver driver, AtWebElement element)
        {
            IJavaScriptExecutor jsx = (IJavaScriptExecutor)driver;
            jsx.ExecuteScript("arguments[0].scrollIntoView(true)", element.WrappedElement);
        }

        /// <summary>
        /// Method to scroll to the top of the page
        /// </summary>
        public void ScrollToTop(IAtWebDriver driver)
        {
            IJavaScriptExecutor jsx = (IJavaScriptExecutor)driver;
            jsx.ExecuteScript("window.scrollTo(0, 0)");
        }

        /// <summary>
        /// Method to scroll to the bottom of the page
        /// </summary>
        public void ScrollToBottom(IAtWebDriver driver)
        {
            IJavaScriptExecutor jsx = (IJavaScriptExecutor)driver;
            jsx.ExecuteScript("window.scrollBy(0, document.body.clientHeight)");
        }

        /// <summary>
        /// Method to scroll element to the center of the page
        /// </summary>
        public void ScrollElementToCenter(IAtWebDriver driver, AtWebElement element)
        {
            var elementPosition = element.Location.Y;
            var windowHieght = driver.Manage().Window.Size.Height;
            var newPosition = elementPosition - (windowHieght / 2);
            IJavaScriptExecutor jsx = (IJavaScriptExecutor)driver;
            jsx.ExecuteScript("window.scrollTo(0, " + newPosition + ");");
        }


        #endregion
    }
}