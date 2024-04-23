using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces
{
    public interface IFoodService
    {
        Task<ServiceResponse<FoodDTO>> GetFoodItemById(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<FoodDTO>>> GetFoodItems(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddFoodItem(FoodDTO foodItem, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateFoodItem(FoodDTO foodItem, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeleteFoodItem(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddIngredientToFood(Guid foodId, Guid ingredientId);
        Task<ServiceResponse> RemoveIngredientFromFood(Guid foodId, Guid ingredientId);
    }
}
