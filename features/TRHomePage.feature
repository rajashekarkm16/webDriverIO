Feature: The TravelrepublicHomepage

    Scenario Outline: As a user, I can Open TR HomePage

        Given I open the browser and navigate to trsite <TRUrl>
        Then I should see a holidaytab with text <TabText>

        Examples:
            | TRUrl                                          | TabText                |
            | https://pp2.travelrepublic.co.uk               | HOLIDAYS               |
            

    



