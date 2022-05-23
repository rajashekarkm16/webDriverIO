Feature: SortBy

    Background:
        Given Hotel search result page is open for destination 'Palm Jumeirah'

    @C2927480 @C2927483
    Scenario: view of sort option
    When I open sort options
    Then Sort by 'RECOMMENDED' should be checked
    And  Sort modal should display all sort options
    And Close sort modal Icon should be displayed 

    @C2935340
    Scenario Outline: Verify sort criteria added to query
        When I open sort options
        And I select <option> sort by option
        Then Selected sort by <selectedSortOption> should be displayed
        And Sort criteria <queryOption> added to query url
        Examples:
            | option                        | selectedSortOption | queryOption                   |
            | PRICE_LOWEST_FIRST            | Price              | PRICE_LOWEST_FIRST            |
            | CUSTOMER_RATING_HIGHEST_FIRST | Customer Rating    | CUSTOMER_RATING_HIGHEST_FIRST |
            | STAR_RATING_HIGHEST_FIRST     | Star Rating        | STAR_RATING_HIGHEST_FIRST     |

    #Perform search for destination which has less hotels in it
    @C2927484 @C2933868 @C2933869 @C2933870
    Scenario Outline: Verify sort in hotel search results
        When I open sort options
        And I select <option> sort by option
        Then Selected sort by <selectedSortOption> should be displayed
        And Search results are sorted based on the selected <selectedSortOption>
        Examples:
            | option                        | selectedSortOption |
            | PRICE_LOWEST_FIRST            | Price              |
            | CUSTOMER_RATING_HIGHEST_FIRST | Customer Rating    |
            | STAR_RATING_HIGHEST_FIRST     | Star Rating        |










