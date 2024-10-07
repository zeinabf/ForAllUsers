using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.Role.Dto
{
    public sealed record UserRoleRegistrationDto(
     int userid,
        int roleid

);
}
