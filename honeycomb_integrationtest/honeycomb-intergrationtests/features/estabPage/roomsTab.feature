Feature: Rooms tab
        As an website customer
        Would like to check hotel card has the relavant information

        Background:
                Given I am on the 'Home' page
                When I select 'HOTEL ONLY' tab

        @C2933872 @C2933876
        Scenario Outline: Display of Room Type,Board Type and Price
                Given I am on hotel estab page for <Estab>
                When I click on the 'ROOMS' tab
                Then Room types and board types will display
                # And Total price should be displayed for each board
                # And Cheapest room is selected by default
                # And the radio button should be displayed as ticked
                # And room selected should be highlighted

                Examples:
                        | Estab                    |
                        | FIVE Palm Jumeirah Dubai |


        @C2933908 @skip
        Scenario Outline: Special Offer details to display against board type
                Given I am on hotel estab page for <Estab>
                When I clicks on the rooms tab
                Then Special offer information will display against the board type it is available for in red text

                Examples:
                        | Estab                |
                        | Grange Holborn Hotel |

        @C2933877 @skip
        Scenario Outline: Display of Was price,Now price and delta price
                Given I am on hotel estab page for <Estab>
                When I clicks on the rooms tab
                Then Room types and board types will display
                And Was price and now price should be displayed against each board
                And Was price should be displayed with a strike through
                And Delta price should be displayed
                And Difference in cost will be displayed between the cheapest room and the alternative board types available

                Examples:
                        | Estab                    |
                        | FIVE Palm Jumeirah Dubai |

        @C2933909 @skip
        Scenario Outline: Show room content against room types
                Given I am on hotel estab page for <Estab>
                When I clicks on the rooms tab
                Then Room type should have room descriptions

                Examples:
                        | Estab                    |
                        | FIVE Palm Jumeirah Dubai |


        @C2933882 @skip
        Scenario Outline: Secure Today pill to display against board type for Flexible, Refundable rooms only
                Given I am on hotel estab page for <Estab>
                When I clicks on the rooms tab
                Then Secure today pill should be displayed against the board types for Flexible, Refundable rooms only
                And Do not show secure today pill against non refundable single room
                Examples:
                        | Estab                    |
                        | FIVE Palm Jumeirah Dubai |

        @C2933883 @skip
        Scenario Outline: Secure Today Pill will display in the summary panel and NOT show against the board type for multi room
                Given I am on hotel Estab page for <Estab>
                When I click on the rooms tab
                Then Secure today pill should not be displayed for multi room

                Examples:
                        | Estab                    |
                        | FIVE Palm Jumeirah Dubai |

        @C2933911 @skip
        Scenario Outline: Selecting to Show More against the description, additional information and Show Less will be displayed
                Given I am on hotel Estab page for <Estab>
                When I click on the rooms tab
                And Description exceeds the limit
                Then Show more link to be displayed on the page
                When Show more link has been selected
                Then Show all the content available
                And Show less link to be displayed at the end of the text

                Examples:
                        | Estab                    |
                        | FIVE Palm Jumeirah Dubai |





