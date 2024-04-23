using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Review:BaseEntity
    {
        public Guid UserId { get; set; } = default!;
        public User User { get; set; } = default!;
        public Guid FoodId { get; set; } = default!;
        public Food Food { get; set; } = default!;
        public int Rating { get; set; } = default!;
        public string? Comment { get; set; } = default!;


    }
}
