using ApplicationRegistry.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Data.Configuration;

public class EventDataModelConfiguration : IEntityTypeConfiguration<EventDataModel>
{
    public void Configure(EntityTypeBuilder<EventDataModel> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasOne<ApplicationDataModel>(x => x.Application)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.ApplicationCode);
    }
}
