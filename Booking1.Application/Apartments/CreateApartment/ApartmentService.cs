using System.ComponentModel.DataAnnotations;

namespace Booking1.Application.Apartments.CreateApartment
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository repository;
        private readonly CreateApartmentValidation validator;

        public ApartmentService(IApartmentRepository repo)
        {
            repository = repo;
            validator = new CreateApartmentValidation();
        }

        public async Task<Guid> RegisterApartmentAsync(CreateApartmentDto dto)
        {
            var result = validator.Validate(dto);
            if (!result.IsValid)
                throw new ValidationException(string.Join("; ", result.Errors));

            return await repository.CreateAsync(dto);
        }


    }
}
