using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class DishUpdateRequest
    {
        [Required(ErrorMessage = "El nombre del plato es obligatorio")]
        [MaxLength(225, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
        public string name { get; set; }

        public string? description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int category { get; set; }
       
        public string? image { get; set; }
        
        
        public bool isActive { get; set; }
    }
}
