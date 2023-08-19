using ApplicationRegistry.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationRegistry.Data.Configuration;

public class ApplicationDataModelConfiguration : IEntityTypeConfiguration<ApplicationDataModel>
{
    public void Configure(EntityTypeBuilder<ApplicationDataModel> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany<EventDataModel>(x => x.Events)
            .WithOne(x => x.Application)
            .HasForeignKey(x => x.ApplicationCode);
    }
}
