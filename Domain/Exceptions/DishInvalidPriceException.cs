using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DishInvalidPriceException : Exception
    {
        public DishInvalidPriceException() : base(DishErrorMessages.InvalidPrice) { }
    }
}
