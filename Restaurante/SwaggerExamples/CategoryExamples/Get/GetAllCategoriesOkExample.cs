using Application.Models.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.SwaggerExamples.CategoryExamples.Get
{
    public class GetAllCategoriesOkExample: IExamplesProvider<IEnumerable<CategoryResponse>>
    {
        public IEnumerable<CategoryResponse> GetExamples() 
        {
            return new List<CategoryResponse>
            {
                new CategoryResponse
                {
                    id = 1,
                    name = "Pizzas",
                    description = "Pizzas artesanales con masa tradicional",
                    order = 1
                },
                new CategoryResponse
                {
                    id = 2,
                    name = "Pastas",
                    description = "Pastas frescas caseras",
                    order = 2
                },
                new CategoryResponse
                {
                    id = 3,
                    name = "Ensaladas",
                    description = "Ensaladas frescas y saludables",
                    order = 3
                },
                new CategoryResponse
                {
                    id = 4,
                    name = "Postres",
                    description = "Postres caseros y dulces tradicionales",
                    order = 4
                }
            };
        }
    }
}
