import Page from './basePage'
import EstabHotelInformation from '../modals/estabHotelInformation'

export class HotelEstabPage extends Page {
    get hotelName() { return $("//h1"); }
    get starRating() { return $$("//span[contains(@class,'css-1efx0ee-StarRating')]//*[name()='svg']"); }
    get location() { return $("//div[contains(@class,'StyleUtility')]/h3"); }
    get customerRating() { return $$("//span[contains(@class,'Badge')]"); }
    get customerReviews() { return $("//p[contains(@class,'Body')]"); }
    get breadcrumText() {return $("//nav")}
    get breadbrumbLink(){return $("//div[contains(@class,'StyleUtility')]//nav//a")}
    get totalPrice(){return $$("//div[contains(@class,'Stacker')]/div[contains(@class,'Stacker')]/div")}  //could be 1 or 2
    get perPersonPrice(){return $("//strong")}

    private async  CaptureHotelInformation() {        
        EstabHotelInformation.HotelName = await (await this.hotelName).getText();
        EstabHotelInformation.StarRating = await (await this.starRating).length;
        EstabHotelInformation.Location = await (await this.location).getText();
        if((await this.totalPrice.length===2)){
            EstabHotelInformation.TotalPrice = await (await this.totalPrice[1]).getText();
        }
        EstabHotelInformation.CustomerRating = await(this.customerRating).getText();
        EstabHotelInformation.CustomerReviews = await(this.customerReviews).getText();
        return EstabHotelInformation;
    } 

    async waitForPageLoad(): Promise<void> {
        {
            await (await this.hotelName()).waitForExist({timeout:50000}); 
        }
    }
    async getCustomerRating(){
        return await (await this.customerRating).length;
    }
    async getPerPersonPrice(){
        return await (await this.perPersonPrice()).getText();
    }
    async getBreadCerumbText(){
        return await (await this.breadcrumText()).getText();
    }
    async getHotelName(){
        return await (await this.hotelName()).getText();
    }
}

export default new HotelEstabPage;