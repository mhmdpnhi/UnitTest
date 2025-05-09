using WebApplication_MVC.Models.Entities;

namespace WebApplication_MVC.Models.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        //IEnumerable<Product> GetByFilter(object[] filters);
        Product GetById(long id);
        bool Create(Product pr);
        bool Delete(long id);
    }
}
