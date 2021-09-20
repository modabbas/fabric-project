using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FabricProject.Models.Security
{
   public class FbRole: IdentityRole<int>
    {
        public string NameAr { get; set; }
    }
}
