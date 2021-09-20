using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Data.Repositories;
using FabricProject.Dto.CustomerOrderDetailsMachine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Speculum.ViewModel;
using Speculum.ViewModel.CustomerOrderDetailMachine;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerOrderDetailsMachineController : Controller
    {
        private ICustomerOrderDetailsMachineRepository CustomerOrderDetailMachineRepository { get; }
        private ICustomerOrderDetailsRepository CustomerOrderDetailsRepository { get; }
        private IMachineRepository MachineRepository { get; }


        public CustomerOrderDetailsMachineController(ICustomerOrderDetailsRepository customerOrderDetailsRepository,
            ICustomerOrderDetailsMachineRepository customerOrderDetailMachineRepository, IMachineRepository machineRepository)
        {

            CustomerOrderDetailsRepository = customerOrderDetailsRepository;
            MachineRepository = machineRepository;
            CustomerOrderDetailMachineRepository = customerOrderDetailMachineRepository;
        }
        public IActionResult Index()
        {
            var result = new CustomerOrderDetailMachineViewModel()
            {
                GetMachineDto = MachineRepository.Get(),
                GetCustomerOrderDetailsDto = CustomerOrderDetailsRepository.GetCustomerOrderDetails(),
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Index(CustomerOrderDetailMachineViewModel customerOrderDetailMachineViewModel)
        {
            var setResult = CustomerOrderDetailMachineRepository.SetOrderMachine(new CustomerOrderDetailsMachineDto()
            {
                Id = customerOrderDetailMachineViewModel.Id,
                CustomerOrderDetailsId = customerOrderDetailMachineViewModel.CustomerOrderDetailId.Value,
                MachineId = customerOrderDetailMachineViewModel.MachineId.Value,
                AmountWater = customerOrderDetailMachineViewModel.AmountWater,

            });
            if (setResult)
            {
                return View("Index");
            }
            return View("Index");
        }

        [HttpGet]
        private IActionResult FillCustomerOrderMachineFormViewModel(CustomerOrderDetailMachineViewModel customerOrderDetailMachineViewModel)
        {
            if (customerOrderDetailMachineViewModel == null)
            {
                customerOrderDetailMachineViewModel = new CustomerOrderDetailMachineViewModel();
            }
            customerOrderDetailMachineViewModel.Machines = MachineRepository.Get(null)
                .Select(machineDto => new SelectListItem()
                {
                    Value = machineDto.Id.ToString(),
                    Text = machineDto.MachineType.ToString(),
                });
            customerOrderDetailMachineViewModel.CustomerOrderDetail = CustomerOrderDetailsRepository
                .GetCustomerOrderDetails()
                 .Select(ordercustomerdetail => new SelectListItem()
                 {
                     Value = ordercustomerdetail.Id.ToString(),
                 });
            return View(customerOrderDetailMachineViewModel);
        }
        [HttpPost]
        public IActionResult SetCustomerOrderDetailMachine(CustomerOrderDetailMachineViewModel customerorderdetailViewModel)
        {
            var setResult = CustomerOrderDetailMachineRepository.SetOrderMachine(new CustomerOrderDetailsMachineDto()
            {
                MachineId = customerorderdetailViewModel.MachineId.Value,
                CustomerOrderDetailsId = customerorderdetailViewModel.CustomerOrderDetailId.Value,
                AmountWater = customerorderdetailViewModel.AmountWater,
                Id = customerorderdetailViewModel.Id,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now
            });
            return Json(setResult);
        }
        [HttpDelete]
        public IActionResult RemoveCustomerOrderDetailMachine(int id)
        {
            var removeResult = CustomerOrderDetailMachineRepository.RemoveOrderMachine(id);
            return Json(removeResult);
        }

    }
}