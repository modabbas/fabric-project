using FabricProject.Dto.Cloth;
using FabricProject.Dto.Color;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabricProject.Dto.Lab
{
    public class LabDto
    {
        public int Id { get; set; }

        public int? ClothId { get; set; }

        public int? NewColorId { get; set; }

        public int SuccuessPercent { get; set; }

        public bool IsAdd { get; set; } = false;

        public IList<ColorDto> ColorsDto { get; set; }
        public IList<ClothDto> ClothsDto { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public string ColorName { get; set; }
        public string ClothName { get; set; }
        public LabDto()
        {
            CreatedAt = DateTime.Now;
            EditedAt = DateTime.Now;
        }


    }
}
