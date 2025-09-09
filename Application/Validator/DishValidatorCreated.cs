using Application.Models.Request;
using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator
{
    public class DishValidatorCreated
    {
        public void Validate(DishRequest dish)
        {
            if (string.IsNullOrWhiteSpace(dish.name))
                throw new Exception(DishErrorMessages.EmptyName);
            if (dish.price <= 0)
                throw new Exception(DishErrorMessages.InvalidPrice);
        }
    }
}
