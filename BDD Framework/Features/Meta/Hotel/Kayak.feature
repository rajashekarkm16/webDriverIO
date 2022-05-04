@MetaRegression
Feature: MetaKayakHotel
	In order to verify meta deeplink URLs
	As a tester
	I want to generate deepLink urls from meta and use them in the browser

Background: 
	Given Click on Accept and close cookies button
	And I am on Meta Franklin site 

	@UKOnly
Scenario Outline: verify KayakUK Hotel MetaChannel
Given I am on Hotel tab on meta search page and choose environment
And I run hotel search for <metaChannel> <noOfRooms> <occupancy> <startDaysFromCurrentDate> <duration> <estabIds>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Validate ImageReference added for <metaChannel>
And Validate totalprice is matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel | noOfRooms | occupancy | startDaysFromCurrentDate | duration | estabIds             |
	| Kayak UK    | 1         | 2,1,0     | 30                       | 6        | 117436,117347,117305 |


Scenario Outline: verify KayakIE Hotel MetaChannel
Given I am on Hotel tab on meta search page and choose environment
And I run hotel search for <metaChannel> <noOfRooms> <occupancy> <startDaysFromCurrentDate> <duration> <estabIds>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Validate ImageReference added for <metaChannel>
And Validate totalprice is matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel | noOfRooms | occupancy | startDaysFromCurrentDate | duration | estabIds              |
	| Kayak IE    | 1         | 2,1,1     | 15                       | 6        | 2540494,117468,117305 |
