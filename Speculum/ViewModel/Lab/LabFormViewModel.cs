using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Lab
{
    public class LabFormViewModel
    {
        public int Id { get; set; }
        public IEnumerable<SelectListItem> Colors { get; set; }

        public IEnumerable<SelectListItem> Cloths { get; set; }

        public string OldColor { get; set; }

        public int NewColorId { get; set; }

        public int SuccessPercent { get; set; }
        public ActiveViewModel Active { get; set; }


    }
}
