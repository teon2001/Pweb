using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Ingredient: BaseEntity
    {
        public string Name { get; set; } = default!;
        public bool IsAllergen { get; set; }
        public ICollection<Ingredient_Food> IngredientsFoods { get; set; } = new List<Ingredient_Food>();
    }
}
