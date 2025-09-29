using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidDeliveryException : Exception
    {
        public InvalidDeliveryException() : base(ErrorMessages.InvalidDelivery) { }
    }
}
