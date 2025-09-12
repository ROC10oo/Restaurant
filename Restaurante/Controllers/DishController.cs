using Application.Enums;
using Application.Interfaces.IDish;
using Application.Models.Request;
using Application.Models.Response;
using Application.Validator;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Restaurant.SwaggerExamples.DishExamples.Create;
using Restaurant.SwaggerExamples.DishExamples.Get;
using Restaurant.SwaggerExamples.DishExamples.Update;
using Swashbuckle.AspNetCore.Filters;


namespace Restaurant.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IGetDishesService _getDishesService;
        private readonly ICreateDishService _createDishService;
        private readonly IUpdateDishService _updateDishService;

        public DishController(IGetDishesService getDishesService, ICreateDishService createDishService, IUpdateDishService updateDishService)
        {
            _getDishesService = getDishesService;
            _createDishService = createDishService;
            _updateDishService = updateDishService;
        }






        /// <summary>
        /// Crea un nuevo plato
        /// </summary>
        /// <remarks>
        /// Agrega un nuevo plato al menú del restaurante.
        /// 
        /// **Validaciones:**
        /// - El nombre del plato debe ser único
        /// - El precio debe ser mayor a 0
        /// - La categoría debe existir
        /// 
        /// 
        ///
        /// **Casos de uso:**
        /// - Agregar nuevos platos al menú
        /// - Platos estacionales o especiales del chef
        /// 
        /// </remarks>
        /// <param name="request">Datos del plato a crear</param>
        /// <response code="201">Plato creado exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="409">Ya existe un plato con el mismo nombre</response>
        /// <response code="500">Error interno del servidor</response>
        /// 

        [HttpPost]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status201Created)]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreateDishCreatedExample))]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateDishBadRequestExamples))]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(CreateDishConflictExamples))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateDish([FromBody] DishRequest request)
        {
            var dish = await _createDishService.CreateDish(request);

            return CreatedAtAction(nameof(CreateDish), new { id = dish.id }, dish);
        }



        /// <summary>
        /// Actualizar plato existente
        /// </summary>
        /// <remarks>
        /// Actualiza todos los campos de un plato existente en el menú.
        /// 
        /// **Validaciones:**
        /// 
        /// - El plato debe existir en el sistema
        /// - Si se cambia el nombre, debe ser único
        /// - El precio debe ser mayor a 0
        /// - La categoría debe existir
        ///
        /// **Casos de uso:**
        /// - Actualizar precios de platos
        /// - Modificar descripciones o ingredientes
        /// - Cambiar categorías de platos
        /// - Activar/desactivar platos del menú
        /// - Actualizar imágenes de platos
        /// </remarks>
        /// /// <param name="id">ID único del plato a actualizar
        ///</param>
        ///
        /// <param name="request">Datos para actualizar el plato</param>
        /// <response code="200">Plato actualizado exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="404">Plato no encontrado</response>
        ///  <response code="409">Conflicto - nombre duplicado</response>	
        /// <response code="500">Error interno del servidor</response>


        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateDishBadRequestExamples))]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UpdateDishNotFoundExample))]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(UpdateDishConflictExample))]


        public async Task<IActionResult> UpdateDish(Guid id, [FromBody] DishRequestUpdate request)
        {
            var resultado = await _updateDishService.UpdateDish(request, id);
            return Ok(resultado);

        }


        /// <summary>
        /// Buscar platos
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de platos del menú con opciones de filtrado y ordenamiento.
        ///
        /// **Filtros disponibles:**
        /// - Por nombre (búsqueda parcial)
        /// - Por categoría
        /// - Solo platos activos/todos
        ///
        /// **Ordenamiento:**
        /// - Por precio ascendente o descendente
        /// - Sin ordenamiento específico
        ///
        /// **Casos de uso:**
        /// - Mostrar menú completo a los clientes
        /// - Buscar platos específicos
        /// - Filtrar por categorías (entrantes, principales, postres)
        /// - Administración del menú (incluyendo platos inactivos)
        /// </remarks>
        /// <param name="name">
        /// Buscar platos por nombre (búsqueda parcial)
        /// </param>
        /// <param name="category">
        /// Filtrar por categoría de plato
        /// </param>
        /// <param name="sortByPrice">
        /// Ordenar por precio:
        ///   * <c>asc</c>: ascendente (menor a mayor)
        ///   * <c>desc</c>: descendente (mayor a menor)
        ///   * vacío: sin ordenamiento específico
        /// </param>
        /// <param name="onlyActive">
        /// Filtrar por estado:
        ///   * <c>true</c>: devolver solo platos disponibles
        ///   * <c>false</c>:  devolver todos los platos (incluyendo inactivos)
        /// </param>
        /// /// <response code="200">Lista de platos obtenida exitosamente</response>
        /// <response code="400">Parámetros de búsqueda inválidos</response>
        /// 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GetDishBadRequestExample))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishResponse>>> GetDishes([FromQuery] string name = null, [FromQuery] int? category = null, [FromQuery] OrderByPrice? sortByPrice = OrderByPrice.asc, [FromQuery] bool onlyActive = true)
        {

            var dishes = await _getDishesService.GetDishes(name, category, sortByPrice, onlyActive);

            return Ok(dishes);
        }

    }
}

