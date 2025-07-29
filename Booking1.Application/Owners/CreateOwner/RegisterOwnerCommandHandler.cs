using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking1.Application.Owners.CreateOwner
{
    public class RegisterOwnerCommandHandler : IRequestHandler<RegisterOwnerCommand, Guid>
    {

        private readonly IOwnerRepository _ownerRepository;

        private readonly RegisterOwnerCommandValidation _validationRules;
        public Task<Guid> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
