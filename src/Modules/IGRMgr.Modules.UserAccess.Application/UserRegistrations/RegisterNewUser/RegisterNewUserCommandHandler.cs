using IGRMgr.Modules.UserAccess.Application.Configuration.Processing;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUsersCounter _usersCounter;

        public RegisterNewUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUsersCounter usersCounter)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _usersCounter = usersCounter;
        }

        public async Task<Unit> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var userRegistration = UserRegistration.RegisterNewUser(Guid.NewGuid(), request.Email, request.FirstName,
                request.LastName, request.MiddleName, request.Gender, request.PhoneNumber, request.Role, _usersCounter);

            await _userRegistrationRepository.AddAsync(userRegistration);

            return Unit.Value;
        }
    }
}
