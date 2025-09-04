using Booking1.Application.Apartments;
using Booking1.Application.Bookings.UpdateBooking;
using Booking1.Domain.Enums;
using Booking1.Domain.Models;
using FluentValidation;

namespace Booking1.Application.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        private readonly IApartmentRepository apartmentRepository;

        private readonly IValidator<CreateBookingDto> createValidator;

        private readonly IValidator<UpdateBookingDto> updateValidator;

        public BookingService(IBookingRepository bookingRepository, IApartmentRepository apartmentRepository, IValidator<CreateBookingDto> createValidator, IValidator<UpdateBookingDto> updateValidator)
        {
            this.bookingRepository = bookingRepository;
            this.apartmentRepository = apartmentRepository;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
        }

        public async Task<Guid> CreateAsync(CreateBookingDto dto)
        {
            var validation = createValidator.Validate(dto);
            if (!validation.IsValid)
                throw new ArgumentException(string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));

            var apartment = await apartmentRepository.GetByIdAsync(dto.ApartmentId);
            if (apartment == null)
                throw new ArgumentException("Apartment not found.");

            var nights = (dto.End - dto.Start).Days;
            if (nights <= 0)
                throw new ArgumentException("Booking must be at least 1 night.");

            var existing = await bookingRepository.GetByApartmentAsync(dto.ApartmentId);

            foreach (var b in existing)
            {
                bool isActive = b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending;
                bool overlaps = dto.Start < b.End && dto.End > b.Start;

                if (isActive && overlaps)
                {
                    throw new InvalidOperationException("Apartment not available for selected dates.");
                }
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                ApartmentId = dto.ApartmentId,
                UserId = dto.UserId,
                Start = dto.Start,
                End = dto.End,
                CleaningFee = apartment.CleaningFee,
                AmenitiesUpCharge = dto.AmenitiesUpCharge,
                PriceForPeriod = nights * apartment.Price,
                Status = BookingStatus.Pending,
                CreatedOnUtc = DateTime.UtcNow
            };

            booking.TotalPrice = booking.PriceForPeriod + booking.CleaningFee + booking.AmenitiesUpCharge;

            await bookingRepository.AddAsync(booking);
            return booking.Id;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await bookingRepository.GetByIdAsync(id);
        }


        public async Task<List<Booking>> GetAllAsync()
        {
            return await bookingRepository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateBookingDto dto)
        {
            var booking = await bookingRepository.GetByIdAsync(id);
            if (booking == null) return false;

            var validation = updateValidator.Validate(dto);
            if (!validation.IsValid)
                throw new ArgumentException(string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));

            booking.Start = dto.Start;
            booking.End = dto.End;

            if (dto.Status == BookingStatus.Canceled)
            {
                booking.Status = BookingStatus.Canceled;
                booking.RejectedOnUtc = DateTime.UtcNow;
            }
            else if (dto.Status == BookingStatus.Confirmed)
            {
                booking.Status = BookingStatus.Confirmed;
            }
            else if (dto.Status == BookingStatus.Completed)
            {
                booking.Status = BookingStatus.Completed;
            }
            else if (dto.Status == BookingStatus.Pending)
            {
                booking.Status = BookingStatus.Pending;
            }

            var apartment = await apartmentRepository.GetByIdAsync(booking.ApartmentId);
            var nights = (dto.End - dto.Start).Days;

            booking.PriceForPeriod = nights > 0 ? nights * apartment.Price : 0;
            booking.TotalPrice = booking.PriceForPeriod + booking.CleaningFee + booking.AmenitiesUpCharge;

            return await bookingRepository.UpdateAsync(booking);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await bookingRepository.DeleteAsync(id);
        }


        public async Task<bool> CheckAvailabilityAsync(Guid apartmentId, DateTime start, DateTime end)
        {

            var bookings = await bookingRepository.GetByApartmentAsync(apartmentId);


            foreach (var booking in bookings)
            {

                bool isActive = booking.Status == BookingStatus.Confirmed || booking.Status == BookingStatus.Pending;

                if (isActive)
                {

                    bool overlaps = start < booking.End && end > booking.Start;

                    if (overlaps)
                    {

                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> ConfirmBookingAsync(Guid bookingId, Guid ownerId)
        {
            var booking = await bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            if (!IsOwnerOfApartment(booking, ownerId)) return false;

            booking.Status = BookingStatus.Confirmed;
            booking.ConfirmedOnUtc = DateTime.UtcNow;

            return await bookingRepository.UpdateAsync(booking);
        }

        public async Task<bool> RejectBookingAsync(Guid bookingId, Guid ownerId)
        {
            var booking = await bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            if (!IsOwnerOfApartment(booking, ownerId)) return false;

            booking.Status = BookingStatus.Rejected;
            booking.RejectedOnUtc = DateTime.UtcNow;

            return await bookingRepository.UpdateAsync(booking);
        }


        public async Task<bool> CompleteBookingAsync(Guid bookingId, Guid ownerId)
        {
            var booking = await bookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            if (!IsOwnerOfApartment(booking, ownerId)) return false;

            booking.Status = BookingStatus.Completed;
            booking.CompletedOnUtc = DateTime.UtcNow;

            return await bookingRepository.UpdateAsync(booking);
        }

        private bool IsOwnerOfApartment(Booking booking, Guid ownerId)
        {
            foreach (var owner in booking.Apartment.Owners)
            {
                if (owner.UserId == ownerId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
