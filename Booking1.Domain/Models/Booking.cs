using Booking1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Domain.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Apartment")]
        public Guid ApartmentId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal PriceForPeriod { get; set; }
        public decimal CleaningFee { get; set; }
        public decimal AmenitiesUpCharge { get; set; }
        public decimal TotalPrice { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ConfirmedOnUtc { get; set; }
        public DateTime? RejectedOnUtc { get; set; }
        public DateTime? CompletedOnUtc { get; set; }

        public Apartment Apartment { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
