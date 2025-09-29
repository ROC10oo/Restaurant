using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DishExamples.Get
{
    public class GetDishNotFoundExample : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                Message = ErrorMessages.DishNotExists,
            };
        }
    }
}
