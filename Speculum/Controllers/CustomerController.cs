using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Customer;

namespace Speculum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController:Controller
    {
        private ICustomerRepository CustomerRepository { get; }

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.CustomerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var vm = new CustomerViewModel()
            {
                GetCustomers = CustomerRepository.GetCustomers(),
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult SetCustomer(CustomerViewModel customerViewModel)
        {
            var setResult = CustomerRepository.SetCustomer(new CustomerDto()
            {
                CustomerId = customerViewModel.CustomerId,
                Name = customerViewModel.Name,
                Phone = customerViewModel.Phone,
                Email = customerViewModel.Email,
                CreatedAt = DateTime.Now,
                EditedAt=DateTime.Now
            });
              return Json(setResult);
        }

     
        [HttpDelete]
        public IActionResult CustomerRemove(int id)
        {
          var removeResult = CustomerRepository.RemoveCustomer(id);
            return Json(removeResult);
        }

    }
}
