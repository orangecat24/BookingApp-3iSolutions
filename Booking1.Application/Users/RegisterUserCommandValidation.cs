using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Users
{
    public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidation()
        {
            RuleFor(x => x.CreateUserDto.FirstName)
                .NotEmpty()
                .WithMessage("FirstName cnanot be empty.");
           
            RuleFor(x => x.CreateUserDto.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
           
            RuleFor(x => x.CreateUserDto.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(10)
                .WithMessage("Password must be at least 6 characters long.")
                .Matches("[0-11]");

        }
    }
}
