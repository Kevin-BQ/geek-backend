using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class RoleUserAplication : IdentityUserRole<int>
    {
        public UserAplication UserAplication { get; set; }
        public RoleAplication RoleAplication { get; set; }
    }
}
