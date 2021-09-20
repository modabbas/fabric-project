using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FabricProject.Models.Security
{
  public  class FbUserToken: IdentityUserToken<int>
    {
        [Key]
        public int Id { get; set; }
    }
}
