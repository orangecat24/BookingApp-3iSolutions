using Booking1.Application.Apartments.CreateApartment;
using Booking1.Domain.Models;

namespace Booking1.Application.Apartments
{
    public interface IApartmentService
    {
        Task<Guid> CreateAsync(CreateApartmentDto dto);
        Task<Apartment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Apartment>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, CreateApartmentDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
