#@v3regression
Feature: HolidayPaymentsWithAllCardTypes
	In order to validate the card payments
	As a QA
	I want to make payments with various cards

	Background: 
Given Click on Accept and close cookies button

Scenario Outline: Holiday booking with card payment
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Complete the holiday booking with different <cardType>
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests | cardType         |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | Amex             |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | MasterCardCredit |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | Connect          |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | MasterCardDebit  |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | SwitchMaestro    |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | VisaDebitDelta   |
	| Tenerife    | London            | 80        | 6      | 2,1,0  | VisaElectron     |
