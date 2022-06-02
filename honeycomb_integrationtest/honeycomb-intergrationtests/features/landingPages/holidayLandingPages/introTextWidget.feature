Feature: Intro Text Widget
    As an website customer
    Would like to check introTextWidget has the relavant information

Background:
Given I am on holiday landing page

@C2925903
Scenario: Verify the intro text widget
Then Landing page is displayed with intro text widget header and body text 

@C2925904
Scenario: Verify more and less links on intro text widget
Then More link should be displayed and enabled
When I click on more link
Then Intro Text should get enlarged
And Less link should be displayed and enabled
When I click on less
Then Intro text should be collapsed
And More link should be displayed and enabled