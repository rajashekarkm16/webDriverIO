@v3regression @UKOnly
Feature:  TravelInsurance
	In order to verify travel insurance
	As a tester
	I want to get insurance as a product in holidays bookings

Background: 
	Given Click on Accept and close cookies button

	@TC_1595372
Scenario Outline: Insurance default selection
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
Then I have my own travel insurance should be selected by default if insurance is available
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 30        | 6      | 2,0,0  |

	@TC_1595545
Scenario Outline: Validate More info information link
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Click on More insurance info link
Then Insurance information modal should be displayed
When I click on View Policy Documentation link
Then Policy documentation pdf link should be displayed
When I click on View Insurance Product Information Document
Then Insurance Product Information Document pdf link should be displayed
When I close the modal
Then Insurance information modal should be closed
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 30        | 6      | 2,0,0  |

	@TC_1595544
Scenario Outline: Validate View policy information link
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Click on View policy information link
Then Insurance details modal should be displayed
When I click on View Policy Documentation link
Then Policy documentation pdf link should be displayed
When I click on View Insurance Product Information Document
Then Insurance Product Information Document pdf link should be displayed
When I click on View Insurance Product Information Document
Then Insurance Product Information Document pdf link should be displayed
When I close the modal
Then Insurance details modal should be closed
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 30        | 6      | 2,0,0  |

	@TC_2791235 @TC_2791233
Scenario Outline: Check Add to basket button enabled and disabled with DOB
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Click on available insurance card
Then Add to basket button should be disabled
When I enter valid DOB for all occupants
Then Add to basket button should be enabled
When I enter invalid DOB for all occupants
Then Add to basket button should be disabled
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 20        | 6      | 2,0,0  |

	@TC_2772532 @TC_2790638 @TC_2791147
Scenario Outline: Check 'From' qualifier above occupants insurance price on insurance card
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Click on available insurance card
Then From qualifier should be displayed along with individual occupants insurance price
When I enter valid DOB for all occupants
Then From qualifier should not be displayed along with individual occupants insurance price

Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 30        | 6      | 2,0,0:2,1,1 |

	@TC_2791305
Scenario Outline: Verify DOB is retained when changing insurance selection
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Click on option 1 on the available insuarce card
And I enter valid DOB for all occupants
And Click on option 2 on the available insuarce card
Then Occupants DOB data should be retained on the new selection
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Dubai       | London            | 30        | 6      | 2,0,0:2,1,1 |

	@TC_2790826 @TC_2805586
Scenario Outline: Verify update basket button on insurance card
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Add a Travel Insurance
And Check selected travel insurance and price is updated in booking summary
And Change adult DOB to other insurance band
And Click on update basket button
Then Check selected travel insurance and price is updated in booking summary
When I enter invalid DOB for all occupants
Then Update basket button should be disabled
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Dubai       | London            | 25        | 6      | 2,0,0:2,1,1 |

	@TC_2805585 @TC_2790641
Scenario Outline: DOB field validations on insurance card
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When I am on holiday extras page with preselected flight
And Click on available insurance card
And I enter invalid DOB for all occupants
Then "Please enter a valid date of birth" error message should be displayed on inusrance card
And Appropriate error message is displayed for adult age less than 18
And Appropriate error message is displayed for child age different from searched age
And Appropriate error message is displayed for infant age different from searched age
And Green tick should be displayed on entering valid DOB for all occupants
And Appropriate error message is displayed for adult age more than 84
Examples:
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 20        | 6      | 2,1,1  |

	@TC_2815396 @v3livesanityUK
Scenario Outline: Verify Add Insurance button from Pop up 
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Select I have my own travel insurance option
And Click on continue to book button
Then Insurance not added pop up should be displayed
When I click on add insurance button
Then Pop up should be closed and available Insurance options should be displayed
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 90        | 6      | 2,0,0  |
	
	@TC_2815399
Scenario Outline: Add and remove travel insurance
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Add a Travel Insurance
And Check selected travel insurance and price is updated in booking summary
And Select I have my own travel insurance option
Then Travel insurance should be removed from booking summary
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Dubai       | London            | 25        | 6      | 2,0,0  |

	@TC_1595376	@TC_1595377 @TC_1595378
Scenario Outline: Check Insurance products cards based on regions
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
Then Insurance product name should be based on <destination>
# Please dont change below test data
Examples: 
	| destination | departure_airport | departure | return | guests |
	| Tenerife    | London            | 30        | 6      | 2,0,0  |
	| Dubai       | London            | 70        | 6      | 2,0,0  |
	| New York    | London            | 20        | 6      | 2,0,0  |

	@TC_2791149
Scenario Outline: Check infants insurance price on insurance card
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates 
When I am on holiday extras page with preselected flight
And Click on available insurance card
And I enter valid DOB for all occupants
Then Infant price should be zero
Examples: 
	| destination | departure_airport | departure | return | guests      |
	| Dubai       | London            | 30        | 6      | 2,1,1:2,1,1 |