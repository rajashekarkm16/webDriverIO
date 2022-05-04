
Feature: PSD2 Enabled Tests on Holidays 
	In order to make hotel booking with PSD2 
	As a accounts manager
	I want to bookings to be complete

Background: 
Given Click on Accept and close cookies button

Scenario Outline: Verify holiday booking with PSD2 With Challenge flow
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Check selected holiday products and total price
And Populate Holiday Guest details with first name <firstName> and last name <lastName>
And Choose the full payment holiday booking using VisaCredit payment and complete 3DS booking
Then Booking references of booked items are available
And Booked holiday details matches
And Validate hotel information, lead contact and price details in database
Examples: 
	| destination | departure_airport | departure | return | guests | firstName   | lastName               | Expected   |
	| Alicante    | London            | 70        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGEIDENTIFIED    | AUTHORISED |
	| Alicante    | London            | 60        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGENOTIDENTIFIED | AUTHORISED |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGEVALIDERROR    | AUTHORISED |
	| Alicante    | London            | 70        | 4      | 2,0,0  | ThreeDSVTWO | CHALLENGEIDENTIFIED    | AUTHORISED |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeDSVTWO | CHALLENGEVALIDERROR    | AUTHORISED |
	| Alicante    | London            | 60        | 4      | 2,0,0  | ThreeDSVTWO | BYPASSEDAFTERCHALLENGE | AUTHORISED |



Scenario Outline: Verify holiday booking with PSD2 With frictionless flow
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Check selected holiday products and total price
And Populate Holiday Guest details with first name <firstName> and last name <lastName>
And Choose the full payment holiday booking using VisaCredit payment and complete booking
Then Booking references of booked items are available
And Booked holiday details matches
And Validate hotel information, lead contact and price details in database
Examples: 
	| destination | departure_airport | departure | return | guests | firstName   | lastName                  | Expected   |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeDSVONE | NOTENROLLED               | AUTHORISED |
	| Alicante    | London            | 70        | 4      | 2,0,0  | ThreeDSVONE | UNKNOWNENROLLMENT         | AUTHORISED |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSIDENTIFIED    | AUTHORISED |
	| Alicante    | London            | 60        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSNOTIDENTIFIED | AUTHORISED |
	| Alicante    | London            | 70        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSVALIDERROR    | AUTHORISED |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeDS     | BYPASSED                  | AUTHORISED |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeD      | Authorised           | AUTHORISED  |
	| Alicante    | London            | 80        | 4      | 2,0,0  | Refused     | Refused              | AUTHORISED  |


Scenario Outline: Verify holiday booking refused after challenge window
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Check selected holiday products and total price
And Populate Holiday Guest details with first name <firstName> and last name <lastName>
And Choose the full payment holiday booking using VisaCredit payment and complete 3DS booking
Then Booking should be declined
Examples: 
	| destination | departure_airport | departure | return | guests | firstName   | lastName                 | Expected |
	| Alicante    | London            | 80        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGEUNKNOWNIDENTITY | REFUSED  |
	| Alicante    | London            | 70        | 4      | 2,0,0  | ThreeDSVTWO | CHALLENGEUNKNOWNIDENTITY | REFUSED  |


Scenario Outline: Verify holiday booking directly refused
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Check selected holiday products and total price
And Populate Holiday Guest details with first name <firstName> and last name <lastName>
And Choose the full payment holiday booking using VisaCredit payment and complete booking
Then Booking should be declined
Examples: 
	| destination | departure_airport | departure | return | guests | firstName   | lastName             | Expected |
	| Alicante    | London            | 60        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSREJECTED | REFUSED  |
	

