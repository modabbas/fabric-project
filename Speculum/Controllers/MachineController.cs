using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Data.Repositories;
using FabricProject.Dto.Machine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Machine;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin,Machine")]
    public class MachineController : Controller
    {
        IMachineRepository MachineRepository;

        public MachineController(IMachineRepository machineRepository)
        {
            this.MachineRepository = machineRepository;
        }

        // GET: Machine
        public ActionResult Index()
        {
            var Machine = new MachineViewModel()
            {
                GetMachines = MachineRepository.Get(),
            };
            return View(Machine);
        }

        [HttpPost]
        public ActionResult SetMachine(MachineViewModel machine)
        {
            var setResult = MachineRepository.Set(new MachineDto()
            {
                Id =machine.Id,
                Name =machine.Name,
                Number =machine.Number,
                MachineType= machine.machineType,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now
            });
            return Json(setResult);
        }

        [HttpDelete]
        public IActionResult RemoveMachine(int id)
        {
            var result = MachineRepository.Remove(id);
            return Json(result);
        }
    }
}