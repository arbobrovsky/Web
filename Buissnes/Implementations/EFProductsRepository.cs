using BusinessLogic.Interfaces;
using Data;
using Data.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly EFDBContext _context;
        public EFProductsRepository(EFDBContext context)
        {
            _context = context;
        }
        public void DeleteProduct(Product achieve)
        {
            _context.Products.Remove(achieve);
            _context.SaveChanges();
        }

        public async Task<Product> GetProductById(int productId, bool includeCategories = false)
        {
            if (includeCategories)
                return await _context.Set<Product>().Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
            else
                return await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetAllProducts(bool includeCategories = false)
        {
            if (includeCategories)
                return await _context.Set<Product>().Include(x => x.Category).AsNoTracking().ToListAsync();
            else
                return await _context.Products.ToListAsync();
        }
        public void SaveProduct(Product achieve)
        {
            achieve.DateEditor = DateTime.Now;
            if (achieve.Id == 0)
                _context.Products.Add(achieve);
            else
                _context.Entry(achieve).State = EntityState.Modified;
            _context.SaveChanges();
        }


    }
}
