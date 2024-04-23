using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class IngredientProjectionSpec : BaseSpec<IngredientProjectionSpec, Ingredient, IngredientDTO>
    {
        protected override Expression<Func<Ingredient, IngredientDTO>> Spec => ingredient => new IngredientDTO
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            IsAllergen = ingredient.IsAllergen,
        };

        public IngredientProjectionSpec(Guid id) : base(id)
        {
            Query.Where(item => item.Id == id); // Assuming base(id) sets this condition
        }

        public IngredientProjectionSpec()
        {
        }

        public IngredientProjectionSpec(string? search, Guid foodId)
        {

            Query.Where(i => i.IngredientsFoods.Any(iif => iif.Food.IngredientsFoods.Any(uf => uf.FoodId == foodId)));
        }
    }
}
