import { Given, When, Then } from '@wdio/cucumber-framework';
import HomePage from '../pageobjects/homePage';
import SearchUnit from '../components/searchUnit';
import Utility from '../utilities/dateTimeUtility'
import HotelSearchResultsPage from '../pageobjects/HotelSearchResultsPage';
import HotelEstabPage from '../pageobjects/HotelEstabPage';

Given(/^I am on hotel Estab page for Estab (.+)$/, async (estabName) => {
await SearchUnit.populateSearchByDestination(estabName);
await HotelEstabPage.waitForHotelEstabPage();
});

Then(/^Per person and total price text should be displayed$/, async () => {
    await HotelEstabPage.validateTotalPriceAndPerPersonPriceText();
});

When(/^I capture Per person and total price$/, async() => {
    await HotelEstabPage.capturePerPersonPrice();
    await HotelEstabPage.captureTotalPrice();
});

Then(/^Per person price should be less then totalprice$/, async() => {
    await HotelEstabPage.capturePerPersonPrice();
    await HotelEstabPage.captureTotalPrice();
	await HotelEstabPage.verifyPerPersonpriceIsLessThenTotalPrice();
});

Then(/^Was and Now Price should be displayed$/, async () => {
    await HotelEstabPage.wasAndNowPriceDisplayed();
});

Then(/^Was price should be greater than now price$/, async() => {
    await HotelEstabPage.captureWasAndNowPrice();
    await HotelEstabPage.verifyNowPriceIsLessThenWasPrice();
});

Then(/^Hotel image should be displayed$/, async () => {
    await expect(await HotelEstabPage.isHotelImageDisplayed()).toEqual(true);
});

Then(/^Hotel should have multiple thumbnail images$/, async () => {
    await expect(await HotelEstabPage.isHotelImageThumbnailDisplayed()).toEqual(true);
});
  
Then(/^Clicking on each image should display corresponding image$/, async () =>{
    await HotelEstabPage.hotelThumbnailImageValidation();
});

When(/^I click on the full screen button$/, async () => {
    await HotelEstabPage.clickOnFullScreen();
});

Then(/^I should be able to scroll through the images$/, async () => {
    await HotelEstabPage.hotelImageNavigationValidation();
});

When(/^I click on the close button$/, async () => {
    await HotelEstabPage.closeFullScreen();
});

Then(/^I should exit fullscreen$/, async () => {
    await HotelEstabPage.verifyFullScreenClosed();
});

Then(/^Hotel name(.+), star ratings, (.+) and (.+) should be displayed$/, async (estab, location, province) => {
    await HotelEstabPage.verifyHotelInfo(estab, location, province);
});

Then(/^Hotel average review Score and total number of reviews should be displayed$/, async() => {
    await HotelEstabPage.verifyHotelReviewOptions();
});

Then(/^Mandatory transfer message (.+) should be displayed in the estab hotel card$/, async(message) =>{
    await HotelEstabPage.verifyReturnTransferMessageInHotelCard(message);
});

Then(/^No reviews message (.+) should be displayed$/, async(message) => {
    await HotelEstabPage.verifyNoReviewsMessage(message);
});

Then(/^Price includes text, Party overview, Board type, Cancellation Information, Local charges should be displayed$/, async () =>{
    await HotelEstabPage.verifyPriceIncludeSection();
  });

Then(/^Offer information should be displayed$/, async () => {
    await HotelEstabPage.hotelOfferInformationDisplayed();
});

Then(/^Offer information should not be displayed$/, async () => {
    await HotelEstabPage.hotelOfferInformationNotDisplayed();
});

Then(/^Hotel Card should be displayed with No Image icon$/, async () => {
    await HotelEstabPage.noImageIconValidation();
});