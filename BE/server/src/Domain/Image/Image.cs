namespace NShop.src.Domain.Image;
using NShop.src.Domain.Product;
using NShop.src.Application.Shared.Type;

    public class Image : BaseEntity 
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }

