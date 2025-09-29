using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ErrorsMessages
{
    public class ErrorMessages
    {
        public const string EmptyName = "El nombre del plato es obligatorio";
        public const string InvalidPrice = "El precio debe ser mayor a cero";
        public const string DishAlreadyExists = "Ya existe un plato con ese nombre";
        public const string CategoryNotExists = "Debe existir una categoría";
        public const string DishNotExists = "Plato no encontrado";
        public const string InvalidParameter = "Parámetros de ordenamiento inválidos";
        public const string InvalidId = "Formato de ID inválido";
        public const string DishUsedInOrder = "No se puede eliminar el plato porque está incluido en órdenes activas";
        public const string DishNotAvailableOrNotExists = "El plato especificado no existe o no está disponible";
        public const string InvalidCant = "La cantidad debe ser mayor a 0";
        public const string InvalidDelivery = "Debe especificar un tipo de entrega válido";
        public const string InvalidDate = "Rango de fechas inválido";
        public const string OrderNotExists = "Orden no encontrada";
        public const string DishNotAvailable = "El plato especificado no está disponible";
        public const string OrderInPreparation = "No se puede modificar una orden que ya está en preparación";
        public const string OrderItemNotFound = "Item no encontrado en la orden";
        public const string InvalidStatus = "El estado especificado no es válido";
    }
}
