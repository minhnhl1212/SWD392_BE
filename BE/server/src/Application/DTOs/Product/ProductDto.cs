namespace NShop.src.Application.DTOs.Product;
using AutoMapper;
using NShop.src.Application.DTOs.Supplier;
using NShop.src.Application.DTOs.Users;
using NShop.src.Domain.Product;


public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }

    }


public class ProductProfile : Profile
{

    public ProductProfile(){
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();


        CreateMap<Product, ProductUpdateDto>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<ProductUpdateDto, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<Product, ProductCreateDto>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<ProductCreateDto, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }

}