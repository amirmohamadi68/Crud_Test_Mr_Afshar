namespace Mc2.CrudTest.Domain.Customers
{
    public record FirstName
    {
        public const int Length = 30;
        public string Value { get; set; }
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
        public static implicit operator string(FirstName firstName) => firstName.Value;
        
    }
}