using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ListUserDto
    {
        public string Username { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
