using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCKohi.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [DataType(DataType.Text)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "下次直接登录")]
        public bool RememberMe { get; set; }
    }
}
