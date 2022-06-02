Feature: API End 2 End Tests

    Scenario: TR E2E test
        When I complete a hotel e2e booking via API for domain <domain> to destination <destination> for dates <checkInDaysFromCurrentDay> and <duration>
        # Then Check booking id is created
        Examples:
            | domain | destination | checkInDaysFromCurrentDay | duration |
            | UK     | Maldives    | 60                        | 7        |


#  Scenario: Sunmaster E2E test
#         When I complete a hotel e2e booking via API for domain <domain> to destination <destination> for dates <checkInDaysFromCurrentDay> and <duration>
#         Then Check booking id is created
#         Examples:
#             | domain | destination | checkInDaysFromCurrentDay | duration |
#             | UK     | Maldives    | 60                        | 7        |

