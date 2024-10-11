using NShop.src.Application.Shared.Type;
namespace NShop.src.Domain.Suppliers
   
{
    public class Suppliers : BaseEntity
    {
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; } = null!;
        public string? Status { get; set; } 
    }
}
