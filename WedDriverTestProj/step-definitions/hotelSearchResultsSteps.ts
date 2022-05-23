import { Given, When, Then } from '@wdio/cucumber-framework';
import SearchUnit from '../components/searchUnit';
import HotelSearchResultsPage from '../pageobjects/HotelSearchResultsPage';
import FilterDetails from '../modals/FilterDetails';


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
    await expect(numReview>0).toEqual(true);
});    

Then(/^Smiley icon is displayed$/, async () => {
    await expect(await HotelSearchResultsPage.isSmileyIconExist(1)).toEqual(true);

});

Then(/^Hotel card should have secure today pill with message (.+)$/, async (Message) => {
    await HotelSearchResultsPage.waitForPageLoad();
    const pillTodayText = await HotelSearchResultsPage.getSecureTodayPillText(1);
    await expect(pillTodayText).toEqual(Message);
});

Then(/^Price summary information should be displayed$/, async() => {
	await expect(await HotelSearchResultsPage.isPriceSummarySectionDisplayed()).toEqual(true);
});

Then(/^\'(.*)\' and per person should be displayed in brackets$/, async(PerPersonText) => {
    await HotelSearchResultsPage.verifyPerPriceInsideBracket(PerPersonText);
    await expect(await HotelSearchResultsPage.isPerPersonPriceDisplayed()).toEqual(true);
});

Then(/^\'(.*)\' and total price should be displayed$/, async(TotalPriceText) => {
	await expect(await HotelSearchResultsPage.getTotalPriceText()).toEqual(TotalPriceText);
    await expect(await HotelSearchResultsPage.isTotalPriceDisplayed()).toEqual(true);
});

When(/^I land on hotel search results page$/, async() => {
	await HotelSearchResultsPage.waitForPageLoad();
});

Then(/^\'(.*)\' should be displayed$/, async(TotalPriceText) => {
	await expect(await HotelSearchResultsPage.getTotalPriceText()).toEqual(TotalPriceText);
});

Then(/^Was price and Now Price should be displayed$/, async() => {
	await HotelSearchResultsPage.waitForPageLoad();
    await HotelSearchResultsPage.isHotelCardWithOldPriceAvailalble();
    await HotelSearchResultsPage.clickOnHotelName();
});

//#region (Left Nav Filter)

Given(/^Hotel search result page is open for destination \'(.*)\'$/, async (destination) => {

Then(/^I see hotel has local charges displayed$/, async (LocalChargesText) => {
	await HotelSearchResultsPage.verifyPriceIncludeListContains(LocalChargesText);
});


    if (FilterDetails.HotelSearchResultPageUrl == undefined || FilterDetails.HotelSearchResultPageUrl === '') {
        await SearchUnit.open("homePage");
        await SearchUnit.selectTab("HOTEL ONLY")
        await SearchUnit.selectDestination(destination)
        await SearchUnit.populateDate(30, 2);      
        await SearchUnit.clickonSearchButton();
    }
    else {
        await browser.navigateTo(FilterDetails.HotelSearchResultPageUrl);
    }

    await HotelSearchResultsPage.waitForPageLoad();
    FilterDetails.HotelSearchResultPageUrl = await browser.getUrl();

});

Then(/^All filter items should be unchecked for \'(.*)\'$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(await filterName);
    FilterDetails.FilterItems= allFilterItems;
    await HotelSearchResultsPage.verifyAllUncheckedFilterItem(await filterName, allFilterItems);
});

Then(/^Each Hotel card label should have \'(.*)\' listed in the filter$/, async (filterName) => {
    await HotelSearchResultsPage.verifyFilterLabelonEachHotelCard(await filterName,FilterDetails.FilterItems);
});

Then(/^I check one by one \'(.*)\' filter and verify hotel count and each hotel has selected filter$/, async (filterName) => {
    var allFilterItems = await HotelSearchResultsPage.getAllFilterItems(await filterName);
    await HotelSearchResultsPage.verifyFilterLabelOnHotelCardAfterSingleFilterSelection(await filterName,allFilterItems);
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
    await HotelSearchResultsPage.toggleFilter(await filterName,false)
});

Then(/^Filter \'(.*)\' should be collapsed$/, async (filterName) => {
    expect(await HotelSearchResultsPage.isFilterExpanded(await filterName)).toBe("false");
});

Then(/^Filter item should not be displayed for \'(.*)\'$/, async (filterName) => {
    switch(await filterName)
    {
        case "Star Rating":
            await HotelSearchResultsPage.veriftStarRatingIsNotDisplayed();
            break;
        default:
            await HotelSearchResultsPage.verifyFilterItemIsNotDisplayed(await filterName);
            break;
    }    
});

When(/^I expand \'(.*)\' filter$/, async (filterName) => {
    await HotelSearchResultsPage.toggleFilter(await filterName,true)
});

When(/^I check first filter for \'(.*)\'$/, async (filterName) => {
    var filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(await filterName);
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterDisabledItems = await filterItems[1];
    await HotelSearchResultsPage.checkFilterItem(await filterName,FilterDetails.FilterEnabledItems[0]);
});

When(/^I refresh the page$/, async () => {
    await browser.refresh();
    await HotelSearchResultsPage.waitForSearchResultsToLoad();
});

Then(/^first filter should be checked for \'(.*)\'$/, async (filterName) => {
    await HotelSearchResultsPage.verifyCheckedFilterItem(await filterName,FilterDetails.FilterEnabledItems[0]);
});

When(/^I select lowest star rating filter$/, async () => {
    var filterItems = await HotelSearchResultsPage.getAllStarRating();
    FilterDetails.FilterItems = await filterItems[0];
    await HotelSearchResultsPage.selectStarRating(await filterItems[0]);
});

When(/^I check first filter of \'(.*)\' and verify disabled filter items of \'(.*)\'$/, async (filterName :string,secondFilter:string) => {
    var firstFilterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    FilterDetails.FilterItems = await firstFilterItems[0];
    var secondFilterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(secondFilter);
    FilterDetails.FilterEnabledItems = await secondFilterItems[0];
    FilterDetails.FilterDisabledItems = await secondFilterItems[1];
    await HotelSearchResultsPage.checkFilterItem(filterName,FilterDetails.FilterItems[0]);
});

When(/^I uncheck first filter of \'(.*)\'$/, async (filterName:string) => {
    await HotelSearchResultsPage.uncheckFilterItem(filterName,FilterDetails.FilterItems[0]);
});

Then(/^lowest star rating filter should be selected$/, async () => {
    await HotelSearchResultsPage.verifySelectedStarRating(FilterDetails.FilterItems[0])
});

Then(/^All star should be unselected$/, async () => {
    var allFilterItems = await HotelSearchResultsPage.getAllStarRating();
    FilterDetails.FilterItems= allFilterItems;
    await HotelSearchResultsPage.verifyAllStarRatingIsUnSelected(allFilterItems);
});

Then(/^I select one by one star rating filter and verify hotel count and each hotel card has selected star rating$/, async () => {
    var allFilterItems = await HotelSearchResultsPage.getAllStarRating();
    await HotelSearchResultsPage.verifyStarRatingOnHotelCardAfterSingleSelection('Star Rating',allFilterItems);
});

Then(/^I select multiple star rating filter and verify hotel count and each hotel card has selected star rating$/, async () => {
    var allFilterItems = await HotelSearchResultsPage.getAllStarRating();
    FilterDetails.FilterItems = allFilterItems;
    await HotelSearchResultsPage.verifyStarRatingOnHotelCardAfterMutipleSelection('Star Rating',allFilterItems);
});

Then(/^I unselect one by one all star rating and verify hotel count and each hotel card has selected star rating$/, async () => {
    await HotelSearchResultsPage.verifyStarRatingOnHotelCardAfterUnSelecting('Star Rating',FilterDetails.FilterItems);
});

Then(/^Disabled \'(.*)\' filter label should not be displayed for each hotel card$/, async (filterName:string) => {
    var filterItems:any;
    if(filterName.toString() =='Star Rating')
    {
         filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
    }
    else{
         filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    }    
    var disabledFilterItems = await filterItems[1];
    FilterDetails.FilterEnabledItems = await filterItems[0];
    FilterDetails.FilterDisabledItems = await filterItems[1];
    if(await disabledFilterItems.length >0)
    {
        await HotelSearchResultsPage.verifyFilterLabelonEachHotelCard(filterName,FilterDetails.FilterEnabledItems);
    }
});

When(/^I unselect lowest star rating filter$/, async () => {
    await HotelSearchResultsPage.unSelectStarRating(FilterDetails.FilterItems[0]);
});

Then(/^Disabled filter count should be 0 for \'(.*)\' filter$/, async (filterName:string) => {
    var filterItems : any;
    if(filterName.toLowerCase() ==='star rating')
    {
        filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledStarRating();
    }
    else{
         filterItems = await HotelSearchResultsPage.getAllEnabledAndDisabledFilterItems(filterName);
    }
    var disabledFilterItems = await filterItems[1];
    expect(Number(await disabledFilterItems.length)).toEqual(0);
});

When(/^I check \'(.*)\' filter which was disbaled after (?:.*?) (?:.*?) filter selection$/, async (filterName) => {
    if(FilterDetails.FilterDisabledItems.length>0)
    await HotelSearchResultsPage.checkFilterItem(await filterName,FilterDetails.FilterDisabledItems[0]);
});

Then(/^lowest star rating filter should be disabled$/, async () => {
    await HotelSearchResultsPage.verifyDisabledStarRating(FilterDetails.FilterItems[0]);
});

Then(/^first filter of \'(.*)\' should be disabled$/, async (filterName:string) => {
    await HotelSearchResultsPage.verifyDisabledFilterItem(await filterName,FilterDetails.FilterItems[0]);
});

When(/^I uncheck \'(.*)\' filter which was disbaled after (?:.*?) (?:.*?) filter selection$/, async (filterName) => {
    if(FilterDetails.FilterDisabledItems.length>0)
    await HotelSearchResultsPage.uncheckFilterItem(await filterName,FilterDetails.FilterDisabledItems[0]);
});

//#region [Total Price Filter]

Then(/^Default Minimum Value and Maximum Value on Price range bar is displayed$/, async() => {
	await HotelSearchResultsPage.isDefaultPriceSliderDisplayed();
    await HotelSearchResultsPage.verifyFilteredMinAndMaxPriceValueDisplayed();
});

Then(/^Price Range Bar and Price Range Indicator should be set to default price$/, async() => {
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

Then(/^I should see hotels in filtered price range$/, async() => {
	await HotelSearchResultsPage.verifyFilteredPriceRange();
});

Then(/^Other sub categories to be gray out on price range bar$/, async() => {
	await HotelSearchResultsPage.verifyReadOnlyPriceSliderBar();
});

Then(/^Price values should be different at starting and ending points of Price range bar$/, async () => {
	await HotelSearchResultsPage.verifySliderMinAndMaxPriceValueDisplayed();
});

Then(/^The Price value on starting and ending points of price range bar is displayed$/, async () => {
	await HotelSearchResultsPage.verifySliderMinAndMaxPriceValueDisplayed();
});

Then(/^Price values changes in Price Range Indicator at starting and ending points$/, async() => {
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

Then(/^Slider bar should be read only for irrelevant filter$/, async() => {
	await HotelSearchResultsPage.verifyReadOnlyRatingSliderBar();
});

Then(/^On bottom of Rating Range Bar the Rating value as \'(.+)\' and \'(.+)\' is displayed$/, async (MinRating, MaxRating) => {
	await HotelSearchResultsPage.verifyMinAndMaxSliderCustomerRating(MinRating, MaxRating);
});

//#endregion

//#endregion (Left Nav)


//#region [Sort By]

When(/^I open sort options$/, async() => {
	await HotelSearchResultsPage.openSortByModal();
});


Then(/^Close sort modal Icon should be displayed$/, async() => {
	await HotelSearchResultsPage.isSortByModalOpened();
});


Then(/^Sort by \'(.*)\' should be checked$/, async(sortOption:string) => {
	await HotelSearchResultsPage.verifySortByChecked(sortOption)
});

Then(/^Sort modal should display all sort options$/, async() => {
	await HotelSearchResultsPage.VerifySortByOptionsDisplayed()
});


When(/^I select (.+) sort by option$/, async(sortOption:string) => {
	await HotelSearchResultsPage.selectSortBy(sortOption);    
});


Then(/^Selected sort by (.+) should be displayed$/, async (selectedSortOption:string) => {
	await HotelSearchResultsPage.validateSortBySelectedText(selectedSortOption);
});


Then(/^Sort criteria (.+) added to query url$/, async(queryOption:string) => {
	await HotelSearchResultsPage.validateSortCriteriaAddedToQueryURL(queryOption);
});


Then(/^Search results are sorted based on the selected (.+)$/, async(selectedSortOption:string) => {
	await HotelSearchResultsPage.verifyAppliedSortOption(selectedSortOption);
});




//#endregion[Sort By]