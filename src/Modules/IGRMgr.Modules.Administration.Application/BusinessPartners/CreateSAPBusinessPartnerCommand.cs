using IGRMgr.Modules.Administration.Application.Configuration.Processing.InternalCommands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    internal class CreateSAPBusinessPartnerCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateSAPBusinessPartnerCommand(Guid id,
            string firstName, 
            string lastName, 
            string phone, 
            string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phone;
            EmailAddress = email;
        }

        internal string FirstName { get; }

        internal string LastName { get; }

        internal string PhoneNumber { get; }

        internal string EmailAddress { get; }
    }
}
