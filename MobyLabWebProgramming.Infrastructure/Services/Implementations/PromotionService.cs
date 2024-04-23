using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations
{
    public class PromotionService : IPromotionService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public PromotionService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<PromotionDTO>> GetPromotionById(Guid id, CancellationToken cancellationToken = default)
        {
            var promotion = await _repository.GetAsync(new PromotionProjectionSpec(id), cancellationToken);
            if (promotion == null)
                return ServiceResponse<PromotionDTO>.FromError(CommonErrors.PromotionNotFound);

            return ServiceResponse<PromotionDTO>.ForSuccess(promotion);
        }

        public async Task<ServiceResponse<PagedResponse<PromotionDTO>>> GetPromotions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var promotions = await _repository.PageAsync(pagination, new PromotionProjectionSpec(), cancellationToken);
            return ServiceResponse<PagedResponse<PromotionDTO>>.ForSuccess(promotions);
        }

        public async Task<ServiceResponse> AddPromotion(PromotionDTO promotion, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser.Role != UserRoleEnum.Admin)
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can add promotions."));

            var newPromotion = new Promotion
            {
                Code = promotion.Code,
                ValidFrom = promotion.ValidFrom,
                ValidTo = promotion.ValidTo,
                DiscountPercentage = promotion.DiscountPercentage
            };

            await _repository.AddAsync(newPromotion, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdatePromotion(PromotionDTO promotion, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser.Role != UserRoleEnum.Admin)
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can update promotions."));

            var existingPromotion = await _repository.GetAsync<Promotion>(new PromotionProjectionSpec(promotion.Id), cancellationToken);
            if (existingPromotion == null)
                return ServiceResponse.FromError(CommonErrors.PromotionNotFound);

            existingPromotion.Code = promotion.Code;
            existingPromotion.ValidFrom = promotion.ValidFrom;
            existingPromotion.ValidTo = promotion.ValidTo;
            existingPromotion.DiscountPercentage = promotion.DiscountPercentage;
            await _repository.UpdateAsync<Promotion>(existingPromotion, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeletePromotion(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (requestingUser.Role != UserRoleEnum.Admin)
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can delete promotions."));

            await _repository.DeleteAsync<Promotion>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }
    }

}
