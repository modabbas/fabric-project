using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Dto.CustomerOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.CustomerOrder;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin,Receiving")]
    public class CustomerOrderController:Controller
    {
        private ICustomerRepository CustomerRepository { get; }
        private IClothRespository ClothRepository { get; }
        private ICustomerOrderRepository CustomerOrderRepository { get; }
        public CustomerOrderController(ICustomerRepository customerRepository, IClothRespository clothRepository,ICustomerOrderRepository customerOrderRepository)
        {

            this.CustomerRepository = customerRepository;
            this.ClothRepository = clothRepository;
            this.CustomerOrderRepository = customerOrderRepository;
        }
        public IActionResult Index()
        {
            var vm = new CustomerOrderViewModel()
            {
                CustomerOrders = CustomerOrderRepository.GetCustomerOrders(),
                Customers = CustomerRepository.GetCustomers(),
                Cloths=ClothRepository.GetCloths(),
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult SetBigOrder(CustomerOrderViewModel customerOrderViewModel)
        {
            var setResult = CustomerOrderRepository.SetCustomerOrder(new CustomerOrderDto()
            {
                CustomerId = customerOrderViewModel.CustomerId,
                ClothId = customerOrderViewModel.ClothId,
                Weight = customerOrderViewModel.Weight,
                PercentCotton = customerOrderViewModel.PercentCotton,
                PercentPolister = customerOrderViewModel.PercentPolister,
                CreatedAt = DateTime.Now,
                EditedAt = DateTime.Now

            });
            return Json(setResult);
            
        }

        [HttpPut]
        public IActionResult EditBigOrder(CustomerOrderViewModel customerOrderViewModel)
        {
            var setResult = CustomerOrderRepository.SetCustomerOrder(new CustomerOrderDto()
            {
                Id = customerOrderViewModel.Id,
                CustomerId = customerOrderViewModel.CustomerId,
                ClothId = customerOrderViewModel.ClothId,
                Weight = customerOrderViewModel.Weight,
                PercentCotton = customerOrderViewModel.PercentCotton,
                PercentPolister = customerOrderViewModel.PercentPolister,
            });
           
            return Json(setResult);

        }

        public IActionResult RemoveBigOrder(int id)
        {
            var result = CustomerOrderRepository.RemoveCustomerOrder(id);
            return Json(result);
        }
    }
}
