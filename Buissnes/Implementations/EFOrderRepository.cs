using BusinessLogic.Interfaces;
using Data;
using Data.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly EFDBContext _context;

        public EFOrderRepository(EFDBContext context)
        {
            _context = context;
        }
        public void DeleteOrder(Order achieve)
        {
            _context.Orders.Remove(achieve);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Order>> GetAllOrders(bool includeOrder_products = false)
        {
            if (includeOrder_products)
                return await _context.Set<Order>().Include(x => x.OrderProducts).AsNoTracking().ToListAsync();
            else
                return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId, bool includeOrders_product = false)
        {
            if (includeOrders_product)
                return await _context.Set<Order>().Include(x => x.OrderProducts).AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == orderId);
            else
                return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public Task<IList<Order_product>> GetOrderProducts(int order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderPhoneAsync(string phone)
        {
            return _context.Orders.Where(x => x.CustomerPhone == phone).FirstOrDefaultAsync();
        }

        public  void SaveOrder(Order achieve, int[] productsId)
        {
            if (achieve.OrderId == 0)
            {
               achieve.RegistrationTime = DateTime.Now;
                _context.Orders.Add(achieve);

            }
            else
                if (!achieve.Status)
                _context.Entry(achieve).State = EntityState.Modified;

            _context.SaveChanges();

            if (productsId.FirstOrDefault() != 0)
            {
                SaveOrderProducts(productsId, achieve.OrderId);
            }
        }

        private async void SaveOrderProducts(int[] productsId, int orderId)
        {
            if (orderId != 0)
            {
                foreach (var item in productsId)
                {
                    var order_product = new Order_product()
                    {
                        OrderId = orderId,
                        ProductsId = item
                    };

                    await _context.Order_Products.AddAsync(order_product);
                }
                _context.SaveChanges();
            }
        }
    }
}