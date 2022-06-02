import { Given, When, Then } from '@wdio/cucumber-framework';
import homePage from '../pageobjects/homePage';
import LandingPage from '../pageobjects/landingPage';
import { HolidaysLandingPageUrl } from '../utilities/constants';


Given(/^I am on holiday landing page$/, async ()=> {
    await homePage.goToUrl(HolidaysLandingPageUrl);
});

Then(/^Landing page is displayed with intro text widget header and body text$/, async ()=> {
    await LandingPage.introTextWidgetValidation();
});

Then(/^More link should be displayed and enabled$/, async () => {
    await LandingPage.verifyMoreLink();
});

When(/^I click on more link$/, async () => {
    await LandingPage.clickMoreLink();
});

Then(/^Intro Text should get enlarged$/, async () => {
    await LandingPage.verifyIntroTextEnlarged();
});

Then(/^Less link should be displayed and enabled$/, async () => {
    await LandingPage.verifyLessLink();
});

When(/^I click on less$/, async () => {
    await LandingPage.clickLessLink();
});

Then(/^Intro text should be collapsed$/, async () => {
    await LandingPage.verifyIntroTextCollapsed();
});

Then(/^Landing page is displayed with things to do widget header and body text$/, async () => {
    await LandingPage.thingsToDoWidgetValidation();
});

Then(/^Tabs, images and image text should be displayed$/, async() => {
    await LandingPage.thingsToDoWidgetContentValidation();
});

Then(/^Images are not clickable$/, async () => {
    await LandingPage.verifyImageNotClickable();
});

Then(/^Header and body text are not clickable$/, async() => {
    await LandingPage.verifyTextNotClickable();
});

When(/^I select any tab$/, async() => {
    await LandingPage.selectAnyTab();
});

Then(/^Selected Tab should be highlighted$/, async() => {
    await LandingPage.verifyTabHighlighted();
});

Then(/^Tab image and text are displayed$/, async() => {
    await LandingPage.tabImageTextValidation();
});

When(/^I click on next$/,async () => {
    await LandingPage.selectAnyTab();
    await LandingPage.navigateImageRight();
});

When(/^I click on previous$/, async () => {
    await LandingPage.navigateImageleft();
});

Then(/^Next image should be displayed$/, async() => {
    
});

Then(/^Previous image should be displayed$/, async() => {
    
});