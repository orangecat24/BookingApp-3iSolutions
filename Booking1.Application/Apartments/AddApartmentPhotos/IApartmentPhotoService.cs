namespace Booking1.Application.Apartments.AddApartmentPhotos
{
    public interface IApartmentPhotoService
    {
        Task<Guid> UploadAsync(Guid apartmentId, AddApartmentPhotoDto dto);
    }
}
