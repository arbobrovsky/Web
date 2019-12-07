using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entityes
{
    public class Category
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public string Description { get; set; }
        public IList<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
