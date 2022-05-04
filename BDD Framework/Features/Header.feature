@v3regression @v3livesanity
Feature: Header
	In order to validate the Header 
	As an end user
	I want to verify the functionality of Header links

Background: 
	Given Click on Accept and close cookies button

@TC_1483579
Scenario: Verify Travel Republic Logo
Given I am on hotel search results page
When I click on Travel republic logo
Then Home page should be displayed

@TC_2774637
Scenario: Verify help centre logo
Given I am on hotel search results page
When I click on the help centre logo
Then User should be navigated to help centre page

@TC_2774626
Scenario: Verify header on TR site
Given I am on hotel search results page
Then Header should be as per design

@DesktopOnly @TC_2774639 @UKOnly
Scenario: Verify ATOL protected logo
Given I am on hotel search results page
When I click on ATOL protected logo
Then ATOL protected modal should be displayed
And Links on ATOL modal should be clickable

@TC_1483580 @TC_2774623 @TC_2774624
Scenario: Verify menu links from header
Given I am on hotel search results page
When I click on main menu icon
Then Main menu options should be displayed
When I click on Destinations from menu option
Then Second level menu options should be displayed
When I select available destination option 
Then Third level menu option relavant to selected destination should be displayed

@TC_1483581
Scenario: Verify Account menu when user is not signed in
Given I am on hotel search results page
When I click on account menu
Then Sign option should be displayed

@TC_2774615
Scenario: Verify Account menu when user is signed in
Given I am on hotel search results page
And Sign in as a user from hotel search results page
When I click on account menu
Then Account signed in menu options should be displayed