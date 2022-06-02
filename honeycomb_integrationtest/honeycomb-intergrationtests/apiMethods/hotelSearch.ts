 class HotelSearch
{
    async getHotelSearchQuery(destinationId, checkInDate="2022-09-01", checkout ="2022-09-10" )
    {
        const hotelSearchQuery = [
            {
              "query": `mutation HotelSearch($request: HotelSearchRequest!) {
                hotelSearch(request: $request) {
                  tripStateId
                }
              }`,
              "variables": `{
                  "request": {
                    "checkInDate": "${checkInDate}",
                    "checkOutDate": "${checkout}",
                    "destinationId": ${destinationId},
                    "rooms": [
                      {
                        "adults":2 ,
                        "childAges": []
                      }
                    ]
                  }
                }`
            }]
        return hotelSearchQuery;   
    }
}

export default new HotelSearch;
