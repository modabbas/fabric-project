using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Cloth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Cloth;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin,Laboratory")]
    public class ClothController : Controller
    {
        private IClothRespository ClothRepository { get; }
        private IMaterialRepository MaterialRepository { get; }
        private IClothMaterialRepository ClothMaterialRepository { get; }
        public ClothController(IClothRespository clothRepository, IMaterialRepository materialRepository, IClothMaterialRepository clothMaterialRepository)
        {

            this.ClothRepository = clothRepository;
            this.MaterialRepository = materialRepository;
            this.ClothMaterialRepository = clothMaterialRepository;
        }
        public IActionResult Index()
        {
            var vm = new ClothViewModel()
            {
                GetCloths = ClothRepository.GetCloths(),
            };
            return View(vm);
        }

        public IActionResult ClothMaterials()
        {
            var vm = new ClothViewModel()
            {
                GetCloths = ClothRepository.GetCloths(),
                GetMaterials = MaterialRepository.GetMaterials(),
                GetClothMaterial = ClothMaterialRepository.GetClothMaterials()
            };
            return View(vm);
        }

        public IActionResult GetCloth(int id)
        {
            return Json(ClothRepository.GetById(id));
        }

        [HttpPost]

        public IActionResult SetCloth(ClothViewModel clothViewModel)
        {
            var newColor = ClothRepository.SetCloth(new ClothDto()
            {
                Id = clothViewModel.Id,
                Name = clothViewModel.Name,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now,

            });
            return Json(newColor);
        }

        public IActionResult RemoveCloth(int id)
        {
            var removeClothResult = ClothRepository.RemoveCloth(id);
            return Json(removeClothResult);
        }



        //Cloth Material
        [HttpPost]
        public IActionResult ClothMaterials(ClothViewModel customerViewModel)
        {
            var setResult = ClothMaterialRepository.SetClothMaterial(new FabricProject.Dto.ClothMaterial.ClothMaterialDto()
            {
                Id = customerViewModel.Id,
                MaterailId = customerViewModel.MaterialId,
                ClothId = customerViewModel.ClothId,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now

            });
            if (setResult)
            {
                return RedirectToAction("ClothMaterials");
            }
            return RedirectToAction("ClothMaterials");
        }

        public IActionResult RemoveClothMaterials(int id)
        {
            ClothMaterialRepository.RemoveClothMaterial(id);
            return RedirectToAction("ClothMaterials");
        }

    }
}
