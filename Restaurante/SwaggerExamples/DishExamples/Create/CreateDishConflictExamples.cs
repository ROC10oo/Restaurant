using Application.Models.Response;
using Domain.ErrorsMessages;
using Swashbuckle.AspNetCore.Filters;
using Domain.Entities;

namespace Restaurant.SwaggerExamples.DishExamples.Create
{
    public class CreateDishConflictExamples : IExamplesProvider<ApiErrorr>
    {
        public ApiErrorr GetExamples()
        {
            return new ApiErrorr
            {
                Message = DishErrorMessages.DishAlreadyExists
            };
        }
    }
}
