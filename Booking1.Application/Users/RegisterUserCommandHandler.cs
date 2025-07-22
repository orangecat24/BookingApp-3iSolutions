using Booking1.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly RegisterUserCommandValidation _validationRules;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validationRules = new RegisterUserCommandValidation();
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validationRules.Validate(request);
            // Just pass the DTO to the repository
            return await _userRepository.RegisterUserAsync(request.CreateUserDto);

        }
    }

}
