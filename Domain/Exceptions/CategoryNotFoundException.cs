using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException() : base(DishErrorMessages.CategoryNotExists) { }
    }
}
