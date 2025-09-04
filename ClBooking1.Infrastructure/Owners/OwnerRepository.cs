using Booking1.Application.Owners;
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

        public async Task<Guid> AddAsync(User user, Owner owner)
        {
            _context.Users.Add(user);
            _context.Owners.Add(owner);

            await _context.SaveChangesAsync();
            return owner.Id;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var normalized = email.Trim().ToLowerInvariant();
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalized);
        }

        public async Task<Owner?> GetByIdAsync(Guid id)
        {
            return await _context.Owners
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners
                .Include(o => o.User)
                .ToListAsync();
        }

        public async Task UpdateAsync(Owner owner)
        {
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Owner owner)
        {
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
        }
    }
}
