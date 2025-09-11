using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;
using Domain.ErrorsMessages;
using Domain.Entities;

namespace Restaurant.SwaggerExamples.DishExamples.Update
{
    public class UpdateDishBadRequestExamples : IExamplesProvider<ApiErrorr>
    {
        public ApiErrorr GetExamples()
        {
            return new ApiErrorr
            {
                Message = DishErrorMessages.InvalidPrice
            };
        }
    }
}
