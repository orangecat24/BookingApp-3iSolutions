using Booking1.Domain.Models;

namespace Booking1.Application.Apartments
{
    public interface IApartmentRepository
    {
        Task<Guid> AddAsync(Apartment apartment);
        Task<Apartment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Apartment>> GetAllAsync();
        Task UpdateAsync(Apartment apartment);
        Task DeleteAsync(Apartment apartment);

    }
}
