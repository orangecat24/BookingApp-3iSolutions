using Booking1.Application.Owners.CreateOwner;

namespace Booking1.Application.Owners
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly CreateOwnerValidation _validator;


        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
            _validator = new CreateOwnerValidation();
        }

        public async Task<Guid> RegisterOwnerAsync(CreateOwnerDto dto)
        {

            var validation = _validator.Validate(dto);

            var existing = await _ownerRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("user with this email already exists.");
            }

            dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);


            return await _ownerRepository.RegisterOwnerAsync(dto);
        }
    }

}





