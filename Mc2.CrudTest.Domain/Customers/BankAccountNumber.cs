namespace Mc2.CrudTest.Domain.Customers
{
    public record BankAccountNumber
    {
        private const int Length = 50;
        public string Value { get; private set; }
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