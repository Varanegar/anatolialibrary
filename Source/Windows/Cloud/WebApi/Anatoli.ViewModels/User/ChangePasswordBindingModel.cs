using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.User
{
    public class ChangePasswordBindingModel
    {
        public string UserId { get; set; }
        public bool ForgetPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "کلمه عبور {0} می بایست خداقل {2} کاراکتر باشد.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور و تکرار آن برابر نمی باشند.")]
        public string ConfirmPassword { get; set; }

    }

}
