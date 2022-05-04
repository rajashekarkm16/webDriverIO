@MetaRegression
Feature: MetaTripAdvisorHotel
	In order to verify meta deeplink URLs
	As a tester
	I want to generate deepLink urls from meta and use them in the browser

Background: 
	Given Click on Accept and close cookies button
	And I am on Meta Franklin site 

@UKOnly
Scenario Outline: verify TripAdvisorDesktopUK Hotel MetaChannel
Given I am on Hotel tab on meta search page and choose environment
And I run hotel search for <metaChannel> <noOfRooms> <occupancy> <startDaysFromCurrentDate> <duration> <estabIds>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Validate ImageReference added for <metaChannel>
And Validate totalprice is matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel               | noOfRooms | occupancy | startDaysFromCurrentDate | duration | estabIds              |
	| Trip Advisor (desktop) UK | 1         | 2,1,1     | 20                       | 8        | 117436,2540494,394244 |


Scenario Outline: verify TripAdvisorDesktopIE Hotel MetaChannel
Given I am on Hotel tab on meta search page and choose environment
And I run hotel search for <metaChannel> <noOfRooms> <occupancy> <startDaysFromCurrentDate> <duration> <estabIds>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Validate ImageReference added for <metaChannel>
And Validate totalprice is matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel               | noOfRooms | occupancy | startDaysFromCurrentDate | duration | estabIds               |
	| Trip Advisor (desktop) IE | 1         | 2,1,1     | 30                       | 6        | 4546942,1619097,126042 |


Scenario Outline: verify TripAdvisorMobileIE Hotel MetaChannel
Given I am on Hotel tab on meta search page and choose environment
And I run hotel search for <metaChannel> <noOfRooms> <occupancy> <startDaysFromCurrentDate> <duration> <estabIds>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Validate ImageReference added for <metaChannel>
And Validate totalprice is matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel              | noOfRooms | occupancy | startDaysFromCurrentDate | duration | estabIds              |
	| Trip Advisor (mobile) IE | 1         | 2,1,1     | 10                       | 5        | 231186,1619097,740339 |

@UKOnly
Scenario Outline: verify TripAdvisorMobileUK Hotel MetaChannel
Given I am on Hotel tab on meta search page and choose environment
And I run hotel search for <metaChannel> <noOfRooms> <occupancy> <startDaysFromCurrentDate> <duration> <estabIds>
Then Validate Meta Reference in url and cookies <metaChannel>
When I select room type 1 and board type 1
And Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Validate ImageReference added for <metaChannel>
And Validate totalprice is matching from metachannel
And Booking references of booked items are available

Examples: 
	| metaChannel              | noOfRooms | occupancy | startDaysFromCurrentDate | duration | estabIds             |
	| Trip Advisor (mobile) UK | 1         | 2,0,0     | 20                       | 6        | 231186,740339,394244 |


