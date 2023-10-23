using IMS.Application.Exceptions;
using IMS.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace IMS.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var message = string.Empty;

            switch (exception)
            {
                case IMSValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(validationException.ValdationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    message = badRequestException.Message;
                    break;
                case IMSNotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;
                case Exception:
                    message = "Server error";
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            var result = JsonSerializer.Serialize(new
            {
                message,
                isError = true
            });

            return context.Response.WriteAsync(result);
        }
    }
}
