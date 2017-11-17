using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public virtual int PostId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 3)]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(1000)]
        public string Meta { get; set; }

        [Required]
        [StringLength(200)]
        public string UrlSlug { get; set; }

        [Required]        
        public bool Published { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<TagPost> TagsPosts { get; set; }
    }
}
