
﻿using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Enums;
using Dnata.Automation.BDDFramework.WebElements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dnata.Automation.BDDFramework.BasePage
{
   public class BasePage
    {
        private readonly IAtWebDriver _webDriver;
        
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

        public BasePage(IAtWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private string GetLocatorWithReplacementValues( string locatorValue, params string[] replaceHashValues)
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

        public AtBy GetBy(LocatorType locatorType, string locatorValue, params string[] param)
        {
            MethodInfo byMethod;
            byMethod = typeof(By).GetMethod(ByMechanisms.ContainsKey(locatorType.ToString())
             ? ByMechanisms[locatorType.ToString()]
             : locatorType.ToString());
            string lValue = GetLocatorWithReplacementValues(locatorValue, param);
            return new AtBy()
            {
                locatorType = locatorType,
                locatorValue = lValue,
            by = (By)byMethod?.Invoke(null, new object[1] { lValue })
            };
        }

        #region [ Browser ]

        public void OpenNewActiveTab(string url)
        {
            _webDriver.OpenNewActiveTab(url);
        }

        public void CloseTab(string tabInstance)
        {
            _webDriver.CloseTab(tabInstance);
        }

        public string GetActiveTabId()
        {
            return _webDriver.GetActiveTabId();
        }

        public string GetUrlOfNewWindow()
        {
            string title = _webDriver.GetActiveTabId();
            _webDriver.SwitchToWindow(_webDriver.WindowHandles.Count - 1);
            string Url = _webDriver.Url;
            _webDriver.CloseAdditionalWindows(title);
            return Url;
        }

        public void GoToTab(string tabInstance)
        {
            _webDriver.GoToTab(tabInstance);
        }

        public void CloseAllTabs()
        {
            _webDriver.CloseAllTabs();
        }

        public void GotoURL(string url)
        {
            _webDriver.GotoUrl(url);
        }

        public void NavigateBack()
        {
            _webDriver.NavigateBack();
        }

        public void NavigateForward()
        {
            _webDriver.NavigateForward();
        }

        public void Refresh()
        {
            _webDriver.Refresh();
        }

        public string GetTitle()
        {
            return _webDriver.Title;
        }

        public string GetCurrentURL()
        {
            return _webDriver.Url;
        }

        public void AcceptAlert()
        {
            _webDriver.AcceptAlert();
        }

        public bool IsAlertPresent()
        {
            return _webDriver.IsAlertPresent();
        }

        public void SwitchToWindow(int index)
        {
            _webDriver.SwitchToWindow(index);
        }

        public void SwitchToActiveElement()
        {
            _webDriver.SwitchToActiveElement();
        }

        public void FullScreen()
        {
            _webDriver.FullScreen();
        }

        public void Maximize()
        {
            _webDriver.Maximize();
        }

        public void Minimize()
        {
            _webDriver.Minimize();
        }

        public void SetCookie(string cookieName, string value)
        {
            _webDriver.SetCookie(cookieName, value);
        }

        public string GetCookieValue(string cookieName)
        {
            return _webDriver.GetCookieValue(cookieName);
        }
        public void DeleteAllCookies()
        {
            _webDriver.DeleteAllCookies();
        }

        public void DeleteCookie(string cookieName)
        {
            _webDriver.DeleteCookie(cookieName);
        }

        public void ClearWebStorage()
        {
            _webDriver.ClearWebStorage();
        }

        #endregion
    }
}