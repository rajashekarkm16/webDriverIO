import { OperationCanceledException, textChangeRangeIsUnchanged } from 'typescript';
import { } from 'webdriverio';
import Page from '../pageobjects/basePage';
import HotelCardDetails from '../modals/hotelCardDetails';
import FilterDetails from '../modals/filterDetails';
import { SearchResultsDisplayedPerLoad, SortByOptions } from '../utilities/constants';

class HotelSearchResultsPage extends Page {

    //#region  [Getter/Setters]

    //#region [Breadcrumb]

    get breadCrumb() { return $("//ol[contains(@class,'Breadcrumb')]"); }
    get breadCrumbList() { return $$("//ol[contains(@class,'Breadcrumb')]/li"); }

    //#endregion

    //#region [Filter]

    //private filterSlider(index:number =1){ return $("(//span[@role='slider'])["+index+"]"); }

    // private customerRatingDefault()
    // {
    //     return $("//div[@id='slider-range-filter-ratingRange-content']//p[@class='css-1v9qwus-Body'][1]");
    // }
    private totalPriceSliderButton(index: number = 1) { return $("//div[@id='slider-range-filter-priceRange-content']//span[@role='slider'][" + index + "]"); }
    private totalPriceFilterButtonArrow(indexSlider: number = 1, indexArrow: number = 1) { return $("(//span[@role='slider'])[" + indexSlider + "]/span[contains(@class,'Slider')][" + indexArrow + "]"); }//
    private filteredPriceRange() { return $("//div[@id='slider-range-filter-priceRange-content']//div[contains(@class,'Stacker')]"); }//div[contains(@class,'StyleUtility')][1]
    private filteredPriceMinValue() { return $("//div[@id='slider-range-filter-priceRange-content']/div//p[1]"); }
    private filteredPriceMaxValue() { return $("//div[@id='slider-range-filter-priceRange-content']/div//p[3]"); }
    private priceSliderMinValue() { return $("//div[@id='slider-range-filter-priceRange-content']//span[@aria-hidden='true'][1]"); }
    private priceSliderMaxValue() { return $("//div[@id='slider-range-filter-priceRange-content']//span[@aria-hidden='true'][2]"); }
    private priceSliderGreySlider() { return $("//div[@id='slider-range-filter-priceRange-content']/div[2]//span[@role='presentation']/span[1]") }
    private get totalPriceSlider() { return $("//*[contains(@id,'priceRange')]//span[contains(@class,'Slider')]"); }

    private customerRatingSliderButton(index: number = 1) { return $("//div[@id='slider-range-filter-ratingRange-content']//span[@role='slider'][" + index + "]"); }
    private customerRatingRange() { return $("//div[@id='slider-range-filter-ratingRange-content']//div[contains(@class,'Stacker')]"); }
    private customerRatingMinValue() { return $("//div[@id='slider-range-filter-ratingRange-content']/div[contains(@class,'StyleUtility')][1]//p[1]"); }
    private customerRatingMaxValue() { return $("//div[@id='slider-range-filter-ratingRange-content']/div[contains(@class,'StyleUtility')][1]//p[3]"); }
    private custRatingSliderMinValue() { return $("//div[@id='slider-range-filter-ratingRange-content']//span[@aria-hidden='true'][1]"); }
    private custRatingSliderMaxValue() { return $("//div[@id='slider-range-filter-ratingRange-content']//span[@aria-hidden='true'][2]"); }
    private custRatingGreySlider() { return $("//div[@id='slider-range-filter-ratingRange-content']/div[2]//span[@role='presentation']/span[1]") }
    private get customerRatingSlider() { return $("//*[contains(@id,'ratingRange')]//span[@role='presentation']"); }

    //#endregion 

    //#region [HotelCard]

    private hotelName(index: number = 1) { return $("(//article)[" + index + "]//h4/a"); }
    private starRating(index: number = 1) { return $$("(//article)[" + index + "]//span[contains(@class,'StarRating')]/*[local-name()='svg']"); }
    private address(index: number = 1) { return $("(//article)[" + index + "]//div[@class='css-vzegvy-Stacker']/div[2]//h3"); }
    private customerRating(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'FlexGrid')]/span[contains(@class,'Badge')]"); }
    private smileyIcon(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'FlexGrid')]/*[local-name()='svg']"); }
    private customerReviewsCount(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'FlexGrid')]/p[contains(@class,'Body')]"); }
    private secureTodayForPill(index: number = 1) { return $("(//article)[" + index + "]//div[span[contains(@class,'Pill')]]/span/p"); }
    private totalPrice(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'CardContent')][2]//div[contains(@class,'Heading')]"); }
    private oldTotalPrice(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'Stacker')]/div[contains(@class,'Stacker')]/div[1]"); }
    private newTotalPrice(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'Stacker')]/div[contains(@class,'Stacker')]/div[2]"); }
    private perPersonPrice(index: number = 1) { return $("(//article)[" + index + "]//strong"); }
    private holidayPriceFromText(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'CardContent')][2]//div[contains(@class,'Stacker')]//p[1]"); }
    private perPersonPriceFromText(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'CardContent')][2]//div[contains(@class,'Stacker')]//p[2]"); }
    private viewMoreDetails(index: number = 1) { return $("(//article)[" + index + "]//div[contains(@class,'CardContent')][2]//a"); }
    private priceIncludesText(index: number = 1) { return $("(//article)[" + index + "]//p[@class='css-1oe1yf0-Body']"); }
    private listOfPriceIncludes(index: number = 1) { return $$("(//article)[" + index + "]//ul/li//p"); }
    private priceSummarySection(index: number = 1) { return $("(//article)[1]//div[contains(@class,'CardContent')][2]"); }
    private get showMoreHotels() { return $("//span[text()='Show More Hotels']"); }

    // tabIndex for enable/disabled
    private imageGalleryLeftButton(index: number = 1) { return $("(//article)[" + index + "]//div[@class='image-gallery-slide-wrapper bottom']//button[1]"); }
    private imageGalleryRightButton(index: number = 1) { return $("(//article)[" + index + "]//div[@class='image-gallery-slide-wrapper bottom']//button[2]"); }
    private imageGalleryPositionText(index: number = 1) { return $("(//article)[" + index + "]//div[@class='image-gallery-slide-wrapper bottom']/div/div"); }
    private hotelPill() { return $("(//article)[1]//div[contains(@class, 'Gallery')]//span[contains(@class,'Pill')]"); }
    private hotelPillGallery() { return $("(//article)[1]//div[contains(@class, 'Gallery')]//span"); }
    private get listOfHotelCards() { return $$("//article"); }
    private get listOfAllStarsOnCard() { return $$("//*[contains(@class,'-StarRating')]"); }
    private get listOfBoardTypesFromHotelCards() { return $$("//*[@class='css-1oyikxh-InfoItem']"); }
    private get listOfSponsoredpPills() { return $$("//article//div[contains(@class,'Pill')]//span[contains(text(),'Sponsored')]"); }
    //#endregion

    //#region [Filter locator]

    private async listOfAllFilterItemsByType(filterName: string) {
        return await $$("//p[text()='" + filterName + "']/parent::*[contains(@id,'-filter-')]/..//input[@type='checkbox']/../../div[2]");
    }
    private async filterHeaderByType(filterName: string) {
        return await $("//p[text()='" + filterName + "']/parent::*[contains(@id,'-filter-')]");
    }

    private async filterItemByType(filterName: string, filterItem: string) {
        return await $("//*[contains(@id,'-filter-')]//p[text()='" + filterName + "']/../..//*[text()=\"" + filterItem + "\"]/..//input");
    }

    private async starRatingFilterItem(totalStar: string) {
        return await $("//*[contains(@id,'filter-stars')]//input[@name='" + totalStar + "']");
    }
    private get loaderIcon() { return $("//div[@data-bone='true']"); }
    // private get loaderIcon() { return $("(//div[@data-bone='true'])[2]"); }
    private get filterButton() { return $("//span[text()='Filter']"); }
    private get filtersModal() { return $("//*[text()='Filter Results']"); }
    private get resetFiltersButton() { return $("//span[text()='Reset filters']/.."); }
    private get filterSkeletonLoader() { return $("//*[contains(@class,'xgtcyu-FlexGrid')]//div[contains(@class,'Skeleton')]"); }



    private async listOfAllStarRatingItemsInFilter() {
        return await $$("//*[contains(@id,'filter-stars')]//input");
    }

    //#endregion [Filter locator]

    //#region [SortBY locator]
    private get sortByDailogTitle() { return $("div[aria-labelledby='sort-dialog-title'] h2"); }
    private get closeSortBy() { return $("div[aria-labelledby='sort-dialog-title'] button"); }
    private get sortByOptions() { return $$("//div[@aria-labelledby='sort-dialog-title']//div[contains(@class,'ModalContent')]//div[contains(@class,'Body')]"); }
    private async sortByCheckBox(sortOption) {
        return await $("//div[@aria-labelledby='sort-dialog-title']//input[contains(@value,'" + sortOption + "')]");
    }
    private get selectedSortBy() { return $("//*[text()='Sort by:']/parent::div/a"); }


    //#endregion [SortBy locator]

    //#endregion

    //#region  [Methods]

    //#region [SortBy Methods]
    async verifySortByModalOpened() {
        await expect(await this.closeSortBy).toBeDisplayed({ message: "SortBy modal is not opened" });
    }
    async validateSortBySelectedText(selectedSortByOption: string) {
        await expect(await this.selectedSortBy).toBeDisplayed({ message: "SortBy Text is not displayed" })
        await expect(await this.selectedSortBy).toHaveText(selectedSortByOption, { message: selectedSortByOption + " : selected SortBy Text is not matched" })
    }

    async validateSortCriteriaAddedToQueryURL(sortQueryOption: string) {
        await expect(browser).toHaveUrlContaining(sortQueryOption, { message: sortQueryOption + " : selected SortByQuery in URL is not matched" })
    }

    async selectSortBy(sortbyOption: string) {
        if (!await (await this.closeSortBy).isDisplayed)
            await this.openSortByModal()
        await (await this.closeSortBy).waitForDisplayed()
        await (await this.sortByCheckBox(sortbyOption)).click()
    }

    async VerifySortByOptionsDisplayed() {
        for (let sortByOption = 0; sortByOption < await this.sortByOptions.length; sortByOption++) {
            await expect(await SortByOptions.options).toContain(await this.sortByOptions[sortByOption].getText())
        }
    }

    async openSortByModal() {
        await (await this.selectedSortBy).waitForDisplayed({ timeoutMsg: "SortBy Link is not displyed in Search result page" })
        await await (this.selectedSortBy).scrollIntoView();
        await (await this.selectedSortBy).click();
    }
    async closeSortByModal() {
        await (await this.selectedSortBy).waitForDisplayed({ timeoutMsg: 'close sortBy Icon is not displyed' })
        await (await this.selectedSortBy).click();
        await (await this.selectedSortBy).waitForDisplayed({ reverse: true })
    }

    async verifySortByChecked(sortByOption) {
        await expect(await this.sortByCheckBox(sortByOption)).toBeChecked({ message: sortByOption + "is not checked " })
    }

    async verifyAppliedSortOption(sortbyOption) {
        await this.waitForSearchResultsToLoad();
        await this.viewAllHotels()
        var hotelCardCount = Number(await this.countOfHotelCard());
        var SponsoredPillcount = Number(await this.countOfSponsoredPills())
        if (sortbyOption == "Price") {
            var listOfHotelPrice: number[] = new Array();
            for (let count = SponsoredPillcount + 1; count <= hotelCardCount; count++) {
                var hotelCardPrice = Number(await this.convertCurrencyToNumber((await (this.totalPrice(count)).getText())));
                listOfHotelPrice.push(hotelCardPrice)
            }
            console.log("Hotel Prices : " + listOfHotelPrice)
            expect(listOfHotelPrice.sort()).toEqual(listOfHotelPrice)
        }
        else if (sortbyOption == "Customer Rating") {
            var listOfCustomerRatings: number[] = new Array();
            for (let count = SponsoredPillcount + 1; count <= hotelCardCount; count++) {
                var isCustomerRatingDisplayed = await (await this.customerRating(count)).isDisplayed()
                if (isCustomerRatingDisplayed) {
                    var CustomerRating = Number(await (this.customerRating(count)).getText())
                    listOfCustomerRatings.push(CustomerRating)
                }
            }
            console.log("Customer Ratings : " + listOfCustomerRatings)
            expect(listOfCustomerRatings.sort().reverse()).toEqual(listOfCustomerRatings)
        }
        else if (sortbyOption == "Star Rating") {
            var listOfStarCountOnEachCard: number[] = new Array();
            for (let count = SponsoredPillcount + 1; count <= hotelCardCount; count++) {
                var starCountonHotelCard = Number(await this.getStarRatingByIndex(count))
                listOfStarCountOnEachCard.push(starCountonHotelCard)
            }
            console.log("Star count on each Hotel Card : " + listOfStarCountOnEachCard)
            expect(listOfStarCountOnEachCard.sort().reverse()).toEqual(listOfStarCountOnEachCard)
        }
    }
    //#endregion [SortBy Methods]


    //#region [Filter-CustomerRating & Total Price]
    async setCustomerRatingRange(minRating: number = 0, maxRating: number = 10) {
        console.log('Minimum Rating : ', typeof minRating, 'Maximum Rating is : ', typeof maxRating)
        const currentMinRatingValue: number = Number(await this.getCustRatingFilteredMinValue());
        const currentMaxRatingValue: number = Number(await this.getCustRatingFilteredMaxValue());
        const diffMinRating: number = Number(minRating) - Number(currentMinRatingValue);
        const diffMaxRating: number = Number(maxRating) - Number(currentMaxRatingValue);
        if ((Number(minRating) < Number(maxRating)) && (currentMinRatingValue < currentMaxRatingValue)) {
            await this.moveCustomerRatingSlider('leftSlider', diffMinRating);
            await this.moveCustomerRatingSlider('rightSlider', diffMaxRating);
        }
        else {
            console.log('Check the parameters')
        }
        const minRatingValueAfterChange = Number(await this.getCustRatingFilteredMinValue());
        const maxRatingValueAfterChange = Number(await this.getCustRatingFilteredMaxValue());
        if (minRatingValueAfterChange == minRating) {
            console.log('Successfully modified min rating to -', minRating);
            console.log('Expected :', minRating, '\nActual :', minRatingValueAfterChange);
        }
        else {
            console.log('Expected :', minRating, '\nActual :', minRatingValueAfterChange);
        }
        if (maxRatingValueAfterChange == maxRating) {
            console.log('Successfully modified max rating to -', maxRating);
            console.log('Expected :', maxRating, '\nActual :', maxRatingValueAfterChange);
        }
        else {
            console.log('Expected :', maxRating, '\nActual :', maxRatingValueAfterChange);
        }
    }
    //relativeXAxis : +ve for right side i.e. increase in min price range
    //relativeXAxis : -ve for right side i.e. decrease in min price range
    async dragLeftPriceSliderByXCoordinates(relativeXAxis: number = 30) {
        await (await this.totalPriceSliderButton(1).dragAndDrop({ x: relativeXAxis, y: 0 }));
    }
    //relativeXAxis : +ve for right side i.e. increase in max price range
    //relativeXAxis : -ve for right side i.e. decrease in max price range
    async dragRightPriceSliderByXCoordinates(relativeXAxis: number = -30) {
        await (await this.totalPriceSliderButton(2).dragAndDrop({ x: relativeXAxis, y: 0 }));
    }


    async dragLeftPriceSlider(incOrDecrPrice: number, incrementType: string, relativeXAxis: number = 30) {

        if ((incOrDecrPrice > 0) && (incOrDecrPrice <= 200) && (incrementType == 'interval')) {
            const INCREMENT_FACTOR = 25; //need to make it global constant
            const incTimes = Number(incOrDecrPrice / INCREMENT_FACTOR);
            for (var i = 0; i < incTimes; i++) {
                await (await this.totalPriceFilterButtonArrow(1, 1)).click();
            }
        }
        else if ((incOrDecrPrice > 0) && (incOrDecrPrice >= 200) && (incrementType == 'interval')) {
            const INCREMENT_FACTOR = 25; //need to make it global constant
            const incTimes = Number(incOrDecrPrice / INCREMENT_FACTOR);
            for (var i = 0; i < incTimes; i++) {
                await (await this.totalPriceFilterButtonArrow(1, 3)).click();
            }
        }
        else if ((incOrDecrPrice > 0) && (incrementType = 'random')) {
            await this.dragLeftPriceSliderByXCoordinates(relativeXAxis);
        }
        else if ((incOrDecrPrice < 0) && (incrementType = 'random')) {
            if (relativeXAxis > 0) {
                relativeXAxis = -1 * relativeXAxis;
            }
            await this.dragLeftPriceSliderByXCoordinates(relativeXAxis);
        }
        else if ((incOrDecrPrice < 0) && (incOrDecrPrice >= -200) && (incrementType == 'interval')) {
            const INCREMENT_FACTOR = 25; //need to make it global constant
            const incTimes = Number(incOrDecrPrice / INCREMENT_FACTOR);
            for (var i = 0; i < incTimes; i++) {
                await (await this.totalPriceFilterButtonArrow(1, 1)).click();
            }
        }
        else {
            console.log("Please check your entry. Please check example.");
        }
    }

    // async dragLeftPriceSliderByValue(increaseMiniPriceValueBy: number){

    //     //get the existing default range
    //     const currentMinFilteredPrice = Number(await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMinValue()));
    //     const currentMaxFilteredPrice = Number(await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMaxValue()));
    //     console.log('Minimum is :', currentMinFilteredPrice, 'Max is : ', currentMaxFilteredPrice)
    //     //iterate range
    //     var price: number=0;
    //     var expectedMinPrice: number = Number(increaseMiniPriceValueBy)+currentMinFilteredPrice;
    //     if(Number(expectedMinPrice)<Number(currentMaxFilteredPrice)){
    //         console.log("Check is success.");
    //     }
    //     console.log('Expected min price :', expectedMinPrice)
    //     while (price < expectedMinPrice){
    //         await browser.pause(1000)
    //         await (await this.totalPriceFilterButtonArrow(1, 1)).click();
    //         const minFilteredPrice = Number(await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMinValue()));
    //         price =price+minFilteredPrice;
    //         console.log('Min price reached :', price)
    //     }
    //     //
    //     //
    //     //


    //     // if((incOrDecrPrice>0)&&(incOrDecrPrice<=200)&&(incrementType=='interval')){
    //     //     const INCREMENT_FACTOR=25; //need to make it global constant
    //     //     const incTimes = Number(incOrDecrPrice/INCREMENT_FACTOR);
    //     //     for(var i=0;i<incTimes;i++){
    //     //         await (await this.totalPriceFilterButtonArrow(2, 1)).click();
    //     //     }
    //     // }
    // }

    async dragRightPriceSlider(incOrDecrPrice: number, incrementType: string, relativeXAxis: number = 30) {

        if ((incOrDecrPrice > 0) && (incOrDecrPrice <= 200) && (incrementType == 'interval')) {
            const INCREMENT_FACTOR = 25; //need to make it global constant
            const incTimes = Number(incOrDecrPrice / INCREMENT_FACTOR);
            for (var i = 0; i < incTimes; i++) {
                await (await this.totalPriceFilterButtonArrow(2, 1)).click();
            }
        }
        else if ((incOrDecrPrice > 0) && (incOrDecrPrice >= 200) && (incrementType == 'interval')) {
            const INCREMENT_FACTOR = 25; //need to make it global constant
            const incTimes = Number(incOrDecrPrice / INCREMENT_FACTOR);
            for (var i = 0; i < incTimes; i++) {
                await (await this.totalPriceFilterButtonArrow(2, 3)).click();
            }
        }
        else if ((incOrDecrPrice > 0) && (incrementType = 'random')) {
            await this.dragRightPriceSliderByXCoordinates(relativeXAxis);
        }
        else if ((incOrDecrPrice < 0) && (incrementType = 'random')) {
            if (relativeXAxis > 0) {
                relativeXAxis = -1 * relativeXAxis;
            }
            await this.dragRightPriceSliderByXCoordinates(relativeXAxis);
        }
        else if ((incOrDecrPrice < 0) && (incOrDecrPrice >= -200) && (incrementType == 'interval')) {
            const INCREMENT_FACTOR = 25; //need to make it global constant
            const incTimes = Number(incOrDecrPrice / INCREMENT_FACTOR);
            for (var i = 0; i < incTimes; i++) {
                await (await this.totalPriceFilterButtonArrow(2, 1)).click();
            }
        }
        else {
            console.log("Please check your entry. Please check example.");
        }
    }

    async moveTotalPriceSlider(sliderType: string, toDirection: string, priceAmount: string, relativeXAxis: number = 50) {
        var indexSlider: number;
        var indexArrow: number;
        if (sliderType == 'leftSlider') {
            indexSlider = 1;
        }
        else if (sliderType == 'rightSlider') {
            indexSlider = 2;
        }
        else {
            console.log(sliderType, '- Enter valid sliderType(leftSlider/rightSlider)');
        }
        if (toDirection == 'leftSide') {
            indexArrow = 1
        }
        else if (toDirection == 'rightSide') {
            indexArrow = 2
        }
        else {
            console.log(toDirection, 'Enter valid Direction(leftSide/rightSide)');
        }
        if (priceAmount == 'smallIncrement') {
            console.log('indexArrow -', indexArrow);
            await (await this.totalPriceFilterButtonArrow(indexSlider, indexArrow)).click();
        }
        else if (priceAmount == 'mediumIncrement') {
            console.log('indexArrow - 3');
            await (await this.totalPriceFilterButtonArrow(indexSlider, 3)).click();
        }
        else if (priceAmount == 'largeIncrement') {
            await (await this.totalPriceSliderButton(indexSlider).dragAndDrop({ x: relativeXAxis, y: 0 }));
        }
        else {
            console.log(priceAmount, '- Enter valid moveType(smallIncrement/largeIncrement)');
        }
    }

    async convertCurrencyToNumber(currency: string) {
        return Number(currency.replace(/[^0-9.-]+/g, ""));
    }

    async moveCustomerRatingSlider(sliderType: string, increaseRating: number = 2) {
        var indexSlider: number;
        if (sliderType == 'leftSlider') {
            indexSlider = 1;
        }
        else if (sliderType == 'rightSlider') {
            indexSlider = 2;
        }
        else {
            console.log(sliderType, '- Enter valid sliderType(leftSlider/rightSlider)');
        }
        const INCREASE_FACTOR: number = 25; // need to check exact value
        const relativeXAxis: number = increaseRating * INCREASE_FACTOR;
        await browser.pause(5000)
        await (await this.customerRatingSliderButton(indexSlider).dragAndDrop({ x: relativeXAxis, y: 0 }));
    }

    async getTotalPriceFilteredRange() {
        return await (await this.filteredPriceRange()).getText();
    }
    async getTotalPriceFilteredMinValue() {
        return await (await this.filteredPriceMinValue()).getText();
    }
    async getTotalPriceFilteredMaxValue() {
        return await (await this.filteredPriceMaxValue()).getText();
    }
    async getTotalPriceSliderMinValue() {
        return await (await this.priceSliderMinValue()).getText();
    }
    async getTotalPriceSliderMaxValue() {
        return await (await this.priceSliderMaxValue()).getText();
    }
    async getMinFilteredPrice() {
        return await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMinValue());
    }
    async getMaxFilteredPrice() {
        return await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMaxValue());
    }
    async getMinSliderPrice() {
        return await this.convertCurrencyToNumber(await this.getTotalPriceSliderMinValue());
    }
    async getMaxSliderPrice() {
        return await this.convertCurrencyToNumber(await this.getTotalPriceSliderMaxValue());
    }

    //customerRating
    async getCustomerRatingFilteredRange() {
        return await (await this.customerRatingRange()).getText();
    }
    async getCustRatingFilteredMinValue() {
        return await (await this.customerRatingMinValue()).getText();
    }
    async getCustRatingFilteredMaxValue() {
        return await (await this.customerRatingMaxValue()).getText();
    }
    async getCustRatingSliderMinValue() {
        await this.openFilterModal();
        return await (await this.custRatingSliderMinValue()).getText();
    }
    async getCustRatingSliderMaxValue() {
        await this.openFilterModal();
        return await (await this.custRatingSliderMaxValue()).getText();
    }

    async verifyFilteredCustomerRatingRange(minRating: number, maxRating: number) {
        await browser.pause(5000)
        await this.waitForPageLoad()
        const hotelCardCount = Number(await this.countOfHotelCard());
        console.log("No of Hotel : ", hotelCardCount)
        for (var index = 1; index <= hotelCardCount; index++) {
            const hotelCardCustRating = Number(await (this.customerRating(index)).getText());
            console.log(minRating, hotelCardCustRating, maxRating)
            await expect(maxRating >= hotelCardCustRating).toEqual(true);
            await expect(hotelCardCustRating >= minRating).toEqual(true);
        }
    }

    async verifyFilteredPriceRange() {
        await browser.pause(5000)
        await this.waitForPageLoad()
        var upperMinPrice: number = Number(await this.getMinFilteredPrice());
        var lowerMaxPrice: number = Number(await this.getMaxFilteredPrice());
        const hotelCardCount = Number(await this.countOfHotelCard());
        console.log("No of Hotel : ", hotelCardCount)
        for (var index = 1; index <= hotelCardCount; index++) {
            const hotelCardPrice = Number(await this.convertCurrencyToNumber(await (await (this.totalPrice(index)).getText())));
            console.log("No of Hotel : ", upperMinPrice, lowerMaxPrice, hotelCardPrice)
            await expect(upperMinPrice <= hotelCardPrice).toEqual(true);
            await expect(hotelCardPrice <= lowerMaxPrice).toEqual(true);
        }
    }

    async isDefaultPriceSliderDisplayed() {
        const defaultSliderPricePosition = await (await (await this.priceSliderGreySlider()).nextElement()).getAttribute('style');
        var found: boolean = false;
        console.log("Default", defaultSliderPricePosition)
        const modifiedSliderStype = defaultSliderPricePosition.replace(";", "");
        const leftSliderPosition = "left: 0%";
        const rightSliderPosition = "width: 100%";
        if (modifiedSliderStype.includes(leftSliderPosition)) {
            console.log("left price slider is at default position")
            found = true;
        }
        if (modifiedSliderStype.includes(rightSliderPosition)) {
            console.log("right price slider is at default position")
            found = true;
        }
        return found;
    }

    async verifyReadOnlyPriceSliderBar() {
        const color = await (await this.priceSliderGreySlider()).getCSSProperty('background-color')
        const greyColorSeries = ['rgba(246,245,245,1)']
        expect(greyColorSeries).toContain(await color.value);
    }

    async verifyReadOnlyRatingSliderBar() {
        const color = await (await this.custRatingGreySlider()).getCSSProperty('background-color')
        console.log(color)
        const greyColorSeries = ['rgba(246,245,245,1)']
        expect(greyColorSeries).toContain(await color.value);
    }

    async verifyFilteredMinAndMaxPriceValueDisplayed() {
        await expect(await this.filteredPriceMinValue()).toBeDisplayed();
        await expect(await this.filteredPriceMaxValue()).toBeDisplayed();
        const minFilteredPrice = await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMinValue());
        const maxFilteredPrice = await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMaxValue());
        FilterDetails.DefaultMinPriceValue = minFilteredPrice;
        FilterDetails.DefaultMaxPriceValue = maxFilteredPrice;
        await expect(minFilteredPrice > 0).toEqual(true);
        await expect(maxFilteredPrice > 0).toEqual(true);
        await expect(minFilteredPrice < maxFilteredPrice).toEqual(true);
    }

    async verifySliderMinAndMaxPriceValueDisplayed() {

        await expect(await this.priceSliderMinValue()).toBeDisplayed();
        await expect(await this.priceSliderMaxValue()).toBeDisplayed();
        const minSliderPrice = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMinValue());
        const maxSliderPrice = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMaxValue());
        await expect(minSliderPrice > 0).toEqual(true);
        await expect(maxSliderPrice > 0).toEqual(true);
        await expect(minSliderPrice < maxSliderPrice).toEqual(true);
    }

    async verifyBothFilteredAndSliderRating() {

        await expect(await this.filteredPriceMinValue()).toBeDisplayed();
        await expect(await this.filteredPriceMaxValue()).toBeDisplayed();
        await expect(await this.priceSliderMinValue()).toBeDisplayed();
        await expect(await this.priceSliderMaxValue()).toBeDisplayed();
        const minFilteredPrice = await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMinValue());
        const maxFilteredPrice = await this.convertCurrencyToNumber(await this.getTotalPriceFilteredMaxValue());
        const minSliderPrice = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMinValue());
        const maxSliderPrice = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMaxValue());
        await expect(minSliderPrice == minFilteredPrice).toEqual(true);
        await expect(maxSliderPrice == maxFilteredPrice).toEqual(true);

    }

    async verifyPriceRangeChangesWithSlider() {
        const minSliderPrice = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMinValue());
        const maxSliderPrice = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMaxValue());
        await this.dragLeftPriceSlider(200, 'interval')
        await this.dragRightPriceSlider(100, 'interval')
        await this.waitForPageLoad();
        const minSliderPriceAfterChange = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMinValue());
        const maxSliderPriceAfterChange = await this.convertCurrencyToNumber(await this.getTotalPriceSliderMaxValue());
        await expect(minSliderPrice != minSliderPriceAfterChange).toEqual(true);
        await expect(maxSliderPrice != maxSliderPriceAfterChange).toEqual(true);
    }

    //verifies if minimum and maximum rating set is as per expectation
    async verifyMinAndMaxSliderCustomerRating(minRating: number, maxRating: number) {
        minRating = Number(minRating);
        maxRating = Number(maxRating);
        console.log("--", minRating, '-', maxRating, '-');
        //await expect(minRating < maxRating).toEqual(true);
        const actualMinRating = Number(await this.getCustRatingSliderMinValue());
        const actualMaxRating = Number(await this.getCustRatingSliderMaxValue());
        console.log(actualMinRating, actualMaxRating);
        await expect(actualMinRating == minRating).toEqual(true);
        console.log('Expected Min Rating:', minRating, '\nActual Min Rating:', actualMinRating);
        await expect(actualMaxRating == maxRating).toEqual(true);
        console.log('Expected Max Rating:', maxRating, '\nActual Max Rating:', actualMaxRating);
    }

    //#endregion
    async waitForPageLoad(index: number = 1): Promise<void> {
        await (await this.hotelName(index)).waitForExist({ timeout: 90000 });
        await (await this.perPersonPrice(index)).waitForExist({ timeout: 30000 });

    }
    async clickOnHotelName(index: number = 1) {
        await this.hotelName(index).click();
    }
    async getHotelNameByIndex(index: number = 1) {
        return await (await this.hotelName(index)).getText();
    }
    async getStarRatingByIndex(index: number = 1) {
        return await (await this.starRating(index)).length;
    }
    async getAddressByIndex(index: number = 1) {
        return await (await this.address(index)).getText();
    }
    async isPriceSummarySectionDisplayed(index: number = 1) {
        return await (await this.priceSummarySection(index)).isExisting();
    }
    async isTotalPriceDisplayed(index: number = 1) {
        return await (await this.totalPrice(index)).isExisting();
    }
    async isPerPersonPriceDisplayed(index: number = 1) {
        return await (await this.perPersonPrice(index)).isExisting();
    }
    async verifyPerPriceInsideBracket(substring: string, index: number = 1) {
        var isPriceNumber: boolean = false;
        var perPersoneText: string = await (await this.perPersonPrice(index)).getText();
        if ((perPersoneText.includes('£')) || (perPersoneText.includes('(')) || (perPersoneText.includes(')'))) {
            const priceOnly: any = perPersoneText.replace('(', '').replace(')', '').replace('£', '').replace(substring, '');
            if (priceOnly.match(/^[0-9]+$/) != null) {
                console.log('Per person price is number : ', priceOnly)
                isPriceNumber = true;
            }
            else {
                console.log('Per person price is not a number : ', priceOnly)
            }
        }
        else {
            console.log('Per person price format is not proper : ', perPersoneText)
        }
        await expect(isPriceNumber).toEqual(true);
    }

    async verifyTotalPriceAmount(index: number = 1) {
        var isPriceNumber: boolean = false;
        var totalPriceText: string = await (await this.totalPrice(index)).getText();
        if ((totalPriceText.includes('£'))) {
            const priceOnly: any = totalPriceText.replace('£', '');
            if (priceOnly.match(/^[0-9]+$/) != null) {
                console.log('Total price is number : ', priceOnly)
                isPriceNumber = true;
            }
            else {
                console.log('Total price is not a number : ', priceOnly)
            }
        }
        else {
            console.log('Total price format is not proper : ', totalPriceText)
        }
        return isPriceNumber;
    }
    async getTotalPriceText(index: number = 1) {
        return await (await this.holidayPriceFromText(index)).getText();

    }

    async getListOfPriceIncludes(index: number = 1) {
        const listOfPriceIncludeItems: string[] = new Array();
        await this.listOfPriceIncludes(index).forEach(async element => {
            listOfPriceIncludeItems.push(await element.getText())
        });
        return listOfPriceIncludeItems;
    }

    async isHotelCardWithOldPriceAvailalble(clickOnCard: boolean = true) {
        var found: boolean = false;
        try {
            const noOfHotel: number = await this.countOfHotelCard();
            console.log(noOfHotel);
            for (var i = 1; i < noOfHotel; i++) {
                await (await this.listOfHotelCards[i - 1]).scrollIntoView();
                await browser.pause(1000);
                const strOldTotalVal = await (await this.oldTotalPrice(i)).getText();
                console.log('Was Total Price is : ', strOldTotalVal);
                if (strOldTotalVal != '') {
                    const strNewTotalVal = await (await this.newTotalPrice(i)).getText();
                    console.log('Now Total Price : ', strNewTotalVal);
                    found = true;
                    if (clickOnCard) {
                        await (await this.listOfHotelCards[i - 1]).click();
                    }
                    break;
                }
            }
        }
        catch (Error) {
            console.log('Error in isClickHotelCardWithOldPriceAvailalble() method')
        }
        return found;
    }

    async countOfHotelCard() {
        return await this.listOfHotelCards.length;
    }

    async countOfSponsoredPills() {
        return await this.listOfSponsoredpPills.length;
    }

    async verifyPriceIncludeListContains(priceIncludesItemMessage, hotelIndex: number = 1) {
        await expect(await (await this.getListOfPriceIncludes(hotelIndex)).includes(priceIncludesItemMessage)).toEqual(true);
    }

    async isHotePillDisplayed() {
        await (await this.hotelPillGallery()).waitForExist({ timeout: 50000 })
        await (await this.hotelPillGallery()).scrollIntoView();
        return await (await this.hotelPillGallery()).isExisting();
    }

    async getHotelPillText() {
        await (await this.hotelPill()).scrollIntoView();
        const pillText = await (await this.hotelPill()).getText();
        console.log('Pill text is : ', pillText);
        return await pillText;
    }

    async captureHotelInformation(index: number = 1) {
        HotelCardDetails.HotelName = await this.getHotelNameByIndex(index);
        HotelCardDetails.StarRating = await this.getStarRatingByIndex(index);
        HotelCardDetails.Address = await this.getAddressByIndex(index);
        HotelCardDetails.CustomerRating = Number(await (this.customerRating(index)).getText());
        HotelCardDetails.CustomerReviewsCount = Number((await (this.customerReviewsCount(index)).getText()).split(' ')[2]);
        HotelCardDetails.SecureTodayFromPrice = (await (await this.secureTodayForPill(index)).getText()).split(' ')[3];
        HotelCardDetails.TotalPrice = Number(await (await (this.totalPrice(index)).getText()));
        HotelCardDetails.PerPersonPrice = Number(await (await this.perPersonPrice(index)).getText());
        HotelCardDetails.Currency = await (await this.totalPrice(index)).getText();
        HotelCardDetails.IsPriceIncludesSectionDisplayed = await (await this.priceIncludesText(index)).isExisting();
        HotelCardDetails.PriceIncludes = await (await this.getListOfPriceIncludes(index));
        return HotelCardDetails;
    }

    async verifyAverageReviewScore() {
        await this.captureHotelInformation();
        await expect((HotelCardDetails.CustomerRating) > 0).toEqual(true);
    }

    async isSmileyIconExist(index: number = 0) {
        return await (await this.smileyIcon(index)).isExisting();
    }
    async getSecureTodayPillText(index: number = 0) {
        const texxt = await (await this.secureTodayForPill(index)).getText();
        console.log('Text displayed is : ', texxt);
        return texxt;
    }
    async clickSecureTodayPill(index: number = 0) {
        await (await this.listOfHotelCards[index]).scrollIntoView();
        //await (await this.secureTodayForPill(index)).jsclick();
        await this.jsClick(await this.secureTodayForPill(index));
    }
    async isSecureTodayPillClickable(index: number = 0) {
        return (await (await this.secureTodayForPill(index).isClickable()));
    }
    async getcustomerReviewsCount(index: number = 0) {
        const reviewText = await (this.customerReviewsCount(index)).getText();
        const reviewCountText = reviewText.split(' ');
        return Number(reviewCountText[2]);
    }

    async selectAHotel(index: number = 0) {
        try {
            await (await this.viewMoreDetails(index)).waitForExist({ timeout: 50000 })
            await (await this.viewMoreDetails(index)).scrollIntoView();
            //click was not working change to jsClick
            await this.jsClick(await this.viewMoreDetails(index));
        }
        catch (error) {
        }
    }

    async verifyHotelNameForAllHotels() {
        expect(await (await this.getAllHotelNames()).includes(null)).toEqual(false);
    }

    async verifyStarRatingForAllHotels() {
        expect(await (await this.getListOfStarRatings()).includes(null)).toEqual(false);
    }

    async verifyAddressForAllHotels() {
        expect(await (await this.getListOfAddress()).includes(null)).toEqual(false);
    }

    async getAllHotelNames() {
        var listOfHotel: string[] = new Array();
        var hotelsCounts: number = await this.countOfHotelCard();
        console.log('No Of Hotels are : ', hotelsCounts)
        for (var i = 1; i <= hotelsCounts; i++) {
            listOfHotel.push(await (await this.getHotelNameByIndex(i)));
        }
        return listOfHotel
    }

    async getListOfStarRatings() {
        var listOfStartRatings: number[] = new Array();
        var counts: number = await this.countOfHotelCard();
        console.log('Star Ratings of all ', counts, ' Hotels are : ')
        for (var i = 1; i < counts; i++) {
            listOfStartRatings.push(await (await this.getStarRatingByIndex(i)));
        }
        return listOfStartRatings;
    }

    async getListOfAddress() {
        var listOfAddress: string[] = new Array();;
        var counts: number = await this.countOfHotelCard();
        console.log('Respective address of all ', counts, ' Hotels are : ')
        for (var i = 1; i <= counts; i++) {
            listOfAddress.push(await (await this.getAddressByIndex(i)));
        }
        return listOfAddress;
    }

    //#region [Filter Method]

    async isFilterExpanded(filterName: string) {
        await this.openFilterModal();
        return await (await this.filterHeaderByType(filterName)).getAttribute("aria-expanded");
    }

    async toggleFilter(filterName: string, isExpanded: boolean) {
        var isFilterSectionExpanded = await this.isFilterExpanded(filterName);
        if (isExpanded) {
            if (isFilterSectionExpanded === 'false') {
                await (await this.filterHeaderByType(filterName)).click();
            }
        }
        else {
            if (isFilterSectionExpanded === 'true') {
                await (await this.filterHeaderByType(filterName)).click();
            }
        }
    }

    async checkFilterItem(filterName: string, filterItem: string) {
        await this.openFilterModal();
        var isSelected = await (await this.filterItemByType(filterName, filterItem)).isSelected();
        if (!isSelected) {
            await (await this.filterItemByType(filterName, filterItem)).scrollIntoView();
            await (await this.filterItemByType(filterName, filterItem)).click();
            await this.waitForSearchResultsToLoad();
        }
        await this.verifyCheckedFilterItem(filterName, filterItem);
    }

    async verifyCheckedFilterItem(filterName: string, filterItem: string) {
        await this.openFilterModal();
        expect(await this.filterItemByType(filterName, filterItem)).toBeSelected();
    }

    async uncheckFilterItem(filterName: string, filterItem: string) {
        await this.openFilterModal();
        var isSelected = await (await this.filterItemByType(filterName, filterItem)).isSelected();
        if (isSelected) {
            await (await this.filterItemByType(filterName, filterItem)).scrollIntoView();
            await (await this.filterItemByType(filterName, filterItem)).click();
            await this.waitForSearchResultsToLoad();
        }
        await this.verifyUncheckedFilterItem(filterName, filterItem);
    }
    async uncheckSpecificFilterItems(filterName: string, filterItems: any) {
        for (let allItem = 0; allItem < await filterItems.length; allItem++) {
            await this.uncheckFilterItem(filterName, await filterItems[allItem]);
        }
    }

    async checkSpecificFilterItems(filterName: string, filterItems: any) {
        for (let allItem = 0; allItem < await filterItems.length; allItem++) {
            await this.checkFilterItem(filterName, await filterItems[allItem]);
        }
    }

    async verifyUncheckedFilterItem(filterName: string, filterItem: string) {
        await this.openFilterModal();
        expect(await this.filterItemByType(filterName, filterItem)).not.toBeSelected();
    }

    async verifyAllUncheckedFilterItem(filterName: string, filterItem: any) {
        for (let item = 0; item < filterItem.length; item++) {
            await this.verifyUncheckedFilterItem(filterName, await filterItem[item]);
        }
    }

    async verifyFilterLabelonEachHotelCard(filterName: string, allFilterItem: any) {
        await this.closeFilterModal();
        await this.viewAllHotels();
        var allFilterItem: any;
        switch (filterName.trim()) {
            case "Board Type":
                var ListofHotelItem = await this.listOfBoardTypesFromHotelCards;
                for (let hotelItem = 0; hotelItem < ListofHotelItem.length; hotelItem++) {
                    await ListofHotelItem[hotelItem].scrollIntoView();
                    var hotelCardLabelName = await ListofHotelItem[hotelItem].getText();
                    hotelCardLabelName = hotelCardLabelName.replace("And", "&");
                    hotelCardLabelName = hotelCardLabelName.replace("All Inclusive", "All-Inclusive");
                    try {
                        expect(await allFilterItem).toContain(hotelCardLabelName);
                    }
                    catch (error) {
                        throw new Error("Hotel card section display '" + hotelCardLabelName + "' filter label however expected Board Type filter should be '" + await allFilterItem + "'")
                    }
                }
                break;
            case "Star Rating":
                var ListofStar = await this.listOfAllStarsOnCard;
                for (let hotelItem = 0; hotelItem < ListofStar.length; hotelItem++) {
                    var hotelCardLabelName = (await ListofStar[hotelItem].$$('svg').length).toString();
                    try {
                        expect(await allFilterItem).toContain(hotelCardLabelName);
                    }
                    catch (error) {
                        throw new Error("Hotel card section display '" + hotelCardLabelName + "' star rating however expected Star Rating filter should be '" + await allFilterItem + "'")
                    }
                }
                break;

        }
    }

    async verifyFilterLabelOnHotelCardAfterSingleFilterSelection(filterName: string, filterItem: any) {
        var orginalHotelCount = await this.getAllHotelsCount();
        for (let filterIndex = 0; filterIndex < await filterItem.length; filterIndex++) {
            var selectedFilterItem = [];
            await this.uncheckSpecificFilterItems(filterName, await filterItem);
            var filterItemName = await filterItem[filterIndex];
            selectedFilterItem.push(await filterItemName);
            await this.checkFilterItem(filterName, await filterItemName);
            await this.verifyFilterLabelonEachHotelCard(filterName, selectedFilterItem);
            var hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeLessThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("Before applied filter hotel count was " + orginalHotelCount + " and after applied filter " + filterItemName + " hotel count is " + hotelCountAfterFilter)
            }

        }

    }

    async verifyFilterLabelOnHotelCardAfterMultipleFilterSelection(filterName: string, filterItem: any) {
        var selectedFilterItem = [];
        var filterItemsBeforeCheck = [];
        selectedFilterItem.push(await filterItem[0]);
        filterItemsBeforeCheck.push(await filterItem[0])
        var toalHotelCountWithoutFilter = await this.getAllHotelsCount();
        var hotelCountAfterFilter: number;
        await this.checkFilterItem(filterName, await filterItem[0])
        for (let filterIndex = 1; filterIndex < await filterItem.length; filterIndex++) {
            var orginalHotelCount = await this.getAllHotelsCount();
            var filterItemName = await filterItem[filterIndex];
            selectedFilterItem.push(await filterItemName);
            await this.checkFilterItem(filterName, await filterItemName);
            await this.verifyFilterLabelonEachHotelCard(filterName, selectedFilterItem);
            hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeGreaterThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("Total  " + orginalHotelCount + " hotel displayed for selected filters " + filterItemsBeforeCheck + " and total hotel " + hotelCountAfterFilter + " is displayed for selected filter " + selectedFilterItem)
            }
            filterItemsBeforeCheck.push(await filterItemName)
        }
        try {
            expect(Number(toalHotelCountWithoutFilter)).toBe(Number(hotelCountAfterFilter));
        }
        catch (error) {
            throw new Error("Original hotel count was " + toalHotelCountWithoutFilter + " and total hotel count is " + hotelCountAfterFilter + " after applied all filter items")
        }
    }

    async verifyFilterLabelOnHotelCardAfterUncheckFilter(filterName: string, filterItem: any) {
        const allFilterItem = await filterItem;
        var selectedFilterItem = await allFilterItem;
        for (let filterIndex = 0; filterIndex < await allFilterItem.length - 1; filterIndex++) {
            var orginalHotelCount = await this.getAllHotelsCount();
            var filterItemName = await allFilterItem[filterIndex];
            selectedFilterItem = selectedFilterItem.filter(e => e !== filterItemName)
            await this.uncheckFilterItem(filterName, await filterItemName);
            await this.verifyFilterLabelonEachHotelCard(filterName, selectedFilterItem);
            var hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeLessThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("After applied filter hotel count was " + orginalHotelCount + " and hotel count is " + hotelCountAfterFilter + " after unchecking filter " + filterItemName)
            }
        }
    }

    async verifySelectedStarRating(starRating: any) {
        await this.openFilterModal();
        expect(await this.starRatingFilterItem(await starRating)).toHaveAttribute('checked', 'true')
    }

    async selectStarRating(starRating: any) {
        await this.openFilterModal();
        var isSelected = await (await this.starRatingFilterItem(await starRating)).getAttribute('checked')
        if (isSelected !== 'true') {
            await (await this.starRatingFilterItem(await starRating)).click();
            await this.waitForSearchResultsToLoad();
        }
        this.verifySelectedStarRating(await starRating);
    }

    async verifyUnSelectStarRating(starRating: any) {
        await this.openFilterModal();
        expect(await this.starRatingFilterItem(await starRating)).not.toHaveAttribute('checked', 'true')
    }

    async unSelectStarRating(starRating: any) {
        await this.openFilterModal();
        var isSelected = await (await this.starRatingFilterItem(starRating)).getAttribute('checked')
        if (isSelected === 'true') {
            await (await this.starRatingFilterItem(await starRating)).click();
            await this.waitForSearchResultsToLoad();
        }
        await this.verifyUnSelectStarRating(await starRating);
    }

    async verifyDisabledStarRating(starRating: any) {
        await this.openFilterModal();
        expect(await this.starRatingFilterItem(await starRating)).not.toBeEnabled();
    }

    async verifyDisabledFilterItem(filterName: string, filterItem: string) {
        await this.openFilterModal();
        expect(await this.filterItemByType(filterName, filterItem)).not.toBeEnabled();
    }

    async verifyAllStarRatingIsUnSelected(filterItem: any) {
        for (let item = 0; item < await filterItem.length; item++) {
            await this.verifyUnSelectStarRating(await filterItem[item]);
        }
    }

    async unSelectSpecificStarRating(filterItems: any) {
        for (let allItem = 0; allItem < filterItems.length; allItem++) {
            await this.unSelectStarRating(filterItems[allItem])
        }
    }

    async selectSpecificStarRating(filterItems: any) {
        for (let allItem = 0; allItem < filterItems.length; allItem++) {
            await this.selectStarRating(filterItems[allItem])
        }
    }

    async verifyStarRatingOnHotelCardAfterSingleSelection(filterName: string, filterItem: any) {
        var orginalHotelCount = await this.getAllHotelsCount();
        for (let filterIndex = 0; filterIndex < await filterItem.length; filterIndex++) {
            var selectedFilterItem = [];
            await this.unSelectSpecificStarRating(await filterItem);
            var filterItemName = await filterItem[filterIndex];
            selectedFilterItem.push(await filterItemName);
            await this.selectStarRating(await filterItemName);
            await this.verifyFilterLabelonEachHotelCard(filterName, selectedFilterItem);
            var hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeLessThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("Before applied filter hotel count was " + orginalHotelCount + " and after applied star rating filter " + filterItemName + " hotel count is " + hotelCountAfterFilter)
            }
        }

    }

    async verifyStarRatingOnHotelCardAfterMutipleSelection(filterName: string, filterItem: any) {
        var hotelCountAfterFilter: number;
        var selectedFilterItem = [];
        var filterItemsBeforeCheck = [];
        var toalHotelCountWithoutFilter = await this.getAllHotelsCount();
        selectedFilterItem.push(await filterItem[0]);
        filterItemsBeforeCheck.push(await filterItem[0])
        await this.selectStarRating(await filterItem[0])
        for (let filterIndex = 1; filterIndex < await filterItem.length; filterIndex++) {
            var orginalHotelCount = await this.getAllHotelsCount();
            var filterItemName = await filterItem[filterIndex];
            selectedFilterItem.push(await filterItemName);
            await this.selectStarRating(await filterItemName);
            await this.verifyFilterLabelonEachHotelCard(filterName, selectedFilterItem);
            hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeGreaterThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("Total  " + orginalHotelCount + " hotel displayed for selected star rating " + filterItemsBeforeCheck + " and total hotel " + hotelCountAfterFilter + " is displayed for selected star rating " + selectedFilterItem)
            }
            filterItemsBeforeCheck.push(await filterItemName)
        }
        try {
            expect(Number(toalHotelCountWithoutFilter)).toBe(Number(hotelCountAfterFilter));
        }
        catch (error) {
            throw new Error("Original hotel count was " + toalHotelCountWithoutFilter + " without any filter and total hotel count is " + hotelCountAfterFilter + " after applied all filter")
        }
    }

    async verifyStarRatingOnHotelCardAfterUnSelecting(filterName: string, filterItem: any) {
        var selectedFilterItem = [];
        const allFilterItem = await filterItem;
        selectedFilterItem = await allFilterItem;
        for (let filterIndex = 0; filterIndex < await allFilterItem.length - 1; filterIndex++) {
            var orginalHotelCount = await this.getAllHotelsCount();
            var filterItemName = await allFilterItem[filterIndex];
            selectedFilterItem = selectedFilterItem.filter(e => e !== filterItemName)
            await this.unSelectStarRating(await filterItemName);
            await this.verifyFilterLabelonEachHotelCard(filterName, selectedFilterItem);
            var hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeLessThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("After applied filter hotel count was " + orginalHotelCount + " and hotel count is " + hotelCountAfterFilter + " after unselecting star rating " + filterItemName)
            }
        }
    }

    async openFilterModal() {
        if (global.isMobileView) {
            var filtersection = await this.filtersModal.isDisplayed();
            if (!filtersection) {
                await this.jsClick(await this.filterButton);
                expect(await this.filtersModal).toBeDisplayed();
            }
        }

    }

    async closeFilterModal() {
        if (global.isMobileView) {
            var filtersection = await this.filtersModal.isDisplayed();
            if (filtersection) {
                await this.closeModal();
            }
        }
    }

    async getAllFilterItems(filterName: any) {
        await this.openFilterModal();
        var allfilterItem = [];
        var allRunTimeFilterItem = await this.listOfAllFilterItemsByType(await filterName);
        for (let item = 0; item < allRunTimeFilterItem.length; item++) {
            var itemText = await allRunTimeFilterItem[item].getText();
            allfilterItem.push(itemText);
        }
        return allfilterItem;
    }

    async getAllEnabledAndDisabledFilterItems(filterName: string) {
        var allFilterItems = [];
        await this.openFilterModal();
        var allEnablefilterItem = [];
        var allDisablefilterItem = [];
        var allRunTimeFilterItem = await this.listOfAllFilterItemsByType(filterName);
        for (let item = 0; item < allRunTimeFilterItem.length; item++) {
            var itemText = await allRunTimeFilterItem[item].getText();
            var isEnable = await (await this.filterItemByType(filterName, itemText)).isEnabled();
            if (isEnable) {

                allEnablefilterItem.push(itemText);
            }
            else {
                allDisablefilterItem.push(itemText);
            }
        }
        allFilterItems.push(allEnablefilterItem);
        allFilterItems.push(allDisablefilterItem);
        return allFilterItems;
    }

    async getAllEnabledAndDisabledStarRating() {
        var allFilterItems = [];
        await this.openFilterModal();
        var allEnablefilterItem = [];
        var allDisablefilterItem = [];
        var allRunTimeFilterItem = await this.listOfAllStarRatingItemsInFilter();
        for (let item = 0; item < allRunTimeFilterItem.length; item++) {
            var itemText = await allRunTimeFilterItem[item].getAttribute('name');
            var isEnable = await allRunTimeFilterItem[item].isEnabled();
            if (isEnable) {

                allEnablefilterItem.push(itemText);
            }
            else {
                allDisablefilterItem.push(itemText);
            }
        }
        allFilterItems.push(allEnablefilterItem);
        allFilterItems.push(allDisablefilterItem);
        return allFilterItems;
    }

    async getAllStarRating() {
        await this.openFilterModal();
        var allfilterItem = [];
        await (await this.filterHeaderByType('Star Rating')).scrollIntoView();
        var allRunTimeFilterItem = await this.listOfAllStarRatingItemsInFilter();
        for (let item = 0; item < allRunTimeFilterItem.length; item++) {
            var itemText = await allRunTimeFilterItem[item].getAttribute('name');
            allfilterItem.push(itemText);
        }
        return allfilterItem;
    }

    async getAllHotelsCount() {
        await this.viewAllHotels();
        return await this.listOfHotelCards.length;
    }

    async verifyFilterItemIsNotDisplayed(filterName: any) {
        await this.openFilterModal();
        expect((await this.listOfAllFilterItemsByType(await filterName))).not.toBeDisplayed();
    }

    async veriftStarRatingIsNotDisplayed() {
        await this.openFilterModal();
        expect(await this.listOfAllStarRatingItemsInFilter()).not.toBeDisplayed();
    }

    async isCustomerRatingSliderDisplayed() {
        await this.openFilterModal();
        return await (await this.customerRatingSlider).isDisplayed();
    }

    async isTotalPriceSliderDisplayed() {
        await this.openFilterModal();
        return await (await this.totalPriceSlider).isDisplayed();
    }

    async waitForSearchResultsToLoad(): Promise<void> {
        try {
            await (await this.loaderIcon).waitForExist({ timeout: 1000 });
        }
        catch (error) { }
        try {
            await (await this.loaderIcon).waitForExist({ timeout: 60000, reverse: true });
        }
        catch (error) { }
    }
    async viewAllHotels() {
        await this.closeFilterModal();
        await this.waitForSearchResultsToLoad();
        var isMoreHotel = await (await this.showMoreHotels).isDisplayed();
        while (isMoreHotel) {
            try {
                await (await this.showMoreHotels).scrollIntoView();
                await (await this.showMoreHotels).click();
                await this.waitForSearchResultsToLoad();
            }
            catch (error) { }

            isMoreHotel = await (await this.showMoreHotels).isDisplayed();
        }
    }


    async isResetFiltersButtonEnabled() {
        await this.openFilterModal();
        return await (await this.resetFiltersButton).isEnabled();
    }

    async clickOnResetFiltersButton() {
        await this.openFilterModal();
        await (await this.resetFiltersButton).click();
        await this.waitForSearchResultsToLoad();
    }

    async verifySelectedFilterItemsInApplicationURL(filterName: string, selectedFilter: any, filterItemsIsNotBlank: boolean) {
        var appURL = (await browser.getUrl()).toLocaleLowerCase();
        filterName = filterName.toLowerCase().replace(/ /g, "");
        if (!filterItemsIsNotBlank) {
            expect(appURL).not.toContain(filterName);
        }
        else {
            var selectedFilterInApplicationURL = appURL.split(filterName + "=")[1];
            selectedFilterInApplicationURL = selectedFilterInApplicationURL.split("&")[0];
            selectedFilterInApplicationURL = selectedFilterInApplicationURL.replace(/%2c/g, ",")
            var allselectedFilterInApplicationURL = selectedFilterInApplicationURL.split(',');
            expect(selectedFilter.length).toEqual(allselectedFilterInApplicationURL.length);
        }
    }

    async setTotalPriceRangeByURL(minRange: string, maxRange: string) {
        var appURL = (await browser.getUrl());
        var totalPriceURL: string;
        if (!appURL.includes('filters.priceRange.lower')) {
            totalPriceURL = "&filters.priceRange.lower=" + minRange + "&filters.priceRange.upper=" + maxRange;
            await browser.navigateTo(appURL + totalPriceURL);
        }
        else {
            var appMinRange = appURL.split("&filters.priceRange.lower=")[1].split("&")[0];
            appURL = appURL.replace("&filters.priceRange.lower=" + appMinRange, "&filters.priceRange.lower=" + minRange);
            var appMaxRange = appURL.split("&filters.priceRange.upper=")[1].split("&")[0];
            appURL = appURL.replace("&filters.priceRange.upper=" + appMaxRange, "&filters.priceRange.upper=" + maxRange);
            await browser.navigateTo(appURL);
        }
    }

    async setCustomerRatingByURL(minRange: string, maxRange: string) {
        var appURL = (await browser.getUrl());
        var totalPriceURL: string;
        if (!appURL.includes('filters.ratingRange.lowers')) {
            totalPriceURL = "&filters.ratingRange.lower=" + minRange + "&filters.ratingRange.upper=" + maxRange;
            await browser.navigateTo(appURL + totalPriceURL);
        }
        else {
            var appMinRange = appURL.split("&filters.ratingRange.lower=")[1].split("&")[0];
            appURL = appURL.replace("&filters.ratingRange.lower=" + appMinRange, "&filters.ratingRange.lower=" + minRange);
            var appMaxRange = appURL.split("&filters.ratingRange.upper=")[1].split("&")[0];
            appURL = appURL.replace("&filters.ratingRange.upper=" + appMaxRange, "&filters.ratingRange.upper=" + maxRange);
            await browser.navigateTo(appURL);
        }
    }

    async getMaxAndMinValuesOfFilterBasedOnExpression(expression: string, minValue: number, maxValue: number) {
        var expValue: string;
        var defaultPrice: Number;
        if (expression.toLocaleLowerCase().includes('min')) {
            defaultPrice = minValue;
            expValue = minValue.toString();
        }
        else {
            defaultPrice = maxValue;
            expValue = maxValue.toString();
        }
        if (expression.includes('+')) {
            expValue = (Number(defaultPrice) + Number(expression.split('+')[1])).toString()
        }
        if (expression.includes('-')) {
            expValue = (Number(defaultPrice) - Number(expression.split('-')[1])).toString()
        }
        return expValue;
    }

    async isShowMoreHotelsDisplayed() {
        return (await this.showMoreHotels).isDisplayed();
    }

    async applyFiltersTillResultCountIsLessThanOrEqualstoDefaultCount() {
        var minRange: number;
        var maxRange: number;
        while (await this.countOfHotelCard() == SearchResultsDisplayedPerLoad && await this.isShowMoreHotelsDisplayed()) {
            await this.openFilterModal();
            minRange = Number(await this.getMinFilteredPrice());
            maxRange = minRange + 5;
            await this.setTotalPriceRangeByURL(minRange.toString(), maxRange.toString());
            await this.waitForSearchResultsToLoad();
            await this.closeFilterModal();
            if (await this.countOfHotelCard() <= SearchResultsDisplayedPerLoad && !(await this.isShowMoreHotelsDisplayed())) {
                break;
            }
        }

    }

    async paginationCheck() {
        var isMoreHotel = await this.isShowMoreHotelsDisplayed();
        while (isMoreHotel) {
            var beforeHotelCount: number;
            var afterHotelCount: number;
            beforeHotelCount = await this.countOfHotelCard();
            await (await this.showMoreHotels).click();
            await this.waitForSearchResultsToLoad();
            afterHotelCount = await this.countOfHotelCard();
            var isMoreHotel = await this.isShowMoreHotelsDisplayed();
            if (isMoreHotel) {
                expect(beforeHotelCount + Number(SearchResultsDisplayedPerLoad)).toBe(afterHotelCount);
            }
            else {
                if (beforeHotelCount + Number(SearchResultsDisplayedPerLoad) > afterHotelCount) {
                    expect(beforeHotelCount + Number(SearchResultsDisplayedPerLoad)).toBeGreaterThan(afterHotelCount);
                }
                else {
                    expect(beforeHotelCount + Number(SearchResultsDisplayedPerLoad)).toEqual(afterHotelCount);

                } 2
            }
        }
    }

    async getSelectedFilterItemsFromListOfFilter(filterName: string, filterItems: any) {
        var selectedFilterItem = [];
        for (let item = 0; item < await filterItems.length; item++) {
            var filterItem = await filterItems[item];
            var isSelected = await this.isFilterItemSelected(filterName, await filterItem);
            if (isSelected) {
                selectedFilterItem.push(await filterItem);
            }
        }
        return selectedFilterItem;
    }

    async isFilterItemSelected(filterName: string, filterItem: string) {
        await this.openFilterModal();
        return await (await this.filterItemByType(filterName, filterItem)).isSelected();
    }

    async isFilterItemEnable(filterName: string, filterItem: string) {
        await this.openFilterModal();
        return await (await this.filterItemByType(filterName, filterItem)).isEnabled();
    }

    async checkSpecificEnableFilterItems(filterName: string, filterItems: any) {
        for (let allItem = 0; allItem < await filterItems.length; allItem++) {
            var isEnable = await this.isFilterItemEnable(filterName, await filterItems[allItem])
            if (isEnable)
                await this.checkFilterItem(filterName, await filterItems[allItem]);
        }
    }

    async verifyHotelCardCountAfterMultipleFilterSelectionForHolidayandPropertyAmenitiesFilter(filterName: string, filterItem: any) {
        var selectedFilterItem = [];
        var filterItemsBeforeCheck = [];
        selectedFilterItem.push(await filterItem[0]);
        var hotelCountAfterFilter: number;
        await this.checkFilterItem(filterName, await filterItem[0])
        for (let filterIndex = 1; filterIndex < await filterItem.length; filterIndex++) {
            var filterItemName = await filterItem[filterIndex];
            var isFilterEnable = await this.isFilterItemEnable(filterName, await filterItemName);
            if (isFilterEnable) {
                var orginalHotelCount = await this.getAllHotelsCount();
                selectedFilterItem.push(await filterItemName);
                await this.checkFilterItem(filterName, await filterItemName);
                hotelCountAfterFilter = await this.getAllHotelsCount();
                try {
                    expect(Number(hotelCountAfterFilter)).toBeLessThanOrEqual(Number(orginalHotelCount));
                }
                catch (error) {
                    throw new Error("Total  " + orginalHotelCount + " hotel displayed for selected filters " + filterItemsBeforeCheck + " and total hotel " + hotelCountAfterFilter + " is displayed for selected filter " + selectedFilterItem)
                }
                filterItemsBeforeCheck.push(await filterItemName)
            }
        }
    }

    async verifyHotelCardCountAfterFilterUnCheckingForHolidayandPropertyAmenitiesFilter(filterName: string, filterItem: any) {
        const allFilterItem = await filterItem;
        var selectedFilterItem = await allFilterItem;
        for (let filterIndex = 0; filterIndex < await allFilterItem.length - 1; filterIndex++) {
            var orginalHotelCount = await this.getAllHotelsCount();
            var filterItemName = await allFilterItem[filterIndex];
            selectedFilterItem = selectedFilterItem.filter(e => e !== filterItemName)
            await this.uncheckFilterItem(filterName, await filterItemName);
            var hotelCountAfterFilter = await this.getAllHotelsCount();
            try {
                expect(Number(hotelCountAfterFilter)).toBeGreaterThanOrEqual(Number(orginalHotelCount));
            }
            catch (error) {
                throw new Error("After applied filter hotel count was " + orginalHotelCount + " and hotel count is " + hotelCountAfterFilter + " after unchecking filter " + filterItemName)
            }
        }
    }

    async verifyDisplayedHotelsCount(expectedCount: Number) {
        var beforeHotelCount: number;
        await this.waitForPageLoad();
        beforeHotelCount = await this.countOfHotelCard();
        expect(beforeHotelCount).toEqual(expectedCount);
    }

    async clickShowMoreHotels() {
        await (await this.showMoreHotels).scrollIntoView();
        await (await this.showMoreHotels).click();
        await this.waitForSearchResultsToLoad();
    }

    async showMoreCountIncrement(expectedCount: number) {
        var incrementCount: Number;
        incrementCount = await this.countOfHotelCard();
        expect(incrementCount).toBeGreaterThan(expectedCount);
        expect(incrementCount).toBeLessThanOrEqual(2 * expectedCount);
    }
    //#endregion (pagination)

    //#endregion
    async isSkeletonLoadingDisplayed() {
        return await (await this.loaderIcon).waitForDisplayed({ timeout: 90000 });
    }
    async isSkeletonFilterLoadingDisplayed() {
        return await (await this.filterSkeletonLoader).waitForDisplayed({ timeout: 90000 });
    }

    async waitAndClickShowMoreHotels() {
        await this.waitForSearchResultsToLoad();
        await this.isShowMoreHotelsDisplayed();
        await (await this.showMoreHotels).scrollIntoView();
        await (await this.showMoreHotels).click();
    }
}

export default new HotelSearchResultsPage();