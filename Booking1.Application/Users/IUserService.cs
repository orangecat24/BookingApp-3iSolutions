using Booking1.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Users2
{
    public interface IUserService
    {
        Task<Guid> RegisterUserAsync(CreateUserDto createUserDto);
    }
}
