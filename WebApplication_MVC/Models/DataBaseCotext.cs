using Microsoft.EntityFrameworkCore;
using WebApplication_MVC.Models.Entities;

namespace WebApplication_MVC.Models
{
    public class DataBaseCotext : DbContext
    {
        public DataBaseCotext(DbContextOptions<DataBaseCotext> options)
            : base(options)
        {
                
        }

        public DbSet<Product> Products { get; set; }
    }
}
