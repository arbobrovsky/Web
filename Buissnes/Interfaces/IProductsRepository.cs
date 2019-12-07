using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProducts(bool includeCategories = false);
        Task<Product> GetProductById(int productId, bool includeCategories = false);
        void SaveProduct(Product achieve);
        void DeleteProduct(Product achieve);

    }
}
