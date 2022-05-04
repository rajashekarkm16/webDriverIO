using Dnata.Automation.BDDFramework.API.BaseSteps;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Tests.Steps
{
    [Binding]
    public sealed class CommonSteps : BaseSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly IHomePage _homePage;

        public CommonSteps(ScenarioContext scenarioContext, IHomePage homePage)
            :base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _homePage = homePage;
        }

        [When(@"I navigate to landing page (.*), (.*), (.*)")]
        public void WhenINavigateToLandingPageSpain(string destinationName, string destinationID, int ProductType)
        {
            _homePage.NavigateToLandingPage(Config.GetLandingPageURL(destinationName, destinationID, ProductType));
        }

    }
}
