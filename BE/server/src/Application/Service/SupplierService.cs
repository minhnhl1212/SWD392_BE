namespace NShop.src.Application.Service;

using AutoMapper;
using Database;
using Microsoft.AspNetCore.Mvc;
using NShop.src.Application.DTOs.Supplier;

using NShop.src.Application.Services;
using NShop.src.Application.Shared.Type;
using NShop.src.Domain.Product;
using NShop.src.Domain.Suppliers;
using NShop.src.Domain.Users;
using NShop.src.Infrastructure.Repository.Suppliers;
using NuGet.Protocol.Core.Types;


public interface ISupplierService
    {

     Task<IActionResult> HandleCreate(SupplierCreateDto dto);
        Task<IActionResult> HandleDelete(Guid id);
        Task<IActionResult> HandleUpdate(Guid id, SupplierUpdateDto dto);
         Task<IActionResult> HandleGetById(Guid id);
}

public class SupplierService : BaseService, ISupplierService
{
    private readonly ISuppliersRepository _supplierrepo;

    public SupplierService(NShopDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx) : base(dbContext, mapper, httpCtx)
    {
        _supplierrepo = new SuppliersRepository(dbContext);
    }
    public async Task<IActionResult> HandleCreate(SupplierCreateDto dto)
    {
        var suppliers = _mapper.Map<Suppliers>(dto);
        await _supplierrepo.AddAsync(suppliers);

        return SuccessResp.Created("User created successfully");

    }

    public async Task<IActionResult> HandleDelete(Guid id)
    {
        var supplier = _supplierrepo.GetSuppliersbyId(id);
        if (supplier == null)
        {

            return ErrorResp.NotFound("Supplier not found");
        }
        await _supplierrepo.DeleteAsync(id);

        return SuccessResp.Ok("Success");


    }

    public  async Task<IActionResult> HandleGetById(Guid id)
    {
        var supplier = await _supplierrepo.GetSuppliersbyId(id);

        if (supplier == null)
        {
            return ErrorResp.NotFound("Supplier not found");
        }

        var result = _mapper.Map<SupplierDto>(supplier);

        return SuccessResp.Ok(result);
    }

    public async Task<IActionResult> HandleUpdate(Guid id, SupplierUpdateDto dto)
    {
        var supplier = await _supplierrepo.GetSuppliersbyId(id);
        if (supplier == null)
        {
            return ErrorResp.NotFound("Supplier not found");
        }
          _mapper.Map(dto, supplier);

        await _supplierrepo.UpdateAsync(id, supplier);
        return  SuccessResp.Ok("Success");
    }
}
