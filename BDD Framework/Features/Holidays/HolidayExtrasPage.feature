@holidayregression @v3regression
Feature: HolidayExtrasPage
	In order to add extras in Holiday booking 
	As a end user
	I want to choose a extra and validate the basket

Background: 
Given Click on Accept and close cookies button

@TC_1636895 @TC_1636896
Scenario Outline: Verify Baggage Include Message and Add Bag Section
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
Then Baggage included info should be displayed
And Add bag section should be dislayed 

Examples: 
	| destination    | departure_airport | departure | return | guests |
	| Tenerife South | London            | 80        | 4      | 2,0,0  |

@TC_1650632
Scenario Outline:Verify Baggage Added status
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Select random baggage
Then Baggage added status should be displayed


Examples: 
	| destination    | departure_airport | departure | return | guests |
	| Tenerife South | London            | 80        | 4      | 2,0,0  |

@TC_1650631 @MobileOnly
Scenario Outline:Verify Baggage added and removed Toast message
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Select random baggage
Then Baggage Added message is displayed on toast
And Baggage added status should be displayed
When Remove Baggage
Then Baggage Remove message is displayed on toast

Examples: 
	| destination    | departure_airport | departure | return | guests |
	| Tenerife South | London            | 80        | 4      | 2,0,0  |



