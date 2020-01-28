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
    public class EFCategoriesRepository : ICategoriesRepository
    {
        private readonly EFDBContext _context;
        public EFCategoriesRepository(EFDBContext context)
        {
            _context = context;
        }

        public void DeleteCategory(Category achieve)
        {
            if (achieve != null)
            {
                _context.Categories.Remove(achieve);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
          
                return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int? categorytId)
        {
                return await _context.Categories.FirstOrDefaultAsync(x => x.Id == categorytId);
        }

        public void SaveCategory(Category achieve)
        {
            if (achieve.Id == 0)
                _context.Categories.Add(achieve);
            else
                _context.Entry(achieve).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
