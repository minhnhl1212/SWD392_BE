namespace Repository.Users;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NShop.src.Application.DTOs.User;
using Database;
using NShop.src.Domain.Users;

public class UserRepository : IUserRepository
{
  private readonly NShopDbContext _dbContext;

  public UserRepository(NShopDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<User> AddAsync(User user)
  {

    // Add user to database
    var result = await _dbContext.Users.AddAsync(user);

    // Save changes
    await _dbContext.SaveChangesAsync();

    return result.Entity;
  }

  public async Task<User> DeleteAsync(User user)
  {

    // Remove user from database
    var result = _dbContext.Users.Remove(user);

    // Save changes
    _dbContext.SaveChanges();

    return result.Entity;
  }

  public async Task<IEnumerable<User>> GetAllAsync()
  {
    // Get all users from database
    var users = _dbContext.Users.AsEnumerable();

    return users;
  }

  public async Task<UserList> GetUserListAsync(UserQuery query)
  {
    string searchKeyword = query.SearchKeyword ?? "";
    int page = query.Page < 0 ? 0 : query.Page;
    int pageSize = query.PageSize <= 0 ? 10 : query.PageSize;

    var q = _dbContext.Users
      .Where(u => u.Email.Contains(searchKeyword) || u.FullName.Contains(searchKeyword))
      .AsQueryable();

    var total = q.Count();

    var users = q
      .Skip(page * pageSize)
      .Take(pageSize)
      .ToList();

    return new UserList { Users = users, Total = total };
  }

  public async Task<User> GetByIdAsync(Guid id)
  {
    var user = await _dbContext.Users.FindAsync(id);

    return user;
  }

  public async Task<User> UpdateAsync(Guid userId, User user)
  {

    // Update user in database
    var u = await _dbContext.Users.FindAsync(userId);

    if (u == null) return null;

    var result = _dbContext.Users.Update(user);
    // Save changes
    await _dbContext.SaveChangesAsync();

    return result.Entity;
  }

  public async Task<User> GetUserByEmail(string email)
  {
    var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

    return user;
  }
}