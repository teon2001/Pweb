using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class ReviewDTO
    {
        public Guid Id { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public Guid FoodId { get; set; } = default!;
        public int Rating { get; set; } = default!;
        public string? Comment { get; set; } = default!;
    }

}
