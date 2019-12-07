using System;
using BusinessLogic;
using BusinessLogic.Implementations;
using BusinessLogic.Interfaces;
using Data;
using Data.Entityes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebCore.Repository
{
    public static class LogicManagementDb
    {
        public static void ServiceDescriptors(IServiceCollection services, IConfiguration Configuration)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFDBContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("Data")));
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddTransient<ICategoriesRepository, EFCategoriesRepository>();
            services.AddTransient<IContactRepository, EFContactRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<EFDBContext>();
            services.AddScoped<DataManager>();

        }
    }
}
