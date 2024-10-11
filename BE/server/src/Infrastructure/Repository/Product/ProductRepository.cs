
using Database;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using NShop.src.Application.DTOs.Product;
using NShop.src.Domain.Users;
using Repository.Users;

namespace NShop.src.Infrastructure.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly NShopDbContext _dbContext;

        public ProductRepository(NShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Domain.Product.Product> AddAsync(Domain.Product.Product product)
        {
            var result = await _dbContext.Products.AddAsync(product);

            // Save changes
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Domain.Product.Product> DeleteAsync(Domain.Product.Product product)
        {
             var result = _dbContext.Products.Remove(product);

            // Save changes
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public async Task<IEnumerable<Domain.Product.Product>> GetAllAsync()
        {
            var products = _dbContext.Products.AsEnumerable();

            return products;
        }

        public  async Task<Domain.Product.Product> GetByIdAsync(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            return product;
        }

        public async Task<ProductList> GetProductListAsync(ProductQuery query)
        {
            string searchKeyword = query.SearchKeyword ?? "";
            int page = query.Page < 0 ? 0 : query.Page;
            int pageSize = query.PageSize <= 0 ? 10 : query.PageSize;

            var q = _dbContext.Products
              .Where(u => u.Name.Contains(searchKeyword) || u.Description.Contains(searchKeyword)).Where(u => u.CategoryId.ToString() == query.Categories)
              
              .AsQueryable();

            var total = q.Count();

            var products = q
              .Skip(page * pageSize)
              .Take(pageSize)
              .ToList();

            return new ProductList { Products = products, Total = total };
        }

        public  async Task<Domain.Product.Product> UpdateAsync(Guid Id, Domain.Product.Product product)
        {
            var u = await _dbContext.Products.FindAsync(Id);

            if (u == null) return null;

            var result = _dbContext.Products.Update(product);
            // Save changes
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
