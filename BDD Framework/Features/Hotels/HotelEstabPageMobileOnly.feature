@hotelregression @v3regression 
Feature: HotelEstabPageMobileOnly
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Booking Summary Page


Background: 
Given Click on Accept and close cookies button


@TC_2762719 @MobileOnly @v3livesanity
Scenario: Verify choose rooms button
Given I am on hotel estab page
Then Choose room button is displayed
When I click on the choose room button
Then Rooms tab is selected by default
And Rooms tab is scrolled to the top of the page

@MobileOnly @TC_2774906 @TC_2791682
Scenario: Verify your room button and room summary page
Given I perform hotel search for London dates 55 and 7 with 2,0,0:2,0,0
When I select a random hotel from the results
And I select the first room
Then Your room button is displayed on the sticky footer
And Clicking on your room sticky button opens the room summary page

@MobileOnly @TC_2791683
Scenario: Verify change room link in room selection summary
Given I perform hotel search for London dates 60 and 7 with 2,0,0:2,0,0
When I select a random hotel from the results
And I select the first room
And Click on your room button on sticky footer
And Click on change room link for room 1
Then Rooms tab is selected by default
And Pre selected room is displayed

@MobileOnly @TC_2791684 @v3livesanity
Scenario: Verify close room selection summary popup
Given I perform hotel search for London dates 55 and 7 with 2,0,0:2,0,0
When I select a random hotel from the results
And I select the first room
And Click on your room button on sticky footer
And Close the room selection summary
Then First room tab is marked as green
And Second room is marked as blue

@MobileOnly @TC_2775794
Scenario: Verify free cancellation message in board type selection page
	Given I perform a hotel search for Dubai
	When I select a random hotel from the results
	And I select random room
	Then Free cancellation message is displayed in board type page

	@MobileOnly @TC_2775795
Scenario: Verify non refundable message in board type selection page
	Given I perform hotel search for Dubai dates 4 and 3 with 2,0,0
	When I select a random hotel from the results
	And I select random room
	Then Non refundable message is displayed in board selection page

	@MobileOnly @TC_2775796 @v3livesanity
Scenario: Verify secure today pill in board type selection page
	Given I perform a hotel search for Dubai
	When I select a random hotel from the results
	And I select random room
	Then Secure today pill is displayed on in board type page

#	@MobileOnly @TC_2775797
#Scenario: Verify pay monthly pill in board type selection page
#	Given I perform hotel search for Dubai dates 190 and 3 with 2,0,0
#	When I select a random hotel from the results
#	And I select random room
#	Then Pay monthly pill is displayed on in board type page

	@MobileOnly @TC_2775797
Scenario: Verify board type filter dropdown in board type selection page
	Given I perform a hotel search for Dubai
	When I select a random hotel from the results
	And I select random room
	And Select a random board type filter from board type dropdown
	Then Selected filter board type is only displayed

	@MobileOnly @TC_2792458
Scenario: Verify change room link in room selection for multirooms
     Given I perform hotel search for London dates 60 and 7 with 2,0,0:2,0,0:2,0,0
	 When I select a random hotel from the results
     And I select the distinct room types
	 And Click on your room button and select change room link for room 1
	 Then pre selected room 1 is highlighted and displayed with info
	 When Click on your room button and select change room link for room 2
	 Then pre selected room 2 is highlighted and displayed with info