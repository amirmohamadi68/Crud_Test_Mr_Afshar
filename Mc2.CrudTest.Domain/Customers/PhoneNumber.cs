namespace Mc2.CrudTest.Domain.Customers
{
    public record PhoneNumber
    {
        // i decide make phone number easier :D
        private const int PhoneLength = 13;
        // private const int CountryCharCodeLength = 2;
        public ulong PhoneValue { get; private set; }
        // public string CountryCharCode { get; init; }
        private PhoneNumber(ulong phoneValue)
        {
            PhoneValue = phoneValue;

        }
        public static PhoneNumber Create(string phoneValue)
        {
            if (phoneValue.Equals(0))
                throw new ArgumentNullException(nameof(phoneValue));
            ulong convertedNumber = ConvertToUlong(phoneValue);
            return new PhoneNumber(convertedNumber);
            //other check
        }
        public static ulong ConvertToUlong(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                // it hasnt need this validate because i have valifator if app layer
                throw new ArgumentNullException(nameof(phoneNumber));

            // Remove non-digit characters
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Try to parse the string to ulong
            if (ulong.TryParse(digitsOnly, out ulong result))
            {
                return result;
            }
            else
                throw new ArgumentException(nameof(phoneNumber));
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
}