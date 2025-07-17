using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Domain.Models
{
    public class Review
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Booking")]
        public Guid BookingId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public Booking Booking { get; set; }
    }
}
