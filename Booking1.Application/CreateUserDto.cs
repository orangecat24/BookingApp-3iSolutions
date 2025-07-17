using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application
{
    public class CreateUserDto
    {
        // dto tek application

        public string FirstName { get; set; }

        public string LastName { get; set; } 

        public string Email { get; set; }

        public string Password { get; set; }
        
        public string Country { get; set; } 
    }
}
