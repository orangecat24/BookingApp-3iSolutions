using Booking1.Application.Apartments.AddApartmentPhotos;
using Booking1.Application.Apartments.CreateApartment;
using Booking1.Application.Authentication;
using Booking1.Application.Owners;
using Booking1.Application.Users2;
using ClBooking1.Infrastructure.Apartments;
using ClBooking1.Infrastructure.Authentication;
using ClBooking1.Infrastructure.Owners;
using ClBooking1.Infrastructure.Users;
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

            return services;
        }

    }
}

