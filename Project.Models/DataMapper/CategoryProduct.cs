using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class CategoryProduct
    {
        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }
        [Key, Column(Order = 2)]
        public int ProductId { get; set; }
        // thuộc tính liên kết
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
