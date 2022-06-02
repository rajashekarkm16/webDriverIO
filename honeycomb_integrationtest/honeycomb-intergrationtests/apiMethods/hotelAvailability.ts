 class HotelAvailability
{
    async getHotelAvailabilityQuery(estabId, tripStateId )
    {
      const hotelsAvailabilityQuery = [
        {
          "query": `query HotelAvailability($estabId: Int!, $tripStateId: ID!) {
        hotelAvailability(estabID: $estabId, tripStateId: $tripStateId) {
          requestedRooms {
            availability {
              boardOptions {
                boardOptionId
              }
            }
          }
        }
      }`,
          "variables": `{
          "estabId": ${estabId},
          "tripStateId": "${tripStateId}"
        }`
        }]
        return hotelsAvailabilityQuery;   
    }
}

export default new HotelAvailability;
