using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiMovies.Exceptions;
using WebApiMovies.Exceptions.Models;

namespace WebApiMovies.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogError("Request {HttpMetod} {Path} {Exception}", context.Request.Method, context.Request.Path, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(ex);
            var message = context.Response.StatusCode == 500 ? "Internal server error" : ex.Message;
            ExceptionDetailsModel response = new ExceptionDetailsModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            await context.Response.WriteAsync(response.ToString());
            
        }

        private static HttpStatusCode GetStatusCode(Exception ex)
        {
            switch (ex)
            {
                case NotFoundException _:
                    return HttpStatusCode.NotFound;
                case BadRequestException _:
                    return HttpStatusCode.BadRequest;
                case UnauthorizedException _:
                    return HttpStatusCode.Unauthorized;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        } 
    }
}
