 class HotelSearchResults
{
    async getHotelSearchResultsQuery(tripStateIdLocal )
    {
      const getHotelSearchResults = [
        {
          "query": `query HotelSearchResults($tripStateId: ID!) {
        hotelSearchResults(tripStateId: $tripStateId) {
          availabilitySearchId
          hotels {
            hotel {
              estabId
            }
          }
        }
      }`,
          "variables": `{
          "tripStateId": "${tripStateIdLocal}"
        }` 
        }]
        return getHotelSearchResults;   
    }
}

export default new HotelSearchResults;
