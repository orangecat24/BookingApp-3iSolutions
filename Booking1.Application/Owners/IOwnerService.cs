using Booking1.Application.Owners.CreateOwner;
using Booking1.Domain.Models;

namespace Booking1.Application.Owners
{
    public interface IOwnerService
    {
        Task<Guid> RegisterOwnerAsync(CreateOwnerDto dto);
        Task<Owner?> GetByIdAsync(Guid id);
        Task<IEnumerable<Owner>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, Owner owner);
        Task<bool> DeleteAsync(Guid id);
    }
}
