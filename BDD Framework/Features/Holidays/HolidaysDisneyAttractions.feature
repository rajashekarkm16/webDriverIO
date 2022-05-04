@holidayregression @v3regression @end2end 
Feature: HolidaysDisneyAttractions
	In order to make holiday booking with Additinal Mandatory extras
	As a end user
	I want to choose a hotel and flight of my choice

Background: 
Given Click on Accept and close cookies button


Scenario Outline: Verify Disney Attractions tickets are shown on holiday booking summary
Given I perform a holiday search to <hotelName> from <departure_airport> for <guests> during <departure> and <return> dates
#When Select the first hotel from the results
When Select random rooms and board type
And Select random flight
And I continue to book 
Then Additional Package Details should be shown on booking summary for <return> days booking
When I continue to Guest details
And I fill holiday guests details
And Add phone number
And Add email id
And Proceed to choose payment page
#Then AdditionalPackageDetails should be shown on booking summary for <return> days booking
And Complete the full payment holiday booking using VisaCredit payment from Payment Details
Then Booking references of booked items are available
And Validate that the <return> mandatory extras are shown on confirmation page
And Validate the <return> mandatory disney ticket details are saved
Examples: 
| hotelName        | departure | return | guests      | departure_airport |
| Disneyland Hotel | 190       | 7      | 2,0,0:2,0,0 | London            |
| Disneyland Hotel | 190       | 14     | 2,0,0       | London            |



Scenario Outline: Verify Disney Attractions tickets are not shown on holiday booking summary
Given I perform a holiday search to <hotelName> from <departure_airport> for <guests> during <departure> and <return> dates
#When Select the first hotel from the results
When Select random rooms and board type
And Select random flight
And I continue to book
Then Additional Package Details should not be shown on booking summary for <return> days booking
Examples: 
| hotelName        | departure | return | guests      | departure_airport |
| Disneyland Hotel | 190       | 4      | 2,0,0       | London            |
| Disneyland Hotel | 190       | 15     | 2,0,0:2,1,0 | London            |