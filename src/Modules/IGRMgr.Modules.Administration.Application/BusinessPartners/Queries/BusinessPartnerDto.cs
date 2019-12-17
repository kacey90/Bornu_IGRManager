using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.BusinessPartners.Queries
{
    public class BusinessPartnerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
