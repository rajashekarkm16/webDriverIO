import { Given, When, Then } from '@wdio/cucumber-framework';
import SearchUnit from '../components/searchUnit';
import HotelSearchResultsPage from '../pageobjects/HotelSearchResultsPage';

Given(/^I am on hotel search result page for destination \'(.+)\'$/, async (Destination) => {
    await SearchUnit.populateSearchByDestination(Destination);
});

Given(/^I am on hotels search result page for destination \'(.*)\', checkin date as  \'(.*)\' duration \'(.*)\' GuestInfo \'(.*)\'$/, async (Destination, CheckinDate, DurationOfDays, GuestsData) => {
    await SearchUnit.populateSearch(Destination, CheckinDate, DurationOfDays, GuestsData);
});

Then(/^I should land on the hotel search result page$/, async () => {
    await HotelSearchResultsPage.waitForPageLoad();
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



