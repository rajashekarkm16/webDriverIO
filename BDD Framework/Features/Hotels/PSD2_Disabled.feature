
Feature: PSD2 Disabled Tests on Hotels
	In order to make hotel booking with PSD2 
	As a accounts manager
	I want to bookings to be complete

Background: 
Given Click on Accept and close cookies button

Scenario Outline: Verify hotel booking with 3DS flow
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Populate Hotel Guest details with first name <firstName> and last name <lastName>
And Choose the full payment hotel booking using VisaCredit payment and complete 3DS booking
Then Booking references of booked items are available
And Booked hotel details matches
And Validate hotel information, lead contact and price details in database
Examples: 
	| destination | departure | return | guests      | firstName | lastName   | Expected   |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeD    | Authorised | AUTHORISED |

Scenario Outline: Verify hotel booking directly refused 
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Populate Hotel Guest details with first name <firstName> and last name <lastName>
And Choose the full payment hotel booking using VisaCredit payment and complete booking
Then Booking should be declined
Examples: 
	| destination | departure | return | guests      | firstName | lastName | Expected |
	| London      | 60        | 5      | 2,1,0,2,0,0 | Refused   | Refused  | REFUSED  |



Scenario Outline: Verify hotel booking with PSD2 With Challenge flow
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Populate Hotel Guest details with first name <firstName> and last name <lastName>
And Choose the full payment hotel booking using VisaCredit payment and complete booking
Then Booking references of booked items are available
And Booked hotel details matches
And Validate hotel information, lead contact and price details in database
Examples: 
	| destination | departure | return | guests      | firstName   | lastName                  | Expected   |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGEIDENTIFIED       | AUTHORISED |
	| London      | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGENOTIDENTIFIED    | AUTHORISED |
	| Dubai       | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGEVALIDERROR       | AUTHORISED |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | CHALLENGEIDENTIFIED       | AUTHORISED |
	| London      | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | CHALLENGEVALIDERROR       | AUTHORISED |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | BYPASSEDAFTERCHALLENGE    | AUTHORISED |
	| Newyork     | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | NOTENROLLED               | AUTHORISED |
	| Dubai       | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | UNKNOWNENROLLMENT         | AUTHORISED |
	| Newyork     | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSIDENTIFIED    | AUTHORISED |
	| London      | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSNOTIDENTIFIED | AUTHORISED |
	| London      | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSVALIDERROR    | AUTHORISED |
	| Dubai       | 60        | 5      | 2,1,0,2,0,0 | ThreeDS     | BYPASSED                  | AUTHORISED |
	| Dubai       | 70        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGEUNKNOWNIDENTITY  | REFUSED    |
	| Tenerife    | 80        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | CHALLENGEUNKNOWNIDENTITY  | REFUSED    |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSREJECTED      | REFUSED    |


	
