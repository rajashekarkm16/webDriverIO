@v3regression @holidayregression 
Feature: HolidayFlightFilters
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Holiday Flight filters


Background: 
	Given Click on Accept and close cookies button


#@TC_2774539 @NotLive
#Scenario Outline: Verify no filters button displayed when no flight results
#	Given I am on holiday flight search results for <destination> from <departure_airport>
#	And There are no alternate flight search results
#	Then Flight filter button should not be displayed
#Examples:
#	| destination | departure_airport |
#	| Singapore   | London            |

@TC_2600158
Scenario Outline: Verify close button on filter modal
Given I am on holiday flight search results for <destination> from <departure_airport>
And Store the flight search results
When I open flight filters
And I apply the available flight filters
And Click on close filter button
Then I want filter modal to be closed
And Flight Search Results should change as per filters applied
Examples:
	| destination | departure_airport |
	| Dubai       | London            |

@TC_2600159 @TC_2600204 
Scenario: Verify reset filters button
Given I am on holiday flight filters modal
When all available filters are selected
And I click on Reset filter button
Then all selected filters are cleared
And View Matches button count should same as it was before applying fliters

@TC_2600160
Scenario Outline: Verify number of stops filter
Given I am on holiday flight filters modal for <destination> from <departure_airport>
When Number of stops filter should be displayed if available
Then I should see available number of stops
And I can select more than one option and each selection should change to blue
And View Matches button count should be decreased
When I apply a Direct stop filter
Then I want to see flight results as per selected Direct stop
When I apply a One stop filter
Then I want to see flight results as per selected One stop
When I apply a Two stop filter
Then I want to see flight results as per selected Two stop
Examples:
| destination | departure_airport |
| Dubai       | London            |


@TC_2600162
Scenario Outline: Verify airport toggle filter
Given I am on holiday flight filters modal for Tenerife from London
When Departure and return to same airport toggle is displayed
Then I should be able to toggle ON the option
And selection should turn to blue
And View Matches button count should be decreased
When I click View Matches button
Then I want to see flight results as per selected airport toggle ON filter
When I open flight filters
And I toggle OFF the option 
Then selection should turn to grey
And View Matches button count should be increased 
When I click View Matches button
Then I want to see flight results as per selected airport toggle OFF filter

@TC_2600183
Scenario Outline: Verify departure airport filter
Given I am on holiday flight filters modal for <destination> and <departure_airport>
When Departure airport filter should be displayed
Then I should see available Departure Airports
And I can select multiple departure airports and selection should change to blue
And View Matches button count should be decreased
When I select departure airport <filter>
When I click View Matches button
Then I want to see flight results as per selected departure airport <filter>
Examples:
| destination | departure_airport | filter         |
| Tenerife    | London            | London Gatwick |

#@TC_2600191
#Scenario: Verify outbound flight time departure slider
#Given I am on holiday flight filters modal
#When Outbound Departure time slider should be displayed
#Then I can adjust the time with left and right sliders
#And Outbound Departure time should update
#And View Matches button count should be updated

#@TC_2608141
#Scenario: Verify return flight time departure slider
#Given I am on holiday flight filters modal
#When Return Departure time slider should be displayed
#Then I can adjust the time with left and right sliders
#And Return Departure time should update
#And View Matches button count should be updated

@TC_2774538
Scenario Outline: Departure Airport filter should not be displayed- When only one airport available 
Given I am on holiday flight search results for <destination> from <departure_airport>
And Departure airport on flights srp is having only <departure_airport>
When I open flight filters
Then Departure airport filter should not be displayed
Examples:
	| destination | departure_airport |
	| Dubai       | London Gatwick    |

@TC_2608139 @v3livesanityUK
Scenario Outline: Verify airlines filter
Given I am on holiday flight filters modal
When Airline filter is displayed
Then I should see available Airlines
And I can select multiple Airlines and checkbox should be selected
And View Matches button count should be decreased
When I click View Matches button
Then I want to see flight results as per selected airlines filter
Examples:
	| destination | departure_airport |
	| Tenerife    | London            |

@TC_2762694 @v3livesanityUK  
Scenario Outline: Verify flight filters modal
Given I am on holiday flight filters modal for <destination> from <departure_airport>
Then Reset all button should be displayed
And Number of stops filter should be displayed if available
And Departure and return to same airport toggle should be displayed
And Departure airport filter should be displayed
And Outbound Departure time slider should be displayed
And Return Departure time slider should be displayed
And Airline filter should be displayed
Examples:
	| destination | departure_airport |
	| Dubai       | London            |


