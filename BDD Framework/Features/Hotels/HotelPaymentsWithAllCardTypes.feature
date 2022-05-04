@hotelregression @v3regression @payments 
Feature: HotelPaymentsWithAllCardTypes
	In order to validate the card payments
	As a QA
	I want to make payments with various cards

	Background: 
Given Click on Accept and close cookies button

@TC_2778813 @TC_2778814 @TC_2778815 @TC_2778818 @TC_2778819 @TC_2778820 @TC_2768655 @TC_2768650 @TC_2768648
Scenario Outline: Hotel booking with card payment
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Complete the full payment hotel booking using <cardType> payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests | cardType         |
	| Tenerife    | London            | 30        | 6      | 2,0,0  | Amex             |
	| Tenerife    | London            | 30        | 6      | 2,0,0  | MasterCardCredit |
	| Tenerife    | London            | 40        | 6      | 2,0,0  | Connect          |
	| Tenerife    | London            | 30        | 6      | 2,0,0  | VisaDebitDelta   |
	| Tenerife    | London            | 30        | 6      | 2,0,0  | VisaElectron     |
	| Tenerife    | London            | 30        | 6      | 2,0,0  | VisaCredit       |


#           | Tenerife          | London    | 80     | 6      | 2,0,0            | MasterCardDebit |
	#| #           | Tenerife          | London    | 80     | 6      | 2,1,0            | SwitchMaestro |
	#@TC_2768648 @TC_2768655 @TC_2768650 @TC_2768651
