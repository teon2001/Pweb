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
    public interface IPromotionService
    {
        Task<ServiceResponse<PromotionDTO>> GetPromotionById(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<PromotionDTO>>> GetPromotions(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddPromotion(PromotionDTO promotion, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdatePromotion(PromotionDTO promotion, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeletePromotion(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    }

}
