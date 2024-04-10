using Site.Persistence.Configurations.IdentityConfigurations;
using Site.Persistence.Seeds.IdentitySeed;
using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Persistence.Contexts.IdentityContext
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //schema Sql
            builder.Entity<IdentityUser<string>>().ToTable("Users", "identity");
            builder.Entity<IdentityRole<string>>().ToTable("Roles", "identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "identity");


            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(p => new { p.LoginProvider, p.ProviderKey });
            builder.Entity<IdentityUserRole<string>>()
                .HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserToken<string>>()
                .HasKey(p => new { p.UserId, p.LoginProvider, p.Name });


            //Configuration
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            //Seed Data
            IdentityDbContextSeed.SeedData(builder);

        }
    }
}
