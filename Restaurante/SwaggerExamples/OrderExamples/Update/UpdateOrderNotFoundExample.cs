using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderExamples.Update
{
    public class UpdateOrderNotFoundExample : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                Message = ErrorMessages.OrderNotExists,
            };
        }
    }
}
