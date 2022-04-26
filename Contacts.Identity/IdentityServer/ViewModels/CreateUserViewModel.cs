using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
    }
}
