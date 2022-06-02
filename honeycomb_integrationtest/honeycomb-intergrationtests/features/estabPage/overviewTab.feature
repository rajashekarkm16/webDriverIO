Feature: Overview tab
    As an website customer
    Would like to check hotel card has the relavant information

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    @C2925936 @C2926547 @C2926019 @C2926014
    Scenario: Section in Overview Tab
        Given I am on hotel Estab page for Estab <Estab>
        Then Overview Tab should be selected and displayed
        And Rooms,reviews tab is displayed and not selected
        And Hotel information ,facilities, reviews are displayed
        And Facilities with relavant icons should be displayed
        Examples:
            | Estab                     |
            | Pullman Paris Tour Eiffel |

    @C2925945
    Scenario: Hotel with no reviews
        Given I am on hotel Estab page for Estab <Estab>
        Then No review message <Message> should be displayed
        Then See all customer reviews should not be displayed
        Examples:
            | Estab             | Message                                             |
            | Le Mathurin Hotel | We are still waiting for our first customer review! |

    @C2925941
    Scenario: See All Customer Reviews not displayed for 2 reviews
        Given I am on hotel Estab page for Estab <Estab>
        Then See all customer reviews should not be displayed
        Examples:
            | Estab                     |
            | Ibis Paris Berthier Hotel |
    
    @C2925941
    Scenario: See All Customer Reviews displayed for >2 reviews
        Given I am on hotel Estab page for Estab <Estab>
        Then See all customer reviews should be displayed
        Examples:
            | Estab            |
            | Secret De Paris |

    @C2926674 
    Scenario:Show more or show less Hotel information
        Given I am on hotel Estab page for Estab <Estab>
        And Hotel information is displayed
        Then I want to see faded content
        When I click on show more
        Then Show less link should be displayed
        When I click on Show less
        Then Show more link should be displayed
        And I want to see faded content
        Examples:
            | Estab                     |
            | Pullman Paris Tour Eiffel |

    @C2926665 @2926675 @skip
    Scenario:See all customer reviews
        Given  I am on hotel Estab page for Estab <Estab>
        When I click on see all customer reviews
        Then I will be navigated to the reviews tab
        And Reviews will be displayed along with the date of reviewer, name of reviewer and location of reviewer
            | Estab             |
            | Fairmont The Palm |


    @C2926071 @2926076 @C2927488 @2927491 @skip
    Scenario:Map on the overview tab
        Given I am on hotel Estab page for Estab <Estab>
        When I have click on map
        And I click on pin
        Then Was/now price should be displayed
        And Image ,hotel name,close should be displayed
        When I click on close
        Then map modal should be closed
            | Estab             |
            | Fairmont The Palm |

