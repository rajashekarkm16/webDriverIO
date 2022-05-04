Feature: GoogleAnalytics
	In order to make sure that Google Tracking events are triggered
	As a marketing analyst
	I want to be sure that correct events are triggered

@mytag
Scenario: Validate GA Tracking on Hotel Search Results
	Given I am on hotel search results page
	Then Validate the GA Tracking

Scenario: Validate GA Tracking on Hotel Search Results fliter click
Given I am on hotel search results page
Then Validate the GA Tracking for filter button click


#Scenario Outline: Verify hotel booking with special request GA
#Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
#When Select a random hotel from the results
#And Select random rooms and board type
#And Check the selected rooms and total cost
#And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization and special request
#Then Booking references of booked items are available
#Then Validate GA Tracking for payment
#Examples: 
#	| destination | departure | return | guests |
#	| Tenerife    | 40        | 5      | 2,1,0  |

	#Pass/fail



