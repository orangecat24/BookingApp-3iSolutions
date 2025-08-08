using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Booking1.Application.Owners.CreateOwner
{
    public class CreateOwnerValidation : AbstractValidator<CreateOwnerDto>
    {
        public CreateOwnerValidation()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("First name is required.")
               .MaximumLength(50).WithMessage("First name must be 50 characters or fewer.")
               .Matches("^[A-Za-z]+$").WithMessage("First name can only contain letters.");

            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must be 50 characters or fewer.")
                .Matches("^[A-Za-z]+$").WithMessage("Last name can only contain letters.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must be 50 characters or fewer.");

            
            RuleFor(x => x.IdentityCardNumber)
                .NotEmpty().WithMessage("Identity Card Number is required.")
                .MaximumLength(20).WithMessage("Identity Card Number must be 20 characters or fewer.");

            RuleFor(x => x.BankAccount)
                .NotEmpty().WithMessage("Bank Account is required.")
                .MaximumLength(30).WithMessage("Bank Account must be 30 characters or fewer.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\+?\d+$").WithMessage("Phone Number must contain only digits (and optional +).")
                .MaximumLength(20).WithMessage("Phone Number must be 20 characters or fewer.");
        }
    }
}
