@hotelregression @v3regression 
Feature: GuestInfoPage
	In order to review the selected products
	As a mobile end user
	I want to verify the usability of Booking Page

Background: 
Given Click on Accept and close cookies button

@TC_2762711 @TC_2770075
Scenario Outline: Verify hotel room guests details
Given I perform hotel search for <destination> dates <departure> and <duration> with <guests>
When I select a random hotel from the results
And Select random rooms and board type
And Confirm the hotel booking selection
Then I can fill the passenger <name> details
And Add phone number
And Add email id
And Proceed to choose payment page
Examples: 
	| destination | departure | duration | guests            | name           |
	| London      | 80        | 2        | 2,0,0:2,0,0:2,0,0 | AutoTest       |
	| London      | 60        | 2        | 2,1,0:1,0,0       | TestAutomation |
	| London      | 60        | 2        | 2,0,1             | TestAutomation |

	@TC_2762715 @TC_2762716
Scenario: Verify name fields in guest information page
	Given I am on guest information page for hotel flow
	When I enter first  àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜçÇßØøÅåÆæ_-
	Then Firstname field accepts true information
	When I enter first abcdefghijklmnopqrstuvwxyz
	Then Firstname field accepts true information
	When I enter first ABCDEFGHIJKLMNOPQRSTUVWXYZ
	Then Firstname field accepts true information
	When I enter first 123
	Then Firstname field accepts false information
	When I enter sur àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜçÇßØøÅåÆæ_-
	Then Surname field accepts true information
	When I enter sur abcdefghijklmnopqrstuvwxyz
	Then Surname field accepts true information
	When I enter sur ABCDEFGHIJKLMNOPQRSTUVWXYZ
	Then Surname field accepts true information
	When I enter sur 123
	Then Surname field accepts false information

	@TC_2762718
	Scenario: Verify phone number field in guest information page
	Given I am on guest information page for hotel flow
	When I enter first test
	And I enter sur test
	And I enter phone adsd 
	Then Phonenumber field accepts false information
	When I enter phone !@#@#$$%
	Then Phonenumber field accepts false information
	When I enter phone 7894561230
	Then Phonenumber field accepts true information

	@TC_2762717 @TC_2762711
	Scenario: Verify email field in guest information page
	Given I am on guest information page for hotel flow
	When I enter first test
	And I enter sur test
	And I enter phone 12345
	And I enter email abc@com 
	Then Email field accepts false information
	When I enter email abc.com 
	Then Email field accepts false information
	When I enter email abc.@co
	Then Email field accepts false information
	When I enter email 12345
	Then Email field accepts false information
	When I enter email bjohnson@travelrepublic.com
	Then Email field accepts true information

	@TC_2762727
Scenario Outline: Verify special request field in guest information page
	Given I am on guest information page for hotel flow
	When I toggle the switch on for the special requests
	And Enter special request text with 1001 charcters
	Then Special request <error_message> should be displayed
	When Valid special request is entered
	Then Special request <error_message> should not be displayed
Examples: 
| error_message                          |
| Please enter less than 1000 characters |

Scenario: Verify mandatory fields validation in guest information page
	Given I am on booking summary page
	When I confirm the booking selection
	And I proceed to choose payment plan
	Then Error messages for the mandatory fields violation are displayed

	@TC2767002
Scenario: Verify Secure checkout message
   Given I am on guest information page for hotel flow
   Then Secure Checkout Message is displayed

   @TC2770078
Scenario: Verify Offers and Discount checkbox and text 
  	Given I am on guest information page for hotel flow
   Then Offers and Discount checkbox is displayed
   And Yes, I want to receive Travel Republic’s exclusive offers and discounts. text is displayed 
 
   @Tc_2770076 @holidayregression
Scenario: Verify Passenger Caption  Guest info page
Given I am on guest information page for holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then I want to see the passenger captions on guest information page
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 20        | 6      | 2,1,1  |

	@TC_2770077 @holidayregression
Scenario: Validate Date of birth field on guest info page
   Given I am on guest information page for holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
   When I enter invalid Date of birth in adult DOB field
   And I populate contact details
   Then  Appropriate error message should be displayed for incorrect adult age
   When  I enter invalid Date of birth in Child DOB field
   Then  Appropriate error message should be displayed for incorrect child age
   When  I enter invalid Date of birth in infant DOB field
  Then Appropriate error message should be displayed for incorrect infant age
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 30       | 6      | 2,1,1  |

   @TC_2770102
Scenario: verify lead guest details prepopulated 
  Given Sign in as a user from home page
  And I am on guest information page for hotel flow
  Then user details should be prepopulated in guest info
