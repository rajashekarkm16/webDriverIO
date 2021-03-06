// ------------------------------------------------------------------------------
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
namespace Dnata.TravelRepublic.MobileWeb.UI.Tests.Features.Meta.Hotel
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.8.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("MetaTripAdvisorHotel")]
    [NUnit.Framework.CategoryAttribute("MetaRegression")]
    public partial class MetaTripAdvisorHotelFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "MetaRegression"};
        
#line 1 "TripAdvisor.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Meta/Hotel", "MetaTripAdvisorHotel", "\tIn order to verify meta deeplink URLs\r\n\tAs a tester\r\n\tI want to generate deepLin" +
                    "k urls from meta and use them in the browser", ProgrammingLanguage.CSharp, new string[] {
                        "MetaRegression"});
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
#line 9
 testRunner.And("I am on Meta Franklin site", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("verify TripAdvisorDesktopUK Hotel MetaChannel")]
        [NUnit.Framework.CategoryAttribute("UKOnly")]
        [NUnit.Framework.TestCaseAttribute("Trip Advisor (desktop) UK", "1", "2,1,1", "20", "8", "117436,2540494,394244", null)]
        public virtual void VerifyTripAdvisorDesktopUKHotelMetaChannel(string metaChannel, string noOfRooms, string occupancy, string startDaysFromCurrentDate, string duration, string estabIds, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "UKOnly"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("metaChannel", metaChannel);
            argumentsOfScenario.Add("noOfRooms", noOfRooms);
            argumentsOfScenario.Add("occupancy", occupancy);
            argumentsOfScenario.Add("startDaysFromCurrentDate", startDaysFromCurrentDate);
            argumentsOfScenario.Add("duration", duration);
            argumentsOfScenario.Add("estabIds", estabIds);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("verify TripAdvisorDesktopUK Hotel MetaChannel", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 12
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
#line 13
testRunner.Given("I am on Hotel tab on meta search page and choose environment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 14
testRunner.And(string.Format("I run hotel search for {0} {1} {2} {3} {4} {5}", metaChannel, noOfRooms, occupancy, startDaysFromCurrentDate, duration, estabIds), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 15
testRunner.Then(string.Format("Validate Meta Reference in url and cookies {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 16
testRunner.When("I select room type 1 and board type 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
testRunner.And("Complete the full payment hotel booking using VisaCredit payment with ThreeDS fal" +
                        "se authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 18
testRunner.Then(string.Format("Validate ImageReference added for {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 19
testRunner.And("Validate totalprice is matching from metachannel", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 20
testRunner.And("Booking references of booked items are available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("verify TripAdvisorDesktopIE Hotel MetaChannel")]
        [NUnit.Framework.TestCaseAttribute("Trip Advisor (desktop) IE", "1", "2,1,1", "30", "6", "4546942,1619097,126042", null)]
        public virtual void VerifyTripAdvisorDesktopIEHotelMetaChannel(string metaChannel, string noOfRooms, string occupancy, string startDaysFromCurrentDate, string duration, string estabIds, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("metaChannel", metaChannel);
            argumentsOfScenario.Add("noOfRooms", noOfRooms);
            argumentsOfScenario.Add("occupancy", occupancy);
            argumentsOfScenario.Add("startDaysFromCurrentDate", startDaysFromCurrentDate);
            argumentsOfScenario.Add("duration", duration);
            argumentsOfScenario.Add("estabIds", estabIds);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("verify TripAdvisorDesktopIE Hotel MetaChannel", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 27
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
#line 28
testRunner.Given("I am on Hotel tab on meta search page and choose environment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 29
testRunner.And(string.Format("I run hotel search for {0} {1} {2} {3} {4} {5}", metaChannel, noOfRooms, occupancy, startDaysFromCurrentDate, duration, estabIds), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 30
testRunner.Then(string.Format("Validate Meta Reference in url and cookies {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 31
testRunner.When("I select room type 1 and board type 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 32
testRunner.And("Complete the full payment hotel booking using VisaCredit payment with ThreeDS fal" +
                        "se authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 33
testRunner.Then(string.Format("Validate ImageReference added for {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 34
testRunner.And("Validate totalprice is matching from metachannel", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 35
testRunner.And("Booking references of booked items are available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("verify TripAdvisorMobileIE Hotel MetaChannel")]
        [NUnit.Framework.TestCaseAttribute("Trip Advisor (mobile) IE", "1", "2,1,1", "10", "5", "231186,1619097,740339", null)]
        public virtual void VerifyTripAdvisorMobileIEHotelMetaChannel(string metaChannel, string noOfRooms, string occupancy, string startDaysFromCurrentDate, string duration, string estabIds, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("metaChannel", metaChannel);
            argumentsOfScenario.Add("noOfRooms", noOfRooms);
            argumentsOfScenario.Add("occupancy", occupancy);
            argumentsOfScenario.Add("startDaysFromCurrentDate", startDaysFromCurrentDate);
            argumentsOfScenario.Add("duration", duration);
            argumentsOfScenario.Add("estabIds", estabIds);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("verify TripAdvisorMobileIE Hotel MetaChannel", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 42
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
#line 43
testRunner.Given("I am on Hotel tab on meta search page and choose environment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 44
testRunner.And(string.Format("I run hotel search for {0} {1} {2} {3} {4} {5}", metaChannel, noOfRooms, occupancy, startDaysFromCurrentDate, duration, estabIds), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 45
testRunner.Then(string.Format("Validate Meta Reference in url and cookies {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 46
testRunner.When("I select room type 1 and board type 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 47
testRunner.And("Complete the full payment hotel booking using VisaCredit payment with ThreeDS fal" +
                        "se authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 48
testRunner.Then(string.Format("Validate ImageReference added for {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 49
testRunner.And("Validate totalprice is matching from metachannel", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 50
testRunner.And("Booking references of booked items are available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("verify TripAdvisorMobileUK Hotel MetaChannel")]
        [NUnit.Framework.CategoryAttribute("UKOnly")]
        [NUnit.Framework.TestCaseAttribute("Trip Advisor (mobile) UK", "1", "2,0,0", "20", "6", "231186,740339,394244", null)]
        public virtual void VerifyTripAdvisorMobileUKHotelMetaChannel(string metaChannel, string noOfRooms, string occupancy, string startDaysFromCurrentDate, string duration, string estabIds, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "UKOnly"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("metaChannel", metaChannel);
            argumentsOfScenario.Add("noOfRooms", noOfRooms);
            argumentsOfScenario.Add("occupancy", occupancy);
            argumentsOfScenario.Add("startDaysFromCurrentDate", startDaysFromCurrentDate);
            argumentsOfScenario.Add("duration", duration);
            argumentsOfScenario.Add("estabIds", estabIds);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("verify TripAdvisorMobileUK Hotel MetaChannel", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 57
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
#line 58
testRunner.Given("I am on Hotel tab on meta search page and choose environment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 59
testRunner.And(string.Format("I run hotel search for {0} {1} {2} {3} {4} {5}", metaChannel, noOfRooms, occupancy, startDaysFromCurrentDate, duration, estabIds), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 60
testRunner.Then(string.Format("Validate Meta Reference in url and cookies {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 61
testRunner.When("I select room type 1 and board type 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 62
testRunner.And("Complete the full payment hotel booking using VisaCredit payment with ThreeDS fal" +
                        "se authorization", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 63
testRunner.Then(string.Format("Validate ImageReference added for {0}", metaChannel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 64
testRunner.And("Validate totalprice is matching from metachannel", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 65
testRunner.And("Booking references of booked items are available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
