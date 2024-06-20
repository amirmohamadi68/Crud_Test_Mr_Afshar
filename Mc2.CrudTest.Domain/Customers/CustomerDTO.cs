using Mc2.CrudTest.Domain.Customers;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public record CustomerDTO
    {
    
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public ulong PhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
    }
}