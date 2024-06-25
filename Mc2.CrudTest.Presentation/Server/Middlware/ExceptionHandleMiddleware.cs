using Azure;
using Mc2.CrudTest.Domain.Core;
using Microsoft.AspNetCore.Http;
using Response = Mc2.CrudTest.Domain.Core.Response;

namespace Mc2.CrudTest.Presentation.Server.Middlware
{
    
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(ex, httpContext);
            }
        }

        private async Task HandleException(Exception ex, HttpContext httpContext)
        {
            if (ex is InvalidOperationException)
            {
                httpContext.Response.StatusCode = 400; //HTTP status code
                                                       //httpContext.Response.WriteAsync("Invalid operation");
                                                       //httpContext.Response.WriteAsync("Invalid operation");             
                await httpContext.Response.WriteAsJsonAsync(
                    Response.Create(_StatusCode: 400, _ErrorMessage: "Invalid operation", false)
                );
            }
            else if (ex is ArgumentException)
            {
                await httpContext.Response.WriteAsync($"ArgumentException : {ex.Message}");
            }
            else if (ex is CustomerValidateException)
            {
                List<ValidationError> dataError = (ex as CustomerValidateException).GetErrors();
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsJsonAsync(
                  new  GenericRespons<List<ValidationError>>(400, "Validate error", false , dataError));
            }
            else if (ex is ArgumentNullException)
             {    httpContext.Response.StatusCode = 400;
           
                await httpContext.Response.WriteAsJsonAsync(
                   Response.Create(_StatusCode: 400, _ErrorMessage: $"{ex.Message} is null ", true)
               );
            }
            else if (ex is SystemException)
            {
                httpContext.Response.StatusCode = 400;
            }
            else
            {
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync("Unknown error");
            }
        }
    }


    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}
