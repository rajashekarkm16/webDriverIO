Feature: Overview tab 
    As an website customer
    Would like to check hotel card has the relavant information

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

@C2925936 @C2926547 @2926019 @skip
    Scenario: Section in Overview Tab
        Given I am on hotel Estab page for Estab <Estab>
        Then Overview Tab should be selected and displayed
        And Rooms,Reviews tab is displayed and not selected
        And Hotel information ,Facilities, Reviews are displayed
     
       
@C2925941 @skip
Scenario: Hotel with no reviews
    Given I am on hotel Estab page for Estab <Estab>
    Then No reviews message <Message> should be displayed

    Examples:
        | Estab    | Message                                            |
        | Le Mathurin Hotel | We are still waiting for our first customer review |

@2925941 @skip
Scenario: See All Customer Reviews not displayed for 0-2 results
    Given I am on hotel Estab page for Estab <Estab>
    Then See All Customer Reviews should not be displayed

    Examples:
        | Estab    |  ReviewsCount |
        | Le Mathurin Hotel | 0 |
        | Hipark Paris La Villette | 1|
        | Ibis Paris Berthier Hotel | 2|

@2926014 @2926674 @skip
Scenario: Show whole review or show less on overviews tab
    Given I am on hotel Estab page for Estab <Estab>
    Then I want to see faded content 
    When I click on Show whole review
    Then show less link should be displayed
    When I click on Show less
    Then Show whole review link should be displayed
    And I want to see faded content 

    Examples:
        | Estab   |
        | Pullman Paris Tour Eiffel |





        



