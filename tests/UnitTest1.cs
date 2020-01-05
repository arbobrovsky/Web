using BusinessLogic;
using Data.Entityes;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCore.Controllers;
using Xunit;

namespace tests
{
    public class UnitTest1
    {

        private readonly DataManager _dataManager;
        [Fact]
        public async Task Test1()
        {

            var mock = new Mock<DataManager>();
            mock.Setup(item => item.Products.GetAllProducts(true)).Returns(list());
            var controller = new HomeController(mock.Object);
            ViewResult result = await controller.Testing() as ViewResult;
        }


        private async Task<IEnumerable<Product>> list()
        {
            var products = new List<Product>() {
                new Product{Id=23, ProductName="один",Price=1},
                new Product{Id=24, ProductName="два",Price=2},
                new Product{Id=25, ProductName="три",Price=3},
                new Product{ Id=26,ProductName="четре",Price=4},
            };


            return products;
        }
    }
}
