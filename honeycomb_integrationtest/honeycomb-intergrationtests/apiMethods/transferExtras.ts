 class TransferExtras
{
    async getTransferExtrasQuery(tripStateId)
    {
      const getTransfersExtrasQuery = [
        {
          "query": `query GetExtras($tripStateId: ID!) {
      getExtras(tripStateId: $tripStateId) {
        extraType
        extras {
          description
          id
          isSelected
          ... on TransferExtra {
            appliesToRoom
            description
            id
            isSelected
            price
            priceFormatted
          }
          
        }
        productId
      }
    }`,
          "variables": `{
        "tripStateId": "${tripStateId}"
      }`
        }]
        return getTransfersExtrasQuery;   
    }
}

export default new TransferExtras;
