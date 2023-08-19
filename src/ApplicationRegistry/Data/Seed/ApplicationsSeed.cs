using ApplicationRegistry.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationRegistry.Data.Seed;

public static class ApplicationsSeed
{
    public static void SeedApplications(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationDataModel>()
            .HasData(new List<ApplicationDataModel>
            {
                new()
                {
                    Code = "NOTIFICATIONS",
                    Name = "Notifications System",
                    Events = new List<EventDataModel>
                    {
                        new()
                        {
                            Code = "NOTIFICATIONS_WEBHOOK_CREATED",
                            Name = "WebbHook Created",
                            ApplicationCode = "NOTIFICATIONS"
                        },
                        new()
                        {
                            Code = "NOTIFICATIONS_WEBHOOK_REMOVED",
                            Name = "WebbHook Removed",
                            ApplicationCode = "NOTIFICATIONS"
                        }
                    }
                }
            });
    }
}
