using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public string Attributes { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        // thuộc tính liên kết
        [ForeignKey("OrderId")]
        public Orders Orders { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("SizeId")]
        public Size Size { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }

    }
}
