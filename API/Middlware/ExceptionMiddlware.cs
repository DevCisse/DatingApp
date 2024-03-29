using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middlware
{
    public class ExceptionMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddlware(RequestDelegate next,ILogger<ExceptionMiddlware> logger, IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }


        // public async Task InvokeAsync(HttpContext context)
        // {
        //     try
        //     {
        //         await _next(context);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogError(ex, ex.Message);
        //         context.Response.ContentType = MediaTypeNames.Application.Json;
        //         context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

        //         var response  = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString()) : new ApiException(context.Response.StatusCode,"Internal Server error");

        //         var jsonOptions = new JsonSerializerOptions
        //         {
        //             PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //         };

        //         var json  = JsonSerializer.Serialize(response,jsonOptions);
        //         await context.Response.WriteAsync(json);
        //     }
        // }


         public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}