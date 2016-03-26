using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.User
{
    public class CreateUserBindingModel 
    {
        public int? ID { get; set; }

        public Guid? UniqueId { get; set; }

        //[Required]
        [EmailAddress(ErrorMessage = "فرمت ایمیل نا معتبر است.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Mobile { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "کلمه عبور {0} می بایست خداقل {2} کاراکتر باشد.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن برابر نمی باشند.")]
        public string ConfirmPassword { get; set; }

        public bool SendPassSMS { get; set; }

    }
}
