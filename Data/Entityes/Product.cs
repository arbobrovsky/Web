using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entityes
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TimeWasted { get; set; }
        public DateTime DateEditor { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
