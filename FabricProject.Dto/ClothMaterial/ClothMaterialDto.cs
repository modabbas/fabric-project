using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.ClothMaterial
{
   public class ClothMaterialDto
    {
        public int ClothId { get; set; }
        public string Cloth { get; set; }

        public int MaterailId { get; set; }
        public string Material { get; set; }

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }
    }
}
