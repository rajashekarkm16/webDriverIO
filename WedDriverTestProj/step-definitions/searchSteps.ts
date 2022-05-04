import { Given, When, Then } from '@wdio/cucumber-framework';
import HomePage from '../pageobjects/homePage';
import DateTimeUtility from '../utilities/dateTimeUtility'
import { SearchCriteria } from '../modals/searchCriteria';
import SearchUnit from '../components/searchUnit';

//#region [Home Page Steps]

Given(/^I am on the \'(.*)\' page$/, async (pageName) => {
	await HomePage.open(pageName);
});

When(/^I select \'(.*)\' tab$/, async (tabName) => {
	await SearchUnit.selectTab(tabName);
});

When(/^I click search button$/, async () => {
	await SearchUnit.clickonSearchButton();
});

//#region [Destination]

When(/^I select destination as \'(.*)\'$/, async (destination) => {
	SearchCriteria.Destination = destination;
	await SearchUnit.selectDestination(destination);
});

Then(/^Destination placeholder should be \'(.*)\'$/, async (placeHolderName) => {
	await SearchUnit.verifyDestinationPlaceholder(placeHolderName);
});


When(/^I open destination autocompleter$/, async () => {
	await SearchUnit.openDestinationAutoCompleter();
});

Then(/^Destination autocompleter header should be \'(.*)\'$/, async (alertHeader) => {
	await SearchUnit.verifyDestinationAutoCompleterHeader(alertHeader);
});

Then(/^Destination autocompleter category header should be \'(.*)\'$/, async (alertCategoryHeader) => {
	await SearchUnit.verifyDestinationAutoCompleterCategoryHeader(alertCategoryHeader);
});

Then(/^Destination autocompleter category header should have map icon$/, async () => {
	await SearchUnit.verifyDestinationAutoCompleterCategoryIcon();
});

Then(/^Destinations in autocompleter should show hotel count$/, async () => {
	await SearchUnit.verifyHotelCountInDestinationAutoCompleter();
});

Then(/^I close destination autocompleter$/, async () => {
	await SearchUnit.closePopUp();
});

Then(/^Autocompleter should be closed$/, async () => {
	await SearchUnit.verifyDestinationAutoCompleterClosed();
});

Then(/^Placeholder,label and destination section should be in red color$/, async () => {
	await SearchUnit.verifyDestinationErrors();
});

When(/^I enter destination \'(.*)\'$/, async (destinationName) => {
	await SearchUnit.populateDestination(destinationName);
});

When(/^I select \'(.*)\' from destination autocompleter alert using keyboard$/, async (destinationName) => {
	await SearchUnit.selectDestinationFromAutocompleterUsingKeyborad(destinationName);
});

Then(/^Destination should display \'(.*)\'$/, async (searchResult) => {
	await SearchUnit.verifyDestination(searchResult);
});

When(/^Destination autocompleter should display \'(.*)\'$/, async (searchResult) => {
	await SearchUnit.verifyResultsInDestinationAutoCompleter(searchResult);
});

Then(/^Destination autocompleter subcategory header should be \'(.*)\'$/, async (alertSubCategoryHeader) => {
	await SearchUnit.verifyDestinationAutoCompleterSubCategoryHeader(alertSubCategoryHeader);
});

When(/^I select \'(.*)\' from destination autocompleter search result$/, async (searchResult) => {
	await SearchUnit.selectDestinationFromAutoCompleter(searchResult);
});

Then(/^Focus should be in \'(.*)\' (?:.*?)$/, async (activeAlertText) => {
	await SearchUnit.verifyActiveElementText(activeAlertText);
});

Then(/^Destination autocompleter subcategory header should have hotel icon$/, async () => {
	await SearchUnit.verifyDestinationAutoCompleterHotelIcon();
});

Then(/^Destination autocompleter should display no matches found$/, async () => {
	await SearchUnit.verifyNoMatchesFound();
});

//#endregion [Destination]

//#region [Dates]

When(/^I select a checkin date which is \'(.*)\' days ahead from now and checkout \'(.*)\' days later$/, async (checkInDate, checkOutDate) => {
	SearchCriteria.CheckinDate = await DateTimeUtility.addOrSubtractDaysToCurrentDate(checkInDate);
	SearchCriteria.CheckoutDate = await DateTimeUtility.addOrSubtractDaysToCurrentDate(Number(checkInDate) + Number(checkOutDate));
	await SearchUnit.selectDate(SearchCriteria.CheckinDate);
	await SearchUnit.selectDate(SearchCriteria.CheckoutDate);
	await SearchUnit.closeCalenderPopUp();
});


When(/^I select a checkin date \'(.*)\' days ahead from now$/, async (checkInDate) => {
	SearchCriteria.CheckinDate = await DateTimeUtility.addOrSubtractDaysToCurrentDate(checkInDate);
	await SearchUnit.selectDate(SearchCriteria.CheckinDate);
});

When(/^I select a checkout date \'(.*)\' days later$/, async (checkOutDate) => {	
	const checkInDaysFromToday= await DateTimeUtility.getDateDifference(await DateTimeUtility.addOrSubtractDaysToCurrentDate(0),SearchCriteria.CheckinDate,"days")
	SearchCriteria.CheckoutDate = await DateTimeUtility.addOrSubtractDaysToCurrentDate(Number(checkInDaysFromToday) + Number(checkOutDate));	
	await SearchUnit.selectDate(SearchCriteria.CheckoutDate);
});

When(/^I open calender modal$/, async () => {
	await SearchUnit.openCalenderForm();
});

When(/^I select current month from the calender$/, async () => {
	var currentDate = await DateTimeUtility.addOrSubtractDaysToCurrentDate(0);
	await SearchUnit.selectMonthFromCalender(currentDate);
});

Then(/^Header should be 'Dates'$/, async () => {
	await SearchUnit.verifyDateHeader();
});

Then(/^Date placeholder should be \'(.*)\'$/, async (placeholderText) => {
	await SearchUnit.verifyDatePlaceholder(placeholderText);
});

Then(/^Date modal header should be \'(.*)\'$/, async (modalHeader) => {
	await SearchUnit.verifyDateModalHeader(modalHeader);
});

Then(/^Close date modal icon should be displayed$/, async () => {
	await SearchUnit.verifyCrossIconDisplayed();
});

When(/^I click on cross icon$/, async () => {
	await SearchUnit.closeCalenderPopUp()
});

Then(/^Calendar modal should be closed$/, async () => {
	await SearchUnit.verifyCalendarModalClosed()
});

Then(/^Calendar modal should be opened$/, async () => {
	await SearchUnit.verifyCalendarModalOpened()
});

Then(/^Current and next month name and year text should be displayed$/, async () => {
	await SearchUnit.verifyCurrentAndNextMonthText();
});

Then(/^Current day should be highlighted$/, async () => {
	await SearchUnit.verifyDefaultSelectedDay();
});

Then(/^Footer message should display \'(.*)\'$/, async(footerMessage) => {
	await SearchUnit.validateFooterMessage(footerMessage);
});

Then(/^Footer message should display selected checkin date and checkout date$/, async() => {
	await SearchUnit.validateDateSelectedInFooter(SearchCriteria.CheckinDate,SearchCriteria.CheckoutDate);
});

Then(/^Footer message should display number of nights$/, async() => {
	await SearchUnit.validateNumberOfNightsInFooter(SearchCriteria.CheckinDate,SearchCriteria.CheckoutDate);
});

Then(/^Reset link should not be displayed$/, async() => {
	await SearchUnit.verifyResetLinkNotDisplayed();
});

When(/^I click on reset$/, async () => {
	await SearchUnit.clickReset();
});

Then(/^Reset link should be displayed$/, async () => {
	await SearchUnit.verifyResetLinkDisplayed();
});


Then(/^Done button should not be displayed$/, async() => {
	await SearchUnit.verifyDoneButtonNotEnabled()
});


Then(/^Done button should be enabled and displayed$/, async() => {
	await SearchUnit.verifyDoneButtonDisplayed();
	await SearchUnit.verifyDoneButtonEnabled();
});

When(/^I click on done$/, async() => {
	await SearchUnit.clickDoneonCalendar();
});

Then(/^Selected checkin date should be displayed in date field$/, async () => {
	await SearchUnit.validateCheckinDateSelectedInDateField(SearchCriteria.CheckinDate);
});

Then(/^Checkin date should be highlighted in date modal$/, async() => {
	await SearchUnit.verifyCheckiDateHighlightedInCalendarModal(SearchCriteria.CheckinDate);
});



Then(/^Selected checkin date and checkout date should be displayed in date field$/, async() => {
	await SearchUnit.validateDateSelectedInDateField(SearchCriteria.CheckinDate,SearchCriteria.CheckoutDate);
});

Then(/^Previous icon should be disabled$/, async () => {
	await SearchUnit.verifyPreviousIconDisabled();
});

Then(/^Next icon should be enabled$/, async () => {
	await SearchUnit.verifyNextIconEnabled();
});

When(/^I click on next icon$/, async() => {
	await SearchUnit.clickonNextIcon();
});

Then(/^I should be able to see the next month$/, async() => {
	await SearchUnit.verifyIsNextMonthDisplayed();
});

Then(/^Previous icon should be enabled$/, async() => {
	await SearchUnit.verifyPreviousIconEnabled();
});

When(/^I click on previous icon$/, async() => {
	await SearchUnit.clickonPreviousIcon();
});

Then(/^It should navigate to previous month$/, async() => {
	await SearchUnit.verifyIsPreviousMonthDisplayed();
});

Then(/^calendar should display 36 months from current month$/, async() => {
	await SearchUnit.verifyNumberOfMonthsTobedisplayedinCalendar();
});

Then(/^Days before today should be disabled$/, async() => {
	await SearchUnit.verifyDisableDaysBeforeToday();
});

//#endregion [Dates]

//#region [Guests Feature Step]
When(/^I add room and guest information with data \'(.*)\'$/, async (roomData) => {
	SearchCriteria.RoomAndGuestInformation = roomData;
	await SearchUnit.selectRoomAndGuests(roomData);
});

Then(/^Guests should display \'(.*)\'$/, async (totalGuestsInfo) => {
	await SearchUnit.verifyGuests(totalGuestsInfo);
});

When(/^I open Rooms & guests modal$/, async () => {
	await SearchUnit.openRoomsAndGuestsModal();
});

Then(/^Room \'(.*)\' should be selected$/, async (roomNumber) => {
	await SearchUnit.verifyRoomsSection(roomNumber);
});

Then(/^Adults count should display \'(.*)\' for Room \'(.*)\'$/, async (adultsCount, roomNumber) => {
	await SearchUnit.verifyAdultsCountInRooms(roomNumber, adultsCount);
});

Then(/^Children count should display \'(.*)\' for Room \'(.*)\'$/, async (childrenCount, roomNumber) => {
	await SearchUnit.verifyChildrenCountRoomWise(roomNumber, childrenCount);
});

When(/^I select room \'(.*)\'$/, async (roomCount) => {
	await SearchUnit.selectRoom(roomCount);
});

Then(/^Room \'(.+)\' should be in \'(.+)\' color$/, async (roomNumber, colorName) => {
	await SearchUnit.verifyColorForRoom(roomNumber, colorName);
});

When(/^I select \'(.*)\' adult for room \'(.*)\'$/, async (adultCount, roomNumber) => {
	await SearchUnit.selectAdult(roomNumber, adultCount);
});

Then(/^Adults decrease icon should be disabled for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.disabledCheckForDecreaseIconForAdults(roomNumber);
});

Then(/^Adults increase icon should be disabled for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.disabledCheckForIncreaseIconForAdults(roomNumber);
});

Then(/^Adults decrease icon should be enabled for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.enabledCheckForDecreaseIconForAdults(roomNumber);
});

Then(/^Adults increase icon should be enabled for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.enabledCheckForIncreaseIconForAdults(roomNumber);
});

Then(/^Help message should be \'(.*)\'$/, async (helpText) => {
	await SearchUnit.verifyHelpMessage(helpText);
});

Then(/^Room information should be \'(.*)\'$/, async (roomsInformation) => {
	await SearchUnit.verifyRoomsInformation(roomsInformation);
});

Then(/^Adults information should be \'(.*)\'$/, async (adultsInformation) => {
	await SearchUnit.verifyAdultsInformation(adultsInformation);
});

Then(/^Children information should be \'(.*)\'$/, async (childrenInformation) => {
	await SearchUnit.verifyChildrenInformation(childrenInformation);
});

When(/^I close Rooms & guests modal$/, async () => {
	await SearchUnit.closePopUp();
});

Then(/^Rooms & guests modal should be closed$/, async () => {
	await SearchUnit.verifyRoomsAndGuestsModalClosed();
});

When(/^I select \'(.*)\' children for room \'(.*)\'$/, async (childrenCount, roomNumber) => {
	await SearchUnit.selectChildren(roomNumber, childrenCount);
})

Then(/^Children decrease icon should be disabled for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.disabledCheckForDecreaseIconForChildren(roomNumber);
});

Then(/^Children increase icon should be disabled for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.disabledCheckForIncreaseIconForChildren(roomNumber);
});

Then(/^Done button should be enabled$/, async () => {
	await SearchUnit.enabledCheckForDoneButton();
});

Then(/^Done button should be disabled$/, async () => {
	await SearchUnit.disabledCheckForDoneButton();
});

Then(/^Child age box should not displayed for Room \'(.*)\'$/, async (roomNumber) => {
	await SearchUnit.verifyChildAgeBoxNotDisplayed(roomNumber);
});

Then(/^Age validation message should be 'Please enter ages of all children'$/, async () => {
	await SearchUnit.verifyAgeValidationMessage();
});

Then(/^Age validation message should be in red color$/, async () => {
	await SearchUnit.verifyAgeValidationMessageInRedColor();
});

Then(/^Child \'(.+)\' age box outline should be in red color for Room \'(.+)\'$/, async (childNumber, roomNumber) => {
	await SearchUnit.verifyChildAgeBoxInRedColor(roomNumber, childNumber);
});

Then(/^Placeholder,label and Guests section should be in red color$/, async () => {
	await SearchUnit.verifyGuestsErrors();
});

When(/^I (?:.*?) child age \'(.+)\' for child number \'(.+)\' in Room \'(.+)\'$/, async (childAge, childNumber, roomNumber) => {
	await SearchUnit.selectChildAge(roomNumber, childNumber, childAge);
});

Then(/^Child Age should be \'(.+)\' for child number \'(.+)\' in Room \'(.+)\'$/, async (childAge, childNumber, roomNumber) => {
	await SearchUnit.verifyChildrenAgeRoomWise(roomNumber, childNumber, childAge);
});

//#endregion [Guest Feature Step]

//#endregion [Home Page Steps]








