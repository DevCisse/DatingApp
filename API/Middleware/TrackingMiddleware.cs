using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public TrackingMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                //TODO:if user is not loggedin in add user to in memory store or somewhere

                var ip = context.Connection.RemoteIpAddress.ToString();
                List<string> currentlyLoggedInUsers = new List<string> { "::1", "172.32.32.1" };
                if(currentlyLoggedInUsers.Any(x => x == ip))
               // if (ip == "::1")
                {

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                    var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode) : new ApiException(context.Response.StatusCode, $"You are already loggedin at {ip}");


                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var json = JsonSerializer.Serialize(response, jsonOptions);

                    await context.Response.WriteAsync(json);
                }

                await _next(context);
            }
            catch (System.Exception ex)
            {


            }

            
        }


        private string GetIpAddress()
        {
            return "";
        }
    }
}
