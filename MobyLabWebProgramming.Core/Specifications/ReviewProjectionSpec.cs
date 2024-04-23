using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class ReviewProjectionSpec : BaseSpec<ReviewProjectionSpec, Review, ReviewDTO>
    {
        protected override Expression<Func<Review, ReviewDTO>> Spec => review => new ReviewDTO
        {
            Id = review.Id,
            UserId = review.UserId,
            FoodId = review.FoodId,
            Rating = review.Rating,
            Comment = review.Comment
        };

        public ReviewProjectionSpec(Guid id) : base(id)
        {
            Query.Where(item => item.Id == id);
        }

        public ReviewProjectionSpec()
        {
        }

        // ... alți constructori și metode pentru filtre specifice sau criterii de căutare
        // Constructor for paginated results and potentially filtering
        //public ReviewProjectionSpec(PaginationSearchQueryParams queryParams)
        //{
        //    if (!string.IsNullOrWhiteSpace(queryParams.Search))
        //    {
        //        string searchPattern = $"%{queryParams.Search}%";
        //        Query.Where(review => EF.Functions.Like(review.Comment, searchPattern)); // Example of simple search in comments
        //    }

        //    Query
        //        .Skip(queryParams.PageIndex * queryParams.PageSize)
        //        .Take(queryParams.PageSize)
        //        .Select(Projection); // Applying the projection
        //}
    }
}
