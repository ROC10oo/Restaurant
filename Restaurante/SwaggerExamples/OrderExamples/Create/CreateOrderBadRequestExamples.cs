using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderExamples.Create
{
    public class CreateOrderBadRequestExamples : IMultipleExamplesProvider<ApiError>
    {
        public IEnumerable<SwaggerExample<ApiError>> GetExamples()
        {
            return new[]
            {
                SwaggerExample.Create("Plato no valido", new ApiError { Message = ErrorMessages.DishNotAvailableOrNotExists }),
                SwaggerExample.Create("Cantidad invalida", new ApiError { Message = ErrorMessages.InvalidCant }),
                SwaggerExample.Create("Tipo de entrega faltante", new ApiError { Message = ErrorMessages.InvalidDelivery }),
            };
        }
    }
}
