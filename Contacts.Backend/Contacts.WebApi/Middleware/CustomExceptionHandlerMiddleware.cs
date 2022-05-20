using System;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Contacts.Application.Common.Exceptions;

namespace Contacts.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        
        public CustomExceptionHandlerMiddleware(RequestDelegate next) => 
            _next = next;

        public async Task Invoke(HttpContext context)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            
            switch (exception)
            {
                case ValidationException validationEx:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationEx.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
                result = JsonSerializer.Serialize(new { errpr = exception.Message });

            return context.Response.WriteAsync(result);

        }

    }
}
