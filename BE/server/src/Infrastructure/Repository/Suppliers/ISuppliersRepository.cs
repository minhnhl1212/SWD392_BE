namespace NShop.src.Infrastructure.Repository.Suppliers;
using NShop.src.Domain.Suppliers;

    public interface ISuppliersRepository
    {

    Task<Suppliers> GetSuppliersbyId(Guid Id);
    Task<Suppliers> AddAsync(Suppliers suppliers);
    Task<Suppliers> UpdateAsync(Guid Id, Suppliers suppliers);
    Task<Suppliers> DeleteAsync(Guid Id);
}

