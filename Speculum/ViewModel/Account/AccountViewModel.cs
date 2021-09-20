using FabricProject.Dto;
using FabricProject.Dto.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Speculum.ViewModel.Account
{
    public class AccountViewModel
    {
        public IEnumerable<UserDto> GetAllUsers { get; set; }
        public IEnumerable<RoleDto> GetRoles { get; set; }
        /// /////////////////////////////////////////////////////////////////
        /// /////////////////////////////////////////////////////////////////
        public int Id { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال اسم المستخدم")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "لايمكن كتابة اسم المستخدم باللغة العربية او الرموز[ - و _ ]....")]
        [MinLength(4, ErrorMessage = "لا يمكن كتابة أقل من أربع أحرف لاسم المستخدم")]
        [MaxLength(15, ErrorMessage = "لا يمكن كتابة أكثر من خمسة عشر حرفاً")]
        public string UserName { get; set; }
        [MinLength(4, ErrorMessage = "لا يمكن كتابة أقل من أربع أحرف لكلمة السر")]
        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "الرجاء إدخال كلمة السر")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Role { get; set; }
        public ActiveViewModel Active { get; set; }
    }
}
