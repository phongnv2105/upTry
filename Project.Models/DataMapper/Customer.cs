using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [RegularExpression(@"^(([\w])+(@)+([\w])+(.)+([a-zA-Z]{2,4}))$", ErrorMessage = "Email invalidate!")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string CodeConfirm { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^(([07]||[08]||[09]||[03]||[05])+([\d]{8}))$", ErrorMessage = "Phone Number invalidate!")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Avartar { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? Update_at { get; set; }
        public bool? Status { get; set; }
        // thuộc tính liên kết
        public ICollection<Orders> Orders { get; set; }
    }
}
