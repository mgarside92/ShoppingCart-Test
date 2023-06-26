namespace ShoppingCart.Models.ViewModels
{
    public class CartViewModel
    {
        public double Balance { get; set; } = 0;
        public List<CartItemViewModel> Products { get; set; }
    }
}
