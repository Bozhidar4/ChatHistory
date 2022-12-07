using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ChatHistory.Api.Core
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, exception.Message);

            string result;
            var code = HttpStatusCode.InternalServerError;

            var errorMessage = exception.Message + exception.InnerException ?? "\nInner exception: " + exception?.InnerException?.Message;

            if (exception is ValidationException ||
                exception is System.ComponentModel.DataAnnotations.ValidationException)
            {
                code = HttpStatusCode.BadRequest;
            }

            if (exception is ValidationException)
            {
                errorMessage = errorMessage.Replace("Validation failed: \r\n -- ", "");

                int innerExceptionIndex = errorMessage.IndexOf("\nInner exception", StringComparison.InvariantCulture);
                if (innerExceptionIndex >= 0)
                    errorMessage = errorMessage.Remove(innerExceptionIndex);

                result = JsonConvert.SerializeObject(new { errorMessage });
            }
            else
            {
                result = errorMessage;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
