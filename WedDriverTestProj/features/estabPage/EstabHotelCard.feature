Feature: Estab Hotel Card
    As an website customer
    Would like to check hotel card has the relavant information

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    @C2924534
    Scenario: Mandatory Transfers message on hotel card
        Given I am on hotel Estab page for Estab <Estab>
        And Mandatory transfer message <Message> should be displayed in the hotel card
        Examples:
            | Estab                    | Message                   |
            | Innahura Maldives Resort | Return Transfers to Hotel |

@C2924817 @C2924810 @C2924807
Scenario: Hotel image and Gallery
    Given I am on hotel Estab page for Estab <Estab>
    Then Hotel image should be displayed
    And Hotel should have multiple thumbnail images
    When User clicks on each image
    Then Corresponding image should be displayed
    When User click on the full screen button
    Then Should be an option to naviagte through the images
    And User should be able to scroll through the images
    When User clicks on the close button
    Then User should exit fullscreen

    Examples:
        | Estab    |
        | Maldives |

@C2924823 @C2924854
Scenario: Hotel Name / Location / Star Rating on the Estab page when search by hotel
    Given I am on hotel Estab page for Estab <Estab>
    Then  Hotel name, star ratings, location and province should be displayed
    And Hotel average review Score and total number of  reviews should be displayed
    Examples:
        | Estab    |
        | Maldives |

@C2924837
Scenario: Hotel with no reviews
    Given I am on hotel Estab page for Estab <Estab>
    Then  No reviews message <Message> should be displayed

    Examples:
        | Estab    | Message                                            |
        | Maldives | We are still waiting for our first customer review |

#         @C2925021 @C2925022
# Scenario: Verify the display of sponsored and discount pill
#     Given I am on hotel Estab page for Estab <Estab>
#     Then  Hotel information is returned
#     And   should display sponsored and discount pill
#     Examples:
#         | Estab    | Message                   |
#         | Maldives | Return Transfers to Hotel |

@C2925934
Scenario: Total Price and Per person price
    Given I am on hotel Estab page for Estab <Estab>
    Then Per person and total price should be displayed
    And Pricing summary information should be displayed

    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |

@C2925935
Scenario: Was price and Now price
    Given I am on hotel Estab page for Estab <Estab>
    Then 'Total Price From' text should be displayed
    And Was price alongside Now Price should be displayed
    And Per person price should be displayed
    And 'Per person' text should be displayed
    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |

@C2925937
Scenario: Verify board type, Summary, key information
    Given I am on hotel Estab page for Estab <Estab>
    And Price includes text should be displayed
    And Party overview should be displayed
    And Board type should be displayed
    And Cancellation Information Should be displayed
    And Local charges Should be displayed
    And Special Offe Should be displayed

    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |

 @C2926496
Scenario: Hotel has a special offer available
    Given I am on hotel Estab page for Estab <Estab>
    When Hotel card is generated
    Then Offer information should be displayed

    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |

 @C2926497
Scenario: Hotel does not have a special offer available
    Given I am on hotel Estab page for Estab <Estab>
    When Hotel card is generated
    Then Offer information should not be displayed

    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |


 @C2925940
Scenario: Favorite pill on Green color and White text
    Given I am on hotel Estab page for Estab <Estab>
    Then 'Customer Favourite' text should be displayed on the card
    And  Customer Favourite pill on the card should be displayed green color and white text

    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |

@C2926497
Scenario: Image has  a 404 error
    Given I am on hotel Estab page for Estab <Estab>
    Then Hotel information should be displayed
    And Hotel Card should be displayed with No Image icon

    Examples:
        | Estab    | Message                   |
        | Maldives | Return Transfers to Hotel |











