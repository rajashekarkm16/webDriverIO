Feature: Pagination


    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    @C2933884
    Scenario: Verify pagination with less than default results count
        Given I am on hotel search result page for destination 'Downtown Dubai'
        When I apply filters until the results count should be less than or equals to default count
        Then Show more hotels button should not be displayed

    @C2934718
    Scenario: Verify show more hotels pagination functionality
        Given I am on hotel search result page for destination 'Downtown Dubai'
        Then Validate hotel pagination
        And Show more hotels button should not be displayed

    @C2934729
    Scenario: Verify show more hotels
        Given I am on hotel search result page for destination 'Dubai'
        Then Hotel search result count matches the default count
        When I click show more hotels button
        Then Search results count is incremented