using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Areas.Identity.Models
{
    public class PersonalViewModel
    {
        public User User { get; set; }
        public List<OrderUserViewModel> OrderUserViewModel { get; set; }

    }
    public class OrderUserViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public int[] SelectProductsId { get; set; }
    }
}
