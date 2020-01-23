using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Presentation.Models;
using WebCore.Models;
using Data.Entityes;
using Newtonsoft.Json;
using WebCore.Areas.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServicesManager _serviceManager;
        private readonly DataManager _dataManager;
        private readonly UserManager<User> _userManager;
        private readonly ClientManager _clientManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;


        public HomeController(DataManager dataManager, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServicesManager(_dataManager);
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _clientManager = new ClientManager(null, _roleManager, userManager);
        }


        public async Task<IActionResult> Index()
        {
            var model = await _serviceManager.GetAllModel();

            return View(model);
        }
       
        public async Task<IActionResult> Testing()
        {
            return View(await _dataManager.Products.GetAllProducts(true));
        }

        public IActionResult Galleria()
        {
            return PartialView();
        }

        public async Task<IActionResult> Prices()
        {
            return View(await _serviceManager.GetAllModel());
        }


        [HttpGet]
        public async Task<IActionResult> Service(int categoryId)
        {
            if (categoryId == 0)
            {
                return NotFound();
            }

            return View(await _serviceManager.Categories.CategoryDBToViewModelById(categoryId));
        }




        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await _serviceManager.OrderService.CreateNewOrderViewModel());
        }


        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var model = await _serviceManager.OrderService.CreateNewOrderViewModel();
            if (_signInManager.IsSignedIn(User)){

                var identityUser = await _userManager.GetUserAsync(User);
                model.CustomerName = identityUser.CustomerName;
                model.CustomerEmail = identityUser.Email;
                string phone = identityUser.PhoneNumber.ToString();
                model.CustomerPhone = phone;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderViewModel model)
        {

            if (ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await _serviceManager.OrderService.SaveOrderModelToDb(model);
           
            }
            return RedirectToAction("Index");
        }
    }



}
