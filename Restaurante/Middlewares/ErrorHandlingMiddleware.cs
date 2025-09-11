using Application.Models.Response;
using Domain.Entities;
using Domain.ErrorsMessages;
using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Restaurant.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = ex.Message;

            switch (ex)
            {
                case InvalidParameterException:
                case CategoryNotFoundException:
                case DishNameEmptyException:
                case DishInvalidPriceException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case DishAlreadyExistsException:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                case DishNotFoundException:
                    statusCode = HttpStatusCode.NotFound; 
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var response = new ApiError { Message = message };
            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
