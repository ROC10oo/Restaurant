using Application.Exceptions;
using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Interfaces.IOrderItem;
using Application.Models.Response;
using Domain.ErrorsMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesDish
{
    public class DeleteDishService : IDeleteDishService
    {
        private readonly IDishQuery _dishQuery;
        private readonly IDishCommand _dishCommand;
        private readonly IOrderItemQuery _orderItemQuery;

        public DeleteDishService(IDishQuery dishQuery, IDishCommand dishCommand, IOrderItemQuery orderItemQuery)
        {
            _dishQuery = dishQuery;
            _dishCommand = dishCommand;
            _orderItemQuery = orderItemQuery;
        }

        public async Task<DishResponse> DeleteDish(Guid id)
        {
            var dish = await _dishQuery.GetDishById(id);

            if (dish == null)
            {
                throw new DishNotFoundException();
            }


            //Verifico si el plato es usado en algun item de las ordenes
            bool UsedDish = await _orderItemQuery.ExistsByDishId(id);

            if (UsedDish) //Si es usado en alguna order entonces lanzo exception 
            {
                throw new DishUsedInOrderException();
            }

            //si resulta false, marco la disponibilidad del plato como falso
            dish.Available = false;

            await _dishCommand.UpdateDish(dish); // y lo termino de modificar con el update


            return new DishResponse
            {
                id = dish.DishId,
                name = dish.Name,
                description = dish.Description,
                price = dish.Price,
                Category = new GenericResponse { Id = dish.CategoryId, Name = dish.Category.Name },
                isActive = dish.Available,
                image = dish.ImageUrl,
                createdAt = dish.CreateDate,
                updatedAt = dish.UpdateDate,
            };
        }
    }
}
