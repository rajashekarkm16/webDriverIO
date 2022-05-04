@v3regression @holidayregression
Feature: HolidayHotelEstabPage
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Holiday Hotel Estab Page


Background: 
Given Click on Accept and close cookies button

@TC_2292595 @TC_2292593 @TC_2292594
Scenario Outline: Verify search itinerary in holiday hotel estab page
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I select a random hotel from the results
Then I should see the selected hotel estab page
And Search itinerary is populated accordingly in estab page
Examples: 
| destination | departure_airport | departure | return | guests            |
| Tenerife    | London            | 55        | 7      | 2,1,1:1,1,0:1,0,0 |
| Tenerife    | London            | 90        | 7      | 2,1,1:1,0,0       |
| Tenerife    | London            | 45        | 10     | 2,1,1             |

@MobileOnly
Scenario: Holiday Price on Choose Board validation
	Given I am on holiday hotel estab page
	When I select random room
	Then I should see the board selection page
	And Holiday price should be displayed
	And Continue to Flights button should be displayed

@TC_2279491	
Scenario: Verify price includes section on the holiday hotel estab page
	Given I am on holiday hotel estab page
	Then Return flights link and board type details should be displayed under price includes section in estab page
	And Free cancellation message is displayed in estab page
    And Clicking the message opens free cancellation dialog modal on estab page

	@TC_2772538
Scenario Outline: Verify amount on pay monthly pill matches the pay monthly pop up modal on the estab page
	   Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	   When I select a random hotel from the results
       Then I should see the selected hotel estab page 
       And Pay monthly pill is displayed on estab page
       And Monthly deposit price matches in the secure today pop up modal on estab page
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London            | 200       | 10     | 2,1,1  |

	
