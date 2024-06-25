namespace Mc2.CrudTest.Domain.Customers
{
    public record DateOfBirth
    {
        private const double ValidYearsOld = 365 * 99;
        public string Value { get; private set; }
        private DateOfBirth(string value)
        {
            Value = value;
        }
        public static DateOfBirth Create(DateTime value)
        {
            //if (DateTime.UtcNow<value.AddDays(ValidYearsOld))
            //    throw new Exception("max years must be in 99 years old : "+(value));

            return new DateOfBirth(value.ToShortDateString());
        }
    }
}