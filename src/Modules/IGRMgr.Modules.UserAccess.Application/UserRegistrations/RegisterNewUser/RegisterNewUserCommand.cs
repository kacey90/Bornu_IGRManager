using IGRMgr.Modules.UserAccess.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class RegisterNewUserCommand : CommandBase
    {
        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }
        
        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Role { get; }

        public RegisterNewUserCommand(string email, string firstName, string lastName, string middleName, string phoneNumber, 
            string gender, string role)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Role = role;
        }
    }
}
