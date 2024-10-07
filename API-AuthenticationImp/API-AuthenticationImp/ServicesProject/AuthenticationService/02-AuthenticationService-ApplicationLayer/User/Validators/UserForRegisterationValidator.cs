using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Validators
{
    public class UserForRegistrationValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationValidator()
        {
            RuleFor(_ => _.FirstName).NotEmpty();

            RuleFor(_ => _.LastName).NotEmpty();

            RuleFor(_ => _.Email).NotEmpty().EmailAddress();

            RuleFor(_ => _.UserName)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("UserName length must be minimum 6")
                .MaximumLength(30)
                .WithMessage("UserName length must not exceed 30");

            RuleFor(_ => _.Password)
                .NotEmpty()
                .WithMessage("Your password cannot be empty")
                .MinimumLength(6)
                .WithMessage("Your password length must be at least 6.")
                .MaximumLength(16)
                .WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+")
                .WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+")
                .WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+")
                .WithMessage("Your password must contain at least one number.")
                .Matches(@"[\@\!\?\*\.]+")
                .WithMessage("Your password must contain at least one (@!? *.).");

            // RuleFor(_ => _.ConfirmPassword)
            //     .Equal(_ => _.Password)
            //     .WithMessage("ConfirmPassword must equal Password");
        }
    }
}
