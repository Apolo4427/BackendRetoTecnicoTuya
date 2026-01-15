using BackendTuya.src.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackendTuya.src.Api.Middlewares
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/problem+json";

                var (status, title) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                    ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
                    _ => (StatusCodes.Status500InternalServerError, "Server Error")
                };

                context.Response.StatusCode = status;

                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = status,
                    Title = title,
                    Detail = ex.Message
                });
            }
        }
    }

}