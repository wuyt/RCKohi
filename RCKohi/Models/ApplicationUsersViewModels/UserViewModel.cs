using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCKohi.Models.ApplicationUsersViewModels
{
    public class UserViewModel
    {
        [Display(Name = "用户ID")]
        public string Id { get; set; }

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

        [Display(Name = "权限")]
        public IList<string> Roles { get; set; }
    }
}
