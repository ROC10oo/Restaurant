using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Domain.Entities;

namespace Restaurant.SwaggerExamples.DishExamples.Create
{
    public class CreateDishBadRequestExamples: IMultipleExamplesProvider<ApiError>
    {
        public IEnumerable<SwaggerExample<ApiError>> GetExamples()
        {
            return new[]
            {
                SwaggerExample.Create("Precio inválido", new ApiError { Message = DishErrorMessages.InvalidPrice }),
                SwaggerExample.Create("Nombre vacío", new ApiError { Message = DishErrorMessages.EmptyName }),
                SwaggerExample.Create("Cateogria inexistente", new ApiError { Message = DishErrorMessages.CategoryNotExists }),
            };
        }
    }
}
