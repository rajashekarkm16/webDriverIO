@v3regression
Feature: HotelLandingPage
	In order to validate the landing page
	As an end user
	I want to verify the functionality of Hotel landing pages for specific locations

Background: 
	Given Click on Accept and close cookies button
	And When I access Travel Republic site

#Hero Widget
Scenario Outline: Validate Hero widget with facade
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Check hero wideget is displayed
	And Hero image url matches the facade image url 
	Examples: 
	| DestinationName | DestinationID | ProductType |
	# To do
	#| florida         | 874           | 1           |
	| Australia       | 54            | 2           |

#Landing Page Search Modal Tests
@TC_2845173
Scenario: Hotel search on hotel landing page
Given I navigate to hotels landing page url
And Click on hotels tab on search modal on landing page
When I populate destination <destination> guests <guestsData> during <departureDate> and <returnDate> on landing page search modal
And Search for hotel availability on landing page
Then Hotel search results page should be displayed
And Search itinerary is populated accordingly for landing Page search
Examples: 
| destination | guestsData |departureDate | returnDate |
| Dubai       | 2,1,1      |60            | 7          |

@TC_2840932 @holidayregression
Scenario: Holiday search on hotel landing page
Given I navigate to hotels landing page url
When Click on holidays tab on search modal
And I populate destination <destination> airport <airport> guests <guestsData> during <departureDate> and <returnDate> in landing page search modal
And Search for holidays on landing page
Then Hotel search results page should be displayed
And Search itinerary is populated accordingly for landing Page search
Examples: 
| destination | airport | guestsData | departureDate | returnDate |
| Dubai       | London  | 2,1,1      | 60            | 7          |

#Head line price widget test
@TC_2840940
Scenario: Verify headline price widget text on hotel landing page
Given I navigate to hotels landing page url
Then Headline widget should display heading and description

# To do
#Scenario Outline: Verify headline price widget text with facade
#	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
#	And Add <DestinationID>, <ProductType> to FacadeContent
#	Then Headline widget heading and description matches with facade response
#	Examples: 
#	| DestinationName | DestinationID | ProductType |
#	| florida           | 874            | 1           |

#Intro Widget
@TC_2840941 @TC_2840943
Scenario: Verify Intro widget on hotel landing page
Given I navigate to hotels landing page url
Then Intro widget should be displayed
And Links on Intro widget should be clickable

# To do
#Scenario Outline: Verify Intro widget with facade
#	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
#	And Add <DestinationID>, <ProductType> to FacadeContent
#	Then Intro widget heading and description matches with facade response <ProductType>
#	And Links on Intro widget should be clickable
#	Examples: 
#	| DestinationName | DestinationID | ProductType |
#	| florida           | 874            | 1           |

@TC_2840942
Scenario: Verify Intro widget More/Less button on hotel landing page
Given I navigate to hotels landing page url
Then Intro widget should be displayed
When I click on more button
Then Intro text should be expanded
And Less button is displayed 
When I click on Less button
Then Intro text should be collapsed
And More button is displayed

@TC_2845174
# To do
#Scenario Outline: Validate breadcrumb on hotel landing page
#    When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
#	And Add <DestinationID>, <ProductType> to FacadeContent
#	Then Breadcrumb should be below the hero widget
#	And Breadcrumb item should match with facade response
#	And Validate breadcrumbs are navigating to respective pages <ProductType>
#	Examples: 
#	| DestinationName | DestinationID | ProductType |
#	| florida           | 874            | 1           |

@TC_2845168
Scenario Outline: Validate FAQ widget on hotel landing page
    When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then FAQ widget should be displayed
	And FAQ heading and content of all available list should match with facade response
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 1           |

	@TC_2840936 @TC_2840937
Scenario Outline: Validate footer link widget on hotel landing page
    When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Footer links widget should be displayed 
	And Footer links data should match with facade response <ProductType>
	And Validate hotel footer links widget navigation <DestinationName>
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 1           |

	@TC_2845128
Scenario Outline: Validate Map widget on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Map widget should be displayed and matches with the facade response
	And Map is not interactive
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 1           |

	@TC_2845161 @TC_2845162
Scenario Outline: Validate usp widget on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Usp on landing page should match with the facade response
	And Validate the usps on landing page
	Examples: 
	| DestinationName | DestinationID | ProductType |	
	| spain           | 17            | 1           |

	@TC_2845170 @TC_2845707
Scenario Outline: Validate key facts widget on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Key facts on landing page should match with the facade response	
	And Key facts items should be aligned to center
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 1           |

	@TC_2845324 @TC_2845704 TC_2845325
Scenario Outline: Validate interlinking widget details on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Interlinking widget should be displayed
	Then Interlinking widget heading and description should match with the facade response
	And Validate interlinking widget tabs details
	And Validate hotel cards details on each available tabs
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 1           |

	@TC_2845705
Scenario Outline: Validate top pick links on interlinking widget hotel cards on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Top pick destination should match with the facade response
	When I click a top pick destination
	Then Destination landing page should open in same tab
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 1           |

	@TC_2845326
Scenario Outline: Validate interlinking widget hotel cards selection on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	When I click on destination image
	Then Hotel landing page for selected destination should be displayed
	When I navigate back
	When I click on the View more button
	Then Hotel landing page for selected destination should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 1           |

	@TC_2850794
Scenario Outline: Validate overview/offers view tab on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Over view and offers view tab should be displayed
	When I click on offers tab
	Then Offers view tab should be displayed
	When I click on overview tab
	Then Overview tab should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| spain         | 17           | 1           |

	@TC_2845314 @TC_2845315 @TC_2845698 @TC_2845699
	Scenario Outline: Validate similar destinations widget details on hotels landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Similar destination widget should be displayed on hotels landing page
	And Similar destination title and description should match with the facade response
	And Similar destination tabs should not be displayed on hotels landing page
	And Validate hotel card details on similar destination widget
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 1           |

	@TC_2845316
	Scenario Outline: Validate similar destination widget card selection on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	When I click on destination image on similar destination hotel card
	Then Hotel landing page for selected destination should be displayed
	When I navigate back
	When I click on the select button on similar destination hotel card
	Then Hotel landing page for selected destination should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 1           |