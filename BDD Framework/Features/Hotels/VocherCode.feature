@hotelregression @v3regression
Feature: VocherCodeFeature
	In order to verify voucher code feature  
	As a tester
	I want to get voucher code as a modal and verify functionality 

@TC2863236
Scenario Outline: Verify Voucher code details in Booking Form
Given I perform hotel search for <destination> dates 15 and 4 with 2,0,0
When Select random rooms and board type
Then Voucher code details should be displayed

Examples: 
| destination         |
| Flamingo Beach Mate |

@TC2863260 @TC2863262
Scenario Outline: Verify Voucher code Applied and Removed
Given I perform hotel search for <destination> dates 30 and 4 with 2,0,0
When Select random rooms and board type
And Enter and apply voucher code <voucherCode> 
Then validate voucher code success message 
When Remove voucher code
Then Validate voucher code removed success message

Examples: 
| destination         | voucherCode |
| Flamingo Beach Mate | Testvoucher |


@TC2863268
Scenario Outline: Verify Invalid or Expired Voucher code 
Given I perform hotel search for <destination> dates 40 and 4 with 2,0,0
When Select random rooms and board type
And Enter and apply voucher code <voucherCode>
Then Validate invalid voucher code message <voucherCode>

Examples: 
| destination         | voucherCode        |
| Flamingo Beach Mate | InvalidVoucherCode |

@TC2864159
Scenario Outline: Verify Promotion criteria not met 
Given I perform hotel search for <destination> dates 30 and 4 with 2,0,0
When I select room type 1 and board type 2
And Enter and apply voucher code <voucherCode>
Then Validate message for promo criteria not met <voucherCode>

Examples: 
| destination          | voucherCode |
| Apartamentos Aguamar | Testvoucher |

#Departure must be within 10 days from today 
@TC2875576
Scenario Outline: Verify Better Deal Message
Given I perform hotel search for <destination> dates 03 and 03 with 2,0,0
When Select random rooms and board type
And Enter and apply voucher code <voucherCode>
Then Validate better deal message <voucherCode>

Examples: 
| destination         | voucherCode |
| Flamingo Beach Mate | Testvoucher |

#departue date has to be after 02 days from today as per setUp in p&P
@TC2863259 @TC2863784
Scenario Outline: Verify Voucher code Discount and Details in Derwent
Given I perform hotel search for <destination> dates 40 and 4 with 2,0,0
When Select random rooms and board type
And Enter and apply voucher code <voucherCode> 
Then validate voucher code success message 
And Voucher code discount has to updated in booking summary 
When Complete the full payment hotel booking using VisaCredit payment with ThreeDS false authorization 
Then Booking references of booked items are available
And Booked item in databse has voucher code Discount

Examples: 
| destination         | voucherCode |
| Coral California | Testvoucher |










