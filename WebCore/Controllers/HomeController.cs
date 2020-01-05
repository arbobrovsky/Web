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

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServicesManager _serviceManager;
        private readonly DataManager _dataManager;

        
        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServicesManager(_dataManager);
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderViewModel model)
        {
            //if (await _serviceManager.OrderService.SearchOrder(model.CustomerPhone))
            //{

            //}

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
