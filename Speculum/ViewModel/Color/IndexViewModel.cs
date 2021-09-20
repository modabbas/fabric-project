using FabricProject.Dto.Color;
using Speculum.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Color
{
    public class IndexViewModel:FabricProjectViewModel
    {
      
        public IEnumerable<ColorDto> Colors { get; set; }
        public IndexViewModel()
        {
            PageTitle = "Colors";
        }
        public ActiveViewModel Active { get; set; }

    }
}
