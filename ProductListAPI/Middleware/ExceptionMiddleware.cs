using ProtoType.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProtoTypeAPI.Middleware
{
    /// <summary>
    /// Error Middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;        
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="telemetryClient"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;            
            _logger = logger;
        }

        /// <summary>
        /// Invoke Method
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Error error = new();
            error.Code = (int)HttpStatusCode.InternalServerError;
            error.Message = "Internal Server Error";
            error.Type = "Unhandle Exception";
            error.ErrorID = DateTime.UtcNow.Ticks;

            Dictionary<string, string> errorId = new()
            {
                { "ErrorID", error.ErrorID.Value.ToString() }
            };

            _logger.LogError(exception, exception.Message);
            _logger.LogInformation($"Error Message: {exception.Message} \r\n Stack Trace: {exception.StackTrace}");

            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
