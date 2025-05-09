using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication_MVC.Controllers;
using WebApplication_MVC.Models.Entities;
using WebApplication_MVC.Models.Services;
using WebApplication_MVC.Test.MockData;

namespace WebApplication_MVC.Test
{
    public class ProductControllerTest
    {

        [Fact]
        public void Index_Test()
        {
            // Arrange
            var moqData = new ProductMockData();

            var moq = new Mock<IProductService>();

            moq.Setup(p => p.GetAll()).Returns(moqData.GetList());
                
            var prController = new ProductController(moq.Object);

            // Act
            var result = prController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);
            
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(1024, -1024)]
        public void Details_Test(long validId, long invalidId)
        {
            // Arrange
            var moqData = new ProductMockData();

            var moq = new Mock<IProductService>();

            //moq.Setup(p => p.GetById(validId)).Returns(moqData.GetList().FirstOrDefault(x => x.ID == validId));

            var prController = new ProductController(moq.Object);

            // Act
            var result = prController.Details(validId);

            //Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.True(viewResult.Model is Product or null);

            // ---------- InvalidId Test

            // Arrange
            //moq.Setup(p => p.GetById(invalidId)).Returns(moqData.GetList().FirstOrDefault(x => x.ID == invalidId));

            //Act
            var invalidResult = prController.Details(invalidId);

            //Assert
            Assert.IsType<NotFoundResult>(invalidResult);
        }

        [Fact]
        public void Create_Test()
        {
            // Arrange
            var moq = new Mock<IProductService>();
            var prController = new ProductController(moq.Object);

            var validPr = new Product()
            {
                ID = 1,
                Name = "Name",
                Description = "Description",
                Price = 23.543
            };

            moq.Setup(x => x.Create(validPr)).Returns(true);

            // Act
            var result = prController.Create(validPr);

            //Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
            Assert.Null(redirect.ControllerName);

            // ---------- InvalidId Test
            //Arrange
            var invalidPr = new Product()
            {
                Name = "name",
                Price = 18
            };
            moq.Setup(x => x.Create(invalidPr)).Returns(false);

            prController.ModelState.AddModelError("Name", "نام را وارد کنید");

            //Act
            var invalidResult = prController.Create(invalidPr);

            //Assert
            Assert.IsType<BadRequestObjectResult>(invalidResult);

            prController.ModelState.Clear();

            // ---------- InvalidId Test 2
            //Arrange
            var invalidPr2 = new Product()
            {
                Name = "name",
                Price = 18
            };
            moq.Setup(x => x.Create(invalidPr2)).Returns(false);

            //Act
            var invalidResult2 = prController.Create(invalidPr2);

            //Assert
            Assert.IsType<ViewResult>(invalidResult2);
        }
    }
}
