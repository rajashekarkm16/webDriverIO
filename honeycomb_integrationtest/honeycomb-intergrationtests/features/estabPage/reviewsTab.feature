Feature: Reviews tab

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    @C2926020
    Scenario Outline: Display of reviews under Reviews tab for hotel which has more than 10 reviews
        Given I am on hotel Estab page for Estab <Estab>
        When I click on the reviews tab
        Then Default reviews should be displayed
        And  Show more reviews button should be available
        When I click on show more reviews button
        Then Next set of reviews should be displayed underneath the previous reviews
        And Show more reviews button should be displayed

        Examples:
            | Estab                |
            | Apartamentos Aguamar |

    @C2926027
    Scenario Outline: Display of Review details and not displaying show more reviews button for hotel which has less than 5 reviews
        Given I am on hotel Estab page for Estab <Estab>
        When I click on the reviews tab
        Then Name, location and check in date of the customer should be displayed
        And  Show more review button should not be displayed
        Examples:
            | Estab               |
            | Reethi Beach Resort |


    @C2926073 @C2926080
    Scenario: Display of Review content, icon and score for Hotel has more than 1 review
        Given I am on hotel Estab page for Estab <Estab>
        When I click on the reviews tab
        Then Review information should be displayed
        And Average review score should be displayed along with the review content
        And Relevant icon should be displayed
        When I click on show whole review link
        Then Review content should expand with show less link
        When I click on show less link
        Then Review content should collapse with show whole review link

        Examples:
            | Estab               |
            | JA Ocean View Hotel |


    @C2927554
    Scenario: Display of text when hotel does not have any reviews
        Given I am on hotel Estab page for Estab <Estab>
        When I click on the reviews tab
        Then show grey box with the <message>

        Examples:
            | Estab                    | message                                             |
            | Innahura Maldives Resort | We are still waiting for our first customer review! |