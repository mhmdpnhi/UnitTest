using WebApi.Models.Entities;

namespace WebApi.Models.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(long id);
        bool Create(Product pr);
        bool Delete(long id);
    }
}
