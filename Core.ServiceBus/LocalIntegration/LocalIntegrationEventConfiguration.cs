using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.LocalIntegration
{
    public class LocalIntegrationEventConfiguration : IEntityTypeConfiguration<LocalIntegrationEvent>
    {
        public void Configure(EntityTypeBuilder<LocalIntegrationEvent> builder)
        {
            builder.ToTable("LocalIntegrationEvents");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
               .UseHiLo("localIntegrationEventseq");

            builder.Property(e => e.BinaryBody).IsRequired();
            builder.Property(e => e.JsonBoby).IsRequired();
            builder.Property(e => e.ModelName)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.ModelNamespace)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(e => e.CreatedAt).HasColumnType("datetime2").IsRequired();
        }
    }
}
