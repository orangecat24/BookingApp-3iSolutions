using Booking1.Application.Apartments.CreateApartment;
using Booking1.Domain.Models;

namespace ClBooking1.Infrastructure.Apartments
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _context;

        public ApartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateApartmentDto dto)
        {
            var apt = new Apartment
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                Description = dto.Description,
                CleaningFee = dto.CleaningFee,
                Amenities = dto.Amenities,
                LastBookedOnUtc = null
            };

            _context.Apartments.Add(apt);
            await _context.SaveChangesAsync();
            return apt.Id;
        }
    }
}
