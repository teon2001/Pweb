using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MobyLabWebProgramming.Infrastructure.Database;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public IngredientService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<IngredientDTO>> GetIngredientById(Guid id, CancellationToken cancellationToken = default)
        {
            var ingredient = await _repository.GetAsync(new IngredientProjectionSpec(id), cancellationToken);
            if (ingredient == null)
                return ServiceResponse<IngredientDTO>.FromError(CommonErrors.IngredientNotFound);

            return ServiceResponse<IngredientDTO>.ForSuccess(ingredient);
        }

        public async Task<ServiceResponse<PagedResponse<IngredientDTO>>> GetIngredients(PaginationSearchQueryParams pagination, Guid foodId, CancellationToken cancellationToken = default)
        {
            var ingredients = await _repository.PageAsync(pagination, new IngredientProjectionSpec(pagination.Search, foodId), cancellationToken);
            return ServiceResponse<PagedResponse<IngredientDTO>>.ForSuccess(ingredients);
        }

        public async Task<ServiceResponse> AddIngredient(IngredientDTO ingredient, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser.Role != UserRoleEnum.Admin)
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can add ingredients."));

            var newIngredient = new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                IsAllergen = ingredient.IsAllergen
            };

            await _repository.AddAsync(newIngredient, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateIngredient(IngredientDTO ingredient, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser.Role != UserRoleEnum.Admin)
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can update ingredients."));

            var existingIngredient = await _repository.GetAsync<Ingredient>(new IngredientProjectionSpec(ingredient.Id), cancellationToken);
            if (existingIngredient == null)
                return ServiceResponse.FromError(CommonErrors.IngredientNotFound);

            existingIngredient.Name = ingredient.Name;
            existingIngredient.IsAllergen = ingredient.IsAllergen;
            await _repository.UpdateAsync(existingIngredient, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteIngredient(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser.Role != UserRoleEnum.Admin)
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can delete ingredients."));

            await _repository.DeleteAsync<Ingredient>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }
    }

}
