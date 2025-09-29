using Application.Interfaces.IOrder;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.SwaggerExamples.DishExamples.Update;
using Restaurant.SwaggerExamples.OrderExamples.Create;
using Restaurant.SwaggerExamples.OrderExamples.Get;
using Restaurant.SwaggerExamples.OrderExamples.Update;
using Swashbuckle.AspNetCore.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IGetOrdersService _getOrdersService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;


        public OrderController(ICreateOrderService createOrderService, IGetOrdersService getOrdersService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _getOrdersService = getOrdersService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }


        /// <summary>
        /// Crea nueva orden
        /// </summary>
        /// <remarks>
        /// Crea una nueva orden con los platos solicitados por el cliente.
        /// 
        /// **Proceso:**
        /// 1. Se valida que todos los platos existan y estén activos
        /// 2. Se calcula el total de la orden
        /// 3. Se asigna un número de orden único
        /// 4. Se crean los items individuales de la orden
        /// 
        /// 
        ///
        /// **Validaciones:**
        /// - Los platos deben existir y estar activos
        /// - Las cantidades deben ser mayores a 0
        /// - Debe especificarse tipo de entrega
        /// 
        /// </remarks>
        /// <param name="request">Datos de la nueva orden</param>
        /// <response code="201">Orden creada exitosamente</response>
        /// <response code="400">Datos de orden inválidos</response>
        /// 

        [ProducesResponseType(typeof(OrderCreateResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(CreateOrderBadRequestExamples))]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var order = await _createOrderService.CreateOrder(request);

            return CreatedAtAction(nameof(CreateOrder), new { id = order.orderNumber }, order);
        }


        /// <summary>
        /// Buscar ordenes
        /// </summary>
        /// <remarks>
        /// Obtiene una lista de órdenes con filtros opcionales.
        ///
        /// **Filtros disponibles:**
        /// - Por rango de fechas (desde/hasta)
        /// - Por estado de la orden
        /// 
        /// 
        /// **Casos de uso:**
        /// - Ver órdenes del día para cocina
        /// - Historial de órdenes del cliente
        /// - Reportes de ventas por período
        /// - Seguimiento de órdenes pendientes
        /// </remarks>
        /// <param name="from">
        /// Fecha y hora de inicio para filtrar órdenes
        /// </param>
        /// <param name="to">
        /// Fecha y hora de fin para filtrar órdenes
        /// </param>
        /// <param name="statusId">
        /// Filtrar por estado de la orden
        /// </param>
        /// /// <response code="200">Lista de órdenes obtenida exitosamente</response>
        /// <response code="400">Parámetros de búsqueda inválidos</response>
        /// 
        [ProducesResponseType(typeof(IEnumerable<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllOrdersOKExample))]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GetOrdersBadRequestExample))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderCreateResponse>>> GetOrders([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? statusId)
        {

            var orders = await _getOrdersService.GetOrders(from, to, statusId);

            return Ok(orders);
        }



        /// <summary>
        /// Obtener orden por numero
        /// </summary>
        /// <remarks>
        /// Obtiene los detalles completos de una orden específica.
        ///
        ///
        /// **Información incluida:**
        /// - Detalles de la orden (número, total, estado)
        /// - Información de entrega
        /// - Lista completa de items con sus estados individuales
        /// - Información de cada plato incluido
        /// 
        /// **Casos de uso:**
        /// - Seguimiento de orden por parte del cliente
        /// - Detalles para cocina y entrega
        /// - Historial detallado de órdenes
        /// </remarks>
        /// /// <response code="200">Orden encontrada exitosamente</response>
        ///  <response code="404">Orden no encontrada</response>
        ///  
        [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GetOrderNotFoundExample))]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsResponse>> GetOrder([FromRoute] long id)
        {
            var order = await _getOrderService.GetOrderById(id);
            return Ok(order);
        }





        /// <summary>
        /// Actualizar orden existente
        /// </summary>
        /// <remarks>
        /// Actualiza los items de una orden existente.
        /// 
        /// **Limitaciones:**
        /// 
        /// - Solo se pueden actualizar órdenes que no esten cerradas
        /// - No se pueden agregar items de platos inactivos
        /// - El total se recalcula automáticamente
        ///
        /// **Casos de uso:**
        /// - Cliente quiere agregar más platos a su orden
        /// - Modificar cantidades antes de que comience la preparación
        /// - Cambiar notas especiales de los platos
        /// </remarks>
        /// /// <param name="id">ID único de la orden a actualizar
        ///</param>
        ///
        /// <param name="request">Items actualizados de la orden</param>
        /// <response code="200">Orden actualizada exitosamente</response>
        /// <response code="400">Datos de actualización inválidos</response>
        /// <response code="404">Orden no encontrada</response>
        /// 

        [ProducesResponseType(typeof(OrderUpdateReponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(UpdateOrderBadRequestExamples))]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(UpdateOrderNotFoundExample))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderUpdateRequest request)
        {
            var resultado = await _updateOrderService.UpdateOrder(request, id);
            return Ok(resultado);

        }
    }
}
