using NShop.src.Application.Shared.Type;
namespace NShop.src.Domain.Order;
using NShop.src.Application.Shared.Enum;
public class Order : BaseEntity
{
   
    public decimal TotalPrice { get; set; }
    public OrderStatusEnum Status { get; set; }
    public string? PaymentMethod { get; set; }
    public List<OrderDetails> OrderDetails{ get; set; } = null!;
}

