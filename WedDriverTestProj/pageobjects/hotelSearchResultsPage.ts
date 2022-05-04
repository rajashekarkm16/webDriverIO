import { OperationCanceledException } from 'typescript';
import {} from 'webdriverio';
import Page from '../pageobjects/basePage';
import hotelCardDetails, { HotelCardDetails }  from '../modals/hotelCardDetails';

class HotelSearchResultsPage extends Page {
   
    //#region  [Getter/Setters]

    //#region [Breadcrumb]

    get breadCrumb() { return $("//ol[contains(@class,'Breadcrumb')]"); }

    get breadCrumbList() { return $$("//ol[contains(@class,'Breadcrumb')]/li"); }

    //#endregion
  
    //#region [HotelCard]

    private hotelName(index:number =1){ return $("(//article)["+index+"]//h4/a"); }
    private starRating(index:number=1){ return $$("(//article)["+index+"]//span[contains(@class,'StarRating')]/*[local-name()='svg']"); }
    private address(index:number = 1){ return $("(//article)["+index+"]//div[@class='css-vzegvy-Stacker']/div[2]//h3"); }
    private customerRating(index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'FlexGrid')]/span[contains(@class,'Badge')]"); }
    private smileyIcon(index:number=1) { return $("(//article)["+index+"]//div[contains(@class,'FlexGrid')]/*[local-name()='svg']"); }
    private customerReviewsCount(index:number=1) { return $("(//article)["+index+"]//div[contains(@class,'FlexGrid')]/p[contains(@class,'Body')]"); }
    private secureTodayForPill(index:number=1){ return $("(//article)["+index+"]//div[span[contains(@class,'Pill')]]/span/p"); }
    private totalPrice(index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'CardContent')][2]//div[contains(@class,'Heading')]"); }
    private oldTotalPrice(index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'Stacker')]/div[contains(@class,'Stacker')]/div[1]"); }
    private newTotalPrice(index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'Stacker')]/div[contains(@class,'Stacker')]/div[2]"); }
    private perPersonPrice(index:number=1){ return $("(//article)["+index+"]//strong"); }
    private holidayPriceFromText (index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'CardContent')][2]//div[contains(@class,'Stacker')]//p[1]"); }
    private perPersonPriceFromText (index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'CardContent')][2]//div[contains(@class,'Stacker')]//p[2]"); }
    private viewMoreDetails(index:number=1){ return $("(//article)["+index+"]//div[contains(@class,'CardContent')][2]//a"); }
    private priceIncludesText(index:number=1){ return $("(//article)["+index+"]//p[@class='css-1oe1yf0-Body']"); }
    private listOfPriceIncludes (index:number=1){ return $$("(//article)["+index+"]//ul/li//p"); }
    private priceSummarySection(index:number=1){ return $("(//article)[1]//div[contains(@class,'CardContent')][2]"); }

    // tabIndex for enable/disabled
    private imageGalleryLeftButton (index:number=1){ return $("(//article)["+index+"]//div[@class='image-gallery-slide-wrapper bottom']//button[1]"); }
    private imageGalleryRightButton (index:number=1){ return $("(//article)["+index+"]//div[@class='image-gallery-slide-wrapper bottom']//button[2]"); }
    private imageGalleryPositionText(index:number=1){return $("(//article)["+index+"]//div[@class='image-gallery-slide-wrapper bottom']/div/div"); }
    private hotelPill(){ return $("(//article)[1]//div[contains(@class, 'Gallery')]//span[contains(@class,'Pill')]"); }
    private hotelPillGallery(){ return $("(//article)[1]//div[contains(@class, 'Gallery')]//span"); }
    private get listOfHotelCard() {return $$("//article"); }

   //#endregion

   //#endregion

    //#region  [Methods]

    async waitForPageLoad(index:number =1): Promise<void> { 
            await (await this.hotelName(index)).waitForExist({timeout:90000});     
    }
    async clickOnHotelName(index:number =1){
        await this.hotelName(index).click();
    }
    async getHotelNameByIndex(index:number = 1)
    {
        return await (await this.hotelName(index)).getText();
    }
    async getStarRatingByIndex(index:number=1)
    {
        return await (await this.starRating(index)).length;
    }
    async getAddressByIndex(index:number=1)
    {
        return await (await  this.address(index)).getText();
    }
    async isPriceSummarySectionDisplayed(index:number=1){
        return await (await  this.priceSummarySection(index)).isExisting();
    }
    async isTotalPriceDisplayed(index:number=1){
        return await (await  this.totalPrice(index)).isExisting();
    }
    async isPerPersonPriceDisplayed(index:number=1){
        return await (await  this.perPersonPrice(index)).isExisting();
    }
    async verifyPerPriceInsideBracket(substring:string, index:number=1){
        var isPriceNumber: boolean = false;
        var perPersoneText : string= await (await this.perPersonPrice(index)).getText();
        if((perPersoneText.includes('£'))||(perPersoneText.includes('('))||(perPersoneText.includes(')'))){
            const priceOnly:any = perPersoneText.replace('(', '').replace(')','').replace('£','').replace(substring,'');
            if(priceOnly.match(/^[0-9]+$/) != null){
                console.log('Per person price is number : ', priceOnly)
                isPriceNumber = true;
            }
            else{
                console.log('Per person price is not a number : ', priceOnly)
            }
        }
        else{
            console.log('Per person price format is not proper : ', perPersoneText)
        }
        await expect(isPriceNumber).toEqual(true);
    }

    async verifyTotalPriceAmount(index:number=1){
        var isPriceNumber: boolean = false;
        var totalPriceText : string= await (await this.totalPrice(index)).getText();
        if((totalPriceText.includes('£'))){
            const priceOnly:any = totalPriceText.replace('£','');
            if(priceOnly.match(/^[0-9]+$/) != null){
                console.log('Total price is number : ', priceOnly)
                isPriceNumber = true;
            }
            else{
                console.log('Total price is not a number : ', priceOnly)
            }
        }
        else{
            console.log('Total price format is not proper : ', totalPriceText)
        }
        return isPriceNumber;
    }
    async getTotalPriceText(index:number=1){
       return await (await this.holidayPriceFromText(index)).getText();

    }



    async getListOfPriceIncludes(index:number = 1)
    {
        const listOfPriceIncludeItems :string[] = new Array();
        await this.listOfPriceIncludes(index).forEach(async element => {
            listOfPriceIncludeItems.push(await element.getText())      
       });
       return listOfPriceIncludeItems;
    }

    async isHotelCardWithOldPriceAvailalble(clickOnCard:boolean=true){
        var found: boolean = false;
        try{
            const noOfHotel:number=await this.countOfHotelCard();
            console.log(noOfHotel);
            for(var i=1;i<noOfHotel;i++){
                await (await this.listOfHotelCard[i-1]).scrollIntoView();
                await browser.pause(1000);
                const strOldTotalVal = await (await this.oldTotalPrice(i)).getText();      
                console.log('Was Total Price is : ', strOldTotalVal);
                if(strOldTotalVal!=''){
                    const strNewTotalVal = await (await this.newTotalPrice(i)).getText(); 
                    console.log('Now Total Price : ', strNewTotalVal);
                    found = true;
                    if(clickOnCard){
                        await (await this.listOfHotelCard[i-1]).click();  
                    }         
                    break;
                }
            }
        }
        catch(Error){
            console.log('Error in isClickHotelCardWithOldPriceAvailalble() method')
        }
        return found;
    }

    async countOfHotelCard(){
        return await this.listOfHotelCard.length;
    }

    async verifyPriceIncludeListContains(priceIncludesItemMessage, hotelIndex:number=1){
       await expect(await (await this.getListOfPriceIncludes(hotelIndex)).includes(priceIncludesItemMessage)).toEqual(true);
    }

    async isHotePillDisplayed(){
        await (await this.hotelPillGallery()).waitForExist({ timeout: 50000 })
        await (await this.hotelPillGallery()).scrollIntoView();
        return await (await this.hotelPillGallery()).isExisting();
    }

    async getHotelPillText(){
        await (await this.hotelPill()).scrollIntoView();
        const pillText = await (await this.hotelPill()).getText();
        console.log('Pill text is : ', pillText);
        return await pillText;
    }

    async captureHotelInformation(index:number=1)
    {
        hotelCardDetails.HotelName  = await this.getHotelNameByIndex(index);
        hotelCardDetails.StarRating  = await this.getStarRatingByIndex(index);
        hotelCardDetails.Address  = await this.getAddressByIndex(index);
        hotelCardDetails.CustomerRating = Number(await (this.customerRating(index)).getText());
        hotelCardDetails.CustomerReviewsCount = Number((await (this.customerReviewsCount(index)).getText()).split(' ')[2]);
        hotelCardDetails.SecureTodayFromPrice = (await (await this.secureTodayForPill(index)).getText()).split(' ')[3];
        hotelCardDetails.TotalPrice =Number(await (await (this.totalPrice(index)).getText()));
        hotelCardDetails.PerPersonPrice = Number(await (await this.perPersonPrice(index)).getText());
        hotelCardDetails.Currency = await (await this.totalPrice(index)).getText();
        hotelCardDetails.IsPriceIncludesSectionDisplayed = await (await this.priceIncludesText(index)).isExisting();
        hotelCardDetails.PriceIncludes =await (await this.getListOfPriceIncludes(index));
        return hotelCardDetails;
    }   

    async verifyAverageReviewScore(){
        await this.captureHotelInformation();
        await expect((hotelCardDetails.CustomerRating)>0).toEqual(true);
    }

    async isSmileyIconExist(index : number = 0){
        return await (await this.smileyIcon(index)).isExisting();
    }
    async getSecureTodayPillText(index : number = 0){
        const texxt = await (await this.secureTodayForPill(index)).getText();
        console.log('Text displayed is : ',texxt);
        return texxt;
    }
    async clickSecureTodayPill(index : number = 0){
        await (await this.listOfHotelCard[index]).scrollIntoView();
        //await (await this.secureTodayForPill(index)).jsclick();
        await this.jsClick(await this.secureTodayForPill(index));
    }
    async isSecureTodayPillClickable(index : number = 0){
        return (await (await this.secureTodayForPill(index).isClickable()));
    }
    async getcustomerReviewsCount(index : number = 0){
        const reviewText = await (this.customerReviewsCount(index)).getText();
        const reviewCountText = reviewText.split(' ');  
        return Number(reviewCountText[2]);
     }

    async selectAHotel( index:number = 0) {
        try {
            await (await this.viewMoreDetails(index)).waitForExist({ timeout: 50000 })
            await (await this.viewMoreDetails(index)).scrollIntoView();
            await (await this.viewMoreDetails(index)).click();
        }
        catch (error) {
        }
    }

    async verifyHotelNameForAllHotels(){
        expect(await (await this.getAllHotelNames()).includes(null)).toEqual(false);
    }
    
    async verifyStarRatingForAllHotels(){   
        expect(await (await this.getListOfStarRatings()).includes(null)).toEqual(false);
    }

    async verifyAddressForAllHotels(){   
        expect(await (await this.getListOfAddress()).includes(null)).toEqual(false);
    }
    
    async getAllHotelNames(){
        var listOfHotel: string[]= new Array();
        var hotelsCounts: number = await this.countOfHotelCard();
        console.log('No Of Hotels are : ', hotelsCounts)
        for(var i=1; i<=hotelsCounts;i++){
            listOfHotel.push(await (await this.getHotelNameByIndex(i)));
        }
        return listOfHotel
    }

    async getListOfStarRatings(){
        var listOfStartRatings: number[]= new Array();
        var counts: number = await this.countOfHotelCard();
        console.log('Star Ratings of all ', counts, ' Hotels are : ')
        for(var i=1; i<counts;i++){
            listOfStartRatings.push(await (await this.getStarRatingByIndex(i)));
        }
        return listOfStartRatings;
    }

    async getListOfAddress(){
        var listOfAddress: string[]= new Array();;
        var counts: number = await this.countOfHotelCard();
        console.log('Respective address of all ', counts, ' Hotels are : ')
        for(var i=1; i<=counts;i++){
            listOfAddress.push(await (await this.getAddressByIndex(i)));
        }
        return listOfAddress;
    }
    //#endregion
}

export default new HotelSearchResultsPage();
