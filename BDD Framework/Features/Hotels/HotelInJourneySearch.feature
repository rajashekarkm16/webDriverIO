@v3regression @hotelregression @v3livesanity
Feature: HotelInJourneySearch
	In order to validate in journey search on hotel only flow
	As a cutomer
	I want to modify search data and check the results

	Background: 
Given Click on Accept and close cookies button
	@TC_2811812 @TC_2806991
	Scenario Outline: Edit destination on estab page search modal
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit destination to <new_destination>
	Then Destination on search modal should be updated to <new_destination>
	And Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page should be displayed
	And Search location should match with <new_destination>
Examples: 
	| destination | new_destination |
	| Tenerife    | Dubai           |

	@TC_2811628 @TC_2806986
	Scenario Outline: Edit destination to new location on search results page search modal
	Given I perform a hotel search for <destination>
	When I click on search icon
	And Edit destination to <new_destination>
	Then Destination on search modal should be updated to <new_destination>
	And Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page should be displayed
	And Search location should match with <new_destination>
Examples: 
	| destination | new_destination |
	| Tenerife    | Dubai           |

	@TC_2806989
Scenario Outline: Edit destination to new hotel on search results page search modal
	Given I perform a hotel search for <destination>
	When I click on search icon
	And Edit destination to <new_hotel>
	Then Destination on search modal should be updated to <new_hotel>
	And Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel estab should be displayed
	And Search location should match with <new_hotel>
Examples: 
	| destination | new_hotel    |
	| Tenerife    | Burj Al Arab |

Scenario Outline: Edit destination to different hotel on estab page search modal
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit destination to <new_hotel>
	Then Destination on search modal should be updated to <new_hotel>
	And Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel estab should be displayed
	And Search location should match with <new_hotel>
Examples: 
	| destination | new_hotel    |
	| Tenerife    | Burj Al Arab |

Scenario Outline: Validate this hotel only toggle on estab page by editing guests
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit guests <guestsData> 
	Then Update availability toggle should be visible
	When I select This Hotel Only toggle
	And Search for hotel availability
	Then Hotel estab should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination | guestsData  |
	| Tenerife    | 2,1,1:1,1,0 |

	@TC_811627
Scenario Outline: Validate all hotels toggle on estab page by editing guests
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit guests <guestsData> 
	Then Update availability toggle should be visible
	When I select All Hotels toggle
	And Search for hotel availability
	Then Hotel search results page should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination | guestsData  |
	| Tenerife    | 2,1,1:1,1,0 |

@TC_2811626
	Scenario Outline: Validate this hotel only toggle on estab page by editing dates
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Update <departure> and <return> dates
	Then Update availability toggle should be visible
	When I select This Hotel Only toggle
	And Search for hotel availability
	Then Hotel estab should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination | departure | return | 
	| Tenerife    | 25        | 7      | 

	@TC_2811626 @v3livesanity
Scenario Outline: Validate this hotel only toggle on estab page by editing dates and guests
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit guests <guestsData> 
	And Update <departure> and <return> dates	
	Then Update availability toggle should be visible
	When I select This Hotel Only toggle
	And Search for hotel availability
	Then Hotel estab should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination | guestsData  | departure | return | 
	| Tenerife    | 2,1,1:1,1,0 | 25        | 7      | 

	@TC_811627
Scenario Outline: Validate all hotels toggle on estab page by editing dates
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Update <departure> and <return> dates
	Then Update availability toggle should be visible
	When I select All Hotels toggle
	And Search for hotel availability
	Then Hotel search results page should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination |departure | return | 
	| Tenerife    |25        | 7      |

	@TC_811627 @v3livesanity
Scenario Outline: Validate all hotels toggle on estab page by editing dates and guests
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit guests <guestsData> 
	And Update <departure> and <return> dates
	Then Update availability toggle should be visible
	When I select All Hotels toggle
	And Search for hotel availability
	Then Hotel search results page should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination | guestsData  |departure | return | 
	| Tenerife    | 2,1,1:1,1,0 |25        | 7      | 

Scenario Outline: Update destination, dates and guests from search results page
	Given I perform a hotel search for <destination>
	When I click on search icon
	And Edit destination to <new_destination>
	Then Destination on search modal should be updated to <new_destination>	
	When Edit guests <guestsData> 
	And Update <departure> and <return> dates
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination |new_destination | guestsData  |departure | return | 
	| Tenerife    |Dubai           | 2,1,1:1,1,0 |25        | 7      | 
	
Scenario Outline: Update destination, dates and guests from estab page
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I click on search icon
	And Edit destination to <new_destination>
	Then Destination on search modal should be updated to <new_destination>	
	When Edit guests <guestsData> 
	And Update <departure> and <return> dates
	Then Update availability toggle should not be visible
	When Search for hotel availability
	Then Hotel search results page should be displayed
	And Search itinerary is populated accordingly
Examples: 
	| destination |new_destination | guestsData  |departure | return | 
	| Tenerife    |Dubai           | 2,1,1:1,1,0 |25        | 7      | 

	Scenario: Verifying the close functionality on search modal
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
	When I click on search icon
	And Click guests field    
	And Click on number of rooms as '3'
	And click on close icon on occupancy 
	Then Guests details should be pre-populated as per the initital search
Examples:
	| destination | departure | return | guests |
	| Tenerife    | 60        | 7      | 2,1,1  |