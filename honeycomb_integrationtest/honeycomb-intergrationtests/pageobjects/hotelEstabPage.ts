import Page from './basePage'
import EstabHotelInformation from '../modals/estabHotelInformation'
import CommonFunctions from '../utilities/commonFunctions';
import {OverviewTabFacilities, OverviewTabHotelInfo, OverviewTabHotelInfoOverview, OverviewTabReviews} from '../utilities/constants'
import BoardTypeDetails  from '../modals/boardTypeDetails';

export class HotelEstabPage extends Page {
    private get hotelName() { return $("//h1"); }
    private get starRating() { return $$("//span[contains(@class,'StarRating')]//*[name()='svg']"); }
    private get location() { return $("//div[contains(@class,'StyleUtility')]/h3[contains(@class,'Body')]"); }
    private get customerRating() { return $("//span[contains(@class,'Badge')]"); }
    private get customerReviews() { return $("//p[contains(@class,'Body')]"); }
    private get breadcrumbText() {return $("//nav")}​
    private breadCrumbLink(linkName){return $("//div[contains(@class,'StyleUtility')]//nav//a/span[.=" +linkName+"]")}
    private get totalPriceFromText(){return $("//p[text()='Total price from']")}
    private get totalPrice(){return $$("//div[contains(@class,'Stacker')]/div[contains(@class,'Stacker')]/div")}  //could be 1 or 2
    private get perPersonPrice(){return $("//strong")}
    private get hotelImage(){return $("//div[contains(@class,'center')]//img")}
    private get hotelImageThumbnail(){return $$("//span[contains(@class,'thumbnail-inner')]/img")}
    private get hotelImageFullScreen(){return $("//span[text()='Full Screen']")}
    // To click on left scroll pass index 0, right scroll pass 1, close full screen pass 2
    private get fullScreenButtons(){return $$("//div[contains(@class,'bottom fullscreen')]//button[contains(@class,'IconButton')]")}
    private get transferMeassage(){return $("//*[contains(@class,'Bus')]//following-sibling::p")}
    private get reviewText(){return $("//p[text()='Our Customer Rating']")}
    private get reviewRating(){return $("//span[contains(@class,'Badge')]")}
    private get reviewNumber(){return $("//p[contains(text(),'based on')]")}
    private get noReviewText(){return $("(//div[contains(@class,'StyleUtility')]/p)[3]")}

    private get priceIncludesSection(){return $("//div[contains(@class,'dx-StyleUtility')]")}
    private get priceIncludeText(){return $("//p[contains(text(),'Price includes')]")}
    private get priceIncludeItems(){return $$("//p[contains(text(),'Price includes')]//following::ul/li")}
    private get boardTypeInfo(){return $("(//*[contains(@class,'KnifeAndForkIcon')]//following-sibling::p)[1]")}
    private get guestInfo(){return $("//*[contains(@class,'PeopleIcon')]//following-sibling::p")}
    private get localChargesInfo(){return $("//*[contains(@class,'PercentageIcon')]//following-sibling::p")}
    private get offerInfo(){return $("//*[contains(@class,'TagIcon')]//following-sibling::p")}
    private get selctedImage(){return $("//div[contains(@class,'image-gallery-slide  center')]")}
    private get hotelPrice() { return $("//div[contains(@class,'Stacker')]/div[contains(@class,'Heading')]"); }
    private get loadingImage(){return $("//*[contains(@class,'Throbber')]")}
    //#region [OverviewTab]
    private async tabContainer(tabName) { return await $("//button[contains(text(),'"+tabName+"')]"); }
    private async overviewItem() { return await $$("//*[@id='hotel-tab0']//h3[contains(@class,'Heading')]"); }
    private async facilitiesIcon(index: number = 1) { return await $("(//div[contains(@class,'IconCard')])["+index+"]"); }
    private async facilitiesDetail(index: number = 1) { return await $("(//p[contains(@class,'IconCard')])["+index+"]"); }
    private async facilitiesIconArray() { return await $$("//div[contains(@class,'IconCard')]"); }
    private async seeAllCustomerReviewsButton() { return await $("//span[text()='See All Customer Reviews']"); }
    //#endregion

    //#region [Rooms Tab]
    private get perPersonPriceToogle(){return $("//div[@id='hotel-tab1']//div[@role='group']/button[text()='Total Price']")}
    private get totalPriceToogle(){return $("//div[@id='hotel-tab1']//div[@role='group']/button[text()='Per Person Price']")}
    private get RoomTypes() { return  $$("//div[@id='hotel-tab1']/div//img"); }
    

    
    private async RoomType(roomTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ Number(roomTypeIndex)+1 +"]//h3)[1]"); }
    private async RoomTypeImage(roomTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ Number(roomTypeIndex)+1 +"]//img)[1]"); }
    private async RoomTypeText(roomTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ Number(roomTypeIndex)+1 +"]//h3)[1]/parent::div/div[2]//p"); }

    private async BoardTypes(roomTypeIndex) { return await $$("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul//input"); }
    private async BoardType(roomTypeIndex,boardTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul//input[@id='board-option-"+ (Number(boardTypeIndex)-1) +"']"); }
    private async BoardTypeDetails(roomTypeIndex,boardTypeIndex) { return await $$("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul/li["+ (Number(2*boardTypeIndex)-1) +"]/div/div[2]//p"); }
    private async SecureTodayFromPillOnBoardType(roomTypeIndex,boardTypeIndex) { return await $$("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul/li["+ (Number(2*boardTypeIndex)-1) +"]/div/div[2]/div//span[contains(text(),'Secure')]"); }
    private async BoardTypePrice(roomTypeIndex,boardTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul/li["+ (Number(2*boardTypeIndex)-1) +"]/div/div[3]//h3"); }
    private async BoardTypeStrikedOffPrice(roomTypeIndex,boardTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul/li["+ (Number(2*boardTypeIndex)-1) +"]/div/div[3]/div/div"); }
    private async BoardTypeTotalPrice(roomTypeIndex,boardTypeIndex) { return await $("(//div[@id='hotel-tab1']/div/div["+ (Number(roomTypeIndex)+1) +"]//h3)[1]/parent::div/following::div[1]/div/ul/li["+ (Number(2*boardTypeIndex)-1) +"]/div/div[3]/div/p"); }


    async GetBoardTypeDetails(roomTypeIndex, boardTypeIndex)
    {
        await (await this.BoardType(roomTypeIndex,boardTypeIndex)).scrollIntoView();
        var boardTypeDetails =  await (await this.BoardTypeDetails(roomTypeIndex,boardTypeIndex));
        BoardTypeDetails.BoardName =await boardTypeDetails[0].getText();
        console.log("******* Board Name " + BoardTypeDetails.BoardName);
        boardTypeDetails.forEach(async(element) => {
                var detailsText  = await element.getText()
                console.log("******* board details" + detailsText);

                if(detailsText.includes("Free hotel"))
                {
                    BoardTypeDetails.FreeCanxMessage = detailsText
                    BoardTypeDetails.FreeCanxDate = detailsText.split("before")[1]
                }
                else if (detailsText.includes("Non-Refundable"))
                    BoardTypeDetails.IsRefundable = false
                else if (detailsText.includes("Refundable"))
                    BoardTypeDetails.IsRefundable = false
                else if (detailsText.includes("Save upto"))
                    BoardTypeDetails.PromotionText = detailsText
        });

        var priceText = await (await this.BoardTypePrice(roomTypeIndex,boardTypeIndex)).getText();
        console.log("******* Price" + priceText);
        if (priceText == "Selected")
        {
             BoardTypeDetails.DeltaPrice = "0"
             BoardTypeDetails.IsSelected = true;
        }
        else
        {
             BoardTypeDetails.DeltaPrice = priceText
             BoardTypeDetails.IsSelected = false;
             BoardTypeDetails.TotalPrice = await (await this.BoardTypeTotalPrice(roomTypeIndex,boardTypeIndex)).getText();
             console.log("******* Price" + BoardTypeDetails.TotalPrice);
             if (await (await this.BoardTypeStrikedOffPrice(roomTypeIndex,boardTypeIndex)).isDisplayed)
             BoardTypeDetails.StrikedOffPrice = await (await this.BoardTypeStrikedOffPrice(roomTypeIndex,boardTypeIndex)).getText();
        } 
     
             
    }

    private async isPerPersonPriceToggleSelected() {
        return await (await this.perPersonPriceToogle).getAttribute("aria-pressed")=="true";
    }

    private async isTotalPriceToogleSelected() {
        return await (await this.totalPriceToogle).getAttribute("aria-pressed")=="true";
    }

    async GetRoomTypesCount() {
    
        let roomTypeCount = Number(await (await this.RoomTypes).length)
        console.log(" Room type Count " + roomTypeCount);
        return roomTypeCount;
    }

    async GetBoardTypesCount(roomTypeIndex:number)
    {
        console.log(" Board type Count " + await (await this.BoardTypes(roomTypeIndex)).length);
        return await (await this.BoardTypes(roomTypeIndex)).length;
    }

    private async SelectBoardType(roomTypeIndex, boardTypeIndex)
    {
         await (await this.BoardType(roomTypeIndex,boardTypeIndex)).scrollIntoView();
         await (await this.BoardType(roomTypeIndex,boardTypeIndex)).click();
    } 

     async GetBoardTypeName(roomTypeIndex, boardTypeIndex)
    {
         await (await this.BoardType(roomTypeIndex,boardTypeIndex)).scrollIntoView();
        var boardTypeDetails =  await (await this.BoardTypeDetails(roomTypeIndex,boardTypeIndex)).length;
        return boardTypeDetails[0].getText();
    } 

    






    //#endregion

   


    private get wasPrice() {
        return $("//div[contains(@class,'Stacker')]/div[contains(@class,'Body')]");
    }

    private get totalPriceText() {
        return $("//div[@class='css-vzegvy-Stacker']/p[1]");
    }

    private get perPersonText() {
        return $("//*[contains(@class,'vzegvy-Stacker')]/p[contains(@class,'1i1elx2-Body')]");
    }  
    private get listOfFacilities() { return $$("//div[contains(@class,'IconCard')]//p"); } 
    //get durationInfo()
    //get cancellationInfo()

    private async  CaptureHotelInformation() {        
        EstabHotelInformation.HotelName = await (await this.hotelName).getText();
        EstabHotelInformation.StarRating = await (await this.starRating).length;
        EstabHotelInformation.Location = await (await this.location).getText();
        if((await this.totalPrice.length===2)){
            EstabHotelInformation.TotalPrice = Number(await (await this.totalPrice[1]).getText());
        }
        //EstabHotelInformation.CustomerRating = await(this.customerRating).getText();
        //EstabHotelInformation.CustomerReviews = await(this.customerReviews).getText();
        return EstabHotelInformation;
    } 

    async waitForHotelEstabPage(): Promise<void> {
        {
            await (await this.totalPriceFromText).waitForDisplayed({timeout:50000}); 
        }
    }

    async isHotelImageDisplayed(){
        return await (await  this.hotelImage).isDisplayed();
    }

    async isHotelImageThumbnailDisplayed(){
        const count = await (this.hotelImageThumbnail).length;
        if(await (await this.hotelImageThumbnail[1]).isDisplayed() && count>1){
            return true;
        }
        else
        {
            return false;
        }   
    }

    async hotelThumbnailImageValidation(){
        for(let thumbnail in await this.hotelImageThumbnail){
            if(Number(thumbnail)>=5){
                break;
            }
            const attrValue =await (await this.hotelImageThumbnail[thumbnail]).getAttribute("src");
            const hrefThumbnailLink=await (attrValue).split('?');
            await this.hotelImageThumbnail[thumbnail].click();
            await browser.pause(5000);
            const hrefImageLink = await (await (await this.hotelImage).getAttribute('src')).split('?');
            await expect(hrefImageLink[0]).toEqual(hrefThumbnailLink[0]); 
        }
    }

    async clickOnFullScreen(){
        await (await this.hotelImageFullScreen).click();
    }

    async hotelImageNavigationValidation()
    {
        var minIteration = 5;
        if(await this.hotelImageThumbnail.length<minIteration)
        minIteration=await this.hotelImageThumbnail.length;

        for(let counter=minIteration; counter>=1;counter--){
            var ariaValue=await(await this.selctedImage).getAttribute('aria-label');
            await expect(await (await this.selctedImage)).toHaveAttrContaining('aria-label', await counter.toString()); 
            if(await this.fullScreenButtons[0].isEnabled())
            await this.fullScreenButtons[0].click();
            await browser.pause(4000);
        }
        for(let counter=1; counter<=minIteration;counter++){
            await expect(await (await this.selctedImage)).toHaveAttrContaining('aria-label', await counter.toString()); 
            if(await this.fullScreenButtons[1].isEnabled())
            await this.fullScreenButtons[1].click();
            await browser.pause(4000);
        }
    }

    async closeFullScreen(){
        await this.fullScreenButtons[2].click();
    }

    async verifyFullScreenClosed(){
        await expect(await this.hotelImageFullScreen).toBeDisplayed({message:'full screen is not closed'});
    }

    async verifyHotelInfo(name:string, location:string, province:string){
        await expect(await this.hotelName).toBeDisplayed({message:'hotel name not displayed'});
        await expect(await this.hotelName).toHaveText(name, {message:'Hotel name do not match'});
        await expect(await this.starRating).toBeDisplayed({message:'starrating not displayed'});
        await expect(await this.location).toHaveText(location+", "+province, {message:'location do not match'});
    }

    async verifyReturnTransferMessageInHotelCard(message:string){
        await expect(await this.transferMeassage).toBeDisplayed({message:'Transfer message is not displayedS'});
        await expect(await this.transferMeassage).toHaveText(message, {message:'Transfer message do not match'});
    }

    async verifyHotelReviewOptions(){
        await expect(await this.reviewText).toBeDisplayed({message:'Review text is not displayed'});
        await expect(await this.reviewNumber).toBeDisplayed({message:'Number of reviews is not displayed'});
        await expect(await this.reviewRating).toBeDisplayed({message:'Customer rating is not displayed'});
    }

    async verifyNoReviewsMessage(noReviewMessage:string){
        await expect(await this.noReviewText).toBeDisplayed({message:'No reviews text is not displayed'});
        await expect(await this.noReviewText).toHaveText(noReviewMessage,{message:'Text do not match'});
    }

    async verifyPriceIncludeSection(){
        await expect(await this.priceIncludeText).toBeDisplayed({message: 'price include text is not displayed'});
        await expect(await this.boardTypeInfo).toBeDisplayed({message: 'Board type info is not displayed'});
        await expect(await this.guestInfo).toBeDisplayed({message: 'guest info is not displayed'});
        await expect(await this.localChargesInfo).toBeDisplayed({message: 'localCharges Info is not displayed'});
        //await expect(await this.durationInfo).toBeDisplayed({message: 'duration Info is not displayed'});
        //await expect(await this.cancellationInfo).toBeDisplayed({message: 'cancellation Info is not displayed'});
    }

    async hotelOfferInformationDisplayed(){
        await expect(await this.offerInfo).toBeDisplayed({message:'Offer is not displayed'});
    }

    async hotelOfferInformationNotDisplayed(){
        await expect(await this.offerInfo).not.toBeDisplayed({message:'Offer is displayed'});
    }

    async noImageIconValidation(){
        await expect(await (await this.hotelImage).getAttribute('src')).toEqual('/images/placeholder.svg');
    }

    async getCustomerRating(){
        return Number(await (await this.customerRating).getText());
    }
    
      

    async validateTotalPriceAndPerPersonPriceText() {        
        await expect(await this.totalPriceText).toBeDisplayed({ message: "Total Price Text is not displayed" })
        await expect(await this.totalPriceText).toHaveTextContaining('Total price from', { message: "Total Price Text is not matched" })
        await expect(await this.perPersonText).toBeDisplayed({ message: "PerPerson Price Text is not displayed" })
        await expect(await this.perPersonText).toHaveTextContaining('Per Person ', { message: "PerPerson Price Text is not matched" })
    }

    async captureTotalPrice() {        
        EstabHotelInformation.TotalPrice = Number(await CommonFunctions.RemoveCurrencyInfo(await (await this.hotelPrice).getText()))
        console.log("Total Price : "+EstabHotelInformation.TotalPrice)       
    }

    async capturePerPersonPrice() {        
        EstabHotelInformation.PerPersonPrice = Number(await CommonFunctions.RemoveCurrencyInfo(await (await this.perPersonPrice).getText()))
        console.log("Per person Price : "+EstabHotelInformation.PerPersonPrice)        
    }

    async verifyPerPersonpriceIsLessThenTotalPrice()
    {
        await expect(await EstabHotelInformation.PerPersonPrice).toBeLessThan(await EstabHotelInformation.TotalPrice)
    }

    async wasAndNowPriceDisplayed() {       
        await expect(await this.wasPrice).toBeDisplayed({ message: "Was Price is not displayed" })
        await expect(await this.hotelPrice).toBeDisplayed({ message: "Was Price is not displayed" })
    }

    async verifyNowPriceIsLessThenWasPrice()
    {
        await expect(await EstabHotelInformation.NowPrice).toBeLessThan(await EstabHotelInformation.WasPrice)
    }

    async captureWasAndNowPrice() {
        EstabHotelInformation.WasPrice = Number(await CommonFunctions.RemoveCurrencyInfo(await this.wasPrice.getText()))
        EstabHotelInformation.NowPrice = Number(await CommonFunctions.RemoveCurrencyInfo(await this.hotelPrice.getText()))
        console.log("Was Price "+ EstabHotelInformation.WasPrice)
        console.log("\nNow Price "+ EstabHotelInformation.NowPrice)
    }

    async getPerPersonPrice(){
        return Number(await (await this.perPersonPrice).getText());
    }
    async getBreadCrumbText(){
        return await (await this.breadcrumbText).getText();
    }
    async getHotelName(){
        return await (await this.hotelName).getText();
    }

    async getAllFacilities() {
        var allFacilities = [];
        var allRunTimeFacilities = await this.listOfFacilities;
        for (let item = 0; item < allRunTimeFacilities.length; item++) {
            var itemText = await allRunTimeFacilities[item].getText();
            allFacilities.push(itemText);
        }
        return allFacilities;
    }
    //#region [OverviewTab]

    //#region [Rooms Tab]
    async clickTab(tabName: string)
    {
        (await this.tabContainer(tabName)).scrollIntoView();
        (await this.tabContainer(tabName)).click();
    }
    //#endregion

    async isEstabPageTabSelected(tabName: string){
        if((await (await this.tabContainer(tabName)).getAttribute("tabindex"))=="0"){
            return true;
        }
        else{
            return false;
        }
        
    }

    async isEstabPageTabDisplayed(tabName: string){
        return await (await this.tabContainer(tabName)).isDisplayed();     
    }

    async verifyEstabTabDisplayedAndSelected(tabname: string, isDisplayed:boolean=true, isSelected: boolean = true){
        await (await this.tabContainer(tabname)).scrollIntoView();
        await browser.pause(1000);
        await expect(await this.isEstabPageTabDisplayed(tabname)).toEqual(isDisplayed);
        await expect(await this.isEstabPageTabSelected(tabname)).toEqual(isSelected);
    }

    async verifyOverviewSectionHeadings(){
        var sectionHeading = [OverviewTabHotelInfo, OverviewTabReviews, OverviewTabFacilities, OverviewTabHotelInfoOverview];
        await (await this.overviewItem()).forEach(async element => {
            const item: string = await element.getText();
            console.log("Heading under Overview is : ", item);
            await expect(sectionHeading.includes(item)).toEqual(true); 
       });
    }

    async verifyFacilitiesIconAndNameAreDisplayed(){     
        var countOfFacilities = Number(await (await this.facilitiesIconArray()).length);
        console.log("No. of Facilities displayed are : ", countOfFacilities);
        for(var index=1;index<=countOfFacilities;index++){
            await expect(await (await this.facilitiesIcon(index)).isDisplayed()).toEqual(true);
            await expect(await (await this.facilitiesDetail(index)).getText()).not.toEqual(null);
            console.log(index,'-', await (await this.facilitiesDetail(index)).getText());
        }
    }

    async getReviewCount(){
        try{
            return Number(await (await (await this.reviewNumber).getText()).split(" ")[2]);
        }
        catch(error){
            return 0;
        }
    }

    async verifyForSeeAllCustomerReviewButtonDisplay(){
        const noOfReviewCount = await this.getReviewCount();
        if(noOfReviewCount<=2){
            expect((await this.seeAllCustomerReviewsButton())).not.toBeDisplayed({message:'See All Customer Review Button is displayed'});
        }
        else{
            expect((await this.seeAllCustomerReviewsButton())).toBeDisplayed({message:'See All Customer Review Button is not displayed'});
        }
    }

    async verifyNoReviewMessage(expectedReviewTxt){
        var actualReviewText;
        await (await this.reviewText).scrollIntoView();
        actualReviewText = await (await this.noReviewText).getText();
        await expect(expectedReviewTxt).toEqual(actualReviewText);
    }

    async clickBreadcrumbLink(linkName: String){
              this.jsClick(await this.breadCrumbLink(linkName));
    }
    //#endregion
}

export default new HotelEstabPage;