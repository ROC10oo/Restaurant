using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ErrorsMessages
{
    public class DishErrorMessages
    {
        public const string EmptyName = "El nombre del plato es obligatorio";
        public const string InvalidPrice = "El precio debe ser mayor a cero";
        public const string DishAlreadyExists = "Ya existe un plato con ese nombre";
        public const string CategoryNotExists = "Debe existir una categoría";
        public const string DishNotExists = "Plato no encontrado";
        public const string InvalidParameter = "Parámetros de ordenamiento inválidos";
    }
}
