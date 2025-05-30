using CondoApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CondoApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // 🟢 These properties will be assigned per request (via scoped injection in Program.cs)!
        public int? CurrentTenantId { get; set; }
        public bool IsSuperAdmin { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🟢 Global query filter for Apartment
            builder.Entity<Apartment>(entity =>
            {
                entity.HasIndex(a => a.TenantId);
                entity.HasQueryFilter(a =>
                    IsSuperAdmin || (CurrentTenantId != null && a.TenantId == CurrentTenantId));
            });

            // 🟢 Global query filter for Person
            builder.Entity<Person>(entity =>
            {
                entity.HasIndex(p => p.TenantId);
                entity.HasQueryFilter(p =>
                    IsSuperAdmin || (CurrentTenantId != null && p.TenantId == CurrentTenantId));
            });

            // 🟢 Tenant configuration
            builder.Entity<Tenant>(entity =>
            {
                entity.Property(t => t.TenantName).IsRequired().HasMaxLength(255);
            });

            // ⚠️ Additional entity configurations can be uncommented and used as needed
        }
    }
}
