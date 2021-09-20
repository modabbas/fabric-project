using FabrciProject.IData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin,Laboratory")]
    public class WareHouseController:Controller
    {
        private IColorRepository ColorRepository { get; }

        private IMaterialRepository MaterialRepository { get; }

        public WareHouseController(IColorRepository colorRepository,IMaterialRepository materialRepository)
        {

            this.ColorRepository = colorRepository;
            this.MaterialRepository = materialRepository;
        }
        public IActionResult Index()
        {
            var vm = new WareHouseViewModel()
            {
                GetColors = ColorRepository.GetColors(),
                GetMaterial=MaterialRepository.GetMaterials(),
            };
            return View(vm);
        }
        

    }
}
