using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Persistence.Configurations.ApplicationConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.Property(cb => cb.Title)
              .IsRequired()
               .HasMaxLength(100);


            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
        }
    }
}
