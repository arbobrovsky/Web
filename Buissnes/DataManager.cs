using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DataManager
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IOrderRepository _orderRepository;
  

        public DataManager(
            IProductsRepository productsRepository,
            ICategoriesRepository categoriesRepository,
            IContactRepository contactRepository, 
            IOrderRepository orderRepository
            )
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _contactRepository = contactRepository;
            _orderRepository = orderRepository;
        }

        public IProductsRepository Products { get { return _productsRepository; } }
        public ICategoriesRepository Categories { get { return _categoriesRepository; } }
        public IContactRepository Contacts { get { return _contactRepository; } }
        public IOrderRepository Order{ get { return _orderRepository; } }
       
    }
}
