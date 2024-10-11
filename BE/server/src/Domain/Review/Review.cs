namespace NShop.src.Domain.Review;
using NShop.src.Domain.Product;
using NShop.src.Domain.Users;
using NShop.src.Application.Shared.Type;

public class Review : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public double Rating { get; set; }
    public string Content { get; set; } = null!;



    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

