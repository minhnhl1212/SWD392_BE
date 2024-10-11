# be-net

## Cấu trúc:
1. Application:
  - Chứa các logic Business: Controllers, DTOs, Services, Shared,...
2. Core:
  - Chứa các Service tái sử dụng lại trong project: Mail, GCS(Google Cloud Storage), Jwt, ...
3. Domain:
  - Chứa các Entity, Use Case.
4. Infrastructure:
  - Làm việc liên quan tới Database
  - Chứa các Repository, Database Init, Cache

## Các bước bắt đầu build 1 feature:
- Build liệt kê ra các entity liên quan. -> Tạo các repository CRUD cơ bản.
- List ra các API cần thiết cho Controllers(Chỉ cần đơn giản là URL).
- Tạo Service handle các API đó. -> Inject vào controller.
- Trong service Inject repository vào để xử lý. Service sẽ nhận kết quả từ repository để trả response lại.
- Update thêm repository khi cần connect vào database.

## Code mẫu cho Controller
Chỉ cần thay lại tên "Users" thành controller theo mục đính

```
namespace Controllers;

using NShop.src.Application.DTOs.User;
using NShop.src.Application.DTOs.Users;
using NShop.src.Application.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{

  private readonly ILogger<UserController> _logger;
  private readonly IUserService _service;

  public UserController(ILogger<UserController> logger, IUserService service)
  {
    _logger = logger;
    _service = service;
  }

  /// ... HTTP Request ở đây
}

```

### Code mẫu cho Service
- Chỉ cần thay thế tên là thành 1 service khác.
- Và luôn lưu ý cần phải tạo interface cho Service trước để Controller gọi qua interface.
- Inject Service global ở Program.cs để Controller có thể lấy xài.
```
// Services
builder.Services.AddScoped<IUserService, UserService>();
```

```
namespace NShop.src.Application.Service;

using NShop.src.Application.DTOs.Pagination;
using NShop.src.Application.DTOs.User;
using NShop.src.Application.DTOs.Users;
using NShop.src.Application.Services;
using NShop.src.Application.Shared.Enum;
using NShop.src.Application.Shared.Type;
using AutoMapper;
using Azure;
using Database;
using Domain.Users;
using NShop.src.Application.Shared.Constant;
using Microsoft.AspNetCore.Mvc;
using Repository.Users;

public interface IUserService // Tạo interface
{
  ///  ... các API service ở đây
  Task<IActionResult> HandleGetAllAsync(UserQuery query);
  Task<IActionResult> HandleCreateAsync(UserCreateDto dto);
}

public class UserService : BaseService, IUserService // Implement Services
{
  private readonly IUserRepository _repository; // Inject repository

  public UserService(TodoDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx) : base(dbContext, mapper, httpCtx)
  {
    _repository = new UserRepository(dbContext); 
  }

  public async Task<IActionResult> HandleCreateAsync(UserCreateDto dto)
  {
    var user = _mapper.Map<User>(dto);

    user.Status = UserStatusEnum.Active.ToString();
    user.RoleId = new Guid(UserConst.DEFAULT_ROLE_ADMIN_ID);

    await _repository.AddAsync(user);

    return SuccessResp.Created("User created successfully"); // Tra resp bằng object chung SuccessResp
  }

  public async Task<IActionResult> HandleGetAllAsync(UserQuery query)
  {
    var resp = await _repository.GetUserListAsync(query);

    var users = _mapper.Map<IEnumerable<UserDto>>(resp.Users); // Map từ model về Dto

    var result = new
    {
      Data = users,
      Total = resp.Total,
      Page = query.Page,
      PageSize = query.PageSize
    };

    return SuccessResp.Ok(result); // trả resp bằng object chung
  }
}

```

### Code mẫu cho Repository
- Thay tên lại là có thể xử lý được.
- Code Mẫu CRUD cơ bản. Vẫn phải tạo interface trước để Service gọi thông qua interface.
- Inject dbContext vào để xử lý thông qua Entity Framework.
```
public class UserRepository : IUserRepository
{
  private readonly TodoDbContext _dbContext;

  public UserRepository(TodoDbContext dbContext)
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
}
```