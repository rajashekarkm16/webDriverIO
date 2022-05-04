@v3regression @holidayregression
Feature: HolidayInJourneySearch
	In order to validate in journey search on holiday flow
	As a cutomer
	I want to modify search data and check the results

	Background: 
Given Click on Accept and close cookies button

	@TC_2806991
	Scenario: Verify change destination from holiday hotel search results page
	Given I perform a holiday search for Tenerife from London Heathrow
	When I click on search icon
	And Edit destination to Dubai
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page is displayed for searched Dubai

	Scenario Outline: Verify change destination from holiday hotel estab page
	Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I select a random hotel from the results
	And I click on search icon
	And Edit destination to Dubai
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page is displayed for searched Dubai

	Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London Heathrow   | 40        | 7      | 2,0,0  |
	
	@TC_2807048
	Scenario: Verify change departure airport from holiday hotel search results page
	Given I perform a holiday search for Tenerife from London Heathrow
	When I click on search icon
	And Update departure airport on search modal to London Gatwick
	And Search for hotel availability
	And Select a random hotel from the results
	And Select random rooms and board type
	Then Departure airport should match in pre selected flight card

	@TC_2807049
	Scenario: Verify add more departure airports from holiday hotel search results page
	Given I perform a holiday search for Tenerife from London
	When I click on search icon
	And I add departure airport on search modal to London Gatwick-London Heathrow-Belfast International
	Then Flying from field should be updated with selected airports

	Scenario Outline: Verify change destination and departure airport from holiday estab page
	Given I perform a holiday search for <destination> from <departureLocation>
	When Select a random hotel from the results
	And Update departure <newDepartureLocation> and destination <newDestination> in search modal
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page is displayed for searched <newDestination>
	And Departure <newDepartureLocation> should match in flight search results
	Examples:
	| destination | departureLocation | newDestination | newDepartureLocation |
	| Dubai       | London            | Tenerife       | London Gatwick       |

	Scenario Outline: Verify changing departure airport for selected hotel availability
	Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I select a random hotel from the results
	And Click on search icon
	And Update departure airport on search modal to London Gatwick
	Then Update availability toggle should be visible
	When I select update this hotel only from availability toggle 
	And Search for hotel availability
	Then Hotel estab page of selected hotel is displayed

	Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London Heathrow   | 60        | 7      | 2,0,0  |

	Scenario Outline: Verify changing departure airport for all hotels availability
	Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I select a random hotel from the results
	And Click on search icon
	And Update departure airport on search modal to London Gatwick
	Then Update availability toggle should be visible
	When I select all hotels from availability toggle 
	And Search for hotel availability
	Then Hotel search results page is displayed for searched Dubai

	Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London Heathrow   | 40        | 7      | 2,0,0  |

	Scenario Outline: Verify changing dates for selected hotel availability from estab page
	Given I perform a holiday search to Tenerife from London for 2,1,0 during 60 and 7 dates
	When I select a random hotel from the results
	And Click on search icon
	And Update <departure> and <return> dates
	Then Update availability toggle should be visible
	When I select update this hotel only from availability toggle 
	And Search for hotel availability
	Then Hotel estab page of selected hotel is displayed
	And Search itinerary is populated accordingly in estab page
	Examples:
	| departure | return |
	| 100       | 7      |

	Scenario Outline: Verify changing dates for all hotels availability from estab page
	Given I perform a holiday search to Tenerife from London for 2,1,0 during 60 and 7 dates
	When I select a random hotel from the results
	And Click on search icon
	And Update <departure> and <return> dates
	Then Update availability toggle should be visible
	When I select all hotels from availability toggle
	And Search for hotel availability
	Then Hotel search results page is displayed for searched Tenerife
	And Search itinerary is populated accordingly
	Examples:
	| departure | return |
	| 100       | 7      |

	Scenario Outline: Verify changing guests for selected hotel availability from estab page
	Given I perform a holiday search to Tenerife from London for 2,1,0 during 60 and 7 dates
	When I select a random hotel from the results
	And Click on search icon
	And Edit guests <guestsData>
	Then Update availability toggle should be visible
	When I select update this hotel only from availability toggle 
	And Search for hotel availability
	Then Hotel estab page of selected hotel is displayed
	And Search itinerary is populated accordingly in estab page
	Examples:
	| guestsData  |
	| 2,1,0:2,1,0 |  

	Scenario Outline: Verify changing guests for all hotels availability from estab page
	Given I perform a holiday search to Tenerife from London for 2,1,0 during 60 and 7 dates
	When I select a random hotel from the results
	And Click on search icon
	And Edit guests <guestsData>
	Then Update availability toggle should be visible
	When I select all hotels from availability toggle
	And Search for hotel availability
	Then Hotel search results page is displayed for searched Tenerife
	And Search itinerary is populated accordingly
	Examples:
	| guestsData  |
	| 2,1,0:2,1,0 | 

	@v3livesanityUK
	Scenario Outline: Verify change search from holiday hotel search results page
	Given I perform a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
	When Update departure <newDepartureLocation> destination <newDestination> guests <newGuestsData> during <newDepartureDate> and <newReturnDate> in search modal
	Then Hotel search results page is displayed for searched <newDestination>
	And Search itinerary is populated accordingly
	And Departure <newDepartureLocation> should match in flight search results
	Examples:
	| destination | departureLocation | guests | departureDate | returnDate | newDestination | newDepartureLocation | newGuestsData | newDepartureDate | newReturnDate |
	| Dubai       | London            | 2,0,0  | 60            | 7          | Tenerife       | London Gatwick       | 2,0,0:2,1,0   | 135              | 10            |
	
	@TC_2811626 @TC_2811628 @v3livesanityUK
	Scenario Outline: Verify change search from holiday hotel estab page for this hotel toggle
	Given I perform a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
	When I select a random hotel from the results
	And Update departure <newDepartureLocation> guests <newGuestsData> during <newDepartureDate> and <newReturnDate> in estab page search modal
	And I select update this hotel only from availability toggle 
	And Search for hotel availability
	Then Search itinerary is populated accordingly in estab page
	And Departure <newDepartureLocation> should match in flight search results from estab page
	Examples:
	| destination | departureLocation | guests      | departureDate | returnDate | newDepartureLocation | newGuestsData | newDepartureDate | newReturnDate |
	| Majorca     | London            | 2,0,0:2,1,0 | 40            | 7          | London Gatwick       | 2,0,0         | 50               | 10            |


	@TC_2811627 @v3livesanityUK
	# Current bug -> FUN-2778
	Scenario Outline: Verify change search from holiday hotel estab page for all hotels toggle
	Given I perform a holiday search to <destination> from <departureLocation> for <guests> during <departureDate> and <returnDate> dates
	When I select a random hotel from the results
	And Update departure <newDepartureLocation> guests <newGuestsData> during <newDepartureDate> and <newReturnDate> in estab page search modal
	And I select all hotels from availability toggle
	And Search for hotel availability
	Then Search itinerary is populated accordingly
	And Hotel search results page is displayed for searched <destination>
	And Departure <newDepartureLocation> should match in flight search results
	Examples:
	| destination | departureLocation | guests | departureDate | returnDate | newDepartureLocation | newGuestsData | newDepartureDate | newReturnDate |
	| Tenerife     | London            | 2,0,0  | 50            | 7          | London Gatwick       | 2,0,0:2,1,0   | 80               | 10            |           


	@TC_2811812
	Scenario: Verify change destination to different hotel from holiday hotel search results page
	Given I perform a holiday search for Tenerife from London Heathrow
	When I click on search icon
	And Edit destination to Rixos The Palm Dubai Hotel and Suites
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel estab should be displayed
	And Search location should match with Rixos The Palm Dubai Hotel and Suites 

	@TC_2811812
	Scenario Outline: Verify change destination to different hotel from holiday hotel estab page
	Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I select a random hotel from the results
	And I click on search icon
	And Edit destination to Atlantis the Palm
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel estab should be displayed
	And Search location should match with Atlantis the Palm 

	Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London Heathrow   | 40        | 7      | 2,0,0  |

