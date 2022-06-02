 class SelectExtras
{
    async getSelectExtrasQuery(tripStateId, productId, extrasId, transfersId, extraType ="HotelExtra",passengerIndex=null,quantity=1,roomIndex=0)
    {
      const selectExtrasQuery = [
        {
          "query": `mutation SelectExtra($request: ExtraRequest!) {
      selectExtra(request: $request) {
        tripStateId
      }
    }`,
          "variables": `{
        "request": {
          "tripStateId": "${tripStateId}",
          "productId": "${productId}",
          "options": [
            {
              "extraId": "${extrasId}",
              "extraOptionId": "${transfersId}",
              "extraType": "${extraType}",
              "passengerIndex": ${passengerIndex},
              "quantity": ${quantity},
              "roomIndex": ${roomIndex}
            }
          ]
        }
      }`
        }]
        return selectExtrasQuery;   
    }
}

export default new SelectExtras;
