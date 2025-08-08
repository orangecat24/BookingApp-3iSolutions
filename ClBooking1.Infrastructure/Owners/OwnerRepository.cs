using Booking1.Application.Owners;
using Booking1.Application.Owners.CreateOwner;
using Booking1.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClBooking1.Infrastructure.Owners
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;

        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> RegisterOwnerAsync(CreateOwnerDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Country = dto.Country,
                CreatedOnUtc = DateTime.UtcNow
            };

            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                IdentityCardNumber = dto.IdentityCardNumber,
                BankAccount = dto.BankAccount,
                PhoneNumber = dto.PhoneNumber,
                User = user
            };

            _context.Users.Add(user);
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return owner.Id;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
