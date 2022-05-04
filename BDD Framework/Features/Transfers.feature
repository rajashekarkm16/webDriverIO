@v3regression @holidayregression
Feature:  Transfers
	In order to verify transfer
	As a tester
	I want to get transfer as a product in holidays bookings


Background: 
	Given Click on Accept and close cookies button

@TC_1636837	
Scenario Outline: Add new transfer when a transfer is already added
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Select option 1 from available transfers
And Select option 2 from available transfers
Then Transfer option 2 should be added
And Transfer option 1 should be removed
And Check selected transfer and price is updated in booking summary
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London            | 50        | 6      | 2,0,0  |

Scenario Outline: Validate remove transfer link on transfer card
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Select random transfers if transfer is available
And Remove the added transfer
Then Tansfer should be removed
And Transfers should be removed from booking summary
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London            | 80        | 6      | 2,0,0  |