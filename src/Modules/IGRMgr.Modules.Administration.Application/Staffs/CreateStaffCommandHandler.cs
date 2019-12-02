using IGRMgr.Modules.Administration.Application.Configuration.Processing;
using IGRMgr.Modules.Administration.Domain.Staffs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Staffs
{
    internal class CreateStaffCommandHandler : ICommandHandler<CreateStaffCommand>
    {
        private readonly IStaffRepository _staffRepository;

        public CreateStaffCommandHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<Unit> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            var staff = Staff.Create(request.Id, request.FirstName, request.LastName, request.MiddleName, request.Email,
                request.PhoneNumber, request.StaffNo, request.Gender, request.DateOfBirth, request.JobTitle);

            await _staffRepository.AddAsync(staff);

            return Unit.Value;
        }
    }
}
