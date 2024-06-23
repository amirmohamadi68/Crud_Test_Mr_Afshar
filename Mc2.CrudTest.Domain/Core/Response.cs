
namespace Mc2.CrudTest.Domain.Core
{
    public record Response
    {
        private Response(int _StatusCode, string _ErrorMessage, bool _HasError)
        {
            StatusCode = _StatusCode!;
            ErrorMessage = _ErrorMessage!;
            HasError = _HasError!;
        
        }
       public   int StatusCode { get; private set; }
         public string ErrorMessage { get; private set; }
        public bool HasError { get; private set; }
        public static Response Create(int _StatusCode, string _ErrorMessage, bool _HasError)
        {
            return new Response(_StatusCode, _ErrorMessage, _HasError);
        }
    }
    public class GenericRespons<T>
    {
        public GenericRespons(int statusCode, string errorMessage, bool hasError, T data)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            HasError = hasError;
            Data = data;
        }

        public int StatusCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool HasError { get; private set; }
        public T Data { get; private set; }
       

    }
}