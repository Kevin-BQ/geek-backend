using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class UserAplication: IdentityUser<int>
    {
        public string LastName { get; set; }
        public string Names { get; set; }
        public ICollection<RoleUserAplication> UserRole { get; set; }

    }
}
