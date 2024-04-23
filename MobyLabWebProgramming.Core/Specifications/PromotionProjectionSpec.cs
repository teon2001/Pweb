using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class PromotionProjectionSpec : BaseSpec<PromotionProjectionSpec, Promotion, PromotionDTO>
    {
        protected override Expression<Func<Promotion, PromotionDTO>> Spec => promotion => new PromotionDTO
        {
            Id = promotion.Id,
            Code = promotion.Code,
            ValidFrom = promotion.ValidFrom,
            ValidTo = promotion.ValidTo,
            DiscountPercentage = promotion.DiscountPercentage,
        };

        public PromotionProjectionSpec(Guid id) : base(id)
        {
            Query.Where(item => item.Id == id);
        }

        public PromotionProjectionSpec()
        {
        }
    }
}
