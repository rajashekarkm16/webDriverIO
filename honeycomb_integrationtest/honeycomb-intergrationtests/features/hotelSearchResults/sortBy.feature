Feature: Sort By

    Background:
        Given Hotel search result page is open for destination 'Palm Jumeirah'

    @C2927480 @C2927483
    Scenario: View sort options
        When I open sort options
        Then Sort by 'RECOMMENDED' should be checked
        And  Sort modal should display all sort options
        And Close sort modal Icon should be displayed

    #Perform search for destination which has less hotels in it
    @C2935340 @C2927484 @C2933868 @C2933869 @C2933870
    Scenario Outline: Verify sort criteria added to query
        When I open sort options
        And I select <sortOption> sort by option
        Then Selected sort by <sortOptionText> should be displayed
        And Sort criteria <sortOption> should be added to query url
        And Search results are sorted based on the selected <sortOptionText>
        Examples:
            | sortOption                    | sortOptionText  |
            | PRICE_LOWEST_FIRST            | Price           |
            | CUSTOMER_RATING_HIGHEST_FIRST | Customer Rating |
            | STAR_RATING_HIGHEST_FIRST     | Star Rating     |

    Scenario: Verify Recommended sort 
    When I open sort options
    Then Capture the list of hotel names 
    And I select 'PRICE_LOWEST_FIRST' sort by option
    When I open sort options
    And I select 'RECOMMENDED_HIGHEST_FIRST' sort by option
    Then Captured hotel names should match
  










