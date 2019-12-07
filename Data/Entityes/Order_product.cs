using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entityes
{
    public class Order_product
    {
        public int Order_productId { get; set; }
        public int ProductsId { get; set; }
        public virtual Product Products { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
