using FabrciProject.IData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.OrderMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.Controllers
{
   [Authorize(Roles = "Admin")]
    public class OrderMaterialController:Controller
    {
        private ICustomerOrderDetailsRepository CustomerOrderDetailsRepository { get; }
        private IOrderMaterialsRepository OrderMaterialsRepository { get; }
        public OrderMaterialController(IOrderMaterialsRepository orderMaterialsRepository,ICustomerOrderDetailsRepository customerOrderDetailsRepository)
        {
 
            this.OrderMaterialsRepository = orderMaterialsRepository;
            this.CustomerOrderDetailsRepository = customerOrderDetailsRepository;
        }
        public IActionResult Index()
        {
            var vm = new OrderMaterialViewModel()
            {
                orderMaterials = OrderMaterialsRepository.GetOrderMaterials()
              //  Materials=OrderMaterialsRepository.GetMaterials(),
               // Orders=CustomerOrderDetailsRepository.GetCustomerOrderDetails(),
            };
            return View(vm);
        }
 
        [HttpPost]
        public IActionResult Index(OrderMaterialViewModel orderMaterialViewModel)
        {
            var setResult = OrderMaterialsRepository.SetOrderMaterial(new FabricProject.Dto.OrderMaterials.OrderMaterialsDto()
            {
                Id = orderMaterialViewModel.Id,

                ClothMaterialId = orderMaterialViewModel.ClothMaterialId,
                OrderDetailCustomerId = orderMaterialViewModel.OrderDetailCustomerId,
                Amount=orderMaterialViewModel.Amount,

                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now

            });
            if (setResult)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveOrderMaterial(int id)
        {
            OrderMaterialsRepository.RemoveOrderMaterial(id);

            return RedirectToAction("Index");
        }
        public IActionResult Get(int id)
        {
         

            return Json(OrderMaterialsRepository.GetMaterials());
        }

    }
}
