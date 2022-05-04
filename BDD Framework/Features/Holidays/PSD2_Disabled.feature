
Feature: PSD2 Disabled Tests on Holidays
	In order to make hotel booking with PSD2 
	As a accounts manager
	I want to bookings to be complete

Background: 
Given Click on Accept and close cookies button

Scenario Outline: Verify Holiday booking with 3DS flow
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
	| destination | departure_airport | departure | return | guests | firstName | lastName   | Expected   |
	| Dubai       | London            | 20        | 4      | 2,0,0  | ThreeD    | Authorised | AUTHORISED |

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
	| destination | departure_airport | departure | return | guests | firstName | lastName | Expected   |
	| Alicante    | London            | 20        | 4      | 2,0,0  | Refused   | Refused  | AUTHORISED |


Scenario Outline: Verify holiday booking with PSD2 With Challenge flow
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
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGEIDENTIFIED       | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGENOTIDENTIFIED    | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGEVALIDERROR       | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | CHALLENGEIDENTIFIED       | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | CHALLENGEVALIDERROR       | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | BYPASSEDAFTERCHALLENGE    | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVONE | NOTENROLLED               | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVONE | UNKNOWNENROLLMENT         | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSIDENTIFIED    | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSNOTIDENTIFIED | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSVALIDERROR    | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDS     | BYPASSED                  | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVONE | CHALLENGEUNKNOWNIDENTITY  | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | CHALLENGEUNKNOWNIDENTITY  | AUTHORISED |
| Alicante    | London            | 20        | 4      | 2,0,0  | ThreeDSVTWO | FRICTIONLESSREJECTED      | AUTHORISED |
	
