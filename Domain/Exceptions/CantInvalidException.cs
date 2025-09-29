using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CantInvalidException: Exception
    {
        public CantInvalidException() : base(ErrorMessages.InvalidCant) { }
    }
}
