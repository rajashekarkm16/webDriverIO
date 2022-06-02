import { Given, When, Then } from '@wdio/cucumber-framework';
import SearchUnit from '../pageobjects/components/searchUnit';
import HotelSearchResultsPage from '../pageobjects/hotelSearchResultsPage';
import FilterDetails from '../modals/filterDetails';
import { SearchCriteria } from '../modals/searchCriteria';
import HotelEstabPage from '../pageobjects/hotelEstabPage';
import { SearchResultsDisplayedPerLoad } from '../utilities/constants';
import hotelEstabPage from '../pageobjects/hotelEstabPage';


Given(/^I am on hotel search result page for destination \'(.+)\'$/, async (Destination) => {
    await SearchUnit.populateSearchByDestination(Destination);
});

Given(/^I am on hotels search result page for destination \'(.*)\', checkin date as  \'(.*)\' duration \'(.*)\' GuestInfo \'(.*)\'$/, async (Destination, CheckinDate, DurationOfDays, GuestsData) => {
    await SearchUnit.populateSearch(Destination, CheckinDate, DurationOfDays, GuestsData);
});

Then(/^Hotel card should display star ratings, location and the Hotel name for all hotels$/, async () => {
    await HotelSearchResultsPage.verifyHotelNameForAllHotels();
    await HotelSearchResultsPage.verifyStarRatingForAllHotels();
    await HotelSearchResultsPage.verifyAddressForAllHotels();
});

Then(/^Mandatory transfer message (.+) should be displayed in the hotel card$/, async (message) => {
    await HotelSearchResultsPage.verifyPriceIncludeListContains(message);
});

Then(/^Average review score should be dsiplayed on hotel card$/, async () => {
    await HotelSearchResultsPage.waitForPageLoad();
    await HotelSearchResultsPage.verifyAverageReviewScore();
});

Then(/^Total number of reviews are displayed$/, async () => {
    const numReview: number = await HotelSearchResultsPage.getcustomerReviewsCount(1);
    await expect(numReview > 0).toEqual(true);
});

Then(/^Smiley icon is displayed$/, async () => {
    await expect(await HotelSearchResultsPage.isSmileyIconExist(1)).toEqual(true);

});

Then(/^Hotel card should have secure today pill with message (.+)$/, async (Message) => {
    await HotelSearchResultsPage.waitForPageLoad();
    const pillTodayText = await HotelSearchResultsPage.getSecureTodayPillText(1);
    await expect(pillTodayText).toEqual(Message);
});

Then(/^Price summary information should be displayed$/, async () => {
    await expect(await HotelSearchResultsPage.isPriceSummarySectionDisplayed()).toEqual(true);
});

Then(/^\'(.*)\' and per person should be displayed in brackets$/, async (PerPersonText) => {
    await HotelSearchResultsPage.verifyPerPriceInsideBracket(PerPersonText);
    await expect(await HotelSearchResultsPage.isPerPersonPriceDisplayed()).toEqual(true);
});

Then(/^\'(.*)\' and total price should be displayed$/, async (TotalPriceText) => {
    await expect(await HotelSearchResultsPage.getTotalPriceText()).toEqual(TotalPriceText);
    await expect(await HotelSearchResultsPage.isTotalPriceDisplayed()).toEqual(true);
});

When(/^I land on hotel search results page$/, async () => {
    await HotelSearchResultsPage.waitForPageLoad();
});

Then(/^\'(.*)\' should be displayed$/, async (TotalPriceText) => {
    await expect(await HotelSearchResultsPage.getTotalPriceText()).toEqual(TotalPriceText);
});

Then(/^Was price and Now Price should be displayed$/, async () => {
    await HotelSearchResultsPage.waitForPageLoad();
    await HotelSearchResultsPage.isHotelCardWithOldPriceAvailalble();
    await HotelSearchResultsPage.clickOnHotelName();
});

//#region (Left Nav Filter)

Given(/^Hotel search result page is open for destination \'(.*)\'$/, async (destination: string) => {
    if (FilterDetails.HotelSearchResultPageUrl == undefined || FilterDetails.HotelSearchResultPageUrl === '') {
        await SearchUnit.open("homePage");
        await SearchUnit.selectTab("HOTEL ONLY")
        await SearchUnit.selectDestination(destination);
        SearchCriteria.Destination = destination
        await SearchUnit.populateDate(30, 2);
        await SearchUnit.clickonSearchButton();
        await HotelSearchResultsPage.waitForPageLoad();
        FilterDetails.HotelSearchResultPageUrl = await browser.getUrl();
    }
    else {
        await browser.navigateTo(FilterDetails.HotelSearchResultPageUrl);
        await HotelSearchResultsPage.waitForPageLoad();
    }

});

Then(/^I see hotel has local charges displayed$/, async (LocalChargesText) => {
    await HotelSearchResultsPage.verifyPriceIncludeListContains(LocalChargesText);
});

Then(/^All filter items should be unchecked for \'(.*)\'$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(await filterName);
    FilterDetails.FilterItems = allFilterItems;
    await HotelSearchResultsPage.verifyAllUncheckedFilterItem(await filterName, allFilterItems);
});

Then(/^Each Hotel card label should have \'(.*)\' listed in the filter$/, async (filterName) => {
    await HotelSearchResultsPage.verifyFilterLabelonEachHotelCard(await filterName, FilterDetails.FilterItems);
});

Then(/^I check one by one \'(.*)\' filter and verify hotel count and each hotel has selected filter$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(await filterName);
    await HotelSearchResultsPage.verifyFilterLabelOnHotelCardAfterSingleFilterSelection(await filterName, allFilterItems);
});

Then(/^I check multiple \'(.*)\' filters and verify hotel count and each hotel has selected filter$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(await filterName);
    FilterDetails.FilterItems = allFilterItems;
    await HotelSearchResultsPage.verifyFilterLabelOnHotelCardAfterMultipleFilterSelection(await filterName, allFilterItems);
});

Then(/^I uncheck one by one all \'(.*)\' filter and verify hotel count and each hotel has selected filter$/, async (filterName) => {
    await HotelSearchResultsPage.verifyFilterLabelOnHotelCardAfterUncheckFilter(await filterName, FilterDetails.FilterItems);
});

Then(/^Filter \'(.*)\' should be expanded$/, async (filterName) => {
    expect(await HotelSearchResultsPage.isFilterExpanded(await filterName)).toBe("true");
});

When(/^I collapse \'(.*)\' filter$/, async (filterName) => {
    await HotelSearchResultsPage.toggleFilter(await filterName, false)
});

Then(/^Filter \'(.*)\' should be collapsed$/, async (filterName) => {
    expect(await HotelSearchResultsPage.isFilterExpanded(await filterName)).toBe("false");
});

Then(/^Filter item should not be displayed for \'(.*)\'$/, async (filterName) => {
    switch (await filterName) {
        case "Star Rating":
            await HotelSearchResultsPage.veriftStarRatingIsNotDisplayed();
            break;
        case "Total Price":
            expect(await HotelSearchResultsPage.isTotalPriceSliderDisplayed()).toBe(false);
            break;
        case "Our Customer Rating":
            expect(await HotelSearchResultsPage.isCustomerRatingSliderDisplayed()).toBe(false);
            break;
        default:
            await HotelSearchResultsPage.verifyFilterItemIsNotDisplayed(await filterName);
            break;
    }
});

When(/^I expand \'(.*)\' filter$/, async (filterName) => {
    await HotelSearchResultsPage.toggleFilter(await filterName, true)
});

When(/^I check first filter for \'(.*)\'$/, async (filterName) => {
    await HotelSearchResultsPage.waitForPageLoad();
    var filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(await filterName);
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterDisabledItems = await filterItems[1];
    FilterDetails.FilterItems = await filterItems[0];
    await HotelSearchResultsPage.checkFilterItem(await filterName, FilterDetails.FilterEnabledItems[0]);
});

When(/^I refresh the page$/, async () => {
    await browser.refresh();
    await HotelSearchResultsPage.waitForSearchResultsToLoad();
});

Then(/^first filter should be checked for \'(.*)\'$/, async (filterName) => {
    await HotelSearchResultsPage.verifyCheckedFilterItem(await filterName, FilterDetails.FilterEnabledItems[0]);
});

When(/^I select lowest star rating filter$/, async () => {
    var filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterItems = await filterItems[0];
    await HotelSearchResultsPage.selectStarRating(FilterDetails.FilterEnabledItems[0]);
});

When(/^I select lowest star rating filter and verify disabled filter items of \'(.*)\'$/, async (filterName: string) => {
    var filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterItems = await filterItems[0];
    await HotelSearchResultsPage.selectStarRating(FilterDetails.FilterEnabledItems[0]);
    var allFilterItem = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    FilterDetails.FilterDisabledItems = allFilterItem[1];
});

When(/^I check first filter of \'(.*)\' and verify disabled filter items of \'(.*)\'$/, async (filterName: string, secondFilter: string) => {
    var firstFilterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    FilterDetails.FilterItems = await firstFilterItems[0];
    var secondFilterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(secondFilter);
    FilterDetails.FilterEnabledItems = await secondFilterItems[0];
    FilterDetails.FilterDisabledItems = await secondFilterItems[1];
    await HotelSearchResultsPage.checkFilterItem(filterName, FilterDetails.FilterItems[0]);
});

When(/^I uncheck first filter of \'(.*)\'$/, async (filterName: string) => {
    await HotelSearchResultsPage.uncheckFilterItem(filterName, FilterDetails.FilterItems[0]);
});

Then(/^lowest star rating filter should be selected$/, async () => {
    await HotelSearchResultsPage.verifySelectedStarRating(FilterDetails.FilterItems[0])
});

Then(/^All star should be unselected$/, async () => {
    var allFilterItems = await HotelSearchResultsPage.getAllStarRating();
    FilterDetails.FilterItems = allFilterItems;
    await HotelSearchResultsPage.verifyAllStarRatingIsUnSelected(allFilterItems);
});

Then(/^I select one by one star rating filter and verify hotel count and each hotel card has selected star rating$/, async () => {
    var allFilterItems = await HotelSearchResultsPage.getAllStarRating();
    await HotelSearchResultsPage.verifyStarRatingOnHotelCardAfterSingleSelection('Star Rating', allFilterItems);
});

Then(/^I select multiple star rating filter and verify hotel count and each hotel card has selected star rating$/, async () => {
    var allFilterItems = await HotelSearchResultsPage.getAllStarRating();
    FilterDetails.FilterItems = allFilterItems;
    await HotelSearchResultsPage.verifyStarRatingOnHotelCardAfterMutipleSelection('Star Rating', allFilterItems);
});

Then(/^I unselect one by one all star rating and verify hotel count and each hotel card has selected star rating$/, async () => {
    await HotelSearchResultsPage.verifyStarRatingOnHotelCardAfterUnSelecting('Star Rating', FilterDetails.FilterItems);
});

Then(/^Disabled \'(.*)\' filter label should not be displayed for each hotel card$/, async (filterName: string) => {
    var allItems: any;
    var enabledFilterItems: any;
    var disabledFilterItems: any;
    switch (filterName.toLocaleLowerCase()) {
        case "star rating":
            allItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
            enabledFilterItems = await allItems[0];
            disabledFilterItems = await allItems[1];
            break;
        default:
            allItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
            enabledFilterItems = await allItems[0];
            disabledFilterItems = await allItems[1];
    }
    if (await disabledFilterItems.length > 0) {
        await HotelSearchResultsPage.verifyFilterLabelonEachHotelCard(filterName, await enabledFilterItems);
    }
});

When(/^I unselect lowest star rating filter$/, async () => {
    await HotelSearchResultsPage.unSelectStarRating(FilterDetails.FilterItems[0]);
});

Then(/^Disabled filter count should be 0 for \'(.*)\' filter$/, async (filterName: string) => {
    var filterItems: any;
    if (filterName.toLowerCase() === 'star rating') {
        filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
    }
    else {
        filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    }
    var disabledFilterItems = await filterItems[1];
    expect(Number(await disabledFilterItems.length)).toEqual(0);
});

When(/^I check \'(.*)\' filter which was disbaled after (?:.*?) (?:.*?) filter selection$/, async (filterName) => {
    if (FilterDetails.FilterDisabledItems.length > 0) {
        await HotelSearchResultsPage.checkFilterItem(await filterName, FilterDetails.FilterDisabledItems[0]);
    }
});

Then(/^lowest star rating filter should be disabled$/, async () => {
    if (FilterDetails.FilterDisabledItems.length > 0)
        await HotelSearchResultsPage.verifyDisabledStarRating(FilterDetails.FilterEnabledItems[0]);
});

Then(/^first filter of \'(.*)\' should be disabled$/, async (filterName: string) => {
    await HotelSearchResultsPage.verifyDisabledFilterItem(filterName, FilterDetails.FilterItems[0]);
});

When(/^I uncheck \'(.*)\' filter which was disbaled after (?:.*?) (?:.*?) filter selection$/, async (filterName) => {
    if (FilterDetails.FilterDisabledItems.length > 0)
        await HotelSearchResultsPage.uncheckFilterItem(await filterName, FilterDetails.FilterDisabledItems[0]);
});

//#region [Total Price Filter]

Then(/^Default Minimum Value and Maximum Value on Price range bar is displayed$/, async () => {
    await HotelSearchResultsPage.isDefaultPriceSliderDisplayed();
    await HotelSearchResultsPage.verifyFilteredMinAndMaxPriceValueDisplayed();
});

Then(/^Price Range Bar and Price Range Indicator should be set to default price$/, async () => {
    await HotelSearchResultsPage.verifyBothFilteredAndSliderRating();
});

When(/^I increase minimum Total price and decrease maximum total price$/, async () => {
    await HotelSearchResultsPage.dragLeftPriceSliderByXCoordinates(40)
    await HotelSearchResultsPage.verifyFilteredPriceRange();
    await HotelSearchResultsPage.dragRightPriceSliderByXCoordinates(-30)
    await HotelSearchResultsPage.verifyFilteredPriceRange();
});

When(/^I decrease minimum Total price and increase maximum total price$/, async () => {
    await HotelSearchResultsPage.dragLeftPriceSliderByXCoordinates(-30)
    await HotelSearchResultsPage.verifyFilteredPriceRange();
    await HotelSearchResultsPage.dragRightPriceSliderByXCoordinates(20)
    await HotelSearchResultsPage.verifyFilteredPriceRange();
});

Then(/^I should see hotels in filtered price range$/, async () => {
    await HotelSearchResultsPage.verifyFilteredPriceRange();
});

Then(/^Other sub categories to be gray out on price range bar$/, async () => {
    await HotelSearchResultsPage.verifyReadOnlyPriceSliderBar();
});

Then(/^Price values should be different at starting and ending points of Price range bar$/, async () => {
    await HotelSearchResultsPage.verifySliderMinAndMaxPriceValueDisplayed();
});

Then(/^The Price value on starting and ending points of price range bar is displayed$/, async () => {
    await HotelSearchResultsPage.verifySliderMinAndMaxPriceValueDisplayed();
});

Then(/^Price values changes in Price Range Indicator at starting and ending points$/, async () => {
    await HotelSearchResultsPage.verifyPriceRangeChangesWithSlider();
});

//#endregion 

//#region [Customer Rating Filter]

When(/^I drag the customer rating bar for \'(.+)\' and \'(.+)\'$/, async (MinRating, MaxRating) => {
    await HotelSearchResultsPage.setCustomerRatingRange(MinRating, MaxRating);
});

Then(/^I should see relevant search results within \'(.+)\' and \'(.+)\'$/, async (MinRating, MaxRating) => {
    await HotelSearchResultsPage.verifyFilteredCustomerRatingRange(MinRating, MaxRating);
});

Then(/^Default Customer Slider Rating value should vary between \'(.+)\' and \'(.+)\'$/, async (DefaultMinRating, DefaultMaxRating) => {
    await HotelSearchResultsPage.verifyMinAndMaxSliderCustomerRating(DefaultMinRating, DefaultMaxRating);
});

When(/^I change the drag bar selection for \'(.+)\' and \'(.+)\'$/, async (UpdatedMinRating, UpdatedMaxRating) => {
    await HotelSearchResultsPage.setCustomerRatingRange(UpdatedMinRating, UpdatedMaxRating);
});

Then(/^Slider bar should be read only for irrelevant filter$/, async () => {
    await HotelSearchResultsPage.verifyReadOnlyRatingSliderBar();
});

Then(/^On bottom of Rating Range Bar the Rating value as \'(.+)\' and \'(.+)\' is displayed$/, async (MinRating, MaxRating) => {
    await HotelSearchResultsPage.verifyMinAndMaxSliderCustomerRating(MinRating, MaxRating);
});

Then(/^Reset filters button should be disabled$/, async () => {
    expect(await HotelSearchResultsPage.isResetFiltersButtonEnabled()).toBe(false);
});

Then(/^Reset filters button should be enabled$/, async () => {
    expect(await HotelSearchResultsPage.isResetFiltersButtonEnabled()).toBe(true);
});

Then(/^I check all filters for \'(.+)\'$/, async (filterName: string) => {
    var filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterDisabledItems = await filterItems[1];
    FilterDetails.FilterItems = await filterItems[0];
    if (filterName.trim().toLocaleLowerCase() == 'holiday types' || filterName.trim().toLocaleLowerCase() == 'property amenities') {
        await HotelSearchResultsPage.checkSpecificEnableFilterItems(filterName, FilterDetails.FilterEnabledItems);
    }
    else {
        await HotelSearchResultsPage.checkSpecificFilterItems(filterName, FilterDetails.FilterItems);
    }
});

Then(/^Selected \'(.+)\' filters should be displayed in application url$/, async (filterName: string) => {
    if (filterName.trim().toLocaleLowerCase() == 'holiday types' || filterName.trim().toLocaleLowerCase() == 'property amenities') {
        FilterDetails.FilterEnabledItems = await HotelSearchResultsPage.getSelectedFilterItemsFromListOfFilter(filterName, FilterDetails.FilterEnabledItems);
    }
    await HotelSearchResultsPage.verifySelectedFilterItemsInApplicationURL(filterName, FilterDetails.FilterEnabledItems, true);
});

Then(/^\'(.+)\' filters should not be displayed in application url$/, async (filterName: string) => {
    await HotelSearchResultsPage.verifySelectedFilterItemsInApplicationURL(filterName, FilterDetails.FilterEnabledItems, false);
});

When(/^I select all star rating filter$/, async () => {
    var filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterDisabledItems = await filterItems[1];
    FilterDetails.FilterItems = await filterItems[0];
    await HotelSearchResultsPage.selectSpecificStarRating(FilterDetails.FilterEnabledItems);
});

When(/^I click reset filters button$/, async () => {
    await HotelSearchResultsPage.clickOnResetFiltersButton();
});

When(/^I select hotel for index \'(.+)\'$/, async (hotelIndex: string) => {
    await HotelSearchResultsPage.selectAHotel(Number(hotelIndex));
    await HotelEstabPage.waitForHotelEstabPage();
});

Then(/^Selected Property Amenities filter should be displayed on Estab page$/, async () => {
    var facilitiesOnEstabPage = await HotelEstabPage.getAllFacilities();
    facilitiesOnEstabPage.map(async (m) => expect(FilterDetails.FilterEnabledItems).toContain(await m));
});

Then(/^I should see total price range$/, async () => {
    FilterDetails.DefaultMinPriceValue = await HotelSearchResultsPage.getMinFilteredPrice();
    FilterDetails.DefaultMaxPriceValue = await HotelSearchResultsPage.getMaxFilteredPrice();
});

Then(/^I should see customer rating range$/, async () => {
    FilterDetails.DefaultMinCustomerRating = Number(await HotelSearchResultsPage.getCustRatingSliderMinValue());
    FilterDetails.DefaultMaxCustomerRating = Number(await HotelSearchResultsPage.getCustRatingSliderMaxValue());
});

When(/^I select price range \'(.+)\'$/, async (priceRange: string) => {
    var inputMinRange = priceRange.split(":")[0].trim();
    var inputMaxRange = priceRange.split(":")[1].trim();
    var minRange = await HotelSearchResultsPage.getMaxAndMinValuesOfFilterBasedOnExpression(inputMinRange, FilterDetails.DefaultMinPriceValue, FilterDetails.DefaultMaxPriceValue);
    var maxRange = await HotelSearchResultsPage.getMaxAndMinValuesOfFilterBasedOnExpression(inputMaxRange, FilterDetails.DefaultMinPriceValue, FilterDetails.DefaultMaxPriceValue);
    await HotelSearchResultsPage.setTotalPriceRangeByURL(minRange, maxRange);
});

When(/^I select customer rating range \'(.+)\'$/, async (customerRatingRange: string) => {
    var inputMinRange = customerRatingRange.split(":")[0].trim();
    var inputMaxRange = customerRatingRange.split(":")[1].trim();
    var minRange = await HotelSearchResultsPage.getMaxAndMinValuesOfFilterBasedOnExpression(inputMinRange, FilterDetails.DefaultMinCustomerRating, FilterDetails.DefaultMaxCustomerRating);
    var maxRange = await HotelSearchResultsPage.getMaxAndMinValuesOfFilterBasedOnExpression(inputMaxRange, FilterDetails.DefaultMinCustomerRating, FilterDetails.DefaultMaxCustomerRating);
    await HotelSearchResultsPage.setCustomerRatingByURL(minRange, maxRange);
});

Then(/^Hotel should be displayed$/, async () => {
    expect(Number(await HotelSearchResultsPage.countOfHotelCard())).toBeGreaterThan(0);
});

Then(/^Price Range should be reset$/, async () => {
    var minValueAfterReset = await HotelSearchResultsPage.getMinFilteredPrice();
    var maxValueAfterReset = await HotelSearchResultsPage.getMaxFilteredPrice();
    expect(minValueAfterReset).toBe(FilterDetails.DefaultMinPriceValue);
    expect(maxValueAfterReset).toBe(FilterDetails.DefaultMaxPriceValue);
});

Then(/^Customer rating Range should be reset$/, async () => {
    var minValueAfterReset = await HotelSearchResultsPage.getCustRatingSliderMinValue();
    var maxValueAfterReset = await HotelSearchResultsPage.getCustRatingSliderMaxValue();
    expect(Number(minValueAfterReset)).toBe(FilterDetails.DefaultMinCustomerRating);
    expect(Number(maxValueAfterReset)).toBe(FilterDetails.DefaultMaxCustomerRating);
});

//#region (Pagination)
Then(/^Validate hotel pagination$/, async () => {
    await HotelSearchResultsPage.paginationCheck();
});

When(/^I apply filters until the results count should be less than or equals to default count$/, async () => {
    await HotelSearchResultsPage.applyFiltersTillResultCountIsLessThanOrEqualstoDefaultCount();
});
Then(/^Show more hotels button should not be displayed$/, async () => {
    expect(await HotelSearchResultsPage.isShowMoreHotelsDisplayed()).toEqual(false);
});
Then(/^Hotel search result count matches the default count$/, async () => {
    await HotelSearchResultsPage.verifyDisplayedHotelsCount(SearchResultsDisplayedPerLoad);
});

When(/^I click show more hotels button$/, async () => {
    await HotelSearchResultsPage.clickShowMoreHotels();
});

When(/^Search results count is incremented$/, async () => {
    await HotelSearchResultsPage.showMoreCountIncrement(SearchResultsDisplayedPerLoad);
});
//# region (Pagination)

//# region (Skeleton Loading)

When(/^Search results skeleton loading page should be displayed$/, async () => {
    expect(await HotelSearchResultsPage.isSkeletonLoadingDisplayed()).toBe(true);
});

When(/^Filter results skeleton loading page should be displayed$/, async () => {
    await HotelSearchResultsPage.openFilterModal();
    expect(await HotelSearchResultsPage.isSkeletonFilterLoadingDisplayed()).toBe(true);
});

When(/^I click any hotel from search results$/, async () => {
    await HotelSearchResultsPage.selectAHotel();
});
When(/^I click \'(.*)\' link from the breadcrumb$/, async (linkName) => {
    await hotelEstabPage.clickBreadcrumbLink(linkName);
});

Given(/^I am on hotel search result page for destination \'(.*)\' without pageload$/, async (destination) => {
    if (FilterDetails.HotelSearchResultPageUrl == undefined || FilterDetails.HotelSearchResultPageUrl === '') {
        await SearchUnit.open("homePage");
        await SearchUnit.selectTab("HOTEL ONLY")
        await SearchUnit.selectDestination(await destination)
        await SearchUnit.populateDate(30, 2);
        await SearchUnit.clickonSearchButton();
        FilterDetails.HotelSearchResultPageUrl = await browser.getUrl();
    }
    else {
        await browser.navigateTo(FilterDetails.HotelSearchResultPageUrl);
    }
});

When(/^I click show more hotels$/, async () => {
    await HotelSearchResultsPage.waitAndClickShowMoreHotels();
});

//#endregion (Left Nav)



//#endregion (Left Nav)


//#region [Sort By]

When(/^I open sort options$/, async () => {
    await HotelSearchResultsPage.openSortByModal();
});


Then(/^Close sort modal Icon should be displayed$/, async () => {
    await HotelSearchResultsPage.verifySortByModalOpened();
});


Then(/^Sort by \'(.*)\' should be checked$/, async (sortOption: string) => {
    await HotelSearchResultsPage.verifySortByChecked(sortOption)
});

Then(/^Sort modal should display all sort options$/, async () => {
    await HotelSearchResultsPage.VerifySortByOptionsDisplayed()
});


When(/^I select (.+) sort by option$/, async (sortOption: string) => {
    await HotelSearchResultsPage.selectSortBy(sortOption);
});


Then(/^Selected sort by (.+) should be displayed$/, async (selectedSortOption: string) => {
    await HotelSearchResultsPage.validateSortBySelectedText(selectedSortOption);
});

Then(/^Sort criteria (.+) should be added to query url$/, async (queryOption: string) => {
    await HotelSearchResultsPage.validateSortCriteriaAddedToQueryURL(queryOption);
});


Then(/^Search results are sorted based on the selected (.+)$/, async (selectedSortOption: string) => {
    await HotelSearchResultsPage.verifyAppliedSortOption(selectedSortOption);

});

Then(/^I check one by one \'(.*)\' filters and verify hotel count should be less than or equal to original hotel count$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(await filterName);
    await HotelSearchResultsPage.verifyFilterLabelOnHotelCardAfterSingleFilterSelection(await filterName, allFilterItems);
});

Then(/^I check multiple \'(.*)\' filters and verify hotel count should be less than or equal to after selecting each filter$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(filterName);
    FilterDetails.FilterItems = allFilterItems;
    await HotelSearchResultsPage.verifyHotelCardCountAfterMultipleFilterSelectionForHolidayandPropertyAmenitiesFilter(filterName, allFilterItems);
});

Then(/^I uncheck one by one all \'(.*)\' filters and verify hotel count should be greater than or equal to after unselecting each filter$/, async (filterName: string) => {
    FilterDetails.FilterItems = await HotelSearchResultsPage.getSelectedFilterItemsFromListOfFilter(filterName, FilterDetails.FilterItems)
    await HotelSearchResultsPage.verifyHotelCardCountAfterFilterUnCheckingForHolidayandPropertyAmenitiesFilter(filterName, FilterDetails.FilterItems);
})

//#endregion[Sort By]
