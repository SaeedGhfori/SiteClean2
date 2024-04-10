using Site.Domain.Attributes;
using Site.Persistence.Configurations.ApplicationConfigurations;
using Site.Persistence.Seeds.ApplicationSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Persistence.Contexts.ApplicationContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Auditable
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemoved").HasDefaultValue(false);
                }
            }

            #region Configuration

            builder.ApplyConfiguration(new CategoryBrandConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new InvoiceConfiguration());

            #endregion

            //Seed Data
            ApplicationContextSeed.SeedData(builder);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted
                );
            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var updated = entityType.FindProperty("UpdateTime");
                    var RemoveTime = entityType.FindProperty("RemoveTime");
                    var IsRemoved = entityType.FindProperty("IsRemoved");
                    if (item.State == EntityState.Added && inserted != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                    }
                    if (item.State == EntityState.Modified && updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && RemoveTime != null && IsRemoved != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemoved").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }

            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                            p.State == EntityState.Added ||
                            p.State == EntityState.Deleted);

            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var updated = entityType.FindProperty("UpdateTime");
                    var RemoveTime = entityType.FindProperty("RemoveTime");
                    var IsRemoved = entityType.FindProperty("IsRemoved");

                    if (item.State == EntityState.Added && inserted != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Modified && updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && RemoveTime != null && IsRemoved != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemoved").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
