using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Presentation.Models;
using Data.Enums;
using static Data.Enums.PageEnum;
using Microsoft.AspNetCore.Identity;
using WebCore.Areas.Identity;
using Data.Entityes;
using WebCore.Areas.Identity.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private readonly ServicesManager _serviceManager;
        private readonly DataManager _dataManager;
        private readonly UserManager<User> _userManager;
        private readonly ClientManager _clientManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ManagerController(DataManager dataManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServicesManager(_dataManager);
            _userManager = userManager;
            _roleManager = roleManager;
            _clientManager = new ClientManager(null, _roleManager, userManager);
        }

        public async Task<IActionResult> Index()
        {
            var model = await _serviceManager.Products.GetProductsViewModelList();

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            if (!string.IsNullOrEmpty(role.RoleName))
            {
                var complite = await _clientManager.CreateRoleAsync(role.RoleName);
                foreach (var item in complite)
                {
                    if (item.Succedeed)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, item.Message);
                    }
                }
            }
            else ModelState.AddModelError(string.Empty, "Заполните поле");

            return View(role);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            return View(await _clientManager.ChangeRoleViewModelAsync(userId));
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(string userId, List<string> roles)
        {
            //_clientManager.ChangeRoleAsync(userId, roles);
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Users");
            }

            return NotFound();


            

        }

        [HttpGet]
        public async Task<IActionResult> Roles() => View(await _clientManager.RolesListAsync());


        [HttpGet]
        public async Task<IActionResult> Content() => View(await _serviceManager.GetAllModel());


        [HttpGet]
        public async Task<IActionResult> PageEditor(PageType pageType, int Id)
        {
            PageEditModel editModel;
            switch (pageType)
            {
                case PageType.Product:
                    if (Id != 0)
                    {
                        editModel = await _serviceManager.Products.ProductDBToViewModelById(Id);
                        editModel.Title = "Редактирование продукта";
                    }
                    else
                    {
                        editModel = await _serviceManager.Products.CreateNewProductView();
                        editModel.Title = "Новая запись продукта";
                    }
                    break;
                case PageType.Category:
                    if (Id != 0)
                    {
                        editModel = await _serviceManager.Categories.CategoryDBToViewModelById(Id);
                        editModel.Title = "Редактирование категории";
                    }
                    else
                    {
                        editModel = _serviceManager.Categories.CreateNewCategoryView();
                        editModel.Title = "Новая запись категории";
                    }
                    break;

                case PageType.Contact:
                    if (Id != 0)
                    {
                        editModel = await _serviceManager.ContactService.ContactDBToViewModelById(Id);
                        editModel.Title = "Редактирование контакта";
                    }
                    else
                    {
                        editModel = _serviceManager.ContactService.CreateNewContactViewModel();
                        editModel.Title = "Новая запись контакта";
                    }
                    break;

                default: editModel = null; break;
            }

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductViewModel model)
        {
            string url = "";
            if (ModelState.IsValid)
            {
                await _serviceManager.Products.SaveProductEditModelToDb(model);
                return RedirectToAction("Content", "Manager");
            }
            else
            {
                if (model.Id == 0)
                {
                    await _serviceManager.Products.SaveProductEditModelToDb(model);
                    url = Url.Action("PageEditor", "Manager", new { pageType = PageType.Product.ToString() });
                }
                else
                {
                    await _serviceManager.Products.SaveProductEditModelToDb(model);
                    url = Url.Action("PageEditor", "Manager", new { id = model.Id, pageType = PageType.Product.ToString() });
                }
            }
            

            return Redirect(url);

        }

        public async Task<IActionResult> Users() => View(await _clientManager.UsersAsync());



        [HttpPost]
        public async Task<IActionResult> SaveContact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.ContactService.SaveContactEditModelToDb(model);
            }

            return RedirectToAction("Content", "Manager");

        }

        [HttpPost]
        public IActionResult DeleteContact(ContactViewModel model)
        {
            _serviceManager.ContactService.DeleteContact(model);

            return RedirectToAction("Content", "Manager");
        }

        [HttpPost]
        public IActionResult DeleteProduct(ProductViewModel model)
        {
            _serviceManager.Products.DeleteProduct(model);

            return RedirectToAction("Content", "Manager");
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(CategoryViewModel model)
        {
            await _serviceManager.Categories.SaveCategoryEditModelToDb(model);

            return RedirectToAction("Content", "Manager");
        }

        [HttpPost]
        public IActionResult DeleteCategory(CategoryViewModel model)
        {
            _serviceManager.Categories.DeleteCategory(model);

            return RedirectToAction("Content", "Manager");
        }

        [HttpGet]
        public async Task<IActionResult> OrderTest()
        {
            var model = await _serviceManager.OrderService.CreateNewOrderViewModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var model = await _serviceManager.OrderService.CreateNewOrderViewModel();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> OrderTest(OrderViewModel model)
        {
            await _serviceManager.OrderService.SaveOrderModelToDb(model);

            return RedirectToAction("Index", "Home");
        }



    }
}