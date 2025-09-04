using Booking1.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking1.Application.Apartments.AddApartmentPhotos
{
    public class AparmentPhotoService : IApartmentPhotoService
    {
        private readonly IApartmentPhotoRepository repository;
        private readonly AddApartmentPhotoValidator validator;

        public AparmentPhotoService(IApartmentPhotoRepository repo)
        {
            repository = repo;
            validator = new AddApartmentPhotoValidator();
        }

        public async Task<List<ApartmentPhoto>> ListPhotosAsync(Guid apartmentId)
        {
            return await repository.getPhotosByApartment(apartmentId);
        }

        public async Task<Guid> UploadAsync(Guid apartmentId, AddApartmentPhotoDto dto)
        {

            var result = validator.Validate(dto);
            if (!result.IsValid)
                throw new ValidationException(string.Join("; ", result.Errors));


            var photo = new ApartmentPhoto
            {
                Id = Guid.NewGuid(),
                ApartmentId = apartmentId,
                ImageBase64 = dto.ImageBase64,
                ImageName = dto.ImageName,
                ImageType = dto.ImageType,
                CreatedAt = DateTime.UtcNow
            };


            return await repository.AddAsync(photo);
        }

        public async Task<bool> DeleteAsync(Guid photoId)
        {
            return await repository.DeleteAsync(photoId);
        }

        public async Task<ApartmentPhoto?> GetByIdAsync(Guid photoId)
        {
            return await repository.GetPhotoByIdAsync(photoId);
        }
    }
}
