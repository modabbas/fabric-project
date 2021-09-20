using Exceptions;
using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto.Color;
using FabricProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Speculum.Controllers
{

    [Authorize(Roles = "Admin,Laboratory")]
    public class ColorController : Controller
    {

        private IColorRepository ColorRepository { get; }

        public ColorController(IColorRepository colorRepository)
        {

            this.ColorRepository = colorRepository;
        }

        public IActionResult GetColor(int id)
        {
            return Json(ColorRepository.GetById(id));
        }

        [HttpGet]


        public IActionResult Index()
        {
            var vm = new ColorViewModel()
            {
                GetColors = ColorRepository.GetColors(),
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult SetColor(ColorViewModel colorViewModel)
        {
            var newColor = ColorRepository.SetColor(new ColorDto()
            {
                Id = colorViewModel.Id,
                Name = colorViewModel.Name,
                Amount = colorViewModel.Amount,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now,

            });

            return Json(newColor);
        }


        [HttpDelete]
        public IActionResult RemoveColor(int id)
        {
            var removeColorResult = ColorRepository.RemoveColor(id);
            return Json(removeColorResult);
        }

    }
}
