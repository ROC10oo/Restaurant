using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DishNotAvailableOrNotExistsException: Exception
    {
        public DishNotAvailableOrNotExistsException() : base(ErrorMessages.DishNotAvailable) { }
    }
}
