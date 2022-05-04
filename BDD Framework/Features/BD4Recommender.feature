@v3regression
Feature: BD4Recommender
	In order to validate BD4Recommender 
	As an end user
	I want to verify the functionality of BD4 Recommender cards

Background: 
	Given Click on Accept and close cookies button

	@TC_2807050 @v3livesanity
Scenario Outline: Verify hotel recommender card from search results page
Given I perform a hotel search for <destination>
Then I should see hotel recommender displayed
When I click on hotel recommender
Then Hotel estab should be displayed
And Hotel information should match as per the selected recommender card
Examples: 
| destination    |
| Dubai          |
| Jumeirah Beach |

@TC_2807052
Scenario Outline: : Verify hotel recommender card from estab page
Given I perform a hotel search for <destination>
When I select a random hotel from the results
Then I should see hotel recommender displayed
When I click on hotel recommender
Then Hotel estab should be displayed
And Hotel information should match as per the selected recommender card
Examples: 
| destination |
| Dubai       |

@TC_2807059
Scenario Outline: Verify hotel recommender should not be displayed when no results available on hotel search results page
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When  No search results are available
Then Hotel recommender should not be displayed
Examples:
| destination | departure | return | guests      |
| Maldives    | 100       | 6      | 2,5,2:2,2,2 |

@TC_2810425
Scenario Outline: Verify hotel recommender should not be displayed when no rooms available on estab page
Given I perform hotel search for <Hotel_Name> dates <departure> and <return> with <guests>
When  No rooms are available
Then Hotel recommender should not be displayed
Examples:
| Hotel_Name            | departure | return | guests |
| Sikara Hotel, Chennai | 100       | 6      | 2,1,1  |

@TC_2807051 @holidayregression
Scenario Outline: : Verify holiday recommender card from search results page
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then I should see hotel recommender displayed
When I click on hotel recommender
Then Hotel estab should be displayed
And Hotel information should match as per the selected recommender card
Examples: 
| destination    | departure_airport | departure | return | guests |
| Dubai          | London            | 55        | 7      | 2,0,0  |
| Jumeirah Beach | London            | 55        | 7      | 2,0,0  |

@TC_2807053 @v3livesanityUK @holidayregression
Scenario Outline: : Verify holiday recommender card from estab page
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I select a random hotel from the results
Then I should see hotel recommender displayed
When I click on hotel recommender
Then Hotel estab should be displayed
And Hotel information should match as per the selected recommender card
Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 55        | 7      | 2,1,1  |

@TC_2807059 @holidayregression
Scenario Outline: : Verify holiday recommender card should not be displayed when no results available on search results page
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When  No search results are available
Then Hotel recommender should not be displayed
Examples: 
| destination | departure_airport | departure | return | guests            |
| Florida     | London            | 55        | 7      | 4,1,1:2,1,0:2,1,1 |