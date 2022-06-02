import Page from './basePage'


export class ReviewsTabPage extends Page {

    private get reviewsTab() { return $("button#hotel-tab2"); }
    private get showMoreReviewsButton() { return $("//span[text()='Show More Reviews']"); }
    private get noReviewsText() { return $("//div[@id='hotel-tab2']//div//div"); }
    private get noOfReviews() { return $$("//div[@id='hotel-tab2']//div[contains(@id,'honey')]") }
    private get reviewText() { return $("//div[contains(@id,'honey')]") }
    private get checkInDate() { return $("//div[@id='hotel-tab2']//p[contains(@class,'TextBase')]") }
    private get averageReviewScore() { return $("//div[@id='hotel-tab2']//span[contains(@class,'Badge')]") }
    private get nameAndLocation() { return $("//div[@id='hotel-tab2']//h4[contains(@class,'Heading')]") }
    private get reviewScoreExpressionImage() { return $("//div[@id='hotel-tab2']//*[contains(@class,'SvgIcon')]") }
    private get showWholeReviewLink() { return $("//div[@id='hotel-tab2']//span[text()='Show whole review...']") }
    private get showLessLink() { return $("//span[text()='Show less']") }


    async clickReviewsTab() {
        await this.reviewsTab.click();
    }

    async numberOfReviews() {
        var reviewsCount = await this.noOfReviews;
        return reviewsCount;
    }

    async verifyShowMoreReviewsButtonIsDisplayed() {
        await this.showMoreReviewsButton.waitForDisplayed();
        await expect(this.showMoreReviewsButton).toBeDisplayed({ message: "Show more button is not displyed" });
    }

    async clickShowMoreReviewsButton() {
        await this.showMoreReviewsButton.click();
    }

    async getNoReviewsText() {
        return await this.noReviewsText;
    }

    async VerifyReviewerDetails() {
        await expect(this.nameAndLocation).toBePresent({ message: "Name and Location is not displyed" });
        await expect(this.checkInDate).toBePresent({ message: "Checkin Date is not displyed" });
        // Add not null check validation
    }

    async verifyShowMoreReviewButtonNotDisplayed() {
        await expect(this.showMoreReviewsButton).not.toBeDisplayed()
    }

    async VerifyDisplayOfReviewInfo() {
        await expect(this.reviewText).toBePresent({ message: "Review text is not displyed" });
    }

    async verifyDisplayOfAvgReviewsScore() {
        await expect(this.averageReviewScore).toBeDisplayed({ message: "Review score is not displyed" });
    }

    async verifyDisplayOfReviewsImage() {
        await expect(this.reviewScoreExpressionImage).toBeDisplayed({ message: "Review score is not displyed" });
    }

    async LoadMoreReviewsUntilShowWholeReviewLinkIsDisplayedAndClick() {
        while (await !this.showWholeReviewLink.isDisplayed() && await (await this.showMoreReviewsButton).isDisplayed() == true) {
            await this.showMoreReviewsButton.click();
        }
        await this.showWholeReviewLink.waitForDisplayed();
        await this.showWholeReviewLink.click();
    }

    async clickShowLessLink() {
        await this.showLessLink.click();
    }

    async verifyShowLessLinkDisplayed() {
        await expect(this.showLessLink).toBeDisplayed({ message: "Show more link is not displyed" });
        await expect(await(await this.showLessLink).getAttribute("aria-expanded")).toEqual("true");
    }

    async verifyShowWholeReviewLinkDisplayed() {
        await expect(this.showWholeReviewLink).toBeDisplayed({ message: "Show whole review link is not displyed" });
        await expect(await(await this.showWholeReviewLink).getAttribute("aria-expanded")).toEqual("false");

    }
}

export default new ReviewsTabPage;
