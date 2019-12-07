using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders(bool includeOrders_product = false);
        Task<Order> GetOrderById(int orderId, bool includeOrders_product = false);
        void SaveOrder(Order achieve, int[] productsId);
        void DeleteOrder(Order achieve);
        Task<IList<Order_product>> GetOrderProducts(int order);
        Task<Order> GetOrderPhoneAsync(string phone);



    }
}
