using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using WebApi.Controllers;
using WebApi.Models.Entities;
using WebApi.Models.Services;
using WebApiTest.MockData;

namespace WebApiTest
{
    public class ProductControllerTest
    {
        ProductMockData _mockData;
        public ProductControllerTest()
        {
            _mockData = new ProductMockData();
        }

        [Fact]
        public void GetAll_Test()
        {
            //Arrange
            var moq = new Mock<IProductService>();
            moq.Setup(x => x.GetAll()).Returns(_mockData.GetList());

            ProductController prController = new ProductController(moq.Object); 
            
            //Act
            var result = prController.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var list = result as OkObjectResult;
            Assert.IsType<List<Product>>(list.Value);
        }

        [Theory]
        [InlineData(1, -1)]
        public void GetById_Test(int validId, int invalidId)
        {
            //Arrange
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetById(validId)).Returns(_mockData.GetList().FirstOrDefault(p => p.ID == validId));
            var productController = new ProductController(moq.Object);

            //Act
            var result = productController.Get(validId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var product = result as OkObjectResult;
            Assert.IsType<Product>(product.Value);

            // ---------- InvalidId Test

            //Arrange
            moq.Setup(p => p.GetById(invalidId)).Returns(_mockData.GetList().FirstOrDefault(p => p.ID == invalidId));

            //Act
            var invalidResult = productController.Get(invalidId);

            //Assert
            Assert.IsType<NotFoundResult>(invalidResult);
        }

        [Fact]
        public void Post_Test()
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
            moq.Setup(x => x.GetById(1)).Returns(validPr);

            // Act
            var result = prController.Post(validPr);

            //Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.IsType<Product>(createdAtActionResult.Value);
            
            Assert.Equal(nameof(ProductController.Get), createdAtActionResult.ActionName);

            var routeValues = Assert.IsType<RouteValueDictionary>(createdAtActionResult.RouteValues);
            Assert.True(routeValues.ContainsKey("id"));
            Assert.Equal(validPr.ID, routeValues["id"]);

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
            var invalidResult = prController.Post(invalidPr);

            //Assert
            Assert.IsType<BadRequestObjectResult>(invalidResult);

            prController.ModelState.Clear();
        }

        [Theory]
        [InlineData(1, -1)]
        public void Delete_Test(int validId, int invalidId)
        {
            //Arrange 
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.Delete(validId)).Returns(true);
            var prController = new ProductController(moq.Object);

            //Act
            var result = prController.Delete(validId);

            //Arrest
            Assert.IsType<OkObjectResult>(result);

            // ---------- InvalidId Test
            //Arrange
            moq.Setup(p => p.Delete(invalidId)).Returns(false);

            //Act
            var invalidResult = prController.Delete(invalidId);

            //Assert
            Assert.IsType<NotFoundObjectResult>(invalidResult);
        }
    }
}
