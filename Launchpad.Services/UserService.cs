using Launchpad.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.Services
{
    public class UserService
    {
        public int Create(NewUser model)
        {
            int id = 0;
            string salt;
            string passwordHash;

            string password = model.BasicPass;


        }
    }
}
