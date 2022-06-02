Feature: Guests

        Background:
            Given I am on the 'Home' page
             When I select 'HOTEL ONLY' tab
              And I open Rooms & guests modal

        @C2925111 @C2924558 @Web @Mobile @tab
        Scenario Outline:  Static text for Rooms & guests modal
             Then  Room information should be '<RoomsInformation>'
              And  Adults information should be '<AdultsInformation>'
              And  Children information should be '<ChildrenInformation>'
              And  Help message should be '<HelpMessage>'
             When I close Rooms & guests modal
             Then Rooms & guests modal should be closed
        Examples:
                  | RoomsInformation | AdultsInformation | ChildrenInformation | HelpMessage                                                                              |
                  | Up to 3          | 18+ years         | 0-17 years          | Need more than 3 rooms? Travelling with a group of 12 or more? Please call 0208 972 8583 |


        @C2924808 @C2924809 @Web @Mobile @tab
        Scenario: Rooms selection
             Then Guests should display '2 Adults, 1 Room'
              And Room '1' should be selected
              And Adults count should display '2' for Room '1'
              And Children count should display '0' for Room '1'
             When I select room '2'
             Then Room '2' should be selected
              And Guests should display '4 Adults, 2 Rooms'
              And Adults count should display '2' for Room '2'
              And Children count should display '0' for Room '2'
             When I select room '3'
             Then Room '3' should be selected
              And Guests should display '6 Adults, 3 Rooms'
              And Adults count should display '2' for Room '3'
              And Children count should display '0' for Room '3'
              And Done button should be enabled

        @C2924813 @Web @Mobile @tab
        Scenario: Rooms selection in descending order
             When I select room '3'
             Then Room '3' should be selected
              And Guests should display '6 Adults, 3 Rooms'
              And Adults count should display '2' for Room '3'
              And Children count should display '0' for Room '3'
             When I select room '2'
             Then Room '2' should be selected
              And Guests should display '4 Adults, 2 Rooms'
              And Adults count should display '2' for Room '2'
              And Children count should display '0' for Room '2'
             When I select room '1'
             Then Room '1' should be selected
              And Guests should display '2 Adults, 1 Room'
              And Adults count should display '2' for Room '1'
              And Children count should display '0' for Room '1'

        @C2924818 @Web @Mobile @tab
        Scenario: Verify Rooms selection
             Then Room '1' should be in 'highlighted' color
              And Room '2' should be in 'grey' color
              And Room '3' should be in 'grey' color
             When I select room '2'
             Then Room '2' should be in 'highlighted' color
              And Room '1' should be in 'grey' color
              And Room '3' should be in 'grey' color
             When I select room '3'
             Then Room '3' should be in 'highlighted' color
              And Room '1' should be in 'grey' color
              And Room '2' should be in 'grey' color


        @C2924826 @C2924832 @C2924831 @Web @Mobile @tab
        Scenario: Max and Min Adults selection
             When I select '1' adult for room '1'
             Then Adults count should display '1' for Room '1'
              And Guests should display '1 Adult, 1 Room'
              And Adults decrease icon should be disabled for Room '1'
             When I select '12' adult for room '1'
             Then Adults count should display '12' for Room '1'
              And Guests should display '12 Adults, 1 Room'
              And Adults increase icon should be disabled for Room '1'
              And Adults decrease icon should be enabled for Room '1'
             When I select '11' adult for room '1'
             Then Adults count should display '11' for Room '1'
              And Guests should display '11 Adults, 1 Room'
              And Adults increase icon should be enabled for Room '1'

        @C2924857 @C2924985 @C2924990 @C2925008 @Web @Mobile @tab
        Scenario: Max and Min Children selection
              And Children decrease icon should be disabled for Room '1'
             When I select '12' children for room '1'
             Then Children count should display '12' for Room '1'
              And Guests should display '2 Adults, 12 Children, 1 Room'
              And Children increase icon should be disabled for Room '1'
             When I select '11' children for room '1'
             Then Children count should display '11' for Room '1'
              And Guests should display '2 Adults, 11 Children, 1 Room'
              And Done button should be disabled
             When I select '0' children for room '1'
             Then Child age box should not displayed for Room '1'

        @C2925170 @Web @Mobile @tab
        Scenario: Validation Message check when Child Age is not provided
             When I select '1' children for room '1'
             Then Children count should display '1' for Room '1'
             When I close Rooms & guests modal
              And I click search button
              And I open Rooms & guests modal
             Then Age validation message should be 'Please enter ages of all children'
              And Age validation message should be in red color
              And Child '1' age box outline should be in red color for Room '1'
              And  Placeholder,label and Guests section should be in red color

        @C2924858 @C2924937 @C2924992 @C2925009 @Web @Mobile @tab
        Scenario Outline: Child Age Selection
             When I add room and guest information with data '<GuestsData>'
              And I open Rooms & guests modal
              And I update child age '17' for child number '1' in Room '1'
             Then Child Age should be '17' for child number '1' in Room '1'
             When I update child age '16' for child number '2' in Room '2'
             Then Child Age should be '16' for child number '2' in Room '2'
             When I update child age '0' for child number '3' in Room '3'
             Then Child Age should be '0' for child number '3' in Room '3'

        Examples:
                  | GuestsData                                                                                     |
                  | [{"Adults": 1,"Child": [1,2,3]},{"Adults": 2,"Child": [4,5,6]},{"Adults": 3,"Child": [7,8,9]}] |

        @Web @Mobile @tab
        Scenario Outline: Focus test for unfilled selection
             When I close Rooms & guests modal
              And I click search button
             Then Focus should be in 'Destination' field
             When I select destination as '<Destination>'
              And I click search button
             Then Focus should be in 'Dates' field
             When I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
              And I add room and guest information with data '<GuestsData>'
             Then Focus should be in 'Search' field

        Examples:
                  | GuestsData      | Destination | UpdatedGuestsData | CheckinDate | TotalDays |
                  | [{"Adults": 1}] | London      | [{"Adults": 2}]   | 20          | 3         |

