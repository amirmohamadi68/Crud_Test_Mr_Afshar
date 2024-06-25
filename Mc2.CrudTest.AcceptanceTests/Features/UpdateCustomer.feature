Feature: Update Customer
@UpdateSuccessfully
Scenario: Update customer successfully when data is valid
    Given  there is customer in DB
	Given Update customer information (<FirstName>,<Lastname>,<DateOfBirth>,<PhoneNumber>,<Email>,<BankAccountNumber>)
    When I send a PUT request to update the customer
    Then Update result should be succeeded
    
    Examples:
         | FirstName | Lastname   | DateOfBirth | PhoneNumber   | Email                      | BankAccountNumber |
         | Amir2      | Mohamadi2   | 1990-01-01  | +989362174891 | a.mohamadi2.d68@gmail.com   | 999999999999      |

Scenario: Update customer when email is invalid
    Given Update customer information ( <FirstName>, <Lastname>, <DateOfBirth>, <PhoneNumber>, <Email>, <BankAccountNumber>)
    When I send a PUT request to update the customer
    Then Update result should be failed
    
    Examples:
      | FirstName | Lastname   | DateOfBirth | PhoneNumber   | Email               | BankAccountNumber |
      | Amir      | Mohamadi   | 1991-02-02  | +989362174891 | a.mohamaail.com     | 999999999999      |

Scenario: Update customer with invalid phone number
    Given Update customer information ( <FirstName>, <Lastname>, <DateOfBirth>, <PhoneNumber>, <Email>, <BankAccountNumber>)
    When I send a PUT request to update the customer
    Then Update result should be failed

    Examples:
   | FirstName | Lastname   | DateOfBirth | PhoneNumber  | Email                      | BankAccountNumber |
   | Amir      | Mohamadi   | 1991-02-02  | +362174891   | a.mohamadi.d68@gmail.com   | 999999999999      |

Scenario: Update customer when email is repeated 
    Given Update customer information ( <FirstName>, <Lastname>, <DateOfBirth>, <PhoneNumber>, <Email>, <BankAccountNumber>)
    When I send a PUT request to update the customer
    Then Update result should be failed
    
    Examples:
    | FirstName | Lastname | DateOfBirth | PhoneNumber   | Email                      | BankAccountNumber |
    | Aaaaa     | Aaammm   | 1991-02-02  | +989362174891 | a.mmmmmmm@example.com      | 999999999999      |