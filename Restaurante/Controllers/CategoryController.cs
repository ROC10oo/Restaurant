using Application.Interfaces.ICategory;
using Application.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.SwaggerExamples.CategoryExamples.Get;
using Restaurant.SwaggerExamples.DishExamples.Create;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGetCategoriesService _getCategoryService;

        public CategoryController(IGetCategoriesService getCategorys)
        {
            _getCategoryService = getCategorys;
        }


        /// <summary>
        /// Obtener categorias de platos
        /// </summary>
        /// <remarks>
        /// Obtiene todas las categorías disponibles para clasificar platos.
        ///
        ///
        /// **Casos de uso:**
        /// - Mostrar categorías en formularios de creación/edición de platos
        /// - Filtros de búsqueda en el menú
        /// - Organización del menú por secciones
        /// </remarks>
        /// /// <response code="200">Lista de categorías obtenida exitosamente</response>
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>),StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllCategoriesOkExample))]
        [HttpGet]
        [Route("/api/v1/Category")]
        public async Task<ActionResult<CategoryResponse>> GetCategories()
        {
            var categories = await _getCategoryService.GetAllCategories();
            return Ok(categories);
        }

    }
}
