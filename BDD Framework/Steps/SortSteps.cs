using Dnata.Automation.BDDFramework.Reporting.CustomReporter;
using Dnata.TravelRepublic.MobileWeb.UI.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Dnata.TravelRepublic.MobileWeb.UI.Steps
{
    [Binding]
    public sealed class SortSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _context;
        private readonly ISortComponent _sortComponent;
        private readonly IHotelSearchResults _hotelSearchResults;
        private readonly IFlightSearchResults _flightSearchResults;

        public SortSteps(ISortComponent sortComponent, IHotelSearchResults hotelSearchResults, IFlightSearchResults flightSearchResults, ScenarioContext context)
        {
            _sortComponent = sortComponent;
            _hotelSearchResults = hotelSearchResults;
            _flightSearchResults = flightSearchResults;
            _context = context;
        }

        [When(@"I open sort options")]
        public void WhenIOpenSortOptions()
        {
            _hotelSearchResults.EditSortOption();
        }

        [When(@"Select the sort (.*)")]
        public void WhenISelectTheSort(string option)
        {
            _sortComponent.SelectSortOption(option);
        }

        [When(@"Select the flight sort (.*)")]
        public void WhenSelectTheFlightSortRecommended(string option)
        {
            _sortComponent.SelectFlightSortOption(option);
        }


        [Then(@"Search results are sorted based on the selected (.*)")]
        public void ThenSearchResultsAreSortedBasedOnTheSelected(string option)
        {
            Assert.IsTrue(_hotelSearchResults.VerifyAppliedSortOption(option));
        }

        [Then(@"Duplicate search results are not displayed")]
        public void ThenDuplicateSearchResultsAreNotDisplayed()
        {
            Assert.IsFalse(_hotelSearchResults.VerifyNoDuplicates());
        }

        [Then(@"Recommended option is selected by default")]
        public void ThenRecommendedOptionIsSelectedByDefault()
        {
            Assert.AreEqual("Recommended", _sortComponent.GetSelectedSortOption(), "Recommended  sort option validation");
        }
    }
}
