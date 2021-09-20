using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.OrderMaterials
{
  public  class OrderMaterialsDto
    {
        public int MaterialId { get; set; }
        public int ClothMaterialId { get; set; }

        public int Id { get; set; }

        public int Amount { get; set; }

        public string Material { get; set; }
        
        public int weight { get; set; }

        public int OrderDetailCustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

    }
}
