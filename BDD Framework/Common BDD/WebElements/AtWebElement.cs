using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Dnata.Automation.BDDFramework.DriverDecorators;
using System.Threading;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.UI;

namespace Dnata.Automation.BDDFramework.WebElements
{
    public class AtWebElement : IAtWebElement
    {
        private readonly IWebElement _element;

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

        public string TagName => _element.TagName;

        public string Text => _element.Text;

        public string InnerText => _element.GetAttribute("innerText");

        public string Value => _element.GetAttribute("value");
        
        public bool Enabled => _element == null ? false : _element.Enabled && !IsDisabled();

        private bool IsDisabled()
        {
            var classes = _element.GetAttribute("class").Split();
            return classes.Any(x => x == "disabled");
        }

        public bool Selected => _element == null ? false : _element.Selected;

        public Point Location => _element.Location;

        public Size Size => _element.Size;

        public bool Displayed => _element == null ? false : _element.Displayed;

        public bool Visible =>  _element == null ? false : _element.Displayed && _element.Enabled;

        public bool Exists => _element == null ? false : (_element != null);

        public Point LocationOnScreenOnceScrolledIntoView => _element.Location;

        public ICoordinates Coordinates { get; }

        public IWebElement WrappedElement => _element;

        public By Selector { get; set; }

        public LocatorType Locator { get; set; }

        public string LocatorValue { get; set; }

        public IAtWebDriver Driver { get; set; }

        public AtWebElement(ICoordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public AtWebElement(IWebElement element = null)
        {
            _element = element;
        }


        public AtWebElement FindElement(AtWebElement element)
        {
            return Driver.FindElements(_element, element.Locator, element.LocatorValue)[0];
        }

        public ReadOnlyCollection<AtWebElement> FindElements(AtWebElement element)
        {
            return Driver.FindElements(_element, element.Locator, element.LocatorValue);
        }

        public AtWebElement FindElement(LocatorType locatorType, string locatorValue, params string[] param)
        {
            return Driver.FindElements(_element, locatorType, locatorValue, param)[0];
        }

        public AtWebElement FindElementByIndex(LocatorType locatorType, string locatorValue, int index, params string[] param)
        {
            return Driver.FindElements(_element, locatorType, locatorValue)[index-1];
        }

        public ReadOnlyCollection<AtWebElement> FindElements(LocatorType locatorType, string locatorValue, params string[] param)
        {
            return Driver.FindElements(_element, locatorType, locatorValue,param);
        }

        public AtWebElement FindElement(AtBy atBy)
        {
           return Driver.FindElements(_element, atBy)[0];
        }

        public AtWebElement FindElementByIndex(AtBy atBy, int index)
        {
           return Driver.FindElements(_element, atBy)[index];
        }

        public ReadOnlyCollection<AtWebElement> FindElements(AtBy atBy)
        {
           return Driver.FindElements(_element, atBy);
        }


        public IWebElement FindElement(By by)
        {
            return _element.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var elements = _element.FindElements(by);
            var webElementList = new List<IWebElement>();
            if (by.ToString().Contains("ClassName"))
            {
                for (var index = 0; index < elements.Count; ++index)
                {
                    webElementList.Add(new AtWebElement(elements[index])
                    {
                        Selector = By.ClassName($"{by.ToString().Replace("By.ClassName[Contains]: ", "")}[{index}]"),
                        Driver = Driver
                    });
                }
            }
            else if (by.ToString().Contains("CssSelector"))
            {
                for (var index = 0; index < elements.Count; ++index)
                {
                    webElementList.Add(new AtWebElement(elements[index])
                    {
                        Selector = By.CssSelector(
                            $"{by.ToString().Replace("By.CssSelector: ", "")}:nth-child({index + 1})"),
                        Driver = Driver
                    });
                }
            }
            else if (by.ToString().Contains("XPath"))
            {
                for (var index = 0; index < elements.Count; ++index)
                {
                    webElementList.Add(new AtWebElement(elements[index])
                    {
                        Selector = By.XPath($"{by.ToString().Replace("By.XPath: ", "")}[{index + 1}]"),
                        Driver = Driver
                    });
                }
            }
            else
            {
                webElementList.AddRange(elements.Select(element => new AtWebElement(element)
                {
                    Selector = by,
                    Driver = Driver
                }));
            }

            return new ReadOnlyCollection<IWebElement>(webElementList);
        }

        public void Clear()
        {
            try
            {
                Driver.WaitForElementVisible(this);
                _element.Clear();
            }
            catch (StaleElementReferenceException)
            {
                if (!(_element is IAtWebElement element))
                    return;
                Driver.FindElement(element.Locator, element.LocatorValue).Clear();
            }
        }

        public void SendKeys(string text)
        {
            try
            {
                Driver.WaitForElementVisible(this);
                _element.SendKeys(text);
            }
            catch (StaleElementReferenceException)
            {
                if (!(_element is IAtWebElement element))
                    return;
                Driver.FindElement(element.Locator, element.LocatorValue).SendKeys(text);
            }
        }

        public void LazySendKeys(string text, int timeout = 500)
        {

            Driver.WaitForElementVisible(this);
            foreach (var letter in text)
            {
                _element.SendKeys(letter.ToString());
                Thread.Sleep(timeout);
            }
        }

        public void SendKeysUsingActions(string text)
        {
            try
            {
                Actions actions = new Actions(Driver);
                actions.MoveToElement(_element).SendKeys(_element, text).Build().Perform();
            }
            catch (Exception e)
            {
                throw new Exception("Element is not editable. " + e.Message);
            }
        }

        public void Submit()
        {
            try
            {
                Driver.WaitForElementVisible(this);
                _element.Submit();
            }
            catch (StaleElementReferenceException)
            {
                if (!(_element is IAtWebElement element))
                    return;
                Driver.FindElement(element.Locator, element.LocatorValue).Submit();
            }
        }

        public void Click()
        {
            try
            {
                Driver.WaitForDomReady();
                Driver.WaitForElementVisible(this);
                _element.Click();
            }
            catch (StaleElementReferenceException)
            {
                if (!(_element is IAtWebElement element))
                    return;
                Driver.FindElement(element.Locator, element.LocatorValue).Click();
            }
            catch (Exception e)
            {
                throw new Exception("Element is not clickable. "+e.Message);
            }
        }

        public void ClickUsingActions()
        {
            try
            {
                Actions actions = new Actions(Driver);
                actions.MoveToElement(_element).Click(_element);
                actions.Build().Perform();
            }
            catch(Exception e)
            {
                throw new Exception("Element is not clickable. " + e.Message);
            }
        }

        public string GetAttribute(string attributeName)
        {
            return _element.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return _element.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return _element.GetProperty(propertyName);
        }

        public virtual void EnterText(string text)
        {
            Driver.WaitForDomReady();
            Click();
            Clear();
            SendKeys(text);
            Driver.WaitForDomReady();
        }

        public void Check()
        {
            if (!_element.Selected)
            {
                _element.Click();
            }
        }

        public void UnCheck()
        {
            if (_element.Selected)
            {
                _element.Click();
            }
        }

        public virtual string GetText(TimeSpan? pollingInterval = null, TimeSpan? waitingTime = null)
        {
            Driver.WaitForDomReady();
            Driver.WaitForElementVisible(this);
            var effectiveInterval = pollingInterval ?? TimeSpan.FromMilliseconds(500);
            var effectiveWaitingTime = waitingTime ?? TimeSpan.FromSeconds(3);
            var elapsedTime = TimeSpan.Zero;

            while (string.IsNullOrWhiteSpace(Text) && elapsedTime <= effectiveWaitingTime)
            {
                Thread.Sleep(effectiveInterval);
                elapsedTime += effectiveInterval;
            }

            return Text;
        }
        /// <summary>
        /// Returns textNode content only excluding all child elements
        /// </summary>
        /// <returns>Element's text</returns>
        public string GetTextNode()
        {
            var text = Text.Trim();
            var children = FindElements(By.XPath("./*"));
            foreach (var child in children)
            {
                if (!string.IsNullOrWhiteSpace(child.Text))
                {
                    text = text.Replace(child.Text, "").Trim();
                }
            }

            return text;
        }

        public virtual void DragAndDropElement(IWebElement targetElement)
        {
            Driver.WaitForPageLoaded();
            var action = new Actions(Driver);
            action.DragAndDrop(_element, targetElement).Build().Perform();
            Driver.WaitForPageLoaded();
        }

        public  void DragSliderByOffset(int xCoordinate, int yCoordinate)
        {
            Actions slide = new Actions(Driver);
            slide.DragAndDropToOffset(_element, xCoordinate, yCoordinate).Perform();
        }


        public virtual void SelectDropdownOptionByValue(string optionValue)
        {
            Driver.WaitForElementVisible(this);
            try
            {
                new SelectElement(this).SelectByText(optionValue);
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = TagName,
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public virtual void SelectDropdownOptionByIndex(int index)
        {
            Driver.WaitForElementVisible(this);
            try
            {
                new SelectElement(this).SelectByIndex(index);
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = TagName,
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public virtual List<string> GetDropdownOptions()
        {
            Driver.WaitForElementVisible(this);
            try
            {
                var options = new SelectElement(this).Options;

                return options.Select(x => x.Text).ToList();
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = TagName,
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }
        
        public string GetDefaultDropDownValue()
        {
            Driver.WaitForElementVisible(this);
            try
            {
                var defaultValue = new SelectElement(this);
                return defaultValue.SelectedOption.Text;
            }
            catch
            {
                throw new AtElementNotPresentException()
                {
                    ElementSelector = TagName,
                    ErrorContext = AtExceptionTypes.Navigation
                };
            }
        }

        public virtual void ClickButtonUsingJs()
        {
            try
            {
                var ex = (IJavaScriptExecutor) Driver;
                ex.ExecuteScript("arguments[0].click();", _element);
            }
            catch (Exception e)
            {
                throw new AtGenericException($"Unable to click the button: {this}. Exception message - {e.Message}");
            }
        }

        public virtual void SendKeysUsingJs(string text)
        {
            try
            {
                var ex = (IJavaScriptExecutor)Driver;
                ex.ExecuteScript($"arguments[0].value='{text}';", _element);
            }
            catch (Exception e)
            {
                throw new AtGenericException($"Unable to send keys - {text} the element: {this}. Exception message - {e.Message}");
            }
        }

        private void JSClick()
        {
            var script =
                "return (function(elements){\n\rvar returnValue = false;\n\rvar elementToClick = elements[0];\n\relementToClick.addEventListener('click', function(){\n\rreturnValue = true;\n\rreturn true;\n\r})\n\relementToClick.click();\n\rreturn returnValue;\n\r})(arguments);\n\r";
            try
            {
                if (!(bool)((IJavaScriptExecutor)Driver).ExecuteScript(script, (object)this))
                    throw new ElementNotVisibleException();
            }
            catch (StaleElementReferenceException)
            {
                var webElement = ResolveStaleElementReferences();
                if (!(bool)((IJavaScriptExecutor)Driver).ExecuteScript(script, (object)webElement))
                    throw new ElementNotVisibleException();
            }
        }

        private void ClickViaSeleniumAction()
        {
            try
            {
                new Actions(Driver).Click(this).Perform();
            }
            catch (StaleElementReferenceException)
            {
                var onElement = ResolveStaleElementReferences();
                new Actions(Driver).Click(onElement).Perform();
            }
            catch (Exception e)
            {
                throw new AtGenericException($"Unable to click the button: {this}. Exception message - {e.Message}");
            }
        }

        private IWebElement ResolveStaleElementReferences()
        {
            if (!(this is IAtWebElement atWebElement)) return this;
            try
            {
                var selector = atWebElement.Selector;
                return Driver.FindElement(atWebElement.Locator, atWebElement.LocatorValue);
            }
            catch
            {
                // ignored
            }

            return this;
        }

        public void SendKeysByChar(string text)
        {
            try
            {
                foreach (char val in text)
                {
                    SendKeys(val.ToString());
                    Driver.SetImplicitWait(TimeSpan.FromMilliseconds(10));
                }
            }
            catch (StaleElementReferenceException)
            {
                if (!(_element is IAtWebElement element))
                    return;
                Driver.FindElement(element.Locator, element.LocatorValue).SendKeysByChar(text);
            }
        }

        public void MouseHover()
        {
            try
            {
                Actions actions = new Actions(Driver);
                actions.MoveToElement(_element).Build().Perform();
            }
            catch(Exception e)
            {
                throw new ElementNotInteractableException("Failed to hover on the element. " + e.Message);
            }
        }

        public void MoveSlider(int value)
        {
            try
            {
                Actions slider = new Actions(Driver);
                slider.ClickAndHold(_element);
                slider.MoveByOffset(value, 0).Release().Perform();
            }
            catch(Exception e)
            {
                throw new ElementNotInteractableException("Failed to move the slider. " + e.Message);
            }
        }

        public string GetDomAttribute(string attributeName)
        {
            return _element.GetDomAttribute(attributeName);
        }

        public string GetDomProperty(string propertyName)
        {;
            return _element.GetDomProperty(propertyName);
        }

        public ISearchContext GetShadowRoot()
        {
            return _element.GetShadowRoot();
        }
    }
}