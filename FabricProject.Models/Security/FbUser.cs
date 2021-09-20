using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Models.Security
{
  public class FbUser: IdentityUser<int>
    {
        public DateTime AccountDate { get; set; }
    }
}
