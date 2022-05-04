@v3regression @holidayregression 
Feature: HolidayFlightSearchResults
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Holiday Flight Search Results Page


Background: 
	Given Click on Accept and close cookies button

@TC_2775792 @TC_1756022 @TC_8046551 @v3livesanityUK
Scenario Outline: Verify Flight search result page
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then Flight search results are displayed
	And Booking Summary header should be displayed
	And Baggage option <message> should be displayed
	And Sort by link should be displayed
	And Show more flights button should be displayed if more flights available
	And Pre-selected flight card holiday price should be zero
Examples: 
	| destination | departure_airport | departure | return | guests | message            |
	| Tenerife    | London            | 80        | 6      | 2,0,0  | Need checked bags? |

	@TC_1707896 @MobileOnly
Scenario: Verify Booking Summary button
	Given I am on holiday Flight search results page
	Then Booking Summary header should be displayed
	And Total price should be displayed on booking summary bar	
	When I click on Booking Summary button
	Then Booking Summary screen should be displayed

	@TC_1713113
Scenario Outline: Verify Flight Card (Direct Flight Same Carrier Operated)
	Given I am on holiday flight search results for <destination> from <departure_airport>
	Then I want to see the Additional Flight cards displayed
	And the flight card for direct flight same carrier information displayed 
Examples:
	| destination | departure_airport |
	| Dubai       | London            |

	@TC_1755975
Scenario Outline: Verify Flight Card (Pre-selected Flight)
	Given I am on holiday flight search results for <destination> from <departure_airport>
	Then I want to see the Additional Flight cards displayed
	And the flight card for pre-selected flights information displayed
Examples:
	| destination | departure_airport |
	| Tenerife    | London            |

	@TC_1755972
Scenario Outline: Verify Flight Card (Direct Flight Different Carrier)
    Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then I Want to see the Preselected Flight card displayed
	Then I want to see the Additional Flight cards displayed
	And the flight card for direct flight different carrier information displayed

Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London            | 80        | 6      | 2,0,0  |

	@TC_1755973
Scenario Outline: Verify Flight Card (Different Carrier Each Flight)
	Given I am on holiday flight search results for <destination> from <departure_airport>
	Then I want to see the Additional Flight cards displayed
	And the flight card for different carrier each flight information displayed
Examples:
	| destination | departure_airport |
	| Majorca    | London            |

	@TC_1755974 @v3livesanityUK
Scenario Outline: Verify Flight Card (Open Jaw)
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then I Want to see the Preselected Flight card displayed
	Then I want to see the Additional Flight cards displayed
	And Flight card for open jaw flight information displayed

Examples: 
	| destination | departure_airport | departure | return | guests |
	| PMI    | London            | 20        | 6      | 2,0,0  |

Scenario Outline: Verify Flight Card (Connecting Flights)
	Given I am on holiday flight search results for <destination> from <departure_airport>
	Then I want to see the Additional Flight cards displayed
	And the flight card for connecting flights information displayed
Examples:
	| destination | departure_airport |
	| Dubai       | London            |

Scenario: Verify Keep button on flight card
	Given I am on holiday Flight search results page
	When I click on the Keep button
	Then Extras selection screen should be displayed

Scenario: Verify Select button on flight card
	Given I am on holiday Flight search results page
	When I select a random flight from the results
	Then Extras selection screen should be displayed

	@v3livesanityUK
Scenario Outline: Verify show more Flights button 
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	When I click show more Flights button
	Then Additional flight results should be loaded
	And The show more flights button should move to the bottom of the list

Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 20        | 6      | 2,0,0  |

	@v3livesanityUK
Scenario Outline: Verify flight sort 
	Given I am on holiday flight search results for <destination> from <departure_airport>
	When I open flight sort options
	And Select the flight sort <option>
	Then Flight search results are sorted based on the selected <option>	
Examples:
| destination | departure_airport | option                    |
| Dubai       | London            | Price (Cheapest first)    |
| Dubai       | London            | Duration (Quickest first) |

Scenario: Verify flight details modal is displayed
	Given I am on holiday Flight search results page
	When I click on Return flights link
	Then Flight details modal should be displayed

	@TC_2821702 @TC_2821703 @TC_2821704 @TC_2821705
Scenario Outline: Verify flight cut off time 
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then I want to see the Additional Flight cards displayed
	And Outbound arrival time should be after 4AM
	And Inbound departure time should be after 3AM
Examples: 
	| destination    | departure_airport | departure | return | guests |
	| Costa Blanca   | London            | 70        | 6      | 2,0,0  |
	| Veneto         | London            | 100       | 6      | 2,0,0  |
	| Tenerife South | London            | 120       | 6      | 2,0,0  |

	@TC_1895946
Scenario: Verify default sort option
    Given I am on holiday Flight search results page
	When I open flight sort options
	Then Recommended option is selected by default

	@TC_2772703
Scenario: Verify show more Flights button not displayed 
	        Given I am on holiday flight search results for Tenerife South from LGW
            And flights search results contains less flights 
           Then Show more flights button should not displayed

     @TC_1707892
Scenario Outline: Verify no filtered results available
           Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
		   When I applied filters to see view matches count is zero
           Then View matches button should be disabled
Examples:
| destination | departure_airport | departure | return | guests |
| Majorca     | london            | 40        | 1      | 2,0,0:1,0,0  |

    @TC_2774535
Scenario: Verify number of stops filter should not be displayed when only direct flights
		 Given I am on holiday flight filters modal for Belfast international from LGW
		 Then Number of stops filter should not be displayed

# Not working because of functional issue(Known)
#     @TC_2774537 
#Scenario Outline: Verify depart/return to same airport should not be displayed for only closed jaw flight results
#            Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
#			Then  Depart/Return to same airport should not be displayed
#Examples:
#| destination | departure_airport | departure | return | guests | 
#| Dubai       | LGW               | 50        | 1      | 2,0,0  |

    @TC_2774521
Scenario Outline: Verify arriving on future days
           Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
		   Then flight card should display arriving on future days as per design
Examples:
| destination | departure_airport | departure | return | guests |
| Maldives    | London Gatwick    | 80        | 6      | 2,0,0  |


Scenario Outline: Verify stopover Flights info
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then I Want to see the Preselected Flight card displayed
	Then I want to see the Additional Flight cards displayed
	And Flight cards should display stopover information

Examples: 
	| destination  | departure_airport | departure | return | guests |
	| Costa Blanca | London            | 40        | 6      | 2,0,0  |


Scenario Outline: Verify Hand Luggage on flight card
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then I Want to see the Preselected Flight card displayed
	Then Flight card should display hand luggage for passenger

Examples: 
	| destination | departure_airport | departure | return | guests |
	| Maldives    | London Heathrow   | 30        | 6      | 2,0,0  |


# Re visit and modify the validation for Flight Inclusion info based on the card it is displayed 
Scenario Outline: Verify Stop info and flight inclusion in flight modal
	Given I am on holiday flight search results page for <destination> from <departure_airport> for <guests> during <departure> and <return> dates
	Then I want to see the flight details modal with flight NoOfStops information
	And I Want to see the flight details modal with Flight Inclusion information
	
Examples: 
	| destination  | departure_airport | departure | return | guests |
	#| AMS          | LCY               | 60        | 6      | 2,0,0  |
	| New York | London Heathrow            | 25        | 3      | 2,0,0  |
	#| Maldives     | London Heathrow   | 70        | 6      | 2,0,0  |


@TC_2600191
Scenario Outline: Verify outbound flight time departure slider
Given I am on holiday flight filters modal for <destination> and <departureAiport>
Then Outbound Departure time slider should be displayed
And Verify Outbound departure times filter

Examples:
| destination | departureAiport |
| Veneto      | London          |


Scenario Outline: Verify Return flight time departure slider
Given I am on holiday flight filters modal for <destination> and <departureAiport>
Then Outbound Departure time slider should be displayed
And Verify Return departure times filter

Examples:
| destination | departureAiport |
| Veneto      | London          |

