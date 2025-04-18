using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class Context : DbContext
{
    private readonly IConfiguration _configuration;

    public Context(DbContextOptions<Context> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<HistoricalQuery> HistoricalQueries { get; set; }
    public virtual DbSet<ApiKey> ApiKeys { get; set; }
    public virtual DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(c => c.Id);  // Define Id (User) as the primary key

        modelBuilder.Entity<User>().Property(u => u.UserName)
            .HasMaxLength(50)   // Define UserName (User) as MaxLength=50
            .IsRequired();

        modelBuilder.Entity<User>().Property(u => u.Password)
          .HasMaxLength(255)    // Define Password (User) as MaxLength=255
          .IsRequired();

        modelBuilder.Entity<City>()
            .HasKey(c => c.Id);  // Define Id (City) as the primary key

        modelBuilder.Entity<HistoricalQuery>()
            .HasKey(c => c.Id);  // Define Id (HistoricalQuery) as the primary key

        modelBuilder.Entity<ApiKey>()
            .HasKey(c => c.Id);  // Define Id (ApiKey) as the primary key
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    // Use IConfiguration to get the connection string
    //    var connectionString = _configuration.GetConnectionString("DbConnection");
    //    optionsBuilder.UseOracle(connectionString);
    //}
}
