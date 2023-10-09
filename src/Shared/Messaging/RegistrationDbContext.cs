using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Shared.Messaging;

/// <summary>
/// Run command from project root : dotnet ef migrations add OutboxInitialMigration --context RegistrationDbContext -o ./Messaging/Migrations --startup-project ../Notifications.Host.Web/Notifications.Host.Web.csproj
/// </summary>
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

