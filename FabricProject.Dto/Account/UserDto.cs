using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Account
{
   public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string RoleAr { get; set; }
        public int RoleId { get; set; }
        public DateTime AccountDate { get; set; }
        public string ErrorCode { get; set; }
        public bool Testing { get; set; } = false;
    }
}
