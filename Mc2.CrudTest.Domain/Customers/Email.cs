using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Domain.Customers
{
    public record Email
    {
        public const int MaxLength = 256;
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        private static readonly Lazy<Regex> EmailFormatRegex =
            new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));
        public string Value { get; private set; }
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
}