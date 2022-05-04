@MetaRegression
Feature: MetaTravelSupermarketUKHoliday
	In order to verify meta deeplink URLs
	As a tester
	I want to generate deepLink urls from meta and use them in the browser

Background: 
	Given Click on Accept and close cookies button
	And I am on Meta Franklin site 

@UKOnly
Scenario Outline: verify TravelSupermarketUK Holiday MetaChannel
Given I am on Holiday tab on meta search page and choose environment
And I run holiday search for <metaChannel> <departureAirport> <destinationAirport> <countryID> <provinceID> <startDaysFromCurrentDate> <duration> <occupancy>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Confirm the pre selected flight
And Continuetobook without any Extras
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Validate ImageReference added for <metaChannel>
And Validate Flight and Room price are matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel           | departureAirport | destinationAirport | countryID | provinceID | startDaysFromCurrentDate | duration | occupancy |
	| Travel Supermarket UK | LGW              | TFS                | 3522      | 54875      | 40                       | 7        | 2,0,0     |