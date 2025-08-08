namespace Booking1.Application.Apartments.CreateApartment
{
    public interface IApartmentService
    {
        Task<Guid> RegisterApartmentAsync(CreateApartmentDto createApartmentDto);
    }
}
