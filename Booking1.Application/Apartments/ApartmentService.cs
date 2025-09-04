using Booking1.Application.Apartments.CreateApartment;
using Booking1.Domain.Models;
using FluentValidation;

namespace Booking1.Application.Apartments
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        private readonly IValidator<CreateApartmentDto> _validator;

        public ApartmentService(IApartmentRepository apartmentRepository, IValidator<CreateApartmentDto> validator)
        {
            _apartmentRepository = apartmentRepository;
            _validator = validator;
        }

        public async Task<Guid> CreateAsync(CreateApartmentDto dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ArgumentException(string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));
            }

            var apartment = new Apartment
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                Description = dto.Description,
                CleaningFee = dto.CleaningFee,
                Amenities = dto.Amenities
            };

            await _apartmentRepository.AddAsync(apartment);
            return apartment.Id;
        }

        public async Task<Apartment?> GetByIdAsync(Guid id)
        {
            return await _apartmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Apartment>> GetAllAsync()
        {
            return await _apartmentRepository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, CreateApartmentDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                throw new ArgumentException(
                    string.Join(", ", validation.Errors.Select(e => e.ErrorMessage))
                );
            }

            var existing = await _apartmentRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = dto.Name;
            existing.Address = dto.Address;
            existing.Price = dto.Price;
            existing.Description = dto.Description;
            existing.CleaningFee = dto.CleaningFee;
            existing.Amenities = dto.Amenities;
            existing.LastBookedOnUtc = existing.LastBookedOnUtc;

            await _apartmentRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);
            if (apartment == null)
            {
                return false;
            }

            await _apartmentRepository.DeleteAsync(apartment);
            return true;
        }
    }

}
