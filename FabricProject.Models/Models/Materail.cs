using FabricProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Materail : BaseModel
    {
        public string Name { get; set; }

        public string Amount { get; set; }

        public ICollection<ClothMaterail> ClothMaterails { get; set; }
      
        public Materail()
        {
            ClothMaterails = new List<ClothMaterail>();
            
        }
    }
}
