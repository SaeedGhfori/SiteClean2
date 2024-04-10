using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Persistence.Configurations.ApplicationConfigurations
{
    public class CategoryBrandConfiguration : IEntityTypeConfiguration<CategoryBrand>
    {
        public void Configure(EntityTypeBuilder<CategoryBrand> builder)
        {
            builder.ToTable("CategoryBrands");
            builder.Property(cb => cb.Brand)
                   .IsRequired()
                   .HasMaxLength(100);


            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

        }
    }
}
