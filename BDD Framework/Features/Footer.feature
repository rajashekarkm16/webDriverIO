@v3regression @v3livesanity
Feature: Footer
	In order to validate the Footer 
	As an end user
	I want to verify the functionality of Footer links

	Background: 
	Given Click on Accept and close cookies button

Scenario: Verify Footer on TR site
Given I am on hotel search results page
Then Footer should be as per design
And ABTA/ITAA logo should be displayed

Scenario: Verify Terms of Business Link
Given I am on hotel search results page
When I click Terms of Business Link on footer
Then Terms of Business modal should be displayed

Scenario: Verify Standard Cancellations Terms Link
Given I am on hotel search results page
When I click Standard Cancellations Terms link on footer
Then Standard Cancellations Terms site should be displayed


Scenario: Verify Footer links
Given I am on hotel search results page
Then Footer Links should be displayed and clickable

Scenario: Verify Call us number 
Given I am on hotel search results page
Then Call us number should be displayed




