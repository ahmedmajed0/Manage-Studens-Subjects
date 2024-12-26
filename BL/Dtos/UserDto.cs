using AppResources.Messages;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class UserDto : BaseTableDto
    {
        [Required(ErrorMessageResourceName = "RequiredEmail", ErrorMessageResourceType = typeof(Messages), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; } = null!;
        [Required(ErrorMessageResourceName = "RequiredPassword", ErrorMessageResourceType = typeof(Messages), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Messages))]
        public string Password { get; set; } = null!;
        [Required(ErrorMessageResourceName = "RequiredConfirmPassword", ErrorMessageResourceType = typeof(Messages), AllowEmptyStrings = false)]
        [Compare(nameof(Password), ErrorMessageResourceName = "ConfirmPasswordMismatch", ErrorMessageResourceType = typeof(Messages))]
        public string? ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
