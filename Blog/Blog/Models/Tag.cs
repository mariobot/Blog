using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string UrlSlug { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public ICollection<TagPost> TagsPosts { get; set; }
    }
}
