using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderExamples.Update
{
    public class UpdateOrderBadRequestExamples : IMultipleExamplesProvider<ApiError>
    {
        public IEnumerable<SwaggerExample<ApiError>> GetExamples()
        {
            return new[]
            {
                SwaggerExample.Create("Orden en preparacion", new ApiError { Message = ErrorMessages.OrderInPreparation }),
                SwaggerExample.Create("Plato no disponible", new ApiError { Message = ErrorMessages.DishNotAvailable }),
            };
        }
    }
}
