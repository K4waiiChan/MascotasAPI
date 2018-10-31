using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace PetsAPI.Models
{
    public class User : IdentityUser<string>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
