using IGRMgr.Modules.UserAccess.Application.Configuration.Processing;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using IGRMgr.Modules.UserAccess.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.Users.CreateUser
{
    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            IUserRegistrationRepository userRegistrationRepository,
            IUserRepository userRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRegistration = await _userRegistrationRepository.GetByIdAsync(request.UserRegistrationId);

            var user = userRegistration.CreateUser();

            //var password = PasswordManager.GeneratePassword();
            //user._password = password;

            await _userRepository.AddAsync(user);

            return Unit.Value;
        }
    }
}
