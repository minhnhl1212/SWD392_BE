namespace NShop.src.Domain.Order;
using NShop.src.Domain.Product;



    public class OrderDetails
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
      
    

        public Product Product { get; set; } = null!;

        public Order Order { get; set; } = null!;
    }

