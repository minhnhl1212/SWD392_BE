namespace Repository.Users;

using NShop.src.Application.DTOs.User;
using NShop.src.Domain.Users;

public interface IUserRepository
{
  Task<User> GetByIdAsync(Guid id);
  Task<IEnumerable<User>> GetAllAsync();
  Task<UserList> GetUserListAsync(UserQuery query);
  Task<User> AddAsync(User user);
  Task<User> UpdateAsync(Guid userId, User user);
  Task<User> DeleteAsync(User user);
  Task<User> GetUserByEmail(string email);
}

public class UserList
{
  public IEnumerable<User> Users { get; set; } = [];
  public int Total { get; set; }
}