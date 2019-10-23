using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        //[Required]
        //public int UserId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        //public int Total { get; set; }
        public DateTime? CreateDate { get; set; }
        //public string Payment { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
        // thuộc tính liên kết
        //[ForeignKey("UserId")]
        //public Users Users { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
