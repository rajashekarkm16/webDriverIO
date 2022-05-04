
Feature: PSD2 Enabled Tests on Hotels 
	In order to make hotel booking with PSD2 
	As a accounts manager
	I want to bookings to be complete

Background: 
Given Click on Accept and close cookies button


Scenario Outline: Verify hotel booking with PSD2 With Challenge flow
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
	| destination | departure | return | guests      | firstName   | lastName               | Expected   |
	| Tenerife    | 70       | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGEIDENTIFIED    | AUTHORISED |
	| Tenerife    | 65        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGENOTIDENTIFIED | AUTHORISED |
	| Tenerife    | 77        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGEVALIDERROR    | AUTHORISED |
	| Tenerife    | 40        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | CHALLENGEIDENTIFIED    | AUTHORISED |
	| Tenerife    | 70        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | CHALLENGEVALIDERROR    | AUTHORISED |
	| Tenerife    | 75        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | BYPASSEDAFTERCHALLENGE | AUTHORISED |


	Scenario Outline: Verify hotel booking with PSD2 With Frictionless flow
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
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | NOTENROLLED               | AUTHORISED |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | UNKNOWNENROLLMENT         | AUTHORISED |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSIDENTIFIED    | AUTHORISED |
	| Tenerife    | 70        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSNOTIDENTIFIED | AUTHORISED |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | FRICTIONLESSVALIDERROR    | AUTHORISED |
	| Tenerife    | 60        | 5      | 2,1,0,2,0,0 | ThreeDS     | BYPASSED                  | AUTHORISED |
	| Tenerife    | 50        | 5      | 2,1,0,2,0,0 | ThreeD      | Authorised           | AUTHORISED  |
	| Tenerife    | 50        | 5      | 2,1,0       | Refused     | Refused              | AUTHORISED  |



Scenario Outline: Verify hotel booking refused after challenge window
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Populate Hotel Guest details with first name <firstName> and last name <lastName>
And Choose the full payment hotel booking using VisaCredit payment and complete 3DS booking
Then Booking should be declined

Examples: 
	| destination | departure | return | guests      | firstName   | lastName                 | Expected |
	| Tenerife    | 20        | 5      | 2,1,0,2,0,0 | ThreeDSVONE | CHALLENGEUNKNOWNIDENTITY | REFUSED  |
	| Tenerife    | 70        | 5      | 2,1,0,2,0,0 | ThreeDSVTWO | CHALLENGEUNKNOWNIDENTITY | REFUSED  |

	
Scenario Outline: Verify hotel booking directly refused 
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Populate Hotel Guest details with first name <firstName> and last name <lastName>
And Choose the full payment hotel booking using VisaCredit payment and complete booking
Then Booking should be declined

Examples: 
	| destination | departure | return | guests      | firstName   | lastName             | Expected |
	| Tenerife    | 80        | 5      | 2,1,0,1,1,0 | ThreeDSVTWO | FRICTIONLESSREJECTED | REFUSED  |
	
