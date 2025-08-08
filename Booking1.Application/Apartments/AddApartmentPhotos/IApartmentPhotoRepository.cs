using Booking1.Domain.Models;

namespace Booking1.Application.Apartments.AddApartmentPhotos
{
    public interface IApartmentPhotoRepository
    {
        Task<Guid> AddAsync(ApartmentPhoto photo);
    }
}
