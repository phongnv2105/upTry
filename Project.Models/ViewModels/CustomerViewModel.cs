using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class CustomerViewModel
    {
        public class CustomerLogin
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
        public class CustomerRegister
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Phone { get; set; }
        }
    }
}
