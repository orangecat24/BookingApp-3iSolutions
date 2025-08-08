using Booking1.Application.Owners.CreateOwner;
using Booking1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Owners
{
    public interface IOwnerRepository
    {
        Task<Guid> RegisterOwnerAsync(CreateOwnerDto createOwnerDto);
        Task<User?> GetByEmailAsync(string email);
    }
}
