using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CondoApp.WebApi.Models;

namespace CondoApp.WebApi.Services
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status200OK; // Always 200

                var apiResponse = new ApiResponse<object>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                };

                var json = JsonSerializer.Serialize(apiResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }

}