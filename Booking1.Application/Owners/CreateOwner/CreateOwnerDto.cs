using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Owners.CreateOwner
{
    public class CreateOwnerDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string IdentityCardNumber { get; set; } = null!;
        public string BankAccount { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

    }
}
