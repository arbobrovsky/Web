using BusinessLogic;
using Data.Entityes;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class CategoryService
    {

        private readonly DataManager _dataManager;

        public CategoryService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }


        public async Task<CategoryViewModel> CategoryDBToViewModelById(int CategoryId = 0)
        {
            var categoryDbModel = await _dataManager.Categories.GetCategoryById(CategoryId);

            return new CategoryViewModel()
            {
                Id = categoryDbModel.Id,
                NameCategory = categoryDbModel.NameCategory,
                Description = categoryDbModel.Description,

            };
        }


        public async Task<CategoryViewModel> SaveCategoryEditModelToDb(CategoryViewModel model)
        {
            Category categoryDbModel = null;

            if (model.Id != 0)
            {
                categoryDbModel = await _dataManager.Categories.GetCategoryById(model.Id);
            }
            else
            {
                categoryDbModel = new Category();
            }
            categoryDbModel.NameCategory = model.NameCategory;
            categoryDbModel.Description = model.Description;
            _dataManager.Categories.SaveCategory(categoryDbModel);

            return await CategoryDBToViewModelById(categoryDbModel.Id);
        }

        public void DeleteCategory(CategoryViewModel model)
        {
            Category categoryDbModel = new Category()
            {
                Id = model.Id,
                NameCategory = model.NameCategory,
                Description = model.Description
            };
            _dataManager.Categories.DeleteCategory(categoryDbModel);
        }

        public async Task<List<CategoryViewModel>> GetCategoriesViewModelList()
        {
            var categoryes = await _dataManager.Categories.GetAllCategories();
            List<CategoryViewModel> CategoryList = new List<CategoryViewModel>();
            foreach (var item in categoryes)
            {
                CategoryList.Add(new CategoryViewModel {
                    Id = item.Id,
                    NameCategory = item.NameCategory,
                    Description = item.Description
                });
            }

            return CategoryList;
        }

        public CategoryViewModel CreateNewCategoryView()
        {
            return new CategoryViewModel() { };
        }
    }
}
