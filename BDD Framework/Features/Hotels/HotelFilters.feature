@hotelregression @v3regression 
Feature: HotelFilters
	In order to validate the Hotel Filters
	As an end user
	I want to verify the functionality of Hotel filters

	Background: 
Given Click on Accept and close cookies button

@TC_1525307 @TC_1533939 @TC_1636926 @v3livesanity @MobileOnly
Scenario: Verify view matches on filter criteria
Given I am on hotel search results page
When I click on filter icon
Then View matches button should display the search result count
And Title should be Filter Results
And Filter model should have reset filter button

@TC_1533862 @v3livesanity
Scenario: Verify Total Price filter
Given I am on hotel search results page
When I click on filter icon
And Filter by Total Price
Then Validate by total price filter
And Check the filtered count in hotel search results

@TC_1533864
Scenario Outline: Verify Star Rating filter
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When I click on filter icon
Then Filter by available Star Ratings and validate it
Examples: 
| destination          | departure | return | guests |
| Dubai                | 55        | 7      | 2,0,0  |
#| Kingston Upon Thames | 55        | 7      | 2,0,0  |

@TC_1533911
Scenario Outline: Verify Our Customer Rating filter
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When I click on filter icon
Then Filter by available Customer Ratings and validate it
Examples: 
| destination   | departure | return | guests |
#| Kingston upon | 55        | 7      | 2,0,0  |
| Dubai         | 55        | 7      | 2,0,0  |

@TC_1533863
Scenario Outline: Verify Our Board type filter
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When I click on filter icon
Then Filter by available Board Type and validate it
Examples: 
| destination   | departure | return | guests |
| Dubai         | 55        | 7      | 2,0,0  |

@TC_1533863  @DesktopOnly
Scenario: Verify multiple Board type filter
Given  I am on hotel search results page
When I click on filter icon
Then Filter by multiple available Board Type and validate it

@TC_1762001 @MobileOnly
Scenario: Verify Close button of filter modal
Given  I am on hotel search results page
When I click on filter icon
And I click on close button
Then I want filter modal to be closed

@TC_1533913
Scenario: Verify Property Amenities filter
Given I am on hotel search results page
When I click on filter icon
Then Filter by property amenities and validate it

@TC_1533913
Scenario: Verify multiple Property Amenities filters
Given I am on hotel search results page
When I click on filter icon
Then Filter by multiple property amenities and validate it

@TC_1533912
Scenario: Verify Holiday Types filter
Given I am on hotel search results page
When I click on filter icon
Then Filter by holiday type and validate it

#When matches are zero then button is disabled
@MobileOnly
Scenario Outline: Verify view matches button is disabled
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When I click on filter icon
And I click on customer rating filter icon
Then View matches button should be disabled
Examples: 
| destination   | departure | return | guests |
| Tenerife      | 55        | 7      | 2,0,0  |

