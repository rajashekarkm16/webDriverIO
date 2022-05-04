using System;
using System.Collections.Generic;
using System.Threading;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Exceptions;
using Dnata.Automation.BDDFramework.Handlers.BaseNavigation;
using Dnata.Automation.BDDFramework.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Dnata.Automation.BDDFramework.Handlers
{
    public abstract class BaseClickHandler 
    {
        private readonly IBaseNavigationHandler _navigationHandler;
        private readonly List<Action<AtWebElement, IAtWebDriver>> _clickHandlers;

        public BaseClickHandler(IBaseNavigationHandler navigationHandler)
        {
            _navigationHandler = navigationHandler;
            _clickHandlers = new List<Action<AtWebElement, IAtWebDriver>>
            {
                ExecuteSeleniumClick,
                ExecuteJavascriptClick,
                ExecuteSeleniumActionClick
            };
        }

        public virtual void AtWaitAndClick(IAtWebDriver driver, AtWebElement atElement, Func<bool> condition,
            TimeSpan pollingInterval, int iterations)
        {
            _navigationHandler.WaitForElementPresent(driver, atElement, pollingInterval, iterations);
            AtClick(driver, atElement, condition, TimeSpan.FromMilliseconds(250.0), 10);
        }

        public virtual void AtClick(IAtWebDriver driver, AtWebElement element, int iterations)
        {
            for (var index = 0; index < iterations; ++index)
            {
                try
                {
                    ExecuteSeleniumClick(element, driver);
                    return;
                }
                catch
                {
                    // ignored
                }

                Thread.Sleep(1000);
            }

            throw new AtElementNotClickableException()
            {
                ElementSelector = ReturnBoxedElementValue(element),
                ErrorContext = AtExceptionTypes.Navigation
            };
        }

        public virtual void AtClick(IAtWebDriver driver, AtWebElement element, Func<bool> condition,
            TimeSpan pollingInterval, int iterations)
        {
            try
            {
                for (var index = 0; index < iterations; ++index)
                {
                    foreach (var clickHandler in _clickHandlers)
                    {
                        try
                        {
                            try
                            {
                                clickHandler(element, driver);
                                _navigationHandler.WaitForDomReady(driver, TimeSpan.FromMilliseconds(240.0), 10);
                            }
                            catch
                            {
                                // ignored
                            }

                            if (condition())
                            {
                                break;
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    if (index.Equals(iterations - 1))
                        throw new Exception("AtClick Errored: Condition Never Returned True!");

                    Thread.Sleep(pollingInterval);
                }
            }
            catch (Exception ex)
            {
                throw new AtElementNotClickableException(ex.Message)
                {
                    ElementSelector = ReturnBoxedElementValue(element),
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        private void ExecuteSeleniumActionClick(AtWebElement element, IAtWebDriver driver)
        {
            try
            {
                new Actions(driver).Click(element).Perform();
            }
            catch (StaleElementReferenceException)
            {
                var onElement = ResolveStaleElementReferences(element, driver);
                new Actions(driver).Click(onElement).Perform();
            }
        }

        private void ExecuteSeleniumClick(AtWebElement element, IAtWebDriver driver)
        {
            try
            {
                element.Click();
            }
            catch (StaleElementReferenceException)
            {
                ResolveStaleElementReferences(element, driver).Click();
            }
            catch
            {
                // ignored
            }
        }

        private void ExecuteJavascriptClick(AtWebElement element, IAtWebDriver driver)
        {
            var script =
                "return (function(elements){\n\rvar returnValue = false;\n\rvar elementToClick = elements[0];\n\relementToClick.addEventListener('click', function(){\n\rreturnValue = true;\n\rreturn true;\n\r})\n\relementToClick.click();\n\rreturn returnValue;\n\r})(arguments);\n\r";
            try
            {
                if (!(bool) ((IJavaScriptExecutor) driver).ExecuteScript(script, (object) element))
                    throw new ElementNotVisibleException();
            }
            catch (StaleElementReferenceException)
            {
                var webElement = ResolveStaleElementReferences(element, driver);
                if (!(bool) ((IJavaScriptExecutor) driver).ExecuteScript(script, (object) webElement))
                    throw new ElementNotVisibleException();
            }
        }

        private IWebElement ResolveStaleElementReferences(AtWebElement element, IAtWebDriver driver)
        {
            if (!(element is IAtWebElement atWebElement)) return element;
            try
            {
                var selector = atWebElement.Selector;
                return driver.FindElement(selector);
            }
            catch
            {
                // ignored
            }

            return element;
        }

        private string ReturnBoxedElementValue(AtWebElement element)
        {
            var empty = string.Empty;
            try
            {
                return element is AtWebElement atWebElement ? atWebElement.Selector.ToString() : empty;
            }
            catch
            {
                // ignored
            }

            return empty;
        }
    }
}