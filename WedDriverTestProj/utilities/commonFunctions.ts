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
}
export default new CommonFunctions;





