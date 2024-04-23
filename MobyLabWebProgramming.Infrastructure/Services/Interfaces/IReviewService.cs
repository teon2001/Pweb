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
    public interface IReviewService
    {
        Task<ServiceResponse<ReviewDTO>> GetReviewById(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<PagedResponse<ReviewDTO>>> GetReviews(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
        Task<ServiceResponse> AddReview(ReviewDTO review, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateReview(ReviewDTO review, UserDTO requestingUser, CancellationToken cancellationToken = default);
        Task<ServiceResponse> DeleteReview(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    }

}
