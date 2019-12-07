using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Presentation.Models
{
    public class PageViewModel
    {
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
        public IEnumerable<CategoryViewModel> CategoryViewModels { get; set; }
        public IEnumerable<ContactViewModel> ContactViewModels { get; set; }

    }

    public abstract class PageEditModel 
    {
        public string Title { get; set; }

    }

}