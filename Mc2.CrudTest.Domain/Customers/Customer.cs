using Mc2.CrudTest.Application.Customers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer
    {
        public Customer() { }
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

        public static Customer Create(CustomerDTO customerDTO)
        {
            if (customerDTO == null) throw new ArgumentNullException(nameof(customerDTO)); ;
            // i will fix null refrence with throw custom exception in objects value and design exception handling later dont worry
            return new Customer(new CustomerId(Guid.NewGuid()), FirstName.Create(customerDTO.FirstName), LastName.Create(customerDTO.LastName)
                , DateOfBirth.Create(customerDTO.DateOfBirth), PhoneNumber.Create(customerDTO.PhoneNumber), 
                Email.Create(customerDTO.Email),   BankAccountNumber.Create(customerDTO.BankAccountNumber));

        }
    }
    public record FirstName
    {
        public const int Length = 30;
        public string Value { get; init; }
        private FirstName(string value)
        {
            Value = value;
        }
        public static FirstName Create(string value)
        {
            if (string.IsNullOrEmpty(value))
               throw new ArgumentNullException(nameof(value));
            if (value.Length > Length)
                throw new ArgumentNullException(nameof(value));
            return new FirstName(value);
            //other check
        }
    }
    public record LastName
    {
        public const int Length = 50;
        public string Value { get; init; }
        private LastName(string value)
        {
            Value = value;
        }
        public static LastName Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (value.Length > Length)
                throw new ArgumentNullException(nameof(value));
            return new LastName(value);
            //other check
        }
    }
    public record DateOfBirth
    {
        private const double ValidYearsOld = 365 * 99;
        public string Value { get; init; }
        private DateOfBirth(string value)
        {
            Value = value;
        }
        public static DateOfBirth Create(DateTime value)
        {
            if (value.AddDays(ValidYearsOld) > DateTime.UtcNow)
                throw new ArgumentNullException(nameof(value));

            return new DateOfBirth(value.ToShortDateString());
        }
    }
    public record PhoneNumber
    {
        // i decide make phone number easier :D
        private const int PhoneLength = 13;
       // private const int CountryCharCodeLength = 2;
        public ulong PhoneValue { get; init; }
       // public string CountryCharCode { get; init; }
        private PhoneNumber(ulong phoneValue)
        {
            PhoneValue = phoneValue;
          
        }
        public static PhoneNumber Create(ulong phoneValue)
        {
            if (phoneValue.Equals(0))
                throw new ArgumentNullException(nameof(phoneValue));

            return new PhoneNumber(phoneValue);
            //other check
        }
        //public ulong toUlong(PhoneNumber phoneNumber)
        //{
        //    return CharCodeToDigit(CountryCharCode, PhoneValue);
        //}

        //private ulong CharCodeToDigit(string countryCharCode, int phoneValue)
        //{
        //    throw new NotImplementedException();
        //}
    }
    public record Email
    {
        public const int MaxLength = 256;
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        private static readonly Lazy<Regex> EmailFormatRegex =
            new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));
        public string Value { get; init; }
        public Email()
        {
                
        }
        private Email(string email)
        {
            Value = email;
        }
        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("");
            if (email.Length > MaxLength) throw new Exception();
            if (!EmailFormatRegex.Value.IsMatch(email)) throw new Exception();
            return new Email(email);
            //other check
        }

    }
    public record BankAccountNumber
    {
        private const int Length = 50;
        public string Value { get; init; }
        private BankAccountNumber(string value)
        {
            Value = value;
        }
        public static BankAccountNumber Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (value.Length > Length)
                throw new ArgumentNullException(nameof(value));
            return new BankAccountNumber(value);
            //other check
        }
    }
}