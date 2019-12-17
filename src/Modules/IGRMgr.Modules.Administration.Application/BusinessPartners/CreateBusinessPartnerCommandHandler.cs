using IGRMgr.Modules.Administration.Application.Configuration.Processing;
using IGRMgr.Modules.Administration.Domain.BusinessPartners;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    public class CreateBusinessPartnerCommandHandler : ICommandHandler<CreateBusinessPartnerCommand>
    {
        private readonly IBusinessPartnerRepository _businessPartnerRepository;
        private readonly IBusinessPartnersCounter _businessPartnersCounter;

        public CreateBusinessPartnerCommandHandler(IBusinessPartnerRepository businessPartnerRepository,
            IBusinessPartnersCounter businessPartnersCounter)
        {
            _businessPartnerRepository = businessPartnerRepository;
            _businessPartnersCounter = businessPartnersCounter;
        }

        public async Task<Unit> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            var businessPartner = BusinessPartner.Create(request.Id, request.FirstName, request.LastName, request.PhoneNumber,
                request.Email, null, null, null, null, _businessPartnersCounter);

            await _businessPartnerRepository.AddAsync(businessPartner);

            return Unit.Value;
        }
    }
}
