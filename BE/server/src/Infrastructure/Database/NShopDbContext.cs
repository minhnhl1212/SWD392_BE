namespace Database;

using NShop.src.Application.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using NShop.src.Domain.Users;
using NShop.src.Domain.Role;
using NShop.src.Domain.Store;
using NShop.src.Domain.Product;
using NShop.src.Application.Shared.Constant;
using NShop.src.Domain.Suppliers;
using NShop.src.Domain.Image;
using NShop.src.Domain.Categories;
using NShop.src.Domain.Order;
using NShop.src.Domain.Review;
using NShop.src.Domain.ShoppingCart;
using NShop.src.Domain.CartItems;
using Microsoft.CodeAnalysis;

public class NShopDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Suppliers> Suppliers { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Categories> Categories { get; set; }

    public DbSet<Order> Order { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ShoppingCart> ShoppingCart {  get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    public DbSet<CartItems>  CartItems { get; set; }

    public NShopDbContext(DbContextOptions<NShopDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).IsRequired().HasMaxLength(100);
            e.Property(x => x.Phone).IsRequired(false).HasMaxLength(15);
            e.Property(x => x.Status).IsRequired().HasMaxLength(100);
            e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            e.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
            e.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
        });

        builder.Entity<Role>(e =>
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Name).IsRequired().HasMaxLength(1000);
        e.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
        e.Property(x => x.Status).IsRequired().HasMaxLength(1000);

        // Create some default roles
        e.HasData(new Role { Id = new Guid(UserConst.DEFAULT_ROLE_ADMIN_ID), Name = "Admin", Description = "Admin role", Status = "Active" });
    });

        builder.Entity<Store>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
            e.Property(x => x.Location).IsRequired().HasMaxLength(1000);
            e.Property(x => x.Rating).IsRequired().HasDefaultValue(0);
            e.Property(x => x.Status).IsRequired().HasMaxLength(1000);
            e.Property(x => x.Metadata).IsRequired(false).HasMaxLength(1000);
            e.HasOne(e => e.CreatedUser).WithMany().HasForeignKey(e => e.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            e.HasOne(e => e.UpdatedUser).WithMany().HasForeignKey(e => e.UpdatedBy).OnDelete(DeleteBehavior.NoAction);
            e.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            e.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
            e.HasMany(x => x.Users).WithMany(x => x.Stores);
        });
        builder.Entity<Product>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
            e.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
            e.Property(x => x.Price).IsRequired();
            e.Property(x => x.Status).IsRequired().HasMaxLength(1000);
            e.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            e.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
            e.HasOne(x => x.Categories).WithMany().HasForeignKey(x => x.CategoryId);
            e.HasOne(x => x.Suppliers).WithMany().HasForeignKey(x => x.SupplierId);

        });

        builder.Entity<Image>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Link).IsRequired().HasMaxLength(100); ;
            e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
        });


        builder.Entity<Suppliers>(e =>{
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100); ;
            e.Property(x => x.Address).IsRequired().HasMaxLength(100); ;
            e.Property(x => x.Phone).IsRequired(false).HasMaxLength(100);
            e.Property(x => x.Email).IsRequired().HasMaxLength(100);
            e.Property(x => x.Website).IsRequired(false).HasMaxLength(100);
            e.Property(x => x.Status).IsRequired().HasMaxLength(100);
            e.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            e.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
        });

        builder.Entity<Categories>(e => {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100); ;
            e.Property(x => x.Description).IsRequired().HasMaxLength(100); ;
       
        });

        builder.Entity<Order>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(e => e.Status).IsRequired().HasConversion(
        v => v.ToString(),
        v => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), v));
            e.Property(e => e.TotalPrice).IsRequired().HasDefaultValue(0);
            e.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
            e.HasOne(e => e.CreatedUser).WithMany().HasForeignKey(e => e.CreatedBy).OnDelete(DeleteBehavior.NoAction);
            e.HasOne(e => e.UpdatedUser).WithMany().HasForeignKey(e => e.UpdatedBy).OnDelete(DeleteBehavior.NoAction);
            e.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            e.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
        });
        builder.Entity<Review>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(e => e.Rating).IsRequired().HasDefaultValue(0);
            e.Property(e => e.Content).IsRequired().HasMaxLength(2000);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);

        });

        builder.Entity<Review>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(e => e.Rating).IsRequired().HasDefaultValue(0);
            e.Property(e => e.Content).IsRequired().HasMaxLength(2000);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
        });


        builder.Entity<OrderDetails>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            e.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId);
            e.Property(e => e.Quantity).IsRequired().HasDefaultValue(0);
            e.Property(e => e.Price).IsRequired().HasDefaultValue(0);
           e.Property(e => e.Discount).IsRequired().HasDefaultValue(0);
        });

        builder.Entity<CartItems>(e =>
        {
        e.HasKey(x => new {x.CartId, x.ProductId });
            e.HasOne(x => x.ShoppingCart).WithMany().HasForeignKey(x => x.CartId);
            e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);

            e.Property(e => e.Quantity).IsRequired().HasDefaultValue(0);
        });






    }
}