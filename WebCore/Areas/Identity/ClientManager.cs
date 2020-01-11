using Data;
using Data.Entityes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Areas.Identity.Infrastructure;
using WebCore.Areas.Identity.Models;

namespace WebCore.Areas.Identity
{
    public class ClientManager
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EFDBContext _context;

        public ClientManager(EFDBContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        #region User


        public async Task<List<ManagerUserViewModel>> UsersAsync()
        {
            var result = new List<ManagerUserViewModel>();
            var userFromDb = await _userManager.Users.ToListAsync();

            foreach (var user in userFromDb)
            {
                result.Add(new ManagerUserViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    EmailConfirmed = user.EmailConfirmed,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEnd = user.LockoutEnd,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    SecurityStamp = user.SecurityStamp,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserName = user.UserName,
                });
            }
            return result;
        }

        public async Task<EditUserViewModel> GetUserByIdAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return new EditUserViewModel { UserName = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber,CustomerName=user.CustomerName };
            }
            return null;
        }

        public async Task<PersonalViewModel> PersonalViewModel(string userId)
        {
            var result = new PersonalViewModel();
            User user = await _userManager.FindByIdAsync(userId);
            result.User = user;
            result.OrderUserViewModel = await GetOrderUserViewModels(user.PhoneNumber);
            return result;
        }

       


        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Entry(order).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        private async Task<List<OrderUserViewModel>> GetOrderUserViewModels(string userPhone)
        {
            var orderList = await _context.Orders.Include(x => x.OrderProducts).AsNoTracking().Where(x => x.CustomerPhone == userPhone).ToListAsync();

            var result = new List<OrderUserViewModel>();
            var orders = new List<Order>();
            var ordersProducts = new List<Product>();
            foreach (var item in orderList)
            {
                orders = new List<Order>();
                ordersProducts = new List<Product>();
                orders.Add(item);
                foreach (var product in item.OrderProducts)
                {
                    ordersProducts.Add(await _context.Products.FindAsync(product.ProductsId));
                }
                result.Add(new OrderUserViewModel { Orders = orders, Products = ordersProducts });
            }
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            if (Email != "")
            {
                User user = await _userManager.FindByEmailAsync(Email);
                return user;
            }
            return null;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                return true;
            }
            else
                return false;
        }


        #endregion

        #region Roles

        public async Task<List<RoleViewModel>> RolesListAsync()
        {
            var result = new List<RoleViewModel>();
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var item in roles)
            {
                result.Add(new RoleViewModel { RoleName = item.Name });
            }

            return result;
        }


        public async Task<List<OperationDetails>> CreateRoleAsync(string name)
        {
            var errors = new List<OperationDetails>();
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return new List<OperationDetails>();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        // ModelState.AddModelError(string.Empty, error.Description);
                        errors.Add(new OperationDetails(false, error.Description, error.Code));
                    }
                }
            }
            return errors;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                return true;
            }
            return false;
        }

        public async Task<ChangeRoleViewModel> ChangeRoleViewModelAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                return model;
            }
            return null;
        }

        public async void ChangeRoleAsync(string userId, List<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя

                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles != null)
                {
                    // получаем все роли
                    var allRoles = _roleManager.Roles.ToList();
                    // получаем список ролей, которые были добавлены
                    var addedRoles = roles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(roles);
                    await _userManager.AddToRolesAsync(user, addedRoles);
                    await _userManager.RemoveFromRolesAsync(user, removedRoles);
                }
            }
        }


        #endregion
    }
}
