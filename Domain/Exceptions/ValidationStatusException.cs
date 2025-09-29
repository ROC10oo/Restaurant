using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidationStatusException : Exception
    {
        public ValidationStatusException(): base (ErrorMessages.InvalidStatus) {}
    }
}
