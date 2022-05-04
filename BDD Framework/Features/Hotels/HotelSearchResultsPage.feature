@hotelregression @v3regression 
Feature: HotelSearchResultsPage
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Hotel serach results page

Background: 
Given Click on Accept and close cookies button

@TC_2778046 @TC_1517466
Scenario: Verify default hotel search result count
Given I am on hotel search results page
Then Hotel search results count is 15


@TC_1517573 @TC_2774799
Scenario Outline: Verify No rooms available message on SRP
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then  No rooms available <message> should be displayed
#And Start a new search button should be displayed
#When I click on Start a new search button
#Then Home page should be displayed
Examples:
| destination           | departure | return | guests | message                                                    |
| Sikara Hotel, Chennai | 100       | 6      | 2,1,1  | We couldn't find any rooms available for your chosen dates |

@TC_1518058 @v3livesanity
Scenario: Verify show more button 
Given I am on hotel search results page
When I click show more hotels button
Then Additional 15 results should be loaded
And The “show more hotels” button should move to the bottom of the list


Scenario Outline: Verify CMA compliance text
	Given I am on hotel search results page
	Then CMA compliance <CMAText> is displayed
	Examples: 
	| CMAText                                                                             |
	| Some of these hotels are sponsored, which may affect their position in our listings |

	@TC_1567013 @UKOnly
Scenario Outline: : Verify city taxes is displayed for hotel searches
Given I perform a hotel search for <destination>
Then local charges information link should be displayed on hotel tile
Examples: 
| destination |
| Dubai       |

@TC_1678763
Scenario: Back button on search results page
Given I am on hotel search results page
When I click back button
Then Home page should be displayed

@TC_1678764
Scenario Outline: Back button when no rooms available on estab page
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When  No rooms are available 
And I click back button
Then Home page should be displayed
Examples:
| destination           | departure | return | guests |
| Sikara Hotel, Chennai | 100       | 6      | 2,1,1  |

@TC_1518059 @TC_1517446
	Scenario: Verify Map View
	Given I am on hotel search results page
	When I Click on Map view link
	Then Map view is displayed
	And Multiple hotel locations are displayed

	@TC_1518060 @TC_2774826
	Scenario: Verify select hotel from map view
	Given I am on hotel search results page
	When I Click on Map view link
	And Store hotel information for the pin point to be selected from the map
	Then Selected hotel information matches in the estab page

	@TC_2775351 @TC_ 2774813 @TC_2762615 @TC_2774744 @TC_2762624 @TC_2774796 @TC_2774797 @TC_2774800 @TC_2777685
	Scenario Outline: Verify Search itinerary in search results page
	Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
	Then Search itinerary is populated accordingly
	And Hotels in <destination> shown
	And <destination> is shown search itinerary summary
	Examples: 
	| destination | departure | return | guests      |
	| Tenerife    | 25        | 7      | 2,1,1:1,1,0 |
	| Lisbon      | 45        | 10     | 2,1,1       |
	| London      | 25        | 7      | 2,1,1:1,1,0:1,0,0 |

#	@TC_1517573
#Scenario: New search validation from hotel search results page
#Given I am on hotel search results page
#When I click on search icon
#And Select new search
#Then I am redirected to home page

@TC_1567188 @TC_1653738 @TC_2772816 @TC_2772817 @TC_2772818 @v3livesanity
Scenario Outline: Verify sort in hotel search results
#Accepts actual text from UI as test data. 
	Given I am on hotel search results page
	When I open sort options
	And Select the sort <option>
	Then Search results are sorted based on the selected <option>
	And Duplicate search results are not displayed
	Examples:
	| option                          |
	| Customer Rating (Highest first) |
	| Price (Cheapest first)          |
	| Star Rating (Highest first)     |

@TC_2772537
	Scenario: Validate the bread crumb
	Given I am on hotel search results page
	Then Validate the breadcrumb on hotel results page

	Scenario: Validate the usps 
	Given I am on hotel search results page
	Then Validate the usps on hotel results page

@TC_2777684
Scenario: Verify close sort in hotel search results
	Given I am on hotel search results page
	And Capture all hotel search results
	When I open sort options
	And Close the sort model
	Then Hotel search results remains unchanged 

	@TC_2775799
	Scenario: Verify secure today pill in hotel card
	Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
	Then Secure today pill is displayed
	And Deposit price matches in the secure today pop up modal
	Examples:
	| destination | departure | return | guests      |
	| Dubai       | 100       | 7      | 2,0,0       |
	| Dubai       | 100       | 7      | 2,0,0:2,0,0 |


	#@TC_2775800 @TC_2818373
	#Scenario Outline: Verify pay monthly pill in hotel card
	#Given I perform a hotel search for Dubai
	#Then Pay monthly pill is displayed for hotels
	#And Monthly deposit price matches in the secure today pop up modal

	@TC_2772535
	Scenario: Verify free cancellation message in hotel search results
	Given I perform a hotel search for Dubai
	Then Free cancellation message is displayed in hotel card
	And Clicking the message opens free cancellation dialog modal

	@TC_2772724
	Scenario: Verify non refundable message in hotel search results
	Given I perform hotel search for London dates 4 and 3 with 2,0,0
	Then Non refundable message is displayed in hotel card
	And Clicking the message opens non refundable dialog modal

	@TC_2772530
Scenario Outline: Search alternative dates when no available results on search 
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When No search results are available
When I click Search alternative date button
Then Search modal is displayed
Examples:
| destination | departure | return | guests |
| Oeiras      | 3         | 7      | 12,0,0 |
