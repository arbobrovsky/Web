using Data.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class EFDBContext : IdentityDbContext<User>
    {
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }

        //public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
        //{
        //    public EFDBContext CreateDbContext(string[] args)
        //    {
        //    {
        //        var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
        //      //  optionsBuilder.UseSqlServer("Server=DESKTOP-Q0HH36T\\SQLEXPRESS;Database=WebCoreDb;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("Data"));

        //        return new EFDBContext(optionsBuilder.Options);
        //    }
        //}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_product> Order_Products { get; set; }
       
    } 
}
