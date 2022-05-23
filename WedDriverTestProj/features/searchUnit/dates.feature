Feature: Dates

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    @C2924824
    Scenario: Disable past dates
        When I open calender modal
        Then Days before today should be disabled

    @C2924520 @C2924840
    Scenario:  Static text on Date field and calender modal
        When I open calender modal
        Then Header should be 'Dates'
        And Date placeholder should be 'Add dates'
        And Date modal header should be 'Travel Dates'
        And Close date modal icon should be displayed
        And Current and next month name and year text should be displayed
        And Current day should be highlighted
        And Footer message should display 'Select a check-in date'
        When I select a checkin date '10' days ahead from now
        And Footer message should display 'Select a check-out date'
        When I select a checkout date '5' days later
        Then Footer message should display selected checkin date and checkout date
        And Footer message should display number of nights

    @C2924525 @C2924532
    Scenario Outline:Verify Reset Dates
        When I open calender modal
        Then Reset link should not be displayed
        And I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
        Then Reset link should be displayed
        When I click on reset
        Then Date placeholder should be 'Add dates'
        And Footer message should display 'Select a check-in date'

        Examples:
            | CheckinDate | TotalDays |
            | 10          | 5         |

    @C2924532 @C2924535 @C2924827
    Scenario Outline: Verify Done functionality
        When I open calender modal
        Then Done button should not be enabled
        And I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
        Then Done button should be displayed and enabled
        When I click on done
        Then Selected checkin date and checkout date should be displayed in date field

        Examples:
            | CheckinDate | TotalDays |
            | 10          | 5         |

    @C2924836
    Scenario: Date Periods of the Calendar
        When I open calender modal
        Then calendar should display 36 months from current month

    @C2924856 @Desktop
    Scenario: Previous and Next month controls
        When I open calender modal
        Then Previous icon should be disabled
        And  Next icon should be enabled
        When I click on next icon
        Then I should be able to see the next month
        And  Previous icon should be enabled
        When I click on previous icon
        Then It should navigate to previous month
        And Previous icon should be disabled

    @C2924838
    Scenario: Verify Close Icon functionality
        When I open calender modal
        And Date placeholder should be 'Add dates'
        And  I click on cross icon
        Then Calendar modal should be closed
        And I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
        And I click on cross icon
        Then Selected checkin date and checkout date should be displayed in date field

        Examples:
            | CheckinDate | TotalDays |
            | 10          | 5         |

    @C2933736
    Scenario Outline: Update Dates
        When I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
        Then Selected checkin date and checkout date should be displayed in date field
        And I select a checkin date which is '<UpdateCheckiDate>' days ahead from now and checkout '<UpdateCheckOutDate>' days later
        Then Selected checkin date and checkout date should be displayed in date field
        Examples:
            | CheckinDate | TotalDays | UpdateCheckiDate | UpdateCheckOutDate |
            | 10          | 5         | 15               | 7                  |

    @C2927487
    Scenario Outline: Date field error message
        When I select destination as '<Destination>'
        And I close calendar modal on mobile
        And I click search button
        Then Calendar modal should be opened
        When I close calendar modal on mobile
        And Date placeholder should be 'Please add dates'
        When I select a checkin date '10' days ahead from now
        And I close calendar modal on mobile
        And I click search button
        Then Footer message should display 'Please add a check-out date for your trip'
        And Calendar modal should be opened
        And Checkin date should be highlighted in date modal
        When I close calendar modal on mobile
        Then Selected checkin date should be displayed in date field
        When I select a checkout date '5' days later
        Then Footer message should display selected checkin date and checkout date
        And Footer message should display number of nights
        And Done button should be displayed and enabled
        When I click on done
        Then Selected checkin date and checkout date should be displayed in date field

        Examples:
            | Destination |
            | Dubai       |

    Scenario Outline: No limit on the Duration selected
        When I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
        When I click on done
        Then Selected checkin date and checkout date should be displayed in date field
        Examples:
            | CheckinDate | TotalDays |
            | 10          | 300       |

    Scenario: Validate manual enter date     
     Then Should not allow to enter date manually 






