using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Cloth
{
   public class ClothDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public bool isAdd { get; set; } = false;
    }
}
