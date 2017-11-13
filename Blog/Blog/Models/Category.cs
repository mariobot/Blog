using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string UrlSlug { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
