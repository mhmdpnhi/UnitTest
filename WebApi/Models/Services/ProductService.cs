using WebApi.Models.Entities;

namespace WebApi.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly DataBaseCotext _context;

        public ProductService(DataBaseCotext context)
        {
                _context = context;
        }

        public bool Create(Product pr)
        {
            try
            {
                _context.Products.Add(pr);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var pr = _context.Products.Find(id);
                _context.Products.Remove(pr);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        //public IEnumerable<Product> GetByFilter(object[] filters)
        //{
        //    throw new NotImplementedException();
        //}

        public Product GetById(long id)
        {
            return _context.Products.Find(id);
        }
    }
}
