using ShoppingCart.Models.Entities.Interfaces;

namespace ShoppingCart.Models.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
