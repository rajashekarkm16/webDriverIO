@hotelregression @v3regression 
Feature: HotelEstabPage
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Booking Summary Page


Background: 
Given Click on Accept and close cookies button

@TC_2772533 @v3livesanity
Scenario: Hotel Estab page validation
	Given I am on hotel search results page
	When I store the information of the hotel to be selected from the results
	Then I should see the selected hotel estab page with rooms

	@TC_2774903 @TC_2774904 @TC_2775782 @TC_2775791
Scenario: Hotel Estab page room selection validation
	Given I am on hotel estab page
	When I select random rooms and board type
	Then I should get the selected rooms in booking summary page

Scenario Outline: Verify search itinerary and change search validation from hotel estab page
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When I select a random hotel from the results
Then I should see the selected hotel estab page
And Search itinerary is populated accordingly in estab page
Examples: 
| destination | departure | return | guests            |
| Dubai       | 55        | 7      | 2,1,1:1,1,0:1,0,0 |
| Tenerife    | 90        | 7      | 2,1,1:1,0,0       |
| Tenerife    | 45        | 10     | 2,1,1             |


#Descoped
#Scenario: New search validation from hotel estab page
#Given I am on hotel estab page
#When I click on search icon
#And Select new search
#Then I am redirected to home page

@TC_2762580
Scenario: Hotel Estab page per person price validation
	Given I am on hotel search results page
	When I select a random hotel from the results
	And Toggle to per person price
	Then Total price should match accordingly

	@v3livesanity
	Scenario: Verify per person price toggle updates main room price
	Given I am on hotel search results page
	When I select a random hotel from the results
	Then Main price displays total price and also per person price below
	When Toggle to per person price
	Then Main room price should display only per person price

	
	Scenario: Default price selected in hotel estab page
	Given I am on hotel search results page
	When I select a random hotel from the results
	Then Total price is the default price selected
	#And Background color of selected price is blue
	#And Toggle to per person price updates the color

	@TC_2762568 @TC_2774772 @v3livesanity
	Scenario:  Verify sections in OverView tab
	Given I am on hotel estab page
	Then Overview tab is selected by default
	And Hotel information section is displayed
	And Hotel information toggle switch loads content
	And Facilities section is displayed
	And Faciltities options are displayed

	@TC_2777543 @TC_2762577 @TC_2762595 @TC_2774737 @TC_2762596 @TC_2762597 @v3livesanity
	Scenario: Verify reviews tab
	Given I am on hotel search results page
	And Select a hotel with reviews
	Then Overview tab contains reviews section
	And Clicking on see all customer reviews takes user to reviews tab
	And Reviews are sorted from newer to older dates
	And See more reviews button is displayed
	And More reviews are loaded on clicking see more reviews
	And Moving to overview tab and back to reviews tab contains the last loaded reviews
	And See whole review link is displayed if review has more characters

	@TC_2762575 @TC_2762598
	Scenario Outline: Verify hotel with no reviews 
	Given I perform a hotel search for <hotelName>
	When I click on reviews tab
	Then No reviews should be displayed
	Examples:
	| hotelName                      |
	| Old street superior apartments |

@TC_2762576 @TC_2774825
Scenario: Verify Show whole or show less link in overviews tab
Given I perform a hotel search for Eurostars Grand Marina Hotel
Then Overview tab is selected by default
And I want to see faded content
When I click on Show whole review
Then Show less link should be displayed

	@TC_2762578
Scenario Outline: Verify see all reviews button is hidden for hotel with less than 3 reviews
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
Then Overview tab is selected by default
And See all customer reviews button is not displayed
Examples: 
	| destination         | departure | return | guests |
	| Reethi Beach Resort | 45        | 7      | 2,0,0  |

	@DesktopOnly @TC_2774819
Scenario: Verify your room options button in desktop
Given I am on hotel estab page
Then Your room options button is displayed
When I click on the your room options button
Then Rooms tab is selected by default
And Rooms tab is scrolled to the top of the page

@TC_2762702
Scenario: Verify hotel estab page design
Given I am on hotel estab page
Then Overview tab is selected by default
And Price includes section is displayed in estab page

@TC_2762720
Scenario: Verify clicking back from estab retains the search results
Given I am on hotel search results page
And Capture all hotel search results
When I select a random hotel from the results
And I click back button
Then Hotel search results remains unchanged

@TC_2762701
Scenario: Verify edit search modal in estab page
Given I am on hotel estab page
When I click on search icon
Then Search modal is displayed

@TC_2762704 @v3livesanity
Scenario Outline: Verify secure today pill in estab page
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>
When I select a random hotel from the results
Then Secure today pill is displayed in estab page
And Deposit price matches in the secure today pop up modal on estab page
Examples:
| destination | departure | return | guests      |
| Dubai       | 100       | 7      | 2,0,0       |
| Dubai       | 100       | 7      | 2,0,0:2,0,0 |

@TC_2762705 @v3livesanity
Scenario: Verify free cancellation message in estab page
	Given I perform a hotel search for Majorca
	When I select a random hotel from the results
	Then Free cancellation message is displayed in estab page
	And Clicking the message opens free cancellation dialog modal on estab page

	@UKOnly @TC_2762714
	Scenario: Verify city taxes displayed in estab page
	Given I perform a hotel search for Dubai
	When I select a random hotel from the results
	Then I can see the local charges information link displayed in hotel estab page

	#@TC_2775813 @TC_2818483
	#Scenario Outline: Verify pay monthly pill in estab page
	#Given I perform a hotel search for Dubai
	#When I select a random hotel from the results
	#Then Pay monthly pill is displayed on estab page
	#And Monthly deposit price matches in the secure today pop up modal on estab page

	@TC_2762550 @UKOnly
	Scenario: Verify return transfers in hotel estab page	
	Given I perform hotel search for Reethi beach resort dates 30 and 7 with 2,1,0
	Then Return transfers message is displayed

@TC_2774904 @TC_2774910 @TC_2774905 @TC_2774911 @TC_2762586
Scenario: Verify room selection higlights the selected room number
Given I perform hotel search for London dates 55 and 7 with 2,1,0:2,1,1
When I select a random hotel from the results
And Select rooms tab
Then First room tab is marked as blue during selection
And Occupancy for room 1 is displayed
And Second room is marked as grey
When I select the first room
Then Toast message is displayed on mobile
And First room tab is marked as green
And Second room is marked as blue
And Occupancy for room 2 is displayed
When I select the first room stepper
Then First room tab is marked as blue during selection
And Second room is marked as grey

@TC_2762603 @UKOnly
Scenario Outline: Verify taxes for total and per person price
Given I perform hotel search for <destination> dates <departure> and <return> with <guests>	
When I select a random hotel from the results
Then Local charges Payable at hotel message is displayed in estab page
And Capture local charges amount from modal pop up
When Toggle to per person price
Then Taxes should match accordingly
Examples: 
| destination | departure | return | guests      |
| Lisbon      | 55        | 7      | 1,0,0:1,1,1 |
| Dubai       | 90        | 7      | 2,0,0       |

@TC_2774641
	Scenario: Verify non refundable message in hotel search results
	Given I perform hotel search for London dates 4 and 3 with 2,0,0
	When I select a random hotel from the results
	Then Non refundable option is displayed
	And Clicking the message opens non refundable message dialog modal