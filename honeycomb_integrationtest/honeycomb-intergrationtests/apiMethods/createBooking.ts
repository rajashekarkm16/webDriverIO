 class CreateBooking
{
    async getCreateBookingQuery(tripStateId,firstName ="svs",lastName="prasad",mobileNumber ="8788787787",email ="tsollars@travelrepublic.co.uk" )
    {
      const createBookingQuery = [
        {
          "query": `mutation CreateBooking($request: CreateBookingRequest!) {
            createBooking(request: $request) {
              ... on SuccessfulBookingResponse {
                trip
              }
              ... on UnexpectedbookingResponse {
                message
              }
              ... on ValidationErrorBookingResponse {
                messages
              }
              ... on PaypalBookingResponse {
                acsUrl
              }
              ... on PostPaymentErrorBookingResponse {
                message
              }
              ... on PrePaymentErrorBookingResponse {
                message
              }
              
              ... on ThreeDSBookingResponse {
                acsUrl
                canByPass
                mD
                paReq
                prePaymentID
                tL
                termUrl
              }
              ... on RefreshBookingResponse {
                basketId
                navls {
                  newProductId
                  product {
                    detailsDate
                    productId
                    productType
                    totalAmount
                  }
                }
                staleProducts {
                  newProductId
                  product {
                    detailsDate
                    productId
                    productType
                    totalAmount
                  }
                }
              }
            }
          }`,
          "variables": `{
            "request": {
              "applicationType": 7,
              "tripStateId": "${tripStateId}",
              "userData": {
                "connectingFlight": {
                  "inboundDate": "2022-07-24",
                  "inboundFlightNumber": "TS1234",
                  "outboundDate": "2022-07-31",
                  "outboundFlightNumber": "TS4321"
                },
                "contact": {
                  "countryCode": "UK",
                  "address": "147 London Road",
                  "city": "Kingston",
                  "email": "${email}",
                  "firstName": "${firstName}",
                  "lastName": "${lastName}",
                  "mobileTel": "${mobileNumber}",
                  "postCode": "kt22 6nh",
                  "title": "Mr",
                  "emailSignUp":false
                },
                "paymentDetails": {
                  "cardDetails": {
                    "cardId": null,
                    "cardNumber": "4444333322221111",
                    "cardType": 1,
                    "expiryDate": {
                      "month": 9,
                      "year": 2023
                    },
                    "issueNumber": null,
                    "maskedCardNumber": "4444333322221111",
                    "paymentTypeId": 1,
                    "securityCode": "123",
                    "startDate": {
                      "month": 9,
                      "year": 2020
                    }
                  },
                  "payFullAmount": true
                },
                "rooms": [
                  {
                    "guests": [
                      {
                        "age": 21,
                        "firstName": "svs",
                        "lastName": "prasad",
                        "title": "Mr",
                        "type": 1
                      }
                    ]
                  }
                ]
              }
            }
          }`
        }]
        return createBookingQuery;   
    }
}

export default new CreateBooking;
