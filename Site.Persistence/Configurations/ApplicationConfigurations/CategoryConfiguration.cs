namespace Site.Persistence.Configurations.ApplicationConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.Property(cb => cb.Name)
                   .HasMaxLength(900)
                   .IsUnicode();


            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
        }
    }
}
