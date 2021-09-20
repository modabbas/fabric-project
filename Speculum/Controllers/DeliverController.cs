using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Deliver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Deliver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin,Delivery")]
    public class DeliverController:Controller
    {
        private IDeliverRepository DeliverRepository { get; }
        private ICustomerOrderDetailsRepository CustomerOrderDetailsRepository { get; }

        public DeliverController(IDeliverRepository deliverRepository,ICustomerOrderDetailsRepository customerOrderDetailsRepository)
        {

            this.DeliverRepository = deliverRepository;
            CustomerOrderDetailsRepository = customerOrderDetailsRepository;
        }
        public IActionResult Index()
        {
            var vm = new DeliverViewModel()
            {
                DeliverDetails = DeliverRepository.GetDelivered(),
                Orders=CustomerOrderDetailsRepository.GetDoneOrdersOfDeliver(),
                GoneOrders=DeliverRepository.GetGiven(),
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(DeliverViewModel deliverViewModel)
        {
            var setResult = DeliverRepository.EnterTheDeliver(new DeliverDto()
            {
                Id = deliverViewModel.Id,

                NewLength = deliverViewModel.NewLength,
                OrderDetailCustomerId = deliverViewModel.CustomerOrderId,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now

            });
            if (setResult)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult LeaveDeliver(int id)
        {
            DeliverRepository.LeaveTheDeliver(id);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveDeliver(int id)
        {
            DeliverRepository.RemoveDeliver(id);

            return RedirectToAction("Index");
        }

    }
}
