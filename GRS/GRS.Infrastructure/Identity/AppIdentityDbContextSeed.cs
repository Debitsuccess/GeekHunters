using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRS.Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        private const string adminUser = "admin";
        private const string adminPassword = "P@ssword1";

        public static async Task SeedAsync(IApplicationBuilder app)
        {
            UserManager<ApplicationUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new ApplicationUser(adminUser);
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
