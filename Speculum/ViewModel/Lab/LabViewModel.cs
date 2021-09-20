using FabricProject.Dto.Cloth;
using FabricProject.Dto.Color;
using FabricProject.Dto.Lab;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Lab
{
    public class LabViewModel
    {
        public IEnumerable<LabDto> GetSample { get; set; }

        public IEnumerable<ClothDto> GetCloths { get; set; }

        public IEnumerable<ColorDto> GetColors { get; set; }
        public IEnumerable<SelectListItem> Clothes { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }

        public int Id { get; set; }


        public int NewColorId { get; set; }


        public int ClothId { get; set; }


        public int SuccessPercent { get; set; }
        public ActiveViewModel Active { get; set; }


    }
}
