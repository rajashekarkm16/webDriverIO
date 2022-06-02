Feature: Guest Information

    Background:
        Given I am on Guest information page with tripId '76c24735-32c2-4656-a40d-9b839ebfab06'


    #     @C2934123
    #     Scenario: Verify Guest Information text
    #         Then Guest information heading should be 'Please enter your guest information'
    #         And Guest information text should be displayed

    #     @C2934124
    #     Scenario: Verify valid Lead guest fields
    #         When I click Title
    #         Then I want to see drop down with 'Mr,Ms,Miss,Mrs'
    #         When I select any title
    #         Then Title field should be highlighted and green ticked
    #         When I enter first name àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜçÇßØøÅåÆæ_-
    #         Then First name field should be highlighted and green ticked
    #         When I enter first name abcdefghijklmnopqrstuvwxyz
    #         Then First name field should be highlighted and green ticked
    #         When I enter first name ABCDEFGHIJKLMNOPQRSTUVWXYZ
    #         Then First name field should be highlighted and green ticked
    #         When I enter surname àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜçÇßØøÅåÆæ_-
    #         Then Surname field should be highlighted and green ticked
    #         When I enter surname abcdefghijklmnopqrstuvwxyz
    #         Then Surname field should be highlighted and green ticked
    #         When I enter surname ABCDEFGHIJKLMNOPQRSTUVWXYZ
    #         Then Surname field should be highlighted and green ticked


    #     @C2934125
    #     Scenario: Validate Lead guest error messages
    #         When I click on add my payment details
    #         Then Title error message should be displayed
    #         And  Firstname error message should be displayed
    #         And Surname error message should be displayed
    #         When I enter invalid firstname 123
    #         Then Name invalid error message should be displayed
    #         When I enter invalid firstname 123abc
    #         Then Name invalid error message should be displayed
    #         When I enter invalid firstname Test%$*&
    #         Then Name invalid error message should be displayed
    #         When I enter invalid surname 123
    #         Then Name invalid error message should be displayed
    #         When I enter invalid surname abc123
    #         Then Name invalid error message should be displayed
    #         When I enter invalid surname as John%^$#()
    #         Then Name invalid error message should be displayed

    #     @C2934681
    #     Scenario: UI Billing Contact Details
    #         Then I should see header as 'Billing Contact Details'
    #         And Exclusive offers and discounts displayed


    #     @C2934687 @C2934688 @C2934689
    #     Scenario: Verify valid Billing Contact Details
    #         When I enter phone number 7894561230
    #         Then Phone number field should be highlighted and green ticked
    #         When I enter email test@gmail.com
    #         Then Email field should be highlighted and green ticked
    #         When I select Exclusive offers and discounts
    #         Then check box should be checked to receive the offers and discounts
    #         When I deselect Exclusive offers and discounts
    #         Then check box should be unchecked to receive the offers and discounts

    #     @C2934685 @C2934686
    #     Scenario: Validate Billing Contact Details error messages
    #         When I click on add my payment details
    #         Then Phone number error message should be displayed
    #         And Email error message should be displayed
    #         When I enter invalid phone number 12356789#$%^
    #         Then Phone number invalid error message should be displayed
    #         When I enter invalid phone number 12356789abc
    #         Then Phone number invalid error message should be displayed
    #         When I enter invalid email abc@com
    #         Then Email invalid error message should be displayed
    #         When I enter invalid email abc.com
    #         Then Email invalid error message should be displayed
    #         When I enter invalid email abc.@co
    #         Then Email invalid error message should be displayed
    #         When I enter invalid email abc1234
    #         Then Email invalid error message should be displayed
    #         When I enter invalid email abc1234#$%#$%@gmail.com
    #         Then Email invalid error message should be displayed


    @C2934664 @C2934678 @C2934668 @C2934671 @C2934677
    Scenario: Verify Choose Payment options
        Then Choose a payment header should be <Choose a payment method>
        And Payment options should be displayed
        And 'card' payment option should be selected
        Then Payment terms and conditions should not be displayed
        When I choose payment option as 'paypal'
        Then 'paypal' payment option should be selected
        Then Payment terms and conditions should be displayed
        When I choose payment option as 'apple'
        Then 'apple' payment option should be selected
        Then Payment terms and conditions should be displayed
        When I choose payment option as 'gpay'
        Then 'gpay' payment option should be selected
        Then Payment terms and conditions should be displayed

#     @C2934668
#     Scenario: PayPal
#         When I choose PayPal
#         Then PayPal should be selected with tickmark
#         And Confirmation note should be displayed


#     @C2934671
#     Scenario: Apple Pay
#         When I choose Apple Pay
#         Then Apple Pay should be selected with tickmark
#         And Confirmation note should be displayed

#     @C2934677
#     Scenario: Google Pay
#         When I choose Google Pay
#         Then Google Pay should be selected with tickmark
#         And Confirmation note should be displayed

# @C2934673
# Scenario Outline: Validate Email Me Error Messages
#     When I enter email <emailId>
#     And I click on Send EmailMe
#     Then EmailMe invalid error message should be displayed
#     Examples:
#         | emailId                 |
#         | abc                     |
#         | abc!#.com               |
#         | abc.com                 |
#         #| abc1234#$%#$%@gmail.com |
#         | abc@com                 |
#         | abc   @test.com         |

# @C2934675
# Scenario Outline:Verify Send Email Me
#     When I select checkbox of Send me the best deals and holiday inspiration
#     Then I should see send me best deals checked
#     When I enter email <emailId>
#     And I click on Send EmailMe
#     Then EmailMe this hotel success message should be displayed <emailId>
#     Examples:
#         | emailId        |
#         | john@gmail.com |

#     @C2934672
#     Scenario: Verify Hotel Information text displayed
#         Then I should see header as 'Hotel Information'
#         And Hotel information text should be displayed
#         And Hyper link should be displayed as 'Read hotel information in full'
#         When I click Read hotel information in full
#         Then Hotel Information page should be displayed
#         When I click on cross symbol
#         Then I should see Guest information page

#     @C2934662
#     Scenario: Add special request
#         Then I should see header as 'Special Requests'
#         And Special request description should be displayed
#         When I click add special request icon
#         Then I should see special request text box
#         When I enter special request
#         Then Special request text box should be highlighted in green color and ticked
#         And I should see display of remove special request icon
#         And I should be able to drag the Special request text box
#         When I clear special request
#         Then Special request text box should not be highlighted in green color and ticked
#         When I click remove special request icon
#         Then I shouldnot see Special request textbox
#         And Special request description should be displayed

