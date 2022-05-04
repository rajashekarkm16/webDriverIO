@MetaRegression
Feature: MetaTrivagoHotel
	In order to verify meta deeplink URLs
	As a tester
	I want to generate deepLink urls from meta and use them in the browser

Background: 
	Given Click on Accept and close cookies button
	And I am on Meta Franklin site 

@UKOnly
Scenario Outline: verify TrivagoUK Hotel MetaChannel
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
	| Trivago UK  | 1         | 2,0,0     | 30                       | 5        | 117468,117305,126042 |


Scenario Outline: verify TrivagoIE Hotel MetaChannel
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
	| Trivago IE  | 1         | 2,1,0     | 30                       | 5        | 117468,230981,126042 |

