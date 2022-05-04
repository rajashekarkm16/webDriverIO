@holidayregression @v3regression @end2end 
Feature: HolidaysEndToEnd
	In order to make holiday booking
	As a end user
	I want to choose a hotel and flight of my choice

Background: 
Given Click on Accept and close cookies button

@TC_2769230 @TC_2777739 @TC_2768597 @TC_1633967 @TC_1633986 @TC_1633988 @TC_2790645  @TC_2790642 @TC_2790953 @v3livesanityUK @quicksmoke
Scenario Outline: Holiday booking with all extras
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Select random bag if add baggage option is available
And Select random transfers if transfer is available
And Select random Travel Insurance if Insurance is available
And Check selected holiday products and total price
And Complete the full payment holiday booking using VisaCredit payment with ThreeDS true authorization
Then Booking references of booked items are available
And Booked holiday details matches
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Alicante    | London            | 20        | 4      | 2,0,0  |


@TC_1755967 @TC_2768598 @TC_1636898 @TC_1650629 @TC_2769228 @TC_2769242
Scenario Outline: Holiday booking with additional baggage
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Select random baggage
And Check selected holiday products and total price
And Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
And Booked holiday details matches
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Majorca     | London            | 90        | 6      | 2,1,1:1,1,0 |

@UKOnly @TC_1755965 @TC_1755988 @TC_2774534
Scenario Outline: Holiday booking with paypal payment
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Select random flight
And Check selected holiday products and total price
And Complete the full payment holiday booking using Paypal payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination    | departure_airport | departure | return | guests |
	| Tenerife South | London            | 120       | 6      | 2,0,0  |

@TC_2777738 @v3livesanityUK @TC_2769243
Scenario Outline: Holiday booking with deposit payment
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select room and board Flexible
And Confirm the pre selected flight
And Check selected holiday products and total price
And Complete the deposit payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
And Booked holiday details matches
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Dubai    | London            | 90        | 6      | 2,1,1:1,1,0 |

#	Scenario Outline: Non refundable holiday booking
#Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
#When Select a random hotel from the results
#And Select room and board NonRefundable
#And Confirm the pre selected flight
#And Check selected holiday products and total price
#And Complete the deposit holiday booking
#Then Booking references of booked items are available
#Examples: 
#	| destination | departure_airport | departure | return | guests |
#	| Tenerife    | London            | 150        | 6      | 2,1,0  |

@TC_1755968 @TC_2770079 @TC_2812564 @TC_2818528 @TC_2818538
Scenario Outline: Verify monthly recurring payment holiday booking with extras 
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select room and board Flexible
And Confirm the pre selected flight
And Select random bag if add baggage option is available
And Select random transfers
And Check selected holiday products and total price
And Complete the monthly payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
And Booked holiday details matches
Examples: 
	| destination | departure_airport | departure | return | guests      | type    |
	| Tenerife    | London            | 180       | 7      | 2,0,0:2,1,0 | monthly |

Scenario: Holiday paypal booking on hotel Landing Page
Given When I access Travel Republic site
And Navigated to hotels landing page url
When Click on holidays tab on search modal
Then Landing page search modal should be displayed with pre-populated details
When I populate destination <destination> airport < departure_airport > guests <guestsData> during <departure> and <return> in landing page search modal
And Search for holidays on landing page
Then Hotel search results page should be displayed
When Select a random hotel from the results
And Select random rooms and board type
And Select random flight
And Check selected holiday products and total price
And Complete the full payment holiday booking using Paypal payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
| destination | departure_airport | guestsData | departure | return |
| Majorca     | London            | 2,1,1      | 60        | 7      |

