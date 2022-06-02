class DestinationAutocompleter
{
    async getDestinationAutocompleterQuery(searchTerm :string)
    {
        const query = [
            {
              "query": `query DestinationAutocompleterResults($request: DestinationAutocompleterRequest!) {
                destinationAutocompleterResults(request: $request) {
                  destinations {
                    destinationId
                  }
                  hotels {
                    hotelId
                  }
                }
              }`,
              "variables": `{
                  "request": {
                    "searchTerm": "${searchTerm}"
                  }
                }`
            }
        ]
        return query;
    }
}
export default new DestinationAutocompleter;