using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Profile :  BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; } = default!;

        // Proprietatea FK pentru relația one-to-one cu User
        public Guid UserId { get; set; } = default!;

        // Proprietatea de navigație pentru relația one-to-one cu User
        public User User { get; set; } = default!;
    }
}
