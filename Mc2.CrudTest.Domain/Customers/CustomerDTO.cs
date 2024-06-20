using Mc2.CrudTest.Domain.Customers;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public class CustomerDTO
    {
    
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public ulong PhoneNumber { get; set; }
        public string Email { get; set; } 
        public string BankAccountNumber { get; set; }
    }
}