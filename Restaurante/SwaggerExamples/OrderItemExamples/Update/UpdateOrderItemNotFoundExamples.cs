using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderItemExamples.Update
{
    public class UpdateOrderItemNotFoundExamples : IMultipleExamplesProvider<ApiError>
    {
        public IEnumerable<SwaggerExample<ApiError>> GetExamples()
        {
            return new[]
            {
                SwaggerExample.Create("Orden no encontrada", new ApiError { Message = ErrorMessages.OrderNotExists }),
                SwaggerExample.Create("Item no encontrado", new ApiError { Message = ErrorMessages.OrderItemNotFound }),
            };
        }
    }
}
