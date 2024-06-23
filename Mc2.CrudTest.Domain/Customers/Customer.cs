using Mc2.CrudTest.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : Entity

    {
        public Customer()
        {
            
        }
        //  public static readonly List<DomainEvent> _domainEvents = new();
        private Customer(CustomerId customerId, FirstName firstName, LastName lastName, DateOfBirth dateOfBirth, PhoneNumber phoneNumber, Email email, BankAccountNumber bankAccountNumber)
        {
            Id = customerId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }


        public CustomerId Id { get; private set; }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public DateOfBirth DateOfBirth { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public BankAccountNumber BankAccountNumber { get; private set; }
        public static Customer CreateInstanceForUpdate(CustomerId olddId ,  CustomerDTO customerDTO)
        {
            if (customerDTO == null) throw new ArgumentNullException(nameof(customerDTO)); ;
            var customer = new Customer(olddId, FirstName.Create(customerDTO.FirstName), LastName.Create(customerDTO.LastName)
                            , DateOfBirth.Create(customerDTO.DateOfBirth), PhoneNumber.Create(customerDTO.PhoneNumber),
                            Email.Create(customerDTO.Email), BankAccountNumber.Create(customerDTO.BankAccountNumber));
           
            return customer;
        }

        public static Customer Create(CustomerDTO customerDTO)
        {
            if (customerDTO == null) throw new ArgumentNullException(nameof(customerDTO)); ;
            var customer = new Customer(new CustomerId(Guid.NewGuid()), FirstName.Create(customerDTO.FirstName), LastName.Create(customerDTO.LastName)
                            , DateOfBirth.Create(customerDTO.DateOfBirth), PhoneNumber.Create(customerDTO.PhoneNumber),
                            Email.Create(customerDTO.Email), BankAccountNumber.Create(customerDTO.BankAccountNumber));
            customer.Raise(new CustomerCreatedEvent(Guid.NewGuid(), nameof(CustomerCreatedEvent), customer.FirstName.Value, customer.LastName.Value, customer.Email.Value
                                                        , customer.PhoneNumber.PhoneValue.ToString(), customer.BankAccountNumber.Value, customer.DateOfBirth.Value));
            return customer;
        }
        ///for now i coonsidering for update should pass All properties i will change that to builder pattern at the end
        public  void Update(CustomerDTO newCustomerDTO)
        {
           this. FirstName = FirstName.Create(newCustomerDTO.FirstName);
           this. LastName = LastName.Create(newCustomerDTO.LastName);
           this. DateOfBirth = DateOfBirth.Create(newCustomerDTO.DateOfBirth);
           this. PhoneNumber = PhoneNumber.Create(newCustomerDTO.PhoneNumber);
           this. Email = Email.Create(newCustomerDTO.Email);
           this. BankAccountNumber = BankAccountNumber.Create(newCustomerDTO.BankAccountNumber);

          this.Raise(new CustomerUpdatedEvent(Guid.NewGuid(), Id.CId, nameof(CustomerUpdatedEvent), newCustomerDTO.FirstName, newCustomerDTO.LastName,
              newCustomerDTO.PhoneNumber, newCustomerDTO.Email, newCustomerDTO.BankAccountNumber, newCustomerDTO.DateOfBirth));
        }
    
    }
}