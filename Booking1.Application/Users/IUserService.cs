using Booking1.Application.Users;
using Booking1.Domain.Models;

namespace Booking1.Application.Users2
{
    public interface IUserService
    {
        Task<Guid> RegisterUserAsync(CreateUserDto createUserDto);
        Task<User?> GetByIdAsync(Guid id);

    }
}
