using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class DishRequestUpdate
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int category { get; set; }
        public string image { get; set; }
        public bool isActive { get; set; }
    }
}
