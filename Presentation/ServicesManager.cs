using BusinessLogic;
using Data.Entityes;
using Microsoft.AspNetCore.Identity;
using Presentation.Models;
using Presentation.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class ServicesManager
    {
        DataManager _dataManager;
  
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly ContactService _contactService;
        private readonly OrderService _orderService;

        public ServicesManager(DataManager dataManager)
        {
            _dataManager = dataManager;
            _productService = new ProductService(_dataManager);
            _categoryService = new CategoryService(_dataManager);
            _contactService = new ContactService(_dataManager);
            _orderService = new OrderService(_dataManager);
        }



        public ProductService Products { get { return _productService; } }
        public CategoryService Categories { get { return _categoryService; } }
        public ContactService ContactService { get { return _contactService; } }
        public OrderService OrderService { get { return _orderService; } }
 

        public async Task<PageViewModel> GetAllModel()
        {
            return new PageViewModel
            {
                ProductViewModels = await _productService.GetProductsViewModelList(),
                CategoryViewModels = await _categoryService.GetCategoriesViewModelList(),
                ContactViewModels = await _contactService.GetCategoriesViewModelList()
        };
    }
}
}
