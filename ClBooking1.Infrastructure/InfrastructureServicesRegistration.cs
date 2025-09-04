using Booking1.Application.Apartments;
using Booking1.Application.Apartments.AddApartmentPhotos;
using Booking1.Application.Apartments.CreateApartment;
using Booking1.Application.Authentication;
using Booking1.Application.Bookings;
using Booking1.Application.Bookings.UpdateBooking;
using Booking1.Application.Owners;
using Booking1.Application.Owners.CreateOwner;
using Booking1.Application.Reviews;
using Booking1.Application.Reviews.CreateReview;
using Booking1.Application.Users;
using Booking1.Application.Users2;
using ClBooking1.Infrastructure.Apartments;
using ClBooking1.Infrastructure.Authentication;
using ClBooking1.Infrastructure.Bookings;
using ClBooking1.Infrastructure.Owners;
using ClBooking1.Infrastructure.Reviews;
using ClBooking1.Infrastructure.Users;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClBooking1.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // SERVICES AND REPOSITORIES REG

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IApartmentService, ApartmentService>();

            services.AddScoped<IApartmentPhotoRepository, ApartmentPhotoRepository>();
            services.AddScoped<IApartmentPhotoService, AparmentPhotoService>();

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingService, BookingService>();
            // Reviews
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();



            // VALID REG

            services.AddScoped<IValidator<CreateUserDto>, CreateUserValidation>();
            services.AddScoped<IValidator<CreateOwnerDto>, CreateOwnerValidation>();
            services.AddScoped<IValidator<CreateApartmentDto>, CreateApartmentValidation>();
            services.AddScoped<IValidator<AddApartmentPhotoDto>, AddApartmentPhotoValidator>();
            services.AddScoped<IValidator<CreateBookingDto>, CreateBookingValidator>();
            services.AddScoped<IValidator<UpdateBookingDto>, UpdateBookingValidator>();
            services.AddScoped<IValidator<CreateReviewDto>, CreateReviewValidator>();

            return services;
        }

    }
}

