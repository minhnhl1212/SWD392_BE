namespace Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class NShopDbContextFactory : IDesignTimeDbContextFactory<NShopDbContext>
{
  public NShopDbContext CreateDbContext(string[] args)
  {
    var configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();

    Console.WriteLine($"Using ConnectionString: {configuration.GetConnectionString("DatabaseConnection")}");

    var optionsBuilder = new DbContextOptionsBuilder<NShopDbContext>();
    optionsBuilder.UseNpgsql(configuration.GetConnectionString("DatabaseConnection") ?? "");

    return new NShopDbContext(optionsBuilder.Options);
  }
}
