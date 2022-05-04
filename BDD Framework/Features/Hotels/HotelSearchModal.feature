@v3regression @hotelregression
Feature: HotelSearchModal
	In order to perform a hotel search
	As a end user
	I want to edit the search criteria

Background: 
Given Click on Accept and close cookies button
@TC_2774610
Scenario Outline: Verify pre-populated details on estab page search modal
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And Click on search icon
	Then Destination should be pre-populated as per initital search details <destination>
	And Destination field should be editable
	And Hotel name on search modal should match with the selected hotel
	And Hotel name should not be editable
	And Passenger details should be pre-populated as per initital search details
	And Dates should be pre-populated as per initital search details	
	Examples: 
	| destination |
	| Tenerife    |

@TC_2762609 @TC_2811201
Scenario Outline: Verify No rooms available message and check search alternative dates button from estab page
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And I update <guests> and change <departure> and <return> dates
	Then No rooms available <message> should be displayed
	When I click Search alternative date button
	Then Datepicker modal should be displayed with previous selected <departure> and <return> dates
Examples:
| destination | departure | return | guests | message                                                    |
| Dubai       | 1         | 7      | 12,1,1 | We couldn't find any rooms available for your chosen dates |

@TC_2811200
Scenario Outline: Verify No rooms available message and check search alternative dates button from SRP
	Given I perform a hotel search for <destination>
	When I update <guests> and change <departure> and <return> dates
	Then No rooms available <message> should be displayed
	When I click Search alternative date button
	Then Datepicker modal should be displayed with previous selected <departure> and <return> dates
Examples:
| destination | departure | return | guests | message                                                                                                                                                                                 |
| Dubai       | 0         | 7      | 12,3,1 | Looks like we’re all out of what you’re looking for online… Don’t fret. Our SUPER experienced, holiday lovin’ experts have got what you need. Call us on 0208 972 8592 between 9am-6pm. |
	
Scenario Outline: Verify occupancy modal when all required fields for the search is not populated
	Given I am on hotel search results page
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


Scenario Outline: Verify pre-populated details on occupancy modal
	Given I perform a hotel search for <destination>
	When I select a random hotel from the results
	And Click on search icon
	And Click guests field
	Then Guests details should be pre-populated as per the initital search
	Examples: 
	| destination | departure_airport |
	| Tenerife    | London            |

	
Scenario: Add and remove rooms on occupancy modal
	Given I am on hotel search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click guests field
	And Click on number of rooms as '2'
	Then Guest modal should display 2 rooms details
	When Click on number of rooms as '3'
	Then Guest modal should display 3 rooms details
	When Click on number of rooms as '2'
	Then Guest modal should display 2 rooms details

	
Scenario Outline: Max and Min adults validation on occupancy modal
	Given I am on hotel search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click guests field
	And Populate guests with child age <guestsData>
	Then Verify maximum of 12 adults can be selected
	And Veriy minimum of 1 adults should be selected
Examples:
| guestsData  |
| 2,1,1:1,1,0 |


Scenario: Max and Min children validation on occupancy modal
	Given I am on hotel search results page
	When I select a random hotel from the results
	And I click on search icon
	And Click guests field
	Then Verify maximum of 12 children can be selected
	And Veriy minimum of 0 children can be selected

	
Scenario Outline: Verify pre-populated details on Datepicker modal
	Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
	When I select a random hotel from the results
	And Click on search icon
	And Click dates field
	Then Date details should be pre-populated as per the initial <departure> and <return> date
Examples: 
	| destination | departure | return | guests      |
	| Tenerife    | 60        | 7      | 2,1,1:1,1,0 |

   @TC_2774795	
Scenario Outline: Check reset and close button on datepicker modal
	Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
	When I select a random hotel from the results
	And I click on search icon
	And Click dates field
	And Click on reset dates
	Then Selected dates should be reset
	When Change <newDeparture> and <newRreturn> dates	
	And Click on close button
	Then Date picker modal should be closed
	And Date on search modal should be populated as per the initial <departure> and <return> date
Examples: 
| destination | departure | return | guests | newDeparture | newRreturn |
| Tenerife    | 60        | 7      | 2,1,1  | 25           | 7          |


Scenario Outline: Veriy datepicker modal when all required fields for the search is not populated
	Given I am on hotel search results page
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
| 25        | 7      | Please select a date |

@TC_2762616
Scenario: Verify need more help text
  Given I am on hotel search results page
  When I click on search icon
  And Click guests field
  Then Need more rooms text information is displayed

  @TC_2774811
Scenario: Verify the functionality of changing the child's age
   Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
   When I click on search icon
   Then Guests details should be populated as per the initital search
   When Click guests field    
   And Edit child age
   Then New selected child age is displayed
Examples:
	| destination | departure | return | guests |
	| Tenerife    | 60        | 7      | 2,1,1  |

	@TC_2774813
Scenario: Verifying the close functionality on occupancy modal
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
	When I click on search icon
	And Click guests field    
	And Click on number of rooms as '3'
	And click on close icon on occupancy 
	Then Guests details should be pre-populated as per the initital search
Examples:
	| destination | departure | return | guests |
	| Tenerife    | 60        | 7      | 2,1,1  |


	@TC_2774803
Scenario: Check the date functionality on Datepicker modal
Given  I perform hotel search for <destination> dates <departure> and <return> with <guests>
   When I click on search icon
   And Click dates field
   Then I should be able to select the current date
   And Privious date should be disabled
   And Future date should be enabled
Examples:
	| destination | departure | return | guests |
	| Tenerife    | 60        | 7      | 2,1,1  |

	@TC_2807063
Scenario: Validate edit destination field on destination modal
	Given  I perform hotel search for <destination> dates <departure> and <return> with <guests>
	When I click on search icon
    And Click on destination
    Then <destination> should be pre populated on destination modal
    And <destination> should be highlighted on destination auto completer
Examples:
	| destination | departure | return | guests |
	| Tenerife    | 60        | 7      | 2,1,1  |

	@TC_2806990
Scenario: Validate destination auto completer on destination modal
	Given  I perform hotel search for <destination> dates <departure> and <return> with <guests>
	When I click on search icon
    And Click on destination
	And I enter three letter DUB in the destination text box
    Then Destination auto completer should be displayed
Examples:
	| destination | departure | return | guests |
	| Tenerife    | 60        | 7      | 2,1,1  |
