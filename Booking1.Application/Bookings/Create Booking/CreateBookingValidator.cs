using FluentValidation;

namespace Booking1.Application.Bookings
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.ApartmentId)
                .NotEmpty()
                .WithMessage("ApartmentId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");

            RuleFor(x => x.Start)
                .LessThan(x => x.End)
                .WithMessage("Start date must be before End date.");

            RuleFor(x => x.CleaningFee)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cleaning fee cannot be negative.");

            RuleFor(x => x.AmenitiesUpCharge)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Amenities upcharge cannot be negative.");
        }
    }
}
