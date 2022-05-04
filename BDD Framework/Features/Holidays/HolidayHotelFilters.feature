@holidayregression @v3regression 
Feature: HolidayHotelFilters
	In order to validate the Hotel Filters
	As an end user
	I want to verify the functionality of Hotel filters

	Background: 
	Given Click on Accept and close cookies button

@MobileOnly
Scenario: Verify view matches on filter criteria
Given I am on holiday search results page
When I click on filter icon
Then View matches button should display the search result count
And Title should be Filter Results
And Filter model should have reset filter button

Scenario: Verify Total Price filter
Given I am on holiday search results page
When I click on filter icon
And Filter by Total Price
Then Validate by total price filter
And Check the filtered count in hotel search results

Scenario Outline: Verify Star Rating filter
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I click on filter icon
Then Filter by available Star Ratings and validate it
Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 55        | 7      | 2,0,0  |

@v3livesanityUK
Scenario Outline: Verify Our Customer Rating filter
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I click on filter icon
Then Filter by available Customer Ratings and validate it
Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 55        | 7      | 2,0,0  |


Scenario Outline: Verify Our Board type filter
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I click on filter icon
Then Filter by available Board Type and validate it
Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 55        | 7      | 2,0,0  |

@v3livesanityUK @DesktopOnly
Scenario: Verify multiple Board type filter
Given I am on holiday search results page
When I click on filter icon
Then Filter by multiple available Board Type and validate it

Scenario: Verify Property Amenities filter
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I click on filter icon
Then Filter by property amenities and validate it
Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 55        | 7      | 2,0,0  |

Scenario: Verify multiple Property Amenities filters
Given I am on holiday search results page
When I click on filter icon
Then Filter by multiple property amenities and validate it

Scenario: Verify Holiday Types filter
Given I am on holiday search results page
When I click on filter icon
Then Validate Filter by holiday type
