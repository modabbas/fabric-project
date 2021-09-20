using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabrciProject.IData.Interfaces;
using FabricProject.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Speculum.ViewModel;
using Speculum.ViewModel.Account;


namespace Speculum.Controllers
{
    public class AccountController : Controller
    {
        public IAccountRepository AccountRepository { get; }

        public AccountController(IAccountRepository accountRe)
        {
            AccountRepository = accountRe;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new AccountViewModel()
            {
                GetAllUsers = AccountRepository.GetAllUsers(),
                GetRoles = AccountRepository.GetRoles(),
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Login(int error)
        {
            if (User.Identity.IsAuthenticated && error == 0)
            {
                return RedirectToAction("Index", "Statistics");
            }
            var result = await AccountRepository.CreateFirstAccount(new UserDto() {
             UserName = "admin",
             Password = "admin"
            }, "Admin");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel accountViewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(accountViewModel);
            }

            var result = await AccountRepository.Login(new LoginDto
            {
                Password = accountViewModel.Password,
                UserName = accountViewModel.UserName,
                RememberMe = accountViewModel.RememberMe
            });
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "خطأ في كتابة كلمة السر او اسم المستخدم");
                return View(accountViewModel);
            }
            switch (result.Role)
            {
                case "Receiving":
                    return RedirectToAction("Index", "CustomerOrder");
                case "Delivery":
                    return RedirectToAction("Index", "Deliver");
                case "Machine":
                    return RedirectToAction("MachineOrder", "CustomerOrderDetail");
                case "Laboratory":
                    return RedirectToAction("Index", "Lab");
                default:
                    return RedirectToAction("Index", "Statistics");
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(null);
            }
            var result = await AccountRepository.CreateAccount(new UserDto
            {
                UserName = accountViewModel.UserName,
                Password = accountViewModel.Password,
            }, accountViewModel.Role);
            if (result.ErrorCode != null)
            {
                string Error = result.ErrorCode;
                return Json(Error);
            }
            return Json(true);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(AccountViewModel accountViewModel)
        {
            var user = new UserDto()
            {
                Id = accountViewModel.Id,
                Password = accountViewModel.Password,
                UserName = accountViewModel.UserName,
                Role = accountViewModel.Role,
            };

            var result = await AccountRepository.UpdateAccount(user);
            if (result.ErrorCode != null)
            {
                string Error = result.ErrorCode;
                return Json(Error);
            }
            result.Testing = true;
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var result = await AccountRepository.RemoveUser(id);
            return Json(result);
        }

        public IActionResult Logout()
        {
            AccountRepository.Logout();
            return RedirectToAction(nameof(AccountController.Login));
        }

    }
}
