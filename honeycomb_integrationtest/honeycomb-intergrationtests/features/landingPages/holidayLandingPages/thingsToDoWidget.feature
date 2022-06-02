Feature: Things To Do Widget
    As an website customer
    Would like to check thingsToDoWidget has the relavant information

Background:
Given I am on holiday landing page

@C2925155 @C2925896
Scenario: Verify the Things to do widget and action on images and text
Then Landing page is displayed with things to do widget header and body text
And Tabs, images and image text should be displayed
And Images are not clickable
And Header and body text are not clickable

@C2925161
Scenario: Verify the Things to do widget tabs selection behaviour
When I select any tab
Then Selected Tab should be highlighted
And Tab image and text are displayed

@C2925162 @skip
Scenario: Verify the navigation between the images for things to do widget
When I click on next
Then Next image should be displayed
When I click on previous
Then Previous image should be displayed

@C2925897 @skip
Scenario: Verify the widget when there are no images added
Given there is no image added on the tab content
Then entire tab should be displayed without image
