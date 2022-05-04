import { Given, When, Then } from '@wdio/cucumber-framework';
import HomePage from '../pageobjects/homePage';
import SearchUnit from '../components/searchUnit';
import Utility from '../utilities/dateTimeUtility'
import HotelSearchResultsPage from '../pageobjects/HotelSearchResultsPage';
import HotelEstabPage from '../pageobjects/hotelEstabPage';

Given(/^I am on hotel Estab page for Estab (.+)$/, async (estabName) => {

   await SearchUnit.selectDestination(estabName)

});

  
Given(/^Mandatory transfer message (.+) should be displayed in the hotel card $/, async (message) => {
    //HotelEstabPage

});