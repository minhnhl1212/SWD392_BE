namespace NShop.src.Infrastructure.Repository.Product;

using global::Repository.Users;
using NShop.src.Application.DTOs.Product;
using NShop.src.Application.DTOs.User;
using NShop.src.Domain.Product;
using NShop.src.Domain.Users;


    public interface IProductRepository
    {

        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Guid Id, Product product);
        Task<Product> DeleteAsync(Product product);
        Task<ProductList> GetProductListAsync(ProductQuery query);
}


public class ProductList
{
    public IEnumerable<Product> Products { get; set; } = [];
    public int Total { get; set; }
}






