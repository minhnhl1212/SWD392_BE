namespace NShop.src.Application.DTOs.Supplier;
using AutoMapper;
using NShop.src.Application.DTOs.Users;
using NShop.src.Domain.Suppliers;


    public class SupplierDto
    {
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class SuppplierProfie : Profile {

        public SuppplierProfie()
        {
        CreateMap<Suppliers, SupplierDto>();
        CreateMap<SupplierDto, Suppliers>();




        CreateMap<Suppliers, SupplierCreateDto>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<SupplierCreateDto, Suppliers>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<Suppliers, SupplierUpdateDto>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<SupplierUpdateDto, Suppliers>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
    }



