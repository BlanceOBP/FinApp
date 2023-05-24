using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using FinApp.Exeptions;

namespace FinApp.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            string result;
            switch (exception)
            {
                case BaseException:
                    result = exception.Message;
                    code = BaseException.ErrorCode;
                    break;

                default:
                    result = "Server is error.";
                    code = HttpStatusCode.InternalServerError;
                    break;

            }

            context.Response.ContentType = "text/plain";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
        }

    }
}
