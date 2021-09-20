using FabrciProject.IData.Interfaces;
using FabricProject.Dto.CustomerOrderDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.CustomerOrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
    public class CustomerOrderDetailController : Controller
    {
        private ICustomerOrderRepository CustomerOrderRepository { get; }
        private IColorRepository ColorRepository { get; }
        private ICustomerOrderDetailsRepository CustomerOrderDetailsRepository { get; }
        public CustomerOrderDetailController(ICustomerOrderDetailsRepository customerOrderDetails, IColorRepository colorRepository, ICustomerOrderRepository customerOrderRepository)
        {
            this.CustomerOrderRepository = customerOrderRepository;
            this.ColorRepository = colorRepository;
            this.CustomerOrderDetailsRepository = customerOrderDetails;
        }
        [Authorize(Roles = "Receiving,Admin")]
        public IActionResult Index()
        {
            var vm = new CustomerOrderDetailsViewModel()
            {
                CustomerOrderDetails = CustomerOrderDetailsRepository.GetCustomerOrderDetails(),
                CustomerOrders = CustomerOrderRepository.GetCustomerOrders(),
                Colors = ColorRepository.GetColors(),
                OrdersInMachineDetails = CustomerOrderDetailsRepository.GetOrdersInMachineDetails(),
            };
            return View(vm);
        }
        [Authorize(Roles = "Machine,Admin")]
        public IActionResult MachineOrder()
        {
            var vm = new CustomerOrderDetailsViewModel()
            {
                OrdersInMachineDetails = CustomerOrderDetailsRepository.GetOrdersInMachineDetails(),
            };
            return View(vm);
        }
        [Authorize(Roles = "Delivery,Admin")]
        public IActionResult DoneOrder()
        {
            var vm = new CustomerOrderDetailsViewModel()
            {
                DoneOrders=CustomerOrderDetailsRepository.GetDoneOrders(),
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult SetSmallOrder(CustomerOrderDetailsViewModel customerOrderDetailViewModel)
        {
            var setResult = CustomerOrderDetailsRepository.SetCustomerOrderDetails(new CustomerOrderDetailsDto()
            {
                Id = customerOrderDetailViewModel.Id, 
                ColorId = customerOrderDetailViewModel.ColorId,
                ColorAmount = customerOrderDetailViewModel.ColorAmount,
                OldLenght = customerOrderDetailViewModel.OldLenght,
                CustomerOrderId = customerOrderDetailViewModel.CustomerOrderId,
                PartialWeghit = customerOrderDetailViewModel.PartialWeghit,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now
            });
            return Json(setResult);
        }
        [HttpPut]
        public IActionResult EditSubOrder(CustomerOrderDetailsViewModel customerOrderDetailViewModel)
        {
            var setResult = CustomerOrderDetailsRepository.EditCustomerOrderDetails(new CustomerOrderDetailsDto()
            {
                Id = customerOrderDetailViewModel.Id,
                ColorId = customerOrderDetailViewModel.ColorId,
                ColorAmount = customerOrderDetailViewModel.ColorAmount,
                OldLenght = customerOrderDetailViewModel.OldLenght,
                CustomerOrderId = customerOrderDetailViewModel.CustomerOrderId,
                PartialWeghit = customerOrderDetailViewModel.PartialWeghit,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now
            });
            return Json(setResult);
        }

        [HttpDelete]
        public IActionResult RemoveSubOrder(int id)
        {
          var removeResult = CustomerOrderDetailsRepository.RemoveCustomerOrderDetails(id);
            return Json(removeResult);
        }

        [HttpPut]
        public IActionResult RemoveOrderFromDeliver(int id)
        {
           var result = CustomerOrderDetailsRepository.RemoveOrderFromDeliver(id);

            return Json(result);
        }

        public IActionResult RemoveOrderFromMachine(int id)
        {
            CustomerOrderDetailsRepository.RemoveOrderFromMachine(id);

            return RedirectToAction("Index");
        }

    }
}
