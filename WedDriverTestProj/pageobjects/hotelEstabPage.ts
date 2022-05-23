import Page from './basePage'
import EstabHotelInformation from '../modals/estabHotelInformation'
import CommonFunctions from '../utilities/commonFunctions';

export class HotelEstabPage extends Page {
    private get hotelName() { return $("//h1"); }
    private get starRating() { return $$("//span[contains(@class,'StarRating')]//*[name()='svg']"); }
    private get location() { return $("//div[contains(@class,'StyleUtility')]/h3[contains(@class,'Body')]"); }
    private get customerRating() { return $("//span[contains(@class,'Badge')]"); }
    private get customerReviews() { return $("//p[contains(@class,'Body')]"); }
    private get breadcrumbText() {return $("//nav")}
    private get breadbrumbLink(){return $("//div[contains(@class,'StyleUtility')]//nav//a")}
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
    private get noReviewText(){return $("//p[contains(@class,'cat-Body')]")}
    private get priceIncludesSection(){return $("//div[contains(@class,'dx-StyleUtility')]")}
    private get priceIncludeText(){return $("//p[contains(text(),'Price includes')]")}
    private get priceIncludeItems(){return $$("//p[contains(text(),'Price includes')]//following::ul/li")}
    private get boardTypeInfo(){return $("(//*[contains(@class,'KnifeAndForkIcon')]//following-sibling::p)[1]")}
    private get guestInfo(){return $("//*[contains(@class,'PeopleIcon')]//following-sibling::p")}
    private get localChargesInfo(){return $("//*[contains(@class,'PercentageIcon')]//following-sibling::p")}
    private get offerInfo(){return $("//*[contains(@class,'TagIcon')]//following-sibling::p")}
    private get selctedImage(){return $("//div[contains(@class,'image-gallery-slide  center')]")}
    private get hotelPrice() {
        return $("//div[contains(@class,'Stacker')]/div[contains(@class,'Heading')]");
    }

    private get wasPrice() {
        return $("//div[contains(@class,'Stacker')]/div[contains(@class,'Body')]");
    }

    private get totalPriceText() {
        return $("//div[contains(@class,'Stacker')]/p[1]");
    }

    private get perPersonText() {
        return $("//div[contains(@class,'Stacker')]/p[contains(@class,'Body')][2]");
    }  
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
            await (await this.hotelName).waitForDisplayed({timeout:50000}); 
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
        console.log("Hotel Price "+await (await this.hotelPrice).getText())      
        EstabHotelInformation.TotalPrice = Number(await CommonFunctions.RemoveCurrencyInfo(await (await this.hotelPrice).getText()))
        console.log("Total Price : "+await EstabHotelInformation.TotalPrice)      
        console.log("") 
    }

    async capturePerPersonPrice() {        
        EstabHotelInformation.PerPersonPrice = Number(await CommonFunctions.RemoveCurrencyInfo(await (await this.perPersonPrice).getText()))
        console.log("Per person Price : "+EstabHotelInformation.PerPersonPrice)  
        console.log("")      
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
}

export default new HotelEstabPage;