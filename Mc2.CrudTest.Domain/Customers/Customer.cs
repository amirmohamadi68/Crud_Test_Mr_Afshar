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
        public Guid Id { get; set; }
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        public DateOfBirth DateOfBirth { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }
        public BankAccountNumber BankAccountNumber { get; set; }



    }
    public record FirstName
    {
        private const int Length = 30;
        public string Value { get; init; }
        private FirstName(string value)
        {
            Value = value;
        }
        public static FirstName? Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            if (value.Length > Length)
                return null;
            return new FirstName(value);
            //other check
        }
    }
    public record LastName
    {
        private const int Length = 50;
        public string Value { get; init; }
        private LastName(string value)
        {
            Value = value;
        }
        public static LastName? Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            if (value.Length > Length)
                return null;
            return new LastName(value);
            //other check
        }
    }
    public record DateOfBirth
    {
        private const double ValidYearsOld = 365 * 99;
        public string Value { get; init; }
        private DateOfBirth(DateTime value)
        {
            Value = value.ToShortDateString();
        }
        public static DateOfBirth? Create(DateTime value)
        {
            if (value.AddDays(ValidYearsOld) > DateTime.UtcNow)
                return null;

            return new DateOfBirth(value);
        }
    }
    public record PhoneNumber
    {
        private const int PhoneLength = 11;
        private const int CountryCharCodeLength = 2;
        public int PhoneValue { get; init; }
        public string CountryCharCode { get; init; }
        private PhoneNumber(int phoneValue, string CountryCode)
        {
            PhoneValue = phoneValue;
            CountryCharCode = CountryCode;
        }
        public static PhoneNumber? Create(int phoneValue, string countryCode)
        {
            if (phoneValue.Equals(0))
                return null;
            if (countryCode.Length != CountryCharCodeLength)
                return null;
            return new PhoneNumber(phoneValue, countryCode);
            //other check
        }
        public ulong toUlong(PhoneNumber phoneNumber)
        {
            return CharCodeToDigit(CountryCharCode, PhoneValue);
        }

        private ulong CharCodeToDigit(string countryCharCode, int phoneValue)
        {
            throw new NotImplementedException();
        }
    }
    public record Email
    {
        public const int MaxLength = 256;
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        private static readonly Lazy<Regex> EmailFormatRegex =
            new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));
        public string Value { get; init; }
        private Email(string email)
        {
            Value = email;
        }
        public static Email? Create(string email)
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
        public static BankAccountNumber? Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            if (value.Length > Length)
                return null;
            return new BankAccountNumber(value);
            //other check
        }
    }
}