using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using Domain.ErrorsMessages;
using Domain.Entities;

namespace Restaurant.SwaggerExamples.DishExamples.Update
{
    public class UpdateDishBadRequestExamples : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                Message = DishErrorMessages.InvalidPrice
            };
        }
    }
}
