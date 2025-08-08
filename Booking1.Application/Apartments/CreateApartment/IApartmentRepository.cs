namespace Booking1.Application.Apartments.CreateApartment
{
    public interface IApartmentRepository
    {
        Task<Guid> CreateAsync(CreateApartmentDto dto);
    }
}
