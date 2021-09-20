using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Account
{
   public class LoginDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Role { get; set; }
    }
}
