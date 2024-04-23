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
    public class ReviewService : IReviewService
    {
        private readonly IRepository<WebAppDatabaseContext> _repository;

        public ReviewService(IRepository<WebAppDatabaseContext> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<ReviewDTO>> GetReviewById(Guid id, CancellationToken cancellationToken = default)
        {
            var review = await _repository.GetAsync(new ReviewProjectionSpec(id), cancellationToken);
            if (review == null)
                return ServiceResponse<ReviewDTO>.FromError(CommonErrors.ReviewNotFound);

            return ServiceResponse<ReviewDTO>.ForSuccess(review);
        }

        public async Task<ServiceResponse<PagedResponse<ReviewDTO>>> GetReviews(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
        {
            var reviews = await _repository.PageAsync(pagination, new ReviewProjectionSpec(), cancellationToken);
            return ServiceResponse<PagedResponse<ReviewDTO>>.ForSuccess(reviews);
        }

        public async Task<ServiceResponse> AddReview(ReviewDTO review, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (!UserCanModify(requestingUser))
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Insufficient permissions to add reviews."));

            var newReview = new Review
            {
                UserId = requestingUser.Id,
                FoodId = review.FoodId,
                Rating = review.Rating,
                Comment = review.Comment
            };

            await _repository.AddAsync(newReview, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> UpdateReview(ReviewDTO review, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (!UserCanModify(requestingUser))
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Insufficient permissions to update reviews."));

            var existingReview = await _repository.GetAsync<Review>(new ReviewProjectionSpec(review.Id), cancellationToken);
            if (existingReview == null)
                return ServiceResponse.FromError(CommonErrors.ReviewNotFound);

            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;
            await _repository.UpdateAsync(existingReview, cancellationToken);

            return ServiceResponse.ForSuccess();
        }

        public async Task<ServiceResponse> DeleteReview(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
        {
            if (!UserCanModify(requestingUser))
                return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Insufficient permissions to delete reviews."));

            await _repository.DeleteAsync<Review>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        private bool UserCanModify(UserDTO user)
        {
            // Assume we are checking some permission or role
            return user.Role == UserRoleEnum.Admin;
        }
    }

}
