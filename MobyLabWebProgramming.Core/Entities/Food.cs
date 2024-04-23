using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Food : BaseEntity
    {
        public string? Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string? ImageUrl { get; set; } = default!;
        public ICollection<Review> Reviews { get; set; } = default!;
        public ICollection<Ingredient_Food> IngredientsFoods { get; set; } = new List<Ingredient_Food>();
    }
}
