using Booking1.Domain.Models;

namespace Booking1.Application.Users2
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
    }
}
