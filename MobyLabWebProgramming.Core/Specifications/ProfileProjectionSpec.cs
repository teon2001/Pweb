using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class ProfileProjectionSpec : BaseSpec<ProfileProjectionSpec, Profile, ProfileDTO>
    {
        protected override Expression<Func<Profile, ProfileDTO>> Spec => item => new ProfileDTO
        {
            Id = item.Id,
            FirstName = item.FirstName,
            LastName = item.LastName,
            DateOfBirth = item.DateOfBirth,
            Bio = item.Bio,
            UserId = item.UserId,

        };

        // Constructor for specific food item by ID
        public ProfileProjectionSpec(Guid id)
        {
            Query.Where(item => item.UserId == id);
        }

        // Default constructor for general queries
        public ProfileProjectionSpec()
        {
            // No specific query conditions needed here
        }
        public ProfileProjectionSpec(string? search)
        {
            search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

            if (search == null)
            {
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.FirstName, searchExpr));
        }
    }
}
