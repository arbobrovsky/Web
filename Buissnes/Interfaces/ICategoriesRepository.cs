using Data.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
   public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int? categorytId);
        void SaveCategory(Category achieve);
        void DeleteCategory(Category achieve);
    }
}
