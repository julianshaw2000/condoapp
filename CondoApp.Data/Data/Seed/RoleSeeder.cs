using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CondoApp.Core.Enums;
using CondoApp.Core.Entities;

namespace CondoApp.Data.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = Enum.GetNames(typeof(UserRole));

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager)
        {
            const string superAdminEmail = "superadmin@condojam.com";
            const string superAdminPassword = "Gl@ria100";

            var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);
            if (superAdminUser == null)
            {
                superAdminUser = new ApplicationUser
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(superAdminUser, superAdminPassword);
            }

            if (!await userManager.IsInRoleAsync(superAdminUser, UserRole.SUPERADMIN.ToString()))
            {
                await userManager.AddToRoleAsync(superAdminUser, UserRole.SUPERADMIN.ToString());
            }
        }




    }
}