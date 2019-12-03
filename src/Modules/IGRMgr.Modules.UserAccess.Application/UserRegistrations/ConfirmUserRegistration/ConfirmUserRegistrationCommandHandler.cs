using IGRMgr.Modules.UserAccess.Application.Configuration.Processing;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration
{
    public class ConfirmUserRegistrationCommandHandler : ICommandHandler<ConfirmUserRegistrationCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;

        public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }

        public async Task<Unit> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var userRegistration = 
                await _userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

            userRegistration.Confirm();
            return Unit.Value;
        }
    }
}
