using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Shared.Messaging;

public class RegistrationDbContext :
    DbContext
{
    public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}

