namespace Mc2.CrudTest.Domain.Customers
{
    public record LastName
    {
        public const int Length = 50;
        public string Value { get; private set; }
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
}