using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class FoodProjectionSpec : BaseSpec<FoodProjectionSpec, Food, FoodDTO>
    {
        protected override Expression<Func<Food, FoodDTO>> Spec => item => new FoodDTO
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            ImageUrl = item.ImageUrl
        };

        // Constructor for specific food item by ID
        public FoodProjectionSpec(Guid id) : base(id)
        {
            Query.Where(item => item.Id == id); 
        }

        // Default constructor for general queries
        public FoodProjectionSpec()
        {
            // No specific query conditions needed here
        }
        public FoodProjectionSpec(string? search)
        {
            search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

            if (search == null)
            {
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); 
        }
    }
}
