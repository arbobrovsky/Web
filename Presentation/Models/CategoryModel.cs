using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Models
{
    public class CategoryViewModel : PageEditModel
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public string Description { get; set; }
        public List<ProductViewOrder> Products { get; set; }
    }

}
