using Application.Interfaces.IDish;
using Application.Models.Request;
using Domain.ErrorsMessages;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator
{
    public class DishValidatorUpdate: IValidatorUpdateDish
    {
        public void Validate(DishUpdateRequest dish)
        {
            if (dish.price <= 0)
                throw new DishInvalidPriceException();
        }
    }
}
