Feature: Get Customer
Scenario: Get customer successfully when data is valid
    Given  there is customer in SeedDB	
    When I send a Get request to get the customer
    Then Get Result must have a valid customer
    
Scenario: Get customer successfully when data is invalid
    Given  there is customer in SeedDB	
    When I send a Get request to get the customer with wrong id
    Then Get Result must have a Bad Request
    