using FabricProject.Dto.Color;
using FabricProject.Dto.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Warehouse
{
    public class WareHouseViewModel
    {
        public IEnumerable<ColorDto> GetColors { get; set; }

        public IEnumerable<MaterialDto> GetMaterial { get; set; }
        public ActiveViewModel Active { get; set; }


    }
}
