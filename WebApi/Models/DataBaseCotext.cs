using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.Models
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
