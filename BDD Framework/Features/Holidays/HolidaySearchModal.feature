@v3regression @holidayregression
Feature: HolidaySearchModal
	In order to perform a holiday search
	As a end user
	I want to edit the search criteria

Background: 
Given Click on Accept and close cookies button
@TC_2774610
Scenario Outline: Verify pre-populated details on estab page search modal
	Given I perform a holiday search for <destination> from <departure_airport>
	When I select a random hotel from the results
	And Click on search icon
	Then Destination should be pre-populated as per initital search details <destination>
	And Destination field should be editable
	And Hotel name on search modal should match with the selected hotel
	And Hotel name should not be editable
	And Passenger details should be pre-populated as per initital search details
	And Dates should be pre-populated as per initital search details	
	Examples: 
	| destination | departure_airport |
	| Tenerife    | London            |
@TC_2762609
Scenario Outline: Verify No rooms available message and check search alternative dates button
	Given I perform a holiday search for <destination> from <departure_airport>
	When I select a random hotel from the results
	And I update <guests> and change <departure> and <return> dates
	Then No rooms available <message> should be displayed
	When I click Search alternative date button
	Then Datepicker modal should be displayed with previous selected <departure> and <return> dates
Examples:
| destination | departure_airport | departure | return | guests | message                                                    |
| Dubai       | London            | 1         | 7      | 12,1,1 | We couldn't find any rooms available for your chosen dates |

	@TC_2774751 @TC_2762623 @TC_2292598
Scenario Outline: Verify occupancy modal when all required fields for the search is not populated
	Given I am on holiday search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click guests field
	And Populate guests without child age <guests>
	And Confirm the guests
	Then Verify error message prompting to enter child age is displayed for each room
	When Populate guests with child age <guests>
	And Confirm the guests	
	Then Passenger details should be updated as per selection on search modal
Examples:
| guests      |
| 2,1,1:1,1,0 |

@TC_2762618 @TC_2762620 @TC_2292599
Scenario Outline: Verify pre-populated details on occupancy modal
	Given I perform a holiday search for <destination> from <departure_airport>
	When I select a random hotel from the results
	And Click on search icon
	And Click guests field
	Then Guests details should be pre-populated as per the initital search
	Examples: 
	| destination | departure_airport |
	| Tenerife    | London            |

	@TC_2762615 @TC_2774739 @TC_2762618 @TC_2774742 @TC_2774743 @TC_2292600
Scenario: Add and remove rooms on occupancy modal
	Given I am on holiday search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click guests field
	And Click on number of rooms as '2'
	Then Guest modal should display 2 rooms details
	When Click on number of rooms as '3'
	Then Guest modal should display 3 rooms details
	When Click on number of rooms as '2'
	Then Guest modal should display 2 rooms details

	@TC_2774750 @TC_2292603
Scenario Outline: Max and Min adults validation on occupancy modal
	Given I am on holiday search results page
	When I select a random hotel from the results
	And I click on search icon
	And Edit guests <guestsData>
	And Click guests field
	Then Verify maximum of 12 adults can be selected
	And Veriy minimum of 1 adults should be selected
Examples:
| guestsData  |
| 2,1,1:1,1,0 |

@TC_2762619 @TC_2774745 @TC_2774746 @TC_2774748 @TC_2774794 @TC_2774808 @TC_2292604
Scenario: Max and Min children validation on occupancy modal
	Given I am on holiday search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click guests field
	Then Verify maximum of 12 children can be selected
	And Veriy minimum of 0 children can be selected

	@TC_2762620 @TC_2292605
Scenario Outline: Verify pre-populated details on Datepicker modal
	Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I select a random hotel from the results
	And Click on search icon
	And Click dates field
	Then Date details should be pre-populated as per the initial <departure> and <return> date
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Tenerife    | London            | 60        | 7      | 2,1,1:1,1,0 |

	@TC_2774796 @TC_2762622 @TC_2292607
Scenario Outline: Check reset and close button on datepicker modal
	Given I am on holiday search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click dates field
	And Change <departure> and <return> dates
	And Click on reset dates
	Then Selected dates should be reset
	When Click on close button
	Then Date picker modal should be closed
Examples: 
| departure | return |
| 25        | 7      |

@TC_2292608
Scenario Outline: Veriy datepicker modal when all required fields for the search is not populated
	Given I am on holiday search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click dates field
	And Click on reset dates
	And Confirm dates selection
	Then <Error> message should be displayed
	When Select <departure> date
	And Confirm dates selection
	Then <Error> message should be displayed	
Examples: 
| departure | return | Error                |
| 60        | 7      | Please select a date |

@TC_2774815
Scenario: Verifying close functionality on search modal
	Given I perform a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
	When I store search itinerary and click on edit search icon
	And I populate destination <newDestination> guests <newGuestsData> during <newDepartureDate> and <newReturnDate> on search modal
	And Close search modal
	Then Search itinerary is populated as per initial data
	Examples:
	| destination | departureLocation | guests | departureDate | returnDate | newDestination | newDepartureLocation | newGuestsData | newDepartureDate | newReturnDate |
	| Dubai       | London            | 2,0,0  | 60            | 7          | Tenerife       | London Gatwick       | 2,0,0:2,1,0   | 120              | 6             |

	@TC_2806983 @TC_2774612
Scenario: Validate multiple airport selected in flying from field
	Given I am on holiday search results page
	When I click on search icon
	And Click on flying from field
	#Then Flying from field should be pre-populated for london all airports
	Then All london airports should be pre selected

	@TC_2806982
	Scenario Outline: Validate single airport selected in flying from field
	Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I click on search icon
	And Click on flying from field
	#Then Flying from field should be pre-populated as per the initial <departure_airport>
	Then <departure_airport> alone should be pre selected
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London Gatwick    | 90        | 7      | 2,0,0  |

	@TC_2806981
	Scenario: Validate airport picker modal
	Given I am on holiday search results page
	When I click on search icon
	And Click on flying from field
	Then Validate airport picker header text
	And Other airports near the searched location should be displayed
	And Close modal button should be displayed on airport picker modal
	And Confirm departure airports button should be displayed

	@TC_2806981
	Scenario: Validate error message on airport picker modal when no airport is selected
	Given I am on holiday search results page
	When I click on search icon
	And Click on flying from field
	And Remove all added airports
	And Click confirm departure airport button
	Then Please add airport(s) error message should be displayed on airport picker modal
	#Add an airport and check error message is not displayed

	@TC_2812572
	Scenario: Validate close button on airport picker modal
	Given I am on holiday search results page
	When I click on search icon
	And Click on flying from field
	And Remove all added airports
	And Click close button on airport picker modal
	#Then Flying from text should be unchanged from previous selection for london all airports
	When Click on flying from field
	#Then Flying from field should be pre-populated for london all airports
	Then All london airports should be pre selected 

	@TC_2806984
	Scenario Outline: Validate flying from summary text on search modal
	Given I am on holiday search results page
	When I click on search icon
	And Update departure airport on search modal to <departure_airport>
	Then Flying from text on search modal should match with <departure_airport>
	Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London Gatwick    | 60        | 7      | 2,0,0  |