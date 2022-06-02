import { Given, When, Then } from '@wdio/cucumber-framework';
import GuestInformation from '../pageobjects/guestInformationPage';



Given(/^I am on Guest information page with tripId \'(.+)\'$/, async(tripId) => {
	await GuestInformation.NavigateToGuestInformationPage(tripId)
});

When(/^I enter email (.+)$/, async(emailId) => {
	await GuestInformation.enterEmail(emailId);
});

Then(/^EmailMe invalid error message should be displayed$/, async() => {
	await GuestInformation.validateEmailMeErroMessage()
});

Then(/^EmailMe this hotel success message should be displayed (.+)$/, async(emailId) => {
	await GuestInformation.validateEmailMeSuccessMessage(emailId);
});

When(/^I click on Send EmailMe$/, async() => {
	await GuestInformation.sendEmailMeQuote()
    await GuestInformation.waitForEmailMeToSend()
});

When(/^I select checkbox of Send me the best deals and holiday inspiration$/, async() => {
	await GuestInformation.clickSendBestDeals()
});

Then(/^I should see send me best deals checked$/, async() => {
	await GuestInformation.VerifySendMeBestDealsChecked()
});

Then(/^Choose a payment header should be (.+)$/, async (paymentHeader) => {
	await GuestInformation.verifyPaymentHeading(paymentHeader);
});

Then(/^Payment options should be displayed$/, async() => {
	await GuestInformation.verifyPaymentOptionsDisplayed();
});

Then(/^\'(.+)\' payment option should be selected$/, async(paymentOption) => {
	//await GuestInformation.verifyPaymentOptionisSelected(paymentOption);
});

Then(/^Payment terms and conditions should not be displayed$/, async() => {
	await GuestInformation.verifyPaymentTermsAndConditionsNotDisplayed()
});

When(/^I choose payment option as \'(.+)\'$/, async (paymentOption) => {
	//await GuestInformation.choosePaymentOption(paymentOption);
});

Then(/^Payment terms and conditions should be displayed$/, async() => {
	await GuestInformation.verifyPaymentTermsAndConditionsDisplayed()
});


















