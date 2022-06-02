import {apiConstantsTRUK,apiConstantsSM} from '../utilities/apiConstants'
export class CommonFunctions {

    async RemoveCurrencyInfo(cost: string) {        
        var regex = /\d+/g;
        if (cost.split('\n').length > 1)
            cost = cost.split('\n')[1];
        if (cost.includes("-"))
            return "-" + cost.match(regex)
        else
            return cost.match(regex)
    }

    async getRandomNumber(min, max){
        return Math.floor(Math.random()*(max-min+1)+min);
    }

    async getAPIConstants (domain:string) {
        if (domain.toLowerCase() == "truk")
             return await apiConstantsTRUK
        else
            return await apiConstantsSM
    }
}
export default new CommonFunctions;





