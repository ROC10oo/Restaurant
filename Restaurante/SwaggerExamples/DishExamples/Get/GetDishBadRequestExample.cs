using Domain.Entities;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.DishExamples.Get
{
    public class GetDishBadRequestExample : IExamplesProvider<ApiErrorr>
    {
        public ApiErrorr GetExamples()
        {
            return new ApiErrorr
            {
                Message = DishErrorMessages.InvalidParameter,
            };
        }
    }
}
