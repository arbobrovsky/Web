using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entityes
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerComment { get; set; }

        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int TimeWasted { get; set; }

        public bool Status { get; set; }  //status accept order
        public DateTime RegistrationTime { get; set; } // time registration order
        public virtual List<Order_product> OrderProducts { get; set; }

        public Order()
        {
            OrderProducts = new List<Order_product>();
        }
    }
}
