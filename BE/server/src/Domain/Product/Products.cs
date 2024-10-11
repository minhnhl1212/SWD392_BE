namespace NShop.src.Domain.Product;

using NShop.src.Domain.Suppliers;


using NShop.src.Domain.Categories;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SupplierId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public Categories Categories { get; set; } = null!;


    public Suppliers Suppliers { get; set; } = null!;




}

