using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DishExamples.Update
{
    public class UpdateDishNotFoundExample : IExamplesProvider<ApiErrorr>
    {
        public ApiErrorr GetExamples()
        {
            return new ApiErrorr
            {
                Message = DishErrorMessages.DishNotExists,
            };
        }
    }
}
