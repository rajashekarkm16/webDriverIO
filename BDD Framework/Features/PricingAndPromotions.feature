@PricingAndPromo @v3regression
Feature: PricingAndPromotion
	In order to verify Pricing and Promotion
	As a tester
	I want to get Promo as a product in hotels and holidays for specific locations

	Background: 
	Given Click on Accept and close cookies button

Scenario: Verify Promo Terms and Conditions
Given I am on TR site
And I perform hotel search for Eurostars Grand Marina Hotel dates 60 and 7 with 2,0,0
When I click on promotion terms and conditions 
Then Terms and conditions for promo is displayed


Scenario: Verify promotion in hotel 
Given I perform hotel search for Eurostars Grand Marina Hotel dates 30 and 7 with 2,1,0
Then Was now price should be displayed on estab page
And Sash ribbon should be displayed on estab page 
Then Promo Message should be displayed on estab page
When I select per person price Toggle
Then Promo message should be displayed on boardtype
And Was now price should be displayed on boardtype
When I Select total price Toggle
Then Promo message should be displayed on boardtype
And Was now price should be displayed on boardtype
When I select room type 1 and board type 1
Then Discount should be displayed on booking summary
And Wasnow price on booking summary should be matched 
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Booking references of booked items are available
And Booked item in databse has Promotional Discount



#Scenario Outline:Verify Promotion In Holidays
#Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
#Then Sash ribbon should be displayed on hotel card
#And Was now price should be displayed on hotel card
#And Promo Message should be displayed on hotel card
#When Select <destination> from the search results
#Then Was now price should be displayed on estab page
#And Sash ribbon should be displayed on estab page
#And Promo Message should be displayed on estab page
#When I select per person price Toggle
#Then Promo message should be displayed on boardtype
#And Was now price should be displayed on boardtype
#When I Select total price Toggle
#Then Promo message should be displayed on boardtype
#And Was now price should be displayed on boardtype
#When I select room type 1 and board type 1
#And Confirm the pre selected flight
#And Continuetobook without any Extras
#Then Discount should be displayed on booking summary
#And Wasnow price on booking summary should be matched 
#When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
#Then Booking references of booked items are available
#And Booked item in databse has Promotional Discount
#
#
#Examples: 
#	| destination       | departure_airport | departure | return | guests |
#	| Eurostars Ramblas | London            | 40        | 5      | 2,0,0  |

@holidayregression
Scenario Outline:Verify Promotion In Package
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
#Then Sash ribbon should be displayed on hotel card
#And Was now price should be displayed on hotel card
#And Promo Message should be displayed on hotel card
#When Select <destination> from the search results
Then Was now price should be displayed on estab page
And Sash ribbon should be displayed on estab page 
And Promo Message should be displayed on estab page
When I select per person price Toggle
Then Promo message should be displayed on boardtype
And Was now price should be displayed on boardtype
When I Select total price Toggle
Then Promo message should be displayed on boardtype
And Was now price should be displayed on boardtype
When I select room type 1 and board type 1
And Confirm the pre selected flight
And Continuetobook without any Extras
Then Discount should be displayed on booking summary
And Wasnow price on booking summary should be matched
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
And Booked item in databse has Promotional Discount

Examples: 
	| destination              | departure_airport | departure | return | guests |
	| FIVE Palm Jumeirah Dubai | London            | 60        | 6      | 2,0,0  |



Scenario: Verify URLpromotion in hotel 
Given I have added DiscountCode to URL
And I perform hotel search for Eurostars Cristal Palace Barcelona Hotel dates 120 and 7 with 2,0,0
Then Verify DiscountCode cookie is added 
And Was now price should be displayed on estab page
And UrlSash ribbon should be displayed on estab page
And UrlPromo Message should be displayed on estabpage
When I select per person price Toggle
Then UrlPromo message should be displayed on boardtype
And Was now price should be displayed on boardtype
When I Select total price Toggle
Then UrlPromo message should be displayed on boardtype
And Was now price should be displayed on boardtype
When I select room type 1 and board type 1
Then Discount should be displayed on booking summary
And Wasnow price on booking summary should be matched 
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Booking references of booked items are available
And Booked item in databse has Promotional Discount


@holidayregression
Scenario Outline: Verify URLpromotion in holiday 
Given I have added DiscountCode to URL
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then Verify DiscountCode cookie is added 

Examples: 
	| destination             | departure_airport | departure | return | guests |
	| Mariano Cubi Aparthotel | London            | 20        | 6      | 2,1,1  |

@holidayregression
Scenario Outline: Verify Pill
Given I perform hotel search for <destination> dates 30 and 4 with 2,0,0
Then Pill should be displayed on estab

Examples: 
	| destination                     | 
	| The New Yorker, A Wyndham Hotel | 


Scenario: Verify Price Adjustment in hotel
Given I perform hotel search for Catalonia Park Guell Hotel dates 40 and 1 with 1,0,0 
When I select random rooms and board type
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
And Booked item in database has Price Adjustment 


#Scenario Outline:Verify Price Adjustment in holiday
#Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
#When Select <destination> from the search results
#When I select random rooms and board type
#And Confirm the pre selected flight
#And Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
#Then Booking references of booked items are available
#And Booked item in database has Price Adjustment
#
#
#Examples: 
#	| destination                | departure_airport | departure | return | guests |
#	| Catalonia Park Guell Hotel | London            | 30        | 7      | 2,0,0  |

#P&P rule - 8174
Scenario: Verify StopSell In Hotel
Given I perform hotel search for Best Jacaranda Hotel dates 01 and 3 with 2,0,0 
Then  It should display Stop Sell message 
 

