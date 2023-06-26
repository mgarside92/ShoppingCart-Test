using ShoppingCart.Models.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models.Entities
{
    public class Deal : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int DealQuantity { get; set; }
        public double DealPrice { get; set; }
    }
}
