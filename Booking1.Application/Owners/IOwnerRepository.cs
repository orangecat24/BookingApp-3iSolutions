using Booking1.Domain.Models;

namespace Booking1.Application.Owners
{
    public interface IOwnerRepository
    {
        Task<Guid> AddAsync(User user, Owner owner);
        Task<User?> GetByEmailAsync(string email);
        Task<Owner?> GetByIdAsync(Guid id);
        Task<IEnumerable<Owner>> GetAllAsync();
        Task UpdateAsync(Owner owner);
        Task DeleteAsync(Owner owner);
    }
}
