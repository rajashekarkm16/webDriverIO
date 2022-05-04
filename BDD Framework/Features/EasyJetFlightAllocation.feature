Feature: EasyJetFlightAllocation
As a customer
I want to get easyjet flight preselected
In order to validate flight allocation
	
	Background: 
	Given Click on Accept and close cookies button

	#@TC_2813063 @TC_2813064 @v3livesanityUK
#Scenario Outline: Verify preselected easyjet flightcard
#Given I do a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
#When Select a random hotel from the results
#And Select random rooms and board type
#Then Preselected flight is a easyjet flight
#And Baggage information text should be displayed 
#And Baggage price sash is displayed
#Examples:
#| destination | departureLocation | departureDate | returnDate | guests      |
#| PMI         | LGW               | 01 05 2021    | 08 05 2021 | 2,0,0       |
#| PMI         | LGW               | 01 05 2021    | 08 05 2021 | 2,0,0:1,1,0 |

#@TC_2813065 @TC_2813066
#Scenario Outline: Verify flight allocation details in booking summary
#Given I do a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
#When Select a random hotel from the results
#And Select random rooms and board type
#Then Preselected flight is a easyjet flight
#When Confirm the pre selected flight
#Then Baggage is not available in the extras page
#And Custom baggage message is displayed
#And Flight allocation baggage details is displayed in booking summary
#Examples:
#| destination | departureLocation | departureDate | returnDate | guests |
#| PMI         | LGW               | 16 10 2021    | 23 10 2021 | 2,1,0  |
#
#@TC_2815425
#Scenario Outline: Verify flight allocation details on applying flight filter
#Given I do a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
#When Select a random hotel from the results
#And Select random rooms and board type
#Then Preselected flight is a easyjet flight
#When I open flight filters
#And Apply outbound departure time filter
#Then Preselected flight should not change
#And Baggage information text is displayed
#And Baggage price sash is displayed
#Examples:
#| destination | departureLocation | departureDate | returnDate | guests |
#| PMI         | LGW               | 16 10 2021    | 23 10 2021 | 2,1,0  |