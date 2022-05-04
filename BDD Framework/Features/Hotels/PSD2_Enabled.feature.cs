﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.8.0.0
//      SpecFlow Generator Version:3.8.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dnata.TravelRepublic.MobileWeb.UI.Tests.Features.Hotels
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.8.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("PSD2 Enabled Tests on Hotels")]
    public partial class PSD2EnabledTestsOnHotelsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "PSD2_Enabled.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Hotels", "PSD2 Enabled Tests on Hotels", "\tIn order to make hotel booking with PSD2 \r\n\tAs a accounts manager\r\n\tI want to bo" +
                    "okings to be complete", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line hidden
#line 8
testRunner.Given("Click on Accept and close cookies button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify hotel booking with PSD2 With Challenge flow")]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "70", "5", "2,1,0,2,0,0", "ThreeDSVONE", "CHALLENGEIDENTIFIED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "65", "5", "2,1,0,2,0,0", "ThreeDSVONE", "CHALLENGENOTIDENTIFIED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "77", "5", "2,1,0,2,0,0", "ThreeDSVONE", "CHALLENGEVALIDERROR", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "40", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "CHALLENGEIDENTIFIED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "70", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "CHALLENGEVALIDERROR", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "75", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "BYPASSEDAFTERCHALLENGE", "AUTHORISED", null)]
        public virtual void VerifyHotelBookingWithPSD2WithChallengeFlow(string destination, string departure, string @return, string guests, string firstName, string lastName, string expected, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("destination", destination);
            argumentsOfScenario.Add("departure", departure);
            argumentsOfScenario.Add("return", @return);
            argumentsOfScenario.Add("guests", guests);
            argumentsOfScenario.Add("firstName", firstName);
            argumentsOfScenario.Add("lastName", lastName);
            argumentsOfScenario.Add("Expected", expected);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify hotel booking with PSD2 With Challenge flow", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 11
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 12
testRunner.Given(string.Format("I perform hotel search for {0} dates {1} and {2} with {3}", destination, departure, @return, guests), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 13
testRunner.When("Select a random hotel from the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 14
testRunner.And("Select random rooms and board type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 15
testRunner.And("Check the selected rooms and total cost", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 16
testRunner.And(string.Format("Populate Hotel Guest details with first name {0} and last name {1}", firstName, lastName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 17
testRunner.And("Choose the full payment hotel booking using VisaCredit payment and complete 3DS b" +
                        "ooking", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 18
testRunner.Then("Booking references of booked items are available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 19
testRunner.And("Booked hotel details matches", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 20
testRunner.And("Validate hotel information, lead contact and price details in database", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify hotel booking with PSD2 With Frictionless flow")]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "60", "5", "2,1,0,2,0,0", "ThreeDSVONE", "NOTENROLLED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "60", "5", "2,1,0,2,0,0", "ThreeDSVONE", "UNKNOWNENROLLMENT", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "60", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "FRICTIONLESSIDENTIFIED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "70", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "FRICTIONLESSNOTIDENTIFIED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "60", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "FRICTIONLESSVALIDERROR", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "60", "5", "2,1,0,2,0,0", "ThreeDS", "BYPASSED", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "50", "5", "2,1,0,2,0,0", "ThreeD", "Authorised", "AUTHORISED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "50", "5", "2,1,0", "Refused", "Refused", "AUTHORISED", null)]
        public virtual void VerifyHotelBookingWithPSD2WithFrictionlessFlow(string destination, string departure, string @return, string guests, string firstName, string lastName, string expected, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("destination", destination);
            argumentsOfScenario.Add("departure", departure);
            argumentsOfScenario.Add("return", @return);
            argumentsOfScenario.Add("guests", guests);
            argumentsOfScenario.Add("firstName", firstName);
            argumentsOfScenario.Add("lastName", lastName);
            argumentsOfScenario.Add("Expected", expected);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify hotel booking with PSD2 With Frictionless flow", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 31
 this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 32
testRunner.Given(string.Format("I perform hotel search for {0} dates {1} and {2} with {3}", destination, departure, @return, guests), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 33
testRunner.When("Select a random hotel from the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 34
testRunner.And("Select random rooms and board type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 35
testRunner.And("Check the selected rooms and total cost", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 36
testRunner.And(string.Format("Populate Hotel Guest details with first name {0} and last name {1}", firstName, lastName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 37
testRunner.And("Choose the full payment hotel booking using VisaCredit payment and complete booki" +
                        "ng", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 38
testRunner.Then("Booking references of booked items are available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 39
testRunner.And("Booked hotel details matches", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 40
testRunner.And("Validate hotel information, lead contact and price details in database", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify hotel booking refused after challenge window")]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "20", "5", "2,1,0,2,0,0", "ThreeDSVONE", "CHALLENGEUNKNOWNIDENTITY", "REFUSED", null)]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "70", "5", "2,1,0,2,0,0", "ThreeDSVTWO", "CHALLENGEUNKNOWNIDENTITY", "REFUSED", null)]
        public virtual void VerifyHotelBookingRefusedAfterChallengeWindow(string destination, string departure, string @return, string guests, string firstName, string lastName, string expected, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("destination", destination);
            argumentsOfScenario.Add("departure", departure);
            argumentsOfScenario.Add("return", @return);
            argumentsOfScenario.Add("guests", guests);
            argumentsOfScenario.Add("firstName", firstName);
            argumentsOfScenario.Add("lastName", lastName);
            argumentsOfScenario.Add("Expected", expected);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify hotel booking refused after challenge window", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 54
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 55
testRunner.Given(string.Format("I perform hotel search for {0} dates {1} and {2} with {3}", destination, departure, @return, guests), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 56
testRunner.When("Select a random hotel from the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 57
testRunner.And("Select random rooms and board type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 58
testRunner.And("Check the selected rooms and total cost", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 59
testRunner.And(string.Format("Populate Hotel Guest details with first name {0} and last name {1}", firstName, lastName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 60
testRunner.And("Choose the full payment hotel booking using VisaCredit payment and complete 3DS b" +
                        "ooking", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 61
testRunner.Then("Booking should be declined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify hotel booking directly refused")]
        [NUnit.Framework.TestCaseAttribute("Tenerife", "80", "5", "2,1,0,1,1,0", "ThreeDSVTWO", "FRICTIONLESSREJECTED", "REFUSED", null)]
        public virtual void VerifyHotelBookingDirectlyRefused(string destination, string departure, string @return, string guests, string firstName, string lastName, string expected, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("destination", destination);
            argumentsOfScenario.Add("departure", departure);
            argumentsOfScenario.Add("return", @return);
            argumentsOfScenario.Add("guests", guests);
            argumentsOfScenario.Add("firstName", firstName);
            argumentsOfScenario.Add("lastName", lastName);
            argumentsOfScenario.Add("Expected", expected);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify hotel booking directly refused", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 69
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 70
testRunner.Given(string.Format("I perform hotel search for {0} dates {1} and {2} with {3}", destination, departure, @return, guests), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 71
testRunner.When("Select a random hotel from the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 72
testRunner.And("Select random rooms and board type", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 73
testRunner.And("Check the selected rooms and total cost", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 74
testRunner.And(string.Format("Populate Hotel Guest details with first name {0} and last name {1}", firstName, lastName), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 75
testRunner.And("Choose the full payment hotel booking using VisaCredit payment and complete booki" +
                        "ng", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 76
testRunner.Then("Booking should be declined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
