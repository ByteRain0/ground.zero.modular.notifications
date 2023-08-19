using ApplicationRegistry.Data.Models;
using ApplicationRegistry.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Shared.TokenService;

namespace ApplicationRegistry.Data;

public class ApplicationDbContext : DbContext
{
    private readonly ITokenService _tokenService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITokenService tokenService) :
        base(options)
    {
        _tokenService = tokenService;
    }

    public DbSet<ApplicationDataModel> Applications { get; set; }

    public DbSet<EventDataModel> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.SeedApplications();
    }
}
