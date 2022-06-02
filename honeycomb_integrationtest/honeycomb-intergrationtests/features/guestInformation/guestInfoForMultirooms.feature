Feature: Guest Information when multiroom selected

    Background:
        Given I am on Guest information page with multiple rooms selection

    @C2934127
    Scenario: Validate Lead Guests for multiroom selection
        Then I should see lead guest section for individual rooms
        

    @C2935290
    Scenario: Validate Special request for multiroom selection
        When I click add special request icon
        Then Special request option should be present for each room
        When I enter special request for all the rooms
        Then Special request text box should be highlighted in green and ticked for all the rooms

