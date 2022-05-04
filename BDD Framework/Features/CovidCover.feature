Feature: CovidCover
	In order to verify Covid Cover feature  
	As a tester
	I want to get Covid Cover as a modal and verify functionality 

@UKOnly
Scenario Outline:Verify Covid Cover In Booking Summary and Database
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
When Select a random hotel from the results
And Select random rooms and board type
And Confirm the pre selected flight
And Continuetobook without any Extras
Then Covid cover promotion box should be displayed
And Clicking on Covid cover should open dailog modal with content
And Covid cover plus entry should be in booking summary
When Complete the full payment holiday booking using VisaCredit payment with ThreeDS false authorization
Then Booking references of booked items are available
And Booked item in database has covid protection cover

Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 30        | 4      | 2,0,0  |

@UKOnly
Scenario Outline:Verify Covid Cover In promotion box 
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then  Covid cover promotion box should be displayed
And Clicking on Covid cover should open dailog modal with content
When Select a random hotel from the results
Then Covid cover promotion box should be displayed
And Clicking on Covid cover should open dailog modal with content

Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 30        | 4      | 2,0,0  |

@UKOnly
Scenario Outline:Verify Covid Cover In Price includes 
Given I perform a holiday search to <destination> from <departure_airport> for <guests> during <departure> and <return> dates
Then  Covid cover should be displayed in Price includes section
And Clicking on Covid cover in Price includes should open dailog modal with content
When Select a random hotel from the results
Then  Covid cover should be displayed in Price includes section
And Clicking on Covid cover in Price includes should open dailog modal with content

Examples: 
| destination | departure_airport | departure | return | guests |
| Dubai       | London            | 30        | 4      | 2,0,0  |









