using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;

namespace Blog.Models.ViewModels
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository _blogRepository)
        {
            Categories = _blogRepository.Categories();
        }

        public IList<Category> Categories { get; set; }
    }
}
