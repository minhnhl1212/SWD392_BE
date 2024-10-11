using NShop.src.Domain.Users;

namespace NShop.src.Domain.ShoppingCart
{
    public class ShoppingCart
    {

        public Guid Id { get; set; }
        public Guid UserId  { get; set; }
       public DateTime? CreatedAt    { get; set; }
        public DateTime UpdatedAt { get; set; }



        public User User { get; set; } = null!;

    }
}
