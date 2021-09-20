using FabrciProject.IData.Interfaces;
using FabricProject.DContext;
using FabricProject.Dto;
using FabricProject.Dto.Account;
using FabricProject.Models.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FabricProject.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private FabricProjectDbContext Context { get; set; }
        public UserManager<FbUser> UserManager { get; set; }
        public SignInManager<FbUser> SignInManager { get; set; }
        private IPasswordHasher<FbUser> PasswordHasher { get; }


        public AccountRepository(FabricProjectDbContext context, UserManager<FbUser> userManager, 
            SignInManager<FbUser> signInManager,
            IPasswordHasher<FbUser> passwordHasher)
        {
            Context = context;
            UserManager = userManager;
            SignInManager = signInManager;
            PasswordHasher = passwordHasher;
        }

        public async Task<UserDto> CreateAccount(UserDto userDto, string role)
        {
            // For Create New Accout in My Dashboard
            var userEntity = await UserManager.CreateAsync(new FbUser
            {
                UserName = userDto.UserName,
                EmailConfirmed = true,
                AccountDate = System.DateTime.Now
            }, userDto.Password);
            if (userEntity == IdentityResult.Success)
            {
                try
                {
                    var user = await UserManager.FindByNameAsync(userDto.UserName);
                    var roleResult = await UserManager.AddToRoleAsync(user, role);
                }
                catch (Exception)
                {
                    return null;
                }

                return userDto;
            }
            userDto.ErrorCode = userEntity.Errors.ElementAt(0).Code;// Error Messaging (Access Denied)
            return userDto;
        }

        public async Task<UserDto> CreateFirstAccount(UserDto userDto, string role)
        {
            var count = Context.Users.Count();
            if (!Context.Users.Any())
            {
                var result = await UserManager.CreateAsync(new FbUser
                {
                    UserName = userDto.UserName,
                    EmailConfirmed = true,
                    AccountDate = System.DateTime.Now,
                }, userDto.Password);
                if (result == IdentityResult.Success)
                {
                    try
                    {
                        var user = await UserManager.FindByNameAsync(userDto.UserName);
                        var roleResult = await UserManager.AddToRoleAsync(user, role);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return null;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            // There is A Secret User: { UserName: admin , Password: admin }
            var result = Context.Users
            .Select(user => new UserDto()
            {
                Id = user.Id,
                AccountDate = user.AccountDate,
                UserName = user.UserName,
                Role = string.Join("-", Context.Roles
                .Where(role => Context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId).Contains(role.Id)).Select(role => new {
                    Name = role.Name,
                }).Select(n => n.Name)),
                RoleAr = string.Join("-", Context.Roles
                .Where(role => Context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId).Contains(role.Id)).Select(role => new {
                    NameAr = role.NameAr,
                }).Select(n => n.NameAr)),
                RoleId = Context.Roles
                .Where(role => Context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId).Contains(role.Id)).Select(role => new
                {
                    Id = role.Id
                }).FirstOrDefault().Id
            }).ToList();

            return result;
        }

        public IEnumerable<UserDto> GetRecieverUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return Context.Roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Type = role.Name,
                TypeAr = role.NameAr
            }).ToList();
        }

       
        public async Task<LoginDto> Login(LoginDto loginDto)
        {
            // For Login To My Dashboard 
            var userEntity = Context.Users.Where(user => user.UserName == loginDto.UserName).SingleOrDefault();
            if (userEntity == null)
            {
                return null;
            }
            var loginResult = await SignInManager.PasswordSignInAsync(userEntity, loginDto.Password, loginDto.RememberMe, false);
            if (loginResult.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(loginDto.UserName);
                var role = await UserManager.GetRolesAsync(user);
                loginDto.Role = role[0];
                return loginDto;
            }
            return null;
        }

        public async Task Logout()
        {
            // For Logout from My Dashboard
            await SignInManager.SignOutAsync();
        }

        public async Task<bool> RemoveUser(int id)
        {
            // Remove The Accout Of User finally
            try
            {
                var user = await UserManager.FindByIdAsync(id.ToString());
                var result = await UserManager.DeleteAsync(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UserDto> UpdateAccount(UserDto userDto)
        {
            // Update User Accout 
            try
            {
                var user = await UserManager.FindByIdAsync(userDto.Id.ToString());
                //var userRoles = await UserManager.GetRolesAsync(user);
                if (!userDto.Password.Contains("●"))
                {
                    user.PasswordHash = PasswordHasher.HashPassword(user, userDto.Password);
                }
                Context.UserRoles
                    .RemoveRange(Context.UserRoles
                    .Where(user1 => user1.UserId == userDto.Id));
                Context.SaveChanges();
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                var roleresult = await UserManager.AddToRoleAsync(user, userDto.Role);
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return userDto;
                }
                else
                {
                    userDto.ErrorCode = result.Errors.ElementAt(0).Code;
                    return userDto;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
