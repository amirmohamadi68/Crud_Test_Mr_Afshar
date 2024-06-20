
namespace Mc2.CrudTest.Application.Customers.Commands
{
    public record GenericResponse
    {
        private GenericResponse(int StatusCode, string ErrorMessage, bool HasError)
        {
            StatusCode = StatusCode;
            ErrorMessage = ErrorMessage;
            HasError = HasError;
        
        }
       public   int StatusCode { get; private set; }
         public string ErrorMessage { get; private set; }
        public bool HasError { get; private set; }
        public static GenericResponse Create(int StatusCode, string ErrorMessage, bool HasError)
        {
            return new GenericResponse(StatusCode, ErrorMessage, HasError);
        }
    }
}