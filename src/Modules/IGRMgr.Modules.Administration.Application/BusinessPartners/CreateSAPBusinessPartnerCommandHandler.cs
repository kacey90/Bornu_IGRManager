using IGRMgr.Modules.Administration.Application.Configuration.Processing;
using IGRMgr.Modules.Administration.Application.HttpClients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    internal class CreateSAPBusinessPartnerCommandHandler : ICommandHandler<CreateSAPBusinessPartnerCommand>
    {
        private readonly SAPHttpClient _sapApi;

        internal CreateSAPBusinessPartnerCommandHandler(SAPHttpClient sapApi)
        {
            _sapApi = sapApi;
        }

        public Task<Unit> Handle(CreateSAPBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
