Feature: Hotel Card
  As an website customer
  Would like to check hotel card has the relavant information

  Background:
    Given I am on the 'Home' page
    When I select 'HOTEL ONLY' tab

  @C2924511 @C2924820 @C2924829 @C2924834
  Scenario Outline: Hotel Details and Mandatory Transfers message on hotel card
    Given I am on hotel search result page for destination '<Destination>'
    Then I should land on the hotel search result page
    And Mandatory transfer message <Message> should be displayed in the hotel card
    And Hotel card should display star ratings, location and the Hotel name for all hotels
    And Average review score should be dsiplayed on hotel card
    And Total number of reviews are displayed
    And Smiley icon is displayed
    Examples:
      | Destination | Message                   |
      | Maldives    | Return transfers to hotel |

  @C2925387
  Scenario Outline: Secure today on Hotel Card
    Given I am on hotels search result page for destination '<Destination>', checkin date as  '<CheckinDate>' duration '<DurationOfDays>' GuestInfo '<GuestsData>'
    Then Hotel card should have secure today pill with message <Message>
    Examples:
      | Destination | GuestsData                                                                               | CheckinDate | DurationOfDays | Message               |
      | Dubai       | [{"Adults": 2,"Child": [2,3]}]                                                           | 55          | 5              | Secure today from £15 |
      | Dubai       | [{"Adults": 1,"Child": [2,3]},{"Adults": 2,"Child": [4,6]},{"Adults": 3,"Child": [7,9]}] | 30          | 3              | Secure today from £45 |

  @C2926483
  Scenario Outline: Display of Total Price and Per person price of the hotel
    Given I am on hotel search result page for destination '<Destination>'
    Then I should land on the hotel search result page
    And Price summary information should be displayed
    And '<PerPersonText>' and per person should be displayed in brackets
    And '<TotalPriceText>' and total price should be displayed
    Examples:
      | Destination | TotalPriceText   | PerPersonText |
      | Dubai       | Holiday price from | Per Person |
  
  @C2926484
  Scenario Outline: Display of Was price and Now Price on Pricing Summary
    Given I am on hotels search result page for destination '<Destination>', checkin date as  '<CheckinDate>' duration '<DurationOfDays>' GuestInfo '<GuestsData>'
    Then I should land on the hotel search result page
    And '<TotalPriceText>' and total price should be displayed
    And Was price and Now Price should be displayed
    Examples:
      | Destination | GuestsData                                                                                     | CheckinDate | DurationOfDays | TotalPriceText |
      | Cyprus      | [{"Adults": 1,"Child": [1,2,3]},{"Adults": 2,"Child": [4,5,6]},{"Adults": 3,"Child": [7,8,9]}] | 62          | 3              | Holiday price from |

  # @TC_2925019 @TC_2925387
  # Scenario: I can see sponsored pill on hotel image
  #   Given I am on hotel search result page for destination <Destination>
  #   Then Sponsored pill <SponsoredPill> should be displayed on hotel image
  #   Then Hotel card should have secure today pill with message <Message>
  #   Examples:
  #     | Destination | SponsoredPill     | Message               |
  #     | Dubai       | Customer Favourite|

  # @TC_2925018
  # Scenario: I can see discount pill on hotel image
  #   Given I am on hotel search result page for destination <Destination>
  #   Then Discount pill should be displayed on hotel image
  #   Examples:
  #     | Destination |
  #     | Dubai       |

  # @TC_2925020
  # Scenario: Sponsored pill should override discount pill
  #   Given I am on hotel search result page for destination <Destination>
  #   Then Sponsored pill should be displayed on hotel image
  #   And Discount pill is not displayed on hotel image
  #   Examples:
  #     | Destination |
  #     | Dubai       |

  # @TC_2925178, @Sunmaster
  # Scenario: No pills for Sunmaster
  #   Given I am on hotel search result page for destination <Destination>
  #   Then Sponsored Pill should not be displayed on hotel image
  #   Examples:
  #     | Destination |
  #     | Dubai       |

# @TC_2924855
# Scenario: I see hotel card has no reviews

#   Given there is no review information for a hotel for the destination <Destination>
#   And a website user search for a hotel <Hotel> with no review <Review>
#   When user lands on the hotel search result page
#   Then text to display; We are still waiting for our first customer review
#   Examples:
#     | Destination | Hotel | Review | Message |
#     | Dubai       | Hotel | False  | We are still waiting for our first customer review |


