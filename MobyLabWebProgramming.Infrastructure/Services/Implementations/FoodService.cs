using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using MobyLabWebProgramming.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class FoodService : IFoodService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public FoodService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<FoodDTO>> GetFoodItemById(Guid id, CancellationToken cancellationToken = default)
        {
            var foodItem = await _repository.GetAsync(new FoodProjectionSpec(id), cancellationToken);
            return foodItem != null ?
                ServiceResponse<FoodDTO>.ForSuccess(foodItem) :
                ServiceResponse<FoodDTO>.FromError(CommonErrors.FoodItemNotFound);
        }

        public async Task<ServiceResponse<PagedResponse<FoodDTO>>> GetFoodItems(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var foodItems = await _repository.PageAsync(pagination, new FoodProjectionSpec(pagination.Search), cancellationToken);
            return ServiceResponse<PagedResponse<FoodDTO>>.ForSuccess(foodItems);
        }

        public async Task<ServiceResponse> AddFoodItem(FoodDTO foodItem, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {

            var newItem = new Food { 
                Id = foodItem.Id,
                Name = foodItem.Name, 
                Description = foodItem.Description,
                Price = foodItem.Price,
                ImageUrl = foodItem.ImageUrl,
                               
            };
            await _repository.AddAsync(newItem, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateFoodItem(FoodDTO foodItem, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            // Assume validation and mapping are done here
            var existingItem = await _repository.GetAsync<Food>(new FoodProjectionSpec(foodItem.Id), cancellationToken);
            if (existingItem == null)
                return ServiceResponse.FromError(CommonErrors.FoodItemNotFound);

            existingItem.Name = foodItem.Name;
            existingItem.Price = foodItem.Price;
            existingItem.Description = foodItem.Description;
            await _repository.UpdateAsync(existingItem, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteFoodItem(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync<Food>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> AddIngredientToFood(Guid foodId, Guid ingredientId)
        {
            var food = await _repository.GetAsync<Food>(new FoodProjectionSpec(foodId));
            if (food == null)
                return ServiceResponse.FromError(CommonErrors.FoodItemNotFound);

            var ingredient = await _repository.GetAsync<Ingredient>(new IngredientProjectionSpec(ingredientId));
            if (ingredient == null)
                return ServiceResponse.FromError(CommonErrors.IngredientNotFound);

            food.IngredientsFoods.Add(new Ingredient_Food 
            { 
                FoodId = foodId, 
                IngredientId = ingredientId,
                Food = food,
                Ingredient = ingredient
            });
            await _repository.UpdateAsync(food);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> RemoveIngredientFromFood(Guid foodId, Guid ingredientId)
        {
            var food = await _repository.GetAsync<Food>(new FoodProjectionSpec(foodId));
            if (food == null)
                return ServiceResponse.FromError(CommonErrors.FoodItemNotFound);

            var ingredient = await _repository.GetAsync<Ingredient>(new IngredientProjectionSpec(ingredientId));
            if (ingredient == null)
                return ServiceResponse.FromError(CommonErrors.IngredientNotFound);

            var ingredientFood = food.IngredientsFoods.FirstOrDefault(i => i.FoodId == foodId && i.IngredientId == ingredientId);
            if (ingredientFood == null)
                return ServiceResponse.FromError(CommonErrors.IngredientNotInFood);

            food.IngredientsFoods.Remove(ingredientFood);
            await _repository.UpdateAsync(food);

            return ServiceResponse.ForSuccess();
        }   
    }

}
