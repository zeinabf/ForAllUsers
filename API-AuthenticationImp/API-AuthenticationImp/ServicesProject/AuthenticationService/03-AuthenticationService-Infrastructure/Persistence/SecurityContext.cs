using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
using AuthenticationService._01_AuthenticationService_DomainLayer.User;
using AuthenticationService._03_AuthenticationService_Infrastructure.Persistence.SeedConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._03_AuthenticationService_Infrastructure.Persistence
{
    public class SecurityContext : IdentityDbContext<User, Role, int>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options) {

            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Security");
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
