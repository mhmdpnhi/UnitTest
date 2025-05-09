using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class Product
    {
        public long ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}
