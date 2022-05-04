@mandatorytransfers @v3regression
Feature: MandatoryTransfers
	In order to verify mandatory transfers
	As a tester
	I want to get transfers as a mandatory product in hotels and holidays for specific locations

	Background: 
	Given Click on Accept and close cookies button

@TC_2772548 @TC_2772545 @TC_2772553 @TC_2772554
Scenario Outline: Verify default mandatory transfers in hotels
Given I perform hotel search for <hotel_name> dates <departure> and <return> with <guests>
When I select random rooms and board type
Then Mandatory transfers is available and selected by default
And Requests user to enter flight details
When Check mandatory transfers in booking summary
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples:
| hotel_name          | departure | return | guests      |
| Reethi Beach Resort | 10        | 6      | 2,0,0:2,0,0 |

@smoke @TC_2772545 @TC_2772546 @TC_2772553 @TC_2772554 @TC_2772552 @v3livesanity
Scenario Outline: Verify mandatory transfers in hotels
Given I perform hotel search for <hotel_name> dates <departure> and <return> with <guests>
When I select random rooms and board type
Then Mandatory transfers is available and selected by default
And Requests user to enter flight details
When Choose random transfers available for each room
And Check mandatory transfers in booking summary
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples:
| hotel_name             | departure | return | guests      |
| Summer Island Maldives | 10        | 6      | 2,0,0:2,0,0 |

@TC_2772550
Scenario Outline: Flight information validation
Given I perform hotel search for <hotel_name> dates <departure> and <return> with <guests>
When I select random rooms and board type
And Enter invalid flight details 
Then Flight validation Error messages should be displayed
And User should not be able to continue with the booking
Examples:
| hotel_name          | departure | return | guests      |
| Reethi Beach Resort | 10        | 3      | 2,0,0:2,0,0 |

@smoke @TC_2772553 @TC_2772554 @v3livesanityUK @holidayregression
Scenario Outline: Verify default mandatory transfers in holidays
Given I perform a holiday search to <hotel_name> from <departure_airport> for <guests> during <departure> and <return> dates
#When Select <hotel_name> from the search results
When I select random rooms and board type
And Confirm the pre selected flight
Then Mandatory transfers is available and selected by default
When Check mandatory transfers in booking summary
And Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples:
| hotel_name             | departure_airport | departure | return | guests      |
| Summer Island Maldives | London            | 19        | 6      | 2,1,0:2,0,0 |

@TC_2772546 @TC_2772553 @TC_2772554 @TC_2774785 @TC_2629972 @holidayregression
Scenario Outline: Verify mandatory transfers in holidays
Given I perform a holiday search to <hotel_name> from <departure_airport> for <guests> during <departure> and <return> dates
#When Select <hotel_name> from the search results
Then Return transfers message is displayed
When I select random rooms and board type
And Confirm the pre selected flight
Then Mandatory transfers is available and selected by default
When Choose random transfers available for each room
And Check mandatory transfers in booking summary
And Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples:
| hotel_name             | departure_airport | departure | return | guests      |
| Summer Island Maldives | London            | 19        | 6      | 2,1,0:2,0,0 |
| Reethi Beach Resort    | London            | 60        | 3      | 2,1,0:2,0,0 |

@TC_2772548 @TC_2774640 @TC_2772841 @TC_2684249 @TC_2772544
Scenario Outline: Verify complimentary transfers in hotels
Given I perform hotel search for <hotel_name> dates <departure> and <return> with <guests>
When I select random rooms and board type
Then Mandatory transfers is available and selected by default
And Compliemtary transfers details are displayed
And Mandatory transfers header text is displayed <headerMessage> and <headerText>
And Mandatory transfer important information section is displayed
Examples:
| hotel_name             | departure | return | guests      |
| Summer Island Maldives | 19        | 6      | 2,0,0:2,0,0 |

@TC_2772548 @TC_2774640 @TC_2772841 @TC_2684249 @TC_2772544 @v3livesanityUK @holidayregression
Scenario Outline: Verify complimentary transfers in holidays 
Given I perform a holiday search to <hotel_name > from <departure_airport> for <guests> during <departure> and <return> dates
#When Select Innahura Maldives Resort from the search results
When I select random rooms and board type
And Confirm the pre selected flight
Then Mandatory transfers is available and selected by default
And Compliemtary transfers details are displayed
And Mandatory transfers header text is displayed <headerMessage> and <headerText>
And Mandatory transfer important information section is displayed
Examples:
| hotel_name             | departure_airport | departure | return | guests |
| Summer Island Maldives | London            | 19        | 6      | 2,0,0  |

@TC_2772556 @holidayregression
Scenario Outline: Verify mandatory transfers order in holidays
Given I perform a holiday search to <hotel_name > from <departure_airport> for <guests> during <departure> and <return> dates
#When Select <hotel_name> from the search results
When I select random rooms and board type
And Confirm the pre selected flight
Then Mandatory transfers is available and selected by default
And Cheapest transfer should be displayed first
Examples:
| hotel_name             | departure_airport | departure | return | guests      |
| Summer Island Maldives | London            | 19        | 4      | 2,1,0:2,0,0 |