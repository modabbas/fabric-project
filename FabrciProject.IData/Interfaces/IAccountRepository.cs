using FabricProject.Dto;
using FabricProject.Dto.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FabrciProject.IData.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserDto> CreateAccount(UserDto userDto, string role); // Done
        Task<UserDto> CreateFirstAccount(UserDto userDto, string role);
        Task<LoginDto> Login(LoginDto loginDto); // Done
        Task Logout(); // Done 
        IEnumerable<UserDto> GetAllUsers(); // Done
        IEnumerable<RoleDto> GetRoles();
        Task<UserDto> UpdateAccount(UserDto userDto);
        Task<bool> RemoveUser(int id);
        IEnumerable<UserDto> GetRecieverUsers();
    }
}
