using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCKohi.Models.ApplicationUsersViewModels
{
    public class CreateViewModel
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "电话")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须多于 {2} 位并且少于 {1} 位。", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "权限")]
        public IList<string> Roles { get; set; }
    }
}
