using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Material;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin,Laboratory")]
    public class MaterialController:Controller
    {
        private IMaterialRepository MaterialRepository { get; }

        public MaterialController(IMaterialRepository materialRepository)
        {

            this.MaterialRepository = materialRepository;
        }
        public IActionResult Index()
        {
            var vm = new MaterialViewModel()
            {
                GetMaterials = MaterialRepository.GetMaterials(),
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(MaterialViewModel materialViewModel)
        {

            if ( materialViewModel.Name == null ||
                 Convert.ToInt32(materialViewModel.Amount) == 0 ||
                 Convert.ToInt32(materialViewModel.Amount) < 0 )
            {
                return RedirectToAction("Index");
            }
            var setResult = MaterialRepository.SetMaterial(new MaterialDto()
            {
                Id = materialViewModel.Id,
                Name = materialViewModel.Name,
                Amount=materialViewModel.Amount,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now

            });
            if (setResult)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult MaterialRemove(int id)
        {
            MaterialRepository.RemoveMaterial(id);

            return RedirectToAction("Index");
        }
    }
}
