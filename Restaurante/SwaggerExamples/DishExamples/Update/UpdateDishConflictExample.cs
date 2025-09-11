using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using Domain.ErrorsMessages;
using Domain.Entities;

namespace Restaurant.SwaggerExamples.DishExamples.Update
{
    public class UpdateDishConflictExample : IExamplesProvider<ApiErrorr>
    {
        public ApiErrorr GetExamples()
        {
            return new ApiErrorr
            {
                Message = DishErrorMessages.DishAlreadyExists,
            };
        }
    }
}
