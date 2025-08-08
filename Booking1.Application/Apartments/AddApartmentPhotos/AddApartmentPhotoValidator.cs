using FluentValidation;

namespace Booking1.Application.Apartments.AddApartmentPhotos
{
    public class AddApartmentPhotoValidator : AbstractValidator<AddApartmentPhotoDto>
    {
        public AddApartmentPhotoValidator()
        {
            RuleFor(x => x.ImageBase64)
                .NotEmpty().WithMessage("Image data is required.")
                .Must(base64 =>
                {
                    try
                    {
                        Convert.FromBase64String(base64);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage("Invalid Base64 string.");

            RuleFor(x => x.ImageName)
                .NotEmpty().WithMessage("Image name is required.")
                .MaximumLength(100).WithMessage("Image name too long.");

            RuleFor(x => x.ImageType)
                .NotEmpty().WithMessage("Image type is required.")
                .Must(t => t.Equals("image/png", StringComparison.OrdinalIgnoreCase)
                        || t.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Only JPEG or PNG images are supported.");
        }
    }
}
