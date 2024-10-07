using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Validators
{
    public class UserForRefreshValidator : AbstractValidator<UserForRefreshDto>
    {
        public UserForRefreshValidator()
        {
            RuleFor(_ => _.AccessToken).NotEmpty().WithMessage("Invalid AccessToken");

            RuleFor(_ => _.RefreshToken).NotEmpty().WithMessage("Invalid RefreshToken");
        }
    }
}
