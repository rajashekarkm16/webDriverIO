@DynamicCancellation
Feature: Dynamic Cancellation Holiday
	In order to verify  Dynamic cancellation feature  
	As a tester
	I want to get free cancellation modal and verify functionality 

Background: 
Given Click on Accept and close cookies button
	

# 2Adults scenario checkin in date should be before or untill 31st Dec
# 3Adults and ruledate is 3 scenario checkin in date should be before or untill 31 Aug
#| Paladim & Alagoamar | London            | 10        | 5      | 3,0,0  | 4          | 3        |
# rule date 30 - from 1sep to 31st sep
#| Paladim & Alagoamar | London            | 15        | 7      | 3,0,0  | 4          | 30       |
# rule date 60 - from 1oct to 31st dec
Scenario Outline:Verify Holiday with Extranet  
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then Check Free Cancellation Date with <departure> and <RuleDate> In <destination> Hotel Card 
When Select <destination> from the search results
Then Check Free Cancellation Date with <departure> and <RuleDate> In Estab Price Includes Section 
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on roomType 1 and boardType 1
When I select room type 1 and board type 1
And Confirm the pre selected flight
And Continuetobook without any Extras
Then Check Free Cancellation Date on BookingSummary
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available


Examples: 
	| destination         | departure_airport | departure | return | guests | RuleDate |
	| Paladim & Alagoamar | London            | 15        | 5      | 2,0,0  | 7        |
	| Paladim & Alagoamar | London            | 95        | 7      | 3,0,0  | 60       |
	

# 4 days scenario checkin in date should before or untill 14th sep 
# 10 days scenario checkin in date should be from 15th sep to 31st oct
Scenario Outline: Verify Holiday with Contract
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
#Then Check Free Cancellation Date with <departure> and <RuleDate> In <destination> Hotel Card 
#When Select <destination> from the search results
Then Check Free Cancellation Date with <departure> and <RuleDate> In Estab Price Includes Section 
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on roomType 1 and boardType 1
When I select room type 1 and board type 1
And Confirm the pre selected flight
Then Check Free Cancellation Date on BookingSummary
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available

Examples: 
	| destination              | departure_airport | departure | return | guests | RuleDate |
	| Innahura Maldives Resort | London            | 110       | 6      | 2,0,0  | 3        |
	| Innahura Maldives Resort | London            | 65        | 6      | 2,0,0  | 15       |
	

	#30 days scenario checkin in date should be 1st nov to 31st dec
Scenario Outline: Verify Holiday with Contract for Thirty days Rule
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then Check Free Cancellation Date with <departure> and <RuleDate> In <destination> Hotel Card 
When Select <destination> from the search results
Then Check Free Cancellation Date with <departure> and <RuleDate> In Estab Price Includes Section 
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on roomType 1 and boardType 1
When I select room type 1 and board type 1
And Confirm the pre selected flight
Then Check Free Cancellation Date on BookingSummary
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available

Examples: 
	| destination              | departure_airport | departure | return | guests | RuleDate |
	| Innahura Maldives Resort | London            | 80        | 6      | 2,0,0  | 30       |


Scenario Outline: Verify MultiRoom Holiday with Contract and Extranet
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then Check Free Cancellation Date with <departure> and <ContractRuleDate> In <destination> Hotel Card 
When Select <destination> from the search results
Then Check Free Cancellation Date with <departure> and <ContractRuleDate> In Estab Price Includes Section 
And Clicking the message opens free cancellation dialog modal on estab page
And Check Free Cancellation Date on MultiRoom with <departure> and <ContractRuleDate> and <ExtranetRuleDate>
When Confirm the pre selected flight
Then Check Free Cancellation Date for MultiRoom on BookingSummary 
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
Examples: 
	| destination                   | departure_airport | departure | return | guests      | ContractRuleDate | ExtranetRuleDate |
	| Arabian Courtyard Hotel & Spa | London            | 68        | 5      | 2,0,0:2,0,0 | 15               | 50               |
	





