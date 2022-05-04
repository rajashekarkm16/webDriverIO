@v3regression @holidayregression 
Feature: HolidaySearchResultsPage
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Hotel serach results page

Background: 
Given Click on Accept and close cookies button

@TC_2292592 @TC_2292597 @TC_2292598
Scenario Outline: Verify search itinerary in holiday hotel search results page
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then Search itinerary is populated accordingly
Examples: 
| destination | departure_airport | departure | return | guests            |
| Tenerife    | London            | 55        | 7      | 2,1,1:1,1,0:1,0,0 |
| Tenerife    | London            | 90        | 7      | 2,1,1:1,0,0       |
| Tenerife    | London            | 45        | 10     | 2,1,1             |

Scenario Outline: Capture and verify hotel and flight information
Given I perform a mobile holiday search for <destination> from <departure_airport>
When I store hotel and pre-selected flight information and select the hotel from the results	
Then Hotel information on estab page should match with search results page
Then Flight details pop-up information on estab page should match with search results page
Examples: 
	| destination | departure_airport |
	| Tenerife    | London            |

#	@TC_1755991
#Scenario: Verify holiday search modal is displayed
#Given I am on holiday search results page
#When I click on search icon
#Then Holiday search modal with valid fields should be displayed

@TC_1755992
Scenario: Verify holiday filters modal is displayed
Given I am on holiday search results page
When I click on filter icon
Then Filter modal should be displayed

@v3livesanityUK
Scenario Outline: Verify hotel sort in holiday search results
#Accepts actual text from UI as test data. 
	Given I am on holiday search results page
	When I open sort options
	And Select the sort <option>
	Then Search results are sorted based on the selected <option>
	And Duplicate search results are not displayed
	Examples:
	| option                          |
	| Customer Rating (Highest first) |
	| Price (Cheapest first)          |
	| Star Rating (Highest first)     |

	@TC_1755996
Scenario: Verify Map view is displayed with holiday price
Given I am on holiday search results page
When I Click on Map view link
And I click on one of the hotel icon
And I click on the hotel name
Then Holiday price on map matches with the price on Estab page

@TC_1755998
Scenario: Verify price includes section on the hotel card
Given I am on holiday search results page
Then Return flights link and board type details should be displayed under price includes section 

@TC_1755999
Scenario: Verify flight details modal is displayed
Given I am on holiday search results page
When Click on Return flights link
Then Flight details modal should be displayed

@TC_1756004
Scenario: Verify holiday and per person price is displayed
Given I am on holiday search results page
Then Holiday price from and Per person price should be displayed

#Scenario: Verify holiday updated price is displayed on filters
#Given I am on holiday search results page
#When I click on filter icon
#And Move the slider
#Then Holiday price label should have updated price

