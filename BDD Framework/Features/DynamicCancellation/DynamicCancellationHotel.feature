@DynamicCancellation
Feature: Dynamic Cancellation Hotel
	In order to verify  Dynamic cancellation feature  
	As a tester
	I want to get free cancellation modal and verify functionality 

Background: 
Given Click on Accept and close cookies button


# 25 Days -2Adults scenario checkin in date should be before or untill 30th Apr 2022
Scenario Outline: Verify Hotel with Extranet 
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then Check Free Cancellation Date with <departure> and <RuleDate> In Estab Price Includes Section 
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on roomType 1 and boardType 1
When I select room type 1 and board type 1
Then Check Free Cancellation Date on BookingSummary
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization and special request
Then Booking references of booked items are available
Examples: 
	| destination         | departure | return | guests | RuleDate |
	| Paladim & Alagoamar | 40        | 5      | 2,0,0  | 25       |
	
	

# 15 days scenario checkin in date should be starts from 15th march to 30th April 2022
# 3 days scenario checkin in date should be from 1st may to 30th Nov
Scenario Outline: Verify Hotel with Contract
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then Check Free Cancellation Date with <departure> and <RuleDate> In Estab Price Includes Section  
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on roomType 1 and boardType 1
When I select room type 1 and board type 1
Then Check Free Cancellation Date on BookingSummary
And Requests user to enter flight details
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination              | departure | return | guests | RuleDate |
	| Innahura Maldives Resort | 65        | 5      | 2,0,0  | 15       |
	| Innahura Maldives Resort | 110       | 6      | 2,0,0  | 3        |



#30 days scenario checkin in date should be 1st nov to 31st dec
Scenario Outline: Verify Hotel with Contract for Thirty days Rule
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then Check Free Cancellation Date with <departure> and <RuleDate> In Estab Price Includes Section  
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on roomType 1 and boardType 1
When I select room type 1 and board type 1
Then Check Free Cancellation Date on BookingSummary
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination              | departure | return | guest | RuleDate |
	| Innahura Maldives Resort | 80        | 6      | 2,0,0 | 30       |


	#departure days 1st Jan to 30th April
Scenario Outline: Verify MultiRoom Hotel with Contract and Extranet
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then Check Free Cancellation Date with <departure> and <ContractRuleDate> In Estab Price Includes Section 
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on MultiRoom with <departure> and <ContractRuleDate> and <ExtranetRuleDate>
And Check Free Cancellation Date for MultiRoom on BookingSummary 
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination                   | departure | return | guests      | ContractRuleDate | ExtranetRuleDate |
	| Arabian Courtyard Hotel & Spa | 68        | 5      | 2,0,0:2,0,0 | 15               | 50               |
	



	





