@hotelregression @v3regression 
Feature: HotelBookingSummaryPage
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Booking Page

Background: 
Given Click on Accept and close cookies button

@TC_2774909 @TC_2774911 @TC_2774912
Scenario Outline: Verify hotel room selection
Given I perform hotel search for <destination> dates <departure> and <duration> with <guests>
When I select a random hotel from the results
And Select random rooms and board type
Then I should get the selected rooms in booking summary page
Examples: 
	| destination | departure | duration | guests            |
	| London      | 70        | 7        | 2,1,1:1,1,0:2,0,1 |
	| London      | 70        | 5        | 2,1,0:1,0,0       |
	| London      | 60        | 5        | 1,1,1             |
 

 @TC_2768604 @v3livesanity
Scenario: Verify details in booking summary page
	Given I am on hotel estab page
	When I select random rooms and board type
	Then I should get the selected rooms in booking summary page
	And Price is included in sticky header of booking form

#	Scenario: Verify change room functionality
#Given I am on booking summary page
#When Click change room link
#Then User is able to choose a different room in estab page

Scenario: Verify room information modal in booking summary
Given I am on booking summary page
When I click on the room information icon
Then Modal pop up is displayed with appropriate message