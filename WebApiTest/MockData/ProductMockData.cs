using WebApi.Models.Entities;

namespace WebApiTest.MockData
{
    public class ProductMockData
    {
        public List<Product> GetList()
        {
            var data = new List<Product>
            {
                new Product
                {
                    ID = 1,
                    Name = "name",
                    Description = "description",
                    Price = 19.88
                },
                new Product
                {
                    ID = 2,
                    Name = "name2",
                    Description = "description2",
                    Price = 31.6532
                }
            };

            return data;
        }
    }
}
