using Booking1.Domain.Models;

namespace Booking1.Application.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
