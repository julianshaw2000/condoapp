using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CondoApp.Core.Services;
// using CondoApp.Core.Services;

namespace CondoApi.Infrastructure;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly int? _tenantId;
    private readonly ITenantProvider _tenantProvider;

    public AppDbContext(DbContextOptions<AppDbContext> options,
    ITenantProvider tenantProvider
    ) : base(options)
    {

        _tenantProvider = tenantProvider;
        // _tenantId = tenantProvider.GetTenantId();
        // if (int.TryParse(tenantIdString, out var tid))
        //     _tenantId = tid;
        // else
        //     _tenantId = null;
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Apartment> Apartments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configure your domain entities here

        // builder.Entity<Apartment>(entity =>
        // {
        //     entity.HasOne<Tenant>()
        //         .WithMany()
        //         .HasForeignKey(a => a.TenantId)
        //         .OnDelete(DeleteBehavior.Cascade); // if tenant is deleted, apartments are not deleted

        //     entity.HasOne(a => a.Owner)
        //               .WithMany()
        //               .HasForeignKey(a => a.OwnerId)
        //               .OnDelete(DeleteBehavior.SetNull); // if owner is deleted, OwnerId is set to null   

        // });



        builder.Entity<Apartment>(entity =>
        {
            entity.HasIndex(a => a.TenantId);
            entity.HasQueryFilter(a => a.TenantId == _tenantProvider.GetTenantId());
            // entity.HasQueryFilter(a => a.TenantId == 5);
        });





        builder.Entity<Person>(entity =>
        {


            entity.HasIndex(a => a.TenantId);

            entity.HasQueryFilter(a => a.TenantId == _tenantProvider.GetTenantId());
            // entity.HasOne<Tenant>()
            //     .WithMany()
            //     .HasForeignKey(p => p.TenantId)
            //     .OnDelete(DeleteBehavior.Restrict); // if tenant is deleted, person is not deleted

            // entity.HasOne(p => p.Owner)
            //     .WithMany()
            //     .HasForeignKey(p => p.OwnerId)
            //     .OnDelete(DeleteBehavior.Cascade); // if owner is deleted, person is deleted
        });






        builder.Entity<ApplicationUser>(entity =>
        {
            // entity.HasOne<Tenant>()
            //     .WithMany()
            //     .HasForeignKey(a => a.TenantId)
            //     .OnDelete(DeleteBehavior.Cascade); // if tenant is deleted, apartments are not deleted


        });







        // builder.Entity<Visit>(entity =>
        //    {
        //        entity.HasOne<Tenant>()
        //            .WithMany()
        //            .HasForeignKey(v => v.TenantId)
        //            .OnDelete(DeleteBehavior.Restrict);

        //     //    entity.HasOne<Guest>()
        //     //        .WithMany()
        //     //        .HasForeignKey(v => v.GuestId)
        //     //        .OnDelete(DeleteBehavior.Cascade); // if guest is deleted, visits also deleted
        //    });


        // Tenant configuration
        builder.Entity<Tenant>(entity =>
        {
            entity.Property(t => t.TenantName).IsRequired().HasMaxLength(255);
        });



    }
}
