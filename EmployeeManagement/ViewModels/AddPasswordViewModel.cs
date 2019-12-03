using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class AddPasswordViewModel
    {
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        [Compare("NewPassword", ErrorMessage =
                  "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}