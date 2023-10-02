using Microsoft.EntityFrameworkCore;

namespace Push.Service.Outbox;

public class OutboxDbContext : DbContext
{
    public OutboxDbContext(DbContextOptions<OutboxDbContext> options) :
        base(options)
    {
    }

    public DbSet<OutboxMessageDataModel> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxDbContext).Assembly);
    }
}
