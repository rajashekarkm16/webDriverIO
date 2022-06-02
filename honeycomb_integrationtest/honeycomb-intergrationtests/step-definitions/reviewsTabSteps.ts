import { Given, When, Then } from '@wdio/cucumber-framework';
import ReviewsTab from '../pageobjects/reviewsTabPage';
import { ReviewsDisplayedPerLoad } from '../utilities/constants';


When(/^I click on the reviews tab$/, async () => {
    await ReviewsTab.clickReviewsTab();
});

Then(/^Default reviews should be displayed$/, async () => {
    await expect(await ReviewsTab.numberOfReviews()).toBeElementsArrayOfSize({ eq: ReviewsDisplayedPerLoad })

});

Then(/^Show more reviews button should be available$/, async () => {
    await ReviewsTab.verifyShowMoreReviewsButtonIsDisplayed();
});

When(/^I click on show more reviews button$/, async () => {
    await ReviewsTab.clickShowMoreReviewsButton();
});

Then(/^Next set of reviews should be displayed underneath the previous reviews$/, async () => {
    await expect(await ReviewsTab.numberOfReviews()).toBeElementsArrayOfSize({ eq: 2 * Number(ReviewsDisplayedPerLoad) })
});

Then(/^Show more reviews button should be displayed$/, async () => {
    await ReviewsTab.verifyShowMoreReviewsButtonIsDisplayed();
});


Then(/^show grey box with the (.+)$/, async (message) => {
    await expect(await ReviewsTab.getNoReviewsText()).toHaveText(message)
});

Then(/^Name, location and check in date of the customer should be displayed$/, async () => {
    await ReviewsTab.VerifyReviewerDetails();
});

Then(/^Show more review button should not be displayed$/, async () => {
    await ReviewsTab.verifyShowMoreReviewButtonNotDisplayed();
});

Then(/^Review information should be displayed$/, async () => {
    await ReviewsTab.VerifyDisplayOfReviewInfo();
});

Then(/^Average review score should be displayed along with the review content$/, async () => {
    await ReviewsTab.verifyDisplayOfAvgReviewsScore();
});

Then(/^Relevant icon should be displayed$/, async () => {
    await ReviewsTab.verifyDisplayOfReviewsImage();
});

When(/^I click on show whole review link$/, async () => {
    await ReviewsTab.LoadMoreReviewsUntilShowWholeReviewLinkIsDisplayedAndClick();
});

When(/^I click on show less link$/, async () => {
    await ReviewsTab.clickShowLessLink();
});

Then(/^Review content should expand with show less link$/, async () => {
    await ReviewsTab.verifyShowLessLinkDisplayed()
});

Then(/^Review content should collapse with show whole review link$/, async () => {
    await ReviewsTab.verifyShowWholeReviewLinkDisplayed();
});

