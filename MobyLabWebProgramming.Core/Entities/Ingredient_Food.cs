using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Ingredient_Food
    {
        public Guid FoodId { get; set; } = default!;
        public Food Food { get; set; } = default!;

        public Guid IngredientId { get; set; } = default!;
        public Ingredient Ingredient { get; set; } = default!;
    }
}
