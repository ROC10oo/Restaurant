using Domain.ErrorsMessages;
using System.Net;
using System.Text.Json;
using Domain.Entities;

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

            // Mapeo de mensajes a códigos de estado
            var statusCode = HttpStatusCode.InternalServerError; //500
            var message = ex.Message;

            if (message == DishErrorMessages.EmptyName ||
                message == DishErrorMessages.InvalidPrice ||
                message == DishErrorMessages.CategoryNotExists || 
                message == DishErrorMessages.InvalidParameter)
            {
                statusCode = HttpStatusCode.BadRequest; // 400
            }
            else if (message == DishErrorMessages.DishAlreadyExists)
            {
                statusCode = HttpStatusCode.Conflict; // 409
            }
            else if (message == DishErrorMessages.DishNotExists) // <- 404
            {
                statusCode = HttpStatusCode.NotFound;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new ApiError { Message =  message };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
