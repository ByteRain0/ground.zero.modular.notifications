using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Push.Service.Outbox;

internal class OutboxDataModelConfiguration : IEntityTypeConfiguration<OutboxMessageDataModel>
{
    public void Configure(EntityTypeBuilder<OutboxMessageDataModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Body)
            .HasColumnType("json")
            .IsRequired();

        builder.Property(x => x.Header)
            .HasColumnType("json")
            .IsRequired();

        builder.Property(x => x.Header);

        builder.Property(x => x.OccuredOn)
            .IsRequired();
    }
}
