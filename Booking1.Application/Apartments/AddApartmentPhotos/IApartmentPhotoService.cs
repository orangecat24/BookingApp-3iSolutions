using Booking1.Domain.Models;

namespace Booking1.Application.Apartments.AddApartmentPhotos
{
    public interface IApartmentPhotoService
    {
        Task<Guid> UploadAsync(Guid apartmentId, AddApartmentPhotoDto dto);

        Task<List<ApartmentPhoto>> ListPhotosAsync(Guid apartmentId);

        Task<bool> DeleteAsync(Guid photoId);

        Task<ApartmentPhoto?> GetByIdAsync(Guid photoId);

    }
}
