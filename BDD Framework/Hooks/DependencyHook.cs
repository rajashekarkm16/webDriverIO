using BoDi;
using Dnata.Automation.BDDFramework;
using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.DriverDecorators;
using Dnata.Automation.BDDFramework.Reporting.TestRail;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using Dnata.Automation.BDDFramework.Helpers.Screenshot;
using ReportPortal.Client;
using BrowserStack;
using System.Collections.Generic;
using Dnata.Automation.BDDFramework.BrowserStack;
using Newtonsoft.Json;
using Dnata.Automation.BDDFramework.Helpers;
using OpenQA.Selenium;

namespace Dnata.TravelRepublic.MobileWeb.UI.Tests.Hooks
{
    [Binding]
    public sealed class DependencyHook
    {
        private const string Ie = "IE";
        private const string None = "None";
        private const string Agent = "Agent";
        private const string Public = "Public";

        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        //private readonly ITestRailManager _testRailManager;
        private readonly IScreenShotHelper _screenshotHelper;

        public IAtWebDriver _webDriver;

        public DependencyHook(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext, IScreenShotHelper screenshotHelper)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _screenshotHelper = screenshotHelper;
            //_testRailManager = testRailManager;
        }

        Local local;

        [BeforeScenario(Order = 2)]
        public void BeforeScenario()
        {
            if (Config.IsBrowserStackLocal())
            {
                local = new Local();
                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("key", "FQSpwvLe3LPrJSNsLGDU")
                };
                bsLocalArgs.Add(new KeyValuePair<string, string>("forcelocal", "true"));
                local.start(bsLocalArgs);
                Console.WriteLine("Browser stack local instance is running: "+local.isRunning());
            }            

            // Open the driver
            if (Config.GetBrowser() == "Chrome_Mobile" && !Config.GetIsRemote())
                _webDriver = DriverContext.GetDriver(Config.GetBrowser(), Config.GetMobileChromeDriverOptions(), Config.GetIsRemote(), Config.GetRemoteURL());

            else if (Config.GetIsRemote())
            {
                List<BrowserStackConfig> browserStackConfigs = JsonHelper.ReadFromJson<List<BrowserStackConfig>>("BrowserStackConfig");
                BrowserStackConfig browserStackConfig = browserStackConfigs[browserStackConfigs.FindIndex(config => config.Browser.Equals(Config.GetBrowser()) && config.IsRealMobile.Equals(HelperFunctions.IsBrowserStackMobileDevice()) && config.OS.Equals(Config.GetOS()))];
                string projectName = AtConfiguration.GetConfiguration("ProjectName") + "_" + Config.GetEnvironment() + "_" + Config.GetDomain() + "_" + DateTime.Now.ToString("dd MMM yyyy");
                _webDriver = DriverContext.GetDriver(browserStackConfig, projectName, Config.GetBrowserStackBuild()+_featureContext.FeatureInfo.Title, _scenarioContext.ScenarioInfo.Title, Config.GetRemoteURL(), Config.IsBrowserStackLocal());
            }

            else
                _webDriver = DriverContext.GetDriver(Config.GetBrowser(), Config.GetIsRemote(), Config.GetRemoteURL());

            // Register the driver
            _objectContainer.RegisterInstanceAs(_webDriver);

            // Navigate to the URL 
            _webDriver.GotoUrl(Config.GetApplicationURL(Config.GetDomain(), Config.GetEnvironment()));
            _webDriver.SetCookie("CookiesAccepted", "true");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                if (Config.GetIsRemote())
                {
                    if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                        ((IJavaScriptExecutor)_webDriver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + _scenarioContext.TestError.Message.Replace("\r\n", "") + " \"}}");
                    else
                        ((IJavaScriptExecutor)_webDriver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" \"}}");
                }

                string fileName = AtConfiguration.GetConfiguration<string>("OutputDirectory") + "\\" + DateTime.Now.ToString("ddd dd MM yyyy") +"\\"+ _featureContext.FeatureInfo.Title + "_" + TestContext.CurrentContext.Test.Name.Split('(')[0] + DateTime.Now.ToString("ssmmhhddMM") + ".png";
                if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                {
                    _screenshotHelper.TakeScreenshot(_webDriver, AtConfiguration.GetConfiguration<string>("OutputDirectory") + "\\" + DateTime.Now.ToString("ddd dd MM yyyy"), fileName, false);
                }

                //if (AtConfiguration.GetConfiguration<bool>("isSaveResultsToTestRail"))
                //{
                //    _testRailManager.SendTestCaseResult(Config.GetTestRunID());
                //}
                _webDriver.LogBrowserLogsToConsole();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (Config.IsBrowserStackLocal())
                    local.stop();
                Console.WriteLine("Final URL: " + _webDriver.Url);
                _webDriver.CloseAllTabs();
                _objectContainer.Dispose();
            }
        }

        private static Func<IObjectContainer, T> CreatePage<T>(bool isInternetExplorer)
        {
            return container => container.Resolve<T>(isInternetExplorer ? Ie : None);
        }

        private static Func<IObjectContainer, T> CreatePage<T>(string key)
        {
            return container => container.Resolve<T>(key);
        }

    }



}
