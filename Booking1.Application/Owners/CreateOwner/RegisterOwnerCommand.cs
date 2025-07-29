using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Owners.CreateOwner
{
    public class RegisterOwnerCommand : IRequest<Guid>
    {
        CreateOwnerDto CreateOwnerDto { get; set; }

    }
}
