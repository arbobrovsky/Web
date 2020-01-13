using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Presentation.Models;
using Presentation.Services;
using WebCore.Areas.Identity;
using WebCore.Areas.Identity.Models;
using WebCore.Areas.Identity.Services;

namespace WebCore.Controllers
{


    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ClientManager _userServices;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EFDBContext _context;
        private readonly TwilioVerifyClient _client;

        public AccountController(TwilioVerifyClient client, EFDBContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _userServices = new ClientManager(_context, _roleManager, userManager);
            _client = client;

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await _userServices.PersonalViewModel(user.Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistationViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> role = new List<string>() { "user" };
                User user = new User { Email = model.Email, UserName = model.Email, CustomerName = model.CustomerName };


                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRolesAsync(user, role);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpGet]
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CancelTheOrder(int orderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await _userServices.PersonalViewModel(user.Id);
            var test = model.OrderUserViewModel as List<OrderUserViewModel>;
            foreach (var item in test[0].Orders)
            {
                if (orderId == item.OrderId)
                {
                    _userServices.DeleteOrder(orderId);

                }
            }
            return RedirectToAction("Index", model);

        }

        public IActionResult VerifyPhone()
        {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> OnPostAsync(InputModel Input)
        {
            if (!ModelState.IsValid)
            {
                return View("VerifyPhone");
            }
            //try
            //{
            var result = await _client.StartVerification(Input.DialingCode, Input.PhoneNumber);
            if (result.Success)
            {

                return RedirectToAction("ConfirmPhone", new { Input.DialingCode, Input.PhoneNumber });
            }

            ModelState.AddModelError("", $"There was an error sending the verification code: {result.Message}");
            //}
            //catch (Exception)
            //{
            //    ModelState.AddModelError("",
            //        "There was an error sending the verification code, please check the phone number is correct and try again");
            //}

            return View("VerifyPhone");
        }



        public IActionResult ConfirmPhone(int DialingCode, string PhoneNumber)
        {
            var result = new InputModel { DialingCode = DialingCode, PhoneNumber = PhoneNumber };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPhone(InputModel Input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var result = await _client.CheckVerificationCode(Input.DialingCode, Input.PhoneNumber, Input.VerificationCode);
                if (result.Success)
                {
                    var identityUser = await _userManager.GetUserAsync(User);
                    identityUser.PhoneNumberConfirmed = true;
                    var updateResult = await _userManager.UpdateAsync(identityUser);

                    if (updateResult.Succeeded)
                    {
                        return RedirectToPage("ConfirmPhoneSuccess");
                    }
                    else
                    {
                        ModelState.AddModelError("", "There was an error confirming the verification code, please try again");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"There was an error confirming the verification code: {result.Message}");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("",
                    "There was an error confirming the code, please check the verification code is correct and try again");
            }

            return View();
        }

    }
}