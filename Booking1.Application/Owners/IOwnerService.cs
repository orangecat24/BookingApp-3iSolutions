using Booking1.Application.Owners.CreateOwner;

namespace Booking1.Application.Owners
{
    public interface IOwnerService
    {
        Task<Guid> RegisterOwnerAsync(CreateOwnerDto createOwnerDto);
    }
}
