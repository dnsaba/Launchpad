using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Models.Domain
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
    }
}
