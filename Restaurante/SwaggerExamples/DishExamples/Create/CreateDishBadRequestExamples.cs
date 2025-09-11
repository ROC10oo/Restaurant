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
    public class CreateDishBadRequestExamples: IMultipleExamplesProvider<ApiErrorr>
    {
        public IEnumerable<SwaggerExample<ApiErrorr>> GetExamples()
        {
            return new[]
            {
                SwaggerExample.Create("Precio inválido", new ApiErrorr { Message = DishErrorMessages.InvalidPrice }),
                SwaggerExample.Create("Nombre vacío", new ApiErrorr { Message = DishErrorMessages.EmptyName }),
                SwaggerExample.Create("Cateogria inexistente", new ApiErrorr { Message = DishErrorMessages.CategoryNotExists }),
            };
        }
    }
}
