using Booking1.Application.Authentication;
using Booking1.Application.Owners.CreateOwner;
using Booking1.Application.Users;
using Booking1.Application.Users2;
using Booking1.Domain.Enums;
using Booking1.Domain.Models;

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
            var user = await _userRepo.GetByEmailAsync(loginDto.Email.Trim().ToLowerInvariant());
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return _jwtService.GenerateToken(user);
        }


        public async Task<Guid> RegisterUserAsync(CreateUserDto dto)
        {
            var existing = await _userRepo.GetByEmailAsync(dto.Email.Trim().ToLowerInvariant());
            if (existing != null)
                throw new ArgumentException("Email is already registered.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email.Trim().ToLowerInvariant(),
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Country = dto.Country,
                CreatedOnUtc = DateTime.UtcNow,
                Role = UserRole.Guest   // 👈 set role
            };

            await _userRepo.AddAsync(user);
            return user.Id;
        }

        public async Task<Guid> RegisterOwnerAsync(CreateOwnerDto dto)
        {
            var existing = await _userRepo.GetByEmailAsync(dto.Email.Trim().ToLowerInvariant());
            if (existing != null)
                throw new ArgumentException("Email is already registered.");

            var owner = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email.Trim().ToLowerInvariant(),
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Country = dto.Country,
                CreatedOnUtc = DateTime.UtcNow,
                Role = UserRole.Owner
            };

            await _userRepo.AddAsync(owner);
            return owner.Id;
        }
    }
}
