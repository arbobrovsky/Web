using BusinessLogic;
using Data.Entityes;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class OrderService
    {
        private readonly DataManager _dataManager;
        public OrderService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<OrderViewModel> OrderDBToViewModelById(int OrderId = 0)
        {
            var ModelOrderFromDb = await _dataManager.Order.GetOrderById(OrderId);
            return OrderViewModel(ModelOrderFromDb);
        }


        public async Task<OrderViewModel> SaveOrderModelToDb(OrderViewModel model)
        {
            var orderDbModel = OrderDbModel(model);
            orderDbModel = null;
            if (model.OrderId != 0)
            {
                orderDbModel = await _dataManager.Order.GetOrderById(model.OrderId);
            }
            else
            {
                orderDbModel = new Order();
            }
            orderDbModel = OrderDbModel(model);
            var value = await Amount(model.SelectProductsId);
            orderDbModel.Price = value.Price;
            orderDbModel.TimeWasted = value.TimeWasted;


            _dataManager.Order.SaveOrder(orderDbModel, model.SelectProductsId);


            return await OrderDBToViewModelById(model.OrderId);
        }

        public async Task<OrderViewModel> CreateNewOrderViewModel()
        {
            return new OrderViewModel
            {
                //  Products = await _dataManager.Products.GetAllProducts(),
                Categories = await GetCategoryWithProducts(),
                OrderingTime = await GetDateTimeBookingAsync()
            };
        }

        public async Task<List<Order>> Orders()
        {
            var result = await _dataManager.Order.GetAllOrders(true);
            return result.ToList();
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategoryWithProducts()
        {
            List<ProductViewOrder> productViews = new List<ProductViewOrder>();
            List<CategoryViewModel> categoryViews = new List<CategoryViewModel>();

            var productDb = await _dataManager.Products.GetAllProducts();
            var CategoryDb = await _dataManager.Categories.GetAllCategories();


            foreach (var item in CategoryDb)
            {
                productViews = new List<ProductViewOrder>();
                foreach (var product in productDb.Where(x => x.CategoryId == item.Id))
                {
                    productViews.Add(new ProductViewOrder { Id = product.Id, Name = product.ProductName, Price = product.Price });
                }
                categoryViews.Add(new CategoryViewModel { Id = item.Id, NameCategory = item.NameCategory, Description = item.Description, Products = productViews });
            }


            return categoryViews;
        }


        public async Task<bool> SearchOrder(string phone)
        {
            var takeOrder = await _dataManager.Order.GetOrderPhoneAsync(phone);
            if (takeOrder != null)
            {
                return true;
            }

            return false;
        }


        private OrderViewModel OrderViewModel(Order order)
        {
            return new OrderViewModel
            {
                OrderId = order.OrderId,
                Date = order.Date,
                OrderProducts = order.OrderProducts,
                Price = order.Price,
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                CustomerEmail = order.CustomerEmail,
                CustomerComment = order.CustomerComment,
                RegistrationTime = order.RegistrationTime,
                Status = order.Status
            };
        }

        private Order OrderDbModel(OrderViewModel model)
        {
            var date = new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.Time.Hour, model.Time.Minute, 0);
            return new Order
            {
                OrderId = model.OrderId,
                Date = date,
                OrderProducts = model.OrderProducts,
                Price = model.Price,
                CustomerName = model.CustomerName,
                CustomerPhone = model.CustomerPhone,
                CustomerEmail = model.CustomerEmail,
                CustomerComment = model.CustomerComment,
                RegistrationTime = model.RegistrationTime,
                Status = model.Status
            };
        }

        private async Task<OrderAmoutAndTime> Amount(int[] productId)
        {
            OrderAmoutAndTime result = new OrderAmoutAndTime();
            decimal amount = 0;
            int minWastad = 0;
            foreach (var item in productId)
            {
                var product = await _dataManager.Products.GetProductById(item, false);
                amount += product.Price;
                minWastad += product.TimeWasted;
            }
            result.Price = amount;
            result.TimeWasted = minWastad;

            return result;
        }


        private async Task<List<OrderWorkTIme>> GetDateTimeBookingAsync()
        {
            List<OrderWorkTIme> result = new List<OrderWorkTIme>();
            foreach (var item in await _dataManager.Order.GetAllOrders())
            {
                result.Add(new OrderWorkTIme { OrderingDate = item.Date, TimeWasted = item.TimeWasted });
            }

            return result;
        }



    }
}
