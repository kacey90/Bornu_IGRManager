using IGRMgr.Modules.Administration.Domain.Staffs.Events;
using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Domain.Staffs
{
    public class Staff : Entity, IAggregateRoot
    {
        public StaffId Id { get; private set; }

        private string _firstName;

        private string _lastName;

        private string _middleName;

        private string _name;

        private string _email;

        private string _phoneNumber;

        private string _staffNo;

        private StaffGender _gender;

        private DateTime? _dateOfBirth;

        private string _jobTitle;

        private bool _isActive;

        private DateTime _createDate;

        private Staff()
        {

        }

        private Staff(Guid id, string firstName, string lastName, string middleName, string email, string phoneNumber, 
            string staffNo, string gender, DateTime? dateOfBirth, string jobTitle)
        {
            this.Id = new StaffId(id);
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            _name = !string.IsNullOrWhiteSpace(middleName) ? $"{lastName}, {firstName} {middleName}" : $"{firstName} {lastName}";
            _email = email;
            _phoneNumber = phoneNumber;
            _staffNo = staffNo;
            _gender = new StaffGender(gender);
            _dateOfBirth = dateOfBirth;
            _jobTitle = jobTitle;
            _isActive = true;
            _createDate = DateTime.Now;

            this.AddDomainEvent(new StaffCreatedDomainEvent(this.Id));
        }

        public static Staff Create(Guid id, string firstName, string lastName, string middleName, string email, 
            string phoneNumber, string staffNo, string gender, DateTime? dateOfBirth, string jobTitle)
        {
            return new Staff(id, firstName, lastName, middleName, email, phoneNumber, staffNo, gender, dateOfBirth, jobTitle);
        }
    }
}
