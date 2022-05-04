@hotelregression @v3regression @end2end 
Feature: HotelsDisneyAttractions
	In order to make hotel booking
	As a end user
	I want to choose a hotel of my choice

Background: 
Given Click on Accept and close cookies button


Scenario Outline: Verify Disney Attractions are shown on hotel booking summary
Given I perform hotel search for <hotelName> dates <departure> and <return> with <guests>
When Select random rooms and board type
Then Additional Package Details should be shown on booking summary for <return> days booking
When I continue to Guest details
When I fill the passenger <name> details
And Add phone number
And Add email id
And Proceed to choose payment page
#Then AdditionalPackageDetails should be shown on booking summary for <return> days booking
And Complete the full payment hotel booking using VisaCredit payment from Payment step
Then Booking references of booked items are available
And Validate that the <return> mandatory extras are shown on confirmation page
And Validate the <return> mandatory disney ticket details are saved
Examples: 
| hotelName        | departure | return | guests      | 
| Disneyland Hotel | 180       | 14     | 2,0,0:2,1,0 |
| Disneyland Hotel | 180       | 7      | 2,0,0       | 



Scenario Outline: Verify Disney Attractions are not shown on hotel booking summary
Given I perform hotel search for <hotelName> dates <departure> and <return> with <guests>
When Select random rooms and board type
Then Additional Package Details should not be shown on booking summary for <return> days booking
Examples: 
| hotelName        | departure | return | guests      |
| Disneyland Hotel | 180       | 4      | 2,0,0:2,0,0 |
| Disneyland Hotel | 180       | 16     | 2,0,0       |