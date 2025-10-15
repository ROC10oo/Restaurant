using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EmptyDeliveryToException: Exception
    {
        public EmptyDeliveryToException() : base(ErrorMessages.EmptyDeliveryTo) { }
    }
}
