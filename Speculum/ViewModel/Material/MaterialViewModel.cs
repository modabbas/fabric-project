using FabricProject.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Material
{
    public class MaterialViewModel
    {
        public IEnumerable<MaterialDto> GetMaterials { get; set; }


        public int Id { get; set; }

        public string Name { get; set; }

        public string Amount
        {
            get; set;
        }
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }
        public ActiveViewModel Active { get; set; }

    }
}
