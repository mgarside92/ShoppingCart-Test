using ShoppingCart.Models.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
