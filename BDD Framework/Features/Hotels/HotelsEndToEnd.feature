@hotelregression @v3regression @end2end 
Feature: HotelsEndToEnd
	In order to make hotel booking
	As a end user
	I want to choose a hotel of my choice

Background: 
Given Click on Accept and close cookies button

@TC_1697575 @TC_2769227 @smoke @TC_2768651 @quicksmoke
Scenario Outline: Verify hotel booking with special request
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization and special request
Then Booking references of booked items are available
And Booked hotel details matches
And Validate hotel information, lead contact and price details in database
Examples: 
	| destination | departure | return | guests |
	| Tenerife    | 80        | 6      | 1,1,0  |

	@TC_1697579 @TC_2768596 @v3livesanity @TC_2791546
Scenario Outline: Verify hotel 3DS booking
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS true authorization
Then Booking references of booked items are available
And Validate hotel information, lead contact and price details in database
Examples: 
	| destination | departure | return | guests            |
	| Dubai       | 120       | 6      | 3,0,0:2,1,0:2,0,1 |

	@TC_2768645 @TC_2768652 @v3livesanity
	Scenario Outline: Verify hotel paypal booking
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select random rooms and board type
And Check the selected rooms and total cost
And Confirm the hotel booking selection
And Complete the full payment hotel booking using Paypal payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
| destination | departure | return | guests |
| LCY         | 70        | 7      | 2,1,0  |

@TC_1697573 @TC_2769232
Scenario Outline: Verify hotel booking with deposit payment
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select room and board Flexible
And Check the selected rooms and total cost
And Complete the deposit payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination | departure | return | guests      |
	| Alicante    | 70        | 3      | 2,0,1:2,0,0 |


#Pay monthly turned off in PP
#	@UKOnly 
##Scenario Outline: Verify recurring payment hotel booking
#Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
#When Select a random hotel from the results
#And Select room and board Flexible
#And Check the selected rooms and total cost
#And Confirm the hotel booking selection
#And Complete the monthly payment hotel booking using VisaCredit payment with ThreeDS false authorization
#Then Booking references of booked items are available
#Examples: 
#| destination | departure | return | guests |
#| Tenerife    | 200       | 7      | 2,1,0  |

#	@UKOnly @TC_1697574
#Scenario Outline: Verify recurring payment hotel booking with paypal
#Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
#When Select a random hotel from the results
#And Select room and board Flexible
#And Check the selected rooms and total cost
#And Confirm the hotel booking selection
#And Complete the monthly payment hotel booking using Paypal payment with ThreeDS false authorization
#Then Booking references of booked items are available
#Examples: 
#| destination | departure | return | guests |
#| Tenerife    | 120       | 7      | 2,0,0  |

@TC_2772726 @TC_2768605 
Scenario Outline: Verify Flexible hotel room type booking
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select room and board <type>
And Check the selected rooms and total cost
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
| destination | departure | return | guests | name         | type     |
| Tenerife    | 90        | 7      | 2,0,0  | RoomtypeTest | Flexible |

@TC_1697576 @v3livesanity
Scenario Outline: Verify NonRefundable hotel room type booking
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When Select a random hotel from the results
And Select room and board <type>
And Check the selected rooms and total cost
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
| destination | departure | return | guests | name         | type          |
| Dubai       | 4         | 3      | 2,0,0  | RoomtypeTest | NonRefundable |

@UKOnly @TC_2772725 @TC_2762602 @TC_2762604 @TC_2768603 @v3livesanity
Scenario Outline: Verify city taxes in hotels
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then I can see the local charges information link displayed in hotel tile
When Select a random hotel from the results
Then I can see the local charges information link displayed in hotel estab page
When Select random rooms and board type
And Check the selected rooms and total cost
Then Local tax is displayed in board type selection page
And Local tax is displayed in booking summary page
When Confirm the hotel booking selection
And Fill the passenger <name> details
And Add phone number
And Add email id
And Choose full payment plan
And Proceed to payment page
And Auto populate card holder address
And Enter card details
Then Local tax should match accordingly
Examples: 
| destination | departure | return | guests | name           |
| Dubai       | 80        | 7      | 2,0,0  | TestAutomation |

@UKOnly @TC_2770101 
Scenario Outline: Verify city taxes in deposit hotels
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then I can see the local charges information link displayed in hotel tile
When Select a random hotel from the results
Then I can see the local charges information link displayed in hotel estab page
When Select room and board Flexible 
And Check the selected rooms and total cost
Then Local tax is displayed in board type selection page
And Local tax is displayed in booking summary page
And Pay now section does not include city tax amount
When Confirm the hotel booking selection
And Fill the passenger <name> details
And Add phone number
And Add email id
#And Proceed to choose payment page
Then Local tax message is displayed for pay full amount
And Local tax message is dispalyed for pay deposit amount
Examples: 
| destination | departure | return | guests | name     |
| Dubai       | 60       | 3      | 2,0,0  | AutoTest |