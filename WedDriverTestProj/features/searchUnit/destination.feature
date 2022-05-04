Feature:  Destination

        Background:
            Given I am on the 'Home' page
             When I select 'HOTEL ONLY' tab

        @C2924568 @Web @Mobile @tab
        Scenario: Static Text and Icons for Destination
             Then Destination placeholder should be 'Add a destination'
             When I open destination autocompleter
             Then Destination autocompleter header should be 'Destination or Hotel'
              And  Destination autocompleter category header should be 'Popular destinations'
              And Destination autocompleter category header should have map icon
              And Destinations in autocompleter should show hotel count
             When I close destination autocompleter
             Then Autocompleter should be closed

        @C2924578 @Web @Mobile @tab
        Scenario:  Blank Destination validation
             When I click search button
             Then Destination placeholder should be 'Please add a destination'
              And  Placeholder,label and destination section should be in red color

     #    @C2924576 @40
     #    Scenario Outline:  should select destination from autocompleter using Keyborad
     #         When I enter destination '<SearchInput>'
     #         When I select '<SearchResult>' from destination autocompleter using keyboard
     #         Then Destination should display '<SearchResult>'
     #    Examples:
     #              | SearchInput    | SearchResult                   |
     #              | London Central | Hyde Park Executive Apartments |

        @C2924569 @C2924570 @C2925017 @Web @Mobile @tab
        Scenario Outline: Destination Autocompleter Functionality
             When I enter destination '<InvalidDestination>'
             Then Destination autocompleter should display no matches found
             When I enter destination '<Country>'
             Then Destination autocompleter should display '<State>'
             When I enter destination '<State>'
             Then Destination autocompleter should display '<City>'
             When I enter destination '<City>'
             Then Destination autocompleter should display '<HotelName>'
              And  Destination autocompleter category header should be 'Destinations'
              And  Destination autocompleter subcategory header should be 'Hotels'
              And  Destination autocompleter subcategory header should have hotel icon
             When I select '<HotelName>' from destination autocompleter search result
             Then Focus should be in 'Dates' field
              And Destination should display '<HotelName>'
        Examples:
                  | Country        | State  | City           | HotelName                      | InvalidDestination |
                  | United Kingdom | London | London Central | Hyde Park Executive Apartments | ZZZQQQ             |

        @C2925110 @Web @Mobile @tab
        Scenario:  verify Hotel and Destination section for 3 letter code
             When I enter destination 'DXB'
             Then Destination autocompleter category header should be 'Destinations'
              And Destination autocompleter category header should have map icon
              And Destination autocompleter subcategory header should be 'Hotels'
              And Destination autocompleter subcategory header should have hotel icon