using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Material
{
    public class MaterialDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Amount
        {
            get; set;
        }
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }
    }
}
