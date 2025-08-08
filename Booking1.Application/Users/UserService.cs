using Booking1.Application.Users;

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


            createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            return await _userRepository.RegisterUserAsync(createUserDto);
        }




    }
}
