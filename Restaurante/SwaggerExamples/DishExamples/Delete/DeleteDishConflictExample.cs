using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DishExamples.Delete
{
    public class DeleteDishConflictExample : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                Message = ErrorMessages.DishUsedInOrder,
            };
        }
    }
}
