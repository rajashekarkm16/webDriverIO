Feature: Skeleton Loading
    As an website customer
    Would like to check Skeleton Loading page

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab


    @C2927479 @desktop @tab
    Scenario: Verify skeleton loading for choose hotel breadcrumb
        Given I am on hotel search result page for destination 'Downtown Dubai' without pageload
        And Search results skeleton loading page should be displayed
        And Filter results skeleton loading page should be displayed
        When I select hotel for index '1'
        And I click 'Choose Hotel' link from the breadcrumb
        Then Filter results skeleton loading page should be displayed
        And Search results skeleton loading page should be displayed


    @C2933910
    Scenario: Verify skeleton loading for Sort by
        Given I am on hotel search result page for destination 'Maldives'
        When I open sort options
        And I select 'PRICE_LOWEST_FIRST' sort by option
        Then Search results skeleton loading page should be displayed
        When I open sort options
        And I select 'STAR_RATING_HIGHEST_FIRST' sort by option
        Then Search results skeleton loading page should be displayed

    @C2934696
    Scenario: Verify skeleton loading for Show more Hotels
        Given I am on hotel search result page for destination 'Maldives'
        When I click show more hotels
        Then Search results skeleton loading page should be displayed