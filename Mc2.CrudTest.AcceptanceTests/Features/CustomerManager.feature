Feature: Create Customer

  Scenario: Successfully create a customer
    Given a valid customer DTO
    When the CreateCustomerCommand is handled
    Then the customer should be created
    And the response should indicate success