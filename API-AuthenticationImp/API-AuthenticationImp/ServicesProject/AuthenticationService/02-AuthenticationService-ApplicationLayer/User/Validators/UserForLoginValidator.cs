using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Validators
{
    public class UserForLoginValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginValidator()
        {
            RuleFor(_ => _.UserName).NotEmpty().WithMessage("Invalid UserName");

            RuleFor(_ => _.Password)
                .NotEmpty()
                .WithMessage("Your password cannot be empty")
                //.MaximumLength(16)
                //.WithMessage("Invalid Password")
                //.Matches(@"[A-Z]+")
                //.WithMessage("Invalid Password")
                //.Matches(@"[a-z]+")
                //.WithMessage("Invalid Password")
                //.Matches(@"[0-9]+")
                //.WithMessage("Invalid Password")
                //.Matches(@"[\@\!\?\*\.]+")
                .WithMessage("Invalid Password");
        }
    }
}
