Feature: Filter

        Background:
            Given Hotel search result page is open for destination 'Jebel Ali'

        @C2925134 @C2925135 @C2925163 @C2925195 @web @mobile @tab
        Scenario: Verify default view of Board Type Holiday Type and Property Amenities
             Then All filter items should be unchecked for 'Board Type'
              And Each Hotel card label should have 'Board Type' listed in the filter
              And All filter items should be unchecked for 'Holiday Types'
              And All filter items should be unchecked for 'Property Amenities'

        @C2925942 @web @mobile @tab
        Scenario: Single filter selection for filter Board Type
             Then I check one by one 'Board Type' filter and verify hotel count and each hotel has selected filter

        @C2933603 @C2934129 @web @mobile @tab
        Scenario: Multi filter selection for filter Board Type
             Then I check multiple 'Board Type' filters and verify hotel count and each hotel has selected filter
              And I uncheck one by one all 'Board Type' filter and verify hotel count and each hotel has selected filter

        @C2925139 @C2925175 @C2925167 @C2925195 @C2925148 @C2925154 @web @mobile @tab
        Scenario: Verify collapse and expand behaviour for filters
             Then Filter 'Board Type' should be expanded
             When I collapse 'Board Type' filter
             Then Filter 'Board Type' should be collapsed
              And Filter item should not be displayed for 'Board Type'
             When I expand 'Board Type' filter
             Then Filter 'Board Type' should be expanded
              And Filter 'Star Rating' should be expanded
             When I collapse 'Star Rating' filter
             Then Filter 'Star Rating' should be collapsed
              And Filter item should not be displayed for 'Star Rating'
             When I expand 'Star Rating' filter
             Then Filter 'Star Rating' should be expanded
              And Filter 'Holiday Types' should be expanded
             When I collapse 'Holiday Types' filter
             Then Filter 'Holiday Types' should be collapsed
              And Filter item should not be displayed for 'Holiday Types'
             When I expand 'Holiday Types' filter
             Then Filter 'Holiday Types' should be expanded
              And Filter 'Property Amenities' should be expanded
             When I collapse 'Property Amenities' filter
             Then Filter 'Property Amenities' should be collapsed
              And Filter item should not be displayed for 'Property Amenities'
             When I expand 'Property Amenities' filter
             Then Filter 'Property Amenities' should be expanded
              And Filter 'Total Price' should be expanded
             When I collapse 'Total Price' filter
             Then Filter 'Total Price' should be collapsed
              And Filter item should not be displayed for 'Total Price'
             When I expand 'Total Price' filter
             Then Filter 'Total Price' should be expanded
              And Filter 'Our Customer Rating' should be expanded
             When I collapse 'Our Customer Rating' filter
             Then Filter 'Our Customer Rating' should be collapsed
              And Filter item should not be displayed for 'Our Customer Rating'
             When I expand 'Our Customer Rating' filter
             Then Filter 'Our Customer Rating' should be expanded

        @C2934128 @C2934130 @C2934132 @C2934136 @web @mobile @tab
        Scenario: Verify Filter Item State after Browser refresh
             When I check first filter for 'Board Type'
              And I refresh the page
             Then first filter should be checked for 'Board Type'
             When I select lowest star rating filter
              And I refresh the page
             Then lowest star rating filter should be selected
             When I check first filter for 'Holiday Types'
              And I refresh the page
             Then first filter should be checked for 'Holiday Types'
             When I check first filter for 'Property Amenities'
              And I refresh the page
             Then first filter should be checked for 'Property Amenities'

        @C2925171 @web @mobile @tab
        Scenario: Verify default view of Star Rating
             Then All star should be unselected
              And Each Hotel card label should have 'Star Rating' listed in the filter

        @C2925174 @web @mobile @tab
        Scenario: Single Star selection for filter Star Rating
             Then I select one by one star rating filter and verify hotel count and each hotel card has selected star rating

        @C2925943 @C2934131 @web @mobile @tab
        Scenario: Multi filter selection for filter Star Rating
             Then I select multiple star rating filter and verify hotel count and each hotel card has selected star rating
              And I unselect one by one all star rating and verify hotel count and each hotel card has selected star rating

        @C2925173 @C2925137 @web @mobile @tabs
        Scenario: Filter combination of Star rating and Board Type
             When I select lowest star rating filter and verify disabled filter items of 'Board Type'
             Then Disabled 'Board Type' filter label should not be displayed for each hotel card
             When I unselect lowest star rating filter
             Then Disabled filter count should be 0 for 'Board Type' filter
             When I check 'Board Type' filter which was disbaled after star rating filter selection
             Then Disabled 'Star Rating' filter label should not be displayed for each hotel card
              And lowest star rating filter should be disabled
             When I uncheck 'Board Type' filter which was disbaled after star rating filter selection
             Then Disabled filter count should be 0 for 'Star Rating' filter

        @C2925947 @web @mobile @tab
        Scenario: Single filter selection for filter Holiday Types
             Then I check one by one 'Holiday Types' filters and verify hotel count should be less than or equal to original hotel count

        @C2925166 @C2934133 @web @mobile @tab
        Scenario: Multi filter selection for filter Holiday Types
             Then I check multiple 'Holiday Types' filters and verify hotel count should be less than or equal to after selecting each filter
              And I uncheck one by one all 'Holiday Types' filters and verify hotel count should be greater than or equal to after unselecting each filter

        @C2925165 @web @mobile @tabs
        Scenario: Filter combination of Board Type, Holiday Types and Star Rating
             When I select lowest star rating filter and verify disabled filter items of 'Holiday Types'
              And I unselect lowest star rating filter
             Then Disabled filter count should be 0 for 'Holiday Types' filter
              And Disabled filter count should be 0 for 'Board Type' filter
             When I check 'Holiday Types' filter which was disbaled after star rating filter selection
             Then Disabled 'Star Rating' filter label should not be displayed for each hotel card
              And lowest star rating filter should be disabled
             When I uncheck 'Holiday Types' filter which was disbaled after Board Type filter selection
             Then Disabled filter count should be 0 for 'Holiday Types' filter
              And Disabled filter count should be 0 for 'Board Type' filter

        @C2934137 @web @mobile @tab
        Scenario: Single filter selection for filter Property Amenities
             Then I check one by one 'Property Amenities' filters and verify hotel count should be less than or equal to original hotel count
             
        @C2934137 @web @mobile @tab
        Scenario: Multi filter selection for filter Property Amenities
             Then I check multiple 'Property Amenities' filters and verify hotel count should be less than or equal to after selecting each filter
              And I uncheck one by one all 'Property Amenities' filters and verify hotel count should be greater than or equal to after unselecting each filter

        @C2925165 @web @mobile @tabs
        Scenario: Filter combination of Holiday Types, Property Amenities and Star Rating
             When I select lowest star rating filter and verify disabled filter items of 'Property Amenities'
              And I unselect lowest star rating filter
             Then Disabled filter count should be 0 for 'Property Amenities' filter
              And Disabled filter count should be 0 for 'Holiday Types' filter
             When I check 'Property Amenities' filter which was disbaled after star rating filter selection
             Then Disabled 'Star Rating' filter label should not be displayed for each hotel card
              And lowest star rating filter should be disabled
             When I uncheck 'Property Amenities' filter which was disbaled after Board Type filter selection
             Then Disabled filter count should be 0 for 'Property Amenities' filter
              And Disabled filter count should be 0 for 'Holiday Types' filter

        @C2924511 @C2924820 @C2924829 @C2924834 @web @mobile @tabs
        Scenario Outline: Hotel filter page for customer rating
             Then Default Customer Slider Rating value should vary between '<DefaultMinRating>' and '<DefaultMaxRating>'
             When I drag the customer rating bar for '<MinRating>' and '<MaxRating>'
             Then I should see relevant search results within '<MinRating>' and '<MaxRating>'
              And Slider bar should be read only for irrelevant filter
              And On bottom of Rating Range Bar the Rating value as '<MinRating>' and '<MaxRating>' is displayed
             When I drag the customer rating bar for '<UpdatedMinRating>' and '<UpdatedMaxRating>'
             Then I should see relevant search results within '<UpdatedMinRating>' and '<UpdatedMaxRating>'
              And On bottom of Rating Range Bar the Rating value as '<UpdatedMinRating>' and '<UpdatedMaxRating>' is displayed
        Examples:
                  | DefaultMinRating | DefaultMaxRating | MinRating | MaxRating | UpdatedMinRating | UpdatedMaxRating |
                  | 0                | 10               | 3         | 8         | 4                | 9                |
                  | 0                | 10               | 4         | 10        | 1                | 5                |

        @C2925152 @C2925153 @C2925154 @C2926000
        Scenario: Filter Total Price using slider bar
             Then Total Price filter section with default view expanded is displayed
              And Default Minimum Value and Maximum Value on Price range bar is displayed
              And Price Range Bar and Price Range Indicator should be set to default price
             When I increase minimum Total price and decrease maximum total price
             Then I should see hotels in filtered price range
             When I decrease minimum Total price and increase maximum total price
             Then I should see hotels in filtered price range
              And Other sub categories to be gray out on price range bar
              And The Price value on starting and ending points of price range bar is displayed
              And Price values should be different at starting and ending points of Price range bar
              And Price values changes in Price Range Indicator at starting and ending points

        @C2925616 @C2926450 @C2933471 @web @mobile @tab
        Scenario: Reset Filter
             Then Reset filters button should be disabled
             When I check first filter for 'Board Type'
             Then Reset filters button should be enabled
             When I uncheck first filter of 'Board Type'
             Then Reset filters button should be disabled
             When I check first filter for 'Holiday Types'
             Then Reset filters button should be enabled
             When I uncheck first filter of 'Holiday Types'
             Then Reset filters button should be disabled
             When I check first filter for 'Property Amenities'
             Then Reset filters button should be enabled
             When I uncheck first filter of 'Property Amenities'
             Then Reset filters button should be disabled
             When I select lowest star rating filter
             Then Reset filters button should be enabled
             When I unselect lowest star rating filter
             Then Reset filters button should be disabled
             When I check all filters for 'Board Type'
             Then Selected 'Board Types' filters should be displayed in application url
             When I select all star rating filter
             Then Selected 'Stars' filters should be displayed in application url
             When I check all filters for 'Holiday Types'
             Then Selected 'Holiday Types' filters should be displayed in application url
             When I check all filters for 'Property Amenities'
             Then Selected 'Property Amenities' filters should be displayed in application url
             When I click reset filters button
             Then 'Board Types' filters should not be displayed in application url
              And 'Stars' filters should not be displayed in application url
              And 'Holiday Types' filters should not be displayed in application url
              And 'Property Amenities' filters should not be displayed in application url
              And Reset filters button should be disabled

        @C2926013 @C2933604 @web @mobile @tabs
        Scenario: Verify selected Hotel Property Amenities on Estab page
             When I check all filters for 'Property Amenities'
              And I select hotel for index '1'
             Then Selected Property Amenities filter should be displayed on Estab page

        @C2934734  @web @mobile @tabs
        Scenario: Total Price Range Filter
             Then I should see total price range
             When I select price range 'min:min+1'
             Then Reset filters button should be enabled
              And Hotel should be displayed
             When I click reset filters button
             Then 'Price Range' filters should not be displayed in application url
              And Price Range should be reset
             When I select price range 'max-1:max'
             Then Reset filters button should be enabled
              And Hotel should be displayed
             When I click reset filters button
             Then 'Price Range' filters should not be displayed in application url
              And Price Range should be reset

        @C2934743 @web @mobile @tabs
        Scenario: Customer Rating Range Filter
             Then I should see customer rating range
             When I select customer rating range 'min:min+1'
             Then Reset filters button should be enabled
              And Hotel should be displayed
             When I click reset filters button
             Then 'Rating Range' filters should not be displayed in application url
              And Customer rating Range should be reset
             When I select customer rating range 'max-1:max'
             Then Reset filters button should be enabled
              And Hotel should be displayed
             When I click reset filters button
             Then 'Rating Range' filters should not be displayed in application url
              And Customer rating Range should be reset







