export class HotelCardDetails
{
    HotelName : string;
    StarRating : number;
    Address:string;
    CustomerRating : number;
    CustomerReviewsCount : number;
    IsPriceIncludesSectionDisplayed : boolean;
    PriceIncludes : string[];
    Currency : string;
    TotalPrice : number;
    PerPersonPrice:number;
    StrikedOffPrice:number
    SecureTodayFromPrice : string;
}

export default new HotelCardDetails;