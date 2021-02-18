using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sightseeing.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sightseeing.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(ex, context);
            }
        }

        private Task HandleException(Exception exception, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var responseBody = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseBody = JsonConvert.SerializeObject(validationException.Errors);
                    break;
                case Exception ex:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseBody = JsonConvert.SerializeObject(new { error = exception.Message });
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseBody = JsonConvert.SerializeObject(new { error = exception.Message });
                    break;
            }

            return context.Response.WriteAsync(responseBody);
        }
    }
}
