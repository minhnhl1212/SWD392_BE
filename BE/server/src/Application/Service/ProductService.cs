using AutoMapper;
using Database;
using Microsoft.AspNetCore.Mvc;
using NShop.src.Application.DTOs.Product;
using NShop.src.Application.DTOs.User;
using NShop.src.Application.DTOs.Users;
using NShop.src.Application.Services;
using NShop.src.Application.Shared.Constant;
using NShop.src.Application.Shared.Enum;
using NShop.src.Application.Shared.Type;
using NShop.src.Domain.Product;
using NShop.src.Domain.Users;
using NShop.src.Infrastructure.Repository.Product;
using Repository.Users;
using System.Threading.Tasks;

namespace NShop.src.Application.Service
{
    public interface IProductService
    {

        Task<IActionResult> HandleGetAllAsync(ProductQuery query);
        Task<IActionResult> HandleGetByIdAsync(Guid id);
        Task<IActionResult> HandleCreateAsync(ProductCreateDto dto);
        Task<IActionResult> HandleDeleteAsync(Guid id);
        Task<IActionResult> HandleUpdateAsync(Guid id, ProductUpdateDto dto);
    }


    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _repository;


        public ProductService(NShopDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx) : base(dbContext, mapper, httpCtx)
        {
            _repository = new ProductRepository(dbContext);
        }
        public async Task<IActionResult> HandleCreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _repository.AddAsync(product);

            return SuccessResp.Created("User created successfully");
        }

        public  async Task<IActionResult> HandleDeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return ErrorResp.NotFound("User not found");
            }

            await _repository.DeleteAsync(product);

            return SuccessResp.Ok("User deleted successfully");
        }

        public async Task<IActionResult> HandleGetAllAsync(ProductQuery query)
        {
            var resp = await _repository.GetProductListAsync(query);

            var products = _mapper.Map<IEnumerable<ProductDto>>(resp.Products);

            var result = new
            {
                Data = products,
                Total = resp.Total,
                Page = query.Page,
                PageSize = query.PageSize
            };

            return SuccessResp.Ok(result);
        }

        public Task<IActionResult> HandleGetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

     

        public async Task<IActionResult> HandleUpdateAsync(Guid id, ProductUpdateDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return ErrorResp.NotFound("Product not found");
            }

            _mapper.Map(dto, product);

            await _repository.UpdateAsync(id, product);

            return SuccessResp.Ok("Product updated successfully");
        }
    }
}
