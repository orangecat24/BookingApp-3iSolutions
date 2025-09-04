using FluentValidation;

namespace Booking1.Application.Bookings.UpdateBooking
{
    public class UpdateBookingValidator : AbstractValidator<UpdateBookingDto>
    {
        public UpdateBookingValidator()
        {
            RuleFor(x => x.Start)
                .LessThan(x => x.End)
                .WithMessage("Start date must be before End date.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid booking status.");
        }


    }
}
