Feature: Remove Customer
Scenario: Remove customer successfully when data is valid
    Given  there is customer in SeedDataBase	
    When I send a Remove request to Remove the customer
    Then Get Result must have a Delleted
    
Scenario: Remove customer successfully when data is Invalid
    Given  there is customer in SeedDataBase	
    When I send a Remove request with wrong id
    Then Get Result must have a Not Found
    