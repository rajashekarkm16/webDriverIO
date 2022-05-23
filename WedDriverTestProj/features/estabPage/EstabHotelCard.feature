Feature: EstabHotelCard
    As an website customer
    Would like to check hotel card has the relavant information

    Background:
        Given I am on the 'Home' page
        When I select 'HOTEL ONLY' tab

    @C2925934
    Scenario Outline: Total Price and Per Person Price
        Given I am on hotel Estab page for Estab <Estab>
        Then Per person and total price text should be displayed
        And Per person price should be less then totalprice
        Examples:
            | Estab                    |
            | FIVE Palm Jumeirah Dubai |


    @C2925935
    Scenario: Was Price and Now Price
        Given I am on hotel Estab page for Estab <Estab>
        Then Was and Now Price should be displayed
        #And Was price should be stricked off
        And Was price should be greater than now price

        Examples:
            | Estab           |
            | Bahia Del Duque |

    #         @C2925021 @C2925022
    # Scenario: Verify the display of sponsored and discount pill
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then  Hotel information is returned
    #     And   should display sponsored and discount pill
    #     Examples:
    #         | Estab    | Message                   |
    #         | Maldives | Return Transfers to Hotel |

    #  @C2925940
    # Scenario: Favorite pill on Green color and White text
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then 'Customer Favourite' text should be displayed on the card
    #     And  Customer Favourite pill on the card should be displayed green color and white text

    #     Examples:
    #         | Estab    | Message                   |
    #         | Maldives | Return Transfers to Hotel |

    # @C2924534 @C2924823 @C2924854
    # Scenario: Hotel Name / Location / Star Rating and mandatory Transfers message on hotel card
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then Hotel name<Estab>, star ratings, <Location> and <Province> should be displayed
    #     And Hotel average review Score and total number of reviews should be displayed
    #     And Mandatory transfer message <Message> should be displayed in the estab hotel card
    #     Examples:
    #         | Estab               | Message                   | Location      | Province |
    #         | Reethi Beach Resort | Return transfers to hotel | Fonimagoodhoo | Maldives |

    # @C2924817 @C2924810 @C2924807
    # Scenario: Hotel image and Gallery
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then Hotel image should be displayed
    #     And Hotel should have multiple thumbnail images
    #     And Clicking on each image should display corresponding image
    #     When I click on the full screen button
    #     Then I should be able to scroll through the images
    #     When I click on the close button
    #     Then I should exit fullscreen

    #     Examples:
    #         | Estab                    |
    #         | Innahura Maldives Resort |

    # @C2924837
    # Scenario: Hotel with no reviews
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then  No reviews message <Message> should be displayed

    #     Examples:
    #         | Estab                    | Message                                             |
    #         | Innahura Maldives Resort | We are still waiting for our first customer review! |

    # @C2926497
    # Scenario: Image has  a 404 error
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then Hotel Card should be displayed with No Image icon

    #     Examples:
    #         | Estab          |
    #         | Laguna Park II |

    # @C2925937
    # Scenario: Verify board type, Summary, key information
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then Price includes text, Party overview, Board type, Cancellation Information, Local charges should be displayed

    #     Examples:
    #         | Estab               |
    #         | JA Ocean View Hotel |

    # @C2926496
    # Scenario: Hotel has a special offer available
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then Offer information should be displayed

    #     Examples:
    #         | Estab               |
    #         | JA Ocean View Hotel |

    # @C2926497
    # Scenario: Hotel does not have a special offer available
    #     Given I am on hotel Estab page for Estab <Estab>
    #     Then Offer information should not be displayed

    #     Examples:
    #         | Estab               |
    #         | Reethi Beach Resort |
