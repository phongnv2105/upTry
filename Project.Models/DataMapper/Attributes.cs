using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class Attributes
    {
        [Key]
        public int AttributeId { get; set; }
        [Required]
        public string AttributeName { get; set; }
        public int AtttypeId { get; set; }
        public string Status { get; set; }
        // thuộc tính liên kết
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        [ForeignKey("AtttypeId")]
        public AttributeType AttributeType { get; set; }
    }
}
