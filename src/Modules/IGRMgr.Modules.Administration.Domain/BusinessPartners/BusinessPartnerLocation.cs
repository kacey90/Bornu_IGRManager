using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.BusinessPartners
{
    public class BusinessPartnerLocation : ValueObject
    {
        public BusinessPartnerLocation(string street, string city, string postalCode, string state)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            State = state;
        }
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string State { get; }
    }
}
