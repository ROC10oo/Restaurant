using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDish
{
    public interface IValidatorUpdateDish
    {
        void Validate(DishRequestUpdate dish);
    }
}
