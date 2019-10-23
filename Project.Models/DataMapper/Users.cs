using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^([\w]+@[\w]+.[a-zA-Z]{2,4})$", ErrorMessage = "Email invalidate!")]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
        [Required]
        [RegularExpression(@"^([07]|[08]|[08]|[03]|[05]+[\d]{8})$", ErrorMessage = "Phone Number invalidate!")]
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Gender { get; set; }
        //public int GroupId { get; set; }
        public int Status { get; set; }
        // thuộc tính liên kết
        [ForeignKey("GroupId")]
        public Group Groups { get; set; }
        //[ForeignKey("GroupId")]
        //public Group Groups { get; set; }

    }
}
