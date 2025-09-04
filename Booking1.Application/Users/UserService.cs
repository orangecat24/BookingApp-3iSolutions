using Booking1.Application.Users;
using Booking1.Domain.Enums;
using Booking1.Domain.Models;

namespace Booking1.Application.Users2
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly CreateUserValidation _validator;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validator = new CreateUserValidation();
        }

        public async Task<Guid> RegisterUserAsync(CreateUserDto createUserDto)
        {

            var result = _validator.Validate(createUserDto);
            if (!result.IsValid)
            {
                throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
            }


            var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User wit this email already exists.");
            }


            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email.ToLowerInvariant(),
                Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                Country = createUserDto.Country,
                CreatedOnUtc = DateTime.UtcNow,
                Role = UserRole.Guest
            };


            await _userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

    }
}
