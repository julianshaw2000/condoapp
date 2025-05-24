using CondoApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CondoApi.Infrastructure;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Apartment> Apartments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configure your domain entities here

        builder.Entity<Apartment>()
    .HasOne(a => a.Owner)
    .WithMany() // or .WithMany(u => u.Apartments) if reverse navigation exists
    .HasForeignKey(a => a.OwnerId);


    }
}
