using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._01_AuthenticationService_DomainLayer.Role
{
    public class Role : IdentityRole<int>
    {
        //public string URL { get; set; }
        public string? Description { get; set; }
        
    }
}
