using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.DataMapper
{
    public class AttributeType
    {
        [Key]
        public int AtttypeId { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public bool? Important { get; set; }

        public int OrderBy { get; set; }
        public int Status { get; set; }
        public ICollection<Attributes> Attributes { get; set; }
    }
}
