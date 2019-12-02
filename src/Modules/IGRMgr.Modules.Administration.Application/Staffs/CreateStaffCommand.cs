using IGRMgr.Modules.Administration.Application.Configuration.Processing.InternalCommands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.Administration.Application.Staffs
{
    internal class CreateStaffCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateStaffCommand(Guid id, Guid staffId, string firstName, string lastName, string middleName,
                                    string email, string phoneNumber, string staffNo, string gender,
                                    DateTime? dateOfBirth, string jobTitle)
            : base(id)
        {
            StaffId = staffId;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            PhoneNumber = phoneNumber;
            StaffNo = staffNo;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            JobTitle = jobTitle;
        }

        internal Guid StaffId { get; }
        internal string FirstName { get; }
        internal string LastName { get; }
        internal string MiddleName { get; }
        internal string Email { get; }
        internal string PhoneNumber { get; }
        internal string StaffNo { get; }
        internal string Gender { get; }
        internal DateTime? DateOfBirth { get; }
        internal string JobTitle { get; }
    }
}
