using FabricProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Models.Models
{
    public class Cloth : BaseModel
    {
        public string Name { get; set; }
        
         public ICollection<ClothMaterail> ClothMaterails { get; set; }

        public ICollection<CustomerOrder> CustomerOrders { get; set; }

        public ICollection<Lab> LabCloth { get; set; }
        public Cloth()
        {
        }
    }
}
