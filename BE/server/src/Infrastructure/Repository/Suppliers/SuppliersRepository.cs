namespace NShop.src.Infrastructure.Repository.Suppliers;

using Database;
using Microsoft.EntityFrameworkCore;
using NShop.src.Domain.Suppliers;
using NuGet.Protocol;

public class SuppliersRepository : ISuppliersRepository
    {
        private readonly NShopDbContext _dbContext;

        public SuppliersRepository(NShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Domain.Suppliers.Suppliers> AddAsync(Domain.Suppliers.Suppliers suppliers)
        {

        suppliers.Status = "Active";
        suppliers.CreatedAt = DateTime.UtcNow;
        suppliers.UpdatedAt = DateTime.UtcNow;
        var result = _dbContext.Suppliers.Add(suppliers);
        _dbContext.SaveChanges();
        return result.Entity;
    }

        public async Task<Domain.Suppliers.Suppliers> DeleteAsync(Guid id)
        {
            var result =  await  _dbContext.Suppliers.FindAsync(id);
              if (result == null) 
            return null;
        // Save changes
            result.Status = "deleted";
            var i = _dbContext.Update(result);
            await _dbContext.SaveChangesAsync();
            return i.Entity;
        }

    public  async Task<Suppliers> GetSuppliersbyId(Guid Id)
    {
      var resut = await _dbContext.Suppliers.FindAsync(Id);

        return resut;
    }

    public async Task<Domain.Suppliers.Suppliers> UpdateAsync(Guid Id, Domain.Suppliers.Suppliers suppliers)
    {
        var result = await _dbContext.Suppliers.FindAsync(Id);

        if (result == null)

            return null;

        var u = _dbContext.Update(suppliers);
        await _dbContext.SaveChangesAsync();



        return u.Entity;
    }
          
        }
    
