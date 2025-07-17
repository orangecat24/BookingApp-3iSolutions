using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Domain.Models
{
    public class ApartmentPhoto
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Apartment")]
        public Guid ApartmentId { get; set; }

        [Required]
        public string ImageBase64 { get; set; } = null!;

        [Required]
        public string ImageName { get; set; } = null!;

        [Required]
        public string ImageType { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public Apartment Apartment { get; set; } = null!;
    }
} 
