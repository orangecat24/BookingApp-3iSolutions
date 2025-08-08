using Booking1.Domain.Enums;
using FluentValidation;

namespace Booking1.Application.Apartments.CreateApartment
{
    public class CreateApartmentValidation : AbstractValidator<CreateApartmentDto>
    {
        public CreateApartmentValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150)
                .WithMessage("Name must be 150 characters or fewer.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CleaningFee)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cleaning fee cannot be negative.");

            RuleFor(x => x.Amenities)
                .Must(a => a == null || Enum.IsDefined(typeof(Amenity), a.Value))
                .WithMessage("Invalid amenity selection.");
        }
    }
}
