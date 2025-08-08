using Booking1.Application.Authentication;
using Booking1.Application.Users2;
using System.ComponentModel.DataAnnotations;

namespace ClBooking1.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepo, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        public async Task<string> AuthenticateAsync(LoginDto loginDto)
        {

            var user = await _userRepo.GetByEmailAsync(loginDto.Email);
            if (user == null)
                throw new ValidationException("Invalid email or password.");


            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                throw new ValidationException("Invalid email or password.");


            return _jwtService.GenerateToken(user);
        }
    }
}
