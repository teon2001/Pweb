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
    public interface IIngredientService
    {
        Task<ServiceResponse<IngredientDTO>> GetIngredientById(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<IngredientDTO>>> GetIngredients(PaginationSearchQueryParams pagination, Guid foodId, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddIngredient(IngredientDTO ingredient, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateIngredient(IngredientDTO ingredient, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeleteIngredient(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    }

}
