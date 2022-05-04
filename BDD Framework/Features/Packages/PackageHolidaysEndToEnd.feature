@holidayregression @v3regression @end2end 
Feature: PackageHolidaysEndToEnd
	In order to make holiday booking
	As a end user
	I want to choose a hotel and flight of my choice

	Background: 
Given Click on Accept and close cookies button

	@TC_2777740 @smoke @v3livesanityUK @TC_2778809
	Scenario Outline: Package holiday booking with paypal payment
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Select random flight
And Check selected holiday products and total price
And Complete the holiday booking with paypal payment
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 80       | 6      | 2,0,0  |

	@TC_1755966 @v3livesanityUK @TC_2778808
	Scenario Outline: Package holiday booking with 3DS payment
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Check selected holiday products and total price
And Check package price
And Complete the ThreeDS holiday booking
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 60        | 4      | 2,1,1  |

	@TC_1848113 @TC_1633987 @v3livesanityUK @TC_2770062
	Scenario Outline: Package holiday booking with deposit payment
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select room and board Flexible
And Confirm the pre selected flight
And Select random transfers if transfer is available
And Check selected holiday products and total price
And Complete the deposit holiday booking
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Majorca     | London            | 80       | 6      | 1,1,1  |


	@TC_1848112 @TC_2770023
Scenario Outline: Verify monthly recurring payment package holiday booking with extras
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Select random bag if add baggage option is available
And Select random transfers if transfer is available
And Check selected holiday products and total price
And Complete the holiday booking with recurring <type> payment
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests            | type    |
	| Dubai       | London            | 190       | 7      | 3,0,0:1,0,0:2,1,1 | monthly |

	@TC_2790645  @TC_2790642 @TC_2790953 @UKOnly @smoke @quicksmoke @v3livesanityUK @TC_2768600
Scenario Outline: Package holiday booking with travel insurance
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Add a Travel Insurance
And Check selected travel insurance and price in booking summary
And Complete the holiday booking
Then Booking references of booked items are available
And Insurance details are displayed in booking confirmation page
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Dubai       | London            | 30       | 7      | 2,0,0:2,0,0 |

Scenario: Package 3DS booking on holiday landing page
Given When I access Travel Republic site
And Navigated to holidays landing page url
When Click on holidays tab on search modal
Then Landing page search modal should be displayed with pre-populated details
When I populate destination <destination> airport < departure_airport > guests <guests> during <departure> and <return> in landing page search modal
And Search for holidays on landing page
Then Hotel search results page should be displayed
When Update dates if adjusted
When Select a random hotel from the results
And Select random rooms and board type
And Select random flight
And Check selected holiday products and total price
And Check package price
And Complete the ThreeDS holiday booking
Then Booking references of booked items are available
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 50        | 6      | 2,1,0  |

