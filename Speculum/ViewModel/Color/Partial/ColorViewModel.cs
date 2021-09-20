
using FabricProject.Dto.Color;
using Speculum.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Color
{
    public class ColorViewModel : FabricProjectViewModel
    {
        public int Id { get; set; }

        public IEnumerable<ColorDto> GetColors { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }

        public ActiveViewModel Active { get; set; }


    }
}
