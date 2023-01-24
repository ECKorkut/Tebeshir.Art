using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tebeshir.Art.Authentication.DataLayer;
using Tebeshir.Art.Authentication.Domain.Models;

namespace Tebeshir.Art.Authentication.Infrastructure.DataLayer
{
    public class UserContextSeed
    {
        public static async Task SeedRolesAsync(UserDatabaseContext userContextContext, RoleManager<Role> roleManager)
        {

            if (!userContextContext.Roles.Any())
            {

                var role = new Role("Admin");


                await roleManager.CreateAsync(role);

                await roleManager.CreateAsync(new Role("User"));
            }
        }
        public static async Task SeedUsersAsync(UserDatabaseContext userContextContext,UserManager<User> userManager)
        {
            
            if (!userContextContext.Users.Any())
            {
                
                var user = new User
                {
                    UserName = "Admin",
                    Email = "admin@tebeshir.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "123654");

                await userManager.AddToRoleAsync(user, UserRoles.Admin);
                
                var claim = new Claim("Admin", "Admin");

                await userManager.AddClaimAsync(user, claim);
                
            }
        }
    }
}
