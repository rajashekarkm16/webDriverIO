Feature: WaitDemo

    Scenario Outline: As a user, I can use Waits

        Given I open the browser and navigate to practiceSite <url>
        Then I should see a PageHeading with text <TabText>
        And  validating different Waits

        Examples:
            | url                                           | TabText                |
            | http://www.seleniumframework.com/Practiceform | Practice Form Controls |