import Globalisation from '../apiMethods/globalization'
import commonApiFunctions from '../commonApiFunctions';
import DestinationAutocompleter from '../apiMethods/destinationAutocompleter';
import HotelSearch from '../apiMethods/hotelSearch';
import HotelSearchResults from '../apiMethods/hotelSearchResults'
import HotelAvailability from '../apiMethods/hotelAvailability'
import SelectRooms from '../apiMethods/selectRooms'
import TransferExtras from '../apiMethods/transferExtras'
import SelectExtras from '../apiMethods/selectExtras'
import Checkout from '../apiMethods/checkout'
import CreateBooking from '../apiMethods/createBooking'
import { Given, When, Then } from '@wdio/cucumber-framework';
import  CommonFunctions  from '../utilities/commonFunctions';
import dateTimeUtility from '../utilities/dateTimeUtility';

var endPoint = "http://localhost:4000";
var destinationId = 0
let tripStateIdLocal = "";
let estabIdLocal = 0;
let roomBoardIdLocal = "";
let transferIdLocal = "";
let productIdLocal = "";
let extrasId = "160";




  When(/^I complete a hotel e2e booking via API for domain (.+) to destination (.+) for dates (.+) and (.+)$/, async (domain, destination, checkindaysfromcurrentday, duration)=> {
    
    let apiConstants  = await CommonFunctions.getAPIConstants(domain)
    let x_user_context =  `"cultureCode": "${apiConstants.cultureCode}","currencyCode": "${apiConstants.currencyCode}","domainId": ${apiConstants.currencyCode},"posId": ${apiConstants.posId},"tenantId": ${apiConstants.tenantId}}`
    let headers = {'Content-Type': 'application/json','x_user_context':x_user_context}


    const globalizationQuery = await Globalisation.getGlobalizationQuery(apiConstants.tenantId,apiConstants.url);
    let response = (await commonApiFunctions.executePostRequest(endPoint,globalizationQuery,headers))
    await expect(response.status).toEqual(200);

    const destinatonAutocompleterQuery = await DestinationAutocompleter.getDestinationAutocompleterQuery(destination);
    response = (await commonApiFunctions.executePostRequest(endPoint,destinatonAutocompleterQuery,headers))
    let data = await response.json() ;
    await expect(response.status).toEqual(200);
    destinationId =await  data[0].data.destinationAutocompleterResults.destinations[0].destinationId;
    console.log("Destination Id " + destinationId);

    // Currently single room
    const hotelSearchQuery = await HotelSearch.getHotelSearchQuery(destinationId,await dateTimeUtility.addOrSubtractDaysToCurrentDate(checkindaysfromcurrentday),await dateTimeUtility.addOrSubtractDaysToCurrentDate(Number(checkindaysfromcurrentday)+Number(duration)));
    response = (await commonApiFunctions.executePostRequest(endPoint,hotelSearchQuery,headers))
    data = await response.json();
    expect(response.status).toEqual(200);
    tripStateIdLocal = await data[0].data.hotelSearch.tripStateId;
    console.log("tripstate Id " + tripStateIdLocal);


    const hotelSearchResultsQuery = await HotelSearchResults.getHotelSearchResultsQuery(tripStateIdLocal)
    response = (await commonApiFunctions.executePostRequest(endPoint,hotelSearchResultsQuery,headers))
    data = await response.json();
    estabIdLocal = data[0].data.hotelSearchResults.hotels[0].hotel.estabId;
    console.log("Estab Id " + estabIdLocal);


    const hotelsAvailability = await HotelAvailability.getHotelAvailabilityQuery(estabIdLocal,tripStateIdLocal);
    response = (await commonApiFunctions.executePostRequest(endPoint,hotelsAvailability,headers))
    data = await response.json();
    roomBoardIdLocal = data[0].data.hotelAvailability.requestedRooms[0].availability[0].boardOptions[0].boardOptionId;
    console.log("Room Board Id " + roomBoardIdLocal);


    const selectRoomsQuery = await SelectRooms.getSelectRoomsQuery(estabIdLocal,tripStateIdLocal,roomBoardIdLocal)
    response = (await commonApiFunctions.executePostRequest(endPoint,selectRoomsQuery,headers))
    data = await response.json();

    const transfersExtrasQuery = await TransferExtras.getTransferExtrasQuery(tripStateIdLocal)
      response = (await commonApiFunctions.executePostRequest(endPoint,transfersExtrasQuery,headers))
      data = await response.json();
    transferIdLocal = data[0].data.getExtras[0].extras[0].id
    productIdLocal = data[0].data.getExtras[0].productId
    console.log("transfer id Local " + transferIdLocal);
    console.log("product id Local " + productIdLocal);


    const selectExtras =await SelectExtras.getSelectExtrasQuery(tripStateIdLocal,productIdLocal,extrasId,transferIdLocal)
      response = (await commonApiFunctions.executePostRequest(endPoint,selectExtras,headers))
      data = await response.json();

      const checkOutQuery = await Checkout.getCheckOutQuery(tripStateIdLocal);
    response = (await commonApiFunctions.executePostRequest(endPoint,checkOutQuery,headers))
    data = await response.json();

    let headersWithTrpUTM = {'Content-Type': 'application/json','x_user_context':x_user_context,'cookie': 'trpUTM=580595922771725078'}
    const createBookingQuery = await CreateBooking.getCreateBookingQuery(tripStateIdLocal);
    response = (await commonApiFunctions.executePostRequest(endPoint,createBookingQuery,headersWithTrpUTM));
    data = await response.json();
    console.log(data);
    console.log(data[0].errors);
    console.log("reponse status" + data.createBooking.trip[0]);
  });

  Then(/^Check booking id is created$/, async () => {
    
  });
  