using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required,EmailAddress,Remote("IsEmailInUse",controller:"Account")]
        [ValidEmailDomain(allowedDomain:"abc.abc", ErrorMessage ="Email Domain must be abc.abc")]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Display(Name ="Confirm password")]
        [Compare("Password",ErrorMessage ="Password and confirmation passowrd do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
