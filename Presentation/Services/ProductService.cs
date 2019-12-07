using BusinessLogic;
using Data.Entityes;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class ProductService
    {
        private readonly CategoryService _categoryService;
        private readonly DataManager _dataManager;
        public ProductService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _categoryService = new CategoryService(dataManager);
        }


        public async Task<ProductViewModel> ProductDBToViewModelById(int ProductId = 0)
        {
            var product = await _dataManager.Products.GetProductById(ProductId, true);
            var categoryList = await _categoryService.GetCategoriesViewModelList();

            var ViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                TimeWasted = product.TimeWasted,
                DateEditor = product.DateEditor,
                Categories = categoryList,
                CategoryId = product.CategoryId
            };
            return ViewModel;
        }

        public async Task<ProductViewModel> SaveProductEditModelToDb(ProductViewModel productView)
        {
            Product _productDbModel = null;
            if (productView.Id != 0)
            {
                _productDbModel = await _dataManager.Products.GetProductById(productView.Id);
            }
            else
            {
                _productDbModel = new Product();
            }
            _productDbModel.ProductName = productView.Name;
            _productDbModel.Price = productView.Price;
            _productDbModel.TimeWasted = productView.TimeWasted;
            _productDbModel.Description = productView.Description;
            _productDbModel.CategoryId = productView.CategoryId;
            _dataManager.Products.SaveProduct(_productDbModel);

            return await ProductDBToViewModelById(_productDbModel.Id);
        }

        public void DeleteProduct(ProductViewModel productView)
        {
            Product _productDbModel = new Product()
            {
                Id = productView.Id,
                ProductName = productView.Name,
                Price = productView.Price,
                TimeWasted = productView.TimeWasted,
                Description = productView.Description,
                CategoryId = productView.CategoryId
            };

            _dataManager.Products.DeleteProduct(_productDbModel);
        }

        public async Task<List<ProductViewModel>> GetProductsViewModelList()
        {
            var product = await _dataManager.Products.GetAllProducts();
            List<ProductViewModel> productsModel = new List<ProductViewModel>();

            foreach (var item in product)
            {
                productsModel.Add(new ProductViewModel { Id = item.Id, Name = item.ProductName, Description = item.Description, Price = item.Price, CategoryId = item.CategoryId, DateEditor = item.DateEditor });
            }
            return productsModel;
        }

        public async Task<ProductViewModel> CreateNewProductView()
        {
            var categoryList = await _categoryService.GetCategoriesViewModelList();
            return new ProductViewModel() { Categories = categoryList };
        }

        public async Task<bool> EqulsProductNameWithDb(ProductViewModel product)
        {
            var products = await _dataManager.Products.GetAllProducts();
            if (product.Name.Equals(product.Name))
                return true;
            else
                return false;
        }

    }
}
