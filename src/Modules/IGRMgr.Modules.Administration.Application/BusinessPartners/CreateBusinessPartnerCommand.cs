using IGRMgr.Modules.Administration.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners
{
    public class CreateBusinessPartnerCommand : CommandBase
    {
        public CreateBusinessPartnerCommand(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phone;
            Email = email;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
    }
}
