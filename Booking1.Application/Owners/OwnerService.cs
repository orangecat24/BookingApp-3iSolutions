using Booking1.Application.Owners.CreateOwner;
using Booking1.Domain.Enums;
using Booking1.Domain.Models;
using FluentValidation;

namespace Booking1.Application.Owners
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IValidator<CreateOwnerDto> _validator;

        public OwnerService(IOwnerRepository ownerRepository, IValidator<CreateOwnerDto> validator)
        {
            _ownerRepository = ownerRepository;
            _validator = validator;
        }

        public async Task<Guid> RegisterOwnerAsync(CreateOwnerDto dto)
        {

            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }


            var existingUser = await _ownerRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }


            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email.ToLowerInvariant(),
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Country = dto.Country,
                CreatedOnUtc = DateTime.UtcNow,
                Role = UserRole.Owner
            };

            // create owner entity
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                IdentityCardNumber = dto.IdentityCardNumber,
                BankAccount = dto.BankAccount,
                PhoneNumber = dto.PhoneNumber,
                User = user
            };

            return await _ownerRepository.AddAsync(user, owner);
        }

        public async Task<Owner?> GetByIdAsync(Guid id)
        {
            return await _ownerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _ownerRepository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, Owner updatedOwner)
        {
            var existing = await _ownerRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.IdentityCardNumber = updatedOwner.IdentityCardNumber;
            existing.BankAccount = updatedOwner.BankAccount;
            existing.PhoneNumber = updatedOwner.PhoneNumber;

            await _ownerRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            if (owner == null)
            {
                return false;
            }

            await _ownerRepository.DeleteAsync(owner);
            return true;
        }
    }

}





