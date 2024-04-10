using System.Net;
using Newtonsoft.Json;
using Site.Application.Definitions.Dtos.ErrorException;

namespace Site.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
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
                //log
                //اگر حالت وب اپلیکیشن بود
                //throw new Exception(ex.Message, ex);

                //for WebApi
                await GetResponse(httpContext);
            }

        }
        private Task GetResponse(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "Application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var text = new ErrorDetailsDto
            {
                Message = "خطایی در سمت سرور رخ داد",
                StatusCode = httpContext.Response.StatusCode,
            };
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(text));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
}
