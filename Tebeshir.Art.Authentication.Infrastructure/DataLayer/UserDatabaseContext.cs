

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tebeshir.Art.Authentication.Domain.Models;

namespace Tebeshir.Art.Authentication.DataLayer
{
    public class UserDatabaseContext : IdentityDbContext<User, Role, Guid>
    {
        public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().ToTable("Roles", "Authentication").Property(p => p.Id).HasColumnName("RoleId");
            builder.Entity<User>().ToTable("Users", "Authentication").Property(p => p.Id).HasColumnName("UserId");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "Authentication");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "Authentication");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "Authentication");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "Authentication");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "Authentication");
            
        }
    }
}
