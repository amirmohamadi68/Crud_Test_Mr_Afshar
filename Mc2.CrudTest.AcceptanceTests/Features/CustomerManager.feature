Feature: Create Customer

@CreateSuccesfully
Scenario: Create customer successfully when data is valid
	Given Create customer information (<FirstName>,<Lastname>,<DateOfBirth>,<PhoneNumber>,<Email>,<BankAccountNumber>)
	When I send a POST request to create the customer
	Then Create result should be succeeded
	
	Examples:
		| FirstName | Lastname        | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber |
		| Amir     | Mohamadi      | 1991-02-02  | +989362174891 | a.mohamadi.d68@gmail.com     | 999999999999  |

		
Scenario: Create customer when email is invalid
		Given Create customer information (<FirstName>,<Lastname>,<DateOfBirth>,<PhoneNumber>,<Email>,<BankAccountNumber>)
			When I send a POST request to create the customer
		Then Create result should be failed
		
	Examples:
		| FirstName | Lastname        | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber |
		| Amir     | Mohamadi      | 1991-02-02  | +989362174891 | a.mohamaail.com     | 999999999999  |


  Scenario: Create customer with invalid phone number
		Given Create customer information (<FirstName>,<Lastname>,<DateOfBirth>,<PhoneNumber>,<Email>,<BankAccountNumber>)
			When I send a POST request to create the customer
		Then Create result should be failed

		Examples:
		| FirstName | Lastname        | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber |
			| Amir     | Mohamadi      | 1991-02-02  | +362174891 | a.mohamadi.d68@gmail.com     | 999999999999  |


  Scenario: Create customer when email is repeated 
		Given Create customer information (<FirstName>,<Lastname>,<DateOfBirth>,<PhoneNumber>,<Email>,<BankAccountNumber>)
			When I send a POST request to create the customer
		Then Create result should be failed
		
	Examples:
		| FirstName | Lastname        | DateOfBirth | PhoneNumber   | Email                | BankAccountNumber |
		| Aaaaa     | Aaammm      | 1991-02-02  | +989362174891 | a.mmmmmmm@example.com             | 99999999999  |
     