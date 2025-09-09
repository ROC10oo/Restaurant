using Application.Models.Request;
using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator
{
    public class DishValidatorUpdate
    {
        public void Validate(DishRequestUpdate dish)
        {
            if (dish.price <= 0)
                throw new Exception(DishErrorMessages.InvalidPrice);
        }
    }
}
