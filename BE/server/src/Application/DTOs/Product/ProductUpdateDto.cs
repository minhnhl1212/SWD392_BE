namespace NShop.src.Application.DTOs.Product
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Status { get; set; }
    }
}
