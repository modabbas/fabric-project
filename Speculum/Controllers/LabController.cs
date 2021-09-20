using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Lab;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speculum.ViewModel;
using Speculum.ViewModel.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
    
    [Authorize(Roles = "Admin,Laboratory")]
    public class LabController : Controller
    {
        private ILabRepository LabRepository { get; }

        private IClothRespository ClothRepository { get; }

        private IColorRepository ColorRepository { get; }


        public LabController(ILabRepository labRepository, IClothRespository clothRespository, IColorRepository colorRepository)
        {

            this.LabRepository = labRepository;
            this.ColorRepository = colorRepository;
            this.ClothRepository = clothRespository;
        }
        public IActionResult Index()
        {
            var resultofsamples = new LabViewModel()
            {
                GetSample = LabRepository.GetSamples(),
                GetCloths = ClothRepository.GetCloths(),
                GetColors = ColorRepository.GetColors(),
            };
            return View(resultofsamples);
        }
        [HttpPost]
        public IActionResult SetSample(LabViewModel labViewModel)
        {
            var setResult = LabRepository.SetSample(new LabDto()
            {
                Id = labViewModel.Id,
                ClothId = labViewModel.ClothId,
                NewColorId = labViewModel.NewColorId,
                SuccuessPercent = labViewModel.SuccessPercent,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now,


            });
            return Json(setResult);
        }


        [HttpPut]
        public IActionResult EditSample(LabViewModel labViewModel)
        {
            var setResult = LabRepository.SetSample(new LabDto()
            {
                Id = labViewModel.Id,
                ClothId = labViewModel.ClothId,
                NewColorId = labViewModel.NewColorId,
                SuccuessPercent = labViewModel.SuccessPercent,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now,
            });
            return Json(setResult);
        }


        [HttpDelete]
        public IActionResult RemoveSample(int id)
        {
            var removeColorResult = LabRepository.RemoveSample(id);
            return Json(removeColorResult);
        }

        [HttpGet]
        private IActionResult FillLabFormViewModel(LabFormViewModel labFormViewModel)
        {
            if (labFormViewModel == null)
            {
                labFormViewModel = new LabFormViewModel();
            }
            labFormViewModel.Colors = ColorRepository.GetColors(null)
                .Select(colorDto => new SelectListItem()
                {
                    Value = colorDto.Id.ToString(),
                    Text = colorDto.Name
                });
            labFormViewModel.Cloths = ClothRepository
                .GetCloths()
                 .Select(clothexit => new SelectListItem()
                 {
                     Value = clothexit.Id.ToString(),
                     Text = clothexit.Name
                 });
            return View(labFormViewModel);
        }

    }
}

