namespace NShop.src.Domain.CartItems
{
   
    
    using NShop.src.Domain.ShoppingCart;
    using NShop.src.Domain.Product;
    public class CartItems
    {

        public Guid CartId {  get; set; }
        public Guid ProductId   { get; set; }

        public int Quantity { get; set; }

        public ShoppingCart ShoppingCart { get; set; } 

        public Product Product { get; set; }
    }
}
