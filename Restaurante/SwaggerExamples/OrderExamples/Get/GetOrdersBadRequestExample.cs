using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.OrderExamples.Get
{
    public class GetOrdersBadRequestExample : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                Message = ErrorMessages.InvalidDate,
            };
        }
    }
}
