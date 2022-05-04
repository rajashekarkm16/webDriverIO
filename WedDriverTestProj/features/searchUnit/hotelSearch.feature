Feature: Hotel Search
    As a user, I will book a hotel based on destination

    Background: As a user , I am on Hotel Only Tab page
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    Scenario Outline: To Verify Hotel search information on Hotel search result page
        When I select destination as '<Destination>'
        When I select a checkin date which is '<CheckinDate>' days ahead from now and checkout '<TotalDays>' days later
        When I add room and guest information with data '<GuestInfo>'
        When I click search button
        Examples:
            | Destination | CheckinDate | TotalDays | GuestInfo     |
            | Dubai       | 62          | 2         | [{"Adult":2}] |