using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._03_AuthenticationService_Infrastructure.Persistence.SeedConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.HasData(
            //    new Role
            //    {
            //        Id = 1,
            //        Name = "Admin",
            //        NormalizedName = "ADMIN",
            //        Description = "Admin Role",
            //    },
            //    new Role
            //    {
            //        Id = 2,
            //        Name = "Manager",
            //        NormalizedName = "MANAGER",
            //        Description = "Manager Role is type of user roles",
            //    }
            //    );
        }
    }
}
