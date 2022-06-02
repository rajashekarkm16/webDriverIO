 class Checkout
{
    async getCheckOutQuery(tripStateId)
    {
      const checkOutQuery = [
        {
          "query": `mutation Checkout($request: CheckoutRequest!) {
            checkout(request: $request) {
              preparedBooking {
                bookingProcessId
              }
              tripStateId
            }
          }`,
          "variables": `{
            "request": {
              "tripStateId": "${tripStateId}"
            }
          }`
        }]
        return checkOutQuery;   
    }
}

export default new Checkout;
