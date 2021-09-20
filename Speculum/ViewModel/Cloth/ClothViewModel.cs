using FabricProject.Dto.Cloth;
using FabricProject.Dto.ClothMaterial;
using FabricProject.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Cloth
{
    public class ClothViewModel
    {
        public IEnumerable<ClothDto> GetCloths { get; set; }

        public IEnumerable<MaterialDto> GetMaterials { get; set; }

        public IEnumerable<ClothMaterialDto> GetClothMaterial { get; set; }

        public int MaterialId { get; set; }
        public int ClothId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }
        public ActiveViewModel Active { get; set; }
    }
}
