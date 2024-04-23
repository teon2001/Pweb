using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class IngredientDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsAllergen { get; set; }
    }
}
