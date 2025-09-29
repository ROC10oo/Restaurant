using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderItemExamples.Update
{
    public class UpdateOrderItemBadRequestExamples : IMultipleExamplesProvider<ApiError>
    {
        public IEnumerable<SwaggerExample<ApiError>> GetExamples()
        {
            return new[]
            {
                SwaggerExample.Create("Estado Inválido", new ApiError { Message = ErrorMessages.InvalidStatus }),
                SwaggerExample.Create("Transición no permitida", new ApiError { Message = "No se puede cambiar de 'Entregado' a 'En preparación" }),
            };
        }
    }
}
