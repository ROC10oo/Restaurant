using Application.Models.Response;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;
using Domain.Entities;

namespace Restaurant.SwaggerExamples.DishExamples.Create
{
    public class CreateDishConflictExamples : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                Message = DishErrorMessages.DishAlreadyExists
            };
        }
    }
}
