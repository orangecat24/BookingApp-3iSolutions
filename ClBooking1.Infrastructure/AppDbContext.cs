using Booking1.Domain.Enums;
using Booking1.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ClBooking1.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentPhoto> ApartmentPhotos { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Apartment>()
                .Property(a => a.Amenities) // kap amenitin 
                .HasConversion(
                    v => string.Join(",", v), // join stringet
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(val => Enum.Parse<Amenity>(val))
                          .ToList()
                );

            modelBuilder.Entity<Review>()
                 .HasOne(r => r.User)
                 .WithMany()
                 .HasForeignKey(r => r.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Booking)
                .WithMany()
                .HasForeignKey(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
}
