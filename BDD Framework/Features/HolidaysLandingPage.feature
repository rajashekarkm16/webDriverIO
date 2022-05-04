@v3regression
Feature: HolidaysLandingPage
	In order to validate the landing page
	As an end user
	I want to verify the functionality of Holidays landing pages for specific locations

Background: 
	Given Click on Accept and close cookies button
	And When I access Travel Republic site


#Landing Page Search Modal Tests
@TC_2840934
Scenario: Hotel search on holiday landing page
Given I Navigate to holidays landing page url
When Click on hotels tab on search modal on landing page
When I populate destination <destination> guests <guestsData> during <departureDate> and <returnDate> on landing page search modal
And Search for holidays on landing page
Then Hotel search results page should be displayed
And Search itinerary is populated accordingly for landing Page search
Examples: 
| destination | guestsData |departureDate | returnDate |
| Dubai       | 2,1,1      |60            | 7          |

@TC_2845171 @holidayregression
Scenario: Holiday search on holiday landing page
Given I Navigate to holidays landing page url
When I populate destination <destination> airport <airport> guests <guestsData> during <departureDate> and <returnDate> in landing page search modal
And Search for holidays on landing page
Then Hotel search results page should be displayed
And Search itinerary is populated accordingly for landing Page search
Examples: 
| destination | airport | guestsData | departureDate | returnDate |
| Dubai       | London  | 2,1,1      | 20            | 7          |

#Intro Widget
@TC_2840941 @TC_2840943
Scenario: Verify Intro widget on holiday landing page
Given I Navigate to holidays landing page url
Then Intro widget should be displayed
And Links on Intro widget should be clickable

Scenario Outline: Verify Intro widget with facade
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Intro widget heading and description matches with facade response <ProductType>
	And Links on Intro widget should be clickable
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Australia       | 54            | 2           |

@TC_2840942
Scenario: Verify Intro widget More/Less button on holidays landing page
Given I Navigate to holidays landing page url
Then Intro widget should be displayed
When I click on more button
Then Intro text should be expanded
And Less button is displayed 
When I click on Less button
Then Intro text should be collapsed
And More button is displayed

@TC_2845174
Scenario Outline: Validate  breadcrumb on holidays landing page
    When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Breadcrumb should be below the hero widget
	And Breadcrumb item should match with facade response
	And Validate breadcrumbs are navigating to respective pages <ProductType>
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Benidorm        | 5179          | 2           |

	@TC_2845168
Scenario Outline: Validate FAQ widget on holiday landing page
    When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then FAQ widget should be displayed
	And FAQ heading and content of all available list should match with facade response
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Benidorm        | 5179          | 2           |

	@TC_2845130
Scenario Outline: Validate footer link widget on holiday landing page
    When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Footer links widget should be displayed
	And Footer links data should match with facade response <ProductType>
	And Validate holiday footer links widget navigation <DestinationName>
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Benidorm        | 5179          | 2           |

	@TC_2845128
Scenario Outline: Validate Map widget on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Map widget should be displayed and matches with the facade response
	And Map is not interactive
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Benidorm        | 5179          | 2           |

	@TC_2845161 @TC_2845162
Scenario Outline: Validate usp widget on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Usp on landing page should match with the facade response
	And Validate the usps on landing page
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Benidorm        | 5179          | 2           |

	@TC_2845170 @TC_2845707
Scenario Outline: Validate key facts widget on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Key facts on landing page should match with the facade response
	And Key facts items should be aligned to center
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845159 @TC_2845160
Scenario Outline: Validate weather widget on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Weather widget should be displayed and information should match with facade response
	And Current month bar is selected by default
	When I click on the other month
	Then Only the selected month bar should be highlighted 
	And Temperature on top of the bar must match with average
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845164
Scenario Outline: Validate flight routes widget on holiday landing page for province
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Flight route widget should be displayed and information should match with facade response
	And I should be able to scroll through the available option	and validate content count per page
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845708
Scenario Outline: Validate flight routes widget on holiday landing page for country
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Flight route widget should not be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| spain           | 17            | 2           |

	@TC_2845166 @TC_2845167
Scenario Outline: Validate blog widget on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Blog widget should be displayed and information should match with facade response
	And I should be able to navigate to blog content and it should be linked to corresponding blog page
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

		@TC_2845124 @TC_2845125 @TC_2845126 @ukonly
Scenario Outline: Validate image gallery widget on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Image gallery widget should be displayed
	And I should be able to navigate through image gallery and content should match with facade response
	When I click on the open full screen icon
	Then Image should be displayed in full screen
	When I click on the close full screen icon
	Then Image should be collapsed
	Examples:
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845324 @TC_2845704 TC_2845325
Scenario Outline: Validate interlinking widget details on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Interlinking widget should be displayed
	Then Interlinking widget heading and description should match with the facade response
	And Validate interlinking widget tabs details
	And Validate hotel cards details on each available tabs
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 2           |

	@TC_2845705
Scenario Outline: Validate top pick links on interlinking widget holiday cards on hotel landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Top pick destination should match with the facade response
	When I click a top pick destination
	Then Destination landing page should open in same tab
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 2           |

	@TC_2845924
Scenario Outline: Validate more link on interlinking widget hotel cards on holidays landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Hotel cards with more link should be displayed and description on modal should with the facade response
	When I click on search button on the more description modal
	Then I should be navigated to respective destination landing page
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845326
Scenario Outline: Validate interlinking widget cards selection on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	When I click on destination image
	Then Holiday landing page for selected destination should be displayed
	When I navigate back
	When I click on the View more button
	Then Holiday landing page for selected destination should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2850794
Scenario Outline: Validate overview/offers view tab on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Over view and offers view tab should be displayed
	When I click on offers tab
	Then Offers view tab should be displayed
	When I click on overview tab
	Then Overview tab should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| Spain           | 17            | 2           |

	@TC_2845470 @TC_2845538 @TC_2845706
Scenario Outline: Validate theme widget details on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Theme widget should be displayed
	And Theme widget heading and description should match with the facade response
	And Validate card details on theme widget
	When I select random card on theme widget
	Then Holiday landing page should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845314 @TC_2845315 @TC_2845698 @TC_2845699
Scenario Outline: Validate similar destinations widget details on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	Then Similar destination widget should be displayed on holiday landing page
	And Similar destination title and description should match with the facade response
	And 2 similar destination tabs should be displayed on holidays landing page
	And Validate holiday card details on each available similar destination tabs	
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |

	@TC_2845316
Scenario Outline: Validate similar destination widget card selection on holiday landing page
	When I navigate to landing page <DestinationName>, <DestinationID>, <ProductType>
	And Add <DestinationID>, <ProductType> to FacadeContent
	When I click on destination image on similar destination holiday card
	Then Holiday landing page for selected destination should be displayed
	When I navigate back
	When I click on the select button on similar destination holiday card
	Then Holiday landing page for selected destination should be displayed
	Examples: 
	| DestinationName | DestinationID | ProductType |
	| costa blanca    | 206           | 2           |