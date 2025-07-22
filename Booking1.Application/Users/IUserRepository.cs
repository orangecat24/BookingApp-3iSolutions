using Booking1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Users
{
    public interface IUserRepository
    {
        Task<Guid> RegisterUserAsync(CreateUserDto createUserDto);
    }
}
