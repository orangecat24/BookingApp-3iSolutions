using Booking1.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);


            createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            return await _userRepository.RegisterUserAsync(createUserDto);
        }




    }
}
